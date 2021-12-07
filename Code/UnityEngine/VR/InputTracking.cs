using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.VR
{
	// Token: 0x02000267 RID: 615
	public sealed class InputTracking
	{
		// Token: 0x0600249F RID: 9375 RVA: 0x0002FF5C File Offset: 0x0002E15C
		public static Vector3 GetLocalPosition(VRNode node)
		{
			Vector3 result;
			InputTracking.INTERNAL_CALL_GetLocalPosition(node, out result);
			return result;
		}

		// Token: 0x060024A0 RID: 9376
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetLocalPosition(VRNode node, out Vector3 value);

		// Token: 0x060024A1 RID: 9377 RVA: 0x0002FF74 File Offset: 0x0002E174
		public static Quaternion GetLocalRotation(VRNode node)
		{
			Quaternion result;
			InputTracking.INTERNAL_CALL_GetLocalRotation(node, out result);
			return result;
		}

		// Token: 0x060024A2 RID: 9378
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetLocalRotation(VRNode node, out Quaternion value);

		// Token: 0x060024A3 RID: 9379
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Recenter();
	}
}
