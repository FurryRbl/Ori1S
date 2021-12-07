using System;

namespace UnityEngine
{
	// Token: 0x0200027A RID: 634
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x06002568 RID: 9576 RVA: 0x00033740 File Offset: 0x00031940
		public ContextMenu(string name)
		{
			this.m_ItemName = name;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x00033750 File Offset: 0x00031950
		public string menuItem
		{
			get
			{
				return this.m_ItemName;
			}
		}

		// Token: 0x040009DB RID: 2523
		private string m_ItemName;
	}
}
