using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net.Sockets
{
	/// <summary>Represents an asynchronous socket operation.</summary>
	// Token: 0x020003FD RID: 1021
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		/// <summary>Creates an empty <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The platform is not supported. </exception>
		// Token: 0x060023F3 RID: 9203 RVA: 0x0006C208 File Offset: 0x0006A408
		public SocketAsyncEventArgs()
		{
			this.AcceptSocket = null;
			this.Buffer = null;
			this.BufferList = null;
			this.BytesTransferred = 0;
			this.Count = 0;
			this.DisconnectReuseSocket = false;
			this.LastOperation = SocketAsyncOperation.None;
			this.Offset = 0;
			this.RemoteEndPoint = null;
			this.SendPacketsElements = null;
			this.SendPacketsFlags = TransmitFileOptions.UseDefaultWorkerThread;
			this.SendPacketsSendSize = -1;
			this.SocketError = SocketError.Success;
			this.SocketFlags = SocketFlags.None;
			this.UserToken = null;
		}

		/// <summary>The event used to complete an asynchronous operation.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060023F4 RID: 9204 RVA: 0x0006C284 File Offset: 0x0006A484
		// (remove) Token: 0x060023F5 RID: 9205 RVA: 0x0006C2A0 File Offset: 0x0006A4A0
		public event EventHandler<SocketAsyncEventArgs> Completed;

		/// <summary>Gets or sets the socket to use or the socket created for accepting a connection with an asynchronous socket method.</summary>
		/// <returns>The <see cref="T:System.Net.Sockets.Socket" /> to use or the socket created for accepting a connection with an asynchronous socket method.</returns>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0006C2BC File Offset: 0x0006A4BC
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
		public Socket AcceptSocket { get; set; }

		/// <summary>Gets the data buffer to use with an asynchronous socket method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that represents the data buffer to use with an asynchronous socket method.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x0006C2D0 File Offset: 0x0006A4D0
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x0006C2D8 File Offset: 0x0006A4D8
		public byte[] Buffer { get; private set; }

		/// <summary>Gets or sets an array of data buffers to use with an asynchronous socket method.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents an array of data buffers to use with an asynchronous socket method.</returns>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified on a set operation. This exception occurs if a value other than null is passed and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null.</exception>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0006C2E4 File Offset: 0x0006A4E4
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x0006C2EC File Offset: 0x0006A4EC
		[MonoTODO("not supported in all cases")]
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this._bufferList;
			}
			set
			{
				if (this.Buffer != null && value != null)
				{
					throw new ArgumentException("Buffer and BufferList properties cannot both be non-null.");
				}
				this._bufferList = value;
			}
		}

		/// <summary>Gets the number of bytes transferred in the socket operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of bytes transferred in the socket operation.</returns>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x0006C314 File Offset: 0x0006A514
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x0006C31C File Offset: 0x0006A51C
		public int BytesTransferred { get; private set; }

		/// <summary>Gets the maximum amount of data, in bytes, to send or receive in an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the maximum amount of data, in bytes, to send or receive.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x0006C328 File Offset: 0x0006A528
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x0006C330 File Offset: 0x0006A530
		public int Count { get; private set; }

		/// <summary>Gets or sets a value that specifies if socket can be reused after a disconnect operation.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that specifies if socket can be reused after a disconnect operation.</returns>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x0006C33C File Offset: 0x0006A53C
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x0006C344 File Offset: 0x0006A544
		public bool DisconnectReuseSocket { get; set; }

		/// <summary>Gets the type of socket operation most recently performed with this context object.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketAsyncOperation" /> instance that indicates the type of socket operation most recently performed with this context object.</returns>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x0006C350 File Offset: 0x0006A550
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x0006C358 File Offset: 0x0006A558
		public SocketAsyncOperation LastOperation { get; private set; }

		/// <summary>Gets the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</returns>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0006C364 File Offset: 0x0006A564
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x0006C36C File Offset: 0x0006A56C
		public int Offset { get; private set; }

		/// <summary>Gets or sets the remote IP endpoint for an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> that represents the remote IP endpoint for an asynchronous operation.</returns>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0006C378 File Offset: 0x0006A578
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x0006C380 File Offset: 0x0006A580
		public EndPoint RemoteEndPoint { get; set; }

		/// <summary>Gets the IP address and interface of a received packet.</summary>
		/// <returns>An <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that contains the IP address and interface of a received packet.</returns>
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0006C38C File Offset: 0x0006A58C
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x0006C394 File Offset: 0x0006A594
		public IPPacketInformation ReceiveMessageFromPacketInfo { get; private set; }

		/// <summary>Gets or sets an array of buffers to be sent for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Net.Sockets.SendPacketsElement" /> objects that represent an array of buffers to be sent.</returns>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x0006C3A0 File Offset: 0x0006A5A0
		// (set) Token: 0x0600240B RID: 9227 RVA: 0x0006C3A8 File Offset: 0x0006A5A8
		public SendPacketsElement[] SendPacketsElements { get; set; }

		/// <summary>Gets or sets a bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TransmitFileOptions" /> that contains a bitwise combination of values that are used with an asynchronous operation.</returns>
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x0006C3B4 File Offset: 0x0006A5B4
		// (set) Token: 0x0600240D RID: 9229 RVA: 0x0006C3BC File Offset: 0x0006A5BC
		public TransmitFileOptions SendPacketsFlags { get; set; }

		/// <summary>Gets or sets the size, in bytes, of the data block used in the send operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the data block used in the send operation.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x0006C3C8 File Offset: 0x0006A5C8
		// (set) Token: 0x0600240F RID: 9231 RVA: 0x0006C3D0 File Offset: 0x0006A5D0
		[MonoTODO("unused property")]
		public int SendPacketsSendSize { get; set; }

		/// <summary>Gets or sets the result of the asynchronous socket operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketError" /> that represents the result of the asynchronous socket operation.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x0006C3DC File Offset: 0x0006A5DC
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x0006C3E4 File Offset: 0x0006A5E4
		public SocketError SocketError { get; set; }

		/// <summary>Gets the results of an asynchronous socket operation or sets the behavior of an asynchronous operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketFlags" /> that represents the results of an asynchronous socket operation.</returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0006C3F0 File Offset: 0x0006A5F0
		// (set) Token: 0x06002413 RID: 9235 RVA: 0x0006C3F8 File Offset: 0x0006A5F8
		public SocketFlags SocketFlags { get; set; }

		/// <summary>Gets or sets a user or application object associated with this asynchronous socket operation.</summary>
		/// <returns>An object that represents the user or application object associated with this asynchronous socket operation.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0006C404 File Offset: 0x0006A604
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x0006C40C File Offset: 0x0006A60C
		public object UserToken { get; set; }

		/// <summary>Frees resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> class.</summary>
		// Token: 0x06002416 RID: 9238 RVA: 0x0006C418 File Offset: 0x0006A618
		~SocketAsyncEventArgs()
		{
			this.Dispose(false);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x0006C454 File Offset: 0x0006A654
		private void Dispose(bool disposing)
		{
			Socket acceptSocket = this.AcceptSocket;
			if (acceptSocket != null)
			{
				acceptSocket.Close();
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance and optionally disposes of the managed resources.</summary>
		// Token: 0x06002418 RID: 9240 RVA: 0x0006C480 File Offset: 0x0006A680
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Represents a method that is called when an asynchronous operation completes.</summary>
		/// <param name="e">The event that is signaled.</param>
		// Token: 0x06002419 RID: 9241 RVA: 0x0006C48C File Offset: 0x0006A68C
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				return;
			}
			EventHandler<SocketAsyncEventArgs> completed = e.Completed;
			if (completed != null)
			{
				completed(e.curSocket, e);
			}
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is also not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x0600241A RID: 9242 RVA: 0x0006C4BC File Offset: 0x0006A6BC
		public void SetBuffer(int offset, int count)
		{
			this.SetBufferInternal(this.Buffer, offset, count);
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="buffer">The data buffer to use with an asynchronous socket method.</param>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is also not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x0600241B RID: 9243 RVA: 0x0006C4CC File Offset: 0x0006A6CC
		public void SetBuffer(byte[] buffer, int offset, int count)
		{
			this.SetBufferInternal(buffer, offset, count);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0006C4D8 File Offset: 0x0006A6D8
		private void SetBufferInternal(byte[] buffer, int offset, int count)
		{
			if (buffer != null)
			{
				if (this.BufferList != null)
				{
					throw new ArgumentException("Buffer and BufferList properties cannot both be non-null.");
				}
				int num = buffer.Length;
				if (offset < 0 || (offset != 0 && offset >= num))
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.Count = count;
				this.Offset = offset;
			}
			this.Buffer = buffer;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0006C554 File Offset: 0x0006A754
		private void ReceiveCallback()
		{
			this.SocketError = SocketError.Success;
			this.LastOperation = SocketAsyncOperation.Receive;
			SocketError socketError = SocketError.Success;
			if (!this.curSocket.Connected)
			{
				this.SocketError = SocketError.NotConnected;
				return;
			}
			try
			{
				this.BytesTransferred = this.curSocket.Receive_nochecks(this.Buffer, this.Offset, this.Count, this.SocketFlags, out socketError);
			}
			finally
			{
				this.SocketError = socketError;
				this.OnCompleted(this);
			}
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0006C5E8 File Offset: 0x0006A7E8
		private void ConnectCallback()
		{
			this.LastOperation = SocketAsyncOperation.Connect;
			SocketError socketError = SocketError.AccessDenied;
			try
			{
				socketError = this.TryConnect(this.RemoteEndPoint);
			}
			finally
			{
				this.SocketError = socketError;
				this.OnCompleted(this);
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0006C640 File Offset: 0x0006A840
		private SocketError TryConnect(EndPoint endpoint)
		{
			this.curSocket.Connected = false;
			SocketError result = SocketError.Success;
			try
			{
				if (!this.curSocket.Blocking)
				{
					int num;
					this.curSocket.Poll(-1, SelectMode.SelectWrite, out num);
					result = (SocketError)num;
					if (num != 0)
					{
						return result;
					}
					this.curSocket.Connected = true;
				}
				else
				{
					this.curSocket.seed_endpoint = endpoint;
					this.curSocket.Connect(endpoint);
					this.curSocket.Connected = true;
				}
			}
			catch (SocketException ex)
			{
				result = ex.SocketErrorCode;
			}
			return result;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x0006C6F8 File Offset: 0x0006A8F8
		private void SendCallback()
		{
			this.SocketError = SocketError.Success;
			this.LastOperation = SocketAsyncOperation.Send;
			SocketError socketError = SocketError.Success;
			if (!this.curSocket.Connected)
			{
				this.SocketError = SocketError.NotConnected;
				return;
			}
			try
			{
				if (this.Buffer != null)
				{
					this.BytesTransferred = this.curSocket.Send_nochecks(this.Buffer, this.Offset, this.Count, SocketFlags.None, out socketError);
				}
				else if (this.BufferList != null)
				{
					this.BytesTransferred = 0;
					foreach (ArraySegment<byte> arraySegment in this.BufferList)
					{
						this.BytesTransferred += this.curSocket.Send_nochecks(arraySegment.Array, arraySegment.Offset, arraySegment.Count, SocketFlags.None, out socketError);
						if (socketError != SocketError.Success)
						{
							break;
						}
					}
				}
			}
			finally
			{
				this.SocketError = socketError;
				this.OnCompleted(this);
			}
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0006C830 File Offset: 0x0006AA30
		private void AcceptCallback()
		{
			this.SocketError = SocketError.Success;
			this.LastOperation = SocketAsyncOperation.Accept;
			try
			{
				this.curSocket.Accept(this.AcceptSocket);
			}
			catch (SocketException ex)
			{
				this.SocketError = ex.SocketErrorCode;
				throw;
			}
			finally
			{
				this.OnCompleted(this);
			}
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0006C8B4 File Offset: 0x0006AAB4
		private void DisconnectCallback()
		{
			this.SocketError = SocketError.Success;
			this.LastOperation = SocketAsyncOperation.Disconnect;
			try
			{
				this.curSocket.Disconnect(this.DisconnectReuseSocket);
			}
			catch (SocketException ex)
			{
				this.SocketError = ex.SocketErrorCode;
				throw;
			}
			finally
			{
				this.OnCompleted(this);
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0006C938 File Offset: 0x0006AB38
		private void ReceiveFromCallback()
		{
			this.SocketError = SocketError.Success;
			this.LastOperation = SocketAsyncOperation.ReceiveFrom;
			try
			{
				EndPoint remoteEndPoint = this.RemoteEndPoint;
				if (this.Buffer != null)
				{
					this.BytesTransferred = this.curSocket.ReceiveFrom_nochecks(this.Buffer, this.Offset, this.Count, this.SocketFlags, ref remoteEndPoint);
				}
				else if (this.BufferList != null)
				{
					throw new NotImplementedException();
				}
			}
			catch (SocketException ex)
			{
				this.SocketError = ex.SocketErrorCode;
				throw;
			}
			finally
			{
				this.OnCompleted(this);
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0006C9FC File Offset: 0x0006ABFC
		private void SendToCallback()
		{
			this.SocketError = SocketError.Success;
			this.LastOperation = SocketAsyncOperation.SendTo;
			int i = 0;
			try
			{
				int count = this.Count;
				while (i < count)
				{
					i += this.curSocket.SendTo_nochecks(this.Buffer, this.Offset, count, this.SocketFlags, this.RemoteEndPoint);
				}
				this.BytesTransferred = i;
			}
			catch (SocketException ex)
			{
				this.SocketError = ex.SocketErrorCode;
				throw;
			}
			finally
			{
				this.OnCompleted(this);
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0006CAB0 File Offset: 0x0006ACB0
		internal void DoOperation(SocketAsyncOperation operation, Socket socket)
		{
			this.curSocket = socket;
			ThreadStart start;
			switch (operation)
			{
			case SocketAsyncOperation.Accept:
				start = new ThreadStart(this.AcceptCallback);
				goto IL_BE;
			case SocketAsyncOperation.Connect:
				start = new ThreadStart(this.ConnectCallback);
				goto IL_BE;
			case SocketAsyncOperation.Disconnect:
				start = new ThreadStart(this.DisconnectCallback);
				goto IL_BE;
			case SocketAsyncOperation.Receive:
				start = new ThreadStart(this.ReceiveCallback);
				goto IL_BE;
			case SocketAsyncOperation.ReceiveFrom:
				start = new ThreadStart(this.ReceiveFromCallback);
				goto IL_BE;
			case SocketAsyncOperation.Send:
				start = new ThreadStart(this.SendCallback);
				goto IL_BE;
			case SocketAsyncOperation.SendTo:
				start = new ThreadStart(this.SendToCallback);
				goto IL_BE;
			}
			throw new NotSupportedException();
			IL_BE:
			new Thread(start)
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x04001615 RID: 5653
		private IList<ArraySegment<byte>> _bufferList;

		// Token: 0x04001616 RID: 5654
		private Socket curSocket;
	}
}
