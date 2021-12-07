using System;
using System.Security.Cryptography;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000B6 RID: 182
	internal class DSAManaged : DSA
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0002A834 File Offset: 0x00028A34
		public DSAManaged() : this(1024)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002A844 File Offset: 0x00028A44
		public DSAManaged(int dwKeySize)
		{
			this.KeySizeValue = dwKeySize;
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000A30 RID: 2608 RVA: 0x0002A884 File Offset: 0x00028A84
		// (remove) Token: 0x06000A31 RID: 2609 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		public event DSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002A8BC File Offset: 0x00028ABC
		~DSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002A8F8 File Offset: 0x00028AF8
		private void Generate()
		{
			this.GenerateParams(base.KeySize);
			this.GenerateKeyPair();
			this.keypairGenerated = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002A938 File Offset: 0x00028B38
		private void GenerateKeyPair()
		{
			this.x = BigInteger.GenerateRandom(160);
			while (this.x == 0U || this.x >= this.q)
			{
				this.x.Randomize();
			}
			this.y = this.g.ModPow(this.x, this.p);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002A9AC File Offset: 0x00028BAC
		private void add(byte[] a, byte[] b, int value)
		{
			uint num = (uint)((int)(b[b.Length - 1] & byte.MaxValue) + value);
			a[b.Length - 1] = (byte)num;
			num >>= 8;
			for (int i = b.Length - 2; i >= 0; i--)
			{
				num += (uint)(b[i] & byte.MaxValue);
				a[i] = (byte)num;
				num >>= 8;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002AA04 File Offset: 0x00028C04
		private void GenerateParams(int keyLength)
		{
			byte[] array = new byte[20];
			byte[] array2 = new byte[20];
			byte[] array3 = new byte[20];
			byte[] array4 = new byte[20];
			SHA1 sha = SHA1.Create();
			int num = (keyLength - 1) / 160;
			byte[] array5 = new byte[keyLength / 8];
			bool flag = false;
			while (!flag)
			{
				do
				{
					this.Random.GetBytes(array);
					array2 = sha.ComputeHash(array);
					Array.Copy(array, 0, array3, 0, array.Length);
					this.add(array3, array, 1);
					array3 = sha.ComputeHash(array3);
					for (int num2 = 0; num2 != array4.Length; num2++)
					{
						array4[num2] = (array2[num2] ^ array3[num2]);
					}
					byte[] array6 = array4;
					int num3 = 0;
					array6[num3] |= 128;
					byte[] array7 = array4;
					int num4 = 19;
					array7[num4] |= 1;
					this.q = new BigInteger(array4);
				}
				while (!this.q.IsProbablePrime());
				this.counter = 0;
				int num5 = 2;
				while (this.counter < 4096)
				{
					for (int i = 0; i < num; i++)
					{
						this.add(array2, array, num5 + i);
						array2 = sha.ComputeHash(array2);
						Array.Copy(array2, 0, array5, array5.Length - (i + 1) * array2.Length, array2.Length);
					}
					this.add(array2, array, num5 + num);
					array2 = sha.ComputeHash(array2);
					Array.Copy(array2, array2.Length - (array5.Length - num * array2.Length), array5, 0, array5.Length - num * array2.Length);
					byte[] array8 = array5;
					int num6 = 0;
					array8[num6] |= 128;
					BigInteger bi = new BigInteger(array5);
					BigInteger bi2 = bi % (this.q * 2);
					this.p = bi - (bi2 - 1);
					if (this.p.TestBit((uint)(keyLength - 1)) && this.p.IsProbablePrime())
					{
						flag = true;
						break;
					}
					this.counter++;
					num5 += num + 1;
				}
			}
			BigInteger exp = (this.p - 1) / this.q;
			for (;;)
			{
				BigInteger bigInteger = BigInteger.GenerateRandom(keyLength);
				if (!(bigInteger <= 1) && !(bigInteger >= this.p - 1))
				{
					this.g = bigInteger.ModPow(exp, this.p);
					if (!(this.g <= 1))
					{
						break;
					}
				}
			}
			this.seed = new BigInteger(array);
			this.j = (this.p - 1) / this.q;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002ACE4 File Offset: 0x00028EE4
		private RandomNumberGenerator Random
		{
			get
			{
				if (this.rng == null)
				{
					this.rng = RandomNumberGenerator.Create();
				}
				return this.rng;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002AD04 File Offset: 0x00028F04
		public override int KeySize
		{
			get
			{
				if (this.keypairGenerated)
				{
					return this.p.BitCount();
				}
				return base.KeySize;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002AD24 File Offset: 0x00028F24
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002AD28 File Offset: 0x00028F28
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && this.x == null;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002AD44 File Offset: 0x00028F44
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002AD4C File Offset: 0x00028F4C
		private byte[] NormalizeArray(byte[] array)
		{
			int num = array.Length % 4;
			if (num > 0)
			{
				byte[] array2 = new byte[array.Length + 4 - num];
				Array.Copy(array, 0, array2, 4 - num, array.Length);
				return array2;
			}
			return array;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002AD84 File Offset: 0x00028F84
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.Generate();
			}
			if (includePrivateParameters && this.x == null)
			{
				throw new CryptographicException("no private key to export");
			}
			DSAParameters result = default(DSAParameters);
			result.P = this.NormalizeArray(this.p.GetBytes());
			result.Q = this.NormalizeArray(this.q.GetBytes());
			result.G = this.NormalizeArray(this.g.GetBytes());
			result.Y = this.NormalizeArray(this.y.GetBytes());
			if (!this.j_missing)
			{
				result.J = this.NormalizeArray(this.j.GetBytes());
			}
			if (this.seed != 0U)
			{
				result.Seed = this.NormalizeArray(this.seed.GetBytes());
				result.Counter = this.counter;
			}
			if (includePrivateParameters)
			{
				byte[] bytes = this.x.GetBytes();
				if (bytes.Length == 20)
				{
					result.X = this.NormalizeArray(bytes);
				}
			}
			return result;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002AECC File Offset: 0x000290CC
		public override void ImportParameters(DSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.P == null || parameters.Q == null || parameters.G == null)
			{
				throw new CryptographicException(Locale.GetText("Missing mandatory DSA parameters (P, Q or G)."));
			}
			if (parameters.X == null && parameters.Y == null)
			{
				throw new CryptographicException(Locale.GetText("Missing both public (Y) and private (X) keys."));
			}
			this.p = new BigInteger(parameters.P);
			this.q = new BigInteger(parameters.Q);
			this.g = new BigInteger(parameters.G);
			if (parameters.X != null)
			{
				this.x = new BigInteger(parameters.X);
			}
			else
			{
				this.x = null;
			}
			if (parameters.Y != null)
			{
				this.y = new BigInteger(parameters.Y);
			}
			else
			{
				this.y = this.g.ModPow(this.x, this.p);
			}
			if (parameters.J != null)
			{
				this.j = new BigInteger(parameters.J);
			}
			else
			{
				this.j = (this.p - 1) / this.q;
				this.j_missing = true;
			}
			if (parameters.Seed != null)
			{
				this.seed = new BigInteger(parameters.Seed);
				this.counter = parameters.Counter;
			}
			else
			{
				this.seed = 0;
			}
			this.keypairGenerated = true;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002B080 File Offset: 0x00029280
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbHash.Length != 20)
			{
				throw new CryptographicException("invalid hash length");
			}
			if (!this.keypairGenerated)
			{
				this.Generate();
			}
			if (this.x == null)
			{
				throw new CryptographicException("no private key available for signature");
			}
			BigInteger bi = new BigInteger(rgbHash);
			BigInteger bigInteger = BigInteger.GenerateRandom(160);
			while (bigInteger >= this.q)
			{
				bigInteger.Randomize();
			}
			BigInteger bigInteger2 = this.g.ModPow(bigInteger, this.p) % this.q;
			BigInteger bigInteger3 = bigInteger.ModInverse(this.q) * (bi + this.x * bigInteger2) % this.q;
			byte[] array = new byte[40];
			byte[] bytes = bigInteger2.GetBytes();
			byte[] bytes2 = bigInteger3.GetBytes();
			int destinationIndex = 20 - bytes.Length;
			Array.Copy(bytes, 0, array, destinationIndex, bytes.Length);
			destinationIndex = 40 - bytes2.Length;
			Array.Copy(bytes2, 0, array, destinationIndex, bytes2.Length);
			return array;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002B1C4 File Offset: 0x000293C4
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			if (rgbHash.Length != 20)
			{
				throw new CryptographicException("invalid hash length");
			}
			if (rgbSignature.Length != 40)
			{
				throw new CryptographicException("invalid signature length");
			}
			if (!this.keypairGenerated)
			{
				return false;
			}
			bool result;
			try
			{
				BigInteger bi = new BigInteger(rgbHash);
				byte[] array = new byte[20];
				Array.Copy(rgbSignature, 0, array, 0, 20);
				BigInteger bigInteger = new BigInteger(array);
				Array.Copy(rgbSignature, 20, array, 0, 20);
				BigInteger bigInteger2 = new BigInteger(array);
				if (bigInteger < 0 || this.q <= bigInteger)
				{
					result = false;
				}
				else if (bigInteger2 < 0 || this.q <= bigInteger2)
				{
					result = false;
				}
				else
				{
					BigInteger bi2 = bigInteger2.ModInverse(this.q);
					BigInteger bigInteger3 = bi * bi2 % this.q;
					BigInteger bigInteger4 = bigInteger * bi2 % this.q;
					bigInteger3 = this.g.ModPow(bigInteger3, this.p);
					bigInteger4 = this.y.ModPow(bigInteger4, this.p);
					BigInteger bi3 = bigInteger3 * bigInteger4 % this.p % this.q;
					result = (bi3 == bigInteger);
				}
			}
			catch
			{
				throw new CryptographicException("couldn't compute signature verification");
			}
			return result;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002B394 File Offset: 0x00029594
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.x != null)
				{
					this.x.Clear();
					this.x = null;
				}
				if (disposing)
				{
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
					if (this.g != null)
					{
						this.g.Clear();
						this.g = null;
					}
					if (this.j != null)
					{
						this.j.Clear();
						this.j = null;
					}
					if (this.seed != null)
					{
						this.seed.Clear();
						this.seed = null;
					}
					if (this.y != null)
					{
						this.y.Clear();
						this.y = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x04000253 RID: 595
		private const int defaultKeySize = 1024;

		// Token: 0x04000254 RID: 596
		private bool keypairGenerated;

		// Token: 0x04000255 RID: 597
		private bool m_disposed;

		// Token: 0x04000256 RID: 598
		private BigInteger p;

		// Token: 0x04000257 RID: 599
		private BigInteger q;

		// Token: 0x04000258 RID: 600
		private BigInteger g;

		// Token: 0x04000259 RID: 601
		private BigInteger x;

		// Token: 0x0400025A RID: 602
		private BigInteger y;

		// Token: 0x0400025B RID: 603
		private BigInteger j;

		// Token: 0x0400025C RID: 604
		private BigInteger seed;

		// Token: 0x0400025D RID: 605
		private int counter;

		// Token: 0x0400025E RID: 606
		private bool j_missing;

		// Token: 0x0400025F RID: 607
		private RandomNumberGenerator rng;

		// Token: 0x020006D7 RID: 1751
		// (Invoke) Token: 0x06004350 RID: 17232
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
