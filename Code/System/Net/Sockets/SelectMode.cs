using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x020003F4 RID: 1012
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x040015C5 RID: 5573
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x040015C6 RID: 5574
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x040015C7 RID: 5575
		SelectError
	}
}
