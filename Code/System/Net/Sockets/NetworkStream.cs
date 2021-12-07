using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;

namespace System.Net.Sockets
{
	/// <summary>Provides the underlying stream of data for network access.</summary>
	// Token: 0x020003F1 RID: 1009
	public class NetworkStream : Stream, IDisposable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- The <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x060022DA RID: 8922 RVA: 0x000662E0 File Offset: 0x000644E0
		public NetworkStream(Socket socket) : this(socket, FileAccess.ReadWrite, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="ownsSocket">Set to true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- the value of the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- the <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x060022DB RID: 8923 RVA: 0x000662EC File Offset: 0x000644EC
		public NetworkStream(Socket socket, bool owns_socket) : this(socket, FileAccess.ReadWrite, owns_socket)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specify the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- the <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x060022DC RID: 8924 RVA: 0x000662F8 File Offset: 0x000644F8
		public NetworkStream(Socket socket, FileAccess access) : this(socket, access, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights and the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specifies the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <param name="ownsSocket">Set to true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- The <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x060022DD RID: 8925 RVA: 0x00066304 File Offset: 0x00064504
		public NetworkStream(Socket socket, FileAccess access, bool owns_socket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket is null");
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new ArgumentException("Socket is not of type Stream", "socket");
			}
			if (!socket.Connected)
			{
				throw new IOException("Not connected");
			}
			if (!socket.Blocking)
			{
				throw new IOException("Operation not allowed on a non-blocking socket.");
			}
			this.socket = socket;
			this.owns_socket = owns_socket;
			this.access = access;
			this.readable = this.CanRead;
			this.writeable = this.CanWrite;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		// Token: 0x060022DE RID: 8926 RVA: 0x000663A0 File Offset: 0x000645A0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports reading.</summary>
		/// <returns>true if data can be read from the stream; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000663B0 File Offset: 0x000645B0
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.ReadWrite || this.access == FileAccess.Read;
			}
		}

