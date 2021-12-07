using System;
using System.Runtime.InteropServices;

namespace Mono.Posix
{
	// Token: 0x0200006A RID: 106
	[Obsolete("Use Mono.Unix.Catalog")]
	public class Catalog
	{
		// Token: 0x06000508 RID: 1288
		[DllImport("intl")]
		private static extern IntPtr bindtextdomain(IntPtr domainname, IntPtr dirname);

		// Token: 0x06000509 RID: 1289
		[DllImport("intl")]
		private static extern IntPtr bind_textdomain_codeset(IntPtr domainname, IntPtr codeset);

		// Token: 0x0600050A RID: 1290
		[DllImport("intl")]
		private static extern IntPtr textdomain(IntPtr domainname);

		// Token: 0x0600050B RID: 1291 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public static void Init(string package, string localedir)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(package);
			IntPtr intPtr2 = Marshal.StringToHGlobalAuto(localedir);
			IntPtr intPtr3 = Marshal.StringToHGlobalAuto("UTF-8");
			Catalog.bindtextdomain(intPtr, intPtr2);
			Catalog.bind_textdomain_codeset(intPtr, intPtr3);
			Catalog.textdomain(intPtr);
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			Marshal.FreeHGlobal(intPtr3);
		}

		// Token: 0x0600050C RID: 1292
		[DllImport("intl")]
		private static extern IntPtr gettext(IntPtr instring);

		// Token: 0x0600050D RID: 1293 RVA: 0x0000D408 File Offset: 0x0000B608
		public static string GetString(string s)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			string result = Marshal.PtrToStringAuto(Catalog.gettext(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x0600050E RID: 1294
		[DllImport("intl")]
		private static extern IntPtr ngettext(IntPtr singular, IntPtr plural, int n);

		// Token: 0x0600050F RID: 1295 RVA: 0x0000D430 File Offset: 0x0000B630
		public static string GetPluralString(string s, string p, int n)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			IntPtr intPtr2 = Marshal.StringToHGlobalAuto(p);
			string result = Marshal.PtrToStringAnsi(Catalog.ngettext(intPtr, intPtr2, n));
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			return result;
		}
	}
}
