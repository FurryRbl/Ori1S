using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005F RID: 95
	[UsedByNativeCode]
	public struct Quaternion
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x000064D8 File Offset: 0x000046D8
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x17000160 RID: 352
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000065B0 File Offset: 0x000047B0
		public void Set(float new_x, float new_y, float new_z, float new_w)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
			this.w = new_w;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000065D0 File Offset: 0x000047D0
		public static Quaternion identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000065EC File Offset: 0x000047EC
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00006638 File Offset: 0x00004838
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_AngleAxis(angle, ref axis, out result);
			return result;
		}

		// Token: 0x06000557 RID: 1367
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AngleAxis(float angle, ref Vector3 axis, out Quaternion value);

		// Token: 0x06000558 RID: 1368 RVA: 0x00006650 File Offset: 0x00004850
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000666C File Offset: 0x0000486C
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_FromToRotation(ref fromDirection, ref toDirection, out result);
			return result;
		}

		// Token: 0x0600055A RID: 1370
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_FromToRotation(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion value);

		// Token: 0x0600055B RID: 1371 RVA: 0x00006688 File Offset: 0x00004888
		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00006698 File Offset: 0x00004898
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref upwards, out result);
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000066B4 File Offset: 0x000048B4
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			Vector3 up = Vector3.up;
			Quaternion result;
			Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref up, out result);
			return result;
		}

		// Token: 0x0600055E RID: 1374
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LookRotation(ref Vector3 forward, ref Vector3 upwards, out Quaternion value);

		// Token: 0x0600055F RID: 1375 RVA: 0x000066D4 File Offset: 0x000048D4
		[ExcludeFromDocs]
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000066F0 File Offset: 0x000048F0
		public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation(view, up);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00006700 File Offset: 0x00004900
		public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Slerp(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06000562 RID: 1378
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Slerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		// Token: 0x06000563 RID: 1379 RVA: 0x0000671C File Offset: 0x0000491C
		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06000564 RID: 1380
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SlerpUnclamped(ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		// Token: 0x06000565 RID: 1381 RVA: 0x00006738 File Offset: 0x00004938
		public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Lerp(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06000566 RID: 1382
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Lerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		// Token: 0x06000567 RID: 1383 RVA: 0x00006754 File Offset: 0x00004954
		public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_LerpUnclamped(ref a, ref b, t, out result);
			return result;
		}

		// Token: 0x06000568 RID: 1384
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LerpUnclamped(ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		// Token: 0x06000569 RID: 1385 RVA: 0x00006770 File Offset: 0x00004970
		public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			if (num == 0f)
			{
				return to;
			}
			float t = Mathf.Min(1f, maxDegreesDelta / num);
			return Quaternion.SlerpUnclamped(from, to, t);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000067A8 File Offset: 0x000049A8
		public static Quaternion Inverse(Quaternion rotation)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Inverse(ref rotation, out result);
			return result;
		}

		// Token: 0x0600056B RID: 1387
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Inverse(ref Quaternion rotation, out Quaternion value);

		// Token: 0x0600056C RID: 1388 RVA: 0x000067C0 File Offset: 0x000049C0
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

		// Token: 0x0600056D RID: 1389 RVA: 0x00006818 File Offset: 0x00004A18
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

		// Token: 0x0600056E RID: 1390 RVA: 0x00006874 File Offset: 0x00004A74
		public static float Angle(Quaternion a, Quaternion b)
		{
			float f = Quaternion.Dot(a, b);
			return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * 57.29578f;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000068AC File Offset: 0x00004AAC
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000068C4 File Offset: 0x00004AC4
		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.Internal_ToEulerRad(this) * 57.29578f;
			}
			set
			{
				this = Quaternion.Internal_FromEulerRad(value * 0.017453292f);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000068DC File Offset: 0x00004ADC
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000068F8 File Offset: 0x00004AF8
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000690C File Offset: 0x00004B0C
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			Vector3 result;
			Quaternion.INTERNAL_CALL_Internal_ToEulerRad(ref rotation, out result);
			return result;
		}

		// Token: 0x06000574 RID: 1396
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ToEulerRad(ref Quaternion rotation, out Vector3 value);

		// Token: 0x06000575 RID: 1397 RVA: 0x00006924 File Offset: 0x00004B24
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Internal_FromEulerRad(ref euler, out result);
			return result;
		}

		// Token: 0x06000576 RID: 1398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_FromEulerRad(ref Vector3 euler, out Quaternion value);

		// Token: 0x06000577 RID: 1399 RVA: 0x0000693C File Offset: 0x00004B3C
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.INTERNAL_CALL_Internal_ToAxisAngleRad(ref q, out axis, out angle);
		}

		// Token: 0x06000578 RID: 1400
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ToAxisAngleRad(ref Quaternion q, out Vector3 axis, out float angle);

		// Token: 0x06000579 RID: 1401 RVA: 0x00006948 File Offset: 0x00004B48
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00006958 File Offset: 0x00004B58
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00006960 File Offset: 0x00004B60
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00006978 File Offset: 0x00004B78
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00006988 File Offset: 0x00004B88
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public Vector3 ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00006998 File Offset: 0x00004B98
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000069A8 File Offset: 0x00004BA8
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000069B0 File Offset: 0x00004BB0
		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000069C0 File Offset: 0x00004BC0
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new Vector3(x, y, z));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000069D0 File Offset: 0x00004BD0
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles(Vector3 euler)
		{
			this = Quaternion.EulerRotation(euler);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000069E0 File Offset: 0x00004BE0
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000069E8 File Offset: 0x00004BE8
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public Vector3 ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000069F8 File Offset: 0x00004BF8
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion AxisAngle(Vector3 axis, float angle)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_AxisAngle(ref axis, angle, out result);
			return result;
		}

		// Token: 0x06000586 RID: 1414
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AxisAngle(ref Vector3 axis, float angle, out Quaternion value);

		// Token: 0x06000587 RID: 1415 RVA: 0x00006A10 File Offset: 0x00004C10
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00006A20 File Offset: 0x00004C20
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00006A64 File Offset: 0x00004C64
		public override bool Equals(object other)
		{
			if (!(other is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)other;
			return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result;
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00006D2C File Offset: 0x00004F2C
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) > 0.999999f;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00006D3C File Offset: 0x00004F3C
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) <= 0.999999f;
		}

		// Token: 0x040000DF RID: 223
		public const float kEpsilon = 1E-06f;

		// Token: 0x040000E0 RID: 224
		public float x;

		// Token: 0x040000E1 RID: 225
		public float y;

		// Token: 0x040000E2 RID: 226
		public float z;

		// Token: 0x040000E3 RID: 227
		public float w;
	}
}
