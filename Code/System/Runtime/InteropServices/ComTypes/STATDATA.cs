using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the STATDATA structure.</summary>
	// Token: 0x020004D4 RID: 1236
	public struct STATDATA
	{
		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> enumeration value that determines when the advisory sink is notified of changes in the data.</summary>
		// Token: 0x04001BBA RID: 7098
		public ADVF advf;

		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface that will receive change notifications.</summary>
		// Token: 0x04001BBB RID: 7099
		public IAdviseSink advSink;

		/// <summary>Represents the token that uniquely identifies the advisory connection. This token is returned by the method that sets up the advisory connection.</summary>
		// Token: 0x04001BBC RID: 7100
		public int connection;

		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure for the data of interest to the advise sink. The advise sink receives notification of changes to the data specified by this <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure.</summary>
		// Token: 0x04001BBD RID: 7101
		public FORMATETC formatetc;
	}
}
