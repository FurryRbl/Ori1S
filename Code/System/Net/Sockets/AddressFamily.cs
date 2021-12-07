using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020003EB RID: 1003
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x04001539 RID: 5433
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x0400153A RID: 5434
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x0400153B RID: 5435
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x0400153C RID: 5436
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x0400153D RID: 5437
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x0400153E RID: 5438
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x0400153F RID: 5439
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x04001540 RID: 5440
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x04001541 RID: 5441
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x04001542 RID: 5442
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x04001543 RID: 5443
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x04001544 RID: 5444
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x04001545 RID: 5445
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x04001546 RID: 5446
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x04001547 RID: 5447
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x04001548 RID: 5448
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x04001549 RID: 5449
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x0400154A RID: 5450
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x0400154B RID: 5451
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x0400154C RID: 5452
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x0400154D RID: 5453
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x0400154E RID: 5454
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x0400154F RID: 5455
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x04001550 RID: 5456
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x04001551 RID: 5457
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x04001552 RID: 5458
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x04001553 RID: 5459
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x04001554 RID: 5460
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x04001555 RID: 5461
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x04001556 RID: 5462
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x04001557 RID: 5463
		Max
	}
}
