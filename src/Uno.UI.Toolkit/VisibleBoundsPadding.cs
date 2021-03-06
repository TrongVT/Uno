﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Uno.Collections;
using Uno.Extensions;
using Uno.Logging;
using Uno.UI.Toolkit.Extensions;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#if XAMARIN_IOS
using UIKit;
#endif

namespace Uno.UI.Toolkit
{
	/// <summary>
	/// A behavior which automatically adds padding to a control that ensures its content will always be inside 
	/// the <see cref="ApplicationView.VisibleBounds"/> of the application. Set PaddingMask to 'All' to enable this behavior,
	/// or set PaddingMask to another value to enable it only on a particular side or sides.
	/// </summary>
	public static class VisibleBoundsPadding
	{
		[Flags]
		public enum PaddingMask
		{
			All = Left | Right | Top | Bottom,
			None = 0,
			Top = 1,
			Bottom = 2,
			Left = 4,
			Right = 8
		}

		/// <summary>
		/// The padding of the visible area relative to the entire window.
		/// </summary>
		/// <remarks>This will be 0 if the entire window is 'safe' for content.</remarks>
		public static Thickness WindowPadding
		{
			get
			{
				var visibleBounds = ApplicationView.GetForCurrentView().VisibleBounds;
				var bounds = Window.Current.Bounds;
				return new Thickness(visibleBounds.Left - bounds.Left, visibleBounds.Top - bounds.Top, bounds.Right - visibleBounds.Right, bounds.Bottom - visibleBounds.Bottom);
			}
		}

		/// <summary>
		/// VisibleBounds offset to the reference frame of the window Bounds.
		/// </summary>
		private static Rect OffsetVisibleBounds
		{
			get
			{
				var visibleBounds = ApplicationView.GetForCurrentView().VisibleBounds;
				var bounds = Window.Current.Bounds;
				visibleBounds.X -= bounds.X;
				visibleBounds.Y -= bounds.Y;

				return visibleBounds;
			}
		}

		public static PaddingMask GetPaddingMask(DependencyObject obj)
			=> (PaddingMask)obj.GetValue(PaddingMaskProperty);

		/// <summary>
		/// Set the <see cref="PaddingMask"/> to use on this property. A mask of <see cref="PaddingMask.All"/> will apply visible bounds 
		/// padding on all sides, a mask of <see cref="PaddingMask.Bottom"/> will adjust only the bottom padding, etc. The different options 
		/// can be combined as bit flags. 
		/// </summary>
		public static void SetPaddingMask(DependencyObject obj, PaddingMask value)
			=> obj.SetValue(PaddingMaskProperty, value);

		public static readonly DependencyProperty PaddingMaskProperty =
			DependencyProperty.RegisterAttached("PaddingMask", typeof(PaddingMask), typeof(VisibleBoundsPadding), new PropertyMetadata(PaddingMask.None, OnIsPaddingMaskChanged));

		private static void OnIsPaddingMaskChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
			=> VisibleBoundsDetails.GetInstance(dependencyObject as FrameworkElement).OnIsPaddingMaskChanged((PaddingMask)args.OldValue, (PaddingMask)args.NewValue);

		/// <summary>
		/// If false, ApplicationView.VisibleBounds and Window.Current.Bounds have different aspect ratios (eg portrait vs landscape) which 
		/// might arise transiently when the screen orientation changes.
		/// </summary>
		private static bool AreBoundsAspectRatiosConsistent => ApplicationView.GetForCurrentView().VisibleBounds.GetOrientation() == Window.Current.Bounds.GetOrientation();

		public class VisibleBoundsDetails
		{
			private static ConditionalWeakTable<FrameworkElement, VisibleBoundsDetails> _instances = new ConditionalWeakTable<FrameworkElement, VisibleBoundsDetails>();
			private FrameworkElement _owner;
			private TypedEventHandler<global::Windows.UI.ViewManagement.ApplicationView, object> _visibleBoundsChanged;
			private PaddingMask _paddingMask;
			private Thickness _originalPadding;
			private static Dictionary<Type, DependencyProperty> _paddingPropertyCache = new Dictionary<Type, DependencyProperty>();

