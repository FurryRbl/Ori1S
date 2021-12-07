using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the type description and process transfer information for a variable, function, or a function parameter.</summary>
	// Token: 0x020003EB RID: 1003
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x040012A3 RID: 4771
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x040012A4 RID: 4772
		public ELEMDESC.DESCUNION desc;

		/// <summary>Contains information about an element. </summary>
		// Token: 0x020003EC RID: 1004
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x040012A5 RID: 4773
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x040012A6 RID: 4774
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
