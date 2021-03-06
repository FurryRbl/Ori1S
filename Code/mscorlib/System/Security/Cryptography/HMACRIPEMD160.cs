using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash function.</summary>
	// Token: 0x020005B3 RID: 1459
	[ComVisible(true)]
	public class HMACRIPEMD160 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> class with a randomly generated 64-byte key.</summary>
		// Token: 0x06003828 RID: 14376 RVA: 0x000B66A0 File Offset: 0x000B48A0
		public HMACRIPEMD160() : this(KeyBuilder.Key(8))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> encryption. The key can be any length, but if it is more than 64 bytes long it will be hashed (using SHA-1) to derive a 64-byte key. Therefore, the recommended size of the secret key is 64 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		// Token: 0x06003829 RID: 14377 RVA: 0x000B66B0 File Offset: 0x000B48B0
		public HMACRIPEMD160(byte[] key)
		{
			base.HashName = "RIPEMD160";
			this.HashSizeValue = 160;
			this.Key = key;
		}
	}
}
