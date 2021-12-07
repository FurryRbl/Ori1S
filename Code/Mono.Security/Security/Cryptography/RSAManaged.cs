using System;
using System.Security.Cryptography;
using System.Text;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000036 RID: 54
	public class RSAManaged : RSA
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000E42C File Offset: 0x0000C62C
		public RSAManaged() : this(1024)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000E43C File Offset: 0x0000C63C
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600023F RID: 575 RVA: 0x0000E484 File Offset: 0x0000C684
		// (remove) Token: 0x06000240 RID: 576 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		public event RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x06000241 RID: 577 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		private void GenerateKeyPair()
		{
			int num = this.KeySize + 1 >> 1;
			int bits = this.KeySize - num;
			this.e = 17U;
			do
			{
				this.p = BigInteger.GeneratePseudoPrime(num);
			}
			while (this.p % 17U == 1U);
			for (;;)
			{
				do
				{
					this.q = BigInteger.GeneratePseudoPrime(bits);
				}
				while (this.q % 17U == 1U || !(this.p != this.q));
				this.n = this.p * this.q;
				if (this.n.BitCount() == this.KeySize)
				{
					break;
				}
				if (this.p < this.q)
				{
					this.p = this.q;
				}
			}
			BigInteger bigInteger = this.p - 1;
			BigInteger bi = this.q - 1;
			BigInteger modulus = bigInteger * bi;
			this.d = this.e.ModInverse(modulus);
			this.dp = this.d % bigInteger;
			this.dq = this.d % bi;
			this.qInv = this.q.ModInverse(this.p);
			this.keypairGenerated = true;
			this.isCRTpossible = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000E69C File Offset: 0x0000C89C
		public override int KeySize
		{
			get
			{
				if (this.keypairGenerated)
				{
					int num = this.n.BitCount();
					if ((num & 7) != 0)
					{
						num += 8 - (num & 7);
					}
					return num;
				}
				return base.KeySize;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000E71C File Offset: 0x0000C91C
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E724 File Offset: 0x0000C924
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("private key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = null;
			if (this.keyBlinding)
			{
				bigInteger2 = BigInteger.GenerateRandom(this.n.BitCount());
				bigInteger = bigInteger2.ModPow(this.e, this.n) * bigInteger % this.n;
			}
			BigInteger bigInteger5;
			if (this.isCRTpossible)
			{
				BigInteger bigInteger3 = bigInteger.ModPow(this.dp, this.p);
				BigInteger bigInteger4 = bigInteger.ModPow(this.dq, this.q);
				if (bigInteger4 > bigInteger3)
				{
					BigInteger bi = this.p - (bigInteger4 - bigInteger3) * this.qInv % this.p;
					bigInteger5 = bigInteger4 + this.q * bi;
				}
				else
				{
					BigInteger bi = (bigInteger3 - bigInteger4) * this.qInv % this.p;
					bigInteger5 = bigInteger4 + this.q * bi;
				}
			}
			else
			{
				if (this.PublicOnly)
				{
					throw new CryptographicException(Locale.GetText("Missing private key to decrypt value."));
				}
				bigInteger5 = bigInteger.ModPow(this.d, this.n);
			}
			if (this.keyBlinding)
			{
				bigInteger5 = bigInteger5 * bigInteger2.ModInverse(this.n) % this.n;
				bigInteger2.Clear();
			}
			byte[] paddedValue = this.GetPaddedValue(bigInteger5, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger5.Clear();
			return paddedValue;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		public override byte[] EncryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("public key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = bigInteger.ModPow(this.e, this.n);
			byte[] paddedValue = this.GetPaddedValue(bigInteger2, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger2.Clear();
			return paddedValue;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000E94C File Offset: 0x0000CB4C
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			RSAParameters result = default(RSAParameters);
			result.Exponent = this.e.GetBytes();
			result.Modulus = this.n.GetBytes();
			if (includePrivateParameters)
			{
				if (this.d == null)
				{
					throw new CryptographicException("Missing private key");
				}
				result.D = this.d.GetBytes();
				if (result.D.Length != result.Modulus.Length)
				{
					byte[] array = new byte[result.Modulus.Length];
					Buffer.BlockCopy(result.D, 0, array, array.Length - result.D.Length, result.D.Length);
					result.D = array;
				}
				if (this.p != null && this.q != null && this.dp != null && this.dq != null && this.qInv != null)
				{
					int length = this.KeySize >> 4;
					result.P = this.GetPaddedValue(this.p, length);
					result.Q = this.GetPaddedValue(this.q, length);
					result.DP = this.GetPaddedValue(this.dp, length);
					result.DQ = this.GetPaddedValue(this.dq, length);
					result.InverseQ = this.GetPaddedValue(this.qInv, length);
				}
			}
			return result;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.Exponent == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Exponent"));
			}
			if (parameters.Modulus == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Modulus"));
			}
			this.e = new BigInteger(parameters.Exponent);
			this.n = new BigInteger(parameters.Modulus);
			if (parameters.D != null)
			{
				this.d = new BigInteger(parameters.D);
			}
			if (parameters.DP != null)
			{
				this.dp = new BigInteger(parameters.DP);
			}
			if (parameters.DQ != null)
			{
				this.dq = new BigInteger(parameters.DQ);
			}
			if (parameters.InverseQ != null)
			{
				this.qInv = new BigInteger(parameters.InverseQ);
			}
			if (parameters.P != null)
			{
				this.p = new BigInteger(parameters.P);
			}
			if (parameters.Q != null)
			{
				this.q = new BigInteger(parameters.Q);
			}
			this.keypairGenerated = true;
			bool flag = this.p != null && this.q != null && this.dp != null;
			this.isCRTpossible = (flag && this.dq != null && this.qInv != null);
			if (!flag)
			{
				return;
			}
			bool flag2 = this.n == this.p * this.q;
			if (flag2)
			{
				BigInteger bigInteger = this.p - 1;
				BigInteger bi = this.q - 1;
				BigInteger modulus = bigInteger * bi;
				BigInteger bigInteger2 = this.e.ModInverse(modulus);
				flag2 = (this.d == bigInteger2);
				if (!flag2 && this.isCRTpossible)
				{
					flag2 = (this.dp == bigInteger2 % bigInteger && this.dq == bigInteger2 % bi && this.qInv == this.q.ModInverse(this.p));
				}
			}
			if (!flag2)
			{
				throw new CryptographicException(Locale.GetText("Private/public key mismatch"));
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000ED84 File Offset: 0x0000CF84
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.d != null)
				{
					this.d.Clear();
					this.d = null;
				}
				if (this.p != null)
				{
					this.p.Clear();
					this.p = null;
				}
				if (this.q != null)
				{
					this.q.Clear();
					this.q = null;
				}
				if (this.dp != null)
				{
					this.dp.Clear();
					this.dp = null;
				}
				if (this.dq != null)
				{
					this.dq.Clear();
					this.dq = null;
				}
				if (this.qInv != null)
				{
					this.qInv.Clear();
					this.qInv = null;
				}
				if (disposing)
				{
					if (this.e != null)
					{
						this.e.Clear();
						this.e = null;
					}
					if (this.n != null)
					{
						this.n.Clear();
						this.n = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<RSAKeyValue>");
				stringBuilder.Append("<Modulus>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Modulus));
				stringBuilder.Append("</Modulus>");
				stringBuilder.Append("<Exponent>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Exponent));
				stringBuilder.Append("</Exponent>");
				if (includePrivateParameters)
				{
					if (rsaparameters.P != null)
					{
						stringBuilder.Append("<P>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.P));
						stringBuilder.Append("</P>");
					}
					if (rsaparameters.Q != null)
					{
						stringBuilder.Append("<Q>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.Q));
						stringBuilder.Append("</Q>");
					}
					if (rsaparameters.DP != null)
					{
						stringBuilder.Append("<DP>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DP));
						stringBuilder.Append("</DP>");
					}
					if (rsaparameters.DQ != null)
					{
						stringBuilder.Append("<DQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DQ));
						stringBuilder.Append("</DQ>");
					}
					if (rsaparameters.InverseQ != null)
					{
						stringBuilder.Append("<InverseQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.InverseQ));
						stringBuilder.Append("</InverseQ>");
					}
					stringBuilder.Append("<D>");
					stringBuilder.Append(Convert.ToBase64String(rsaparameters.D));
					stringBuilder.Append("</D>");
				}
				stringBuilder.Append("</RSAKeyValue>");
			}
			catch
			{
				if (rsaparameters.P != null)
				{
					Array.Clear(rsaparameters.P, 0, rsaparameters.P.Length);
				}
				if (rsaparameters.Q != null)
				{
					Array.Clear(rsaparameters.Q, 0, rsaparameters.Q.Length);
				}
				if (rsaparameters.DP != null)
				{
					Array.Clear(rsaparameters.DP, 0, rsaparameters.DP.Length);
				}
				if (rsaparameters.DQ != null)
				{
					Array.Clear(rsaparameters.DQ, 0, rsaparameters.DQ.Length);
				}
				if (rsaparameters.InverseQ != null)
				{
					Array.Clear(rsaparameters.InverseQ, 0, rsaparameters.InverseQ.Length);
				}
				if (rsaparameters.D != null)
				{
					Array.Clear(rsaparameters.D, 0, rsaparameters.D.Length);
				}
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000F18C File Offset: 0x0000D38C
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000F194 File Offset: 0x0000D394
		public bool UseKeyBlinding
		{
			get
			{
				return this.keyBlinding;
			}
			set
			{
				this.keyBlinding = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public bool IsCrtPossible
		{
			get
			{
				return !this.keypairGenerated || this.isCRTpossible;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		private byte[] GetPaddedValue(BigInteger value, int length)
		{
			byte[] bytes = value.GetBytes();
			if (bytes.Length >= length)
			{
				return bytes;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(bytes, 0, array, length - bytes.Length, bytes.Length);
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x040000F2 RID: 242
		private const int defaultKeySize = 1024;

		// Token: 0x040000F3 RID: 243
		private bool isCRTpossible;

		// Token: 0x040000F4 RID: 244
		private bool keyBlinding = true;

		// Token: 0x040000F5 RID: 245
		private bool keypairGenerated;

		// Token: 0x040000F6 RID: 246
		private bool m_disposed;

		// Token: 0x040000F7 RID: 247
		private BigInteger d;

		// Token: 0x040000F8 RID: 248
		private BigInteger p;

		// Token: 0x040000F9 RID: 249
		private BigInteger q;

		// Token: 0x040000FA RID: 250
		private BigInteger dp;

		// Token: 0x040000FB RID: 251
		private BigInteger dq;

		// Token: 0x040000FC RID: 252
		private BigInteger qInv;

		// Token: 0x040000FD RID: 253
		private BigInteger n;

		// Token: 0x040000FE RID: 254
		private BigInteger e;

		// Token: 0x020000C8 RID: 200
		// (Invoke) Token: 0x06000705 RID: 1797
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
