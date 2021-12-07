using System;

namespace System.Security.Policy
{
	// Token: 0x02000643 RID: 1603
	internal interface IBuiltInEvidence
	{
		// Token: 0x06003D13 RID: 15635
		int GetRequiredSize(bool verbose);

		// Token: 0x06003D14 RID: 15636
		int InitFromBuffer(char[] buffer, int position);

		// Token: 0x06003D15 RID: 15637
		int OutputToBuffer(char[] buffer, int position, bool verbose);
	}
}
