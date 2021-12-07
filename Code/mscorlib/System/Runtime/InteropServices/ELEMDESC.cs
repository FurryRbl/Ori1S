using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC" /> instead.</summary>
	// Token: 0x02000388 RID: 904
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x040010F9 RID: 4345
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x040010FA RID: 4346
		public ELEMDESC.DESCUNION desc;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC.DESCUNION" /> instead.</summary>
		// Token: 0x02000389 RID: 905
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x040010FB RID: 4347
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x040010FC RID: 4348
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
