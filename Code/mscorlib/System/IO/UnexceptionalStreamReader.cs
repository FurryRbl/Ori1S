using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x02000261 RID: 609
	internal class UnexceptionalStreamReader : StreamReader
	{
		// Token: 0x06001FB9 RID: 8121 RVA: 0x00074F64 File Offset: 0x00073164
		public UnexceptionalStreamReader(Stream stream) : base(stream)
		{
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00074F70 File Offset: 0x00073170
		public UnexceptionalStreamReader(Stream stream, bool detect_encoding_from_bytemarks) : base(stream, detect_encoding_from_bytemarks)
		{
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00074F7C File Offset: 0x0007317C
		public UnexceptionalStreamReader(Stream stream, Encoding encoding) : base(stream, encoding)
		{
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00074F88 File Offset: 0x00073188
		public UnexceptionalStreamReader(Stream stream, Encoding encoding, bool detect_encoding_from_bytemarks) : base(stream, encoding, detect_encoding_from_bytemarks)
		{
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00074F94 File Offset: 0x00073194
		public UnexceptionalStreamReader(Stream stream, Encoding encoding, bool detect_encoding_from_bytemarks, int buffer_size) : base(stream, encoding, detect_encoding_from_bytemarks, buffer_size)
		{
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00074FA4 File Offset: 0x000731A4
		public UnexceptionalStreamReader(string path) : base(path)
		{
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00074FB0 File Offset: 0x000731B0
		public UnexceptionalStreamReader(string path, bool detect_encoding_from_bytemarks) : base(path, detect_encoding_from_bytemarks)
		{
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00074FBC File Offset: 0x000731BC
		public UnexceptionalStreamReader(string path, Encoding encoding) : base(path, encoding)
		{
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00074FC8 File Offset: 0x000731C8
		public UnexceptionalStreamReader(string path, Encoding encoding, bool detect_encoding_from_bytemarks) : base(path, encoding, detect_encoding_from_bytemarks)
		{
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00074FD4 File Offset: 0x000731D4
		public UnexceptionalStreamReader(string path, Encoding encoding, bool detect_encoding_from_bytemarks, int buffer_size) : base(path, encoding, detect_encoding_from_bytemarks, buffer_size)
		{
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00074FE4 File Offset: 0x000731E4
		static UnexceptionalStreamReader()
		{
			string newLine = Environment.NewLine;
			if (newLine.Length == 1)
			{
				UnexceptionalStreamReader.newlineChar = newLine[0];
			}
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00075024 File Offset: 0x00073224
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00075068 File Offset: 0x00073268
		public override int Read()
		{
			try
			{
				return base.Read();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000750AC File Offset: 0x000732AC
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			if (dest_buffer == null)
			{
				throw new ArgumentNullException("dest_buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest_buffer.Length - count)
			{
				throw new ArgumentException("index + count > dest_buffer.Length");
			}
			int num = 0;
			char c = UnexceptionalStreamReader.newlineChar;
			try
			{
				while (count > 0)
				{
					int num2 = base.Read();
					if (num2 < 0)
					{
						break;
					}
					num++;
					count--;
					dest_buffer[index] = (char)num2;
					if (c != '\0')
					{
						if ((char)num2 == c)
						{
							return num;
						}
					}
					else if (this.CheckEOL((char)num2))
					{
						return num;
					}
					index++;
				}
			}
			catch (IOException)
			{
			}
			return num;
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000751A0 File Offset: 0x000733A0
		private bool CheckEOL(char current)
		{
			int i = 0;
			while (i < UnexceptionalStreamReader.newline.Length)
			{
				if (!UnexceptionalStreamReader.newline[i])
				{
					if (current == Environment.NewLine[i])
					{
						UnexceptionalStreamReader.newline[i] = true;
						return i == UnexceptionalStreamReader.newline.Length - 1;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < UnexceptionalStreamReader.newline.Length; j++)
			{
				UnexceptionalStreamReader.newline[j] = false;
			}
			return false;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00075220 File Offset: 0x00073420
		public override string ReadLine()
		{
			try
			{
				return base.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00075264 File Offset: 0x00073464
		public override string ReadToEnd()
		{
			try
			{
				return base.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04000BD1 RID: 3025
		private static bool[] newline = new bool[Environment.NewLine.Length];

		// Token: 0x04000BD2 RID: 3026
		private static char newlineChar;
	}
}
