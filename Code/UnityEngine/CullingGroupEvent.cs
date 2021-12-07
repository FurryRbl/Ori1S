using System;

namespace UnityEngine
{
	// Token: 0x02000051 RID: 81
	public struct CullingGroupEvent
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x000047A8 File Offset: 0x000029A8
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x000047B0 File Offset: 0x000029B0
		public bool isVisible
		{
			get
			{
				return (this.m_ThisState & 128) != 0;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x000047C4 File Offset: 0x000029C4
		public bool wasVisible
		{
			get
			{
				return (this.m_PrevState & 128) != 0;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x000047D8 File Offset: 0x000029D8
		public bool hasBecomeVisible
		{
			get
			{
				return this.isVisible && !this.wasVisible;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000047F4 File Offset: 0x000029F4
		public bool hasBecomeInvisible
		{
			get
			{
				return !this.isVisible && this.wasVisible;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000480C File Offset: 0x00002A0C
		public int currentDistance
		{
			get
			{
				return (int)(this.m_ThisState & 127);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00004818 File Offset: 0x00002A18
		public int previousDistance
		{
			get
			{
				return (int)(this.m_PrevState & 127);
			}
		}

		// Token: 0x040000BD RID: 189
		private const byte kIsVisibleMask = 128;

		// Token: 0x040000BE RID: 190
		private const byte kDistanceMask = 127;

		// Token: 0x040000BF RID: 191
		private int m_Index;

		// Token: 0x040000C0 RID: 192
		private byte m_PrevState;

		// Token: 0x040000C1 RID: 193
		private byte m_ThisState;
	}
}
