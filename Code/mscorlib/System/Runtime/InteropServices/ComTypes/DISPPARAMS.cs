using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the arguments passed to a method or property by IDispatch::Invoke.</summary>
	// Token: 0x020003EA RID: 1002
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x0400129F RID: 4767
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x040012A0 RID: 4768
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x040012A1 RID: 4769
		public int cArgs;

		/// <summary>Represents the count of named arguments </summary>
		// Token: 0x040012A2 RID: 4770
		public int cNamedArgs;
	}
}
