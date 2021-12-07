using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DISPPARAMS" /> instead.</summary>
	// Token: 0x02000386 RID: 902
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x040010F4 RID: 4340
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x040010F5 RID: 4341
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x040010F6 RID: 4342
		public int cArgs;

		/// <summary>Represents the count of named arguments </summary>
		// Token: 0x040010F7 RID: 4343
		public int cNamedArgs;
	}
}
