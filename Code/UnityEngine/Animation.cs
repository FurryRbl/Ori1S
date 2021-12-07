using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001A2 RID: 418
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001986 RID: 6534
		// (set) Token: 0x06001987 RID: 6535
		public extern AnimationClip clip { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001988 RID: 6536
		// (set) Token: 0x06001989 RID: 6537
		public extern bool playAutomatically { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600198A RID: 6538
		// (set) Token: 0x0600198B RID: 6539
		public extern WrapMode wrapMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600198C RID: 6540 RVA: 0x00018D78 File Offset: 0x00016F78
		public void Stop()
		{
			Animation.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x0600198D RID: 6541
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Stop(Animation self);

		// Token: 0x0600198E RID: 6542 RVA: 0x00018D80 File Offset: 0x00016F80
		public void Stop(string name)
		{
			this.Internal_StopByName(name);
		}

		// Token: 0x0600198F RID: 6543
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_StopByName(string name);

		// Token: 0x06001990 RID: 6544 RVA: 0x00018D8C File Offset: 0x00016F8C
		public void Rewind(string name)
		{
			this.Internal_RewindByName(name);
		}

		// Token: 0x06001991 RID: 6545
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_RewindByName(string name);

		// Token: 0x06001992 RID: 6546 RVA: 0x00018D98 File Offset: 0x00016F98
		public void Rewind()
		{
			Animation.INTERNAL_CALL_Rewind(this);
		}

		// Token: 0x06001993 RID: 6547
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rewind(Animation self);

		// Token: 0x06001994 RID: 6548 RVA: 0x00018DA0 File Offset: 0x00016FA0
		public void Sample()
		{
			Animation.INTERNAL_CALL_Sample(this);
		}

		// Token: 0x06001995 RID: 6549
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Sample(Animation self);

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001996 RID: 6550
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001997 RID: 6551
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying(string name);

		// Token: 0x170006C9 RID: 1737
		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00018DB4 File Offset: 0x00016FB4
		[ExcludeFromDocs]
		public bool Play()
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.Play(mode);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00018DCC File Offset: 0x00016FCC
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x0600199B RID: 6555
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600199C RID: 6556 RVA: 0x00018DD8 File Offset: 0x00016FD8
		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.Play(animation, mode);
		}

		// Token: 0x0600199D RID: 6557
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600199E RID: 6558 RVA: 0x00018DF0 File Offset: 0x00016FF0
		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			this.CrossFade(animation, fadeLength, mode);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00018E08 File Offset: 0x00017008
		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			float fadeLength = 0.3f;
			this.CrossFade(animation, fadeLength, mode);
		}

		// Token: 0x060019A0 RID: 6560
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		// Token: 0x060019A1 RID: 6561 RVA: 0x00018E28 File Offset: 0x00017028
		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			float fadeLength = 0.3f;
			this.Blend(animation, targetWeight, fadeLength);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00018E44 File Offset: 0x00017044
		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			float fadeLength = 0.3f;
			float targetWeight = 1f;
			this.Blend(animation, targetWeight, fadeLength);
		}

		// Token: 0x060019A3 RID: 6563
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x060019A4 RID: 6564 RVA: 0x00018E68 File Offset: 0x00017068
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.CrossFadeQueued(animation, fadeLength, queue, mode);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00018E84 File Offset: 0x00017084
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			QueueMode queue = QueueMode.CompleteOthers;
			return this.CrossFadeQueued(animation, fadeLength, queue, mode);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00018EA0 File Offset: 0x000170A0
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			QueueMode queue = QueueMode.CompleteOthers;
			float fadeLength = 0.3f;
			return this.CrossFadeQueued(animation, fadeLength, queue, mode);
		}

		// Token: 0x060019A7 RID: 6567
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x060019A8 RID: 6568 RVA: 0x00018EC4 File Offset: 0x000170C4
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			return this.PlayQueued(animation, queue, mode);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00018EDC File Offset: 0x000170DC
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			PlayMode mode = PlayMode.StopSameLayer;
			QueueMode queue = QueueMode.CompleteOthers;
			return this.PlayQueued(animation, queue, mode);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00018EF8 File Offset: 0x000170F8
		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		// Token: 0x060019AB RID: 6571
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		// Token: 0x060019AC RID: 6572 RVA: 0x00018F0C File Offset: 0x0001710C
		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			bool addLoopFrame = false;
			this.AddClip(clip, newName, firstFrame, lastFrame, addLoopFrame);
		}

		// Token: 0x060019AD RID: 6573
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveClip(AnimationClip clip);

		// Token: 0x060019AE RID: 6574 RVA: 0x00018F28 File Offset: 0x00017128
		public void RemoveClip(string clipName)
		{
			this.RemoveClip2(clipName);
		}

		// Token: 0x060019AF RID: 6575
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetClipCount();

		// Token: 0x060019B0 RID: 6576
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveClip2(string clipName);

		// Token: 0x060019B1 RID: 6577
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		// Token: 0x060019B2 RID: 6578 RVA: 0x00018F34 File Offset: 0x00017134
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00018F40 File Offset: 0x00017140
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00018F4C File Offset: 0x0001714C
		public void SyncLayer(int layer)
		{
			Animation.INTERNAL_CALL_SyncLayer(this, layer);
		}

		// Token: 0x060019B5 RID: 6581
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SyncLayer(Animation self, int layer);

		// Token: 0x060019B6 RID: 6582 RVA: 0x00018F58 File Offset: 0x00017158
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x060019B7 RID: 6583
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetState(string name);

		// Token: 0x060019B8 RID: 6584
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetStateAtIndex(int index);

		// Token: 0x060019B9 RID: 6585
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetStateCount();

		// Token: 0x060019BA RID: 6586 RVA: 0x00018F60 File Offset: 0x00017160
		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			if (state)
			{
				return state.clip;
			}
			return null;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060019BB RID: 6587
		// (set) Token: 0x060019BC RID: 6588
		public extern bool animatePhysics { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060019BD RID: 6589
		// (set) Token: 0x060019BE RID: 6590
		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060019BF RID: 6591
		// (set) Token: 0x060019C0 RID: 6592
		public extern AnimationCullingType cullingType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00018F88 File Offset: 0x00017188
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00018FA0 File Offset: 0x000171A0
		public Bounds localBounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_localBounds(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x060019C3 RID: 6595
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060019C4 RID: 6596
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x020001A3 RID: 419
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x060019C5 RID: 6597 RVA: 0x00018FAC File Offset: 0x000171AC
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00018FC4 File Offset: 0x000171C4
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x060019C7 RID: 6599 RVA: 0x00018FD8 File Offset: 0x000171D8
			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			// Token: 0x060019C8 RID: 6600 RVA: 0x00019008 File Offset: 0x00017208
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x040004A8 RID: 1192
			private Animation m_Outer;

			// Token: 0x040004A9 RID: 1193
			private int m_CurrentIndex = -1;
		}
	}
}
