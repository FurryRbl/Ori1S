using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CC RID: 460
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TreePrototype
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0001A634 File Offset: 0x00018834
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x0001A63C File Offset: 0x0001883C
		public GameObject prefab
		{
			get
			{
				return this.m_Prefab;
			}
			set
			{
				this.m_Prefab = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0001A648 File Offset: 0x00018848
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x0001A650 File Offset: 0x00018850
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x0400058B RID: 1419
		internal GameObject m_Prefab;

		// Token: 0x0400058C RID: 1420
		internal float m_BendFactor;
	}
}
