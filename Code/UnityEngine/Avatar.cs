using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C3 RID: 451
	public sealed class Avatar : Object
	{
		// Token: 0x06001B23 RID: 6947 RVA: 0x00019CF8 File Offset: 0x00017EF8
		private Avatar()
		{
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001B24 RID: 6948
		public extern bool isValid { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001B25 RID: 6949
		public extern bool isHuman { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B26 RID: 6950
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetMuscleMinMax(int muscleId, float min, float max);

		// Token: 0x06001B27 RID: 6951
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetParameter(int parameterId, float value);

		// Token: 0x06001B28 RID: 6952
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetAxisLength(int humanId);

		// Token: 0x06001B29 RID: 6953 RVA: 0x00019D00 File Offset: 0x00017F00
		internal Quaternion GetPreRotation(int humanId)
		{
			Quaternion result;
			Avatar.INTERNAL_CALL_GetPreRotation(this, humanId, out result);
			return result;
		}

		// Token: 0x06001B2A RID: 6954
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPreRotation(Avatar self, int humanId, out Quaternion value);

		// Token: 0x06001B2B RID: 6955 RVA: 0x00019D18 File Offset: 0x00017F18
		internal Quaternion GetPostRotation(int humanId)
		{
			Quaternion result;
			Avatar.INTERNAL_CALL_GetPostRotation(this, humanId, out result);
			return result;
		}

		// Token: 0x06001B2C RID: 6956
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPostRotation(Avatar self, int humanId, out Quaternion value);

		// Token: 0x06001B2D RID: 6957 RVA: 0x00019D30 File Offset: 0x00017F30
		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			Quaternion result;
			Avatar.INTERNAL_CALL_GetZYPostQ(this, humanId, ref parentQ, ref q, out result);
			return result;
		}

		// Token: 0x06001B2E RID: 6958
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetZYPostQ(Avatar self, int humanId, ref Quaternion parentQ, ref Quaternion q, out Quaternion value);

		// Token: 0x06001B2F RID: 6959 RVA: 0x00019D4C File Offset: 0x00017F4C
		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			Quaternion result;
			Avatar.INTERNAL_CALL_GetZYRoll(this, humanId, ref uvw, out result);
			return result;
		}

		// Token: 0x06001B30 RID: 6960
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetZYRoll(Avatar self, int humanId, ref Vector3 uvw, out Quaternion value);

		// Token: 0x06001B31 RID: 6961 RVA: 0x00019D68 File Offset: 0x00017F68
		internal Vector3 GetLimitSign(int humanId)
		{
			Vector3 result;
			Avatar.INTERNAL_CALL_GetLimitSign(this, humanId, out result);
			return result;
		}

		// Token: 0x06001B32 RID: 6962
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetLimitSign(Avatar self, int humanId, out Vector3 value);
	}
}
