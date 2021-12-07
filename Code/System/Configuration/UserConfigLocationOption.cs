using System;

namespace System.Configuration
{
	// Token: 0x020001DA RID: 474
	internal enum UserConfigLocationOption : uint
	{
		// Token: 0x040004A4 RID: 1188
		Product = 32U,
		// Token: 0x040004A5 RID: 1189
		Product_VersionMajor,
		// Token: 0x040004A6 RID: 1190
		Product_VersionMinor,
		// Token: 0x040004A7 RID: 1191
		Product_VersionBuild = 36U,
		// Token: 0x040004A8 RID: 1192
		Product_VersionRevision = 40U,
		// Token: 0x040004A9 RID: 1193
		Company_Product = 48U,
		// Token: 0x040004AA RID: 1194
		Company_Product_VersionMajor,
		// Token: 0x040004AB RID: 1195
		Company_Product_VersionMinor,
		// Token: 0x040004AC RID: 1196
		Company_Product_VersionBuild = 52U,
		// Token: 0x040004AD RID: 1197
		Company_Product_VersionRevision = 56U,
		// Token: 0x040004AE RID: 1198
		Evidence = 64U,
		// Token: 0x040004AF RID: 1199
		Other = 32768U
	}
}
