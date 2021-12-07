using System;
using System.IO;

namespace System.Net.Security
{
	/// <summary>Provides methods for passing credentials across a stream and requesting or performing authentication for client-server applications.</summary>
	// Token: 0x020003DD RID: 989
	public abstract class AuthenticatedStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.AuthenticatedStream" /> class. </summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.AuthenticatedStream" />  for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A <see cref="T:System.Boolean" /> that indicates whether closing this <see cref="T:System.Net.Security.AuthenticatedStream" />  object also closes <paramref name="innerStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.-or-<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x060021DA RID: 8666 RVA: 0x00063224 File Offset: 0x00061424
		protected AuthenticatedStream(Stream innerStream, bool leaveInnerStreamOpen)
		{
			this.innerStream = innerStream;
			this.leaveStreamOpen = leaveInnerStreamOpen;
		}

		/// <summary>Gets the stream used by this <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x0006323C File Offset: 0x0006143C
		protected Stream InnerStream
		{
			get
			{
				return this.innerStream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>true if successful authentication occurred; otherwise, false. </returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060021DC RID: 8668
		public abstract bool IsAuthenticated { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether data sent using this <see cref="T:System.Net.Security.AuthenticatedStream" /> is encrypted.</summary>
		/// <returns>true if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise, false.</returns>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060021DD RID: 8669
		public abstract bool IsEncrypted { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both server and client have been authenticated.</summary>
		/// <returns>true if the client and server have been authenticated; otherwise, false.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060021DE RID: 8670
		public abstract bool IsMutuallyAuthenticated { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection was authenticated as the server.</summary>
		/// <returns>true if the local endpoint was authenticated as the server side of a client-server authenticated connection; false if the local endpoint was authenticated as the client.</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060021DF RID: 8671
		public abstract bool IsServer { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>true if the data is signed before being transmitted; otherwise, false.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060021E0 RID: 8672
		public abstract bool IsSigned { get; }

		/// <summary>Gets whether the stream used by this <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data has been left open.</summary>
		/// <returns>true if the inner stream has been left open; otherwise, false.</returns>
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00063244 File Offset: 0x00061444
		public bool LeaveInnerStreamOpen
		{
			get
			{
				return this.leaveStreamOpen;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.AuthenticatedStream" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060021E2 RID: 8674 RVA: 0x0006324C File Offset: 0x0006144C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.innerStream != null)
			{
				if (!this.leaveStreamOpen)
				{
					this.innerStream.Close();
				}
				this.innerStream = null;
			}
		}

		// Token: 0x040014F3 RID: 5363
		private Stream innerStream;

		// Token: 0x040014F4 RID: 5364
		private bool leaveStreamOpen;
	}
}
