using System;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	public sealed class WaitWhile : CustomYieldInstruction
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000249C File Offset: 0x0000069C
		public WaitWhile(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000024AC File Offset: 0x000006AC
		public override bool keepWaiting
		{
			get
			{
				return this.m_Predicate();
			}
		}

		// Token: 0x04000069 RID: 105
		private Func<bool> m_Predicate;
	}
}
