using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Sprites
{
	// Token: 0x020000A0 RID: 160
	public sealed class DataUtility
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		public static Vector4 GetInnerUV(Sprite sprite)
		{
			Vector4 result;
			DataUtility.INTERNAL_CALL_GetInnerUV(sprite, out result);
			return result;
		}

		// Token: 0x06000926 RID: 2342
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetInnerUV(Sprite sprite, out Vector4 value);

		// Token: 0x06000927 RID: 2343 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public static Vector4 GetOuterUV(Sprite sprite)
		{
			Vector4 result;
			DataUtility.INTERNAL_CALL_GetOuterUV(sprite, out result);
			return result;
		}

		// Token: 0x06000928 RID: 2344
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetOuterUV(Sprite sprite, out Vector4 value);

		// Token: 0x06000929 RID: 2345 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		public static Vector4 GetPadding(Sprite sprite)
		{
			Vector4 result;
			DataUtility.INTERNAL_CALL_GetPadding(sprite, out result);
			return result;
		}

		// Token: 0x0600092A RID: 2346
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPadding(Sprite sprite, out Vector4 value);

		// Token: 0x0600092B RID: 2347 RVA: 0x0000CD00 File Offset: 0x0000AF00
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 result;
			DataUtility.Internal_GetMinSize(sprite, out result);
			return result;
		}

		// Token: 0x0600092C RID: 2348
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetMinSize(Sprite sprite, out Vector2 output);
	}
}
