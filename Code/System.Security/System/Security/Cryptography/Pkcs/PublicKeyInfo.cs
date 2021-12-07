using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /> class represents information associated with a public key.</summary>
	// Token: 0x02000025 RID: 37
	public sealed class PublicKeyInfo
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000058A0 File Offset: 0x00003AA0
		internal PublicKeyInfo(AlgorithmIdentifier algorithm, byte[] key)
		{
			this._algorithm = algorithm;
			this._key = key;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.Algorithm" /> property retrieves the algorithm identifier associated with the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" />  object that represents the algorithm.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000058B8 File Offset: 0x00003AB8
		public AlgorithmIdentifier Algorithm
		{
			get
			{
				return this._algorithm;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.KeyValue" /> property retrieves the value of the encoded public component of the public key pair.</summary>
		/// <returns>An array of byte values  that represents the encoded public component of the public key pair.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000058C0 File Offset: 0x00003AC0
		public byte[] KeyValue
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x0400007F RID: 127
		private AlgorithmIdentifier _algorithm;

		// Token: 0x04000080 RID: 128
		private byte[] _key;
	}
}
