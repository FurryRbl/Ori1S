using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200012C RID: 300
	public sealed class Rigidbody : Component
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000158D8 File Offset: 0x00013AD8
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000158F0 File Offset: 0x00013AF0
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x060012C3 RID: 4803
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x060012C4 RID: 4804
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000158FC File Offset: 0x00013AFC
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x00015914 File Offset: 0x00013B14
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_angularVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularVelocity(ref value);
			}
		}

		// Token: 0x060012C7 RID: 4807
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularVelocity(out Vector3 value);

		// Token: 0x060012C8 RID: 4808
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularVelocity(ref Vector3 value);

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060012C9 RID: 4809
		// (set) Token: 0x060012CA RID: 4810
		public extern float drag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060012CB RID: 4811
		// (set) Token: 0x060012CC RID: 4812
		public extern float angularDrag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060012CD RID: 4813
		// (set) Token: 0x060012CE RID: 4814
		public extern float mass { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060012CF RID: 4815 RVA: 0x00015920 File Offset: 0x00013B20
		public void SetDensity(float density)
		{
			Rigidbody.INTERNAL_CALL_SetDensity(this, density);
		}

		// Token: 0x060012D0 RID: 4816
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetDensity(Rigidbody self, float density);

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060012D1 RID: 4817
		// (set) Token: 0x060012D2 RID: 4818
		public extern bool useGravity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060012D3 RID: 4819
		// (set) Token: 0x060012D4 RID: 4820
		public extern float maxDepenetrationVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060012D5 RID: 4821
		// (set) Token: 0x060012D6 RID: 4822
		public extern bool isKinematic { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060012D7 RID: 4823
		// (set) Token: 0x060012D8 RID: 4824
		public extern bool freezeRotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060012D9 RID: 4825
		// (set) Token: 0x060012DA RID: 4826
		public extern RigidbodyConstraints constraints { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060012DB RID: 4827
		// (set) Token: 0x060012DC RID: 4828
		public extern CollisionDetectionMode collisionDetectionMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060012DD RID: 4829 RVA: 0x0001592C File Offset: 0x00013B2C
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00015938 File Offset: 0x00013B38
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x060012DF RID: 4831
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForce(Rigidbody self, ref Vector3 force, ForceMode mode);

		// Token: 0x060012E0 RID: 4832 RVA: 0x00015950 File Offset: 0x00013B50
		[ExcludeFromDocs]
		public void AddForce(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			this.AddForce(x, y, z, mode);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0001596C File Offset: 0x00013B6C
		public void AddForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00015980 File Offset: 0x00013B80
		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddRelativeForce(this, ref force, mode);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0001598C File Offset: 0x00013B8C
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddRelativeForce(this, ref force, mode);
		}

		// Token: 0x060012E4 RID: 4836
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddRelativeForce(Rigidbody self, ref Vector3 force, ForceMode mode);

		// Token: 0x060012E5 RID: 4837 RVA: 0x000159A4 File Offset: 0x00013BA4
		[ExcludeFromDocs]
		public void AddRelativeForce(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			this.AddRelativeForce(x, y, z, mode);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000159C0 File Offset: 0x00013BC0
		public void AddRelativeForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000159D4 File Offset: 0x00013BD4
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddTorque(this, ref torque, mode);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000159E0 File Offset: 0x00013BE0
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddTorque(this, ref torque, mode);
		}

		// Token: 0x060012E9 RID: 4841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddTorque(Rigidbody self, ref Vector3 torque, ForceMode mode);

		// Token: 0x060012EA RID: 4842 RVA: 0x000159F8 File Offset: 0x00013BF8
		[ExcludeFromDocs]
		public void AddTorque(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			this.AddTorque(x, y, z, mode);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00015A14 File Offset: 0x00013C14
		public void AddTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00015A28 File Offset: 0x00013C28
		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddRelativeTorque(this, ref torque, mode);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00015A34 File Offset: 0x00013C34
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddRelativeTorque(this, ref torque, mode);
		}

		// Token: 0x060012EE RID: 4846
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddRelativeTorque(Rigidbody self, ref Vector3 torque, ForceMode mode);

		// Token: 0x060012EF RID: 4847 RVA: 0x00015A4C File Offset: 0x00013C4C
		[ExcludeFromDocs]
		public void AddRelativeTorque(float x, float y, float z)
		{
			ForceMode mode = ForceMode.Force;
			this.AddRelativeTorque(x, y, z, mode);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00015A68 File Offset: 0x00013C68
		public void AddRelativeTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00015A7C File Offset: 0x00013C7C
		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00015A8C File Offset: 0x00013C8C
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x060012F3 RID: 4851
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForceAtPosition(Rigidbody self, ref Vector3 force, ref Vector3 position, ForceMode mode);

		// Token: 0x060012F4 RID: 4852 RVA: 0x00015AA8 File Offset: 0x00013CA8
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0F")] float upwardsModifier, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddExplosionForce(this, explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00015AB8 File Offset: 0x00013CB8
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			ForceMode mode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddExplosionForce(this, explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00015AD4 File Offset: 0x00013CD4
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			ForceMode mode = ForceMode.Force;
			float upwardsModifier = 0f;
			Rigidbody.INTERNAL_CALL_AddExplosionForce(this, explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x060012F7 RID: 4855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddExplosionForce(Rigidbody self, float explosionForce, ref Vector3 explosionPosition, float explosionRadius, float upwardsModifier, ForceMode mode);

		// Token: 0x060012F8 RID: 4856 RVA: 0x00015AF8 File Offset: 0x00013CF8
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			Vector3 result;
			Rigidbody.INTERNAL_CALL_ClosestPointOnBounds(this, ref position, out result);
			return result;
		}

		// Token: 0x060012F9 RID: 4857
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClosestPointOnBounds(Rigidbody self, ref Vector3 position, out Vector3 value);

		// Token: 0x060012FA RID: 4858 RVA: 0x00015B10 File Offset: 0x00013D10
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			Vector3 result;
			Rigidbody.INTERNAL_CALL_GetRelativePointVelocity(this, ref relativePoint, out result);
			return result;
		}

		// Token: 0x060012FB RID: 4859
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetRelativePointVelocity(Rigidbody self, ref Vector3 relativePoint, out Vector3 value);

		// Token: 0x060012FC RID: 4860 RVA: 0x00015B28 File Offset: 0x00013D28
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			Vector3 result;
			Rigidbody.INTERNAL_CALL_GetPointVelocity(this, ref worldPoint, out result);
			return result;
		}

		// Token: 0x060012FD RID: 4861
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPointVelocity(Rigidbody self, ref Vector3 worldPoint, out Vector3 value);

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00015B40 File Offset: 0x00013D40
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x00015B58 File Offset: 0x00013D58
		public Vector3 centerOfMass
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_centerOfMass(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_centerOfMass(ref value);
			}
		}

		// Token: 0x06001300 RID: 4864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_centerOfMass(out Vector3 value);

		// Token: 0x06001301 RID: 4865
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_centerOfMass(ref Vector3 value);

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00015B64 File Offset: 0x00013D64
		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_worldCenterOfMass(out result);
				return result;
			}
		}

		// Token: 0x06001303 RID: 4867
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldCenterOfMass(out Vector3 value);

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00015B7C File Offset: 0x00013D7C
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00015B94 File Offset: 0x00013D94
		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_inertiaTensorRotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_inertiaTensorRotation(ref value);
			}
		}

		// Token: 0x06001306 RID: 4870
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_inertiaTensorRotation(out Quaternion value);

		// Token: 0x06001307 RID: 4871
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_inertiaTensorRotation(ref Quaternion value);

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00015BA0 File Offset: 0x00013DA0
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00015BB8 File Offset: 0x00013DB8
		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_inertiaTensor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_inertiaTensor(ref value);
			}
		}

		// Token: 0x0600130A RID: 4874
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_inertiaTensor(out Vector3 value);

		// Token: 0x0600130B RID: 4875
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_inertiaTensor(ref Vector3 value);

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600130C RID: 4876
		// (set) Token: 0x0600130D RID: 4877
		public extern bool detectCollisions { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600130E RID: 4878
		// (set) Token: 0x0600130F RID: 4879
		public extern bool useConeFriction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00015BC4 File Offset: 0x00013DC4
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00015BDC File Offset: 0x00013DDC
		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_position(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_position(ref value);
			}
		}

		// Token: 0x06001312 RID: 4882
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x06001313 RID: 4883
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_position(ref Vector3 value);

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00015BE8 File Offset: 0x00013DE8
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00015C00 File Offset: 0x00013E00
		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_rotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rotation(ref value);
			}
		}

		// Token: 0x06001316 RID: 4886
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x06001317 RID: 4887
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rotation(ref Quaternion value);

		// Token: 0x06001318 RID: 4888 RVA: 0x00015C0C File Offset: 0x00013E0C
		public void MovePosition(Vector3 position)
		{
			Rigidbody.INTERNAL_CALL_MovePosition(this, ref position);
		}

		// Token: 0x06001319 RID: 4889
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MovePosition(Rigidbody self, ref Vector3 position);

		// Token: 0x0600131A RID: 4890 RVA: 0x00015C18 File Offset: 0x00013E18
		public void MoveRotation(Quaternion rot)
		{
			Rigidbody.INTERNAL_CALL_MoveRotation(this, ref rot);
		}

		// Token: 0x0600131B RID: 4891
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MoveRotation(Rigidbody self, ref Quaternion rot);

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600131C RID: 4892
		// (set) Token: 0x0600131D RID: 4893
		public extern RigidbodyInterpolation interpolation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600131E RID: 4894 RVA: 0x00015C24 File Offset: 0x00013E24
		public void Sleep()
		{
			Rigidbody.INTERNAL_CALL_Sleep(this);
		}

		// Token: 0x0600131F RID: 4895
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Sleep(Rigidbody self);

		// Token: 0x06001320 RID: 4896 RVA: 0x00015C2C File Offset: 0x00013E2C
		public bool IsSleeping()
		{
			return Rigidbody.INTERNAL_CALL_IsSleeping(this);
		}

		// Token: 0x06001321 RID: 4897
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_IsSleeping(Rigidbody self);

		// Token: 0x06001322 RID: 4898 RVA: 0x00015C34 File Offset: 0x00013E34
		public void WakeUp()
		{
			Rigidbody.INTERNAL_CALL_WakeUp(this);
		}

		// Token: 0x06001323 RID: 4899
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_WakeUp(Rigidbody self);

		// Token: 0x06001324 RID: 4900 RVA: 0x00015C3C File Offset: 0x00013E3C
		public void ResetCenterOfMass()
		{
			Rigidbody.INTERNAL_CALL_ResetCenterOfMass(this);
		}

		// Token: 0x06001325 RID: 4901
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetCenterOfMass(Rigidbody self);

		// Token: 0x06001326 RID: 4902 RVA: 0x00015C44 File Offset: 0x00013E44
		public void ResetInertiaTensor()
		{
			Rigidbody.INTERNAL_CALL_ResetInertiaTensor(this);
		}

		// Token: 0x06001327 RID: 4903
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetInertiaTensor(Rigidbody self);

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001328 RID: 4904
		// (set) Token: 0x06001329 RID: 4905
		public extern int solverIterationCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600132A RID: 4906
		// (set) Token: 0x0600132B RID: 4907
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public extern float sleepVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600132C RID: 4908
		// (set) Token: 0x0600132D RID: 4909
		[Obsolete("The sleepAngularVelocity is no longer supported. Set Use sleepThreshold to specify energy.")]
		public extern float sleepAngularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600132E RID: 4910
		// (set) Token: 0x0600132F RID: 4911
		public extern float sleepThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001330 RID: 4912
		// (set) Token: 0x06001331 RID: 4913
		public extern float maxAngularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001332 RID: 4914 RVA: 0x00015C4C File Offset: 0x00013E4C
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Rigidbody.INTERNAL_CALL_SweepTest(this, ref direction, out hitInfo, maxDistance, queryTriggerInteraction);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00015C5C File Offset: 0x00013E5C
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Rigidbody.INTERNAL_CALL_SweepTest(this, ref direction, out hitInfo, maxDistance, queryTriggerInteraction);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00015C78 File Offset: 0x00013E78
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			float positiveInfinity = float.PositiveInfinity;
			return Rigidbody.INTERNAL_CALL_SweepTest(this, ref direction, out hitInfo, positiveInfinity, queryTriggerInteraction);
		}

		// Token: 0x06001335 RID: 4917
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SweepTest(Rigidbody self, ref Vector3 direction, out RaycastHit hitInfo, float maxDistance, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06001336 RID: 4918 RVA: 0x00015C98 File Offset: 0x00013E98
		public RaycastHit[] SweepTestAll(Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Rigidbody.INTERNAL_CALL_SweepTestAll(this, ref direction, maxDistance, queryTriggerInteraction);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00015CA4 File Offset: 0x00013EA4
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Rigidbody.INTERNAL_CALL_SweepTestAll(this, ref direction, maxDistance, queryTriggerInteraction);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00015CC0 File Offset: 0x00013EC0
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			float positiveInfinity = float.PositiveInfinity;
			return Rigidbody.INTERNAL_CALL_SweepTestAll(this, ref direction, positiveInfinity, queryTriggerInteraction);
		}

		// Token: 0x06001339 RID: 4921
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_SweepTestAll(Rigidbody self, ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600133A RID: 4922 RVA: 0x00015CE0 File Offset: 0x00013EE0
		[Obsolete("use Rigidbody.maxAngularVelocity instead.")]
		public void SetMaxAngularVelocity(float a)
		{
			this.maxAngularVelocity = a;
		}
	}
}
