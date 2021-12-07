using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Provides methods for protecting and unprotecting memory. This class cannot be inherited.</summary>
	// Token: 0x02000013 RID: 19
	public sealed class ProtectedMemory
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000043B8 File Offset: 0x000025B8
		private ProtectedMemory()
		{
		}

		/// <summary>Protects the specified data.</summary>
		/// <param name="userData">The byte array containing data in memory to protect. The array must be a multiple of 16 bytes. </param>
		/// <param name="scope">One of the <see cref="T:System.Security.Cryptography.MemoryProtectionScope" /> values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="userData" /> must be 16 bytes in length or in multiples of 16 bytes. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support this method. This method can be used only with Microsoft Windows 2000 or later operating systems. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="userData " />is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ProtectMemory" />
		/// </PermissionSet>
		// Token: 0x06000048 RID: 72 RVA: 0x000043C0 File Offset: 0x000025C0
		[MonoTODO("only supported on Windows 2000 SP3 and later")]
		public static void Protect(byte[] userData, MemoryProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			ProtectedMemory.Check(userData.Length, scope);
			try
			{
				uint cbData = (uint)userData.Length;
				ProtectedMemory.MemoryProtectionImplementation memoryProtectionImplementation = ProtectedMemory.impl;
				if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32RtlEncryptMemory)
				{
					if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect)
					{
						throw new PlatformNotSupportedException();
					}
					if (!ProtectedMemory.CryptProtectMemory(userData, cbData, (uint)scope))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					int num = ProtectedMemory.RtlEncryptMemory(userData, cbData, (uint)scope);
					if (num < 0)
					{
						string text = Locale.GetText("Error. NTSTATUS = {0}.", new object[]
						{
							num
						});
						throw new CryptographicException(text);
					}
				}
			}
			catch
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Unprotects data in memory that was protected using the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
		/// <param name="encryptedData">The byte array in memory to unencrypt. </param>
		/// <param name="scope">One of the <see cref="T:System.Security.Cryptography.MemoryProtectionScope" /> values. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support this method. This method can be used only with Microsoft Windows 2000 or later operating systems. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encryptedData " />is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="encryptedData " />is empty.-or-This call was not implemented.-or-NTSTATUS contains an error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnprotectMemory" />
		/// </PermissionSet>
		// Token: 0x06000049 RID: 73 RVA: 0x000044A0 File Offset: 0x000026A0
		[MonoTODO("only supported on Windows 2000 SP3 and later")]
		public static void Unprotect(byte[] encryptedData, MemoryProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			ProtectedMemory.Check(encryptedData.Length, scope);
			try
			{
				uint cbData = (uint)encryptedData.Length;
				ProtectedMemory.MemoryProtectionImplementation memoryProtectionImplementation = ProtectedMemory.impl;
				if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32RtlEncryptMemory)
				{
					if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect)
					{
						throw new PlatformNotSupportedException();
					}
					if (!ProtectedMemory.CryptUnprotectMemory(encryptedData, cbData, (uint)scope))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					int num = ProtectedMemory.RtlDecryptMemory(encryptedData, cbData, (uint)scope);
					if (num < 0)
					{
						string text = Locale.GetText("Error. NTSTATUS = {0}.", new object[]
						{
							num
						});
						throw new CryptographicException(text);
					}
				}
			}
			catch
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004580 File Offset: 0x00002780
		private static void Detect()
		{
			OperatingSystem osversion = Environment.OSVersion;
			PlatformID platform = osversion.Platform;
			if (platform != PlatformID.Win32NT)
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
			}
			else
			{
				Version version = osversion.Version;
				if (version.Major < 5)
				{
					ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				}
				else if (version.Major == 5)
				{
					if (version.Minor < 2)
					{
						ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Win32RtlEncryptMemory;
					}
					else
					{
						ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect;
					}
				}
				else
				{
					ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect;
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004610 File Offset: 0x00002810
		private static void Check(int size, MemoryProtectionScope scope)
		{
			if (size % 16 != 0)
			{
				string text = Locale.GetText("Not a multiple of {0} bytes.", new object[]
				{
					16
				});
				throw new CryptographicException(text);
			}
			if (scope < MemoryProtectionScope.SameProcess || scope > MemoryProtectionScope.SameLogon)
			{
				string text2 = Locale.GetText("Invalid enum value for '{0}'.", new object[]
				{
					"MemoryProtectionScope"
				});
				throw new ArgumentException(text2, "scope");
			}
			ProtectedMemory.MemoryProtectionImplementation memoryProtectionImplementation = ProtectedMemory.impl;
			if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Unknown)
			{
				if (memoryProtectionImplementation == ProtectedMemory.MemoryProtectionImplementation.Unsupported)
				{
					throw new PlatformNotSupportedException();
				}
			}
			else
			{
				ProtectedMemory.Detect();
			}
		}

		// Token: 0x0600004C RID: 76
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "SystemFunction040", SetLastError = true)]
		private static extern int RtlEncryptMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x0600004D RID: 77
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "SystemFunction041", SetLastError = true)]
		private static extern int RtlDecryptMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x0600004E RID: 78
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x0600004F RID: 79
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x04000047 RID: 71
		private const int BlockSize = 16;

		// Token: 0x04000048 RID: 72
		private static ProtectedMemory.MemoryProtectionImplementation impl;

		// Token: 0x02000014 RID: 20
		private enum MemoryProtectionImplementation
		{
			// Token: 0x0400004A RID: 74
			Unknown,
			// Token: 0x0400004B RID: 75
			Win32RtlEncryptMemory,
			// Token: 0x0400004C RID: 76
			Win32CryptoProtect,
			// Token: 0x0400004D RID: 77
			Unsupported = -2147483648
		}
	}
}
