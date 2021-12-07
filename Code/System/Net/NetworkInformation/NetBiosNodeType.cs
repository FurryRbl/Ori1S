using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x020003A1 RID: 929
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x040013CA RID: 5066
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x040013CB RID: 5067
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x040013CC RID: 5068
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x040013CD RID: 5069
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x040013CE RID: 5070
		Hybrid = 8
	}
}
