using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class GameWorld : SaveSerialize
{
	// Token: 0x06000AF5 RID: 2805 RVA: 0x0002FBA0 File Offset: 0x0002DDA0
	public bool HasCompletedEverything()
	{
		bool flag = false;
		foreach (RuntimeGameWorldArea runtimeGameWorldArea in this.RuntimeAreas)
		{
			foreach (RuntimeWorldMapIcon runtimeWorldMapIcon in runtimeGameWorldArea.Icons)
			{
				WorldMapIconType icon = runtimeWorldMapIcon.Icon;
				switch (icon)
				{
				case WorldMapIconType.HealthUpgrade:
				case WorldMapIconType.EnergyUpgrade:
				case WorldMapIconType.AbilityPoint:
				case WorldMapIconType.Experience:
				case WorldMapIconType.MapstonePickup:
					break;
				default:
					if (icon != WorldMapIconType.Keystone)
					{
						continue;
					}
					break;
				}
				flag = true;
			}
		}
		return !flag && this.CompletionPercentage == 100;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0002FC90 File Offset: 0x0002DE90
	public void RevealIcon(MoonGuid icon)
	{
		this.m_revealedIcons.Add(icon);
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0002FC9F File Offset: 0x0002DE9F
	public bool IconRevealed(MoonGuid icon)
	{
		return this.m_revealedIcons.Contains(icon);
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0002FCB0 File Offset: 0x0002DEB0
	public float CompletionAmount
	{
		get
		{
			int num = 0;
			float num2 = 0f;
			for (int i = 0; i < this.RuntimeAreas.Count; i++)
			{
				RuntimeGameWorldArea runtimeGameWorldArea = this.RuntimeAreas[i];
				num++;
				num2 += runtimeGameWorldArea.CompletionAmount;
			}
			return num2 / (float)num;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002FD00 File Offset: 0x0002DF00
	public int CompletionPercentage
	{
		get
		{
			float completionAmount = this.CompletionAmount;
			if (Mathf.Approximately(completionAmount, 1f))
			{
				return 100;
			}
			return Mathf.Clamp(Mathf.RoundToInt(this.CompletionAmount * 100f), 0, 99);
		}
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0002FD40 File Offset: 0x0002DF40
	public GameWorldArea FindAreaFromPosition(Vector3 position)
	{
		for (int i = 0; i < this.Areas.Count; i++)
		{
			GameWorldArea gameWorldArea = this.Areas[i];
			if (gameWorldArea.InsideFace(position))
			{
				return gameWorldArea;
			}
		}
		return null;
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0002FD88 File Offset: 0x0002DF88
	public RuntimeGameWorldArea FindRuntimeArea(GameWorldArea area)
	{
		for (int i = 0; i < this.RuntimeAreas.Count; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = this.RuntimeAreas[i];
			if (runtimeGameWorldArea.Area == area)
			{
				return runtimeGameWorldArea;
			}
		}
		return null;
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0002FDD4 File Offset: 0x0002DFD4
	public override void Awake()
	{
		GameWorld.Instance = this;
		this.RuntimeAreas.Capacity = this.Areas.Count;
		for (int i = 0; i < this.Areas.Count; i++)
		{
			GameWorldArea area = this.Areas[i];
			this.RuntimeAreas.Add(new RuntimeGameWorldArea(area));
		}
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		base.Awake();
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0002FE58 File Offset: 0x0002E058
	public override void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		base.OnDestroy();
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0002FE7C File Offset: 0x0002E07C
	public void OnGameReset()
	{
		for (int i = 0; i < this.RuntimeAreas.Count; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = this.RuntimeAreas[i];
			runtimeGameWorldArea.Initialize();
		}
		this.m_revealedIcons.Clear();
		this.ObjectiveText = null;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0002FECA File Offset: 0x0002E0CA
	public GameWorldArea AreaFromIndex(int i)
	{
		if (i < 0 || i >= this.RuntimeAreas.Count)
		{
			return null;
		}
		return this.RuntimeAreas[i].Area;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0002FEF8 File Offset: 0x0002E0F8
	public int IndexOfArea(GameWorldArea area)
	{
		return this.RuntimeAreas.FindIndex((RuntimeGameWorldArea a) => a.Area == area);
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0002FF2C File Offset: 0x0002E12C
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			int num = 0;
			ar.Serialize(ref num);
			if (this.Areas.Count != num)
			{
				return;
			}
			int num2 = 0;
			while (num2 < num && num2 < this.RuntimeAreas.Count)
			{
				RuntimeGameWorldArea runtimeGameWorldArea = this.RuntimeAreas[num2];
				runtimeGameWorldArea.Serialize(ar);
				num2++;
			}
			this.m_revealedIcons.Clear();
			int num3 = ar.Serialize(0);
			for (int i = 0; i < num3; i++)
			{
				MoonGuid moonGuid = new MoonGuid(0, 0, 0, 0);
				moonGuid.Serialize(ar);
				this.m_revealedIcons.Add(moonGuid);
			}
			int num4 = ar.Serialize(0);
			if (num4 != -1)
			{
				this.ObjectiveText = this.ObjectiveTextProviders[num4];
			}
		}
		else
		{
			ar.Serialize(this.Areas.Count);
			for (int j = 0; j < this.RuntimeAreas.Count; j++)
			{
				RuntimeGameWorldArea runtimeGameWorldArea2 = this.RuntimeAreas[j];
				runtimeGameWorldArea2.Serialize(ar);
			}
			ar.Serialize(this.m_revealedIcons.Count);
			foreach (MoonGuid moonGuid2 in this.m_revealedIcons)
			{
				moonGuid2.Serialize(ar);
			}
			ar.Serialize(this.ObjectiveTextProviders.IndexOf(this.ObjectiveText));
		}
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x000300D0 File Offset: 0x0002E2D0
	public void VisitMapAreasAtPosition(Vector3 currentPlayerPosition)
	{
		for (int i = 0; i < this.RuntimeAreas.Count; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = this.RuntimeAreas[i];
			runtimeGameWorldArea.VisitMapAreaAtPosition(currentPlayerPosition);
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x00030110 File Offset: 0x0002E310
	public GameWorldArea WorldAreaAtPosition(Vector3 worldPosition)
	{
		for (int i = 0; i < this.RuntimeAreas.Count; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = this.RuntimeAreas[i];
			Vector3 position = runtimeGameWorldArea.Area.CageStructureTool.transform.InverseTransformPoint(worldPosition);
			CageStructureTool.Face face = runtimeGameWorldArea.Area.CageStructureTool.FindFaceAtPositionFaster(position);
			if (face != null)
			{
				return runtimeGameWorldArea.Area;
			}
		}
		return null;
	}

	// Token: 0x040008F5 RID: 2293
	public static GameWorld Instance;

	// Token: 0x040008F6 RID: 2294
	public List<GameWorldArea> Areas = new List<GameWorldArea>();

	// Token: 0x040008F7 RID: 2295
	public List<RuntimeGameWorldArea> RuntimeAreas = new List<RuntimeGameWorldArea>();

	// Token: 0x040008F8 RID: 2296
	public RuntimeGameWorldArea CurrentArea;

	// Token: 0x040008F9 RID: 2297
	private readonly HashSet<MoonGuid> m_revealedIcons = new HashSet<MoonGuid>();

	// Token: 0x040008FA RID: 2298
	public List<MessageProvider> ObjectiveTextProviders = new List<MessageProvider>();

	// Token: 0x040008FB RID: 2299
	public MessageProvider ObjectiveText;
}
