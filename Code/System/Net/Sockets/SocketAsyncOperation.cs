using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x020003FE RID: 1022
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x04001628 RID: 5672
		None,
		/// <summary>A socket Accept operation. </summary>
		// Token: 0x04001629 RID: 5673
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x0400162A RID: 5674
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x0400162B RID: 5675
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x0400162C RID: 5676
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x0400162D RID: 5677
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x0400162E RID: 5678
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x0400162F RID: 5679
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x04001630 RID: 5680
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x04001631 RID: 5681
		SendTo
	}
}
