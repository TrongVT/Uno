#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.ApplicationModel.Email
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public   enum EmailMessageDownloadState 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		PartiallyDownloaded,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Downloading,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Downloaded,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Failed,
		#endif
	}
	#endif
}
