using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class defines the algorithm used for a cryptographic operation.</summary>
	// Token: 0x02000015 RID: 21
	public sealed class AlgorithmIdentifier
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class by using a set of default parameters. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000050 RID: 80 RVA: 0x000046A8 File Offset: 0x000028A8
		public AlgorithmIdentifier()
		{
			this._oid = new Oid("1.2.840.113549.3.7", "3des");
			this._params = new byte[0];
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000051 RID: 81 RVA: 0x000046D4 File Offset: 0x000028D4
		public AlgorithmIdentifier(Oid algorithm)
		{
			this._oid = algorithm;
			this._params = new byte[0];
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid,System.Int32)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier and key length.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <param name="keyLength">The length, in bits, of the key.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000052 RID: 82 RVA: 0x000046F0 File Offset: 0x000028F0
		public AlgorithmIdentifier(Oid algorithm, int keyLength)
		{
			this._oid = algorithm;
			this._length = keyLength;
			this._params = new byte[0];
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.KeyLength" /> property sets or retrieves the key length, in bits. This property is not used for algorithms that use a fixed key length.</summary>
		/// <returns>An int value that represents the key length, in bits.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004720 File Offset: 0x00002920
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00004728 File Offset: 0x00002928
		public int KeyLength
		{
			get
			{
				return this._length;
			}
			set
			{
				this._length = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Oid" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" />  object that specifies the object identifier for the algorithm.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the algorithm.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00004734 File Offset: 0x00002934
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000473C File Offset: 0x0000293C
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
			set
			{
				this._oid = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Parameters" /> property sets or retrieves any parameters required by the algorithm.</summary>
		/// <returns>An array of byte values that specifies any parameters required by the algorithm.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004748 File Offset: 0x00002948
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00004750 File Offset: 0x00002950
		public byte[] Parameters
		{
			get
			{
				return this._params;
			}
			set
			{
				this._params = value;
			}
		}

		// Token: 0x0400004E RID: 78
		private Oid _oid;

		// Token: 0x0400004F RID: 79
		private int _length;

		// Token: 0x04000050 RID: 80
		private byte[] _params;
	}
}
