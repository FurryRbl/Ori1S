using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.SHA512" /> hash function.</summary>
	// Token: 0x020005B7 RID: 1463
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> class with a randomly generated key.</summary>
		// Token: 0x06003834 RID: 14388 RVA: 0x000B6844 File Offset: 0x000B4A44
		public HMACSHA512() : this(KeyBuilder.Key(8))
		{
			this.ProduceLegacyHmacValues = HMACSHA512.legacy_mode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA512" /> encryption. The key can be any length. However, if it is more than 64 bytes long it will be hashed (using SHA-1) to derive a 64-byte key. Therefore, the recommended size of the secret key is 64 bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		// Token: 0x06003835 RID: 14389 RVA: 0x000B6860 File Offset: 0x000B4A60
		public HMACSHA512(byte[] key)
		{
			this.ProduceLegacyHmacValues = HMACSHA512.legacy_mode;
			base.HashName = "SHA512";
			this.HashSizeValue = 512;
			this.Key = key;
		}

		/// <summary>Provides a workaround for the .NET Framework version 2.0 implementation of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> algorithm, which is inconsistent with the .NET Framework version 2.0 Service Pack 1 implementation.</summary>
		/// <returns>true to enable .NET Framework version 2.0 Service Pack 1 applications to interact with .NET Framework 2.0 applications; otherwise, false.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000B68B8 File Offset: 0x000B4AB8
		// (set) Token: 0x06003838 RID: 14392 RVA: 0x000B68C0 File Offset: 0x000B4AC0
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.legacy;
			}
			set
			{
				this.legacy = value;
				base.BlockSizeValue = ((!this.legacy) ? 128 : 64);
			}
		}

		// Token: 0x04001863 RID: 6243
		private static bool legacy_mode = Environment.GetEnvironmentVariable("legacyHMACMode") == "1";

		// Token: 0x04001864 RID: 6244
		private bool legacy;
	}
}
