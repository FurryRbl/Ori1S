using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000BE RID: 190
	public sealed class Gyroscope
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x0000F138 File Offset: 0x0000D338
		internal Gyroscope(int index)
		{
			this.m_GyroIndex = index;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0000F148 File Offset: 0x0000D348
		private static Vector3 rotationRate_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.INTERNAL_CALL_rotationRate_Internal(idx, out result);
			return result;
		}

		// Token: 0x06000B3B RID: 2875
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_rotationRate_Internal(int idx, out Vector3 value);

		// Token: 0x06000B3C RID: 2876 RVA: 0x0000F160 File Offset: 0x0000D360
		private static Vector3 rotationRateUnbiased_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.INTERNAL_CALL_rotationRateUnbiased_Internal(idx, out result);
			return result;
		}

		// Token: 0x06000B3D RID: 2877
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_rotationRateUnbiased_Internal(int idx, out Vector3 value);

		// Token: 0x06000B3E RID: 2878 RVA: 0x0000F178 File Offset: 0x0000D378
		private static Vector3 gravity_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.INTERNAL_CALL_gravity_Internal(idx, out result);
			return result;
		}

		// Token: 0x06000B3F RID: 2879
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_gravity_Internal(int idx, out Vector3 value);

		// Token: 0x06000B40 RID: 2880 RVA: 0x0000F190 File Offset: 0x0000D390
		private static Vector3 userAcceleration_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.INTERNAL_CALL_userAcceleration_Internal(idx, out result);
			return result;
		}

		// Token: 0x06000B41 RID: 2881
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_userAcceleration_Internal(int idx, out Vector3 value);

		// Token: 0x06000B42 RID: 2882 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		private static Quaternion attitude_Internal(int idx)
		{
			Quaternion result;
			Gyroscope.INTERNAL_CALL_attitude_Internal(idx, out result);
			return result;
		}

		// Token: 0x06000B43 RID: 2883
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_attitude_Internal(int idx, out Quaternion value);

		// Token: 0x06000B44 RID: 2884
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool getEnabled_Internal(int idx);

		// Token: 0x06000B45 RID: 2885
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setEnabled_Internal(int idx, bool enabled);

		// Token: 0x06000B46 RID: 2886
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float getUpdateInterval_Internal(int idx);

		// Token: 0x06000B47 RID: 2887
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setUpdateInterval_Internal(int idx, float interval);

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public Vector3 rotationRate
		{
			get
			{
				return Gyroscope.rotationRate_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		public Vector3 rotationRateUnbiased
		{
			get
			{
				return Gyroscope.rotationRateUnbiased_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		public Vector3 gravity
		{
			get
			{
				return Gyroscope.gravity_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public Vector3 userAcceleration
		{
			get
			{
				return Gyroscope.userAcceleration_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0000F200 File Offset: 0x0000D400
		public Quaternion attitude
		{
			get
			{
				return Gyroscope.attitude_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0000F210 File Offset: 0x0000D410
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x0000F220 File Offset: 0x0000D420
		public bool enabled
		{
			get
			{
				return Gyroscope.getEnabled_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setEnabled_Internal(this.m_GyroIndex, value);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0000F230 File Offset: 0x0000D430
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x0000F240 File Offset: 0x0000D440
		public float updateInterval
		{
			get
			{
				return Gyroscope.getUpdateInterval_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setUpdateInterval_Internal(this.m_GyroIndex, value);
			}
		}

		// Token: 0x0400024A RID: 586
		private int m_GyroIndex;
	}
}
