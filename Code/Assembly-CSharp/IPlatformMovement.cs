using System;
using UnityEngine;

// Token: 0x020006DC RID: 1756
public interface IPlatformMovement
{
	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x060029FB RID: 10747
	bool IsOnGround { get; }

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x060029FC RID: 10748
	bool HasWallLeft { get; }

	// Token: 0x170006A4 RID: 1700
	// (get) Token: 0x060029FD RID: 10749
	bool HasWallRight { get; }

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x060029FE RID: 10750
	bool IsOnWall { get; }

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x060029FF RID: 10751
	bool MovingHorizontally { get; }

	// Token: 0x170006A7 RID: 1703
	// (get) Token: 0x06002A00 RID: 10752
	bool Jumping { get; }

	// Token: 0x170006A8 RID: 1704
	// (get) Token: 0x06002A01 RID: 10753
	// (set) Token: 0x06002A02 RID: 10754
	Vector2 LocalSpeed { get; set; }

	// Token: 0x170006A9 RID: 1705
	// (get) Token: 0x06002A03 RID: 10755
	// (set) Token: 0x06002A04 RID: 10756
	Vector2 WorldSpeed { get; set; }

	// Token: 0x170006AA RID: 1706
	// (get) Token: 0x06002A05 RID: 10757
	// (set) Token: 0x06002A06 RID: 10758
	float GravityAngle { get; set; }

	// Token: 0x170006AB RID: 1707
	// (get) Token: 0x06002A07 RID: 10759
	float GroundAngle { get; }

	// Token: 0x170006AC RID: 1708
	// (get) Token: 0x06002A08 RID: 10760
	// (set) Token: 0x06002A09 RID: 10761
	Vector3 Position { get; set; }

	// Token: 0x170006AD RID: 1709
	// (get) Token: 0x06002A0A RID: 10762
	// (set) Token: 0x06002A0B RID: 10763
	Vector3 FeetPosition { get; set; }
}
