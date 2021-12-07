using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000091 RID: 145
	[UsedByNativeCode]
	public struct SphericalHarmonicsL2
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0000BD18 File Offset: 0x00009F18
		public void Clear()
		{
			SphericalHarmonicsL2.ClearInternal(ref this);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000BD20 File Offset: 0x00009F20
		private static void ClearInternal(ref SphericalHarmonicsL2 sh)
		{
			SphericalHarmonicsL2.INTERNAL_CALL_ClearInternal(ref sh);
		}

		// Token: 0x060008B6 RID: 2230
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClearInternal(ref SphericalHarmonicsL2 sh);

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000BD28 File Offset: 0x00009F28
		public void AddAmbientLight(Color color)
		{
			SphericalHarmonicsL2.AddAmbientLightInternal(color, ref this);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000BD34 File Offset: 0x00009F34
		private static void AddAmbientLightInternal(Color color, ref SphericalHarmonicsL2 sh)
		{
			SphericalHarmonicsL2.INTERNAL_CALL_AddAmbientLightInternal(ref color, ref sh);
		}

		// Token: 0x060008B9 RID: 2233
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddAmbientLightInternal(ref Color color, ref SphericalHarmonicsL2 sh);

		// Token: 0x060008BA RID: 2234 RVA: 0x0000BD40 File Offset: 0x00009F40
		public void AddDirectionalLight(Vector3 direction, Color color, float intensity)
		{
			Color color2 = color * (2f * intensity);
			SphericalHarmonicsL2.AddDirectionalLightInternal(direction, color2, ref this);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000BD64 File Offset: 0x00009F64
		private static void AddDirectionalLightInternal(Vector3 direction, Color color, ref SphericalHarmonicsL2 sh)
		{
			SphericalHarmonicsL2.INTERNAL_CALL_AddDirectionalLightInternal(ref direction, ref color, ref sh);
		}

		// Token: 0x060008BC RID: 2236
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddDirectionalLightInternal(ref Vector3 direction, ref Color color, ref SphericalHarmonicsL2 sh);

		// Token: 0x170001F6 RID: 502
		public float this[int rgb, int coefficient]
		{
			get
			{
				switch (rgb * 9 + coefficient)
				{
				case 0:
					return this.shr0;
				case 1:
					return this.shr1;
				case 2:
					return this.shr2;
				case 3:
					return this.shr3;
				case 4:
					return this.shr4;
				case 5:
					return this.shr5;
				case 6:
					return this.shr6;
				case 7:
					return this.shr7;
				case 8:
					return this.shr8;
				case 9:
					return this.shg0;
				case 10:
					return this.shg1;
				case 11:
					return this.shg2;
				case 12:
					return this.shg3;
				case 13:
					return this.shg4;
				case 14:
					return this.shg5;
				case 15:
					return this.shg6;
				case 16:
					return this.shg7;
				case 17:
					return this.shg8;
				case 18:
					return this.shb0;
				case 19:
					return this.shb1;
				case 20:
					return this.shb2;
				case 21:
					return this.shb3;
				case 22:
					return this.shb4;
				case 23:
					return this.shb5;
				case 24:
					return this.shb6;
				case 25:
					return this.shb7;
				case 26:
					return this.shb8;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
			}
			set
			{
				switch (rgb * 9 + coefficient)
				{
				case 0:
					this.shr0 = value;
					break;
				case 1:
					this.shr1 = value;
					break;
				case 2:
					this.shr2 = value;
					break;
				case 3:
					this.shr3 = value;
					break;
				case 4:
					this.shr4 = value;
					break;
				case 5:
					this.shr5 = value;
					break;
				case 6:
					this.shr6 = value;
					break;
				case 7:
					this.shr7 = value;
					break;
				case 8:
					this.shr8 = value;
					break;
				case 9:
					this.shg0 = value;
					break;
				case 10:
					this.shg1 = value;
					break;
				case 11:
					this.shg2 = value;
					break;
				case 12:
					this.shg3 = value;
					break;
				case 13:
					this.shg4 = value;
					break;
				case 14:
					this.shg5 = value;
					break;
				case 15:
					this.shg6 = value;
					break;
				case 16:
					this.shg7 = value;
					break;
				case 17:
					this.shg8 = value;
					break;
				case 18:
					this.shb0 = value;
					break;
				case 19:
					this.shb1 = value;
					break;
				case 20:
					this.shb2 = value;
					break;
				case 21:
					this.shb3 = value;
					break;
				case 22:
					this.shb4 = value;
					break;
				case 23:
					this.shb5 = value;
					break;
				case 24:
					this.shb6 = value;
					break;
				case 25:
					this.shb7 = value;
					break;
				case 26:
					this.shb8 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.shr0.GetHashCode();
			num = num * 23 + this.shr1.GetHashCode();
			num = num * 23 + this.shr2.GetHashCode();
			num = num * 23 + this.shr3.GetHashCode();
			num = num * 23 + this.shr4.GetHashCode();
			num = num * 23 + this.shr5.GetHashCode();
			num = num * 23 + this.shr6.GetHashCode();
			num = num * 23 + this.shr7.GetHashCode();
			num = num * 23 + this.shr8.GetHashCode();
			num = num * 23 + this.shg0.GetHashCode();
			num = num * 23 + this.shg1.GetHashCode();
			num = num * 23 + this.shg2.GetHashCode();
			num = num * 23 + this.shg3.GetHashCode();
			num = num * 23 + this.shg4.GetHashCode();
			num = num * 23 + this.shg5.GetHashCode();
			num = num * 23 + this.shg6.GetHashCode();
			num = num * 23 + this.shg7.GetHashCode();
			num = num * 23 + this.shg8.GetHashCode();
			num = num * 23 + this.shb0.GetHashCode();
			num = num * 23 + this.shb1.GetHashCode();
			num = num * 23 + this.shb2.GetHashCode();
			num = num * 23 + this.shb3.GetHashCode();
			num = num * 23 + this.shb4.GetHashCode();
			num = num * 23 + this.shb5.GetHashCode();
			num = num * 23 + this.shb6.GetHashCode();
			num = num * 23 + this.shb7.GetHashCode();
			return num * 23 + this.shb8.GetHashCode();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000C27C File Offset: 0x0000A47C
		public override bool Equals(object other)
		{
			if (!(other is SphericalHarmonicsL2))
			{
				return false;
			}
			SphericalHarmonicsL2 rhs = (SphericalHarmonicsL2)other;
			return this == rhs;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		public static SphericalHarmonicsL2 operator *(SphericalHarmonicsL2 lhs, float rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = lhs.shr0 * rhs,
				shr1 = lhs.shr1 * rhs,
				shr2 = lhs.shr2 * rhs,
				shr3 = lhs.shr3 * rhs,
				shr4 = lhs.shr4 * rhs,
				shr5 = lhs.shr5 * rhs,
				shr6 = lhs.shr6 * rhs,
				shr7 = lhs.shr7 * rhs,
				shr8 = lhs.shr8 * rhs,
				shg0 = lhs.shg0 * rhs,
				shg1 = lhs.shg1 * rhs,
				shg2 = lhs.shg2 * rhs,
				shg3 = lhs.shg3 * rhs,
				shg4 = lhs.shg4 * rhs,
				shg5 = lhs.shg5 * rhs,
				shg6 = lhs.shg6 * rhs,
				shg7 = lhs.shg7 * rhs,
				shg8 = lhs.shg8 * rhs,
				shb0 = lhs.shb0 * rhs,
				shb1 = lhs.shb1 * rhs,
				shb2 = lhs.shb2 * rhs,
				shb3 = lhs.shb3 * rhs,
				shb4 = lhs.shb4 * rhs,
				shb5 = lhs.shb5 * rhs,
				shb6 = lhs.shb6 * rhs,
				shb7 = lhs.shb7 * rhs,
				shb8 = lhs.shb8 * rhs
			};
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000C474 File Offset: 0x0000A674
		public static SphericalHarmonicsL2 operator *(float lhs, SphericalHarmonicsL2 rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = rhs.shr0 * lhs,
				shr1 = rhs.shr1 * lhs,
				shr2 = rhs.shr2 * lhs,
				shr3 = rhs.shr3 * lhs,
				shr4 = rhs.shr4 * lhs,
				shr5 = rhs.shr5 * lhs,
				shr6 = rhs.shr6 * lhs,
				shr7 = rhs.shr7 * lhs,
				shr8 = rhs.shr8 * lhs,
				shg0 = rhs.shg0 * lhs,
				shg1 = rhs.shg1 * lhs,
				shg2 = rhs.shg2 * lhs,
				shg3 = rhs.shg3 * lhs,
				shg4 = rhs.shg4 * lhs,
				shg5 = rhs.shg5 * lhs,
				shg6 = rhs.shg6 * lhs,
				shg7 = rhs.shg7 * lhs,
				shg8 = rhs.shg8 * lhs,
				shb0 = rhs.shb0 * lhs,
				shb1 = rhs.shb1 * lhs,
				shb2 = rhs.shb2 * lhs,
				shb3 = rhs.shb3 * lhs,
				shb4 = rhs.shb4 * lhs,
				shb5 = rhs.shb5 * lhs,
				shb6 = rhs.shb6 * lhs,
				shb7 = rhs.shb7 * lhs,
				shb8 = rhs.shb8 * lhs
			};
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000C63C File Offset: 0x0000A83C
		public static SphericalHarmonicsL2 operator +(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = lhs.shr0 + rhs.shr0,
				shr1 = lhs.shr1 + rhs.shr1,
				shr2 = lhs.shr2 + rhs.shr2,
				shr3 = lhs.shr3 + rhs.shr3,
				shr4 = lhs.shr4 + rhs.shr4,
				shr5 = lhs.shr5 + rhs.shr5,
				shr6 = lhs.shr6 + rhs.shr6,
				shr7 = lhs.shr7 + rhs.shr7,
				shr8 = lhs.shr8 + rhs.shr8,
				shg0 = lhs.shg0 + rhs.shg0,
				shg1 = lhs.shg1 + rhs.shg1,
				shg2 = lhs.shg2 + rhs.shg2,
				shg3 = lhs.shg3 + rhs.shg3,
				shg4 = lhs.shg4 + rhs.shg4,
				shg5 = lhs.shg5 + rhs.shg5,
				shg6 = lhs.shg6 + rhs.shg6,
				shg7 = lhs.shg7 + rhs.shg7,
				shg8 = lhs.shg8 + rhs.shg8,
				shb0 = lhs.shb0 + rhs.shb0,
				shb1 = lhs.shb1 + rhs.shb1,
				shb2 = lhs.shb2 + rhs.shb2,
				shb3 = lhs.shb3 + rhs.shb3,
				shb4 = lhs.shb4 + rhs.shb4,
				shb5 = lhs.shb5 + rhs.shb5,
				shb6 = lhs.shb6 + rhs.shb6,
				shb7 = lhs.shb7 + rhs.shb7,
				shb8 = lhs.shb8 + rhs.shb8
			};
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public static bool operator ==(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return lhs.shr0 == rhs.shr0 && lhs.shr1 == rhs.shr1 && lhs.shr2 == rhs.shr2 && lhs.shr3 == rhs.shr3 && lhs.shr4 == rhs.shr4 && lhs.shr5 == rhs.shr5 && lhs.shr6 == rhs.shr6 && lhs.shr7 == rhs.shr7 && lhs.shr8 == rhs.shr8 && lhs.shg0 == rhs.shg0 && lhs.shg1 == rhs.shg1 && lhs.shg2 == rhs.shg2 && lhs.shg3 == rhs.shg3 && lhs.shg4 == rhs.shg4 && lhs.shg5 == rhs.shg5 && lhs.shg6 == rhs.shg6 && lhs.shg7 == rhs.shg7 && lhs.shg8 == rhs.shg8 && lhs.shb0 == rhs.shb0 && lhs.shb1 == rhs.shb1 && lhs.shb2 == rhs.shb2 && lhs.shb3 == rhs.shb3 && lhs.shb4 == rhs.shb4 && lhs.shb5 == rhs.shb5 && lhs.shb6 == rhs.shb6 && lhs.shb7 == rhs.shb7 && lhs.shb8 == rhs.shb8;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public static bool operator !=(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0400018C RID: 396
		private float shr0;

		// Token: 0x0400018D RID: 397
		private float shr1;

		// Token: 0x0400018E RID: 398
		private float shr2;

		// Token: 0x0400018F RID: 399
		private float shr3;

		// Token: 0x04000190 RID: 400
		private float shr4;

		// Token: 0x04000191 RID: 401
		private float shr5;

		// Token: 0x04000192 RID: 402
		private float shr6;

		// Token: 0x04000193 RID: 403
		private float shr7;

		// Token: 0x04000194 RID: 404
		private float shr8;

		// Token: 0x04000195 RID: 405
		private float shg0;

		// Token: 0x04000196 RID: 406
		private float shg1;

		// Token: 0x04000197 RID: 407
		private float shg2;

		// Token: 0x04000198 RID: 408
		private float shg3;

		// Token: 0x04000199 RID: 409
		private float shg4;

		// Token: 0x0400019A RID: 410
		private float shg5;

		// Token: 0x0400019B RID: 411
		private float shg6;

		// Token: 0x0400019C RID: 412
		private float shg7;

		// Token: 0x0400019D RID: 413
		private float shg8;

		// Token: 0x0400019E RID: 414
		private float shb0;

		// Token: 0x0400019F RID: 415
		private float shb1;

		// Token: 0x040001A0 RID: 416
		private float shb2;

		// Token: 0x040001A1 RID: 417
		private float shb3;

		// Token: 0x040001A2 RID: 418
		private float shb4;

		// Token: 0x040001A3 RID: 419
		private float shb5;

		// Token: 0x040001A4 RID: 420
		private float shb6;

		// Token: 0x040001A5 RID: 421
		private float shb7;

		// Token: 0x040001A6 RID: 422
		private float shb8;
	}
}
