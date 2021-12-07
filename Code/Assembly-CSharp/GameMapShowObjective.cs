using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class GameMapShowObjective : MonoBehaviour
{
	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06000DB2 RID: 3506 RVA: 0x0003FD51 File Offset: 0x0003DF51
	// (remove) Token: 0x06000DB3 RID: 3507 RVA: 0x0003FD6A File Offset: 0x0003DF6A
	public event Action OnFinish;

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0003FD83 File Offset: 0x0003DF83
	public bool IsPerforming
	{
		get
		{
			return this.m_isPerforming;
		}
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0003FD8C File Offset: 0x0003DF8C
	public void ShowObjective(Objective objective, Action onFinish)
	{
		this.m_objective = objective;
		GameWorld.Instance.VisitMapAreasAtPosition(objective.Position);
		this.OnFinish = onFinish;
		this.m_isPerforming = true;
		if (!Objectives.ObjectiveExists(objective))
		{
			Objectives.AddObjective(objective);
		}
		UI.Menu.ShowWorldMap(true);
		WorldMapUI.Instance.HideAreaSelection();
		GameMapUI.Instance.SetShowingObjective();
		objective.Hide();
		this.StartPosition = Characters.Sein.Position;
		this.TargetPosition = objective.Position;
		WorldMapUI.Instance.CameraOffset = Vector3.zero;
		WorldMapUI.Instance.UpdateCameraPosition();
		this.m_startPositionUI = this.WorldToUIPosition(this.StartPosition);
		this.m_targetPositionUI = this.WorldToUIPosition(this.TargetPosition);
		WorldMapUI.Instance.CameraOffset = this.m_startPositionUI;
		WorldMapUI.Instance.UpdateCameraPosition();
		this.ChangeState(GameMapShowObjective.State.StartWait);
		this.UpdateState();
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0003FE80 File Offset: 0x0003E080
	public void Finish()
	{
		GameMapUI.Instance.SetNormal();
		UI.Menu.HideMenuScreen(false);
		if (this.OnFinish != null)
		{
			this.OnFinish();
			this.OnFinish = null;
		}
		this.m_isPerforming = false;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0003FEC6 File Offset: 0x0003E0C6
	public void OnDisable()
	{
		if (this.m_isPerforming)
		{
			this.Finish();
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0003FEDC File Offset: 0x0003E0DC
	public void FixedUpdate()
	{
		if (this.m_isPerforming)
		{
			this.UpdateState();
			if (this.IsComplete)
			{
				this.Finish();
			}
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0003FF0B File Offset: 0x0003E10B
	public bool IsComplete
	{
		get
		{
			return this.CurrentState == GameMapShowObjective.State.Complete;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003FF16 File Offset: 0x0003E116
	public bool IsInEndWait
	{
		get
		{
			return this.CurrentState == GameMapShowObjective.State.EndWait;
		}
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0003FF21 File Offset: 0x0003E121
	public Vector3 WorldToUIPosition(Vector3 position)
	{
		return WorldMapUI.Instance.WorldToUIPosition(position);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0003FF2E File Offset: 0x0003E12E
	public void ChangeState(GameMapShowObjective.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0003FF44 File Offset: 0x0003E144
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case GameMapShowObjective.State.StartWait:
			if (this.m_stateCurrentTime > this.BeginWaitTime || Core.Input.ActionButtonA.OnPressed)
			{
				this.m_objective.Show();
				this.m_objective.SpawnAppearEffect();
				if (this.FinishShowObjectiveSound)
				{
					Sound.Play(this.FinishShowObjectiveSound.GetSound(null), base.transform.position, null);
				}
				this.ChangeState(GameMapShowObjective.State.Move);
			}
			this.Position = this.m_startPositionUI;
			WorldMapUI.Instance.CameraOffset = this.Position;
			break;
		case GameMapShowObjective.State.Move:
			if (this.m_stateCurrentTime > this.MoveTime || Core.Input.ActionButtonA.OnPressed)
			{
				this.ChangeState(GameMapShowObjective.State.EndWait);
			}
			else
			{
				float time = this.m_stateCurrentTime / this.MoveTime;
				this.Position = Vector3.Lerp(this.m_startPositionUI, this.m_targetPositionUI, this.IconMovingCurve.Evaluate(time));
				WorldMapUI.Instance.CameraOffset = this.Position;
			}
			break;
		case GameMapShowObjective.State.EndWait:
			this.Position = this.m_targetPositionUI;
			WorldMapUI.Instance.CameraOffset = this.Position;
			if (this.m_stateCurrentTime > this.EndWaitTime || Core.Input.Jump.OnPressed || Core.Input.SpiritFlame.OnPressed)
			{
				GameMapTransitionManager.Instance.ZoomToAreaMap();
				AreaMapUI.Instance.Navigation.ScrollPosition = this.TargetPosition;
				this.ChangeState(GameMapShowObjective.State.AreaMap);
				GameMapUI.Instance.SetNormal();
			}
			break;
		case GameMapShowObjective.State.AreaMap:
			if (Core.Input.Jump.OnPressed)
			{
				this.ChangeState(GameMapShowObjective.State.Complete);
			}
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x04000B1F RID: 2847
	public AnimationCurve IconMovingCurve;

	// Token: 0x04000B20 RID: 2848
	public Varying2DSoundProvider FinishShowObjectiveSound;

	// Token: 0x04000B21 RID: 2849
	public float BlipInterval = 0.5f;

	// Token: 0x04000B22 RID: 2850
	public float BeginWaitTime = 2f;

	// Token: 0x04000B23 RID: 2851
	public float MoveTime = 2f;

	// Token: 0x04000B24 RID: 2852
	public float EndWaitTime = 1f;

	// Token: 0x04000B25 RID: 2853
	public float AreaMapWaitTime = 2f;

	// Token: 0x04000B26 RID: 2854
	private bool m_isPerforming;

	// Token: 0x04000B27 RID: 2855
	public GameObject ObjectiveAppearEffect;

	// Token: 0x04000B28 RID: 2856
	private Objective m_objective;

	// Token: 0x04000B29 RID: 2857
	public GameMapShowObjective.State CurrentState;

	// Token: 0x04000B2A RID: 2858
	public Vector3 StartPosition;

	// Token: 0x04000B2B RID: 2859
	public Vector3 TargetPosition;

	// Token: 0x04000B2C RID: 2860
	public Vector3 Position;

	// Token: 0x04000B2D RID: 2861
	private float m_stateCurrentTime;

	// Token: 0x04000B2E RID: 2862
	private Vector3 m_startPositionUI;

	// Token: 0x04000B2F RID: 2863
	private Vector3 m_targetPositionUI;

	// Token: 0x0200087F RID: 2175
	public enum State
	{
		// Token: 0x04002C59 RID: 11353
		None,
		// Token: 0x04002C5A RID: 11354
		StartWait,
		// Token: 0x04002C5B RID: 11355
		Move,
		// Token: 0x04002C5C RID: 11356
		EndWait,
		// Token: 0x04002C5D RID: 11357
		AreaMap,
		// Token: 0x04002C5E RID: 11358
		Complete
	}
}
