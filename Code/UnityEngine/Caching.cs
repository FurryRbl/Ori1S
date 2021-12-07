using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	public sealed class Caching
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00002570 File Offset: 0x00000770
		public static bool Authorize(string name, string domain, long size, string signature)
		{
			return Caching.Authorize(name, domain, size, -1, signature);
		}

		// Token: 0x06000091 RID: 145
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Authorize(string name, string domain, long size, int expiration, string signature);

		// Token: 0x06000092 RID: 146 RVA: 0x0000257C File Offset: 0x0000077C
		[Obsolete("Size is now specified as a long")]
		public static bool Authorize(string name, string domain, int size, int expiration, string signature)
		{
			return Caching.Authorize(name, domain, (long)size, expiration, signature);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000258C File Offset: 0x0000078C
		[Obsolete("Size is now specified as a long")]
		public static bool Authorize(string name, string domain, int size, string signature)
		{
			return Caching.Authorize(name, domain, (long)size, signature);
		}

		// Token: 0x06000094 RID: 148
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CleanCache();

		// Token: 0x06000095 RID: 149
		[Obsolete("this API is not for public use.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CleanNamedCache(string name);

		// Token: 0x06000096 RID: 150
		[WrapperlessIcall]
		[Obsolete("This function is obsolete and has no effect.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DeleteFromCache(string url);

		// Token: 0x06000097 RID: 151
		[WrapperlessIcall]
		[Obsolete("This function is obsolete and will always return -1. Use IsVersionCached instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVersionFromCache(string url);

		// Token: 0x06000098 RID: 152 RVA: 0x00002598 File Offset: 0x00000798
		public static bool IsVersionCached(string url, int version)
		{
			Hash128 hash = new Hash128(0U, 0U, 0U, (uint)version);
			return Caching.IsVersionCached(url, hash);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000025B8 File Offset: 0x000007B8
		public static bool IsVersionCached(string url, Hash128 hash)
		{
			return Caching.INTERNAL_CALL_IsVersionCached(url, ref hash);
		}

		// Token: 0x0600009A RID: 154
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_IsVersionCached(string url, ref Hash128 hash);

		// Token: 0x0600009B RID: 155 RVA: 0x000025C4 File Offset: 0x000007C4
		public static bool MarkAsUsed(string url, int version)
		{
			Hash128 hash = new Hash128(0U, 0U, 0U, (uint)version);
			return Caching.MarkAsUsed(url, hash);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000025E4 File Offset: 0x000007E4
		public static bool MarkAsUsed(string url, Hash128 hash)
		{
			return Caching.INTERNAL_CALL_MarkAsUsed(url, ref hash);
		}

		// Token: 0x0600009D RID: 157
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_MarkAsUsed(string url, ref Hash128 hash);

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009E RID: 158
		[Obsolete("this API is not for public use.")]
		public static extern CacheIndex[] index { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009F RID: 159
		public static extern long spaceFree { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A0 RID: 160
		// (set) Token: 0x060000A1 RID: 161
		public static extern long maximumAvailableDiskSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A2 RID: 162
		public static extern long spaceOccupied { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A3 RID: 163
		[Obsolete("Please use Caching.spaceFree instead")]
		public static extern int spaceAvailable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A4 RID: 164
		[Obsolete("Please use Caching.spaceOccupied instead")]
		public static extern int spaceUsed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		public static extern int expirationDelay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A9 RID: 169
		// (set) Token: 0x060000AA RID: 170
		public static extern bool compressionEnabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AB RID: 171
		public static extern bool ready { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
