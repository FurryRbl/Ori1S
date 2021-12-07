using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020004C5 RID: 1221
	public class DownloadProgressChangedEventArgs : System.ComponentModel.ProgressChangedEventArgs
	{
		// Token: 0x06002BCB RID: 11211 RVA: 0x00098C64 File Offset: 0x00096E64
		internal DownloadProgressChangedEventArgs(long bytesReceived, long totalBytesToReceive, object userState) : base((totalBytesToReceive == -1L) ? 0 : ((int)(bytesReceived * 100L / totalBytesToReceive)), userState)
		{
			this.received = bytesReceived;
			this.total = totalBytesToReceive;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x00098C94 File Offset: 0x00096E94
		public long BytesReceived
		{
			get
			{
				return this.received;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data download operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x00098C9C File Offset: 0x00096E9C
		public long TotalBytesToReceive
		{
			get
			{
				return this.total;
			}
		}

		// Token: 0x04001B97 RID: 7063
		private long received;

		// Token: 0x04001B98 RID: 7064
		private long total;
	}
}
