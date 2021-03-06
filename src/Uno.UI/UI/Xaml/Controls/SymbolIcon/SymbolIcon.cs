﻿#if XAMARIN

using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media;

namespace Windows.UI.Xaml.Controls
{
	public sealed partial class SymbolIcon : IconElement
	{
		private FontIcon _icon;

		public SymbolIcon()
		{
			_icon = new FontIcon();

			AddIconElementView(_icon);
		}

		public SymbolIcon(Symbol symbol) : this()
		{
			_icon.Glyph = new string((char)symbol, 1);
		}

		public Symbol Symbol
		{
			get { return (Symbol)GetValue(SymbolProperty); }
			set { SetValue(SymbolProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SymbolProperty =
			DependencyProperty.Register("Symbol", typeof(Symbol), typeof(SymbolIcon), new PropertyMetadata(Symbol.Home, OnSymbolChanged));

		private static void OnSymbolChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var symbol = dependencyObject as SymbolIcon;

			if(symbol != null)
			{
				symbol._icon.Glyph = new string((char)symbol.Symbol, 1);
			}
		}
	}
}
#endif