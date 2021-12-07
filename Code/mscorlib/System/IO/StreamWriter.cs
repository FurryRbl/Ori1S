using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing characters to a stream in a particular encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000258 RID: 600
	[ComVisible(true)]
	[Serializable]
	public class StreamWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream, using UTF-8 encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001F16 RID: 7958 RVA: 0x00073140 File Offset: 0x00071340
		public StreamWriter(Stream stream) : this(stream, Encoding.UTF8Unmarked, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream, using the specified encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06001F17 RID: 7959 RVA: 0x00073154 File Offset: 0x00071354
		public StreamWriter(Stream stream, Encoding encoding) : this(stream, encoding, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream, using the specified encoding and buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="bufferSize">Sets the buffer size. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06001F18 RID: 7960 RVA: 0x00073164 File Offset: 0x00071364
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Can not write to stream");
			}
			this.internalStream = stream;
			this.Initialize(encoding, bufferSize);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the default encoding and buffer size.</summary>
		/// <param name="path">The complete file path to write to. <paramref name="path" /> can be a file name. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string(""). -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001F19 RID: 7961 RVA: 0x000731D0 File Offset: 0x000713D0
		public StreamWriter(string path) : this(path, false, Encoding.UTF8Unmarked, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the default encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists and <paramref name="append" /> is false, the file is overwritten. If the file exists and <paramref name="append" /> is true, the data is appended to the file. Otherwise, a new file is created. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty. -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001F1A RID: 7962 RVA: 0x000731E4 File Offset: 0x000713E4
		public StreamWriter(string path, bool append) : this(path, append, Encoding.UTF8Unmarked, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the specified encoding and default buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists and <paramref name="append" /> is false, the file is overwritten. If the file exists and <paramref name="append" /> is true, the data is appended to the file. Otherwise, a new file is created. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty. -or-<paramref name="path" /> contains the name of a system device (com1, com2, etc).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001F1B RID: 7963 RVA: 0x000731F8 File Offset: 0x000713F8
		public StreamWriter(string path, bool append, Encoding encoding) : this(path, append, encoding, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the specified encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists and <paramref name="append" /> is false, the file is overwritten. If the file exists and <paramref name="append" /> is true, the data is appended to the file. Otherwise, a new file is created. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="bufferSize">Sets the buffer size. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). -or-<paramref name="path" /> contains the name of a system device (com1, com2, etc).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06001F1C RID: 7964 RVA: 0x00073208 File Offset: 0x00071408
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			FileMode mode;
			if (append)
			{
				mode = FileMode.Append;
			}
			else
			{
				mode = FileMode.Create;
			}
			this.internalStream = new FileStream(path, mode, FileAccess.Write, FileShare.Read);
			if (append)
			{
				this.internalStream.Position = this.internalStream.Length;
			}
			else
			{
				this.internalStream.SetLength(0L);
			}
			this.Initialize(encoding, bufferSize);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000732AC File Offset: 0x000714AC
		internal void Initialize(Encoding encoding, int bufferSize)
		{
			this.internalEncoding = encoding;
			this.decode_pos = (this.byte_pos = 0);
			int num = Math.Max(bufferSize, 256);
			this.decode_buf = new char[num];
			this.byte_buf = new byte[encoding.GetMaxByteCount(num)];
			if (this.internalStream.CanSeek && this.internalStream.Position > 0L)
			{
				this.preamble_done = true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.IO.StreamWriter" /> will flush its buffer to the underlying stream after every call to <see cref="M:System.IO.StreamWriter.Write(System.Char)" />.</summary>
		/// <returns>true to force <see cref="T:System.IO.StreamWriter" /> to flush its buffer; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x00073324 File Offset: 0x00071524
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x0007332C File Offset: 0x0007152C
		public virtual bool AutoFlush
		{
			get
			{
				return this.iflush;
			}
			set
			{
				this.iflush = value;
				if (this.iflush)
				{
					this.Flush();
				}
			}
		}

		/// <summary>Gets the underlying stream that interfaces with a backing store.</summary>
		/// <returns>The stream this StreamWriter is writing to.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00073348 File Offset: 0x00071548
		public virtual Stream BaseStream
		{
			get
			{
				return this.internalStream;
			}
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> specified in the constructor for the current instance, or <see cref="T:System.Text.UTF8Encoding" /> if an encoding was not specified.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00073350 File Offset: 0x00071550
		public override Encoding Encoding
		{
			get
			{
				return this.internalEncoding;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StreamWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		// Token: 0x06001F23 RID: 7971 RVA: 0x00073358 File Offset: 0x00071558
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (!this.DisposedAlready && disposing && this.internalStream != null)
			{
				try
				{
					this.Flush();
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				this.DisposedAlready = true;
				try
				{
					this.internalStream.Close();
				}
				catch (Exception ex3)
				{
					if (ex == null)
					{
						ex = ex3;
					}
				}
			}
			this.internalStream = null;
			this.byte_buf = null;
			this.internalEncoding = null;
			this.decode_buf = null;
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current writer is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F24 RID: 7972 RVA: 0x00073414 File Offset: 0x00071614
		public override void Flush()
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			this.Decode();
			if (this.byte_pos > 0)
			{
				this.FlushBytes();
				this.internalStream.Flush();
			}
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0007345C File Offset: 0x0007165C
		private void FlushBytes()
		{
			if (!this.preamble_done && this.byte_pos > 0)
			{
				byte[] preamble = this.internalEncoding.GetPreamble();
				if (preamble.Length > 0)
				{
					this.internalStream.Write(preamble, 0, preamble.Length);
				}
				this.preamble_done = true;
			}
			this.internalStream.Write(this.byte_buf, 0, this.byte_pos);
			this.byte_pos = 0;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000734CC File Offset: 0x000716CC
		private void Decode()
		{
			if (this.byte_pos > 0)
			{
				this.FlushBytes();
			}
			if (this.decode_pos > 0)
			{
				int bytes = this.internalEncoding.GetBytes(this.decode_buf, 0, this.decode_pos, this.byte_buf, this.byte_pos);
				this.byte_pos += bytes;
				this.decode_pos = 0;
			}
		}

		/// <summary>Writes a subarray of characters to the stream.</summary>
		/// <param name="buffer">A character array containing the data to write. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The number of characters to read from <paramref name="buffer" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F27 RID: 7975 RVA: 0x00073534 File Offset: 0x00071734
		public override void Write(char[] buffer, int index, int count)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
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
			this.LowLevelWrite(buffer, index, count);
			if (this.iflush)
			{
				this.Flush();
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000735C8 File Offset: 0x000717C8
		private void LowLevelWrite(char[] buffer, int index, int count)
		{
			while (count > 0)
			{
				int num = this.decode_buf.Length - this.decode_pos;
				if (num == 0)
				{
					this.Decode();
					num = this.decode_buf.Length;
				}
				if (num > count)
				{
					num = count;
				}
				Buffer.BlockCopy(buffer, index * 2, this.decode_buf, this.decode_pos * 2, num * 2);
				count -= num;
				index += num;
				this.decode_pos += num;
			}
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00073644 File Offset: 0x00071844
		private void LowLevelWrite(string s)
		{
			int i = s.Length;
			int num = 0;
			while (i > 0)
			{
				int num2 = this.decode_buf.Length - this.decode_pos;
				if (num2 == 0)
				{
					this.Decode();
					num2 = this.decode_buf.Length;
				}
				if (num2 > i)
				{
					num2 = i;
				}
				for (int j = 0; j < num2; j++)
				{
					this.decode_buf[j + this.decode_pos] = s[j + num];
				}
				i -= num2;
				num += num2;
				this.decode_pos += num2;
			}
		}

		/// <summary>Writes a character to the stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F2A RID: 7978 RVA: 0x000736D4 File Offset: 0x000718D4
		public override void Write(char value)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (this.decode_pos >= this.decode_buf.Length)
			{
				this.Decode();
			}
			this.decode_buf[this.decode_pos++] = value;
			if (this.iflush)
			{
				this.Flush();
			}
		}

		/// <summary>Writes a character array to the stream.</summary>
		/// <param name="buffer">A character array containing the data to write. If <paramref name="buffer" /> is null, nothing is written. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F2B RID: 7979 RVA: 0x0007373C File Offset: 0x0007193C
		public override void Write(char[] buffer)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (buffer != null)
			{
				this.LowLevelWrite(buffer, 0, buffer.Length);
			}
			if (this.iflush)
			{
				this.Flush();
			}
		}

		/// <summary>Writes a string to the stream.</summary>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is null, nothing is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F2C RID: 7980 RVA: 0x00073784 File Offset: 0x00071984
		public override void Write(string value)
		{
			if (this.DisposedAlready)
			{
				throw new ObjectDisposedException("StreamWriter");
			}
			if (value != null)
			{
				this.LowLevelWrite(value);
			}
			if (this.iflush)
			{
				this.Flush();
			}
		}

		/// <summary>Closes the current StreamWriter object and the underlying stream.</summary>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F2D RID: 7981 RVA: 0x000737C8 File Offset: 0x000719C8
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Frees the resources of the current <see cref="T:System.IO.StreamWriter" /> before it is reclaimed by the garbage collector.</summary>
		// Token: 0x06001F2E RID: 7982 RVA: 0x000737D4 File Offset: 0x000719D4
		~StreamWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x04000BB8 RID: 3000
		private const int DefaultBufferSize = 1024;

		// Token: 0x04000BB9 RID: 3001
		private const int DefaultFileBufferSize = 4096;

		// Token: 0x04000BBA RID: 3002
		private const int MinimumBufferSize = 256;

		// Token: 0x04000BBB RID: 3003
		private Encoding internalEncoding;

		// Token: 0x04000BBC RID: 3004
		private Stream internalStream;

		// Token: 0x04000BBD RID: 3005
		private bool iflush;

		// Token: 0x04000BBE RID: 3006
		private byte[] byte_buf;

		// Token: 0x04000BBF RID: 3007
		private int byte_pos;

		// Token: 0x04000BC0 RID: 3008
		private char[] decode_buf;

		// Token: 0x04000BC1 RID: 3009
		private int decode_pos;

		// Token: 0x04000BC2 RID: 3010
		private bool DisposedAlready;

		// Token: 0x04000BC3 RID: 3011
		private bool preamble_done;

		/// <summary>Provides a StreamWriter with no backing store that can be written to, but not read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BC4 RID: 3012
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, Encoding.UTF8Unmarked, 1);
	}
}
