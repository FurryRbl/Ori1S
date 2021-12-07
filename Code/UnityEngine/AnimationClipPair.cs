using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationClipPair
	{
		// Token: 0x0400047E RID: 1150
		public AnimationClip originalClip;

		// Token: 0x0400047F RID: 1151
		public AnimationClip overrideClip;
	}
}
