using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200007F RID: 127
	public class ClipperRegistry
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00015B94 File Offset: 0x00013D94
		protected ClipperRegistry()
		{
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public static ClipperRegistry instance
		{
			get
			{
				if (ClipperRegistry.s_Instance == null)
				{
					ClipperRegistry.s_Instance = new ClipperRegistry();
				}
				return ClipperRegistry.s_Instance;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public void Cull()
		{
			for (int i = 0; i < this.m_Clippers.Count; i++)
			{
				this.m_Clippers[i].PerformClipping();
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015C0C File Offset: 0x00013E0C
		public static void Register(IClipper c)
		{
			if (c == null)
			{
				return;
			}
			ClipperRegistry.instance.m_Clippers.AddUnique(c);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00015C28 File Offset: 0x00013E28
		public static void Unregister(IClipper c)
		{
			ClipperRegistry.instance.m_Clippers.Remove(c);
		}

		// Token: 0x0400023C RID: 572
		private static ClipperRegistry s_Instance;

		// Token: 0x0400023D RID: 573
		private readonly IndexedSet<IClipper> m_Clippers = new IndexedSet<IClipper>();
	}
}
