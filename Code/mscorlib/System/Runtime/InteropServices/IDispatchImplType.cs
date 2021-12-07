using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which IDispatch implementation to use for a particular class.</summary>
	// Token: 0x0200039A RID: 922
	[ComVisible(true)]
	[Obsolete]
	[Serializable]
	public enum IDispatchImplType
	{
		/// <summary>Specifies that the common language runtime decides which IDispatch implementation to use.</summary>
		// Token: 0x04001137 RID: 4407
		SystemDefinedImpl,
		/// <summary>Specifies that the IDispatch implemenation is supplied by the runtime.</summary>
		// Token: 0x04001138 RID: 4408
		InternalImpl,
		/// <summary>Specifies that the IDispatch implementation is supplied by passing the type information for the object to the COM CreateStdDispatch API method.</summary>
		// Token: 0x04001139 RID: 4409
		CompatibleImpl
	}
}
