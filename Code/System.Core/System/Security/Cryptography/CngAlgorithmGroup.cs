using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public sealed class CngAlgorithmGroup : IEquatable<CngAlgorithmGroup>
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x00018058 File Offset: 0x00016258
		public CngAlgorithmGroup(string algorithmGroup)
		{
			if (algorithmGroup == null)
			{
				throw new ArgumentNullException("algorithmGroup");
			}
			if (algorithmGroup.Length == 0)
			{
				throw new ArgumentException("algorithmGroup");
			}
			this.group = algorithmGroup;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001809C File Offset: 0x0001629C
		public string AlgorithmGroup
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000180A4 File Offset: 0x000162A4
		public bool Equals(CngAlgorithmGroup other)
		{
			return this == other;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000180B0 File Offset: 0x000162B0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithmGroup);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000180C0 File Offset: 0x000162C0
		public override int GetHashCode()
		{
			return this.group.GetHashCode();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000180D0 File Offset: 0x000162D0
		public override string ToString()
		{
			return this.group;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000180D8 File Offset: 0x000162D8
		public static CngAlgorithmGroup DiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.dh == null)
				{
					CngAlgorithmGroup.dh = new CngAlgorithmGroup("DH");
				}
				return CngAlgorithmGroup.dh;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001810C File Offset: 0x0001630C
		public static CngAlgorithmGroup Dsa
		{
			get
			{
				if (CngAlgorithmGroup.dsa == null)
				{
					CngAlgorithmGroup.dsa = new CngAlgorithmGroup("DSA");
				}
				return CngAlgorithmGroup.dsa;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00018140 File Offset: 0x00016340
		public static CngAlgorithmGroup ECDiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.ecdh == null)
				{
					CngAlgorithmGroup.ecdh = new CngAlgorithmGroup("ECDH");
				}
				return CngAlgorithmGroup.ecdh;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00018174 File Offset: 0x00016374
		public static CngAlgorithmGroup ECDsa
		{
			get
			{
				if (CngAlgorithmGroup.ecdsa == null)
				{
					CngAlgorithmGroup.ecdsa = new CngAlgorithmGroup("ECDSA");
				}
				return CngAlgorithmGroup.ecdsa;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000181A8 File Offset: 0x000163A8
		public static CngAlgorithmGroup Rsa
		{
			get
			{
				if (CngAlgorithmGroup.rsa == null)
				{
					CngAlgorithmGroup.rsa = new CngAlgorithmGroup("RSA");
				}
				return CngAlgorithmGroup.rsa;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000181DC File Offset: 0x000163DC
		public static bool operator ==(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.group == right.group;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00018210 File Offset: 0x00016410
		public static bool operator !=(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.group != right.group;
		}

		// Token: 0x0400014E RID: 334
		private string group;

		// Token: 0x0400014F RID: 335
		private static CngAlgorithmGroup dh;

		// Token: 0x04000150 RID: 336
		private static CngAlgorithmGroup dsa;

		// Token: 0x04000151 RID: 337
		private static CngAlgorithmGroup ecdh;

		// Token: 0x04000152 RID: 338
		private static CngAlgorithmGroup ecdsa;

		// Token: 0x04000153 RID: 339
		private static CngAlgorithmGroup rsa;
	}
}
