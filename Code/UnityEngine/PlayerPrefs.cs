using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D0 RID: 208
	public sealed class PlayerPrefs
	{
		// Token: 0x06000D18 RID: 3352
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x06000D19 RID: 3353
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetFloat(string key, float value);

		// Token: 0x06000D1A RID: 3354
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x06000D1B RID: 3355 RVA: 0x00010518 File Offset: 0x0000E718
		public static void SetInt(string key, int value)
		{
			if (!PlayerPrefs.TrySetInt(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000D1C RID: 3356
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, [DefaultValue("0")] int defaultValue);

		// Token: 0x06000D1D RID: 3357 RVA: 0x00010534 File Offset: 0x0000E734
		[ExcludeFromDocs]
		public static int GetInt(string key)
		{
			int defaultValue = 0;
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0001054C File Offset: 0x0000E74C
		public static void SetFloat(string key, float value)
		{
			if (!PlayerPrefs.TrySetFloat(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000D1F RID: 3359
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, [DefaultValue("0.0F")] float defaultValue);

		// Token: 0x06000D20 RID: 3360 RVA: 0x00010568 File Offset: 0x0000E768
		[ExcludeFromDocs]
		public static float GetFloat(string key)
		{
			float defaultValue = 0f;
			return PlayerPrefs.GetFloat(key, defaultValue);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00010584 File Offset: 0x0000E784
		public static void SetString(string key, string value)
		{
			if (!PlayerPrefs.TrySetSetString(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000D22 RID: 3362
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, [DefaultValue("\"\"")] string defaultValue);

		// Token: 0x06000D23 RID: 3363 RVA: 0x000105A0 File Offset: 0x0000E7A0
		[ExcludeFromDocs]
		public static string GetString(string key)
		{
			string empty = string.Empty;
			return PlayerPrefs.GetString(key, empty);
		}

		// Token: 0x06000D24 RID: 3364
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		// Token: 0x06000D25 RID: 3365
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteKey(string key);

		// Token: 0x06000D26 RID: 3366
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteAll();

		// Token: 0x06000D27 RID: 3367
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Save();
	}
}
