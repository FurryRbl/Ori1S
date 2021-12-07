using System;
using System.IO;
using System.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000B1 RID: 177
	internal class AuthenticodeBase
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x0002834C File Offset: 0x0002654C
		public AuthenticodeBase()
		{
			this.fileblock = new byte[4096];
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00028364 File Offset: 0x00026564
		internal int PEOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.peOffset;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00028380 File Offset: 0x00026580
		internal int CoffSymbolTableOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.coffSymbolTableOffset;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002839C File Offset: 0x0002659C
		internal int SecurityOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.dirSecurityOffset;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000283B8 File Offset: 0x000265B8
		internal void Open(string filename)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000283E8 File Offset: 0x000265E8
		internal void Close()
		{
			if (this.fs != null)
			{
				this.fs.Close();
				this.fs = null;
				this.blockNo = 0;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002841C File Offset: 0x0002661C
		internal bool ReadFirstBlock()
		{
			if (this.fs == null)
			{
				return false;
			}
			this.fs.Position = 0L;
			this.blockLength = this.fs.Read(this.fileblock, 0, this.fileblock.Length);
			this.blockNo = 1;
			if (this.blockLength < 64)
			{
				return false;
			}
			if (BitConverterLE.ToUInt16(this.fileblock, 0) != 23117)
			{
				return false;
			}
			this.peOffset = BitConverterLE.ToInt32(this.fileblock, 60);
			if (this.peOffset > this.fileblock.Length)
			{
				string message = string.Format(Locale.GetText("Header size too big (> {0} bytes)."), this.fileblock.Length);
				throw new NotSupportedException(message);
			}
			if ((long)this.peOffset > this.fs.Length)
			{
				return false;
			}
			if (BitConverterLE.ToUInt32(this.fileblock, this.peOffset) != 17744U)
			{
				return false;
			}
			this.dirSecurityOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 152);
			this.dirSecuritySize = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 156);
			this.coffSymbolTableOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 12);
			return true;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00028568 File Offset: 0x00026768
		internal byte[] GetSecurityEntry()
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			if (this.dirSecuritySize > 8)
			{
				byte[] array = new byte[this.dirSecuritySize - 8];
				this.fs.Position = (long)(this.dirSecurityOffset + 8);
				this.fs.Read(array, 0, array.Length);
				return array;
			}
			return null;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000285CC File Offset: 0x000267CC
		internal byte[] GetHash(HashAlgorithm hash)
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			this.fs.Position = (long)this.blockLength;
			int num = 0;
			long num2;
			if (this.dirSecurityOffset > 0)
			{
				if (this.dirSecurityOffset < this.blockLength)
				{
					this.blockLength = this.dirSecurityOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.dirSecurityOffset - this.blockLength);
				}
			}
			else if (this.coffSymbolTableOffset > 0)
			{
				this.fileblock[this.PEOffset + 12] = 0;
				this.fileblock[this.PEOffset + 13] = 0;
				this.fileblock[this.PEOffset + 14] = 0;
				this.fileblock[this.PEOffset + 15] = 0;
				this.fileblock[this.PEOffset + 16] = 0;
				this.fileblock[this.PEOffset + 17] = 0;
				this.fileblock[this.PEOffset + 18] = 0;
				this.fileblock[this.PEOffset + 19] = 0;
				if (this.coffSymbolTableOffset < this.blockLength)
				{
					this.blockLength = this.coffSymbolTableOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.coffSymbolTableOffset - this.blockLength);
				}
			}
			else
			{
				num = (int)(this.fs.Length & 7L);
				if (num > 0)
				{
					num = 8 - num;
				}
				num2 = this.fs.Length - (long)this.blockLength;
			}
			int num3 = this.peOffset + 88;
			hash.TransformBlock(this.fileblock, 0, num3, this.fileblock, 0);
			num3 += 4;
			hash.TransformBlock(this.fileblock, num3, 60, this.fileblock, num3);
			num3 += 68;
			if (num2 == 0L)
			{
				hash.TransformFinalBlock(this.fileblock, num3, this.blockLength - num3);
			}
			else
			{
				hash.TransformBlock(this.fileblock, num3, this.blockLength - num3, this.fileblock, num3);
				long num4 = num2 >> 12;
				int num5 = (int)(num2 - (num4 << 12));
				if (num5 == 0)
				{
					num4 -= 1L;
					num5 = 4096;
				}
				for (;;)
				{
					long num6 = num4;
					num4 = num6 - 1L;
					if (num6 <= 0L)
					{
						break;
					}
					this.fs.Read(this.fileblock, 0, this.fileblock.Length);
					hash.TransformBlock(this.fileblock, 0, this.fileblock.Length, this.fileblock, 0);
				}
				if (this.fs.Read(this.fileblock, 0, num5) != num5)
				{
					return null;
				}
				if (num > 0)
				{
					hash.TransformBlock(this.fileblock, 0, num5, this.fileblock, 0);
					hash.TransformFinalBlock(new byte[num], 0, num);
				}
				else
				{
					hash.TransformFinalBlock(this.fileblock, 0, num5);
				}
			}
			return hash.Hash;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00028890 File Offset: 0x00026A90
		protected byte[] HashFile(string fileName, string hashName)
		{
			byte[] result;
			try
			{
				this.Open(fileName);
				HashAlgorithm hash = HashAlgorithm.Create(hashName);
				byte[] hash2 = this.GetHash(hash);
				this.Close();
				result = hash2;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000236 RID: 566
		public const string spcIndirectDataContext = "1.3.6.1.4.1.311.2.1.4";

		// Token: 0x04000237 RID: 567
		private byte[] fileblock;

		// Token: 0x04000238 RID: 568
		private FileStream fs;

		// Token: 0x04000239 RID: 569
		private int blockNo;

		// Token: 0x0400023A RID: 570
		private int blockLength;

		// Token: 0x0400023B RID: 571
		private int peOffset;

		// Token: 0x0400023C RID: 572
		private int dirSecurityOffset;

		// Token: 0x0400023D RID: 573
		private int dirSecuritySize;

		// Token: 0x0400023E RID: 574
		private int coffSymbolTableOffset;
	}
}
