using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties for compressing and decompressing streams using the Deflate algorithm.</summary>
	// Token: 0x02000273 RID: 627
	public class DeflateStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class using the specified stream and <see cref="T:System.IO.Compression.CompressionMode" /> value.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.Compression.CompressionMode" /> values that indicates the action to take.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="stream" /> access right is ReadOnly and <paramref name="mode" /> value is Compress. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001616 RID: 5654 RVA: 0x0003B194 File Offset: 0x00039394
		public DeflateStream(Stream compressedStream, CompressionMode mode) : this(compressedStream, mode, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class using the specified stream and <see cref="T:System.IO.Compression.CompressionMode" /> value, and a value that specifies whether to leave the stream open.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.Compression.CompressionMode" /> values that indicates the action to take.</param>
		/// <param name="leaveOpen">true to leave the stream open; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="stream" /> access right is ReadOnly and <paramref name="mode" /> value is Compress. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001617 RID: 5655 RVA: 0x0003B1A0 File Offset: 0x000393A0
		public DeflateStream(Stream compressedStream, CompressionMode mode, bool leaveOpen) : this(compressedStream, mode, leaveOpen, false)
		{
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0003B1AC File Offset: 0x000393AC
		internal DeflateStream(Stream compressedStream, CompressionMode mode, bool leaveOpen, bool gzip)
		{
			if (compressedStream == null)
			{
				throw new ArgumentNullException("compressedStream");
			}
			if (mode != CompressionMode.Compress && mode != CompressionMode.Decompress)
			{
				throw new ArgumentException("mode");
			}
			this.data = GCHandle.Alloc(this);
			this.base_stream = compressedStream;
			this.feeder = ((mode != CompressionMode.Compress) ? new DeflateStream.UnmanagedReadOrWrite(DeflateStream.UnmanagedRead) : new DeflateStream.UnmanagedReadOrWrite(DeflateStream.UnmanagedWrite));
			this.z_stream = DeflateStream.CreateZStream(mode, gzip, this.feeder, GCHandle.ToIntPtr(this.data));
			if (this.z_stream == IntPtr.Zero)
			{
				this.base_stream = null;
				this.feeder = null;
				throw new NotImplementedException("Failed to initialize zlib. You probably have an old zlib installed. Version 1.2.0.4 or later is required.");
			}
			this.mode = mode;
			this.leaveOpen = leaveOpen;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.DeflateStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001619 RID: 5657 RVA: 0x0003B280 File Offset: 0x00039480
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				IntPtr intPtr = this.z_stream;
				this.z_stream = IntPtr.Zero;
				int result = 0;
				if (intPtr != IntPtr.Zero)
				{
					result = DeflateStream.CloseZStream(intPtr);
				}
				this.io_buffer = null;
				if (!this.leaveOpen)
				{
					Stream stream = this.base_stream;
					if (stream != null)
					{
						stream.Close();
					}
					this.base_stream = null;
				}
				DeflateStream.CheckResult(result, "Dispose");
			}
			if (this.data.IsAllocated)
			{
				this.data.Free();
				this.data = default(GCHandle);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0003B338 File Offset: 0x00039538
		private static int UnmanagedRead(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStream deflateStream = GCHandle.FromIntPtr(data).Target as DeflateStream;
			if (deflateStream == null)
			{
				return -1;
			}
			return deflateStream.UnmanagedRead(buffer, length);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0003B36C File Offset: 0x0003956C
		private unsafe int UnmanagedRead(IntPtr buffer, int length)
		{
			int num = 0;
			int num2 = 1;
			while (length > 0 && num2 > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int count = Math.Min(length, this.io_buffer.Length);
				num2 = this.base_stream.Read(this.io_buffer, 0, count);
				if (num2 > 0)
				{
					Marshal.Copy(this.io_buffer, 0, buffer, num2);
					buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
					length -= num2;
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0003B400 File Offset: 0x00039600
		private static int UnmanagedWrite(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStream deflateStream = GCHandle.FromIntPtr(data).Target as DeflateStream;
			if (deflateStream == null)
			{
				return -1;
			}
			return deflateStream.UnmanagedWrite(buffer, length);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0003B434 File Offset: 0x00039634
		private unsafe int UnmanagedWrite(IntPtr buffer, int length)
		{
			int num = 0;
			while (length > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num2 = Math.Min(length, this.io_buffer.Length);
				Marshal.Copy(buffer, this.io_buffer, 0, num2);
				this.base_stream.Write(this.io_buffer, 0, num2);
				buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0003B4B4 File Offset: 0x000396B4
		private unsafe int ReadInternal(byte[] array, int offset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int result;
			fixed (byte* ptr = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				IntPtr buffer = new IntPtr((void*)(ptr + offset));
				result = DeflateStream.ReadZStream(this.z_stream, buffer, count);
			}
			DeflateStream.CheckResult(result, "ReadInternal");
			return result;
		}

		/// <summary>Reads a specified number of compressed or decompressed bytes into the specified byte array.</summary>
		/// <returns>The number of bytes that were read into the byte array.</returns>
		/// <param name="array">The buffer to store the compressed or decompressed bytes.</param>
		/// <param name="offset">The location in the array to begin reading.</param>
		/// <param name="count">The number of compressed or decompressed bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.Compression.CompressionMode" /> value was Compress when the object was created.- or - The underlying stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than zero.-or-<paramref name="array" /> length minus the index starting point is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The data is in an invalid format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x0600161F RID: 5663 RVA: 0x0003B510 File Offset: 0x00039710
		public override int Read(byte[] dest, int dest_offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dest == null)
			{
				throw new ArgumentNullException("Destination array is null.");
			}
			if (!this.CanRead)
			{
				throw new InvalidOperationException("Stream does not support reading.");
			}
			int num = dest.Length;
			if (dest_offset < 0 || count < 0)
			{
				throw new ArgumentException("Dest or count is negative.");
			}
			if (dest_offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (dest_offset + count > num)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			return this.ReadInternal(dest, dest_offset, count);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0003B5AC File Offset: 0x000397AC
		private unsafe void WriteInternal(byte[] array, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			int result;
			fixed (byte* ptr = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				IntPtr buffer = new IntPtr((void*)(ptr + offset));
				result = DeflateStream.WriteZStream(this.z_stream, buffer, count);
			}
			DeflateStream.CheckResult(result, "WriteInternal");
		}

		/// <summary>Writes compressed or decompressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress or decompress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of bytes to compress.</param>
		// Token: 0x06001621 RID: 5665 RVA: 0x0003B608 File Offset: 0x00039808
		public override void Write(byte[] src, int src_offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (src_offset < 0)
			{
				throw new ArgumentOutOfRangeException("src_offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			this.WriteInternal(src, src_offset, count);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0003B688 File Offset: 0x00039888
		private static void CheckResult(int result, string where)
		{
			if (result >= 0)
			{
				return;
			}
			string str;
			switch (result + 11)
			{
			case 0:
				str = "IO error";
				goto IL_A7;
			case 1:
				str = "Invalid argument(s)";
				goto IL_A7;
			case 5:
				str = "Invalid version";
				goto IL_A7;
			case 6:
				str = "Internal error (no progress possible)";
				goto IL_A7;
			case 7:
				str = "Not enough memory";
				goto IL_A7;
			case 8:
				str = "Corrupted data";
				goto IL_A7;
			case 9:
				str = "Internal error";
				goto IL_A7;
			case 10:
				str = "Unknown error";
				goto IL_A7;
			}
			str = "Unknown error";
			IL_A7:
			throw new IOException(str + " " + where);
		}

		/// <summary>Flushes the contents of the internal buffer of the current stream object to the underlying stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001623 RID: 5667 RVA: 0x0003B750 File Offset: 0x00039950
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.CanWrite)
			{
				int result = DeflateStream.Flush(this.z_stream);
				DeflateStream.CheckResult(result, "Flush");
			}
		}

		/// <summary>Begins an asynchronous read operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous read, which could still be pending.</returns>
		/// <param name="array">The byte array to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin writing data read from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the read is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.IO.IOException">An asynchronous read past the end of the stream was attempted, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.DeflateStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">This call cannot be completed. </exception>
		// Token: 0x06001624 RID: 5668 RVA: 0x0003B79C File Offset: 0x0003999C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			DeflateStream.ReadMethod readMethod = new DeflateStream.ReadMethod(this.ReadInternal);
			return readMethod.BeginInvoke(buffer, offset, count, cback, state);
		}

		/// <summary>Begins an asynchronous write operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous write, which could still be pending.</returns>
		/// <param name="array">The buffer to write data from.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> to begin writing from.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the write is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.IO.IOException">An asynchronous write past the end of the stream was attempted, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.DeflateStream" /> implementation does not support the write operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06001625 RID: 5669 RVA: 0x0003B84C File Offset: 0x00039A4C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanWrite)
			{
				throw new InvalidOperationException("This stream does not support writing");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			DeflateStream.WriteMethod writeMethod = new DeflateStream.WriteMethod(this.WriteInternal);
			return writeMethod.BeginInvoke(buffer, offset, count, cback, state);
		}

		/// <summary>Waits for the pending asynchronous read to complete.</summary>
		/// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. <see cref="T:System.IO.Compression.DeflateStream" /> returns zero (0) only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.SystemException">An exception was thrown during a call to <see cref="M:System.Threading.WaitHandle.WaitOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end call is invalid because asynchronous read operations for this stream are not yet complete.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is null.</exception>
		// Token: 0x06001626 RID: 5670 RVA: 0x0003B8FC File Offset: 0x00039AFC
		public override int EndRead(IAsyncResult async_result)
		{
			if (async_result == null)
			{
				throw new ArgumentNullException("async_result");
			}
			AsyncResult asyncResult = async_result as AsyncResult;
			if (asyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			DeflateStream.ReadMethod readMethod = asyncResult.AsyncDelegate as DeflateStream.ReadMethod;
			if (readMethod == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			return readMethod.EndInvoke(async_result);
		}

		/// <summary>Ends an asynchronous write operation.</summary>
		/// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.Exception">An exception was thrown during a call to <see cref="M:System.Threading.WaitHandle.WaitOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end write call is invalid.</exception>
		// Token: 0x06001627 RID: 5671 RVA: 0x0003B960 File Offset: 0x00039B60
		public override void EndWrite(IAsyncResult async_result)
		{
			if (async_result == null)
			{
				throw new ArgumentNullException("async_result");
			}
			AsyncResult asyncResult = async_result as AsyncResult;
			if (asyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			DeflateStream.WriteMethod writeMethod = asyncResult.AsyncDelegate as DeflateStream.WriteMethod;
			if (writeMethod == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			writeMethod.EndInvoke(async_result);
		}

		/// <summary>This operation is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06001628 RID: 5672 RVA: 0x0003B9C4 File Offset: 0x00039BC4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// <summary>This operation is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06001629 RID: 5673 RVA: 0x0003B9CC File Offset: 0x00039BCC
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a reference to the underlying stream.</summary>
		/// <returns>A stream object that represents the underlying stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream is closed.</exception>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0003B9D4 File Offset: 0x00039BD4
		public Stream BaseStream
		{
			get
			{
				return this.base_stream;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Decompress, and the underlying stream is opened and supports reading; otherwise, false.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0003B9DC File Offset: 0x00039BDC
		public override bool CanRead
		{
			get
			{
				return !this.disposed && this.mode == CompressionMode.Decompress && this.base_stream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0003BA10 File Offset: 0x00039C10
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Compress, and the underlying stream supports writing and is not closed; otherwise, false.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0003BA14 File Offset: 0x00039C14
		public override bool CanWrite
		{
			get
			{
				return !this.disposed && this.mode == CompressionMode.Compress && this.base_stream.CanWrite;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0003BA3C File Offset: 0x00039C3C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0003BA44 File Offset: 0x00039C44
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0003BA4C File Offset: 0x00039C4C
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001631 RID: 5681
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateZStream(CompressionMode compress, bool gzip, DeflateStream.UnmanagedReadOrWrite feeder, IntPtr data);

		// Token: 0x06001632 RID: 5682
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int CloseZStream(IntPtr stream);

		// Token: 0x06001633 RID: 5683
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Flush(IntPtr stream);

		// Token: 0x06001634 RID: 5684
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int ReadZStream(IntPtr stream, IntPtr buffer, int length);

		// Token: 0x06001635 RID: 5685
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int WriteZStream(IntPtr stream, IntPtr buffer, int length);

		// Token: 0x040006E9 RID: 1769
		private const int BufferSize = 4096;

		// Token: 0x040006EA RID: 1770
		private const string LIBNAME = "MonoPosixHelper";

		// Token: 0x040006EB RID: 1771
		private Stream base_stream;

		// Token: 0x040006EC RID: 1772
		private CompressionMode mode;

		// Token: 0x040006ED RID: 1773
		private bool leaveOpen;

		// Token: 0x040006EE RID: 1774
		private bool disposed;

		// Token: 0x040006EF RID: 1775
		private DeflateStream.UnmanagedReadOrWrite feeder;

		// Token: 0x040006F0 RID: 1776
		private IntPtr z_stream;

		// Token: 0x040006F1 RID: 1777
		private byte[] io_buffer;

		// Token: 0x040006F2 RID: 1778
		private GCHandle data;

		// Token: 0x020004DC RID: 1244
		// (Invoke) Token: 0x06002C1C RID: 11292
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int UnmanagedReadOrWrite(IntPtr buffer, int length, IntPtr data);

		// Token: 0x020004DD RID: 1245
		// (Invoke) Token: 0x06002C20 RID: 11296
		private delegate int ReadMethod(byte[] array, int offset, int count);

		// Token: 0x020004DE RID: 1246
		// (Invoke) Token: 0x06002C24 RID: 11300
		private delegate void WriteMethod(byte[] array, int offset, int count);
	}
}
