using System;

namespace System.Configuration
{
	// Token: 0x02000049 RID: 73
	internal interface IConfigXmlNode
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002A3 RID: 675
		string Filename { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002A4 RID: 676
		int LineNumber { get; }
	}
}
