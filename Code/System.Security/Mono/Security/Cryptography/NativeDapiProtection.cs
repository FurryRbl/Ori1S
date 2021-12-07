using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000005 RID: 5
	internal class NativeDapiProtection
	{
		// Token: 0x06000008 RID: 8
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectData(ref NativeDapiProtection.DATA_BLOB pDataIn, string szDataDescr, ref NativeDapiProtection.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, uint dwFlags, ref NativeDapiProtection.DATA_BLOB pDataOut);

		// Token: 0x06000009 RID: 9
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref NativeDapiProtection.DATA_BLOB pDataIn, string szDataDescr, ref NativeDapiProtection.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, uint dwFlags, ref NativeDapiProtection.DATA_BLOB pDataOut);

		// Token: 0x0600000A RID: 10
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "RtlZeroMemory")]
		private static extern void ZeroMemory(IntPtr dest, int size);

		// Token: 0x0600000B RID: 11 RVA: 0x00002728 File Offset: 0x00000928
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			byte[] array = null;
			int num = 0;
			NativeDapiProtection.DATA_BLOB data_BLOB = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB2 = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB3 = default(NativeDapiProtection.DATA_BLOB);
			try
			{
				NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = new NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT(0U);
				data_BLOB.Alloc(userData);
				data_BLOB2.Alloc(optionalEntropy);
				uint num2 = 1U;
				if (scope == DataProtectionScope.LocalMachine)
				{
					num2 |= 4U;
				}
				if (NativeDapiProtection.CryptProtectData(ref data_BLOB, string.Empty, ref data_BLOB2, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num2, ref data_BLOB3))
				{
					array = data_BLOB3.ToBytes();
				}
				else
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			catch (Exception inner)
			{
				string text = Locale.GetText("Error protecting data.");
				throw new CryptographicException(text, inner);
			}
			finally
			{
				data_BLOB3.Free();
				data_BLOB.Free();
				data_BLOB2.Free();
			}
			if (array == null || num != 0)
			{
				throw new CryptographicException(num);
			}
			return array;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000282C File Offset: 0x00000A2C
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			byte[] array = null;
			int num = 0;
			NativeDapiProtection.DATA_BLOB data_BLOB = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB2 = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB3 = default(NativeDapiProtection.DATA_BLOB);
			try
			{
				NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = new NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT(0U);
				data_BLOB.Alloc(encryptedData);
				data_BLOB2.Alloc(optionalEntropy);
				uint num2 = 1U;
				if (scope == DataProtectionScope.LocalMachine)
				{
					num2 |= 4U;
				}
				if (NativeDapiProtection.CryptUnprotectData(ref data_BLOB, null, ref data_BLOB2, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num2, ref data_BLOB3))
				{
					array = data_BLOB3.ToBytes();
				}
				else
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			catch (Exception inner)
			{
				string text = Locale.GetText("Error protecting data.");
				throw new CryptographicException(text, inner);
			}
			finally
			{
				data_BLOB.Free();
				data_BLOB3.Free();
				data_BLOB2.Free();
			}
			if (array == null || num != 0)
			{
				throw new CryptographicException(num);
			}
			return array;
		}

		// Token: 0x04000020 RID: 32
		private const uint CRYPTPROTECT_UI_FORBIDDEN = 1U;

		// Token: 0x04000021 RID: 33
		private const uint CRYPTPROTECT_LOCAL_MACHINE = 4U;

		// Token: 0x02000006 RID: 6
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct DATA_BLOB
		{
			// Token: 0x0600000D RID: 13 RVA: 0x0000292C File Offset: 0x00000B2C
			public void Alloc(int size)
			{
				if (size > 0)
				{
					this.pbData = Marshal.AllocHGlobal(size);
					this.cbData = size;
				}
			}

			// Token: 0x0600000E RID: 14 RVA: 0x00002948 File Offset: 0x00000B48
			public void Alloc(byte[] managedMemory)
			{
				if (managedMemory != null)
				{
					int cb = managedMemory.Length;
					this.pbData = Marshal.AllocHGlobal(cb);
					this.cbData = cb;
					Marshal.Copy(managedMemory, 0, this.pbData, this.cbData);
				}
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002988 File Offset: 0x00000B88
			public void Free()
			{
				if (this.pbData != IntPtr.Zero)
				{
					NativeDapiProtection.ZeroMemory(this.pbData, this.cbData);
					Marshal.FreeHGlobal(this.pbData);
					this.pbData = IntPtr.Zero;
					this.cbData = 0;
				}
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000029D8 File Offset: 0x00000BD8
			public byte[] ToBytes()
			{
				if (this.cbData <= 0)
				{
					return new byte[0];
				}
				byte[] array = new byte[this.cbData];
				Marshal.Copy(this.pbData, array, 0, this.cbData);
				return array;
			}

			// Token: 0x04000022 RID: 34
			private int cbData;

			// Token: 0x04000023 RID: 35
			private IntPtr pbData;
		}

		// Token: 0x02000007 RID: 7
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct CRYPTPROTECT_PROMPTSTRUCT
		{
			// Token: 0x06000011 RID: 17 RVA: 0x00002A18 File Offset: 0x00000C18
			public CRYPTPROTECT_PROMPTSTRUCT(uint flags)
			{
				this.cbSize = Marshal.SizeOf(typeof(NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT));
				this.dwPromptFlags = flags;
				this.hwndApp = IntPtr.Zero;
				this.szPrompt = null;
			}

			// Token: 0x04000024 RID: 36
			private int cbSize;

			// Token: 0x04000025 RID: 37
			private uint dwPromptFlags;

			// Token: 0x04000026 RID: 38
			private IntPtr hwndApp;

			// Token: 0x04000027 RID: 39
			private string szPrompt;
		}
	}
}
