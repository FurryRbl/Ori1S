﻿using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA512" /> hash for the input data.  </summary>
	// Token: 0x020005E1 RID: 1505
	[ComVisible(true)]
	public abstract class SHA512 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.SHA512" />.</summary>
		// Token: 0x06003971 RID: 14705 RVA: 0x000C5074 File Offset: 0x000C3274
		protected SHA512()
		{
			this.HashSizeValue = 512;
		}

		/// <summary>Creates an instance of the default implementation of <see cref="T:System.Security.Cryptography.SHA512" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA512" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003972 RID: 14706 RVA: 0x000C5088 File Offset: 0x000C3288
		public new static SHA512 Create()
		{
			return SHA512.Create("System.Security.Cryptography.SHA512");
		}

		/// <summary>Creates an instance of a specified implementation of <see cref="T:System.Security.Cryptography.SHA512" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA512" /> using the specified implementation.</returns>
		/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.SHA512" /> to be used. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="hashName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06003973 RID: 14707 RVA: 0x000C5094 File Offset: 0x000C3294
		public new static SHA512 Create(string hashName)
		{
			return (SHA512)CryptoConfig.CreateFromName(hashName);
		}
	}
}
