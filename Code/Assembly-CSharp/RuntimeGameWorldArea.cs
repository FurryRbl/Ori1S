using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000149 RID: 329
public class RuntimeGameWorldArea
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x0003D779 File Offset: 0x0003B979
	public RuntimeGameWorldArea(GameWorldArea area)
	{
		this.Area = area;
		this.Initialize();
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0003D7A4 File Offset: 0x0003B9A4
	public Vector2 FindCenterPositionOnDiscoveredAreas()
	{
		int num = 0;
		Vector2 a = Vector2.zero;
		Rect[] facesAsRectangles = this.Area.CageStructureTool.FacesAsRectangles;
		for (int i = 0; i < this.Area.CageStructureTool.Faces.Count; i++)
		{
			CageStructureTool.Face face = this.Area.CageStructureTool.Faces[i];
			if (this.IsDiscovered(face))
			{
				Rect rect = facesAsRectangles[i];
				a += rect.center;
				num++;
			}
		}
		if (num > 0)
		{
			return a / (float)num;
		}
		return this.Area.BoundingRect.center;
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0003D85C File Offset: 0x0003BA5C
	public Vector2 FindCenterPositionOnUndiscoveredAreas()
	{
		int num = 0;
		Vector2 a = Vector2.zero;
		Rect[] facesAsRectangles = this.Area.CageStructureTool.FacesAsRectangles;
		for (int i = 0; i < this.Area.CageStructureTool.Faces.Count; i++)
		{
			CageStructureTool.Face face = this.Area.CageStructureTool.Faces[i];
			if (!this.IsDiscovered(face))
			{
				Rect rect = facesAsRectangles[i];
				a += rect.center;
				num++;
			}
		}
		if (num > 0)
		{
			return a / (float)num;
		}
		return this.Area.BoundingRect.center;
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0003D914 File Offset: 0x0003BB14
	public void Initialize()
	{
		this.m_dirtyCompletionAmount = true;
		this.Icons.Clear();
		this.Icons.Capacity = this.Area.Icons.Count;
		foreach (GameWorldArea.WorldMapIcon icon in this.Area.Icons)
		{
			this.Icons.Add(new RuntimeWorldMapIcon(icon, this));
		}
		this.m_worldAreaStates.Clear();
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0003D9B8 File Offset: 0x0003BBB8
	public bool AreaDiscovered
	{
		get
		{
			return this.m_worldAreaStates.Count > 0;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
	public float CompletionAmount
	{
		get
		{
			if (this.m_dirtyCompletionAmount)
			{
				this.m_dirtyCompletionAmount = false;
				this.UpdateCompletionAmount();
			}
			return this.m_completionAmount;
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0003D9E8 File Offset: 0x0003BBE8
	public void DirtyCompletionAmount()
	{
		this.m_dirtyCompletionAmount = true;
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
	public int CompletionPercentage
	{
		get
		{
			return Mathf.RoundToInt(this.CompletionAmount * 100f);
		}
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0003DA04 File Offset: 0x0003BC04
	private bool IconIsCompletionType(WorldMapIconType type)
	{
		switch (type)
		{
		case WorldMapIconType.HealthUpgrade:
		case WorldMapIconType.EnergyUpgrade:
		case WorldMapIconType.AbilityPoint:
		case WorldMapIconType.Experience:
		case WorldMapIconType.MapstonePickup:
			break;
		default:
			if (type != WorldMapIconType.Keystone)
			{
				return false;
			}
			break;
		}
		return true;
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0003DA4C File Offset: 0x0003BC4C
	public void UpdateCompletionAmount()
	{
		int count = this.Area.CageStructureTool.Faces.Count;
		if (count == 0)
		{
			return;
		}
		int num = 0;
		foreach (CageStructureTool.Face face in this.Area.CageStructureTool.Faces)
		{
			WorldMapAreaState worldMapAreaState;
			if (this.m_worldAreaStates.TryGetValue(face.ID, out worldMapAreaState) && worldMapAreaState == WorldMapAreaState.Visited)
			{
				num++;
			}
		}
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < this.Area.Icons.Count; i++)
		{
			if (this.IconIsCompletionType(this.Area.Icons[i].Icon))
			{
				num2++;
			}
			if (this.IconIsCompletionType(this.Icons[i].Icon))
			{
				num3++;
			}
		}
		this.m_completionAmount = (float)(num + (num2 - num3)) / (float)(count + num2);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0003DB78 File Offset: 0x0003BD78
	public void VisitMapAreaAtPosition(Vector3 worldPosition)
	{
		Vector3 position = this.Area.CageStructureTool.transform.InverseTransformPoint(worldPosition);
		CageStructureTool.Face face = this.Area.CageStructureTool.FindFaceAtPositionFaster(position);
		if (face != null)
		{
			WorldMapAreaState worldMapAreaState;
			if (this.m_worldAreaStates.TryGetValue(face.ID, out worldMapAreaState))
			{
				if (worldMapAreaState != WorldMapAreaState.Visited)
				{
					this.m_dirtyCompletionAmount = true;
					this.m_worldAreaStates[face.ID] = WorldMapAreaState.Visited;
				}
			}
			else
			{
				this.m_dirtyCompletionAmount = true;
				this.m_worldAreaStates[face.ID] = WorldMapAreaState.Visited;
			}
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0003DC0C File Offset: 0x0003BE0C
	private bool HasSenseAbility
	{
		get
		{
			return Characters.Sein && Characters.Sein.PlayerAbilities.Sense.HasAbility;
		}
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0003DC40 File Offset: 0x0003BE40
	public bool IsHidden(Vector3 worldPosition)
	{
		if (this.HasSenseAbility)
		{
			return false;
		}
		Vector3 position = this.Area.CageStructureTool.transform.InverseTransformPoint(worldPosition);
		CageStructureTool.Face face = this.Area.CageStructureTool.FindFaceAtPositionFaster(position);
		return face == null || this.IsHidden(face);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0003DC94 File Offset: 0x0003BE94
	public bool IsDiscovered(Vector3 worldPosition)
	{
		Vector3 position = this.Area.CageStructureTool.transform.InverseTransformPoint(worldPosition);
		CageStructureTool.Face face = this.Area.CageStructureTool.FindFaceAtPositionFaster(position);
		return face != null && this.IsDiscovered(face);
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0003DCD9 File Offset: 0x0003BED9
	public bool IsHidden(CageStructureTool.Face face)
	{
		return !this.m_worldAreaStates.ContainsKey(face.ID) || this.m_worldAreaStates[face.ID] == WorldMapAreaState.Hidden;
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0003DD0C File Offset: 0x0003BF0C
	public bool IsDiscovered(CageStructureTool.Face face)
	{
		return this.m_worldAreaStates.ContainsKey(face.ID) && this.m_worldAreaStates[face.ID] == WorldMapAreaState.Discovered;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0003DD4C File Offset: 0x0003BF4C
	public void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_dirtyCompletionAmount = true;
			this.m_worldAreaStates.Clear();
			int num = ar.Serialize(0);
			for (int i = 0; i < num; i++)
			{
				int key = ar.Serialize(0);
				WorldMapAreaState value = (WorldMapAreaState)ar.Serialize(0);
				this.m_worldAreaStates.Add(key, value);
			}
			num = ar.Serialize(0);
			for (int j = 0; j < num; j++)
			{
				MoonGuid guid = MoonGuid.Empty;
				guid.Serialize(ar);
				WorldMapIconType icon = (WorldMapIconType)ar.Serialize(0);
				RuntimeWorldMapIcon runtimeWorldMapIcon = this.Icons.Find((RuntimeWorldMapIcon a) => a.Guid == guid);
				if (runtimeWorldMapIcon != null)
				{
					runtimeWorldMapIcon.Icon = icon;
				}
			}
		}
		else
		{
			ar.Serialize(this.m_worldAreaStates.Count);
			foreach (KeyValuePair<int, WorldMapAreaState> keyValuePair in this.m_worldAreaStates)
			{
				ar.Serialize(keyValuePair.Key);
				ar.Serialize((int)keyValuePair.Value);
			}
			ar.Serialize(this.Icons.Count);
			foreach (RuntimeWorldMapIcon runtimeWorldMapIcon2 in this.Icons)
			{
				runtimeWorldMapIcon2.Guid.Serialize(ar);
				ar.Serialize((int)runtimeWorldMapIcon2.Icon);
			}
		}
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0003DF0C File Offset: 0x0003C10C
	public void DiscoverAllAreas()
	{
		CageStructureTool cageStructureTool = this.Area.CageStructureTool;
		foreach (CageStructureTool.Face face in cageStructureTool.Faces)
		{
			if (!this.m_worldAreaStates.ContainsKey(face.ID))
			{
				this.m_worldAreaStates[face.ID] = WorldMapAreaState.Discovered;
			}
		}
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0003DF94 File Offset: 0x0003C194
	public void VisitAllAreas()
	{
		this.m_worldAreaStates.Clear();
		CageStructureTool cageStructureTool = this.Area.CageStructureTool;
		foreach (CageStructureTool.Face face in cageStructureTool.Faces)
		{
			this.m_worldAreaStates[face.ID] = WorldMapAreaState.Visited;
		}
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0003E010 File Offset: 0x0003C210
	public bool FaceIsDiscoveredOrVisited(int id)
	{
		WorldMapAreaState worldMapAreaState;
		return this.m_worldAreaStates.TryGetValue(id, out worldMapAreaState) && (worldMapAreaState == WorldMapAreaState.Discovered || worldMapAreaState == WorldMapAreaState.Visited);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0003E044 File Offset: 0x0003C244
	public WorldMapAreaState GetFaceState(int id)
	{
		WorldMapAreaState result;
		if (this.m_worldAreaStates.TryGetValue(id, out result))
		{
		}
		return result;
	}

	// Token: 0x04000ADC RID: 2780
	public GameWorldArea Area;

	// Token: 0x04000ADD RID: 2781
	public List<RuntimeWorldMapIcon> Icons = new List<RuntimeWorldMapIcon>();

	// Token: 0x04000ADE RID: 2782
	private readonly Dictionary<int, WorldMapAreaState> m_worldAreaStates = new Dictionary<int, WorldMapAreaState>();

	// Token: 0x04000ADF RID: 2783
	private float m_completionAmount;

	// Token: 0x04000AE0 RID: 2784
	private bool m_dirtyCompletionAmount;
}
