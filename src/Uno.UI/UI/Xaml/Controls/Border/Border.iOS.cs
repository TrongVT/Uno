﻿#if !HAS_UI_TESTS
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Uno.Disposables;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;

#if XAMARIN_IOS_UNIFIED
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using Windows.UI.Xaml;
#endif

namespace Windows.UI.Xaml.Controls
{
    public partial class Border
	{
		private SerialDisposable _brushColorChanged = new SerialDisposable();
		private BorderLayerRenderer _borderRenderer = new BorderLayerRenderer();

		public Border()
		{
			this.RegisterLoadActions(() => UpdateBorderLayer(), () => _borderRenderer.Clear());
		}

        partial void OnBorderBrushChangedPartial()
		{
			UpdateBorderLayer();
		}

		protected override void OnAfterArrange()
		{
			base.OnAfterArrange();
			UpdateBorderLayer();
		}

		protected override void OnUnloaded()
		{
			base.OnUnloaded();
			_borderRenderer.Clear();
        }

		protected override void OnLoaded()
		{
			base.OnLoaded();
			UpdateBorderLayer();
		}

		private void UpdateBorderLayer(UIImage backgroundImage = null)
		{
			if (IsLoaded)
			{
				backgroundImage = backgroundImage ?? (Background as ImageBrush)?.ImageSource?.ImageData;

                _borderRenderer.UpdateLayer(
					this,
					Background,
					BorderThickness,
					BorderBrush,
					CornerRadius,
					backgroundImage
				);
			}

			base.SetNeedsDisplay();
		}

		protected override void OnBackgroundChanged(DependencyPropertyChangedEventArgs args)
		{
			// Don't call base, we need to keep UIView.BackgroundColor set to transparent
			// because we're overriding draw.

			var old = args.OldValue as ImageBrush;
			if (old != null)
			{
				old.ImageChanged -= OnBackgroundImageBrushChanged;
            }
			var imgBrush = args.NewValue as ImageBrush;
			if (imgBrush != null)
			{
				imgBrush.ImageChanged += OnBackgroundImageBrushChanged;
			}
			else
			{
				UpdateBorderLayer();
			}
		}

		private void OnBackgroundImageBrushChanged(UIImage backgroundImage)
		{
			UpdateBorderLayer(backgroundImage);
		}

		partial void OnBorderThicknessChangedPartial(Thickness oldValue, Thickness newValue)
		{
			UpdateBorderLayer();
			base.SetNeedsLayout();
		}

        partial void OnPaddingChangedPartial(Thickness oldValue, Thickness newValue)
		{
			UpdateBorderLayer();
			base.SetNeedsLayout();
		}

        partial void OnChildChangedPartial(UIView previousValue, UIView newValue)
		{
			previousValue?.RemoveFromSuperview();

            AddSubview(newValue);

			UpdateBorderLayer();
		}

        partial void OnCornerRadiusUpdatedPartial(CornerRadius oldValue, CornerRadius newValue)
		{
			UpdateBorderLayer();
			base.SetNeedsLayout();
		}
	}
}
#endif