#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Graphics.Imaging
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial struct BitmapBounds 
	{
		// Forced skipping of method Windows.Graphics.Imaging.BitmapBounds.BitmapBounds()
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  uint X;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  uint Y;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  uint Width;
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		public  uint Height;
		#endif
	}
}
