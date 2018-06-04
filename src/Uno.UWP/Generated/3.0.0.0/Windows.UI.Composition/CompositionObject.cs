#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.UI.Composition
{
	#if false || false || false || false
	[global::Uno.NotImplemented]
	#endif
	public  partial class CompositionObject : global::System.IDisposable
	{
		#if false || false || false || false
		[global::Uno.NotImplemented]
		public  global::Windows.UI.Composition.Compositor Compositor
		{
			get
			{
				throw new global::System.NotImplementedException("The member Compositor CompositionObject.Compositor is not implemented in Uno.");
			}
		}
		#endif
		#if false || false || false || false
		[global::Uno.NotImplemented]
		public  global::Windows.UI.Core.CoreDispatcher Dispatcher
		{
			get
			{
				throw new global::System.NotImplementedException("The member CoreDispatcher CompositionObject.Dispatcher is not implemented in Uno.");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  global::Windows.UI.Composition.CompositionPropertySet Properties
		{
			get
			{
				throw new global::System.NotImplementedException("The member CompositionPropertySet CompositionObject.Properties is not implemented in Uno.");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  global::Windows.UI.Composition.ImplicitAnimationCollection ImplicitAnimations
		{
			get
			{
				throw new global::System.NotImplementedException("The member ImplicitAnimationCollection CompositionObject.ImplicitAnimations is not implemented in Uno.");
			}
			set
			{
				global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "ImplicitAnimationCollection CompositionObject.ImplicitAnimations");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  string Comment
		{
			get
			{
				throw new global::System.NotImplementedException("The member string CompositionObject.Comment is not implemented in Uno.");
			}
			set
			{
				global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "string CompositionObject.Comment");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  global::Windows.System.DispatcherQueue DispatcherQueue
		{
			get
			{
				throw new global::System.NotImplementedException("The member DispatcherQueue CompositionObject.DispatcherQueue is not implemented in Uno.");
			}
		}
		#endif
		// Forced skipping of method Windows.UI.Composition.CompositionObject.Compositor.get
		// Forced skipping of method Windows.UI.Composition.CompositionObject.Dispatcher.get
		// Forced skipping of method Windows.UI.Composition.CompositionObject.Properties.get
		#if false || false || false || false
		[global::Uno.NotImplemented]
		public  void StartAnimation( string propertyName,  global::Windows.UI.Composition.CompositionAnimation animation)
		{
			global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "void CompositionObject.StartAnimation(string propertyName, CompositionAnimation animation)");
		}
		#endif
		#if false || false || false || false
		[global::Uno.NotImplemented]
		public  void StopAnimation( string propertyName)
		{
			global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "void CompositionObject.StopAnimation(string propertyName)");
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  void Dispose()
		{
			global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "void CompositionObject.Dispose()");
		}
		#endif
		// Forced skipping of method Windows.UI.Composition.CompositionObject.Comment.get
		// Forced skipping of method Windows.UI.Composition.CompositionObject.Comment.set
		// Forced skipping of method Windows.UI.Composition.CompositionObject.ImplicitAnimations.get
		// Forced skipping of method Windows.UI.Composition.CompositionObject.ImplicitAnimations.set
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  void StartAnimationGroup( global::Windows.UI.Composition.ICompositionAnimationBase value)
		{
			global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "void CompositionObject.StartAnimationGroup(ICompositionAnimationBase value)");
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  void StopAnimationGroup( global::Windows.UI.Composition.ICompositionAnimationBase value)
		{
			global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.UI.Composition.CompositionObject", "void CompositionObject.StopAnimationGroup(ICompositionAnimationBase value)");
		}
		#endif
		// Forced skipping of method Windows.UI.Composition.CompositionObject.DispatcherQueue.get
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		[global::Uno.NotImplemented]
		public  global::Windows.UI.Composition.AnimationController TryGetAnimationController( string propertyName)
		{
			throw new global::System.NotImplementedException("The member AnimationController CompositionObject.TryGetAnimationController(string propertyName) is not implemented in Uno.");
		}
		#endif
		// Processing: System.IDisposable
	}
}