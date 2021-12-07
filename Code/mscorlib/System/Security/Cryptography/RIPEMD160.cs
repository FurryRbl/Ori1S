using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of the MD160 hash algorithm inherit.</summary>
	// Token: 0x020005CD RID: 1485
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> class.</summary>
		// Token: 0x060038C4 RID: 14532 RVA: 0x000C0EB4 File Offset: 0x000BF0B4
		protected RIPEMD160()
		{
			this.HashSizeValue = 160;
		}

		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm.</summary>
		/// <returns>A new instance of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but it is not FIPS-compatible.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060038C5 RID: 14533 RVA: 0x000C0EC8 File Offset: 0x000BF0C8
		public new static RIPEMD160 Create()
		{
			return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
		}

		/// <summary>Creates an instance of the specified implementation of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm.</summary>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.RIPEMD160" />.</returns>
		/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.RIPEMD160" /> to use. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="hashName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but it is not FIPS-compatible.</exception>
		// Token: 0x060038C6 RID: 14534 RVA: 0x000C0ED4 File Offset: 0x000BF0D4
		public new static RIPEMD160 Create(string hashName)
		{
			return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
		}
	}
}
