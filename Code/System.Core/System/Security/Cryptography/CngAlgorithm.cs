using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public sealed class CngAlgorithm : IEquatable<CngAlgorithm>
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00017D24 File Offset: 0x00015F24
		public CngAlgorithm(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (algorithm.Length == 0)
			{
				throw new ArgumentException("algorithm");
			}
			this.algo = algorithm;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00017D68 File Offset: 0x00015F68
		public string Algorithm
		{
			get
			{
				return this.algo;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00017D70 File Offset: 0x00015F70
		public bool Equals(CngAlgorithm other)
		{
			return !(other == null) && this.algo == other.algo;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017D94 File Offset: 0x00015F94
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithm);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public override int GetHashCode()
		{
			return this.algo.GetHashCode();
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00017DB4 File Offset: 0x00015FB4
		public override string ToString()
		{
			return this.algo;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00017DBC File Offset: 0x00015FBC
		public static CngAlgorithm ECDiffieHellmanP256
		{
			get
			{
				if (CngAlgorithm.dh256 == null)
				{
					CngAlgorithm.dh256 = new CngAlgorithm("ECDH_P256");
				}
				return CngAlgorithm.dh256;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public static CngAlgorithm ECDiffieHellmanP384
		{
			get
			{
				if (CngAlgorithm.dh384 == null)
				{
					CngAlgorithm.dh384 = new CngAlgorithm("ECDH_P384");
				}
				return CngAlgorithm.dh384;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00017E24 File Offset: 0x00016024
		public static CngAlgorithm ECDiffieHellmanP521
		{
			get
			{
				if (CngAlgorithm.dh521 == null)
				{
					CngAlgorithm.dh521 = new CngAlgorithm("ECDH_P521");
				}
				return CngAlgorithm.dh521;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00017E58 File Offset: 0x00016058
		public static CngAlgorithm ECDsaP256
		{
			get
			{
				if (CngAlgorithm.dsa256 == null)
				{
					CngAlgorithm.dsa256 = new CngAlgorithm("ECDSA_P256");
				}
				return CngAlgorithm.dsa256;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00017E8C File Offset: 0x0001608C
		public static CngAlgorithm ECDsaP384
		{
			get
			{
				if (CngAlgorithm.dsa384 == null)
				{
					CngAlgorithm.dsa384 = new CngAlgorithm("ECDSA_P384");
				}
				return CngAlgorithm.dsa384;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00017EC0 File Offset: 0x000160C0
		public static CngAlgorithm ECDsaP521
		{
			get
			{
				if (CngAlgorithm.dsa521 == null)
				{
					CngAlgorithm.dsa521 = new CngAlgorithm("ECDSA_P521");
				}
				return CngAlgorithm.dsa521;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00017EF4 File Offset: 0x000160F4
		public static CngAlgorithm MD5
		{
			get
			{
				if (CngAlgorithm.md5 == null)
				{
					CngAlgorithm.md5 = new CngAlgorithm("MD5");
				}
				return CngAlgorithm.md5;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00017F28 File Offset: 0x00016128
		public static CngAlgorithm Sha1
		{
			get
			{
				if (CngAlgorithm.sha1 == null)
				{
					CngAlgorithm.sha1 = new CngAlgorithm("SHA1");
				}
				return CngAlgorithm.sha1;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00017F5C File Offset: 0x0001615C
		public static CngAlgorithm Sha256
		{
			get
			{
				if (CngAlgorithm.sha256 == null)
				{
					CngAlgorithm.sha256 = new CngAlgorithm("SHA256");
				}
				return CngAlgorithm.sha256;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00017F90 File Offset: 0x00016190
		public static CngAlgorithm Sha384
		{
			get
			{
				if (CngAlgorithm.sha384 == null)
				{
					CngAlgorithm.sha384 = new CngAlgorithm("SHA384");
				}
				return CngAlgorithm.sha384;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00017FC4 File Offset: 0x000161C4
		public static CngAlgorithm Sha512
		{
			get
			{
				if (CngAlgorithm.sha512 == null)
				{
					CngAlgorithm.sha512 = new CngAlgorithm("SHA512");
				}
				return CngAlgorithm.sha512;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017FF8 File Offset: 0x000161F8
		public static bool operator ==(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.algo == right.algo;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001802C File Offset: 0x0001622C
		public static bool operator !=(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.algo != right.algo;
		}

		// Token: 0x04000142 RID: 322
		private string algo;

		// Token: 0x04000143 RID: 323
		private static CngAlgorithm dh256;

		// Token: 0x04000144 RID: 324
		private static CngAlgorithm dh384;

		// Token: 0x04000145 RID: 325
		private static CngAlgorithm dh521;

		// Token: 0x04000146 RID: 326
		private static CngAlgorithm dsa256;

		// Token: 0x04000147 RID: 327
		private static CngAlgorithm dsa384;

		// Token: 0x04000148 RID: 328
		private static CngAlgorithm dsa521;

		// Token: 0x04000149 RID: 329
		private static CngAlgorithm md5;

		// Token: 0x0400014A RID: 330
		private static CngAlgorithm sha1;

		// Token: 0x0400014B RID: 331
		private static CngAlgorithm sha256;

		// Token: 0x0400014C RID: 332
		private static CngAlgorithm sha384;

		// Token: 0x0400014D RID: 333
		private static CngAlgorithm sha512;
	}
}