			internal VisibleBoundsDetails(FrameworkElement owner)
			{
				_owner = owner;

				_originalPadding = (Thickness)(_owner.GetValue(GetPaddingProperty()) ?? new Thickness(0));

				_visibleBoundsChanged = (s2, e2) => UpdatePadding();

				_owner.LayoutUpdated += (s, e) => UpdatePadding();
				_owner.Loaded += (s, e) => ApplicationView.GetForCurrentView().VisibleBoundsChanged += _visibleBoundsChanged;
				_owner.Unloaded += (s, e) => ApplicationView.GetForCurrentView().VisibleBoundsChanged -= _visibleBoundsChanged;
			}

			private void UpdatePadding()
			{
				if (Window.Current.Content == null)
				{
					return;
				}

				if (!AreBoundsAspectRatiosConsistent)
				{
					return;
				}

				Thickness visibilityPadding;
				
				if (WindowPadding != default(Thickness))
				{
					var scrollAncestor = GetScrollAncestor();

					// If the owner view is scrollable, the visibility of interest is that of the scroll viewport.
					var fixedControl = scrollAncestor ?? _owner;

					var controlBounds = GetRelativeBounds(fixedControl, Window.Current.Content);

					visibilityPadding = CalculateVisibilityPadding(OffsetVisibleBounds, controlBounds);

					if (scrollAncestor != null)
					{
						visibilityPadding = AdjustScrollablePadding(visibilityPadding, scrollAncestor);
					}
				}
				else
				{
					visibilityPadding = default(Thickness);
				}

				var padding = CalculateAppliedPadding(_paddingMask, visibilityPadding);

				ApplyPadding(padding);
			}

			/// <summary>
			/// Calculate the padding required to keep the view entirely within the 'safe' visible bounds of the window.
			/// </summary>
			/// <param name="visibleBounds">The safe visible bounds of the window.</param>
			/// <param name="controlBounds">The bounds of the control, in the window's coordinates.</param>
			private Thickness CalculateVisibilityPadding(Rect visibleBounds, Rect controlBounds)
			{
				var windowPadding = WindowPadding;

				var left = Math.Min(visibleBounds.Left - controlBounds.Left, windowPadding.Left);
				var top = Math.Min(visibleBounds.Top - controlBounds.Top, windowPadding.Top);
				var right = Math.Min(controlBounds.Right - visibleBounds.Right, windowPadding.Right);
				var bottom = Math.Min(controlBounds.Bottom - visibleBounds.Bottom, windowPadding.Bottom);

				return new Thickness(left, top, right, bottom);
			}

			/// <summary>
			/// Apply adjustments when target view is inside of a ScrollViewer.
			/// </summary>
			private Thickness AdjustScrollablePadding(Thickness visibilityPadding, ScrollViewer scrollAncestor)
			{
				var scrollableRoot = scrollAncestor.Content as FrameworkElement;
#if XAMARIN
				if (scrollableRoot is ItemsPresenter)
				{
					// This implies we're probably inside a ListView, in which case the reasoning breaks down in Uno (because ItemsPresenter 
					// is *outside* the scrollable region); we skip the adjustment and hope for the best.
					scrollableRoot = null;
				}
#endif
				if (scrollableRoot != null)
				{
					// Get the spacing already provided by the alignment of the child relative to it ancestor at the root of the scrollable hierarchy.
					var controlBounds = GetRelativeBounds(_owner, scrollableRoot);
					var rootBounds = new Rect(0, 0, scrollableRoot.ActualWidth, scrollableRoot.ActualHeight);

					// Adjust for existing spacing
					visibilityPadding.Left -= controlBounds.Left - rootBounds.Left;
					visibilityPadding.Top -= controlBounds.Top - rootBounds.Top;
					visibilityPadding.Right -= rootBounds.Right - controlBounds.Right;
					visibilityPadding.Bottom -= rootBounds.Bottom - controlBounds.Bottom;
				}

				return visibilityPadding;
			}

