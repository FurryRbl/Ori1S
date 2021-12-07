using System;
using Game;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class WorldMapLogic : MonoBehaviour, ISuspendable
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x0001C931 File Offset: 0x0001AB31
	public void Awake()
	{
		WorldMapLogic.Instance = this;
		SuspensionManager.Register(this);
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0001C93F File Offset: 0x0001AB3F
	public void OnDestroy()
	{
		if (WorldMapLogic.Instance == this)
		{
			WorldMapLogic.Instance = null;
		}
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0001C960 File Offset: 0x0001AB60
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_fixedUpdateCounter++;
		if (this.m_fixedUpdateCounter % 20 != 1)
		{
			return;
		}
		if (UI.Cameras.Current.Target)
		{
			int completionPercentage = GameWorld.Instance.CompletionPercentage;
			Vector3 position = UI.Cameras.Current.Target.position;
			GameWorld.Instance.VisitMapAreasAtPosition(position);
			this.UpdateCurrentArea();
			if (completionPercentage != GameWorld.Instance.CompletionPercentage)
			{
				Telemetry.CompletionHeroStat.SendData(GameWorld.Instance.CompletionPercentage + "%");
			}
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0001CA0C File Offset: 0x0001AC0C
	public void UpdateCurrentArea()
	{
		if (UI.Cameras.Current == null || UI.Cameras.Current.Target == null)
		{
			return;
		}
		World.CurrentArea = null;
		Vector3 position = UI.Cameras.Current.Target.position;
		for (int i = 0; i < WorldMapLogic.m_samplePositions.Length; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = this.FindAreaFromPosition(position + WorldMapLogic.m_samplePositions[i]);
			if (runtimeGameWorldArea != null)
			{
				World.CurrentArea = runtimeGameWorldArea;
				break;
			}
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
	public RuntimeGameWorldArea FindAreaFromPosition(Vector3 cameraPosition)
	{
		for (int i = 0; i < GameWorld.Instance.RuntimeAreas.Count; i++)
		{
			RuntimeGameWorldArea runtimeGameWorldArea = GameWorld.Instance.RuntimeAreas[i];
			Vector3 position = runtimeGameWorldArea.Area.BoundaryCage.transform.InverseTransformPoint(cameraPosition);
			CageStructureTool.Face face = runtimeGameWorldArea.Area.BoundaryCage.FindFaceAtPositionFaster(position);
			if (face != null)
			{
				return runtimeGameWorldArea;
			}
		}
		return null;
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001CB10 File Offset: 0x0001AD10
	// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001CB18 File Offset: 0x0001AD18
	public bool IsSuspended { get; set; }

	// Token: 0x0400052A RID: 1322
	private const int PlayerPositionUpdatePollFrequency = 3;

	// Token: 0x0400052B RID: 1323
	public static WorldMapLogic Instance;

	// Token: 0x0400052C RID: 1324
	private int m_fixedUpdateCounter;

	// Token: 0x0400052D RID: 1325
	public CageStructureTool MapEnabledArea;

	// Token: 0x0400052E RID: 1326
	private static Vector2[] m_samplePositions = new Vector2[]
	{
		Vector2.zero,
		new Vector2(-2f, 2f),
		new Vector2(2f, 2f),
		new Vector2(-2f, -2f),
		new Vector2(2f, -2f)
	};
}
