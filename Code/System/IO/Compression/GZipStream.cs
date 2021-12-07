using System;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties used to compress and decompress streams.</summary>
	// Token: 0x02000274 RID: 628
	public class GZipStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class using the specified stream and <see cref="T:System.IO.Compression.CompressionMode" /> value.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.Compression.CompressionMode" /> values that indicates the action to take.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="stream" /> access right is ReadOnly and <paramref name="mode" /> value is Compress.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> enumeration value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001636 RID: 5686 RVA: 0x0003BA54 File Offset: 0x00039C54
		public GZipStream(Stream compressedStream, CompressionMode mode) : this(compressedStream, mode, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class using the specified stream and <see cref="T:System.IO.Compression.CompressionMode" /> value, and a value that specifies whether to leave the stream open.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.Compression.CompressionMode" /> values that indicates the action to take.</param>
		/// <param name="leaveOpen">true to leave the stream open; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="stream" /> access right is ReadOnly and <paramref name="mode" /> value is Compress. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001637 RID: 5687 RVA: 0x0003BA60 File Offset: 0x00039C60
		public GZipStream(Stream compressedStream, CompressionMode mode, bool leaveOpen)
		{
			this.deflateStream = new DeflateStream(compressedStream, mode, leaveOpen, true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.GZipStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001638 RID: 5688 RVA: 0x0003BA78 File Offset: 0x00039C78
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.deflateStream.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
		/// <returns>The number of bytes that were decompressed into the byte array. If the end of the stream has been reached, zero or the number of bytes read is returned.</returns>
		/// <param name="array">The array used to store decompressed bytes.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The number of bytes decompressed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.Compression.CompressionMode" /> value was Compress when the object was created.- or -The underlying stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than zero.-or-<paramref name="array" /> length minus the index starting point is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The data is in an invalid format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06001639 RID: 5689 RVA: 0x0003BA94 File Offset: 0x00039C94
		public override int Read(byte[] dest, int dest_offset, int count)
		{
			return this.deflateStream.Read(dest, dest_offset, count);
		}

		/// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the compressed bytes will be placed.</param>
		/// <param name="count">The number of bytes compressed.</param>
		/// <exception cref="T:System.ObjectDisposedException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x0600163A RID: 5690 RVA: 0x0003BAA4 File Offset: 0x00039CA4
		public override void Write(byte[] src, int src_offset, int count)
		{
			this.deflateStream.Write(src, src_offset, count);
		}

		/// <summary>Flushes the contents of the internal buffer of the current <see cref="T:System.IO.Compression.GZipStream" /> object to the underlying stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600163B RID: 5691 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		public override void Flush()
		{
			this.deflateStream.Flush();
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x0600163C RID: 5692 RVA: 0x0003BAC4 File Offset: 0x00039CC4
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.deflateStream.Seek(offset, origin);
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x0600163D RID: 5693 RVA: 0x0003BAD4 File Offset: 0x00039CD4
		public override void SetLength(long value)
		{
			this.deflateStream.SetLength(value);
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
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.GZipStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A read operation cannot be performed because the stream is closed.</exception>
		// Token: 0x0600163E RID: 5694 RVA: 0x0003BAE4 File Offset: 0x00039CE4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			return this.deflateStream.BeginRead(buffer, offset, count, cback, state);
		}

		/// <summary>Begins an asynchronous write operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous write, which could still be pending.</returns>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback to be called when the write is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is null. -or-The underlying stream is closed.</exception>
		// Token: 0x0600163F RID: 5695 RVA: 0x0003BAF8 File Offset: 0x00039CF8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			return this.deflateStream.BeginWrite(buffer, offset, count, cback, state);
		}

		/// <summary>Waits for the pending asynchronous read to complete.</summary>
		/// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. <see cref="T:System.IO.Compression.GZipStream" /> returns zero (0) only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06001640 RID: 5696 RVA: 0x0003BB0C File Offset: 0x00039D0C
		public override int EndRead(IAsyncResult async_result)
		{
			return this.deflateStream.EndRead(async_result);
		}

		/// <summary>Handles the end of an asynchronous write operation.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object that represents the asynchronous call.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is null. -or-The underlying stream is closed.</exception>
		// Token: 0x06001641 RID: 5697 RVA: 0x0003BB1C File Offset: 0x00039D1C
		public override void EndWrite(IAsyncResult async_result)
		{
			this.deflateStream.EndWrite(async_result);
		}

		/// <summary>Gets a reference to the underlying stream.</summary>
		/// <returns>A stream object that represents the underlying stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0003BB2C File Offset: 0x00039D2C
		public Stream BaseStream
		{
			get
			{
				return this.deflateStream.BaseStream;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Decompress, and the underlying stream supports reading and is not closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0003BB3C File Offset: 0x00039D3C
		public override bool CanRead
		{
			get
			{
				return this.deflateStream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0003BB4C File Offset: 0x00039D4C
		public override bool CanSeek
		{
			get
			{
				return this.deflateStream.CanSeek;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Compress, and the underlying stream supports writing and is not closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0003BB5C File Offset: 0x00039D5C
		public override bool CanWrite
		{
			get
			{
				return this.deflateStream.CanWrite;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0003BB6C File Offset: 0x00039D6C
		public override long Length
		{
			get
			{
				return this.deflateStream.Length;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0003BB7C File Offset: 0x00039D7C
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x0003BB8C File Offset: 0x00039D8C
		public override long Position
		{
			get
			{
				return this.deflateStream.Position;
			}
			set
			{
				this.deflateStream.Position = value;
			}
		}

		// Token: 0x040006F3 RID: 1779
		private DeflateStream deflateStream;
	}
}
