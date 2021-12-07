using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Provides methods for protecting and unprotecting data. This class cannot be inherited.</summary>
	// Token: 0x02000011 RID: 17
	public sealed class ProtectedData
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000410C File Offset: 0x0000230C
		private ProtectedData()
		{
		}

		/// <summary>Protects the data in a specified byte array and returns a byte array containing the encrypted data.</summary>
		/// <returns>A byte array representing the encrypted data.</returns>
		/// <param name="userData">A byte array containing data to protect. </param>
		/// <param name="optionalEntropy">An optional additional byte array used to increase the complexity of the encryption, or null for no additional complexity. </param>
		/// <param name="scope">One of the <see cref="T:System.Security.Cryptography.DataProtectionScope" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic protection failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support this method. </exception>
		/// <exception cref="T:System.OutOfMemoryException">The system ran out of memory while encrypting the data.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ProtectData" />
		/// </PermissionSet>
		// Token: 0x06000043 RID: 67 RVA: 0x00004114 File Offset: 0x00002314
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			ProtectedData.Check(scope);
			ProtectedData.DataProtectionImplementation dataProtectionImplementation = ProtectedData.impl;
			if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.Win32CryptoProtect)
			{
				if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.ManagedProtection)
				{
					goto IL_8D;
				}
				try
				{
					return ManagedProtection.Protect(userData, optionalEntropy, scope);
				}
				catch (Exception inner)
				{
					string text = Locale.GetText("Data protection failed.");
					throw new CryptographicException(text, inner);
				}
			}
			try
			{
				return NativeDapiProtection.Protect(userData, optionalEntropy, scope);
			}
			catch (Exception inner2)
			{
				string text2 = Locale.GetText("Data protection failed.");
				throw new CryptographicException(text2, inner2);
			}
			IL_8D:
			throw new PlatformNotSupportedException();
		}

		/// <summary>Unprotects the <paramref name="encryptedData" /> parameter and returns a byte array.</summary>
		/// <returns>A byte array representing the unprotected data.</returns>
		/// <param name="encryptedData">A byte array containing data encrypted using the <see cref="M:System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)" /> method. </param>
		/// <param name="optionalEntropy">An additional byte array used to encrypt the data. </param>
		/// <param name="scope">One of the <see cref="T:System.Security.Cryptography.DataProtectionScope" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="encryptedData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic protection failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support this method. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnprotectData" />
		/// </PermissionSet>
		// Token: 0x06000044 RID: 68 RVA: 0x000041EC File Offset: 0x000023EC
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			ProtectedData.Check(scope);
			ProtectedData.DataProtectionImplementation dataProtectionImplementation = ProtectedData.impl;
			if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.Win32CryptoProtect)
			{
				if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.ManagedProtection)
				{
					goto IL_8D;
				}
				try
				{
					return ManagedProtection.Unprotect(encryptedData, optionalEntropy, scope);
				}
				catch (Exception inner)
				{
					string text = Locale.GetText("Data unprotection failed.");
					throw new CryptographicException(text, inner);
				}
			}
			try
			{
				return NativeDapiProtection.Unprotect(encryptedData, optionalEntropy, scope);
			}
			catch (Exception inner2)
			{
				string text2 = Locale.GetText("Data unprotection failed.");
				throw new CryptographicException(text2, inner2);
			}
			IL_8D:
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000042C4 File Offset: 0x000024C4
		private static void Detect()
		{
			OperatingSystem osversion = Environment.OSVersion;
			switch (osversion.Platform)
			{
			case PlatformID.Win32NT:
			{
				Version version = osversion.Version;
				if (version.Major < 5)
				{
					ProtectedData.impl = ProtectedData.DataProtectionImplementation.Unsupported;
				}
				else
				{
					ProtectedData.impl = ProtectedData.DataProtectionImplementation.Win32CryptoProtect;
				}
				return;
			}
			case PlatformID.Unix:
				ProtectedData.impl = ProtectedData.DataProtectionImplementation.ManagedProtection;
				return;
			}
			ProtectedData.impl = ProtectedData.DataProtectionImplementation.Unsupported;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004340 File Offset: 0x00002540
		private static void Check(DataProtectionScope scope)
		{
			if (scope < DataProtectionScope.CurrentUser || scope > DataProtectionScope.LocalMachine)
			{
				string text = Locale.GetText("Invalid enum value '{0}' for '{1}'.", new object[]
				{
					scope,
					"DataProtectionScope"
				});
				throw new ArgumentException(text, "scope");
			}
			ProtectedData.DataProtectionImplementation dataProtectionImplementation = ProtectedData.impl;
			if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.Unknown)
			{
				if (dataProtectionImplementation == ProtectedData.DataProtectionImplementation.Unsupported)
				{
					throw new PlatformNotSupportedException();
				}
			}
			else
			{
				ProtectedData.Detect();
			}
		}

		// Token: 0x04000041 RID: 65
		private static ProtectedData.DataProtectionImplementation impl;

		// Token: 0x02000012 RID: 18
		private enum DataProtectionImplementation
		{
			// Token: 0x04000043 RID: 67
			Unknown,
			// Token: 0x04000044 RID: 68
			Win32CryptoProtect,
			// Token: 0x04000045 RID: 69
			ManagedProtection,
			// Token: 0x04000046 RID: 70
			Unsupported = -2147483648
		}
	}
}
