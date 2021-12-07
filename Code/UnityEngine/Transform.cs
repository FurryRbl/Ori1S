using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CA RID: 202
	public class Transform : Component, IEnumerable
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		protected Transform()
		{
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0000FCFC File Offset: 0x0000DEFC
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

		// Token: 0x06000C7F RID: 3199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x06000C80 RID: 3200
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_position(ref Vector3 value);

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0000FD08 File Offset: 0x0000DF08
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0000FD20 File Offset: 0x0000DF20
		public Vector3 localPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localPosition(ref value);
			}
		}

		// Token: 0x06000C83 RID: 3203
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localPosition(out Vector3 value);

		// Token: 0x06000C84 RID: 3204
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localPosition(ref Vector3 value);

		// Token: 0x06000C85 RID: 3205 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		internal Vector3 GetLocalEulerAngles(RotationOrder order)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_GetLocalEulerAngles(this, order, out result);
			return result;
		}

		// Token: 0x06000C86 RID: 3206
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetLocalEulerAngles(Transform self, RotationOrder order, out Vector3 value);

		// Token: 0x06000C87 RID: 3207 RVA: 0x0000FD44 File Offset: 0x0000DF44
		internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
		{
			Transform.INTERNAL_CALL_SetLocalEulerAngles(this, ref euler, order);
		}

		// Token: 0x06000C88 RID: 3208
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetLocalEulerAngles(Transform self, ref Vector3 euler, RotationOrder order);

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0000FD50 File Offset: 0x0000DF50
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0000FD94 File Offset: 0x0000DF94
		public Vector3 localEulerAngles
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localEulerAngles(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localEulerAngles(ref value);
			}
		}

		// Token: 0x06000C8D RID: 3213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localEulerAngles(out Vector3 value);

		// Token: 0x06000C8E RID: 3214
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localEulerAngles(ref Vector3 value);

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0000FE04 File Offset: 0x0000E004
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0000FE14 File Offset: 0x0000E014
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0000FE2C File Offset: 0x0000E02C
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

		// Token: 0x06000C97 RID: 3223
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x06000C98 RID: 3224
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rotation(ref Quaternion value);

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0000FE38 File Offset: 0x0000E038
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0000FE50 File Offset: 0x0000E050
		public Quaternion localRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_localRotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localRotation(ref value);
			}
		}

		// Token: 0x06000C9B RID: 3227
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localRotation(out Quaternion value);

		// Token: 0x06000C9C RID: 3228
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localRotation(ref Quaternion value);

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0000FE5C File Offset: 0x0000E05C
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0000FE74 File Offset: 0x0000E074
		public Vector3 localScale
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localScale(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localScale(ref value);
			}
		}

		// Token: 0x06000C9F RID: 3231
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localScale(out Vector3 value);

		// Token: 0x06000CA0 RID: 3232
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localScale(ref Vector3 value);

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0000FE80 File Offset: 0x0000E080
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0000FE88 File Offset: 0x0000E088
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				if (this is RectTransform)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000CA3 RID: 3235
		// (set) Token: 0x06000CA4 RID: 3236
		internal extern Transform parentInternal { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		public void SetParent(Transform parent)
		{
			this.SetParent(parent, true);
		}

		// Token: 0x06000CA6 RID: 3238
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParent(Transform parent, bool worldPositionStays);

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_worldToLocalMatrix(out result);
				return result;
			}
		}

		// Token: 0x06000CA8 RID: 3240
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldToLocalMatrix(out Matrix4x4 value);

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0000FECC File Offset: 0x0000E0CC
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_localToWorldMatrix(out result);
				return result;
			}
		}

		// Token: 0x06000CAA RID: 3242
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localToWorldMatrix(out Matrix4x4 value);

		// Token: 0x06000CAB RID: 3243 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		[ExcludeFromDocs]
		public void Translate(Vector3 translation)
		{
			Space relativeTo = Space.Self;
			this.Translate(translation, relativeTo);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.World)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0000FF40 File Offset: 0x0000E140
		[ExcludeFromDocs]
		public void Translate(float x, float y, float z)
		{
			Space relativeTo = Space.Self;
			this.Translate(x, y, z, relativeTo);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0000FF5C File Offset: 0x0000E15C
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0000FF70 File Offset: 0x0000E170
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			if (relativeTo)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		[ExcludeFromDocs]
		public void Rotate(Vector3 eulerAngles)
		{
			Space relativeTo = Space.Self;
			this.Rotate(eulerAngles, relativeTo);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		public void Rotate(Vector3 eulerAngles, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion rhs = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			if (relativeTo == Space.Self)
			{
				this.localRotation *= rhs;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * rhs * this.rotation;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00010058 File Offset: 0x0000E258
		[ExcludeFromDocs]
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			Space relativeTo = Space.Self;
			this.Rotate(xAngle, yAngle, zAngle, relativeTo);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00010074 File Offset: 0x0000E274
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00010088 File Offset: 0x0000E288
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundInternal(this, ref axis, angle);
		}

		// Token: 0x06000CB6 RID: 3254
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateAroundInternal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00010094 File Offset: 0x0000E294
		[ExcludeFromDocs]
		public void Rotate(Vector3 axis, float angle)
		{
			Space relativeTo = Space.Self;
			this.Rotate(axis, angle, relativeTo);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000100AC File Offset: 0x0000E2AC
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.Self)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000100EC File Offset: 0x0000E2EC
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion rotation = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = rotation * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00010138 File Offset: 0x0000E338
		[ExcludeFromDocs]
		public void LookAt(Transform target)
		{
			Vector3 up = Vector3.up;
			this.LookAt(target, up);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00010154 File Offset: 0x0000E354
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			if (target)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00010170 File Offset: 0x0000E370
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref worldUp);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0001017C File Offset: 0x0000E37C
		[ExcludeFromDocs]
		public void LookAt(Vector3 worldPosition)
		{
			Vector3 up = Vector3.up;
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref up);
		}

		// Token: 0x06000CBE RID: 3262
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LookAt(Transform self, ref Vector3 worldPosition, ref Vector3 worldUp);

		// Token: 0x06000CBF RID: 3263 RVA: 0x0001019C File Offset: 0x0000E39C
		public Vector3 TransformDirection(Vector3 direction)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_TransformDirection(this, ref direction, out result);
			return result;
		}

		// Token: 0x06000CC0 RID: 3264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_TransformDirection(Transform self, ref Vector3 direction, out Vector3 value);

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_InverseTransformDirection(this, ref direction, out result);
			return result;
		}

		// Token: 0x06000CC3 RID: 3267
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_InverseTransformDirection(Transform self, ref Vector3 direction, out Vector3 value);

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000101DC File Offset: 0x0000E3DC
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000101EC File Offset: 0x0000E3EC
		public Vector3 TransformVector(Vector3 vector)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_TransformVector(this, ref vector, out result);
			return result;
		}

		// Token: 0x06000CC6 RID: 3270
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_TransformVector(Transform self, ref Vector3 vector, out Vector3 value);

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00010204 File Offset: 0x0000E404
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00010214 File Offset: 0x0000E414
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_InverseTransformVector(this, ref vector, out result);
			return result;
		}

		// Token: 0x06000CC9 RID: 3273
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_InverseTransformVector(Transform self, ref Vector3 vector, out Vector3 value);

		// Token: 0x06000CCA RID: 3274 RVA: 0x0001022C File Offset: 0x0000E42C
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0001023C File Offset: 0x0000E43C
		public Vector3 TransformPoint(Vector3 position)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_TransformPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000CCC RID: 3276
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_TransformPoint(Transform self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000CCD RID: 3277 RVA: 0x00010254 File Offset: 0x0000E454
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00010264 File Offset: 0x0000E464
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			Vector3 result;
			Transform.INTERNAL_CALL_InverseTransformPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000CCF RID: 3279
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_InverseTransformPoint(Transform self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0001027C File Offset: 0x0000E47C
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000CD1 RID: 3281
		public extern Transform root { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000CD2 RID: 3282
		public extern int childCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000CD3 RID: 3283
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DetachChildren();

		// Token: 0x06000CD4 RID: 3284
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsFirstSibling();

		// Token: 0x06000CD5 RID: 3285
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsLastSibling();

		// Token: 0x06000CD6 RID: 3286
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSiblingIndex(int index);

		// Token: 0x06000CD7 RID: 3287
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetSiblingIndex();

		// Token: 0x06000CD8 RID: 3288
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform Find(string name);

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0001028C File Offset: 0x0000E48C
		public Vector3 lossyScale
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_lossyScale(out result);
				return result;
			}
		}

		// Token: 0x06000CDA RID: 3290
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lossyScale(out Vector3 value);

		// Token: 0x06000CDB RID: 3291
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsChildOf(Transform parent);

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000CDC RID: 3292
		// (set) Token: 0x06000CDD RID: 3293
		public extern bool hasChanged { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000CDE RID: 3294 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public Transform FindChild(string name)
		{
			return this.Find(name);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000102B8 File Offset: 0x0000E4B8
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAround(this, ref axis, angle);
		}

		// Token: 0x06000CE1 RID: 3297
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateAround(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000102C4 File Offset: 0x0000E4C4
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundLocal(this, ref axis, angle);
		}

		// Token: 0x06000CE3 RID: 3299
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateAroundLocal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000CE4 RID: 3300
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetChild(int index);

		// Token: 0x06000CE5 RID: 3301
		[WrapperlessIcall]
		[Obsolete("use Transform.childCount instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetChildCount();

		// Token: 0x020000CB RID: 203
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000CE6 RID: 3302 RVA: 0x000102D0 File Offset: 0x0000E4D0
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000102E8 File Offset: 0x0000E4E8
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x000102FC File Offset: 0x0000E4FC
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				return ++this.currentIndex < childCount;
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x0001032C File Offset: 0x0000E52C
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x0400026C RID: 620
			private Transform outer;

			// Token: 0x0400026D RID: 621
			private int currentIndex = -1;
		}
	}
}
