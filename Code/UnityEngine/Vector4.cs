using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000063 RID: 99
	[UsedByNativeCode]
	public struct Vector4
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00008CC0 File Offset: 0x00006EC0
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00008D10 File Offset: 0x00006F10
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x17000181 RID: 385
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
				case 3:
					return this.w;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
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
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00008DFC File Offset: 0x00006FFC
		public void Set(float new_x, float new_y, float new_z, float new_w)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
			this.w = new_w;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00008E1C File Offset: 0x0000701C
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00008E9C File Offset: 0x0000709C
		public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00008F14 File Offset: 0x00007114
		public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
		{
			Vector4 a = target - current;
			float magnitude = a.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + a / magnitude * maxDistanceDelta;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00008F58 File Offset: 0x00007158
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00008FA8 File Offset: 0x000071A8
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00009008 File Offset: 0x00007208
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000904C File Offset: 0x0000724C
		public override bool Equals(object other)
		{
			if (!(other is Vector4))
			{
				return false;
			}
			Vector4 vector = (Vector4)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000090C8 File Offset: 0x000072C8
		public static Vector4 Normalize(Vector4 a)
		{
			float num = Vector4.Magnitude(a);
			if (num > 1E-05f)
			{
				return a / num;
			}
			return Vector4.zero;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000090F4 File Offset: 0x000072F4
		public void Normalize()
		{
			float num = Vector4.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector4.zero;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000913C File Offset: 0x0000733C
		public Vector4 normalized
		{
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000914C File Offset: 0x0000734C
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000091A4 File Offset: 0x000073A4
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format),
				this.w.ToString(format)
			});
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00009200 File Offset: 0x00007400
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000924C File Offset: 0x0000744C
		public static Vector4 Project(Vector4 a, Vector4 b)
		{
			return b * Vector4.Dot(a, b) / Vector4.Dot(b, b);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00009268 File Offset: 0x00007468
		public static float Distance(Vector4 a, Vector4 b)
		{
			return Vector4.Magnitude(a - b);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00009278 File Offset: 0x00007478
		public static float Magnitude(Vector4 a)
		{
			return Mathf.Sqrt(Vector4.Dot(a, a));
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00009288 File Offset: 0x00007488
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(Vector4.Dot(this, this));
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000092A0 File Offset: 0x000074A0
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000092AC File Offset: 0x000074AC
		public float SqrMagnitude()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x000092C0 File Offset: 0x000074C0
		public float sqrMagnitude
		{
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000092D4 File Offset: 0x000074D4
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00009334 File Offset: 0x00007534
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00009394 File Offset: 0x00007594
		public static Vector4 zero
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000093B0 File Offset: 0x000075B0
		public static Vector4 one
		{
			get
			{
				return new Vector4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000093CC File Offset: 0x000075CC
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000941C File Offset: 0x0000761C
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000946C File Offset: 0x0000766C
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00009494 File Offset: 0x00007694
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000094C0 File Offset: 0x000076C0
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000094EC File Offset: 0x000076EC
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00009518 File Offset: 0x00007718
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00009530 File Offset: 0x00007730
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00009548 File Offset: 0x00007748
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000956C File Offset: 0x0000776C
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00009588 File Offset: 0x00007788
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000095A8 File Offset: 0x000077A8
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x040000FA RID: 250
		public const float kEpsilon = 1E-05f;

		// Token: 0x040000FB RID: 251
		public float x;

		// Token: 0x040000FC RID: 252
		public float y;

		// Token: 0x040000FD RID: 253
		public float z;

		// Token: 0x040000FE RID: 254
		public float w;
	}
}
