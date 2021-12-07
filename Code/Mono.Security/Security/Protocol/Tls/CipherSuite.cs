using System;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000080 RID: 128
	internal abstract class CipherSuite
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		public CipherSuite(short code, string name, CipherAlgorithmType cipherAlgorithmType, HashAlgorithmType hashAlgorithmType, ExchangeAlgorithmType exchangeAlgorithmType, bool exportable, bool blockMode, byte keyMaterialSize, byte expandedKeyMaterialSize, short effectiveKeyBits, byte ivSize, byte blockSize)
		{
			this.code = code;
			this.name = name;
			this.cipherAlgorithmType = cipherAlgorithmType;
			this.hashAlgorithmType = hashAlgorithmType;
			this.exchangeAlgorithmType = exchangeAlgorithmType;
			this.isExportable = exportable;
			if (blockMode)
			{
				this.cipherMode = CipherMode.CBC;
			}
			this.keyMaterialSize = keyMaterialSize;
			this.expandedKeyMaterialSize = expandedKeyMaterialSize;
			this.effectiveKeyBits = effectiveKeyBits;
			this.ivSize = ivSize;
			this.blockSize = blockSize;
			this.keyBlockSize = (int)this.keyMaterialSize + this.HashSize + (int)this.ivSize << 1;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001C35C File Offset: 0x0001A55C
		protected ICryptoTransform EncryptionCipher
		{
			get
			{
				return this.encryptionCipher;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001C364 File Offset: 0x0001A564
		protected ICryptoTransform DecryptionCipher
		{
			get
			{
				return this.decryptionCipher;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001C36C File Offset: 0x0001A56C
		protected KeyedHashAlgorithm ClientHMAC
		{
			get
			{
				return this.clientHMAC;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001C374 File Offset: 0x0001A574
		protected KeyedHashAlgorithm ServerHMAC
		{
			get
			{
				return this.serverHMAC;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001C37C File Offset: 0x0001A57C
		public CipherAlgorithmType CipherAlgorithmType
		{
			get
			{
				return this.cipherAlgorithmType;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001C384 File Offset: 0x0001A584
		public string HashAlgorithmName
		{
			get
			{
				switch (this.hashAlgorithmType)
				{
				case HashAlgorithmType.Md5:
					return "MD5";
				case HashAlgorithmType.Sha1:
					return "SHA1";
				}
				return "None";
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		public HashAlgorithmType HashAlgorithmType
		{
			get
			{
				return this.hashAlgorithmType;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
		public int HashSize
		{
			get
			{
				switch (this.hashAlgorithmType)
				{
				case HashAlgorithmType.Md5:
					return 16;
				case HashAlgorithmType.Sha1:
					return 20;
				}
				return 0;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		public ExchangeAlgorithmType ExchangeAlgorithmType
		{
			get
			{
				return this.exchangeAlgorithmType;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001C404 File Offset: 0x0001A604
		public CipherMode CipherMode
		{
			get
			{
				return this.cipherMode;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001C40C File Offset: 0x0001A60C
		public short Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001C414 File Offset: 0x0001A614
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001C41C File Offset: 0x0001A61C
		public bool IsExportable
		{
			get
			{
				return this.isExportable;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001C424 File Offset: 0x0001A624
		public byte KeyMaterialSize
		{
			get
			{
				return this.keyMaterialSize;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001C42C File Offset: 0x0001A62C
		public int KeyBlockSize
		{
			get
			{
				return this.keyBlockSize;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001C434 File Offset: 0x0001A634
		public byte ExpandedKeyMaterialSize
		{
			get
			{
				return this.expandedKeyMaterialSize;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001C43C File Offset: 0x0001A63C
		public short EffectiveKeyBits
		{
			get
			{
				return this.effectiveKeyBits;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001C444 File Offset: 0x0001A644
		public byte IvSize
		{
			get
			{
				return this.ivSize;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001C44C File Offset: 0x0001A64C
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0001C454 File Offset: 0x0001A654
		public Context Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001C460 File Offset: 0x0001A660
		internal void Write(byte[] array, int offset, short value)
		{
			if (offset > array.Length - 2)
			{
				throw new ArgumentException("offset");
			}
			array[offset] = (byte)(value >> 8);
			array[offset + 1] = (byte)value;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001C494 File Offset: 0x0001A694
		internal void Write(byte[] array, int offset, ulong value)
		{
			if (offset > array.Length - 8)
			{
				throw new ArgumentException("offset");
			}
			array[offset] = (byte)(value >> 56);
			array[offset + 1] = (byte)(value >> 48);
			array[offset + 2] = (byte)(value >> 40);
			array[offset + 3] = (byte)(value >> 32);
			array[offset + 4] = (byte)(value >> 24);
			array[offset + 5] = (byte)(value >> 16);
			array[offset + 6] = (byte)(value >> 8);
			array[offset + 7] = (byte)value;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001C504 File Offset: 0x0001A704
		public void InitializeCipher()
		{
			this.createEncryptionCipher();
			this.createDecryptionCipher();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001C514 File Offset: 0x0001A714
		public byte[] EncryptRecord(byte[] fragment, byte[] mac)
		{
			int num = fragment.Length + mac.Length;
			int num2 = 0;
			if (this.CipherMode == CipherMode.CBC)
			{
				num++;
				num2 = (int)this.blockSize - num % (int)this.blockSize;
				if (num2 == (int)this.blockSize)
				{
					num2 = 0;
				}
				num += num2;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(fragment, 0, array, 0, fragment.Length);
			Buffer.BlockCopy(mac, 0, array, fragment.Length, mac.Length);
			if (num2 > 0)
			{
				int num3 = fragment.Length + mac.Length;
				for (int i = num3; i < num3 + num2 + 1; i++)
				{
					array[i] = (byte)num2;
				}
			}
			this.EncryptionCipher.TransformBlock(array, 0, array.Length, array, 0);
			return array;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public void DecryptRecord(byte[] fragment, out byte[] dcrFragment, out byte[] dcrMAC)
		{
			this.DecryptionCipher.TransformBlock(fragment, 0, fragment.Length, fragment, 0);
			int num2;
			if (this.CipherMode == CipherMode.CBC)
			{
				int num = (int)fragment[fragment.Length - 1];
				num2 = fragment.Length - (num + 1) - this.HashSize;
			}
			else
			{
				num2 = fragment.Length - this.HashSize;
			}
			dcrFragment = new byte[num2];
			dcrMAC = new byte[this.HashSize];
			Buffer.BlockCopy(fragment, 0, dcrFragment, 0, dcrFragment.Length);
			Buffer.BlockCopy(fragment, dcrFragment.Length, dcrMAC, 0, dcrMAC.Length);
		}

		// Token: 0x06000493 RID: 1171
		public abstract byte[] ComputeClientRecordMAC(ContentType contentType, byte[] fragment);

		// Token: 0x06000494 RID: 1172
		public abstract byte[] ComputeServerRecordMAC(ContentType contentType, byte[] fragment);

		// Token: 0x06000495 RID: 1173
		public abstract void ComputeMasterSecret(byte[] preMasterSecret);

		// Token: 0x06000496 RID: 1174
		public abstract void ComputeKeys();

		// Token: 0x06000497 RID: 1175 RVA: 0x0001C650 File Offset: 0x0001A850
		public byte[] CreatePremasterSecret()
		{
			ClientContext clientContext = (ClientContext)this.context;
			byte[] secureRandomBytes = this.context.GetSecureRandomBytes(48);
			secureRandomBytes[0] = (byte)(clientContext.ClientHelloProtocol >> 8);
			secureRandomBytes[1] = (byte)clientContext.ClientHelloProtocol;
			return secureRandomBytes;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001C690 File Offset: 0x0001A890
		public byte[] PRF(byte[] secret, string label, byte[] data, int length)
		{
			int num = secret.Length >> 1;
			if ((secret.Length & 1) == 1)
			{
				num++;
			}
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(Encoding.ASCII.GetBytes(label));
			tlsStream.Write(data);
			byte[] seed = tlsStream.ToArray();
			tlsStream.Reset();
			byte[] array = new byte[num];
			Buffer.BlockCopy(secret, 0, array, 0, num);
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(secret, secret.Length - num, array2, 0, num);
			byte[] array3 = this.Expand("MD5", array, seed, length);
			byte[] array4 = this.Expand("SHA1", array2, seed, length);
			byte[] array5 = new byte[length];
			for (int i = 0; i < array5.Length; i++)
			{
				array5[i] = (array3[i] ^ array4[i]);
			}
			return array5;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001C760 File Offset: 0x0001A960
		public byte[] Expand(string hashName, byte[] secret, byte[] seed, int length)
		{
			int num = (!(hashName == "MD5")) ? 20 : 16;
			int num2 = length / num;
			if (length % num > 0)
			{
				num2++;
			}
			Mono.Security.Cryptography.HMAC hmac = new Mono.Security.Cryptography.HMAC(hashName, secret);
			TlsStream tlsStream = new TlsStream();
			byte[][] array = new byte[num2 + 1][];
			array[0] = seed;
			for (int i = 1; i <= num2; i++)
			{
				TlsStream tlsStream2 = new TlsStream();
				hmac.TransformFinalBlock(array[i - 1], 0, array[i - 1].Length);
				array[i] = hmac.Hash;
				tlsStream2.Write(array[i]);
				tlsStream2.Write(seed);
				hmac.TransformFinalBlock(tlsStream2.ToArray(), 0, (int)tlsStream2.Length);
				tlsStream.Write(hmac.Hash);
				tlsStream2.Reset();
			}
			byte[] array2 = new byte[length];
			Buffer.BlockCopy(tlsStream.ToArray(), 0, array2, 0, array2.Length);
			tlsStream.Reset();
			return array2;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001C85C File Offset: 0x0001AA5C
		private void createEncryptionCipher()
		{
			switch (this.cipherAlgorithmType)
			{
			case CipherAlgorithmType.Des:
				this.encryptionAlgorithm = DES.Create();
				break;
			case CipherAlgorithmType.Rc2:
				this.encryptionAlgorithm = RC2.Create();
				break;
			case CipherAlgorithmType.Rc4:
				this.encryptionAlgorithm = new ARC4Managed();
				break;
			case CipherAlgorithmType.Rijndael:
				this.encryptionAlgorithm = Rijndael.Create();
				break;
			case CipherAlgorithmType.TripleDes:
				this.encryptionAlgorithm = TripleDES.Create();
				break;
			}
			if (this.cipherMode == CipherMode.CBC)
			{
				this.encryptionAlgorithm.Mode = this.cipherMode;
				this.encryptionAlgorithm.Padding = PaddingMode.None;
				this.encryptionAlgorithm.KeySize = (int)(this.expandedKeyMaterialSize * 8);
				this.encryptionAlgorithm.BlockSize = (int)(this.blockSize * 8);
			}
			if (this.context is ClientContext)
			{
				this.encryptionAlgorithm.Key = this.context.ClientWriteKey;
				this.encryptionAlgorithm.IV = this.context.ClientWriteIV;
			}
			else
			{
				this.encryptionAlgorithm.Key = this.context.ServerWriteKey;
				this.encryptionAlgorithm.IV = this.context.ServerWriteIV;
			}
			this.encryptionCipher = this.encryptionAlgorithm.CreateEncryptor();
			if (this.context is ClientContext)
			{
				this.clientHMAC = new Mono.Security.Cryptography.HMAC(this.HashAlgorithmName, this.context.Negotiating.ClientWriteMAC);
			}
			else
			{
				this.serverHMAC = new Mono.Security.Cryptography.HMAC(this.HashAlgorithmName, this.context.Negotiating.ServerWriteMAC);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		private void createDecryptionCipher()
		{
			switch (this.cipherAlgorithmType)
			{
			case CipherAlgorithmType.Des:
				this.decryptionAlgorithm = DES.Create();
				break;
			case CipherAlgorithmType.Rc2:
				this.decryptionAlgorithm = RC2.Create();
				break;
			case CipherAlgorithmType.Rc4:
				this.decryptionAlgorithm = new ARC4Managed();
				break;
			case CipherAlgorithmType.Rijndael:
				this.decryptionAlgorithm = Rijndael.Create();
				break;
			case CipherAlgorithmType.TripleDes:
				this.decryptionAlgorithm = TripleDES.Create();
				break;
			}
			if (this.cipherMode == CipherMode.CBC)
			{
				this.decryptionAlgorithm.Mode = this.cipherMode;
				this.decryptionAlgorithm.Padding = PaddingMode.None;
				this.decryptionAlgorithm.KeySize = (int)(this.expandedKeyMaterialSize * 8);
				this.decryptionAlgorithm.BlockSize = (int)(this.blockSize * 8);
			}
			if (this.context is ClientContext)
			{
				this.decryptionAlgorithm.Key = this.context.ServerWriteKey;
				this.decryptionAlgorithm.IV = this.context.ServerWriteIV;
			}
			else
			{
				this.decryptionAlgorithm.Key = this.context.ClientWriteKey;
				this.decryptionAlgorithm.IV = this.context.ClientWriteIV;
			}
			this.decryptionCipher = this.decryptionAlgorithm.CreateDecryptor();
			if (this.context is ClientContext)
			{
				this.serverHMAC = new Mono.Security.Cryptography.HMAC(this.HashAlgorithmName, this.context.Negotiating.ServerWriteMAC);
			}
			else
			{
				this.clientHMAC = new Mono.Security.Cryptography.HMAC(this.HashAlgorithmName, this.context.Negotiating.ClientWriteMAC);
			}
		}

		// Token: 0x0400023C RID: 572
		public static byte[] EmptyArray = new byte[0];

		// Token: 0x0400023D RID: 573
		private short code;

		// Token: 0x0400023E RID: 574
		private string name;

		// Token: 0x0400023F RID: 575
		private CipherAlgorithmType cipherAlgorithmType;

		// Token: 0x04000240 RID: 576
		private HashAlgorithmType hashAlgorithmType;

		// Token: 0x04000241 RID: 577
		private ExchangeAlgorithmType exchangeAlgorithmType;

		// Token: 0x04000242 RID: 578
		private bool isExportable;

		// Token: 0x04000243 RID: 579
		private CipherMode cipherMode;

		// Token: 0x04000244 RID: 580
		private byte keyMaterialSize;

		// Token: 0x04000245 RID: 581
		private int keyBlockSize;

		// Token: 0x04000246 RID: 582
		private byte expandedKeyMaterialSize;

		// Token: 0x04000247 RID: 583
		private short effectiveKeyBits;

		// Token: 0x04000248 RID: 584
		private byte ivSize;

		// Token: 0x04000249 RID: 585
		private byte blockSize;

		// Token: 0x0400024A RID: 586
		private Context context;

		// Token: 0x0400024B RID: 587
		private SymmetricAlgorithm encryptionAlgorithm;

		// Token: 0x0400024C RID: 588
		private ICryptoTransform encryptionCipher;

		// Token: 0x0400024D RID: 589
		private SymmetricAlgorithm decryptionAlgorithm;

		// Token: 0x0400024E RID: 590
		private ICryptoTransform decryptionCipher;

		// Token: 0x0400024F RID: 591
		private KeyedHashAlgorithm clientHMAC;

		// Token: 0x04000250 RID: 592
		private KeyedHashAlgorithm serverHMAC;
	}
}
