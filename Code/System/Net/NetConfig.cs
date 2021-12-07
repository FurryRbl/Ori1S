using System;

namespace System.Net
{
	// Token: 0x02000357 RID: 855
	internal class NetConfig : ICloneable
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x0005CA9C File Offset: 0x0005AC9C
		internal NetConfig()
		{
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0005CAAC File Offset: 0x0005ACAC
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040012CF RID: 4815
		internal bool ipv6Enabled;

		// Token: 0x040012D0 RID: 4816
		internal int MaxResponseHeadersLength = 64;
	}
}
