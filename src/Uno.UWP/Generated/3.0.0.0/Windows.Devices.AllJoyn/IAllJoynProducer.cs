#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Devices.AllJoyn
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial interface IAllJoynProducer 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		void SetBusObject( global::Windows.Devices.AllJoyn.AllJoynBusObject busObject);
		#endif
	}
}
