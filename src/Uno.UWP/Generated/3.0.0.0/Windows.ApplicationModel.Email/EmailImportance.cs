#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.ApplicationModel.Email
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public   enum EmailImportance 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Normal,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		High,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Low,
		#endif
	}
	#endif
}
