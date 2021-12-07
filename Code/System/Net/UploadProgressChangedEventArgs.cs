using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020004C8 RID: 1224
	public class UploadProgressChangedEventArgs : System.ComponentModel.ProgressChangedEventArgs
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x00098CDC File Offset: 0x00096EDC
		internal UploadProgressChangedEventArgs(long bytesReceived, long totalBytesToReceive, long bytesSent, long totalBytesToSend, int progressPercentage, object userState) : base(progressPercentage, userState)
		{
			this.received = bytesReceived;
			this.total_recv = totalBytesToReceive;
			this.sent = bytesSent;
			this.total_send = totalBytesToSend;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x00098D08 File Offset: 0x00096F08
		public long BytesReceived
		{
			get
			{
				return this.received;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data upload operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x00098D10 File Offset: 0x00096F10
		public long TotalBytesToReceive
		{
			get
			{
				return this.total_recv;
			}
		}

		/// <summary>Gets the number of bytes sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes sent.</returns>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x00098D18 File Offset: 0x00096F18
		public long BytesSent
		{
			get
			{
				return this.sent;
			}
		}

		/// <summary>Gets the total number of bytes to send.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be sent.</returns>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x00098D20 File Offset: 0x00096F20
		public long TotalBytesToSend
		{
			get
			{
				return this.total_send;
			}
		}

		// Token: 0x04001B9B RID: 7067
		private long received;

		// Token: 0x04001B9C RID: 7068
		private long sent;

		// Token: 0x04001B9D RID: 7069
		private long total_recv;

		// Token: 0x04001B9E RID: 7070
		private long total_send;
	}
}
