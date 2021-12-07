using System;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	public sealed class WaitUntil : CustomYieldInstruction
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000024BC File Offset: 0x000006BC
		public WaitUntil(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000024CC File Offset: 0x000006CC
		public override bool keepWaiting
		{
			get
			{
				return !this.m_Predicate();
			}
		}

		// Token: 0x0400006A RID: 106
		private Func<bool> m_Predicate;
	}
}
