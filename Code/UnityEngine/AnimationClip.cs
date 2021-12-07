using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200019C RID: 412
	public sealed class AnimationClip : Motion
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x00018D04 File Offset: 0x00016F04
		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		// Token: 0x0600196A RID: 6506
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SampleAnimation(GameObject go, float time);

		// Token: 0x0600196B RID: 6507
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600196C RID: 6508
		public extern float length { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600196D RID: 6509
		internal extern float startTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600196E RID: 6510
		internal extern float stopTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600196F RID: 6511
		// (set) Token: 0x06001970 RID: 6512
		public extern float frameRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001971 RID: 6513
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCurve(string relativePath, Type type, string propertyName, AnimationCurve curve);

		// Token: 0x06001972 RID: 6514 RVA: 0x00018D14 File Offset: 0x00016F14
		public void EnsureQuaternionContinuity()
		{
			AnimationClip.INTERNAL_CALL_EnsureQuaternionContinuity(this);
		}

		// Token: 0x06001973 RID: 6515
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_EnsureQuaternionContinuity(AnimationClip self);

		// Token: 0x06001974 RID: 6516 RVA: 0x00018D1C File Offset: 0x00016F1C
		public void ClearCurves()
		{
			AnimationClip.INTERNAL_CALL_ClearCurves(this);
		}

		// Token: 0x06001975 RID: 6517
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClearCurves(AnimationClip self);

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001976 RID: 6518
		// (set) Token: 0x06001977 RID: 6519
		public extern WrapMode wrapMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00018D24 File Offset: 0x00016F24
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x00018D3C File Offset: 0x00016F3C
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

		// Token: 0x0600197A RID: 6522
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x0600197B RID: 6523
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600197C RID: 6524
		// (set) Token: 0x0600197D RID: 6525
		public new extern bool legacy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600197E RID: 6526
		public extern bool humanMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600197F RID: 6527 RVA: 0x00018D48 File Offset: 0x00016F48
		public void AddEvent(AnimationEvent evt)
		{
			this.AddEventInternal(evt);
		}

		// Token: 0x06001980 RID: 6528
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddEventInternal(object evt);

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x00018D54 File Offset: 0x00016F54
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x00018D64 File Offset: 0x00016F64
		public AnimationEvent[] events
		{
			get
			{
				return (AnimationEvent[])this.GetEventsInternal();
			}
			set
			{
				this.SetEventsInternal(value);
			}
		}

		// Token: 0x06001983 RID: 6531
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetEventsInternal(Array value);

		// Token: 0x06001984 RID: 6532
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Array GetEventsInternal();
	}
}
