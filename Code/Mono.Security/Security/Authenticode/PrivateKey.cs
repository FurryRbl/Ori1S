using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000022 RID: 34
	public class PrivateKey
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00009A30 File Offset: 0x00007C30
		public PrivateKey()
		{
			this.keyType = 2;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009A40 File Offset: 0x00007C40
		public PrivateKey(byte[] data, string password)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!this.Decode(data, password))
			{
				throw new CryptographicException(Locale.GetText("Invalid data and/or password"));
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00009A84 File Offset: 0x00007C84
		public bool Encrypted
		{
			get
			{
				return this.encrypted;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00009A8C File Offset: 0x00007C8C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00009A94 File Offset: 0x00007C94
		public int KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				this.keyType = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00009AA0 File Offset: 0x00007CA0
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00009AB4 File Offset: 0x00007CB4
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00009AD0 File Offset: 0x00007CD0
		public bool Weak
		{
			get
			{
				return !this.encrypted || this.weak;
			}
			set
			{
				this.weak = value;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009ADC File Offset: 0x00007CDC
		private byte[] DeriveKey(byte[] salt, string password)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			SHA1 sha = SHA1.Create();
			sha.TransformBlock(salt, 0, salt.Length, salt, 0);
			sha.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = new byte[16];
			Buffer.BlockCopy(sha.Hash, 0, array, 0, 16);
			sha.Clear();
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009B40 File Offset: 0x00007D40
		private bool Decode(byte[] pvk, string password)
		{
			if (BitConverterLE.ToUInt32(pvk, 0) != 2964713758U)
			{
				return false;
			}
			if (BitConverterLE.ToUInt32(pvk, 4) != 0U)
			{
				return false;
			}
			this.keyType = BitConverterLE.ToInt32(pvk, 8);
			this.encrypted = (BitConverterLE.ToUInt32(pvk, 12) == 1U);
			int num = BitConverterLE.ToInt32(pvk, 16);
			int num2 = BitConverterLE.ToInt32(pvk, 20);
			byte[] array = new byte[num2];
			Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
			if (num > 0)
			{
				if (password == null)
				{
					return false;
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(pvk, 24, array2, 0, num);
				byte[] array3 = this.DeriveKey(array2, password);
				RC4 rc = RC4.Create();
				ICryptoTransform cryptoTransform = rc.CreateDecryptor(array3, null);
				cryptoTransform.TransformBlock(array, 8, array.Length - 8, array, 8);
				try
				{
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
					this.weak = false;
				}
				catch (CryptographicException)
				{
					this.weak = true;
					Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
					Array.Clear(array3, 5, 11);
					RC4 rc2 = RC4.Create();
					cryptoTransform = rc2.CreateDecryptor(array3, null);
					cryptoTransform.TransformBlock(array, 8, array.Length - 8, array, 8);
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				}
				Array.Clear(array3, 0, array3.Length);
			}
			else
			{
				this.weak = true;
				this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				Array.Clear(array, 0, array.Length);
			}
			Array.Clear(pvk, 0, pvk.Length);
			return this.rsa != null;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public void Save(string filename)
		{
			this.Save(filename, null);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009CDC File Offset: 0x00007EDC
		public void Save(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			FileStream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
			try
			{
				byte[] array2 = new byte[4];
				byte[] bytes = BitConverterLE.GetBytes(2964713758U);
				fileStream.Write(bytes, 0, 4);
				fileStream.Write(array2, 0, 4);
				bytes = BitConverterLE.GetBytes(this.keyType);
				fileStream.Write(bytes, 0, 4);
				this.encrypted = (password != null);
				array = CryptoConvert.ToCapiPrivateKeyBlob(this.rsa);
				if (this.encrypted)
				{
					bytes = BitConverterLE.GetBytes(1);
					fileStream.Write(bytes, 0, 4);
					bytes = BitConverterLE.GetBytes(16);
					fileStream.Write(bytes, 0, 4);
					bytes = BitConverterLE.GetBytes(array.Length);
					fileStream.Write(bytes, 0, 4);
					byte[] array3 = new byte[16];
					RC4 rc = RC4.Create();
					byte[] array4 = null;
					try
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						randomNumberGenerator.GetBytes(array3);
						fileStream.Write(array3, 0, array3.Length);
						array4 = this.DeriveKey(array3, password);
						if (this.Weak)
						{
							Array.Clear(array4, 5, 11);
						}
						ICryptoTransform cryptoTransform = rc.CreateEncryptor(array4, null);
						cryptoTransform.TransformBlock(array, 8, array.Length - 8, array, 8);
					}
					finally
					{
						Array.Clear(array3, 0, array3.Length);
						Array.Clear(array4, 0, array4.Length);
						rc.Clear();
					}
				}
				else
				{
					fileStream.Write(array2, 0, 4);
					fileStream.Write(array2, 0, 4);
					bytes = BitConverterLE.GetBytes(array.Length);
					fileStream.Write(bytes, 0, 4);
				}
				fileStream.Write(array, 0, array.Length);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				fileStream.Close();
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009EA8 File Offset: 0x000080A8
		public static PrivateKey CreateFromFile(string filename)
		{
			return PrivateKey.CreateFromFile(filename, null);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009EB4 File Offset: 0x000080B4
		public static PrivateKey CreateFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new PrivateKey(array, password);
		}

		// Token: 0x040000A4 RID: 164
		private const uint magic = 2964713758U;

		// Token: 0x040000A5 RID: 165
		private bool encrypted;

		// Token: 0x040000A6 RID: 166
		private RSA rsa;

		// Token: 0x040000A7 RID: 167
		private bool weak;

		// Token: 0x040000A8 RID: 168
		private int keyType;
	}
}