			/// <summary>
			/// Calculate the padding to apply to the view, based on the selected PaddingMask.
			/// </summary>
			/// <param name="mask">The PaddingMask settings.</param>
			/// <param name="visibilityPadding">The padding required to keep the view entirely within the 'safe' visible bounds of the window.</param>
			/// <returns>The padding that will actually be set on the view.</returns>
			private Thickness CalculateAppliedPadding(PaddingMask mask, Thickness visibilityPadding)
			{
				// Apply left padding if the PaddingMask is "left" or "all"
				var left = mask.HasFlag(PaddingMask.Left)
					? Math.Max(_originalPadding.Left, visibilityPadding.Left)
					: _originalPadding.Left;
				// Apply top padding if the PaddingMask is "top" or "all"
				var top = mask.HasFlag(PaddingMask.Top)
					? Math.Max(_originalPadding.Top, visibilityPadding.Top)
					: _originalPadding.Top;
				// Apply right padding if the PaddingMask is "right" or "all"
				var right = mask.HasFlag(PaddingMask.Right)
					? Math.Max(_originalPadding.Right, visibilityPadding.Right)
					: _originalPadding.Right;
				// Apply bottom padding if the PaddingMask is "bottom" or "all"
				var bottom = mask.HasFlag(PaddingMask.Bottom)
					? Math.Max(_originalPadding.Bottom, visibilityPadding.Bottom)
					: _originalPadding.Bottom;

				return new Thickness(left, top, right, bottom);
			}

			private void ApplyPadding(Thickness padding)
			{
				var property = GetPaddingProperty();

				if (property != null)
				{
					_owner.SetValue(property, padding);
				}
			}

			private DependencyProperty GetPaddingProperty()
			{
				switch (_owner)
				{
					case Grid g:
						return Grid.PaddingProperty;

					case StackPanel g:
						return StackPanel.PaddingProperty;

					case Control c:
						return Control.PaddingProperty;

					case ContentPresenter cp:
						return ContentPresenter.PaddingProperty;

					case Border b:
						return Border.PaddingProperty;
#if XAMARIN
					// This provides support for external Panel implementations on Uno.
					case Panel p:
						return Panel.PaddingProperty;
#endif
				}

				return GetDefaultPaddingProperty();
			}

			private DependencyProperty GetDefaultPaddingProperty()
			{
				var ownerType = _owner.GetType();

				if (!_paddingPropertyCache.TryGetValue(ownerType, out var property))
				{
					property = ownerType
						.GetTypeInfo()
						.GetDeclaredProperty("PaddingProperty")
						?.GetValue(null) as DependencyProperty;

					if (property == null)
					{
						property = ownerType
							.GetTypeInfo()
							.GetDeclaredField("PaddingProperty")
							?.GetValue(null) as DependencyProperty;
					}

					_paddingPropertyCache[ownerType] = property;

					if (property == null)
					{
						this.Log().Warn($"The Padding dependency property does not exist on {ownerType}");
					}
				}

				return property;
			}

			internal static VisibleBoundsDetails GetInstance(FrameworkElement element)
				=> _instances.GetValue(element, e => new VisibleBoundsDetails(e));

			internal void OnIsPaddingMaskChanged(PaddingMask oldValue, PaddingMask newValue)
			{
				_paddingMask = newValue;

				UpdatePadding();
			}

			private ScrollViewer GetScrollAncestor()
			{
				return _owner.FindFirstParent<ScrollViewer>();
			}

			private static Rect GetRelativeBounds(FrameworkElement boundsOf, UIElement relativeTo)
			{
				return boundsOf
					.TransformToVisual(relativeTo)
					.TransformBounds(new Rect(0, 0, boundsOf.ActualWidth, boundsOf.ActualHeight));
			}
		}
	}
}