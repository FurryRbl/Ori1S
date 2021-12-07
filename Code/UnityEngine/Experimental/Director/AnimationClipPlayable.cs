using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020001C8 RID: 456
	public sealed class AnimationClipPlayable : AnimationPlayable
	{
		// Token: 0x06001B4E RID: 6990 RVA: 0x00019FC0 File Offset: 0x000181C0
		public AnimationClipPlayable(AnimationClip clip) : base(false)
		{
			this.m_Ptr = IntPtr.Zero;
			this.InstantiateEnginePlayable(clip);
		}

		// Token: 0x06001B4F RID: 6991
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InstantiateEnginePlayable(AnimationClip clip);

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001B50 RID: 6992
		public extern AnimationClip clip { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B51 RID: 6993 RVA: 0x00019FDC File Offset: 0x000181DC
		public override int AddInput(AnimationPlayable source)
		{
			Debug.LogError("AnimationClipPlayable doesn't support adding inputs");
			return -1;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00019FEC File Offset: 0x000181EC
		public override bool SetInput(AnimationPlayable source, int index)
		{
			Debug.LogError("AnimationClipPlayable doesn't support setting inputs");
			return false;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00019FFC File Offset: 0x000181FC
		public override bool SetInputs(IEnumerable<AnimationPlayable> sources)
		{
			Debug.LogError("AnimationClipPlayable doesn't support setting inputs");
			return false;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0001A00C File Offset: 0x0001820C
		public override bool RemoveInput(int index)
		{
			Debug.LogError("AnimationClipPlayable doesn't support removing inputs");
			return false;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0001A01C File Offset: 0x0001821C
		public override bool RemoveInput(AnimationPlayable playable)
		{
			Debug.LogError("AnimationClipPlayable doesn't support removing inputs");
			return false;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0001A02C File Offset: 0x0001822C
		public override bool RemoveAllInputs()
		{
			Debug.LogError("AnimationClipPlayable doesn't support removing inputs");
			return false;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001B57 RID: 6999
		// (set) Token: 0x06001B58 RID: 7000
		public extern float speed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
