using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of cryptographic random number generators derive.</summary>
	// Token: 0x020005C4 RID: 1476
	[ComVisible(true)]
	public abstract class RandomNumberGenerator
	{
		/// <summary>Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003883 RID: 14467 RVA: 0x000B8280 File Offset: 0x000B6480
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		/// <summary>Creates an instance of the specified implementation of a cryptographic random number generator.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <param name="rngName">The name of the random number generator implementation to use. </param>
		// Token: 0x06003884 RID: 14468 RVA: 0x000B828C File Offset: 0x000B648C
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random bytes. </param>
		// Token: 0x06003885 RID: 14469
		public abstract void GetBytes(byte[] data);

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of nonzero values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random nonzero bytes. </param>
		// Token: 0x06003886 RID: 14470
		public abstract void GetNonZeroBytes(byte[] data);
	}
}
