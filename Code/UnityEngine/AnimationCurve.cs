using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200006A RID: 106
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationCurve
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0000A20C File Offset: 0x0000840C
		public AnimationCurve(params Keyframe[] keys)
		{
			this.Init(keys);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000A21C File Offset: 0x0000841C
		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.Init(null);
		}

		// Token: 0x060006A1 RID: 1697
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000A22C File Offset: 0x0000842C
		~AnimationCurve()
		{
			this.Cleanup();
		}

		// Token: 0x060006A3 RID: 1699
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float Evaluate(float time);

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000A268 File Offset: 0x00008468
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0000A270 File Offset: 0x00008470
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060006A6 RID: 1702
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int AddKey(float time, float value);

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000A27C File Offset: 0x0000847C
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000A288 File Offset: 0x00008488
		private int AddKey_Internal(Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_AddKey_Internal(this, ref key);
		}

		// Token: 0x060006A9 RID: 1705
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_AddKey_Internal(AnimationCurve self, ref Keyframe key);

		// Token: 0x060006AA RID: 1706 RVA: 0x0000A294 File Offset: 0x00008494
		public int MoveKey(int index, Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_MoveKey(this, index, ref key);
		}

		// Token: 0x060006AB RID: 1707
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_MoveKey(AnimationCurve self, int index, ref Keyframe key);

		// Token: 0x060006AC RID: 1708
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveKey(int index);

		// Token: 0x17000193 RID: 403
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey_Internal(index);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006AE RID: 1710
		public extern int length { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060006AF RID: 1711
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000A2AC File Offset: 0x000084AC
		private Keyframe GetKey_Internal(int index)
		{
			Keyframe result;
			AnimationCurve.INTERNAL_CALL_GetKey_Internal(this, index, out result);
			return result;
		}

		// Token: 0x060006B1 RID: 1713
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetKey_Internal(AnimationCurve self, int index, out Keyframe value);

		// Token: 0x060006B2 RID: 1714
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060006B3 RID: 1715
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			float num = (valueEnd - valueStart) / (timeEnd - timeStart);
			Keyframe[] keys = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, num),
				new Keyframe(timeEnd, valueEnd, num, 0f)
			};
			return new AnimationCurve(keys);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000A318 File Offset: 0x00008518
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			Keyframe[] keys = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, 0f),
				new Keyframe(timeEnd, valueEnd, 0f, 0f)
			};
			return new AnimationCurve(keys);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006B6 RID: 1718
		// (set) Token: 0x060006B7 RID: 1719
		public extern WrapMode preWrapMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006B8 RID: 1720
		// (set) Token: 0x060006B9 RID: 1721
		public extern WrapMode postWrapMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060006BA RID: 1722
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init(Keyframe[] keys);

		// Token: 0x04000112 RID: 274
		internal IntPtr m_Ptr;
	}
}
