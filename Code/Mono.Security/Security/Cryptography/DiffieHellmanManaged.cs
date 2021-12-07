﻿using System;
using System.Security.Cryptography;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200002A RID: 42
	public sealed class DiffieHellmanManaged : DiffieHellman
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000B960 File Offset: 0x00009B60
		public DiffieHellmanManaged() : this(1024, 160, DHKeyGeneration.Static)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B974 File Offset: 0x00009B74
		public DiffieHellmanManaged(int bitLength, int l, DHKeyGeneration method)
		{
			if (bitLength < 256 || l < 0)
			{
				throw new ArgumentException();
			}
			BigInteger p;
			BigInteger g;
			this.GenerateKey(bitLength, method, out p, out g);
			this.Initialize(p, g, null, l, false);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public DiffieHellmanManaged(byte[] p, byte[] g, byte[] x)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (x == null)
			{
				this.Initialize(new BigInteger(p), new BigInteger(g), null, 0, true);
			}
			else
			{
				this.Initialize(new BigInteger(p), new BigInteger(g), new BigInteger(x), 0, true);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BA18 File Offset: 0x00009C18
		public DiffieHellmanManaged(byte[] p, byte[] g, int l)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (l < 0)
			{
				throw new ArgumentException();
			}
			this.Initialize(new BigInteger(p), new BigInteger(g), null, l, true);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		private void Initialize(BigInteger p, BigInteger g, BigInteger x, int secretLen, bool checkInput)
		{
			if (checkInput && (!p.IsProbablePrime() || g <= 0 || g >= p || (x != null && (x <= 0 || x > p - 2))))
			{
				throw new CryptographicException();
			}
			if (secretLen == 0)
			{
				secretLen = p.BitCount();
			}
			this.m_P = p;
			this.m_G = g;
			if (x == null)
			{
				BigInteger bi = this.m_P - 1;
				this.m_X = BigInteger.GenerateRandom(secretLen);
				while (this.m_X >= bi || this.m_X == 0U)
				{
					this.m_X = BigInteger.GenerateRandom(secretLen);
				}
			}
			else
			{
				this.m_X = x;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		public override byte[] CreateKeyExchange()
		{
			BigInteger bigInteger = this.m_G.ModPow(this.m_X, this.m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public override byte[] DecryptKeyExchange(byte[] keyEx)
		{
			BigInteger bigInteger = new BigInteger(keyEx);
			BigInteger bigInteger2 = bigInteger.ModPow(this.m_X, this.m_P);
			byte[] bytes = bigInteger2.GetBytes();
			bigInteger2.Clear();
			return bytes;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000BC24 File Offset: 0x00009E24
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "1.2.840.113549.1.3";
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000BC30 File Offset: 0x00009E30
		protected override void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				this.m_P.Clear();
				this.m_G.Clear();
				this.m_X.Clear();
			}
			this.m_Disposed = true;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BC68 File Offset: 0x00009E68
		public override DHParameters ExportParameters(bool includePrivateParameters)
		{
			DHParameters result = default(DHParameters);
			result.P = this.m_P.GetBytes();
			result.G = this.m_G.GetBytes();
			if (includePrivateParameters)
			{
				result.X = this.m_X.GetBytes();
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000BCBC File Offset: 0x00009EBC
		public override void ImportParameters(DHParameters parameters)
		{
			if (parameters.P == null)
			{
				throw new CryptographicException("Missing P value.");
			}
			if (parameters.G == null)
			{
				throw new CryptographicException("Missing G value.");
			}
			BigInteger p = new BigInteger(parameters.P);
			BigInteger g = new BigInteger(parameters.G);
			BigInteger x = null;
			if (parameters.X != null)
			{
				x = new BigInteger(parameters.X);
			}
			this.Initialize(p, g, x, 0, true);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000BD38 File Offset: 0x00009F38
		~DiffieHellmanManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BD74 File Offset: 0x00009F74
		private void GenerateKey(int bitlen, DHKeyGeneration keygen, out BigInteger p, out BigInteger g)
		{
			if (keygen == DHKeyGeneration.Static)
			{
				if (bitlen == 768)
				{
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY768);
				}
				else if (bitlen == 1024)
				{
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY1024);
				}
				else
				{
					if (bitlen != 1536)
					{
						throw new ArgumentException("Invalid bit size.");
					}
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY1536);
				}
				g = new BigInteger(22U);
			}
			else
			{
				p = BigInteger.GeneratePseudoPrime(bitlen);
				g = new BigInteger(3U);
			}
		}

		// Token: 0x040000B9 RID: 185
		private BigInteger m_P;

		// Token: 0x040000BA RID: 186
		private BigInteger m_G;

		// Token: 0x040000BB RID: 187
		private BigInteger m_X;

		// Token: 0x040000BC RID: 188
		private bool m_Disposed;

		// Token: 0x040000BD RID: 189
		private static byte[] m_OAKLEY768 = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			201,
			15,
			218,
			162,
			33,
			104,
			194,
			52,
			196,
			198,
			98,
			139,
			128,
			220,
			28,
			209,
			41,
			2,
			78,
			8,
			138,
			103,
			204,
			116,
			2,
			11,
			190,
			166,
			59,
			19,
			155,
			34,
			81,
			74,
			8,
			121,
			142,
			52,
			4,
			221,
			239,
			149,
			25,
			179,
			205,
			58,
			67,
			27,
			48,
			43,
			10,
			109,
			242,
			95,
			20,
			55,
			79,
			225,
			53,
			109,
			109,
			81,
			194,
			69,
			228,
			133,
			181,
			118,
			98,
			94,
			126,
			198,
			244,
			76,
			66,
			233,
			166,
			58,
			54,
			32,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040000BE RID: 190
		private static byte[] m_OAKLEY1024 = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			201,
			15,
			218,
			162,
			33,
			104,
			194,
			52,
			196,
			198,
			98,
			139,
			128,
			220,
			28,
			209,
			41,
			2,
			78,
			8,
			138,
			103,
			204,
			116,
			2,
			11,
			190,
			166,
			59,
			19,
			155,
			34,
			81,
			74,
			8,
			121,
			142,
			52,
			4,
			221,
			239,
			149,
			25,
			179,
			205,
			58,
			67,
			27,
			48,
			43,
			10,
			109,
			242,
			95,
			20,
			55,
			79,
			225,
			53,
			109,
			109,
			81,
			194,
			69,
			228,
			133,
			181,
			118,
			98,
			94,
			126,
			198,
			244,
			76,
			66,
			233,
			166,
			55,
			237,
			107,
			11,
			byte.MaxValue,
			92,
			182,
			244,
			6,
			183,
			237,
			238,
			56,
			107,
			251,
			90,
			137,
			159,
			165,
			174,
			159,
			36,
			17,
			124,
			75,
			31,
			230,
			73,
			40,
			102,
			81,
			236,
			230,
			83,
			129,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040000BF RID: 191
		private static byte[] m_OAKLEY1536 = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			201,
			15,
			218,
			162,
			33,
			104,
			194,
			52,
			196,
			198,
			98,
			139,
			128,
			220,
			28,
			209,
			41,
			2,
			78,
			8,
			138,
			103,
			204,
			116,
			2,
			11,
			190,
			166,
			59,
			19,
			155,
			34,
			81,
			74,
			8,
			121,
			142,
			52,
			4,
			221,
			239,
			149,
			25,
			179,
			205,
			58,
			67,
			27,
			48,
			43,
			10,
			109,
			242,
			95,
			20,
			55,
			79,
			225,
			53,
			109,
			109,
			81,
			194,
			69,
			228,
			133,
			181,
			118,
			98,
			94,
			126,
			198,
			244,
			76,
			66,
			233,
			166,
			55,
			237,
			107,
			11,
			byte.MaxValue,
			92,
			182,
			244,
			6,
			183,
			237,
			238,
			56,
			107,
			251,
			90,
			137,
			159,
			165,
			174,
			159,
			36,
			17,
			124,
			75,
			31,
			230,
			73,
			40,
			102,
			81,
			236,
			228,
			91,
			61,
			194,
			0,
			124,
			184,
			161,
			99,
			191,
			5,
			152,
			218,
			72,
			54,
			28,
			85,
			211,
			154,
			105,
			22,
			63,
			168,
			253,
			36,
			207,
			95,
			131,
			101,
			93,
			35,
			220,
			163,
			173,
			150,
			28,
			98,
			243,
			86,
			32,
			133,
			82,
			187,
			158,
			213,
			41,
			7,
			112,
			150,
			150,
			109,
			103,
			12,
			53,
			78,
			74,
			188,
			152,
			4,
			241,
			116,
			108,
			8,
			202,
			35,
			115,
			39,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}
