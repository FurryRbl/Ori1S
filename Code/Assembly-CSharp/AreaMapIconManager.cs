using System;
using UnityEngine;

// Token: 0x02000874 RID: 2164
public class AreaMapIconManager : MonoBehaviour
{
	// Token: 0x060030E9 RID: 12521 RVA: 0x000D0529 File Offset: 0x000CE729
	public void Awake()
	{
	}

	// Token: 0x060030EA RID: 12522 RVA: 0x000D052C File Offset: 0x000CE72C
	public void ShowAreaIcons()
	{
		for (int i = 0; i < GameWorld.Instance.RuntimeAreas.Count; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = GameWorld.Instance.RuntimeAreas[i];
			for (int j = 0; j < runtimeGameWorldArea.Icons.Count; j++)
			{
				RuntimeWorldMapIcon runtimeWorldMapIcon = runtimeGameWorldArea.Icons[j];
				runtimeWorldMapIcon.Hide();
			}
			if (!runtimeGameWorldArea.Area.VisitableCondition || runtimeGameWorldArea.Area.VisitableCondition.Validate(null))
			{
				for (int k = 0; k < runtimeGameWorldArea.Icons.Count; k++)
				{
					RuntimeWorldMapIcon runtimeWorldMapIcon2 = runtimeGameWorldArea.Icons[k];
					if (!GameMapUI.Instance.ShowingTeleporters || runtimeWorldMapIcon2.Icon != WorldMapIconType.SavePedestal)
					{
						runtimeWorldMapIcon2.Show();
					}
				}
			}
		}
	}

	// Token: 0x060030EB RID: 12523 RVA: 0x000D061C File Offset: 0x000CE81C
	public GameObject GetIcon(WorldMapIconType iconType)
	{
		switch (iconType)
		{
		case WorldMapIconType.Keystone:
			return this.Icons.Keystone;
		case WorldMapIconType.Mapstone:
			return this.Icons.Mapstone;
		case WorldMapIconType.BreakableWall:
			return this.Icons.BreakableWall;
		case WorldMapIconType.BreakableWallBroken:
			return this.Icons.BreakableWallBroken;
		case WorldMapIconType.StompableFloor:
			return this.Icons.StompableFloor;
		case WorldMapIconType.StompableFloorBroken:
			return this.Icons.StompableFloorBroken;
		case WorldMapIconType.EnergyGateTwo:
			return this.Icons.EnergyGateTwo;
		case WorldMapIconType.EnergyGateOpen:
			return this.Icons.EnergyGateOpen;
		case WorldMapIconType.KeystoneDoorFour:
			return this.Icons.KeystoneDoorFour;
		case WorldMapIconType.KeystoneDoorOpen:
			return this.Icons.KeystoneDoorOpen;
		case WorldMapIconType.AbilityPedestal:
			return this.Icons.AbilityPedestal;
		case WorldMapIconType.HealthUpgrade:
			return this.Icons.HealthUpgrade;
		case WorldMapIconType.EnergyUpgrade:
			return this.Icons.EnergyUpgrade;
		case WorldMapIconType.SavePedestal:
			return this.Icons.SavePedestal;
		case WorldMapIconType.AbilityPoint:
			return this.Icons.AbilityPoint;
		case WorldMapIconType.KeystoneDoorTwo:
			return this.Icons.KeystoneDoorTwo;
		case WorldMapIconType.Experience:
			return this.Icons.Experience;
		case WorldMapIconType.MapstonePickup:
			return this.Icons.MapstonePickup;
		case WorldMapIconType.EnergyGateTwelve:
			return this.Icons.EnergyGateTwelve;
		case WorldMapIconType.EnergyGateTen:
			return this.Icons.EnergyGateTen;
		case WorldMapIconType.EnergyGateEight:
			return this.Icons.EnergyGateEight;
		case WorldMapIconType.EnergyGateSix:
			return this.Icons.EnergyGateSix;
		case WorldMapIconType.EnergyGateFour:
			return this.Icons.EnergyGateFour;
		}
		return null;
	}

	// Token: 0x04002C2E RID: 11310
	public AreaMapIconManager.IconGameObjects Icons;

	// Token: 0x02000875 RID: 2165
	[Serializable]
	public class IconGameObjects
	{
		// Token: 0x04002C2F RID: 11311
		public GameObject Keystone;

		// Token: 0x04002C30 RID: 11312
		public GameObject Mapstone;

		// Token: 0x04002C31 RID: 11313
		public GameObject BreakableWall;

		// Token: 0x04002C32 RID: 11314
		public GameObject BreakableWallBroken;

		// Token: 0x04002C33 RID: 11315
		public GameObject StompableFloor;

		// Token: 0x04002C34 RID: 11316
		public GameObject StompableFloorBroken;

		// Token: 0x04002C35 RID: 11317
		public GameObject EnergyGateOpen;

		// Token: 0x04002C36 RID: 11318
		public GameObject KeystoneDoorTwo;

		// Token: 0x04002C37 RID: 11319
		public GameObject KeystoneDoorFour;

		// Token: 0x04002C38 RID: 11320
		public GameObject KeystoneDoorOpen;

		// Token: 0x04002C39 RID: 11321
		public GameObject AbilityPedestal;

		// Token: 0x04002C3A RID: 11322
		public GameObject HealthUpgrade;

		// Token: 0x04002C3B RID: 11323
		public GameObject EnergyUpgrade;

		// Token: 0x04002C3C RID: 11324
		public GameObject SavePedestal;

		// Token: 0x04002C3D RID: 11325
		public GameObject AbilityPoint;

		// Token: 0x04002C3E RID: 11326
		public GameObject Experience;

		// Token: 0x04002C3F RID: 11327
		public GameObject MapstonePickup;

		// Token: 0x04002C40 RID: 11328
		public GameObject EnergyGateTwelve;

		// Token: 0x04002C41 RID: 11329
		public GameObject EnergyGateTen;

		// Token: 0x04002C42 RID: 11330
		public GameObject EnergyGateEight;

		// Token: 0x04002C43 RID: 11331
		public GameObject EnergyGateSix;

		// Token: 0x04002C44 RID: 11332
		public GameObject EnergyGateFour;

		// Token: 0x04002C45 RID: 11333
		public GameObject EnergyGateTwo;
	}
}
