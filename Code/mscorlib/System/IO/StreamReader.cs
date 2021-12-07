using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextReader" /> that reads characters from a byte stream in a particular encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000256 RID: 598
	[ComVisible(true)]
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x06001EF2 RID: 7922 RVA: 0x00072634 File Offset: 0x00070834
		internal StreamReader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001EF3 RID: 7923 RVA: 0x0007263C File Offset: 0x0007083C
		public StreamReader(Stream stream) : this(stream, Encoding.UTF8Unmarked, true, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001EF4 RID: 7924 RVA: 0x00072650 File Offset: 0x00070850
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks) : this(stream, Encoding.UTF8Unmarked, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06001EF5 RID: 7925 RVA: 0x00072664 File Offset: 0x00070864
		public StreamReader(Stream stream, Encoding encoding) : this(stream, encoding, true, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06001EF6 RID: 7926 RVA: 0x00072674 File Offset: 0x00070874
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(stream, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <param name="bufferSize">The minimum buffer size. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x00072684 File Offset: 0x00070884
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			this.Initialize(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x00072698 File Offset: 0x00070898
		public StreamReader(string path) : this(path, Encoding.UTF8Unmarked, true, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06001EF9 RID: 7929 RVA: 0x000726AC File Offset: 0x000708AC
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks) : this(path, Encoding.UTF8Unmarked, detectEncodingFromByteOrderMarks, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06001EFA RID: 7930 RVA: 0x000726C0 File Offset: 0x000708C0
		public StreamReader(string path, Encoding encoding) : this(path, encoding, true, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06001EFB RID: 7931 RVA: 0x000726D0 File Offset: 0x000708D0
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(path, encoding, detectEncodingFromByteOrderMarks, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is less than or equal to zero. </exception>
		// Token: 0x06001EFC RID: 7932 RVA: 0x000726E0 File Offset: 0x000708E0
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (string.Empty == path)
			{
				throw new ArgumentException("Empty path not allowed");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("path contains invalid characters");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "The minimum size of the buffer must be positive");
			}
			Stream stream = File.OpenRead(path);
			this.Initialize(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00072784 File Offset: 0x00070984
		internal void Initialize(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Cannot read stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "The minimum size of the buffer must be positive");
			}
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.base_stream = stream;
			this.input_buffer = new byte[bufferSize];
			this.buffer_size = bufferSize;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			byte[] preamble = encoding.GetPreamble();
			this.do_checks = ((!detectEncodingFromByteOrderMarks) ? 0 : 1);
			this.do_checks += ((preamble.Length != 0) ? 2 : 0);
			this.decoded_buffer = new char[encoding.GetMaxCharCount(bufferSize) + 1];
			this.decoded_count = 0;
			this.pos = 0;
		}

		/// <summary>Returns the underlying stream.</summary>
		/// <returns>The underlying stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0007287C File Offset: 0x00070A7C
		public virtual Stream BaseStream
		{
			get
			{
				return this.base_stream;
			}
		}

		/// <summary>Gets the current character encoding that the current <see cref="T:System.IO.StreamReader" /> object is using.</summary>
		/// <returns>The current character encoding used by the current reader. The value can be different after the first call to any <see cref="Overload:System.IO.StreamReader.Read" /> method of <see cref="T:System.IO.StreamReader" />, since encoding autodetection is not done until the first call to a <see cref="Overload:System.IO.StreamReader.Read" /> method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00072884 File Offset: 0x00070A84
		public virtual Encoding CurrentEncoding
		{
			get
			{
				if (this.encoding == null)
				{
					throw new Exception();
				}
				return this.encoding;
			}
		}

		/// <summary>Gets a value that indicates whether the current stream position is at the end of the stream.</summary>
		/// <returns>true if the current stream position is at the end of the stream; otherwise false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream has been disposed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x000728A0 File Offset: 0x00070AA0
		public bool EndOfStream
		{
			get
			{
				return this.Peek() < 0;
			}
		}

		/// <summary>Closes the <see cref="T:System.IO.StreamReader" /> object and the underlying stream, and releases any system resources associated with the reader.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F02 RID: 7938 RVA: 0x000728AC File Offset: 0x00070AAC
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Closes the underlying stream, releases the unmanaged resources used by the <see cref="T:System.IO.StreamReader" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F03 RID: 7939 RVA: 0x000728B8 File Offset: 0x00070AB8
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

		// Token: 0x06001F04 RID: 7940 RVA: 0x0007290C File Offset: 0x00070B0C
		private int DoChecks(int count)
		{
			if ((this.do_checks & 2) == 2)
			{
				byte[] preamble = this.encoding.GetPreamble();
				int num = preamble.Length;
				if (count >= num)
				{
					int i;
					for (i = 0; i < num; i++)
					{
						if (this.input_buffer[i] != preamble[i])
						{
							break;
						}
					}
					if (i == num)
					{
						return i;
					}
				}
			}
			if ((this.do_checks & 1) == 1)
			{
				if (count < 2)
				{
					return 0;
				}
				if (this.input_buffer[0] == 254 && this.input_buffer[1] == 255)
				{
					this.encoding = Encoding.BigEndianUnicode;
					return 2;
				}
				if (count < 3)
				{
					return 0;
				}
				if (this.input_buffer[0] == 239 && this.input_buffer[1] == 187 && this.input_buffer[2] == 191)
				{
					this.encoding = Encoding.UTF8Unmarked;
					return 3;
				}
				if (count < 4)
				{
					if (this.input_buffer[0] == 255 && this.input_buffer[1] == 254 && this.input_buffer[2] != 0)
					{
						this.encoding = Encoding.Unicode;
						return 2;
					}
					return 0;
				}
				else
				{
					if (this.input_buffer[0] == 0 && this.input_buffer[1] == 0 && this.input_buffer[2] == 254 && this.input_buffer[3] == 255)
					{
						this.encoding = Encoding.BigEndianUTF32;
						return 4;
					}
					if (this.input_buffer[0] == 255 && this.input_buffer[1] == 254)
					{
						if (this.input_buffer[2] == 0 && this.input_buffer[3] == 0)
						{
							this.encoding = Encoding.UTF32;
							return 4;
						}
						this.encoding = Encoding.Unicode;
						return 2;
					}
				}
			}
			return 0;
		}

		/// <summary>Clears the internal buffer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F05 RID: 7941 RVA: 0x00072AF0 File Offset: 0x00070CF0
		public void DiscardBufferedData()
		{
			this.pos = (this.decoded_count = 0);
			this.mayBlock = false;
			this.decoder = this.encoding.GetDecoder();
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00072B28 File Offset: 0x00070D28
		private int ReadBuffer()
		{
			this.pos = 0;
			this.decoded_count = 0;
			int num = 0;
			for (;;)
			{
				int num2 = this.base_stream.Read(this.input_buffer, 0, this.buffer_size);
				if (num2 <= 0)
				{
					break;
				}
				this.mayBlock = (num2 < this.buffer_size);
				if (this.do_checks > 0)
				{
					Encoding encoding = this.encoding;
					num = this.DoChecks(num2);
					if (encoding != this.encoding)
					{
						int num3 = encoding.GetMaxCharCount(this.buffer_size) + 1;
						int num4 = this.encoding.GetMaxCharCount(this.buffer_size) + 1;
						if (num3 != num4)
						{
							this.decoded_buffer = new char[num4];
						}
						this.decoder = this.encoding.GetDecoder();
					}
					this.do_checks = 0;
					num2 -= num;
				}
				this.decoded_count += this.decoder.GetChars(this.input_buffer, num, num2, this.decoded_buffer, 0);
				num = 0;
				if (this.decoded_count != 0)
				{
					goto Block_5;
				}
			}
			return 0;
			Block_5:
			return this.decoded_count;
		}

		/// <summary>Returns the next available character but does not consume it.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if there are no characters to be read or if the stream does not support seeking.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F07 RID: 7943 RVA: 0x00072C30 File Offset: 0x00070E30
		public override int Peek()
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
			{
				return -1;
			}
			return (int)this.decoded_buffer[this.pos];
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00072C84 File Offset: 0x00070E84
		internal bool DataAvailable()
		{
			return this.pos < this.decoded_count;
		}

		/// <summary>Reads the next character from the input stream and advances the character position by one character.</summary>
		/// <returns>The next character from the input stream represented as an <see cref="T:System.Int32" /> object, or -1 if no more characters are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F09 RID: 7945 RVA: 0x00072C94 File Offset: 0x00070E94
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

		/// <summary>Reads a specified maximum number of characters from the current stream into a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read, or 0 if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="count" /> parameter, depending on whether the data is available within the stream.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index + count - 1" />) replaced by the characters read from the current source. </param>
		/// <param name="index">The index of <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The maximum number of characters to read. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F0A RID: 7946 RVA: 0x00072CF4 File Offset: 0x00070EF4
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (this.base_stream == null)
			{
				throw new ObjectDisposedException("StreamReader", "Cannot read from a closed StreamReader");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > buffer.Length - count)
			{
				throw new ArgumentException("index + count > buffer.Length");
			}
			int num = 0;
			while (count > 0)
			{
				if (this.pos >= this.decoded_count && this.ReadBuffer() == 0)
				{
					return (num <= 0) ? 0 : num;
				}
				int num2 = Math.Min(this.decoded_count - this.pos, count);
				Array.Copy(this.decoded_buffer, this.pos, buffer, index, num2);
				this.pos += num2;
				index += num2;
				count -= num2;
				num += num2;
				if (this.mayBlock)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00072E00 File Offset: 0x00071000
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
					if (this.pos == 0)
					{
						return -2;
					}
					return this.pos - 1;
				}
				else
				{
					this.foundCR = (c == '\r');
					this.pos++;
				}
			}
			return -1;
		}

		/// <summary>Reads a line of characters from the current stream and returns the data as a string.</summary>
		/// <returns>The next line from the input stream, or null if the end of the input stream is reached.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F0C RID: 7948 RVA: 0x00072EC0 File Offset: 0x000710C0
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
			if (num2 == -2)
			{
				return this.line_builder.ToString(0, this.line_builder.Length);
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
				this.line_builder.Append(this.decoded_buffer, num, this.decoded_count - num);
				if (this.ReadBuffer() == 0)
				{
					break;
				}
				num = this.pos;
				num2 = this.FindNextEOL();
				if (num2 < this.decoded_count && num2 >= num)
				{
					goto Block_12;
				}
				if (num2 == -2)
				{
					goto Block_14;
				}
			}
			if (this.line_builder.Capacity > 32768)
			{
				StringBuilder stringBuilder = this.line_builder;
				this.line_builder = null;
				return stringBuilder.ToString(0, stringBuilder.Length);
			}
			return this.line_builder.ToString(0, this.line_builder.Length);
			Block_12:
			this.line_builder.Append(this.decoded_buffer, num, num2 - num);
			if (this.line_builder.Capacity > 32768)
			{
				StringBuilder stringBuilder2 = this.line_builder;
				this.line_builder = null;
				return stringBuilder2.ToString(0, stringBuilder2.Length);
			}
			return this.line_builder.ToString(0, this.line_builder.Length);
			Block_14:
			return this.line_builder.ToString(0, this.line_builder.Length);
		}

		/// <summary>Reads the stream from the current position to the end of the stream.</summary>
		/// <returns>The rest of the stream as a string, from the current position to the end. If the current position is at the end of the stream, returns an empty string ("").</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F0D RID: 7949 RVA: 0x000730AC File Offset: 0x000712AC
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
			while ((charCount = this.Read(array, 0, num)) > 0)
			{
				stringBuilder.Append(array, 0, charCount);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BA8 RID: 2984
		private const int DefaultBufferSize = 1024;

		// Token: 0x04000BA9 RID: 2985
		private const int DefaultFileBufferSize = 4096;

		// Token: 0x04000BAA RID: 2986
		private const int MinimumBufferSize = 128;

		// Token: 0x04000BAB RID: 2987
		private byte[] input_buffer;

		// Token: 0x04000BAC RID: 2988
		private char[] decoded_buffer;

		// Token: 0x04000BAD RID: 2989
		private int decoded_count;

		// Token: 0x04000BAE RID: 2990
		private int pos;

		// Token: 0x04000BAF RID: 2991
		private int buffer_size;

		// Token: 0x04000BB0 RID: 2992
		private int do_checks;

		// Token: 0x04000BB1 RID: 2993
		private Encoding encoding;

		// Token: 0x04000BB2 RID: 2994
		private Decoder decoder;

		// Token: 0x04000BB3 RID: 2995
		private Stream base_stream;

		// Token: 0x04000BB4 RID: 2996
		private bool mayBlock;

		// Token: 0x04000BB5 RID: 2997
		private StringBuilder line_builder;

		/// <summary>A <see cref="T:System.IO.StreamReader" /> object around an empty stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BB6 RID: 2998
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x04000BB7 RID: 2999
		private bool foundCR;

		// Token: 0x02000257 RID: 599
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06001F0F RID: 7951 RVA: 0x00073118 File Offset: 0x00071318
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06001F10 RID: 7952 RVA: 0x0007311C File Offset: 0x0007131C
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06001F11 RID: 7953 RVA: 0x00073120 File Offset: 0x00071320
			public override int Read([In] [Out] char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06001F12 RID: 7954 RVA: 0x00073124 File Offset: 0x00071324
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06001F13 RID: 7955 RVA: 0x00073128 File Offset: 0x00071328
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001F14 RID: 7956 RVA: 0x00073130 File Offset: 0x00071330
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00073138 File Offset: 0x00071338
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}
		}
	}
}
