using System;
using Game;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public class WorldMapOverworldLogic : MonoBehaviour
{
	// Token: 0x0600316E RID: 12654 RVA: 0x000D2F11 File Offset: 0x000D1111
	public void OnGameReset()
	{
	}

	// Token: 0x0600316F RID: 12655 RVA: 0x000D2F13 File Offset: 0x000D1113
	public void Awake()
	{
		WorldMapOverworldLogic.Instance = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06003170 RID: 12656 RVA: 0x000D2F36 File Offset: 0x000D1136
	public void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x000D2F54 File Offset: 0x000D1154
	public Vector3 WorldToOverworld(Vector3 position)
	{
		Vector2 v = Vector2.zero;
		int i;
		for (i = 0; i < this.WorldCoordinates.Length; i++)
		{
			if (this.WorldCoordinates[i].IsInside(position))
			{
				v = this.WorldCoordinates[i].WorldToLocal(position);
				break;
			}
		}
		if (i < this.WorldCoordinates.Length)
		{
			return this.MapCoordinates[i].LocalToWorld(v);
		}
		position.x /= 155.57143f;
		position.y /= 144.375f;
		return position;
	}

	// Token: 0x04002CB2 RID: 11442
	public static WorldMapOverworldLogic Instance;

	// Token: 0x04002CB3 RID: 11443
	public WorldCoordinatesMap[] WorldCoordinates;

	// Token: 0x04002CB4 RID: 11444
	public WorldCoordinatesMap[] MapCoordinates;

	// Token: 0x04002CB5 RID: 11445
	public CageStructureTool CageStructureTool;
}
