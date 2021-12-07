using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000132 RID: 306
	internal class XmlInputStream : Stream
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x00046D2C File Offset: 0x00044F2C
		public XmlInputStream(Stream stream)
		{
			this.Initialize(stream);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00046D3C File Offset: 0x00044F3C
		static XmlInputStream()
		{
			XmlInputStream.StrictUTF8 = new UTF8Encoding(false, true);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00046D5C File Offset: 0x00044F5C
		private static string GetStringFromBytes(byte[] bytes, int index, int count)
		{
			return Encoding.ASCII.GetString(bytes, index, count);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00046D6C File Offset: 0x00044F6C
		private void Initialize(Stream stream)
		{
			this.buffer = new byte[64];
			this.stream = stream;
			this.enc = XmlInputStream.StrictUTF8;
			this.bufLength = stream.Read(this.buffer, 0, this.buffer.Length);
			if (this.bufLength == -1 || this.bufLength == 0)
			{
				return;
			}
			int i = this.ReadByteSpecial();
			int num = i;
			if (num != 254)
			{
				if (num != 255)
				{
					if (num != 60)
					{
						if (num != 239)
						{
							this.bufPos = 0;
						}
						else
						{
							i = this.ReadByteSpecial();
							if (i == 187)
							{
								i = this.ReadByteSpecial();
								if (i != 191)
								{
									this.bufPos = 0;
								}
							}
							else
							{
								this.buffer[--this.bufPos] = 239;
							}
						}
					}
					else
					{
						if (this.bufLength >= 5 && XmlInputStream.GetStringFromBytes(this.buffer, 1, 4) == "?xml")
						{
							this.bufPos += 4;
							i = this.SkipWhitespace();
							if (i == 118)
							{
								while (i >= 0)
								{
									i = this.ReadByteSpecial();
									if (i == 48)
									{
										this.ReadByteSpecial();
										break;
									}
								}
								i = this.SkipWhitespace();
							}
							if (i == 101)
							{
								int num2 = this.bufLength - this.bufPos;
								if (num2 >= 7 && XmlInputStream.GetStringFromBytes(this.buffer, this.bufPos, 7) == "ncoding")
								{
									this.bufPos += 7;
									i = this.SkipWhitespace();
									if (i != 61)
									{
										throw XmlInputStream.encodingException;
									}
									i = this.SkipWhitespace();
									int num3 = i;
									StringBuilder stringBuilder = new StringBuilder();
									for (;;)
									{
										i = this.ReadByteSpecial();
										if (i == num3)
										{
											break;
										}
										if (i < 0)
										{
											goto Block_19;
										}
										stringBuilder.Append((char)i);
									}
									string text = stringBuilder.ToString();
									if (!XmlChar.IsValidIANAEncoding(text))
									{
										throw XmlInputStream.encodingException;
									}
									this.enc = Encoding.GetEncoding(text);
									goto IL_272;
									Block_19:
									throw XmlInputStream.encodingException;
								}
							}
						}
						IL_272:
						this.bufPos = 0;
					}
				}
				else
				{
					i = this.ReadByteSpecial();
					if (i == 254)
					{
						this.enc = Encoding.Unicode;
					}
					else
					{
						this.bufPos = 0;
					}
				}
			}
			else
			{
				i = this.ReadByteSpecial();
				if (i == 255)
				{
					this.enc = Encoding.BigEndianUnicode;
					return;
				}
				this.bufPos = 0;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00047004 File Offset: 0x00045204
		private int ReadByteSpecial()
		{
			if (this.bufLength > this.bufPos)
			{
				return (int)this.buffer[this.bufPos++];
			}
			byte[] dst = new byte[this.buffer.Length * 2];
			Buffer.BlockCopy(this.buffer, 0, dst, 0, this.bufLength);
			int num = this.stream.Read(dst, this.bufLength, this.buffer.Length);
			if (num == -1 || num == 0)
			{
				return -1;
			}
			this.bufLength += num;
			this.buffer = dst;
			return (int)this.buffer[this.bufPos++];
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000470B8 File Offset: 0x000452B8
		private int SkipWhitespace()
		{
			int num;
			for (;;)
			{
				num = this.ReadByteSpecial();
				char c = (char)num;
				switch (c)
				{
				case '\t':
					break;
				case '\n':
					break;
				default:
					if (c != ' ')
					{
						return num;
					}
					break;
				case '\r':
					break;
				}
			}
			return num;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00047114 File Offset: 0x00045314
		public Encoding ActualEncoding
		{
			get
			{
				return this.enc;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0004711C File Offset: 0x0004531C
		public override bool CanRead
		{
			get
			{
				return this.bufLength > this.bufPos || this.stream.CanRead;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0004713C File Offset: 0x0004533C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00047140 File Offset: 0x00045340
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00047144 File Offset: 0x00045344
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00047154 File Offset: 0x00045354
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00047174 File Offset: 0x00045374
		public override long Position
		{
			get
			{
				return this.stream.Position - (long)this.bufLength + (long)this.bufPos;
			}
			set
			{
				if (value < (long)this.bufLength)
				{
					this.bufPos = (int)value;
				}
				else
				{
					this.stream.Position = value - (long)this.bufLength;
				}
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000471B0 File Offset: 0x000453B0
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000471C0 File Offset: 0x000453C0
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000471D0 File Offset: 0x000453D0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result;
			if (count <= this.bufLength - this.bufPos)
			{
				Buffer.BlockCopy(this.buffer, this.bufPos, buffer, offset, count);
				this.bufPos += count;
				result = count;
			}
			else
			{
				int num = this.bufLength - this.bufPos;
				if (this.bufLength > this.bufPos)
				{
					Buffer.BlockCopy(this.buffer, this.bufPos, buffer, offset, num);
					this.bufPos += num;
				}
				result = num + this.stream.Read(buffer, offset + num, count - num);
			}
			return result;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00047270 File Offset: 0x00045470
		public override int ReadByte()
		{
			if (this.bufLength > this.bufPos)
			{
				return (int)this.buffer[this.bufPos++];
			}
			return this.stream.ReadByte();
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000472B4 File Offset: 0x000454B4
		public override long Seek(long offset, SeekOrigin origin)
		{
			int num = this.bufLength - this.bufPos;
			if (origin != SeekOrigin.Current)
			{
				return this.stream.Seek(offset, origin);
			}
			if (offset < (long)num)
			{
				return (long)this.buffer[(int)(checked((IntPtr)(unchecked((long)this.bufPos + offset))))];
			}
			return this.stream.Seek(offset - (long)num, origin);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00047310 File Offset: 0x00045510
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00047320 File Offset: 0x00045520
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000673 RID: 1651
		public static readonly Encoding StrictUTF8;

		// Token: 0x04000674 RID: 1652
		private Encoding enc;

		// Token: 0x04000675 RID: 1653
		private Stream stream;

		// Token: 0x04000676 RID: 1654
		private byte[] buffer;

		// Token: 0x04000677 RID: 1655
		private int bufLength;

		// Token: 0x04000678 RID: 1656
		private int bufPos;

		// Token: 0x04000679 RID: 1657
		private static XmlException encodingException = new XmlException("invalid encoding specification.");
	}
}