		/// <summary>Gets a value that indicates whether the stream supports seeking. This property is not currently supported.This property always returns false.</summary>
		/// <returns>false in all cases to indicate that <see cref="T:System.Net.Sockets.NetworkStream" /> cannot seek a specific location in the stream.</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000663CC File Offset: 0x000645CC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Indicates whether timeout properties are usable for <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x000663D0 File Offset: 0x000645D0
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports writing.</summary>
		/// <returns>true if data can be written to the <see cref="T:System.Net.Sockets.NetworkStream" />; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000663D4 File Offset: 0x000645D4
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.ReadWrite || this.access == FileAccess.Write;
			}
		}

		/// <summary>Gets a value that indicates whether data is available on the <see cref="T:System.Net.Sockets.NetworkStream" /> to be read.</summary>
		/// <returns>true if data is available on the stream to be read; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code, and refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000663F0 File Offset: 0x000645F0
		public virtual bool DataAvailable
		{
			get
			{
				this.CheckDisposed();
				return this.socket.Available > 0;
			}
		}

		/// <summary>Gets the length of the data available on the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The length of the data available on the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00066408 File Offset: 0x00064608
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the current position in the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x00066410 File Offset: 0x00064610
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x00066418 File Offset: 0x00064618
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00066420 File Offset: 0x00064620
		// (set) Token: 0x060022E8 RID: 8936 RVA: 0x00066428 File Offset: 0x00064628
		protected bool Readable
		{
			get
			{
				return this.readable;
			}
			set
			{
				this.readable = value;
			}
		}

		/// <summary>Gets or sets the amount of time that a read operation blocks waiting for data. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a read operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the read operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x00066434 File Offset: 0x00064634
		// (set) Token: 0x060022EA RID: 8938 RVA: 0x00066444 File Offset: 0x00064644
		public override int ReadTimeout
		{
			get
			{
				return this.socket.ReceiveTimeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified is less than or equal to zero and is not Infinite.");
				}
				this.socket.ReceiveTimeout = value;
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> that represents the underlying network connection.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0006647C File Offset: 0x0006467C
		protected Socket Socket
		{
			get
			{
				return this.socket;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> is writable.</summary>
		/// <returns>true if data can be written to the stream; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00066484 File Offset: 0x00064684
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x0006648C File Offset: 0x0006468C
		protected bool Writeable
		{
			get
			{
				return this.writeable;
			}
			set
			{
				this.writeable = value;
			}
		}

		/// <summary>Gets or sets the amount of time that a write operation blocks waiting for data. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a write operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the write operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00066498 File Offset: 0x00064698
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x000664A8 File Offset: 0x000646A8
		public override int WriteTimeout
		{
			get
			{
				return this.socket.SendTimeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "The value specified is less than or equal to zero and is not Infinite");
				}
				this.socket.SendTimeout = value;
			}
		}

		/// <summary>Begins an asynchronous read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data. </param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes. </param>
		/// <param name="state">An object that contains any additional user-defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of the <paramref name="buffer" /> paramater.-or- The <paramref name="size" /> is less than 0.-or- The <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- There was a failure while reading from the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060022F0 RID: 8944 RVA: 0x000664E0 File Offset: 0x000646E0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is null");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || offset + size > num)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			IAsyncResult result;
			try
			{
				result = this.socket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception innerException)
			{
				throw new IOException("BeginReceive failure", innerException);
			}
			return result;
		}

		/// <summary>Begins an asynchronous write to a stream.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin sending the data. </param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes. </param>
		/// <param name="state">An object that contains any additional user-defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- There was a failure while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060022F1 RID: 8945 RVA: 0x00066584 File Offset: 0x00064784
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is null");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || offset + size > num)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			IAsyncResult result;
			try
			{
				result = this.socket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch
			{
				throw new IOException("BeginWrite failure");
			}
			return result;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		// Token: 0x060022F2 RID: 8946 RVA: 0x00066628 File Offset: 0x00064828
		~NetworkStream()
		{
			this.Dispose(false);
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.NetworkStream" /> after waiting the specified time to allow data to be sent.</summary>
		/// <param name="timeout">A 32-bit signed integer that specifies the number of milliseconds to wait to send any remaining data before closing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		// Token: 0x060022F3 RID: 8947 RVA: 0x00066664 File Offset: 0x00064864
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout is less than -1");
			}
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += this.OnTimeoutClose;
			timer.Interval = (double)timeout;
			timer.AutoReset = false;
			timer.Enabled = true;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000666B8 File Offset: 0x000648B8
		private void OnTimeoutClose(object source, System.Timers.ElapsedEventArgs e)
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.NetworkStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060022F5 RID: 8949 RVA: 0x000666C0 File Offset: 0x000648C0
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.owns_socket)
			{
				Socket socket = this.socket;
				if (socket != null)
				{
					socket.Close();
				}
			}
			this.socket = null;
			this.access = (FileAccess)0;
		}

		/// <summary>Handles the end of an asynchronous read.</summary>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that represents an asynchronous call. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022F6 RID: 8950 RVA: 0x0006670C File Offset: 0x0006490C
		public override int EndRead(IAsyncResult ar)
		{
			this.CheckDisposed();
			if (ar == null)
			{
				throw new ArgumentNullException("async result is null");
			}
			int result;
			try
			{
				result = this.socket.EndReceive(ar);
			}
			catch (Exception innerException)
			{
				throw new IOException("EndRead failure", innerException);
			}
			return result;
		}

		/// <summary>Handles the end of an asynchronous write.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents the asynchronous call. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- An error occurred while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022F7 RID: 8951 RVA: 0x00066774 File Offset: 0x00064974
		public override void EndWrite(IAsyncResult ar)
		{
			this.CheckDisposed();
			if (ar == null)
			{
				throw new ArgumentNullException("async result is null");
			}
			try
			{
				this.socket.EndSend(ar);
			}
			catch (Exception innerException)
			{
				throw new IOException("EndWrite failure", innerException);
			}
		}

		/// <summary>Flushes data from the stream. This method is reserved for future use.</summary>
		// Token: 0x060022F8 RID: 8952 RVA: 0x000667D8 File Offset: 0x000649D8
		public override void Flush()
		{
		}

		/// <summary>Reads data from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data to. </param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.-or- There is a failure reading from the network. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022F9 RID: 8953 RVA: 0x000667DC File Offset: 0x000649DC
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is null");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			int result;
			try
			{
				result = this.socket.Receive(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception innerException)
			{
				throw new IOException("Read failure", innerException);
			}
			return result;
		}

		/// <summary>Sets the current position of the stream to the given value. This method is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The position in the stream.</returns>
		/// <param name="offset">This parameter is not used. </param>
		/// <param name="origin">This parameter is not used. </param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		// Token: 0x060022FA RID: 8954 RVA: 0x0006687C File Offset: 0x00064A7C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the length of the stream. This method always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">This parameter is not used. </param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		// Token: 0x060022FB RID: 8955 RVA: 0x00066884 File Offset: 0x00064A84
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Writes data to the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> from which to start writing data. </param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">There was a failure while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.-or- There was a failure reading from the network. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022FC RID: 8956 RVA: 0x0006688C File Offset: 0x00064A8C
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			try
			{
				int num = 0;
				while (size - num > 0)
				{
					num += this.socket.Send(buffer, offset + num, size - num, SocketFlags.None);
				}
			}
			catch (Exception innerException)
			{
				throw new IOException("Write failure", innerException);
			}
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00066944 File Offset: 0x00064B44
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04001584 RID: 5508
		private FileAccess access;

		// Token: 0x04001585 RID: 5509
		private Socket socket;

		// Token: 0x04001586 RID: 5510
		private bool owns_socket;

		// Token: 0x04001587 RID: 5511
		private bool readable;

		// Token: 0x04001588 RID: 5512
		private bool writeable;

		// Token: 0x04001589 RID: 5513
		private bool disposed;
	}
}
