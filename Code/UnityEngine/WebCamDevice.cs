using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	[UsedByNativeCode]
	public struct WebCamDevice
	{
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00018920 File Offset: 0x00016B20
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x00018928 File Offset: 0x00016B28
		public bool isFrontFacing
		{
			get
			{
				return (this.m_Flags & 1) == 1;
			}
		}

		// Token: 0x0400047C RID: 1148
		internal string m_Name;

		// Token: 0x0400047D RID: 1149
		internal int m_Flags;
	}
}
