#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Perception
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public  partial class PerceptionTimestampHelper 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public static global::Windows.Perception.PerceptionTimestamp FromHistoricalTargetTime( global::System.DateTimeOffset targetTime)
		{
			throw new global::System.NotImplementedException("The member PerceptionTimestamp PerceptionTimestampHelper.FromHistoricalTargetTime(DateTimeOffset targetTime) is not implemented in Uno.");
		}
		#endif
	}
}
