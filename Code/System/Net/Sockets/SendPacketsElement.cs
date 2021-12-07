using System;

namespace System.Net.Sockets
{
	/// <summary>Represents an element in a <see cref="T:System.Net.Sockets.SendPacketsElement" /> array.</summary>
	// Token: 0x020003F5 RID: 1013
	public class SendPacketsElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		// Token: 0x060022FE RID: 8958 RVA: 0x00066964 File Offset: 0x00064B64
		public SendPacketsElement(byte[] buffer) : this(buffer, 0, (buffer == null) ? 0 : buffer.Length)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x060022FF RID: 8959 RVA: 0x00066980 File Offset: 0x00064B80
		public SendPacketsElement(byte[] buffer, int offset, int count) : this(buffer, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport. </summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06002300 RID: 8960 RVA: 0x0006698C File Offset: 0x00064B8C
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || offset >= num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count >= num)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Count = count;
			this.EndOfPacket = endOfPacket;
			this.FilePath = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified file.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		// Token: 0x06002301 RID: 8961 RVA: 0x00066A0C File Offset: 0x00064C0C
		public SendPacketsElement(string filepath) : this(filepath, 0, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, offset, and count.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06002302 RID: 8962 RVA: 0x00066A18 File Offset: 0x00064C18
		public SendPacketsElement(string filepath, int offset, int count) : this(filepath, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport. </summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06002303 RID: 8963 RVA: 0x00066A24 File Offset: 0x00064C24
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket)
		{
			if (filepath == null)
			{
				throw new ArgumentNullException("filepath");
			}
			this.Buffer = null;
			this.Offset = offset;
			this.Count = count;
			this.EndOfPacket = endOfPacket;
			this.FilePath = filepath;
		}

		/// <summary>Gets the buffer to be sent if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</summary>
		/// <returns>The byte buffer to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x00066A6C File Offset: 0x00064C6C
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x00066A74 File Offset: 0x00064C74
		public byte[] Buffer { get; private set; }

		/// <summary>Gets the count of bytes to be sent. </summary>
		/// <returns>The count of bytes to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="count" /> parameter.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x00066A80 File Offset: 0x00064C80
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x00066A88 File Offset: 0x00064C88
		public int Count { get; private set; }

		/// <summary>Gets a Boolean value that indicates if this element should not be combined with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <returns>A Boolean value that indicates if this element should not be combined with the next element in a single send request.</returns>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00066A94 File Offset: 0x00064C94
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x00066A9C File Offset: 0x00064C9C
		public bool EndOfPacket { get; private set; }

		/// <summary>Gets the filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</summary>
		/// <returns>The filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</returns>
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x00066AA8 File Offset: 0x00064CA8
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x00066AB0 File Offset: 0x00064CB0
		public string FilePath { get; private set; }

		/// <summary>Gets the offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data. </summary>
		/// <returns>The offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</returns>
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x00066ABC File Offset: 0x00064CBC
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x00066AC4 File Offset: 0x00064CC4
		public int Offset { get; private set; }
	}
}
