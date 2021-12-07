using System;
using System.Collections;
using Game;
using UnityEngine;

// Token: 0x02000328 RID: 808
[Category("Sein")]
public class SeinRunToTargetAction : PerformingAction
{
	// Token: 0x06001796 RID: 6038 RVA: 0x00065529 File Offset: 0x00063729
	public override void Stop()
	{
		base.StopCoroutine("Perform");
		this.Exit();
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x06001797 RID: 6039 RVA: 0x0006553C File Offset: 0x0006373C
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPerforming;
		}
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x00065544 File Offset: 0x00063744
	public override void Perform(IContext context)
	{
		this.m_isPerforming = true;
		Characters.Sein.Controller.IgnoreControllerInput = true;
		if (this.CustomWalkMovingAnimation)
		{
			this.m_originalWalkMovingAnimation = Characters.Sein.Abilities.Run.WalkAnimation;
			Characters.Sein.Abilities.Run.WalkAnimation = this.CustomWalkMovingAnimation;
		}
		if (this.CustomJogMovingAnimation)
		{
			this.m_originalJogMovingAnimation = Characters.Sein.Abilities.Run.JogAnimation;
			Characters.Sein.Abilities.Run.JogAnimation = this.CustomJogMovingAnimation;
		}
		if (this.CustomRunMovingAnimation)
		{
			this.m_originalRunMovingAnimation = Characters.Sein.Abilities.Run.RunAnimation;
			Characters.Sein.Abilities.Run.RunAnimation = this.CustomRunMovingAnimation;
		}
		Characters.Sein.Abilities.Run.Enter();
		base.StartCoroutine(this.Perform());
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x00065658 File Offset: 0x00063858
	public IEnumerator Perform()
	{
		float distance = this.TargetPosition.position.x - Characters.Sein.transform.position.x;
		float time = this.MaxDuration;
		while (Mathf.Abs(distance) > 0.1f && time > 0f)
		{
			Characters.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = Mathf.Sign(distance) * this.SpeedCurve.Evaluate(Mathf.Abs(distance));
			yield return new WaitForFixedUpdate();
			time -= Time.deltaTime;
			distance = this.TargetPosition.position.x - Characters.Sein.transform.position.x;
			while (!Characters.Sein || Characters.Sein.IsSuspended)
			{
				yield return new WaitForFixedUpdate();
			}
		}
		this.Exit();
		yield break;
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x00065674 File Offset: 0x00063874
	private void Exit()
	{
		Characters.Sein.Controller.IgnoreControllerInput = false;
		if (this.CustomWalkMovingAnimation)
		{
			Characters.Sein.Abilities.Run.WalkAnimation = this.m_originalWalkMovingAnimation;
		}
		if (this.CustomJogMovingAnimation)
		{
			Characters.Sein.Abilities.Run.JogAnimation = this.m_originalJogMovingAnimation;
		}
		if (this.CustomRunMovingAnimation)
		{
			Characters.Sein.Abilities.Run.RunAnimation = this.m_originalRunMovingAnimation;
		}
		this.m_isPerforming = false;
	}

	// Token: 0x04001433 RID: 5171
	public AnimationCurve SpeedCurve;

	// Token: 0x04001434 RID: 5172
	[NotNull]
	public Transform TargetPosition;

	// Token: 0x04001435 RID: 5173
	public TextureAnimationWithTransitions CustomWalkMovingAnimation;

	// Token: 0x04001436 RID: 5174
	public TextureAnimationWithTransitions CustomJogMovingAnimation;

	// Token: 0x04001437 RID: 5175
	public TextureAnimationWithTransitions CustomRunMovingAnimation;

	// Token: 0x04001438 RID: 5176
	private TextureAnimationWithTransitions m_originalWalkMovingAnimation;

	// Token: 0x04001439 RID: 5177
	private TextureAnimationWithTransitions m_originalJogMovingAnimation;

	// Token: 0x0400143A RID: 5178
	private TextureAnimationWithTransitions m_originalRunMovingAnimation;

	// Token: 0x0400143B RID: 5179
	private bool m_isPerforming;

	// Token: 0x0400143C RID: 5180
	public float MaxDuration = 10f;
}
