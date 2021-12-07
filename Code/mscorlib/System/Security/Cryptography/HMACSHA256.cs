using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.SHA256" /> hash function.</summary>
	// Token: 0x020005B5 RID: 1461
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA256" /> class with a randomly generated key.</summary>
		// Token: 0x0600382D RID: 14381 RVA: 0x000B675C File Offset: 0x000B495C
		public HMACSHA256() : this(KeyBuilder.Key(8))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA256" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA256" /> encryption. The key can be any length, but if it is more than 64 bytes long it will be hashed (using SHA-1) to derive a 64-byte key. Therefore, the recommended size of the secret key is 64 bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		// Token: 0x0600382E RID: 14382 RVA: 0x000B676C File Offset: 0x000B496C
		public HMACSHA256(byte[] key)
		{
			base.HashName = "SHA256";
			this.HashSizeValue = 256;
			this.Key = key;
		}
	}
}
