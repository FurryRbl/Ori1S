using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200006A RID: 106
	internal class XmlSignatureStreamReader : TextReader
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		public XmlSignatureStreamReader(TextReader input)
		{
			this.source = input;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E9CC File Offset: 0x0000CBCC
		public override void Close()
		{
			this.source.Close();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public override int Peek()
		{
			if (this.source.Peek() == -1)
			{
				return -1;
			}
			if (this.cache != -2147483648)
			{
				return this.cache;
			}
			this.cache = this.source.Read();
			if (this.cache != 13)
			{
				return this.cache;
			}
			if (this.source.Peek() != 10)
			{
				return 13;
			}
			this.cache = int.MinValue;
			return 10;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public override int Read()
		{
			if (this.cache != -2147483648)
			{
				int result = this.cache;
				this.cache = int.MinValue;
				return result;
			}
			int num = this.source.Read();
			if (num != 13)
			{
				return num;
			}
			this.cache = this.source.Read();
			if (this.cache != 10)
			{
				return 13;
			}
			this.cache = int.MinValue;
			return 10;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		public override int ReadBlock([In] [Out] char[] buffer, int index, int count)
		{
			char[] array = new char[count];
			this.source.ReadBlock(array, 0, count);
			int i = index;
			int j = 0;
			while (j < count)
			{
				if (array[j] == '\r')
				{
					if (++j < array.Length && array[j] == '\n')
					{
						buffer[i] = array[j++];
					}
					else
					{
						buffer[i] = '\r';
					}
				}
				else
				{
					buffer[i] = array[j];
				}
				i++;
			}
			while (i < count)
			{
				int num = this.Read();
				if (num < 0)
				{
					break;
				}
				buffer[i++] = (char)num;
			}
			return i;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public override string ReadLine()
		{
			return this.source.ReadLine();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public override string ReadToEnd()
		{
			return this.source.ReadToEnd().Replace("\r\n", "\n");
		}

		// Token: 0x0400019F RID: 415
		private TextReader source;

		// Token: 0x040001A0 RID: 416
		private int cache = int.MinValue;
	}
}
