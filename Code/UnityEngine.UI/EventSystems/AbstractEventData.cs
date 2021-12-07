using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000020 RID: 32
	public abstract class AbstractEventData
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003274 File Offset: 0x00001474
		public virtual void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003280 File Offset: 0x00001480
		public virtual void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000328C File Offset: 0x0000148C
		public virtual bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x0400004B RID: 75
		protected bool m_Used;
	}
}
