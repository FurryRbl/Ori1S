using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B6 RID: 438
	[RequiredByNativeCode]
	public struct HumanBone
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00019BF8 File Offset: 0x00017DF8
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x00019C00 File Offset: 0x00017E00
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00019C0C File Offset: 0x00017E0C
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x00019C14 File Offset: 0x00017E14
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x040004F2 RID: 1266
		private string m_BoneName;

		// Token: 0x040004F3 RID: 1267
		private string m_HumanName;

		// Token: 0x040004F4 RID: 1268
		public HumanLimit limit;
	}
}
