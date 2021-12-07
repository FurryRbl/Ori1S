using System;

namespace UnityEngine
{
	// Token: 0x02000202 RID: 514
	internal sealed class GUIAspectSizer : GUILayoutEntry
	{
		// Token: 0x06001FD5 RID: 8149 RVA: 0x00024694 File Offset: 0x00022894
		public GUIAspectSizer(float aspect, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.aspect = aspect;
			this.ApplyOptions(options);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000246C4 File Offset: 0x000228C4
		public override void CalcHeight()
		{
			this.minHeight = (this.maxHeight = this.rect.width / this.aspect);
		}

		// Token: 0x040007D1 RID: 2001
		private float aspect;
	}
}
