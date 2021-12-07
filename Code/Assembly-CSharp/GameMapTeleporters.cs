using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class GameMapTeleporters : MonoBehaviour
{
	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00040454 File Offset: 0x0003E654
	public List<GameMapTeleporter> Teleporters
	{
		get
		{
			return TeleporterController.Instance.Teleporters;
		}
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00040460 File Offset: 0x0003E660
	[ContextMenu("Show teleporters")]
	public void ShowTeleporters()
	{
		foreach (GameMapTeleporter gameMapTeleporter in this.Teleporters)
		{
			if (gameMapTeleporter.Activated)
			{
				gameMapTeleporter.Show();
			}
		}
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x000404C4 File Offset: 0x0003E6C4
	public void HideTeleporters()
	{
		foreach (GameMapTeleporter gameMapTeleporter in this.Teleporters)
		{
			gameMapTeleporter.Hide();
		}
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x00040520 File Offset: 0x0003E720
	private void ChangeSelection(int index)
	{
		if (this.SelectedIndex == index)
		{
			return;
		}
		this.SetIndex(index);
		if (this.SwitchTeleporterSelectionSound)
		{
			Sound.Play(this.SwitchTeleporterSelectionSound.GetSound(null), base.transform.position, null);
		}
		if (GameMapTransitionManager.Instance.InWorldMapMode)
		{
			AreaMapUI.Instance.Navigation.ScrollPosition = this.Teleporters[index].WorldPosition;
		}
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x000405A4 File Offset: 0x0003E7A4
	private int TeleporterUnderMouse()
	{
		int result = -1;
		if (GameMapTransitionManager.Instance.InWorldMapMode)
		{
			for (int i = 0; i < this.Teleporters.Count; i++)
			{
				GameMapTeleporter gameMapTeleporter = this.Teleporters[i];
				if (gameMapTeleporter.Activated && Vector3.Distance(Core.Input.CursorPositionUI, gameMapTeleporter.WorldMapIconPosition) < 1f)
				{
					result = i;
				}
			}
		}
		if (GameMapTransitionManager.Instance.InAreaMapMode)
		{
			for (int j = 0; j < this.Teleporters.Count; j++)
			{
				GameMapTeleporter gameMapTeleporter2 = this.Teleporters[j];
				if (gameMapTeleporter2.Activated && Vector3.Distance(Core.Input.CursorPositionUI, gameMapTeleporter2.AreaMapIconPosition) < 1f)
				{
					result = j;
				}
			}
		}
		return result;
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0004068C File Offset: 0x0003E88C
	private void AdvanceWorldMap()
	{
		this.m_flyBackTime = 0f;
		if (Core.Input.Axis.magnitude < 0.5f)
		{
			this.m_released = true;
		}
		if (Core.Input.CursorMoved)
		{
			int num = this.TeleporterUnderMouse();
			if (num != -1)
			{
				this.ChangeSelection(num);
			}
		}
		if (Core.Input.Axis.magnitude > 0.5f && this.m_released)
		{
			Vector2 normalized = Core.Input.Axis.normalized;
			Vector2 worldMapIconPosition = this.SelectedTeleporter.WorldMapIconPosition;
			int num2 = -1;
			float num3 = float.MaxValue;
			for (int i = 0; i < this.Teleporters.Count; i++)
			{
				GameMapTeleporter gameMapTeleporter = this.Teleporters[i];
				if (gameMapTeleporter.Activated)
				{
					Vector2 vector = gameMapTeleporter.WorldMapIconPosition - worldMapIconPosition;
					if (vector.magnitude < num3 && Vector3.Dot(vector.normalized, normalized) > 0.707f)
					{
						num3 = vector.magnitude;
						num2 = i;
					}
				}
			}
			if (num2 != -1)
			{
				this.m_released = false;
				this.ChangeSelection(num2);
			}
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x000407C4 File Offset: 0x0003E9C4
	private void AdvanceAreaMap()
	{
		if (Core.Input.CursorMoved)
		{
			int num = this.TeleporterUnderMouse();
			if (num != -1)
			{
				this.ChangeSelection(num);
			}
		}
		if (AreaMapUI.Instance.Navigation.ScrollingSensitivityCurve.Evaluate(Core.Input.Axis.magnitude) > 0f)
		{
			this.m_flyBackTime = 1.1f;
			this.m_previousScrollPosition = AreaMapUI.Instance.Navigation.ScrollPosition;
			float num2 = 9f;
			int index = this.SelectedIndex;
			for (int i = 0; i < this.Teleporters.Count; i++)
			{
				GameMapTeleporter gameMapTeleporter = this.Teleporters[i];
				if (gameMapTeleporter.Activated)
				{
					float magnitude = gameMapTeleporter.AreaMapIconPosition.magnitude;
					if (magnitude < num2)
					{
						index = i;
						num2 = magnitude;
					}
				}
			}
			this.ChangeSelection(index);
		}
		else
		{
			this.m_flyBackTime -= Time.deltaTime;
			if (this.m_flyBackTime < 1f && this.m_flyBackTime > 0f)
			{
				AreaMapUI.Instance.Navigation.ScrollPosition = Vector2.Lerp(this.m_previousScrollPosition, this.Teleporters[this.SelectedIndex].WorldPosition, 1f - Mathf.SmoothStep(0f, 1f, this.m_flyBackTime));
			}
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0004092C File Offset: 0x0003EB2C
	public void Advance()
	{
		if (!GameMapUI.Instance.ShowingTeleporters)
		{
			return;
		}
		foreach (GameMapTeleporter gameMapTeleporter in this.Teleporters)
		{
			gameMapTeleporter.Update();
		}
		if (GameMapTransitionManager.Instance.InWorldMapMode)
		{
			this.AdvanceWorldMap();
		}
		if (GameMapTransitionManager.Instance.InAreaMapMode)
		{
			this.AdvanceAreaMap();
		}
		if (Core.Input.LeftClick.OnPressed)
		{
			this.m_clickedPosition = Core.Input.CursorPositionUI;
		}
		bool flag = Core.Input.LeftClick.OnReleased && Vector2.Distance(Core.Input.CursorPositionUI, this.m_clickedPosition) < 0.01f && this.TeleporterUnderMouse() != -1;
		if (Core.Input.ActionButtonA.OnPressed || flag)
		{
			UI.Menu.HideMenuScreen(false);
			if (this.SelectTeleporterSound)
			{
				Sound.Play(this.SelectTeleporterSound.GetSound(null), base.transform.position, null);
			}
			TeleporterController.BeginTeleportation(this.SelectedTeleporter);
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00040A6C File Offset: 0x0003EC6C
	public void OnDisable()
	{
		this.HideTeleporters();
		if (GameMapUI.Instance.ShowingTeleporters)
		{
			TeleporterController.OnClose();
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00040A88 File Offset: 0x0003EC88
	public GameMapTeleporter SelectedTeleporter
	{
		get
		{
			return this.Teleporters[this.SelectedIndex];
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00040A9C File Offset: 0x0003EC9C
	public void Select(string identifier)
	{
		int num = this.Teleporters.FindIndex((GameMapTeleporter a) => a.Identifier == identifier);
		if (num != -1)
		{
			this.SetIndex(num);
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x00040ADC File Offset: 0x0003ECDC
	public void SetIndex(int index)
	{
		this.SelectedTeleporter.Dehighlight();
		this.SelectedIndex = index;
		this.SelectedTeleporter.Highlight();
		GameWorldArea area = GameWorld.Instance.FindAreaFromPosition(this.SelectedTeleporter.WorldPosition);
		GameMapUI.Instance.CurrentHighlightedArea = GameWorld.Instance.FindRuntimeArea(area);
	}

	// Token: 0x04000B3A RID: 2874
	public SoundProvider SelectTeleporterSound;

	// Token: 0x04000B3B RID: 2875
	public SoundProvider SwitchTeleporterSelectionSound;

	// Token: 0x04000B3C RID: 2876
	public SoundProvider StartTeleportingSound;

	// Token: 0x04000B3D RID: 2877
	public SoundProvider ReachDestinationTeleporterSound;

	// Token: 0x04000B3E RID: 2878
	public SoundProvider OpenWindowSound;

	// Token: 0x04000B3F RID: 2879
	public SoundProvider CloseWindowSound;

	// Token: 0x04000B40 RID: 2880
	public int SelectedIndex;

	// Token: 0x04000B41 RID: 2881
	private bool m_released = true;

	// Token: 0x04000B42 RID: 2882
	private Vector2 m_previousScrollPosition;

	// Token: 0x04000B43 RID: 2883
	private float m_flyBackTime;

	// Token: 0x04000B44 RID: 2884
	private Vector2 m_clickedPosition;
}
