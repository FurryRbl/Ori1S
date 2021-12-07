using System;
using System.Runtime.InteropServices;
using System.Text;
using ManagedSteam.Exceptions;

namespace ManagedSteam.Utility
{
	// Token: 0x02000099 RID: 153
	internal class NativeString : DisposableClass
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00008C18 File Offset: 0x00006E18
		public NativeString(IntPtr nativeString)
		{
			this.nativeString = nativeString;
			this.nativeUtf8 = IntPtr.Zero;
			this.nativeAnsi = IntPtr.Zero;
			this.managedString = string.Empty;
			this.originatedFromManaged = false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00008C4F File Offset: 0x00006E4F
		public NativeString(string managedString)
		{
			this.nativeString = IntPtr.Zero;
			this.nativeUtf8 = IntPtr.Zero;
			this.nativeAnsi = IntPtr.Zero;
			this.managedString = managedString;
			this.originatedFromManaged = true;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00008C88 File Offset: 0x00006E88
		public string ToStringFromUtf8()
		{
			if (this.originatedFromManaged)
			{
				return this.managedString;
			}
			if (IntPtr.Zero == this.nativeString)
			{
				return string.Empty;
			}
			int num = 0;
			while (Marshal.ReadByte(this.nativeString, num) != 0)
			{
				num++;
			}
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			Marshal.Copy(this.nativeString, array, 0, array.Length);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00008CFD File Offset: 0x00006EFD
		public string ToStringFromAnsi()
		{
			if (this.originatedFromManaged)
			{
				return this.managedString;
			}
			return Marshal.PtrToStringAnsi(this.nativeString);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00008D1C File Offset: 0x00006F1C
		public IntPtr ToNativeAsUtf8()
		{
			if (!this.originatedFromManaged)
			{
				throw new ManagedException(ErrorCodes.CantChangeEncoding, new object[0]);
			}
			if (this.nativeUtf8 != IntPtr.Zero)
			{
				return this.nativeUtf8;
			}
			Encoding utf = Encoding.UTF8;
			int byteCount = utf.GetByteCount(this.managedString);
			byte[] array = new byte[byteCount + 1];
			utf.GetBytes(this.managedString, 0, this.managedString.Length, array, 0);
			this.nativeUtf8 = Marshal.AllocHGlobal(array.Length);
			Marshal.Copy(array, 0, this.nativeUtf8, array.Length);
			return this.nativeUtf8;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public IntPtr ToNativeAsAnsi()
		{
			if (!this.originatedFromManaged)
			{
				throw new ManagedException(ErrorCodes.CantChangeEncoding, new object[0]);
			}
			if (this.nativeAnsi != IntPtr.Zero)
			{
				return this.nativeAnsi;
			}
			this.nativeAnsi = Marshal.StringToHGlobalAnsi(this.managedString);
			return this.nativeAnsi;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00008E10 File Offset: 0x00007010
		protected override void CleanUpNativeResources()
		{
			if (this.nativeUtf8 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.nativeUtf8);
			}
			if (this.nativeAnsi != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.nativeAnsi);
			}
			base.CleanUpNativeResources();
		}

		// Token: 0x040002BF RID: 703
		private IntPtr nativeString;

		// Token: 0x040002C0 RID: 704
		private IntPtr nativeUtf8;

		// Token: 0x040002C1 RID: 705
		private IntPtr nativeAnsi;

		// Token: 0x040002C2 RID: 706
		private string managedString;

		// Token: 0x040002C3 RID: 707
		private bool originatedFromManaged;
	}
}
