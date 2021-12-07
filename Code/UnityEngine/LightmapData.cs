using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightmapData
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00003B28 File Offset: 0x00001D28
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00003B30 File Offset: 0x00001D30
		public Texture2D lightmapFar
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00003B3C File Offset: 0x00001D3C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00003B44 File Offset: 0x00001D44
		public Texture2D lightmapNear
		{
			get
			{
				return this.m_Dir;
			}
			set
			{
				this.m_Dir = value;
			}
		}

		// Token: 0x040000A2 RID: 162
		internal Texture2D m_Light;

		// Token: 0x040000A3 RID: 163
		internal Texture2D m_Dir;
	}
}
