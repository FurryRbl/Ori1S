using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200022A RID: 554
	public sealed class ClusterInput
	{
		// Token: 0x0600221D RID: 8733
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string name);

		// Token: 0x0600221E RID: 8734
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string name);

		// Token: 0x0600221F RID: 8735 RVA: 0x0002AC28 File Offset: 0x00028E28
		public static Vector3 GetTrackerPosition(string name)
		{
			Vector3 result;
			ClusterInput.INTERNAL_CALL_GetTrackerPosition(name, out result);
			return result;
		}

		// Token: 0x06002220 RID: 8736
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetTrackerPosition(string name, out Vector3 value);

		// Token: 0x06002221 RID: 8737 RVA: 0x0002AC40 File Offset: 0x00028E40
		public static Quaternion GetTrackerRotation(string name)
		{
			Quaternion result;
			ClusterInput.INTERNAL_CALL_GetTrackerRotation(name, out result);
			return result;
		}

		// Token: 0x06002222 RID: 8738
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetTrackerRotation(string name, out Quaternion value);

		// Token: 0x06002223 RID: 8739
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAxis(string name, float value);

		// Token: 0x06002224 RID: 8740
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetButton(string name, bool value);

		// Token: 0x06002225 RID: 8741 RVA: 0x0002AC58 File Offset: 0x00028E58
		public static void SetTrackerPosition(string name, Vector3 value)
		{
			ClusterInput.INTERNAL_CALL_SetTrackerPosition(name, ref value);
		}

		// Token: 0x06002226 RID: 8742
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTrackerPosition(string name, ref Vector3 value);

		// Token: 0x06002227 RID: 8743 RVA: 0x0002AC64 File Offset: 0x00028E64
		public static void SetTrackerRotation(string name, Quaternion value)
		{
			ClusterInput.INTERNAL_CALL_SetTrackerRotation(name, ref value);
		}

		// Token: 0x06002228 RID: 8744
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTrackerRotation(string name, ref Quaternion value);

		// Token: 0x06002229 RID: 8745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool AddInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type);

		// Token: 0x0600222A RID: 8746
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool EditInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type);

		// Token: 0x0600222B RID: 8747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CheckConnectionToServer(string name);
	}
}
