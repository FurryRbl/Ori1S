using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000131 RID: 305
	internal class NonBlockingStreamReader : TextReader
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x00046724 File Offset: 0x00044924
		public NonBlockingStreamReader(Stream stream, Encoding encoding)
		{
			int num = 1024;
			this.base_stream = stream;
			this.input_buffer = new byte[num];
			this.buffer_size = num;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			this.decoded_buffer = new char[encoding.GetMaxCharCount(num)];
			this.decoded_count = 0;
			this.pos = 0;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0004678C File Offset: 0x0004498C
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00046794 File Offset: 0x00044994
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000467A0 File Offset: 0x000449A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.base_stream != null)
			{
				this.base_stream.Close();
			}
			this.input_buffer = null;
			this.decoded_buffer = null;
			this.encoding = null;
			this.decoder = null;
			this.base_stream = null;
			base.Dispose(disposing);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000467F4 File Offset: 0x000449F4
		public void DiscardBufferedData()
		{
			this.pos = (this.decoded_count = 0);
			this.mayBlock = false;
			this.decoder.Reset();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00046824 File Offset: 0x00044A24
		private int ReadBuffer()
		{
			this.pos = 0;
			this.decoded_count = 0;
			int byteIndex = 0;
			for (;;)
			{
				int num = this.base_stream.Read(this.input_buffer, 0, this.buffer_size);
				if (num == 0)
				{
					break;
				}
				this.mayBlock = (num < this.buffer_size);
				this.decoded_count += this.decoder.GetChars(this.input_buffer, byteIndex, num, this.decoded_buffer, 0);
				byteIndex = 0;
				if (this.decoded_count != 0)
				{
					goto Block_2;
				}
			}
			return 0;
			Block_2:
			return this.decoded_count;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000468B0 File Offset: 0x00044AB0
		public override int Peek()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && (this.mayBlock || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this.decoded_buffer[this.pos];
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00046910 File Offset: 0x00044B10
		public override int Read()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return -1;
			}
			return (int)this.decoded_buffer[this.pos++];
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00046970 File Offset: 0x00044B70
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
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
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return (num <= 0) ? 0 : num;
			}
			int num2 = Math.Min(this.decoded_count - this.pos, count);
			Array.Copy(this.decoded_buffer, this.pos, dest_buffer, index, num2);
			this.pos += num2;
			index += num2;
			count -= num2;
			return num + num2;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00046A60 File Offset: 0x00044C60
		private int FindNextEOL()
		{
			while (this.pos < this.decoded_count)
			{
				char c = this.decoded_buffer[this.pos];
				if (c == '\n')
				{
					this.pos++;
					int num = (!this.foundCR) ? (this.pos - 1) : (this.pos - 2);
					if (num < 0)
					{
						num = 0;
					}
					this.foundCR = false;
					return num;
				}
				if (this.foundCR)
				{
					this.foundCR = false;
					return this.pos - 1;
				}
				this.foundCR = (c == '\r');
				this.pos++;
			}
			return -1;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00046B14 File Offset: 0x00044D14
		public override string ReadLine()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return null;
			}
			int num = this.pos;
			int num2 = this.FindNextEOL();
			if (num2 < this.decoded_count && num2 >= num)
			{
				return new string(this.decoded_buffer, num, num2 - num);
			}
			if (this.line_builder == null)
			{
				this.line_builder = new StringBuilder();
			}
			else
			{
				this.line_builder.Length = 0;
			}
			for (;;)
			{
				if (this.foundCR)
				{
					this.decoded_count--;
				}
				this.line_builder.Append(new string(this.decoded_buffer, num, this.decoded_count - num));
				if (this.ReadBuffer() == 0)
				{
					break;
				}
				num = this.pos;
				num2 = this.FindNextEOL();
				if (num2 < this.decoded_count && num2 >= num)
				{
					goto Block_11;
				}
			}
			if (this.line_builder.Capacity > 32768)
			{
				StringBuilder stringBuilder = this.line_builder;
				this.line_builder = null;
				return stringBuilder.ToString(0, stringBuilder.Length);
			}
			return this.line_builder.ToString(0, this.line_builder.Length);
			Block_11:
			this.line_builder.Append(new string(this.decoded_buffer, num, num2 - num));
			if (this.line_builder.Capacity > 32768)
			{
				StringBuilder stringBuilder2 = this.line_builder;
				this.line_builder = null;
				return stringBuilder2.ToString(0, stringBuilder2.Length);
			}
			return this.line_builder.ToString(0, this.line_builder.Length);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00046CC8 File Offset: 0x00044EC8
		public override string ReadToEnd()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.decoded_buffer.Length;
			char[] array = new char[num];
			int charCount;
			while ((charCount = this.Read(array, 0, num)) != 0)
			{
				stringBuilder.Append(array, 0, charCount);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000665 RID: 1637
		private const int DefaultBufferSize = 1024;

		// Token: 0x04000666 RID: 1638
		private const int DefaultFileBufferSize = 4096;

		// Token: 0x04000667 RID: 1639
		private const int MinimumBufferSize = 128;

		// Token: 0x04000668 RID: 1640
		private byte[] input_buffer;

		// Token: 0x04000669 RID: 1641
		private char[] decoded_buffer;

		// Token: 0x0400066A RID: 1642
		private int decoded_count;

		// Token: 0x0400066B RID: 1643
		private int pos;

		// Token: 0x0400066C RID: 1644
		private int buffer_size;

		// Token: 0x0400066D RID: 1645
		private Encoding encoding;

		// Token: 0x0400066E RID: 1646
		private Decoder decoder;

		// Token: 0x0400066F RID: 1647
		private Stream base_stream;

		// Token: 0x04000670 RID: 1648
		private bool mayBlock;

		// Token: 0x04000671 RID: 1649
		private StringBuilder line_builder;

		// Token: 0x04000672 RID: 1650
		private bool foundCR;
	}
}
