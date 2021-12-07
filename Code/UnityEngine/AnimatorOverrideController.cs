using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000198 RID: 408
	public sealed class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x00018A30 File Offset: 0x00016C30
		public AnimatorOverrideController()
		{
			AnimatorOverrideController.Internal_CreateAnimationSet(this);
		}

		// Token: 0x06001943 RID: 6467
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationSet([Writable] AnimatorOverrideController self);

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001944 RID: 6468
		// (set) Token: 0x06001945 RID: 6469
		public extern RuntimeAnimatorController runtimeAnimatorController { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006AC RID: 1708
		public AnimationClip this[string name]
		{
			get
			{
				return this.Internal_GetClipByName(name, true);
			}
			set
			{
				this.Internal_SetClipByName(name, value);
			}
		}

		// Token: 0x06001948 RID: 6472
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip);

		// Token: 0x06001949 RID: 6473
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetClipByName(string name, AnimationClip clip);

		// Token: 0x170006AD RID: 1709
		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return this.Internal_GetClip(clip, true);
			}
			set
			{
				this.Internal_SetClip(clip, value);
			}
		}

		// Token: 0x0600194C RID: 6476
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip Internal_GetClip(AnimationClip originalClip, bool returnEffectiveClip);

		// Token: 0x0600194D RID: 6477
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetClip(AnimationClip originalClip, AnimationClip overrideClip);

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00018A70 File Offset: 0x00016C70
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x00018B20 File Offset: 0x00016D20
		public AnimationClipPair[] clips
		{
			get
			{
				AnimationClip[] array = this.GetOriginalClips();
				Dictionary<AnimationClip, bool> dictionary = new Dictionary<AnimationClip, bool>(array.Length);
				foreach (AnimationClip key in array)
				{
					dictionary[key] = true;
				}
				array = new AnimationClip[dictionary.Count];
				dictionary.Keys.CopyTo(array, 0);
				AnimationClipPair[] array3 = new AnimationClipPair[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array3[j] = new AnimationClipPair();
					array3[j].originalClip = array[j];
					array3[j].overrideClip = this.Internal_GetClip(array[j], false);
				}
				return array3;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.Internal_SetClip(value[i].originalClip, value[i].overrideClip);
				}
			}
		}

		// Token: 0x06001950 RID: 6480
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip[] GetOriginalClips();

		// Token: 0x06001951 RID: 6481
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip[] GetOverrideClips();
	}
}
