using System;

namespace UnityEngine
{
	// Token: 0x02000278 RID: 632
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x000336BC File Offset: 0x000318BC
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000336D4 File Offset: 0x000318D4
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x000336EC File Offset: 0x000318EC
		public string componentMenu
		{
			get
			{
				return this.m_AddComponentMenu;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000336F4 File Offset: 0x000318F4
		public int componentOrder
		{
			get
			{
				return this.m_Ordering;
			}
		}

		// Token: 0x040009D6 RID: 2518
		private string m_AddComponentMenu;

		// Token: 0x040009D7 RID: 2519
		private int m_Ordering;
	}
}
