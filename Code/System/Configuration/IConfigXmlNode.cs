using System;

namespace System.Configuration
{
	// Token: 0x020001E1 RID: 481
	internal interface IConfigXmlNode
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060010B7 RID: 4279
		string Filename { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060010B8 RID: 4280
		int LineNumber { get; }
	}
}
