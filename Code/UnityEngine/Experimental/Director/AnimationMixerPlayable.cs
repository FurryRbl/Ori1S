using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020001C7 RID: 455
	public class AnimationMixerPlayable : AnimationPlayable
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x00019F34 File Offset: 0x00018134
		public AnimationMixerPlayable() : base(false)
		{
			this.m_Ptr = IntPtr.Zero;
			this.InstantiateEnginePlayable();
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00019F50 File Offset: 0x00018150
		public AnimationMixerPlayable(bool final) : base(false)
		{
			this.m_Ptr = IntPtr.Zero;
			if (final)
			{
				this.InstantiateEnginePlayable();
			}
		}

		// Token: 0x06001B4C RID: 6988
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InstantiateEnginePlayable();

		// Token: 0x06001B4D RID: 6989 RVA: 0x00019F70 File Offset: 0x00018170
		public bool SetInputs(AnimationClip[] clips)
		{
			if (clips == null)
			{
				throw new NullReferenceException("Parameter clips was null. You need to pass in a valid array of clips.");
			}
			AnimationPlayable[] array = new AnimationPlayable[clips.Length];
			for (int i = 0; i < clips.Length; i++)
			{
				array[i] = new AnimationClipPlayable(clips[i]);
			}
			return base.SetInputs(array);
		}
	}
}
