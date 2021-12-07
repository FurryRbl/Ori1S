using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005D RID: 93
	[UsedByNativeCode]
	public struct Vector3
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00005688 File Offset: 0x00003888
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000056A0 File Offset: 0x000038A0
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000056BC File Offset: 0x000038BC
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00005724 File Offset: 0x00003924
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00005784 File Offset: 0x00003984
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Vector3 result;
			Vector3.INTERNAL_CALL_Slerp(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x0600050D RID: 1293
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Slerp(ref Vector3 a, ref Vector3 b, float t, out Vector3 value);

		// Token: 0x0600050E RID: 1294 RVA: 0x000057A0 File Offset: 0x000039A0
		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			Vector3 result;
			Vector3.INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x0600050F RID: 1295
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SlerpUnclamped(ref Vector3 a, ref Vector3 b, float t, out Vector3 value);

		// Token: 0x06000510 RID: 1296 RVA: 0x000057BC File Offset: 0x000039BC
		private static void Internal_OrthoNormalize2(ref Vector3 a, ref Vector3 b)
		{
			Vector3.INTERNAL_CALL_Internal_OrthoNormalize2(ref a, ref b);
		}

		// Token: 0x06000511 RID: 1297
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		// Token: 0x06000512 RID: 1298 RVA: 0x000057C8 File Offset: 0x000039C8
		private static void Internal_OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c)
		{
			Vector3.INTERNAL_CALL_Internal_OrthoNormalize3(ref a, ref b, ref c);
		}

		// Token: 0x06000513 RID: 1299
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		// Token: 0x06000514 RID: 1300 RVA: 0x000057D4 File Offset: 0x000039D4
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.Internal_OrthoNormalize2(ref normal, ref tangent);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000057E0 File Offset: 0x000039E0
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.Internal_OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000057EC File Offset: 0x000039EC
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 a = target - current;
			float magnitude = a.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + a / magnitude * maxDistanceDelta;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00005830 File Offset: 0x00003A30
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			Vector3 result;
			Vector3.INTERNAL_CALL_RotateTowards(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out result);
			return result;
		}

		// Token: 0x06000518 RID: 1304
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateTowards(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 value);

		// Token: 0x06000519 RID: 1305 RVA: 0x0000584C File Offset: 0x00003A4C
		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000586C File Offset: 0x00003A6C
		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00005890 File Offset: 0x00003A90
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector3 vector = current - target;
			Vector3 vector2 = target;
			float maxLength = maxSpeed * smoothTime;
			vector = Vector3.ClampMagnitude(vector, maxLength);
			target = current - vector;
			Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * d;
			Vector3 vector4 = target + (vector + vector3) * d;
			if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
			{
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		// Token: 0x17000153 RID: 339
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00005A2C File Offset: 0x00003C2C
		public void Set(float new_x, float new_y, float new_z)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00005A44 File Offset: 0x00003C44
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00005A84 File Offset: 0x00003C84
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00005B40 File Offset: 0x00003D40
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00005B74 File Offset: 0x00003D74
		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			if (num > 1E-05f)
			{
				return value / num;
			}
			return Vector3.zero;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00005C20 File Offset: 0x00003E20
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00005C68 File Offset: 0x00003E68
		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00005C78 File Offset: 0x00003E78
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1})", new object[]
			{
				this.x,
				this.y,
				this.z
			});
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format)
			});
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00005D0C File Offset: 0x00003F0C
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00005D40 File Offset: 0x00003F40
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			if (num < Mathf.Epsilon)
			{
				return Vector3.zero;
			}
			return onNormal * Vector3.Dot(vector, onNormal) / num;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00005D7C File Offset: 0x00003F7C
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			return vector - Vector3.Project(vector, planeNormal);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00005D8C File Offset: 0x00003F8C
		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return fromThat - Vector3.Project(fromThat, excludeThis);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00005D9C File Offset: 0x00003F9C
		public static float Angle(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public static float Distance(Vector3 a, Vector3 b)
		{
			Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00005E50 File Offset: 0x00004050
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			if (vector.sqrMagnitude > maxLength * maxLength)
			{
				return vector.normalized * maxLength;
			}
			return vector;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00005E70 File Offset: 0x00004070
		public static float Magnitude(Vector3 a)
		{
			return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00005EB4 File Offset: 0x000040B4
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00005EF0 File Offset: 0x000040F0
		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00005F24 File Offset: 0x00004124
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00005F50 File Offset: 0x00004150
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00005F9C File Offset: 0x0000419C
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00005FE8 File Offset: 0x000041E8
		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00006000 File Offset: 0x00004200
		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00006018 File Offset: 0x00004218
		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00006030 File Offset: 0x00004230
		public static Vector3 back
		{
			get
			{
				return new Vector3(0f, 0f, -1f);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00006048 File Offset: 0x00004248
		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00006060 File Offset: 0x00004260
		public static Vector3 down
		{
			get
			{
				return new Vector3(0f, -1f, 0f);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00006078 File Offset: 0x00004278
		public static Vector3 left
		{
			get
			{
				return new Vector3(-1f, 0f, 0f);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00006090 File Offset: 0x00004290
		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000060A8 File Offset: 0x000042A8
		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000060C0 File Offset: 0x000042C0
		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000060F4 File Offset: 0x000042F4
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00006134 File Offset: 0x00004334
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00006174 File Offset: 0x00004374
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00006194 File Offset: 0x00004394
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000061C4 File Offset: 0x000043C4
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000061F4 File Offset: 0x000043F4
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00006224 File Offset: 0x00004424
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000623C File Offset: 0x0000443C
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x040000D7 RID: 215
		public const float kEpsilon = 1E-05f;

		// Token: 0x040000D8 RID: 216
		public float x;

		// Token: 0x040000D9 RID: 217
		public float y;

		// Token: 0x040000DA RID: 218
		public float z;
	}
}
