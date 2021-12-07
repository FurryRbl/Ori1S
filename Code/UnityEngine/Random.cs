using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000CD RID: 205
	public sealed class Random
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000CFE RID: 3326
		// (set) Token: 0x06000CFF RID: 3327
		public static extern int seed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000D00 RID: 3328
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float min, float max);

		// Token: 0x06000D01 RID: 3329 RVA: 0x00010348 File Offset: 0x0000E548
		public static int Range(int min, int max)
		{
			return Random.RandomRangeInt(min, max);
		}

		// Token: 0x06000D02 RID: 3330
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int min, int max);

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D03 RID: 3331
		public static extern float value { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00010354 File Offset: 0x0000E554
		public static Vector3 insideUnitSphere
		{
			get
			{
				Vector3 result;
				Random.INTERNAL_get_insideUnitSphere(out result);
				return result;
			}
		}

		// Token: 0x06000D05 RID: 3333
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_insideUnitSphere(out Vector3 value);

		// Token: 0x06000D06 RID: 3334
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0001036C File Offset: 0x0000E56C
		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 result;
				Random.GetRandomUnitCircle(out result);
				return result;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00010384 File Offset: 0x0000E584
		public static Vector3 onUnitSphere
		{
			get
			{
				Vector3 result;
				Random.INTERNAL_get_onUnitSphere(out result);
				return result;
			}
		}

		// Token: 0x06000D09 RID: 3337
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_onUnitSphere(out Vector3 value);

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0001039C File Offset: 0x0000E59C
		public static Quaternion rotation
		{
			get
			{
				Quaternion result;
				Random.INTERNAL_get_rotation(out result);
				return result;
			}
		}

		// Token: 0x06000D0B RID: 3339
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public static Quaternion rotationUniform
		{
			get
			{
				Quaternion result;
				Random.INTERNAL_get_rotationUniform(out result);
				return result;
			}
		}

		// Token: 0x06000D0D RID: 3341
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_rotationUniform(out Quaternion value);

		// Token: 0x06000D0E RID: 3342 RVA: 0x000103CC File Offset: 0x0000E5CC
		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000103D8 File Offset: 0x0000E5D8
		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000103E4 File Offset: 0x0000E5E4
		public static Color ColorHSV()
		{
			return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00010420 File Offset: 0x0000E620
		public static Color ColorHSV(float hueMin, float hueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00010454 File Offset: 0x0000E654
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00010480 File Offset: 0x0000E680
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000104A4 File Offset: 0x0000E6A4
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float h = Mathf.Lerp(hueMin, hueMax, Random.value);
			float s = Mathf.Lerp(saturationMin, saturationMax, Random.value);
			float v = Mathf.Lerp(valueMin, valueMax, Random.value);
			Color result = Color.HSVToRGB(h, s, v, true);
			result.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
			return result;
		}
	}
}
