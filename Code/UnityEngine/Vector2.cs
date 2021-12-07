using System;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005C RID: 92
	[UsedByNativeCode]
	public struct Vector2
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00004EE8 File Offset: 0x000030E8
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x17000149 RID: 329
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.x;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
				return this.y;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00004F78 File Offset: 0x00003178
		public void Set(float new_x, float new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00004F88 File Offset: 0x00003188
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00004FD4 File Offset: 0x000031D4
		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00005010 File Offset: 0x00003210
		public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
		{
			Vector2 a = target - current;
			float magnitude = a.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + a / magnitude * maxDistanceDelta;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00005054 File Offset: 0x00003254
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000507C File Offset: 0x0000327C
		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000050B4 File Offset: 0x000032B4
		public void Normalize()
		{
			float magnitude = this.magnitude;
			if (magnitude > 1E-05f)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000050F8 File Offset: 0x000032F8
		public Vector2 normalized
		{
			get
			{
				Vector2 result = new Vector2(this.x, this.y);
				result.Normalize();
				return result;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00005120 File Offset: 0x00003320
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1})", new object[]
			{
				this.x,
				this.y
			});
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000515C File Offset: 0x0000335C
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format)
			});
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00005198 File Offset: 0x00003398
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000051B4 File Offset: 0x000033B4
		public override bool Equals(object other)
		{
			if (!(other is Vector2))
			{
				return false;
			}
			Vector2 vector = (Vector2)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00005204 File Offset: 0x00003404
		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
		{
			return -2f * Vector2.Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00005220 File Offset: 0x00003420
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00005244 File Offset: 0x00003444
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00005274 File Offset: 0x00003474
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00005294 File Offset: 0x00003494
		public static float Angle(Vector2 from, Vector2 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector2.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000052D0 File Offset: 0x000034D0
		public static float Distance(Vector2 a, Vector2 b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000052EC File Offset: 0x000034EC
		public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
		{
			if (vector.sqrMagnitude > maxLength * maxLength)
			{
				return vector.normalized * maxLength;
			}
			return vector;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000530C File Offset: 0x0000350C
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00005330 File Offset: 0x00003530
		public float SqrMagnitude()
		{
			return this.x * this.x + this.y * this.y;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00005350 File Offset: 0x00003550
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00005380 File Offset: 0x00003580
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000053B0 File Offset: 0x000035B0
		[ExcludeFromDocs]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000053D0 File Offset: 0x000035D0
		[ExcludeFromDocs]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000053F4 File Offset: 0x000035F4
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector2 vector = current - target;
			Vector2 vector2 = target;
			float maxLength = maxSpeed * smoothTime;
			vector = Vector2.ClampMagnitude(vector, maxLength);
			target = current - vector;
			Vector2 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * d;
			Vector2 vector4 = target + (vector + vector3) * d;
			if (Vector2.Dot(vector2 - current, vector4 - vector2) > 0f)
			{
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000054F0 File Offset: 0x000036F0
		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00005504 File Offset: 0x00003704
		public static Vector2 one
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00005518 File Offset: 0x00003718
		public static Vector2 up
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000552C File Offset: 0x0000372C
		public static Vector2 down
		{
			get
			{
				return new Vector2(0f, -1f);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00005540 File Offset: 0x00003740
		public static Vector2 left
		{
			get
			{
				return new Vector2(-1f, 0f);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00005554 File Offset: 0x00003754
		public static Vector2 right
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00005568 File Offset: 0x00003768
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00005590 File Offset: 0x00003790
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000055B8 File Offset: 0x000037B8
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000055D0 File Offset: 0x000037D0
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000055EC File Offset: 0x000037EC
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00005608 File Offset: 0x00003808
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00005624 File Offset: 0x00003824
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000563C File Offset: 0x0000383C
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00005654 File Offset: 0x00003854
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000566C File Offset: 0x0000386C
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x040000D4 RID: 212
		public const float kEpsilon = 1E-05f;

		// Token: 0x040000D5 RID: 213
		public float x;

		// Token: 0x040000D6 RID: 214
		public float y;
	}
}
