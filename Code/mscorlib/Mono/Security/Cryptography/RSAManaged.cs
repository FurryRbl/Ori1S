using System;
using System.Security.Cryptography;
using System.Text;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000C0 RID: 192
	internal class RSAManaged : RSA
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0002E05C File Offset: 0x0002C25C
		public RSAManaged() : this(1024)
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002E06C File Offset: 0x0002C26C
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000ABE RID: 2750 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		// (remove) Token: 0x06000ABF RID: 2751 RVA: 0x0002E0D0 File Offset: 0x0002C2D0
		public event RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002E0EC File Offset: 0x0002C2EC
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002E128 File Offset: 0x0002C328
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

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002E2CC File Offset: 0x0002C4CC
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002E308 File Offset: 0x0002C508
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002E310 File Offset: 0x0002C510
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002E34C File Offset: 0x0002C54C
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002E354 File Offset: 0x0002C554
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

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002E510 File Offset: 0x0002C710
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

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002E57C File Offset: 0x0002C77C
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

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002E72C File Offset: 0x0002C92C
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

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
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

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002EAF4 File Offset: 0x0002CCF4
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0002EDC4 File Offset: 0x0002CFC4
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0002EDD0 File Offset: 0x0002CFD0
		public bool IsCrtPossible
		{
			get
			{
				return !this.keypairGenerated || this.isCRTpossible;
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002EDE8 File Offset: 0x0002CFE8
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

		// Token: 0x04000290 RID: 656
		private const int defaultKeySize = 1024;

		// Token: 0x04000291 RID: 657
		private bool isCRTpossible;

		// Token: 0x04000292 RID: 658
		private bool keyBlinding = true;

		// Token: 0x04000293 RID: 659
		private bool keypairGenerated;

		// Token: 0x04000294 RID: 660
		private bool m_disposed;

		// Token: 0x04000295 RID: 661
		private BigInteger d;

		// Token: 0x04000296 RID: 662
		private BigInteger p;

		// Token: 0x04000297 RID: 663
		private BigInteger q;

		// Token: 0x04000298 RID: 664
		private BigInteger dp;

		// Token: 0x04000299 RID: 665
		private BigInteger dq;

		// Token: 0x0400029A RID: 666
		private BigInteger qInv;

		// Token: 0x0400029B RID: 667
		private BigInteger n;

		// Token: 0x0400029C RID: 668
		private BigInteger e;

		// Token: 0x020006D8 RID: 1752
		// (Invoke) Token: 0x06004354 RID: 17236
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
