using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Creates a stream whose backing store is memory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000247 RID: 583
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class MemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized to zero.</summary>
		// Token: 0x06001E4A RID: 7754 RVA: 0x00070014 File Offset: 0x0006E214
		public MemoryStream() : this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized as specified.</summary>
		/// <param name="capacity">The initial size of the internal array in bytes. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is negative. </exception>
		// Token: 0x06001E4B RID: 7755 RVA: 0x00070020 File Offset: 0x0006E220
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.canWrite = true;
			this.capacity = capacity;
			this.internalBuffer = new byte[capacity];
			this.expandable = true;
			this.allowGetBuffer = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		// Token: 0x06001E4C RID: 7756 RVA: 0x00070070 File Offset: 0x0006E270
		public MemoryStream(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.InternalConstructor(buffer, 0, buffer.Length, true, false);
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		// Token: 0x06001E4D RID: 7757 RVA: 0x000700A4 File Offset: 0x0006E2A4
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.InternalConstructor(buffer, 0, buffer.Length, writable, false);
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region (index) of a byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is greater than the length of <paramref name="buffer" />. </exception>
		// Token: 0x06001E4E RID: 7758 RVA: 0x000700D8 File Offset: 0x0006E2D8
		public MemoryStream(byte[] buffer, int index, int count)
		{
			this.InternalConstructor(buffer, index, count, true, false);
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index in <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> are negative. </exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is greater than the length of <paramref name="buffer" />. </exception>
		// Token: 0x06001E4F RID: 7759 RVA: 0x000700F8 File Offset: 0x0006E2F8
		public MemoryStream(byte[] buffer, int index, int count, bool writable)
		{
			this.InternalConstructor(buffer, index, count, writable, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified, and the ability to call <see cref="M:System.IO.MemoryStream.GetBuffer" /> set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <param name="publiclyVisible">true to enable <see cref="M:System.IO.MemoryStream.GetBuffer" />, which returns the unsigned byte array from which the stream was created; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06001E50 RID: 7760 RVA: 0x00070118 File Offset: 0x0006E318
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			this.InternalConstructor(buffer, index, count, writable, publiclyVisible);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00070138 File Offset: 0x0006E338
		private void InternalConstructor(byte[] buffer, int index, int count, bool writable, bool publicallyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("index or count is less than 0.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("index+count", "The size of the buffer is less than index + count.");
			}
			this.canWrite = writable;
			this.internalBuffer = buffer;
			this.capacity = count + index;
			this.length = this.capacity;
			this.position = index;
			this.initialIndex = index;
			this.allowGetBuffer = publicallyVisible;
			this.expandable = false;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000701CC File Offset: 0x0006E3CC
		private void CheckIfClosedThrowDisposed()
		{
			if (this.streamClosed)
			{
				throw new ObjectDisposedException("MemoryStream");
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream is open.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000701E4 File Offset: 0x0006E3E4
		public override bool CanRead
		{
			get
			{
				return !this.streamClosed;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream is open.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000701F0 File Offset: 0x0006E3F0
		public override bool CanSeek
		{
			get
			{
				return !this.streamClosed;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x000701FC File Offset: 0x0006E3FC
		public override bool CanWrite
		{
			get
			{
				return !this.streamClosed && this.canWrite;
			}
		}

		/// <summary>Gets or sets the number of bytes allocated for this stream.</summary>
		/// <returns>The length of the usable portion of the buffer for the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A capacity is set that is negative or less than the current length of the stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">set is invoked on a stream whose capacity cannot be modified. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x00070214 File Offset: 0x0006E414
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0007022C File Offset: 0x0006E42C
		public virtual int Capacity
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return this.capacity - this.initialIndex;
			}
			set
			{
				this.CheckIfClosedThrowDisposed();
				if (value == this.capacity)
				{
					return;
				}
				if (!this.expandable)
				{
					throw new NotSupportedException("Cannot expand this MemoryStream");
				}
				if (value < 0 || value < this.length)
				{
					throw new ArgumentOutOfRangeException("value", string.Concat(new object[]
					{
						"New capacity cannot be negative or less than the current capacity ",
						value,
						" ",
						this.capacity
					}));
				}
				byte[] dst = null;
				if (value != 0)
				{
					dst = new byte[value];
					Buffer.BlockCopy(this.internalBuffer, 0, dst, 0, this.length);
				}
				this.dirty_bytes = 0;
				this.internalBuffer = dst;
				this.capacity = value;
			}
		}

		/// <summary>Gets the length of the stream in bytes.</summary>
		/// <returns>The length of the stream in bytes.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000702EC File Offset: 0x0006E4EC
		public override long Length
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return (long)(this.length - this.initialIndex);
			}
		}

		/// <summary>Gets or sets the current position within the stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a negative value or a value greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x00070304 File Offset: 0x0006E504
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x0007031C File Offset: 0x0006E51C
		public override long Position
		{
			get
			{
				this.CheckIfClosedThrowDisposed();
				return (long)(this.position - this.initialIndex);
			}
			set
			{
				this.CheckIfClosedThrowDisposed();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Position cannot be negative");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "Position must be non-negative and less than 2^31 - 1 - origin");
				}
				this.position = this.initialIndex + (int)value;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryStream" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001E5B RID: 7771 RVA: 0x00070374 File Offset: 0x0006E574
		protected override void Dispose(bool disposing)
		{
			this.streamClosed = true;
			this.expandable = false;
		}

		/// <summary>Overrides <see cref="M:System.IO.Stream.Flush" /> so that no action is performed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E5C RID: 7772 RVA: 0x00070384 File Offset: 0x0006E584
		public override void Flush()
		{
		}

		/// <summary>Returns the array of unsigned bytes from which this stream was created.</summary>
		/// <returns>The byte array from which this stream was created, or the underlying array if a byte array was not provided to the <see cref="T:System.IO.MemoryStream" /> constructor during construction of the current instance.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The MemoryStream instance was not created with a publicly visible buffer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E5D RID: 7773 RVA: 0x00070388 File Offset: 0x0006E588
		public virtual byte[] GetBuffer()
		{
			if (!this.allowGetBuffer)
			{
				throw new UnauthorizedAccessException();
			}
			return this.internalBuffer;
		}

		/// <summary>Reads a block of bytes from the current stream and writes the data to <paramref name="buffer" />.</summary>
		/// <returns>The total number of bytes written into the buffer. This can be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached before any bytes are read.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the characters read from the current stream. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin reading. </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E5E RID: 7774 RVA: 0x000703A4 File Offset: 0x0006E5A4
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckIfClosedThrowDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.position >= this.length || count == 0)
			{
				return 0;
			}
			if (this.position > this.length - count)
			{
				count = this.length - this.position;
			}
			Buffer.BlockCopy(this.internalBuffer, this.position, buffer, offset, count);
			this.position += count;
			return count;
		}

		/// <summary>Reads a byte from the current stream.</summary>
		/// <returns>The byte cast to a <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E5F RID: 7775 RVA: 0x0007045C File Offset: 0x0006E65C
		public override int ReadByte()
		{
			this.CheckIfClosedThrowDisposed();
			if (this.position >= this.length)
			{
				return -1;
			}
			return (int)this.internalBuffer[this.position++];
		}

		/// <summary>Sets the position within the current stream to the specified value.</summary>
		/// <returns>The new position within the stream, calculated by combining the initial reference point and the offset.</returns>
		/// <param name="offset">The new position within the stream. This is relative to the <paramref name="loc" /> parameter, and can be positive or negative. </param>
		/// <param name="loc">A value of type <see cref="T:System.IO.SeekOrigin" />, which acts as the seek reference point. </param>
		/// <exception cref="T:System.IO.IOException">Seeking is attempted before the beginning of the stream. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">There is an invalid <see cref="T:System.IO.SeekOrigin" />. -or-<paramref name="offset" /> caused an arithmetic overflow.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E60 RID: 7776 RVA: 0x0007049C File Offset: 0x0006E69C
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.CheckIfClosedThrowDisposed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("Offset out of range. " + offset);
			}
			int num;
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				num = this.initialIndex;
				break;
			case SeekOrigin.Current:
				num = this.position;
				break;
			case SeekOrigin.End:
				num = this.length;
				break;
			default:
				throw new ArgumentException("loc", "Invalid SeekOrigin");
			}
			num += (int)offset;
			if (num < this.initialIndex)
			{
				throw new IOException("Attempted to seek before start of MemoryStream.");
			}
			this.position = num;
			return (long)this.position;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0007055C File Offset: 0x0006E75C
		private int CalculateNewCapacity(int minimum)
		{
			if (minimum < 256)
			{
				minimum = 256;
			}
			if (minimum < this.capacity * 2)
			{
				minimum = this.capacity * 2;
			}
			return minimum;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0007058C File Offset: 0x0006E78C
		private void Expand(int newSize)
		{
			if (newSize > this.capacity)
			{
				this.Capacity = this.CalculateNewCapacity(newSize);
			}
			else if (this.dirty_bytes > 0)
			{
				Array.Clear(this.internalBuffer, this.length, this.dirty_bytes);
				this.dirty_bytes = 0;
			}
		}

		/// <summary>Sets the length of the current stream to the specified value.</summary>
		/// <param name="value">The value at which to set the length. </param>
		/// <exception cref="T:System.NotSupportedException">The current stream is not resizable and <paramref name="value" /> is larger than the current capacity.-or- The current stream does not support writing. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is negative or is greater than the maximum length of the <see cref="T:System.IO.MemoryStream" />, where the maximum length is(<see cref="F:System.Int32.MaxValue" /> - origin), and origin is the index into the underlying buffer at which the stream starts. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E63 RID: 7779 RVA: 0x000705E4 File Offset: 0x0006E7E4
		public override void SetLength(long value)
		{
			if (!this.expandable && value > (long)this.capacity)
			{
				throw new NotSupportedException("Expanding this MemoryStream is not supported");
			}
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException(Locale.GetText("Cannot write to this MemoryStream"));
			}
			if (value < 0L || value + (long)this.initialIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = (int)value + this.initialIndex;
			if (num > this.length)
			{
				this.Expand(num);
			}
			else if (num < this.length)
			{
				this.dirty_bytes += this.length - num;
			}
			this.length = num;
			if (this.position > this.length)
			{
				this.position = this.length;
			}
		}

		/// <summary>Writes the stream contents to a byte array, regardless of the <see cref="P:System.IO.MemoryStream.Position" /> property.</summary>
		/// <returns>A new byte array.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E64 RID: 7780 RVA: 0x000706C0 File Offset: 0x0006E8C0
		public virtual byte[] ToArray()
		{
			int num = this.length - this.initialIndex;
			byte[] array = new byte[num];
			if (this.internalBuffer != null)
			{
				Buffer.BlockCopy(this.internalBuffer, this.initialIndex, array, 0, num);
			}
			return array;
		}

		/// <summary>Writes a block of bytes to the current stream using data read from buffer.</summary>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing from. </param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.-or- The current position is closer than <paramref name="count" /> bytes to the end of the stream, and the capacity cannot be modified. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> are negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E65 RID: 7781 RVA: 0x00070704 File Offset: 0x0006E904
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this stream.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.position > this.length - count)
			{
				this.Expand(this.position + count);
			}
			Buffer.BlockCopy(buffer, offset, this.internalBuffer, this.position, count);
			this.position += count;
			if (this.position >= this.length)
			{
				this.length = this.position;
			}
		}

		/// <summary>Writes a byte to the current stream at the current position.</summary>
		/// <param name="value">The byte to write. </param>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.-or- The current position is at the end of the stream, and the capacity cannot be modified. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E66 RID: 7782 RVA: 0x000707D0 File Offset: 0x0006E9D0
		public override void WriteByte(byte value)
		{
			this.CheckIfClosedThrowDisposed();
			if (!this.canWrite)
			{
				throw new NotSupportedException("Cannot write to this stream.");
			}
			if (this.position >= this.length)
			{
				this.Expand(this.position + 1);
				this.length = this.position + 1;
			}
			this.internalBuffer[this.position++] = value;
		}

		/// <summary>Writes the entire contents of this memory stream to another stream.</summary>
		/// <param name="stream">The stream to write this memory stream to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current or target stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E67 RID: 7783 RVA: 0x00070840 File Offset: 0x0006EA40
		public virtual void WriteTo(Stream stream)
		{
			this.CheckIfClosedThrowDisposed();
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.Write(this.internalBuffer, this.initialIndex, this.length - this.initialIndex);
		}

		// Token: 0x04000B54 RID: 2900
		private bool canWrite;

		// Token: 0x04000B55 RID: 2901
		private bool allowGetBuffer;

		// Token: 0x04000B56 RID: 2902
		private int capacity;

		// Token: 0x04000B57 RID: 2903
		private int length;

		// Token: 0x04000B58 RID: 2904
		private byte[] internalBuffer;

		// Token: 0x04000B59 RID: 2905
		private int initialIndex;

		// Token: 0x04000B5A RID: 2906
		private bool expandable;

		// Token: 0x04000B5B RID: 2907
		private bool streamClosed;

		// Token: 0x04000B5C RID: 2908
		private int position;

		// Token: 0x04000B5D RID: 2909
		private int dirty_bytes;
	}
}
