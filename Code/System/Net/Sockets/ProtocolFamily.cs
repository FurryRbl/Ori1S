using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the type of protocol that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020003F2 RID: 1010
	public enum ProtocolFamily
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x0400158B RID: 5515
		Unknown = -1,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x0400158C RID: 5516
		Unspecified,
		/// <summary>Unix local to host protocol.</summary>
		// Token: 0x0400158D RID: 5517
		Unix,
		/// <summary>IP version 4 protocol.</summary>
		// Token: 0x0400158E RID: 5518
		InterNetwork,
		/// <summary>ARPANET IMP protocol.</summary>
		// Token: 0x0400158F RID: 5519
		ImpLink,
		/// <summary>PUP protocol.</summary>
		// Token: 0x04001590 RID: 5520
		Pup,
		/// <summary>MIT CHAOS protocol.</summary>
		// Token: 0x04001591 RID: 5521
		Chaos,
		/// <summary>IPX or SPX protocol.</summary>
		// Token: 0x04001592 RID: 5522
		Ipx,
		/// <summary>ISO protocol.</summary>
		// Token: 0x04001593 RID: 5523
		Iso,
		/// <summary>European Computer Manufacturers Association (ECMA) protocol.</summary>
		// Token: 0x04001594 RID: 5524
		Ecma,
		/// <summary>DataKit protocol.</summary>
		// Token: 0x04001595 RID: 5525
		DataKit,
		/// <summary>CCITT protocol, such as X.25.</summary>
		// Token: 0x04001596 RID: 5526
		Ccitt,
		/// <summary>IBM SNA protocol.</summary>
		// Token: 0x04001597 RID: 5527
		Sna,
		/// <summary>DECNet protocol.</summary>
		// Token: 0x04001598 RID: 5528
		DecNet,
		/// <summary>Direct data link protocol.</summary>
		// Token: 0x04001599 RID: 5529
		DataLink,
		/// <summary>LAT protocol.</summary>
		// Token: 0x0400159A RID: 5530
		Lat,
		/// <summary>NSC HyperChannel protocol.</summary>
		// Token: 0x0400159B RID: 5531
		HyperChannel,
		/// <summary>AppleTalk protocol.</summary>
		// Token: 0x0400159C RID: 5532
		AppleTalk,
		/// <summary>NetBIOS protocol.</summary>
		// Token: 0x0400159D RID: 5533
		NetBios,
		/// <summary>VoiceView protocol.</summary>
		// Token: 0x0400159E RID: 5534
		VoiceView,
		/// <summary>FireFox protocol.</summary>
		// Token: 0x0400159F RID: 5535
		FireFox,
		/// <summary>Banyan protocol.</summary>
		// Token: 0x040015A0 RID: 5536
		Banyan = 21,
		/// <summary>Native ATM services protocol.</summary>
		// Token: 0x040015A1 RID: 5537
		Atm,
		/// <summary>IP version 6 protocol.</summary>
		// Token: 0x040015A2 RID: 5538
		InterNetworkV6,
		/// <summary>Microsoft Cluster products protocol.</summary>
		// Token: 0x040015A3 RID: 5539
		Cluster,
		/// <summary>IEEE 1284.4 workgroup protocol.</summary>
		// Token: 0x040015A4 RID: 5540
		Ieee12844,
		/// <summary>IrDA protocol.</summary>
		// Token: 0x040015A5 RID: 5541
		Irda,
		/// <summary>Network Designers OSI gateway enabled protocol.</summary>
		// Token: 0x040015A6 RID: 5542
		NetworkDesigners = 28,
		/// <summary>MAX protocol.</summary>
		// Token: 0x040015A7 RID: 5543
		Max,
		/// <summary>Xerox NS protocol.</summary>
		// Token: 0x040015A8 RID: 5544
		NS = 6,
		/// <summary>OSI protocol.</summary>
		// Token: 0x040015A9 RID: 5545
		Osi
	}
}
