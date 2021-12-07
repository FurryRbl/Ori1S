using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000061 RID: 97
	[UsedByNativeCode]
	public struct Matrix4x4
	{
		// Token: 0x17000174 RID: 372
		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x17000175 RID: 373
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.m00;
				case 1:
					return this.m10;
				case 2:
					return this.m20;
				case 3:
					return this.m30;
				case 4:
					return this.m01;
				case 5:
					return this.m11;
				case 6:
					return this.m21;
				case 7:
					return this.m31;
				case 8:
					return this.m02;
				case 9:
					return this.m12;
				case 10:
					return this.m22;
				case 11:
					return this.m32;
				case 12:
					return this.m03;
				case 13:
					return this.m13;
				case 14:
					return this.m23;
				case 15:
					return this.m33;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000782C File Offset: 0x00005A2C
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00007880 File Offset: 0x00005A80
		public override bool Equals(object other)
		{
			if (!(other is Matrix4x4))
			{
				return false;
			}
			Matrix4x4 matrix4x = (Matrix4x4)other;
			return this.GetColumn(0).Equals(matrix4x.GetColumn(0)) && this.GetColumn(1).Equals(matrix4x.GetColumn(1)) && this.GetColumn(2).Equals(matrix4x.GetColumn(2)) && this.GetColumn(3).Equals(matrix4x.GetColumn(3));
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00007924 File Offset: 0x00005B24
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			Matrix4x4 result;
			Matrix4x4.INTERNAL_CALL_Inverse(ref m, out result);
			return result;
		}

		// Token: 0x060005C6 RID: 1478
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Inverse(ref Matrix4x4 m, out Matrix4x4 value);

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000793C File Offset: 0x00005B3C
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			Matrix4x4 result;
			Matrix4x4.INTERNAL_CALL_Transpose(ref m, out result);
			return result;
		}

		// Token: 0x060005C8 RID: 1480
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Transpose(ref Matrix4x4 m, out Matrix4x4 value);

		// Token: 0x060005C9 RID: 1481 RVA: 0x00007954 File Offset: 0x00005B54
		internal static bool Invert(Matrix4x4 inMatrix, out Matrix4x4 dest)
		{
			return Matrix4x4.INTERNAL_CALL_Invert(ref inMatrix, out dest);
		}

		// Token: 0x060005CA RID: 1482
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Invert(ref Matrix4x4 inMatrix, out Matrix4x4 dest);

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00007960 File Offset: 0x00005B60
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00007970 File Offset: 0x00005B70
		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005CD RID: 1485
		public extern bool isIdentity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060005CE RID: 1486 RVA: 0x00007980 File Offset: 0x00005B80
		public static float Determinant(Matrix4x4 m)
		{
			return Matrix4x4.INTERNAL_CALL_Determinant(ref m);
		}

		// Token: 0x060005CF RID: 1487
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_Determinant(ref Matrix4x4 m);

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000798C File Offset: 0x00005B8C
		public float determinant
		{
			get
			{
				return Matrix4x4.Determinant(this);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000799C File Offset: 0x00005B9C
		public Vector4 GetColumn(int i)
		{
			return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000079D0 File Offset: 0x00005BD0
		public Vector4 GetRow(int i)
		{
			return new Vector4(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00007A04 File Offset: 0x00005C04
		public void SetColumn(int i, Vector4 v)
		{
			this[0, i] = v.x;
			this[1, i] = v.y;
			this[2, i] = v.z;
			this[3, i] = v.w;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00007A50 File Offset: 0x00005C50
		public void SetRow(int i, Vector4 v)
		{
			this[i, 0] = v.x;
			this[i, 1] = v.y;
			this[i, 2] = v.z;
			this[i, 3] = v.w;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00007A9C File Offset: 0x00005C9C
		public Vector3 MultiplyPoint(Vector3 v)
		{
			Vector3 result;
			result.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			result.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			result.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			float num = this.m30 * v.x + this.m31 * v.y + this.m32 * v.z + this.m33;
			num = 1f / num;
			result.x *= num;
			result.y *= num;
			result.z *= num;
			return result;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public Vector3 MultiplyPoint3x4(Vector3 v)
		{
			Vector3 result;
			result.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			result.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			result.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			return result;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00007C80 File Offset: 0x00005E80
		public Vector3 MultiplyVector(Vector3 v)
		{
			Vector3 result;
			result.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z;
			result.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z;
			result.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z;
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00007D28 File Offset: 0x00005F28
		public static Matrix4x4 Scale(Vector3 v)
		{
			return new Matrix4x4
			{
				m00 = v.x,
				m01 = 0f,
				m02 = 0f,
				m03 = 0f,
				m10 = 0f,
				m11 = v.y,
				m12 = 0f,
				m13 = 0f,
				m20 = 0f,
				m21 = 0f,
				m22 = v.z,
				m23 = 0f,
				m30 = 0f,
				m31 = 0f,
				m32 = 0f,
				m33 = 1f
			};
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00007E04 File Offset: 0x00006004
		public static Matrix4x4 zero
		{
			get
			{
				return new Matrix4x4
				{
					m00 = 0f,
					m01 = 0f,
					m02 = 0f,
					m03 = 0f,
					m10 = 0f,
					m11 = 0f,
					m12 = 0f,
					m13 = 0f,
					m20 = 0f,
					m21 = 0f,
					m22 = 0f,
					m23 = 0f,
					m30 = 0f,
					m31 = 0f,
					m32 = 0f,
					m33 = 0f
				};
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00007EDC File Offset: 0x000060DC
		public static Matrix4x4 identity
		{
			get
			{
				return new Matrix4x4
				{
					m00 = 1f,
					m01 = 0f,
					m02 = 0f,
					m03 = 0f,
					m10 = 0f,
					m11 = 1f,
					m12 = 0f,
					m13 = 0f,
					m20 = 0f,
					m21 = 0f,
					m22 = 1f,
					m23 = 0f,
					m30 = 0f,
					m31 = 0f,
					m32 = 0f,
					m33 = 1f
				};
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00007FB4 File Offset: 0x000061B4
		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00007FC4 File Offset: 0x000061C4
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			Matrix4x4 result;
			Matrix4x4.INTERNAL_CALL_TRS(ref pos, ref q, ref s, out result);
			return result;
		}

		// Token: 0x060005DD RID: 1501
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_TRS(ref Vector3 pos, ref Quaternion q, ref Vector3 s, out Matrix4x4 value);

		// Token: 0x060005DE RID: 1502 RVA: 0x00007FE0 File Offset: 0x000061E0
		public override string ToString()
		{
			return UnityString.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[]
			{
				this.m00,
				this.m01,
				this.m02,
				this.m03,
				this.m10,
				this.m11,
				this.m12,
				this.m13,
				this.m20,
				this.m21,
				this.m22,
				this.m23,
				this.m30,
				this.m31,
				this.m32,
				this.m33
			});
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000080E8 File Offset: 0x000062E8
		public string ToString(string format)
		{
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format),
				this.m01.ToString(format),
				this.m02.ToString(format),
				this.m03.ToString(format),
				this.m10.ToString(format),
				this.m11.ToString(format),
				this.m12.ToString(format),
				this.m13.ToString(format),
				this.m20.ToString(format),
				this.m21.ToString(format),
				this.m22.ToString(format),
				this.m23.ToString(format),
				this.m30.ToString(format),
				this.m31.ToString(format),
				this.m32.ToString(format),
				this.m33.ToString(format)
			});
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00008200 File Offset: 0x00006400
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 result;
			Matrix4x4.INTERNAL_CALL_Ortho(left, right, bottom, top, zNear, zFar, out result);
			return result;
		}

		// Token: 0x060005E1 RID: 1505
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Ortho(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 value);

		// Token: 0x060005E2 RID: 1506 RVA: 0x00008220 File Offset: 0x00006420
		public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
		{
			Matrix4x4 result;
			Matrix4x4.INTERNAL_CALL_Perspective(fov, aspect, zNear, zFar, out result);
			return result;
		}

		// Token: 0x060005E3 RID: 1507
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Perspective(float fov, float aspect, float zNear, float zFar, out Matrix4x4 value);

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000823C File Offset: 0x0000643C
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return new Matrix4x4
			{
				m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30,
				m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31,
				m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32,
				m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33,
				m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30,
				m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
				m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
				m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,
				m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30,
				m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
				m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
				m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,
				m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30,
				m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
				m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
				m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
			};
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000086B4 File Offset: 0x000068B4
		public static Vector4 operator *(Matrix4x4 lhs, Vector4 v)
		{
			Vector4 result;
			result.x = lhs.m00 * v.x + lhs.m01 * v.y + lhs.m02 * v.z + lhs.m03 * v.w;
			result.y = lhs.m10 * v.x + lhs.m11 * v.y + lhs.m12 * v.z + lhs.m13 * v.w;
			result.z = lhs.m20 * v.x + lhs.m21 * v.y + lhs.m22 * v.z + lhs.m23 * v.w;
			result.w = lhs.m30 * v.x + lhs.m31 * v.y + lhs.m32 * v.z + lhs.m33 * v.w;
			return result;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000087DC File Offset: 0x000069DC
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00008850 File Offset: 0x00006A50
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000E8 RID: 232
		public float m00;

		// Token: 0x040000E9 RID: 233
		public float m10;

		// Token: 0x040000EA RID: 234
		public float m20;

		// Token: 0x040000EB RID: 235
		public float m30;

		// Token: 0x040000EC RID: 236
		public float m01;

		// Token: 0x040000ED RID: 237
		public float m11;

		// Token: 0x040000EE RID: 238
		public float m21;

		// Token: 0x040000EF RID: 239
		public float m31;

		// Token: 0x040000F0 RID: 240
		public float m02;

		// Token: 0x040000F1 RID: 241
		public float m12;

		// Token: 0x040000F2 RID: 242
		public float m22;

		// Token: 0x040000F3 RID: 243
		public float m32;

		// Token: 0x040000F4 RID: 244
		public float m03;

		// Token: 0x040000F5 RID: 245
		public float m13;

		// Token: 0x040000F6 RID: 246
		public float m23;

		// Token: 0x040000F7 RID: 247
		public float m33;
	}
}
