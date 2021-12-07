using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class MovingCutsceneState : CutsceneState
{
	// Token: 0x170001EB RID: 491
	// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000264B8 File Offset: 0x000246B8
	public SeinCharacter Sein
	{
		get
		{
			return Characters.Sein;
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000264C0 File Offset: 0x000246C0
	public override void OnEnter()
	{
		Characters.Sein.CutsceneBlocked.Enter();
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Idle, 210, null, false);
		this.m_enteringTransitionFinished = false;
		this.Sein.PlatformBehaviour.PlatformMovement.UseCenterRayForGroundAngle = true;
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00026524 File Offset: 0x00024724
	public override void OnExit()
	{
		Characters.Sein.CutsceneMovement.Exit();
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Moving, 210, null, false);
		this.Sein.PlatformBehaviour.Visuals.Animation.Animator.SetAnimation(this.Moving, false);
		this.Sein.PlatformBehaviour.Gravity.Active = false;
		this.Sein.PlatformBehaviour.PlatformMovement.UseCenterRayForGroundAngle = false;
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x000265BC File Offset: 0x000247BC
	public override void OnUpdate()
	{
		if (!this.m_enteringTransitionFinished)
		{
			if (this.Sein.PlatformBehaviour.Visuals.Animation.Animator.IsTransitionPlaying)
			{
				return;
			}
			this.m_enteringTransitionFinished = true;
			Characters.Sein.CutsceneBlocked.Exit();
			Characters.Sein.CutsceneMovement.Enter();
		}
		if (this.Sein.PlatformBehaviour.Visuals.Animation.Animator.IsTransitionPlaying)
		{
			return;
		}
		float num = (float)Core.Input.NormalizedHorizontal;
		if (this.ForceMoveLeftTime > 0f)
		{
			this.ForceMoveLeftTime -= Time.deltaTime;
			num = -1f;
		}
		foreach (GameObject gameObject in this.BlockingMovingLeftZones)
		{
			Vector3 position = gameObject.transform.position;
			Vector3 localScale = gameObject.transform.localScale;
			Rect rect = new Rect(position.x - localScale.x * 0.5f, position.y - localScale.y * 0.5f, localScale.x, localScale.y);
			if (rect.Contains(this.Sein.Position) && num > 0f)
			{
				num = 0f;
			}
		}
		if (num < 0f)
		{
			this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = true;
		}
		if (num > 0f)
		{
			this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = false;
		}
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop((num != 0f) ? this.Moving : this.Idle, 210, null, false);
		base.OnUpdate();
	}

	// Token: 0x04000730 RID: 1840
	public TextureAnimationWithTransitions Moving;

	// Token: 0x04000731 RID: 1841
	public TextureAnimationWithTransitions Idle;

	// Token: 0x04000732 RID: 1842
	private bool m_enteringTransitionFinished;

	// Token: 0x04000733 RID: 1843
	public float ForceMoveLeftTime = 2f;

	// Token: 0x04000734 RID: 1844
	public GameObject[] BlockingMovingLeftZones;
}
