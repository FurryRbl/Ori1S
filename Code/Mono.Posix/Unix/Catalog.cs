﻿using System;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000005 RID: 5
	public class Catalog
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000225C File Offset: 0x0000045C
		private Catalog()
		{
		}

		// Token: 0x0600000E RID: 14
		[DllImport("intl")]
		private static extern IntPtr bindtextdomain(IntPtr domainname, IntPtr dirname);

		// Token: 0x0600000F RID: 15
		[DllImport("intl")]
		private static extern IntPtr bind_textdomain_codeset(IntPtr domainname, IntPtr codeset);

		// Token: 0x06000010 RID: 16
		[DllImport("intl")]
		private static extern IntPtr textdomain(IntPtr domainname);

		// Token: 0x06000011 RID: 17 RVA: 0x00002264 File Offset: 0x00000464
		public static void Init(string package, string localedir)
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			Catalog.MarshalStrings(package, out intPtr, localedir, out intPtr2, "UTF-8", out intPtr3);
			try
			{
				if (Catalog.bindtextdomain(intPtr, intPtr2) == IntPtr.Zero)
				{
					throw new UnixIOException(Errno.ENOMEM);
				}
				if (Catalog.bind_textdomain_codeset(intPtr, intPtr3) == IntPtr.Zero)
				{
					throw new UnixIOException(Errno.ENOMEM);
				}
				if (Catalog.textdomain(intPtr) == IntPtr.Zero)
				{
					throw new UnixIOException(Errno.ENOMEM);
				}
			}
			finally
			{
				UnixMarshal.FreeHeap(intPtr);
				UnixMarshal.FreeHeap(intPtr2);
				UnixMarshal.FreeHeap(intPtr3);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002310 File Offset: 0x00000510
		private static void MarshalStrings(string s1, out IntPtr p1, string s2, out IntPtr p2, string s3, out IntPtr p3)
		{
			p1 = (p2 = (p3 = IntPtr.Zero));
			bool flag = true;
			try
			{
				p1 = UnixMarshal.StringToHeap(s1);
				p2 = UnixMarshal.StringToHeap(s2);
				if (s3 != null)
				{
					p3 = UnixMarshal.StringToHeap(s3);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					UnixMarshal.FreeHeap(p1);
					UnixMarshal.FreeHeap(p2);
					UnixMarshal.FreeHeap(p3);
				}
			}
		}

		// Token: 0x06000013 RID: 19
		[DllImport("intl")]
		private static extern IntPtr gettext(IntPtr instring);

		// Token: 0x06000014 RID: 20 RVA: 0x00002394 File Offset: 0x00000594
		public static string GetString(string s)
		{
			IntPtr intPtr = UnixMarshal.StringToHeap(s);
			string result;
			try
			{
				IntPtr intPtr2 = Catalog.gettext(intPtr);
				if (intPtr2 != intPtr)
				{
					result = UnixMarshal.PtrToStringUnix(intPtr2);
				}
				else
				{
					result = s;
				}
			}
			finally
			{
				UnixMarshal.FreeHeap(intPtr);
			}
			return result;
		}

		// Token: 0x06000015 RID: 21
		[DllImport("intl")]
		private static extern IntPtr ngettext(IntPtr singular, IntPtr plural, int n);

		// Token: 0x06000016 RID: 22 RVA: 0x000023F8 File Offset: 0x000005F8
		public static string GetPluralString(string s, string p, int n)
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			Catalog.MarshalStrings(s, out intPtr, p, out intPtr2, null, out intPtr3);
			string result;
			try
			{
				IntPtr intPtr4 = Catalog.ngettext(intPtr, intPtr2, n);
				if (intPtr4 == intPtr)
				{
					result = s;
				}
				else if (intPtr4 == intPtr2)
				{
					result = p;
				}
				else
				{
					result = UnixMarshal.PtrToStringUnix(intPtr4);
				}
			}
			finally
			{
				UnixMarshal.FreeHeap(intPtr);
				UnixMarshal.FreeHeap(intPtr2);
			}
			return result;
		}
	}
}
