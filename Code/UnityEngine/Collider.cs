using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000137 RID: 311
	public class Collider : Component
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600140F RID: 5135
		// (set) Token: 0x06001410 RID: 5136
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001411 RID: 5137
		public extern Rigidbody attachedRigidbody { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001412 RID: 5138
		// (set) Token: 0x06001413 RID: 5139
		public extern bool isTrigger { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001414 RID: 5140
		// (set) Token: 0x06001415 RID: 5141
		public extern float contactOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001416 RID: 5142
		// (set) Token: 0x06001417 RID: 5143
		public extern PhysicMaterial material { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001418 RID: 5144 RVA: 0x0001623C File Offset: 0x0001443C
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			Vector3 result;
			Collider.INTERNAL_CALL_ClosestPointOnBounds(this, ref position, out result);
			return result;
		}

		// Token: 0x06001419 RID: 5145
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClosestPointOnBounds(Collider self, ref Vector3 position, out Vector3 value);

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600141A RID: 5146
		// (set) Token: 0x0600141B RID: 5147
		public extern PhysicMaterial sharedMaterial { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00016254 File Offset: 0x00014454
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x0600141D RID: 5149
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x0600141E RID: 5150 RVA: 0x0001626C File Offset: 0x0001446C
		private static bool Internal_Raycast(Collider col, Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			return Collider.INTERNAL_CALL_Internal_Raycast(col, ref ray, out hitInfo, maxDistance);
		}

		// Token: 0x0600141F RID: 5151
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(Collider col, ref Ray ray, out RaycastHit hitInfo, float maxDistance);

		// Token: 0x06001420 RID: 5152 RVA: 0x00016278 File Offset: 0x00014478
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			return Collider.Internal_Raycast(this, ray, out hitInfo, maxDistance);
		}
	}
}
