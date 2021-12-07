using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000068 RID: 104
	public struct Mathf
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x000099A4 File Offset: 0x00007BA4
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000099B0 File Offset: 0x00007BB0
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000099BC File Offset: 0x00007BBC
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000099C8 File Offset: 0x00007BC8
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000099D4 File Offset: 0x00007BD4
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000099E0 File Offset: 0x00007BE0
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000099EC File Offset: 0x00007BEC
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000099F8 File Offset: 0x00007BF8
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00009A04 File Offset: 0x00007C04
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00009A10 File Offset: 0x00007C10
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00009A18 File Offset: 0x00007C18
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00009A28 File Offset: 0x00007C28
		public static float Min(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] < num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00009A6C File Offset: 0x00007C6C
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00009A7C File Offset: 0x00007C7C
		public static int Min(params int[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0;
			}
			int num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] < num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00009ABC File Offset: 0x00007CBC
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00009ACC File Offset: 0x00007CCC
		public static float Max(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] > num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00009B10 File Offset: 0x00007D10
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00009B20 File Offset: 0x00007D20
		public static int Max(params int[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0;
			}
			int num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] > num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00009B60 File Offset: 0x00007D60
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00009B6C File Offset: 0x00007D6C
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00009B78 File Offset: 0x00007D78
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00009B84 File Offset: 0x00007D84
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00009B90 File Offset: 0x00007D90
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00009B9C File Offset: 0x00007D9C
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00009BA8 File Offset: 0x00007DA8
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00009BCC File Offset: 0x00007DCC
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00009BE4 File Offset: 0x00007DE4
		public static float Sign(float f)
		{
			return (f < 0f) ? -1f : 1f;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00009C00 File Offset: 0x00007E00
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00009C1C File Offset: 0x00007E1C
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00009C38 File Offset: 0x00007E38
		public static float Clamp01(float value)
		{
			if (value < 0f)
			{
				return 0f;
			}
			if (value > 1f)
			{
				return 1f;
			}
			return value;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00009C60 File Offset: 0x00007E60
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01(t);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00009C70 File Offset: 0x00007E70
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00009C7C File Offset: 0x00007E7C
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			if (Mathf.Abs(target - current) <= maxDelta)
			{
				return target;
			}
			return current + Mathf.Sign(target - current) * maxDelta;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.MoveTowards(current, target, maxDelta);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00009CEC File Offset: 0x00007EEC
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00009D28 File Offset: 0x00007F28
		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = false;
			if (value < 0f)
			{
				flag = true;
			}
			float num = Mathf.Abs(value);
			if (num > absmax)
			{
				return (!flag) ? num : (-num);
			}
			float num2 = Mathf.Pow(num / absmax, gamma) * absmax;
			return (!flag) ? num2 : (-num2);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00009D7C File Offset: 0x00007F7C
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00009DBC File Offset: 0x00007FBC
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00009DDC File Offset: 0x00007FDC
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00009E00 File Offset: 0x00008000
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			if (num5 - current > 0f == num8 > num5)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00009EB0 File Offset: 0x000080B0
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00009ED0 File Offset: 0x000080D0
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00009EF4 File Offset: 0x000080F4
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00009F10 File Offset: 0x00008110
		public static float Repeat(float t, float length)
		{
			return t - Mathf.Floor(t / length) * length;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00009F20 File Offset: 0x00008120
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00009F3C File Offset: 0x0000813C
		public static float InverseLerp(float a, float b, float value)
		{
			if (a != b)
			{
				return Mathf.Clamp01((value - a) / (b - a));
			}
			return 0f;
		}

		// Token: 0x06000687 RID: 1671
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ClosestPowerOfTwo(int value);

		// Token: 0x06000688 RID: 1672
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x06000689 RID: 1673
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x0600068A RID: 1674
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPowerOfTwo(int value);

		// Token: 0x0600068B RID: 1675
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NextPowerOfTwo(int value);

		// Token: 0x0600068C RID: 1676 RVA: 0x00009F58 File Offset: 0x00008158
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x0600068D RID: 1677
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x0600068E RID: 1678 RVA: 0x00009F88 File Offset: 0x00008188
		internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			if (num5 == 0f)
			{
				return false;
			}
			float num6 = p3.x - p1.x;
			float num7 = p3.y - p1.y;
			float num8 = (num6 * num4 - num7 * num3) / num5;
			result = new Vector2(p1.x + num8 * num, p1.y + num8 * num2);
			return true;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000A03C File Offset: 0x0000823C
		internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			if (num5 == 0f)
			{
				return false;
			}
			float num6 = p3.x - p1.x;
			float num7 = p3.y - p1.y;
			float num8 = (num6 * num4 - num7 * num3) / num5;
			if (num8 < 0f || num8 > 1f)
			{
				return false;
			}
			float num9 = (num6 * num2 - num7 * num) / num5;
			if (num9 < 0f || num9 > 1f)
			{
				return false;
			}
			result = new Vector2(p1.x + num8 * num, p1.y + num8 * num2);
			return true;
		}

		// Token: 0x06000690 RID: 1680
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort FloatToHalf(float val);

		// Token: 0x06000691 RID: 1681
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float HalfToFloat(ushort val);

		// Token: 0x06000692 RID: 1682 RVA: 0x0000A134 File Offset: 0x00008334
		internal static long RandomToLong(Random r)
		{
			byte[] array = new byte[8];
			r.NextBytes(array);
			return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807UL);
		}

		// Token: 0x04000108 RID: 264
		public const float PI = 3.1415927f;

		// Token: 0x04000109 RID: 265
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x0400010A RID: 266
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x0400010B RID: 267
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x0400010C RID: 268
		public const float Rad2Deg = 57.29578f;

		// Token: 0x0400010D RID: 269
		public static readonly float Epsilon = (!MathfInternal.IsFlushToZeroEnabled) ? MathfInternal.FloatMinDenormal : MathfInternal.FloatMinNormal;
	}
}
