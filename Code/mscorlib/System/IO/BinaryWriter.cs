using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security;

namespace System.IO
{
	/// <summary>Writes primitive types in binary to a stream and supports writing strings in a specific encoding.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	[ComVisible(true)]
	[Serializable]
	public class BinaryWriter : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class that writes to a stream.</summary>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006A9BC File Offset: 0x00068BBC
		protected BinaryWriter() : this(Stream.Null, Encoding.UTF8UnmarkedUnsafe)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the supplied stream and using UTF-8 as the encoding for strings.</summary>
		/// <param name="output">The output stream. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing, or the stream is already closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> is null. </exception>
		// Token: 0x06001CE7 RID: 7399 RVA: 0x0006A9D0 File Offset: 0x00068BD0
		public BinaryWriter(Stream output) : this(output, Encoding.UTF8UnmarkedUnsafe)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the supplied stream and a specific character encoding.</summary>
		/// <param name="output">The supplied stream. </param>
		/// <param name="encoding">The character encoding. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing, or the stream is already closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06001CE8 RID: 7400 RVA: 0x0006A9E0 File Offset: 0x00068BE0
		public BinaryWriter(Stream output, Encoding encoding)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!output.CanWrite)
			{
				throw new ArgumentException(Locale.GetText("Stream does not support writing or already closed."));
			}
			this.OutStream = output;
			this.m_encoding = encoding;
			this.buffer = new byte[16];
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.BinaryWriter" /> and optionally releases the managed resources.</summary>
		// Token: 0x06001CEA RID: 7402 RVA: 0x0006AA58 File Offset: 0x00068C58
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Gets the underlying stream of the <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <returns>The underlying stream associated with the BinaryWriter.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0006AA64 File Offset: 0x00068C64
		public virtual Stream BaseStream
		{
			get
			{
				return this.OutStream;
			}
		}

		/// <summary>Closes the current <see cref="T:System.IO.BinaryWriter" /> and the underlying stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CEC RID: 7404 RVA: 0x0006AA6C File Offset: 0x00068C6C
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.BinaryWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001CED RID: 7405 RVA: 0x0006AA78 File Offset: 0x00068C78
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.OutStream != null)
			{
				this.OutStream.Close();
			}
			this.buffer = null;
			this.m_encoding = null;
			this.disposed = true;
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CEE RID: 7406 RVA: 0x0006AAAC File Offset: 0x00068CAC
		public virtual void Flush()
		{
			this.OutStream.Flush();
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <returns>The position with the current stream.</returns>
		/// <param name="offset">A byte offset relative to <paramref name="origin" />. </param>
		/// <param name="origin">A field of <see cref="T:System.IO.SeekOrigin" /> indicating the reference point from which the new position is to be obtained. </param>
		/// <exception cref="T:System.IO.IOException">The file pointer was moved to an invalid location. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.IO.SeekOrigin" /> value is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CEF RID: 7407 RVA: 0x0006AABC File Offset: 0x00068CBC
		public virtual long Seek(int offset, SeekOrigin origin)
		{
			return this.OutStream.Seek((long)offset, origin);
		}

		/// <summary>Writes a one-byte Boolean value to the current stream, with 0 representing false and 1 representing true.</summary>
		/// <param name="value">The Boolean value to write (0 or 1). </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF0 RID: 7408 RVA: 0x0006AACC File Offset: 0x00068CCC
		public virtual void Write(bool value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = ((!value) ? 0 : 1);
			this.OutStream.Write(this.buffer, 0, 1);
		}

		/// <summary>Writes an unsigned byte to the current stream and advances the stream position by one byte.</summary>
		/// <param name="value">The unsigned byte to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF1 RID: 7409 RVA: 0x0006AB20 File Offset: 0x00068D20
		public virtual void Write(byte value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.OutStream.WriteByte(value);
		}

		/// <summary>Writes a byte array to the underlying stream.</summary>
		/// <param name="buffer">A byte array containing the data to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF2 RID: 7410 RVA: 0x0006AB4C File Offset: 0x00068D4C
		public virtual void Write(byte[] buffer)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a region of a byte array to the current stream.</summary>
		/// <param name="buffer">A byte array containing the data to write. </param>
		/// <param name="index">The starting point in <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF3 RID: 7411 RVA: 0x0006AB98 File Offset: 0x00068D98
		public virtual void Write(byte[] buffer, int index, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, index, count);
		}

		/// <summary>Writes a Unicode character to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream.</summary>
		/// <param name="ch">The non-surrogate, Unicode character to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ch" /> is a single surrogate character.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF4 RID: 7412 RVA: 0x0006ABE0 File Offset: 0x00068DE0
		public virtual void Write(char ch)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			char[] chars = new char[]
			{
				ch
			};
			byte[] bytes = this.m_encoding.GetBytes(chars, 0, 1);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a character array to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream.</summary>
		/// <param name="chars">A character array containing the data to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF5 RID: 7413 RVA: 0x0006AC34 File Offset: 0x00068E34
		public virtual void Write(char[] chars)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this.m_encoding.GetBytes(chars, 0, chars.Length);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a section of a character array to the current stream, and advances the current position of the stream in accordance with the Encoding used and perhaps the specific characters being written to the stream.</summary>
		/// <param name="chars">A character array containing the data to write. </param>
		/// <param name="index">The starting point in <paramref name="chars" /> from which to begin writing. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x0006AC90 File Offset: 0x00068E90
		public virtual void Write(char[] chars, int index, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this.m_encoding.GetBytes(chars, index, count);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a decimal value to the current stream and advances the stream position by sixteen bytes.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF7 RID: 7415 RVA: 0x0006ACE8 File Offset: 0x00068EE8
		public unsafe virtual void Write(decimal value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 16; i++)
				{
					if (i < 4)
					{
						this.buffer[i + 12] = *(ref value + i);
					}
					else if (i < 8)
					{
						this.buffer[i + 4] = *(ref value + i);
					}
					else if (i < 12)
					{
						this.buffer[i - 8] = *(ref value + i);
					}
					else
					{
						this.buffer[i - 8] = *(ref value + i);
					}
				}
			}
			else
			{
				for (int j = 0; j < 16; j++)
				{
					if (j < 4)
					{
						this.buffer[15 - j] = *(ref value + j);
					}
					else if (j < 8)
					{
						this.buffer[15 - j] = *(ref value + j);
					}
					else if (j < 12)
					{
						this.buffer[11 - j] = *(ref value + j);
					}
					else
					{
						this.buffer[19 - j] = *(ref value + j);
					}
				}
			}
			this.OutStream.Write(this.buffer, 0, 16);
		}

		/// <summary>Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte floating-point value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006AE1C File Offset: 0x0006901C
		public virtual void Write(double value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 8);
		}

		/// <summary>Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.</summary>
		/// <param name="value">The two-byte signed integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006AE58 File Offset: 0x00069058
		public virtual void Write(short value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this.buffer, 0, 2);
		}

		/// <summary>Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte signed integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CFA RID: 7418 RVA: 0x0006AEAC File Offset: 0x000690AC
		public virtual void Write(int value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.buffer[2] = (byte)(value >> 16);
			this.buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this.buffer, 0, 4);
		}

		/// <summary>Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte signed integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CFB RID: 7419 RVA: 0x0006AF18 File Offset: 0x00069118
		public virtual void Write(long value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			int i = 0;
			int num = 0;
			while (i < 8)
			{
				this.buffer[i] = (byte)(value >> num);
				i++;
				num += 8;
			}
			this.OutStream.Write(this.buffer, 0, 8);
		}

		/// <summary>Writes a signed byte to the current stream and advances the stream position by one byte.</summary>
		/// <param name="value">The signed byte to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CFC RID: 7420 RVA: 0x0006AF7C File Offset: 0x0006917C
		[CLSCompliant(false)]
		public virtual void Write(sbyte value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.OutStream.Write(this.buffer, 0, 1);
		}

		/// <summary>Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte floating-point value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CFD RID: 7421 RVA: 0x0006AFC4 File Offset: 0x000691C4
		public virtual void Write(float value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 4);
		}

		/// <summary>Writes a length-prefixed string to this stream in the current encoding of the <see cref="T:System.IO.BinaryWriter" />, and advances the current position of the stream in accordance with the encoding used and the specific characters being written to the stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CFE RID: 7422 RVA: 0x0006B000 File Offset: 0x00069200
		public virtual void Write(string value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			int byteCount = this.m_encoding.GetByteCount(value);
			this.Write7BitEncodedInt(byteCount);
			if (this.stringBuffer == null)
			{
				this.stringBuffer = new byte[512];
				this.maxCharsPerRound = 512 / this.m_encoding.GetMaxByteCount(1);
			}
			int num = 0;
			int num2;
			for (int i = value.Length; i > 0; i -= num2)
			{
				num2 = ((i <= this.maxCharsPerRound) ? i : this.maxCharsPerRound);
				int bytes = this.m_encoding.GetBytes(value, num, num2, this.stringBuffer, 0);
				this.OutStream.Write(this.stringBuffer, 0, bytes);
				num += num2;
			}
		}

		/// <summary>Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.</summary>
		/// <param name="value">The two-byte unsigned integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001CFF RID: 7423 RVA: 0x0006B0D0 File Offset: 0x000692D0
		[CLSCompliant(false)]
		public virtual void Write(ushort value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this.buffer, 0, 2);
		}

		/// <summary>Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte unsigned integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D00 RID: 7424 RVA: 0x0006B124 File Offset: 0x00069324
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			this.buffer[0] = (byte)value;
			this.buffer[1] = (byte)(value >> 8);
			this.buffer[2] = (byte)(value >> 16);
			this.buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this.buffer, 0, 4);
		}

		/// <summary>Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte unsigned integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D01 RID: 7425 RVA: 0x0006B190 File Offset: 0x00069390
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("BinaryWriter", "Cannot write to a closed BinaryWriter");
			}
			int i = 0;
			int num = 0;
			while (i < 8)
			{
				this.buffer[i] = (byte)(value >> num);
				i++;
				num += 8;
			}
			this.OutStream.Write(this.buffer, 0, 8);
		}

		/// <summary>Writes a 32-bit integer in a compressed format.</summary>
		/// <param name="value">The 32-bit integer to be written. </param>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed. </exception>
		// Token: 0x06001D02 RID: 7426 RVA: 0x0006B1F4 File Offset: 0x000693F4
		protected void Write7BitEncodedInt(int value)
		{
			do
			{
				int num = value >> 7 & 33554431;
				byte b = (byte)(value & 127);
				if (num != 0)
				{
					b |= 128;
				}
				this.Write(b);
				value = num;
			}
			while (value != 0);
		}

		/// <summary>Specifies a <see cref="T:System.IO.BinaryWriter" /> with no backing store.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AE2 RID: 2786
		public static readonly BinaryWriter Null = new BinaryWriter();

		/// <summary>Holds the underlying stream.</summary>
		// Token: 0x04000AE3 RID: 2787
		protected Stream OutStream;

		// Token: 0x04000AE4 RID: 2788
		private Encoding m_encoding;

		// Token: 0x04000AE5 RID: 2789
		private byte[] buffer;

		// Token: 0x04000AE6 RID: 2790
		private bool disposed;

		// Token: 0x04000AE7 RID: 2791
		private byte[] stringBuffer;

		// Token: 0x04000AE8 RID: 2792
		private int maxCharsPerRound;
	}
}
