using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200012D RID: 301
	internal struct MonoEnumInfo
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x00045230 File Offset: 0x00043430
		private MonoEnumInfo(MonoEnumInfo other)
		{
			this.utype = other.utype;
			this.values = other.values;
			this.names = other.names;
			this.name_hash = other.name_hash;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00045274 File Offset: 0x00043474
		static MonoEnumInfo()
		{
			MonoEnumInfo.global_cache_monitor = new object();
			MonoEnumInfo.global_cache = new Hashtable();
		}

		// Token: 0x060010E9 RID: 4329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_enum_info(Type enumType, out MonoEnumInfo info);

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x000452C0 File Offset: 0x000434C0
		private static Hashtable Cache
		{
			get
			{
				if (MonoEnumInfo.cache == null)
				{
					MonoEnumInfo.cache = new Hashtable();
				}
				return MonoEnumInfo.cache;
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000452DC File Offset: 0x000434DC
		internal static void GetInfo(Type enumType, out MonoEnumInfo info)
		{
			if (MonoEnumInfo.Cache.ContainsKey(enumType))
			{
				info = (MonoEnumInfo)MonoEnumInfo.cache[enumType];
				return;
			}
			object obj = MonoEnumInfo.global_cache_monitor;
			lock (obj)
			{
				if (MonoEnumInfo.global_cache.ContainsKey(enumType))
				{
					object obj2 = MonoEnumInfo.global_cache[enumType];
					MonoEnumInfo.cache[enumType] = obj2;
					info = (MonoEnumInfo)obj2;
					return;
				}
			}
			MonoEnumInfo.get_enum_info(enumType, out info);
			IComparer comparer = null;
			if (!(info.values is byte[]) && !(info.values is ushort[]) && !(info.values is uint[]) && !(info.values is ulong[]))
			{
				if (info.values is int[])
				{
					comparer = MonoEnumInfo.int_comparer;
				}
				else if (info.values is short[])
				{
					comparer = MonoEnumInfo.short_comparer;
				}
				else if (info.values is sbyte[])
				{
					comparer = MonoEnumInfo.sbyte_comparer;
				}
				else if (info.values is long[])
				{
					comparer = MonoEnumInfo.long_comparer;
				}
			}
			Array.Sort(info.values, info.names, comparer);
			if (info.names.Length > 50)
			{
				info.name_hash = new Hashtable(info.names.Length);
				for (int i = 0; i < info.names.Length; i++)
				{
					info.name_hash[info.names[i]] = i;
				}
			}
			MonoEnumInfo monoEnumInfo = new MonoEnumInfo(info);
			object obj3 = MonoEnumInfo.global_cache_monitor;
			lock (obj3)
			{
				MonoEnumInfo.global_cache[enumType] = monoEnumInfo;
			}
		}

		// Token: 0x040004D4 RID: 1236
		internal Type utype;

		// Token: 0x040004D5 RID: 1237
		internal Array values;

		// Token: 0x040004D6 RID: 1238
		internal string[] names;

		// Token: 0x040004D7 RID: 1239
		internal Hashtable name_hash;

		// Token: 0x040004D8 RID: 1240
		[ThreadStatic]
		private static Hashtable cache;

		// Token: 0x040004D9 RID: 1241
		private static Hashtable global_cache;

		// Token: 0x040004DA RID: 1242
		private static object global_cache_monitor;

		// Token: 0x040004DB RID: 1243
		internal static MonoEnumInfo.SByteComparer sbyte_comparer = new MonoEnumInfo.SByteComparer();

		// Token: 0x040004DC RID: 1244
		internal static MonoEnumInfo.ShortComparer short_comparer = new MonoEnumInfo.ShortComparer();

		// Token: 0x040004DD RID: 1245
		internal static MonoEnumInfo.IntComparer int_comparer = new MonoEnumInfo.IntComparer();

		// Token: 0x040004DE RID: 1246
		internal static MonoEnumInfo.LongComparer long_comparer = new MonoEnumInfo.LongComparer();

		// Token: 0x0200012E RID: 302
		internal class SByteComparer : IComparer<sbyte>, IComparer
		{
			// Token: 0x060010ED RID: 4333 RVA: 0x000454F0 File Offset: 0x000436F0
			public int Compare(object x, object y)
			{
				sbyte b = (sbyte)x;
				sbyte b2 = (sbyte)y;
				return (int)((byte)b - (byte)b2);
			}

			// Token: 0x060010EE RID: 4334 RVA: 0x00045510 File Offset: 0x00043710
			public int Compare(sbyte ix, sbyte iy)
			{
				return (int)((byte)ix - (byte)iy);
			}
		}

		// Token: 0x0200012F RID: 303
		internal class ShortComparer : IComparer<short>, IComparer
		{
			// Token: 0x060010F0 RID: 4336 RVA: 0x00045520 File Offset: 0x00043720
			public int Compare(object x, object y)
			{
				short num = (short)x;
				short num2 = (short)y;
				return (int)((ushort)num - (ushort)num2);
			}

			// Token: 0x060010F1 RID: 4337 RVA: 0x00045540 File Offset: 0x00043740
			public int Compare(short ix, short iy)
			{
				return (int)((ushort)ix - (ushort)iy);
			}
		}

		// Token: 0x02000130 RID: 304
		internal class IntComparer : IComparer<int>, IComparer
		{
			// Token: 0x060010F3 RID: 4339 RVA: 0x00045550 File Offset: 0x00043750
			public int Compare(object x, object y)
			{
				int num = (int)x;
				int num2 = (int)y;
				if (num == num2)
				{
					return 0;
				}
				if (num < num2)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x00045580 File Offset: 0x00043780
			public int Compare(int ix, int iy)
			{
				if (ix == iy)
				{
					return 0;
				}
				if (ix < iy)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x02000131 RID: 305
		internal class LongComparer : IComparer<long>, IComparer
		{
			// Token: 0x060010F6 RID: 4342 RVA: 0x000455A0 File Offset: 0x000437A0
			public int Compare(object x, object y)
			{
				long num = (long)x;
				long num2 = (long)y;
				if (num == num2)
				{
					return 0;
				}
				if (num < num2)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x060010F7 RID: 4343 RVA: 0x000455D0 File Offset: 0x000437D0
			public int Compare(long ix, long iy)
			{
				if (ix == iy)
				{
					return 0;
				}
				if (ix < iy)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
