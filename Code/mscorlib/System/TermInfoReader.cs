using System;
using System.IO;
using System.Text;

namespace System
{
	// Token: 0x0200017D RID: 381
	internal class TermInfoReader
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x0005084C File Offset: 0x0004EA4C
		public TermInfoReader(string term, string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				long length = fileStream.Length;
				if (length > 4096L)
				{
					throw new Exception("File must be smaller than 4K");
				}
				this.buffer = new byte[(int)length];
				if (fileStream.Read(this.buffer, 0, this.buffer.Length) != this.buffer.Length)
				{
					throw new Exception("Short read");
				}
				this.ReadHeader(this.buffer, ref this.booleansOffset);
				this.ReadNames(this.buffer, ref this.booleansOffset);
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00050910 File Offset: 0x0004EB10
		public TermInfoReader(string term, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = buffer;
			this.ReadHeader(buffer, ref this.booleansOffset);
			this.ReadNames(buffer, ref this.booleansOffset);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00050958 File Offset: 0x0004EB58
		private void ReadHeader(byte[] buffer, ref int position)
		{
			short @int = this.GetInt16(buffer, position);
			position += 2;
			if (@int != 282)
			{
				throw new Exception(string.Format("Magic number is wrong: {0}", @int));
			}
			this.GetInt16(buffer, position);
			position += 2;
			this.boolSize = this.GetInt16(buffer, position);
			position += 2;
			this.numSize = this.GetInt16(buffer, position);
			position += 2;
			this.strOffsets = this.GetInt16(buffer, position);
			position += 2;
			this.GetInt16(buffer, position);
			position += 2;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000509F8 File Offset: 0x0004EBF8
		private void ReadNames(byte[] buffer, ref int position)
		{
			string @string = this.GetString(buffer, position);
			position += @string.Length + 1;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00050A1C File Offset: 0x0004EC1C
		public bool Get(TermInfoBooleans boolean)
		{
			if (boolean < TermInfoBooleans.AutoLeftMargin || boolean >= TermInfoBooleans.Last || boolean >= (TermInfoBooleans)this.boolSize)
			{
				return false;
			}
			int num = this.booleansOffset;
			num = (int)(num + boolean);
			return this.buffer[num] != 0;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00050A64 File Offset: 0x0004EC64
		public int Get(TermInfoNumbers number)
		{
			if (number < TermInfoNumbers.Columns || number >= TermInfoNumbers.Last || number > (TermInfoNumbers)this.numSize)
			{
				return -1;
			}
			int num = this.booleansOffset + (int)this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num = (int)(num + number * TermInfoNumbers.Lines);
			return (int)this.GetInt16(this.buffer, num);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00050AC0 File Offset: 0x0004ECC0
		public string Get(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + (int)this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += (int)(this.numSize * 2);
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetString(this.buffer, num + (int)(this.strOffsets * 2) + @int);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00050B48 File Offset: 0x0004ED48
		public byte[] GetStringBytes(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + (int)this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += (int)(this.numSize * 2);
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetStringBytes(this.buffer, num + (int)(this.strOffsets * 2) + @int);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00050BD0 File Offset: 0x0004EDD0
		private short GetInt16(byte[] buffer, int offset)
		{
			int num = (int)buffer[offset];
			int num2 = (int)buffer[offset + 1];
			if (num == 255 && num2 == 255)
			{
				return -1;
			}
			return (short)(num + num2 * 256);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00050C0C File Offset: 0x0004EE0C
		private string GetString(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			return Encoding.ASCII.GetString(buffer, offset, num);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00050C40 File Offset: 0x0004EE40
		private byte[] GetStringBytes(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopyInternal(buffer, offset, array, 0, num);
			return array;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00050C7C File Offset: 0x0004EE7C
		internal static string Escape(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (char.IsControl(c))
				{
					stringBuilder.AppendFormat("\\x{0:X2}", (int)c);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000638 RID: 1592
		private short boolSize;

		// Token: 0x04000639 RID: 1593
		private short numSize;

		// Token: 0x0400063A RID: 1594
		private short strOffsets;

		// Token: 0x0400063B RID: 1595
		private byte[] buffer;

		// Token: 0x0400063C RID: 1596
		private int booleansOffset;
	}
}
