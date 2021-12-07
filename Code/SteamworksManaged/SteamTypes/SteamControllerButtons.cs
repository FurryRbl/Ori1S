using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000152 RID: 338
	[Flags]
	public enum SteamControllerButtons : ulong
	{
		// Token: 0x040005F6 RID: 1526
		RightTrigger = 1UL,
		// Token: 0x040005F7 RID: 1527
		LeftTrigger = 2UL,
		// Token: 0x040005F8 RID: 1528
		RightBumper = 4UL,
		// Token: 0x040005F9 RID: 1529
		LeftBumper = 8UL,
		// Token: 0x040005FA RID: 1530
		Button0 = 16UL,
		// Token: 0x040005FB RID: 1531
		Button1 = 32UL,
		// Token: 0x040005FC RID: 1532
		Button2 = 64UL,
		// Token: 0x040005FD RID: 1533
		Button3 = 128UL,
		// Token: 0x040005FE RID: 1534
		Touch0 = 256UL,
		// Token: 0x040005FF RID: 1535
		Touch1 = 512UL,
		// Token: 0x04000600 RID: 1536
		Touch2 = 1024UL,
		// Token: 0x04000601 RID: 1537
		Touch3 = 2048UL,
		// Token: 0x04000602 RID: 1538
		ButtonMenu = 4096UL,
		// Token: 0x04000603 RID: 1539
		ButtonSteam = 8192UL,
		// Token: 0x04000604 RID: 1540
		ButtonEscape = 16384UL,
		// Token: 0x04000605 RID: 1541
		ButtonBackLeft = 32768UL,
		// Token: 0x04000606 RID: 1542
		ButtonBackRight = 65536UL,
		// Token: 0x04000607 RID: 1543
		ButtonLeftpadClicked = 131072UL,
		// Token: 0x04000608 RID: 1544
		ButtonRightpadClicked = 262144UL,
		// Token: 0x04000609 RID: 1545
		LeftpadFingerDown = 524288UL,
		// Token: 0x0400060A RID: 1546
		RightpadFingerDown = 1048576UL
	}
}
