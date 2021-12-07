using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AB RID: 427
	[UsedByNativeCode]
	public struct AnimatorClipInfo
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00019034 File Offset: 0x00017234
		public AnimationClip clip
		{
			get
			{
				return (this.m_ClipInstanceID == 0) ? null : AnimatorClipInfo.ClipInstanceToScriptingObject(this.m_ClipInstanceID);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00019054 File Offset: 0x00017254
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x060019E5 RID: 6629
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip ClipInstanceToScriptingObject(int instanceID);

		// Token: 0x040004C8 RID: 1224
		private int m_ClipInstanceID;

		// Token: 0x040004C9 RID: 1225
		private float m_Weight;
	}
}
