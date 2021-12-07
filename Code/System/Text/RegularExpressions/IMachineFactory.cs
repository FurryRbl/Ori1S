using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000466 RID: 1126
	internal interface IMachineFactory
	{
		// Token: 0x06002849 RID: 10313
		IMachine NewInstance();

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600284A RID: 10314
		// (set) Token: 0x0600284B RID: 10315
		IDictionary Mapping { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600284C RID: 10316
		int GroupCount { get; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600284D RID: 10317
		// (set) Token: 0x0600284E RID: 10318
		int Gap { get; set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600284F RID: 10319
		// (set) Token: 0x06002850 RID: 10320
		string[] NamesMapping { get; set; }
	}
}
