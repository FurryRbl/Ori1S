using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes a connection that exists to a given connection point.</summary>
	// Token: 0x020003E8 RID: 1000
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		/// <summary>Represents a pointer to the IUnknown interface on a connected advisory sink. The caller must call IUnknown::Release on this pointer when the CONNECTDATA structure is no longer needed.</summary>
		// Token: 0x04001296 RID: 4758
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		/// <summary>Represents a connection token that is returned from a call to <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" />.</summary>
		// Token: 0x04001297 RID: 4759
		public int dwCookie;
	}
}
