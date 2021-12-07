using System;
using UnityEngine;

// Token: 0x0200051E RID: 1310
public class FishAttackState : FishState
{
	// Token: 0x060022D3 RID: 8915 RVA: 0x000989FB File Offset: 0x00096BFB
	public FishAttackState(FishEnemy fish) : base(fish)
	{
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x00098A04 File Offset: 0x00096C04
	public override void UpdateState()
	{
		float time = base.CurrentStateTime / this.Fish.Settings.AttackDuration;
		float num = this.Fish.Settings.AttackDistanceOverTime.Evaluate(time);
		float speed = (num - this.m_lastDistance) / Time.deltaTime;
		this.m_lastDistance = num;
		this.Fish.FlyMovement.Speed = speed;
		FishEnemy fish = this.Fish;
		float angle = this.m_attackAngle + this.Fish.Settings.AttackAngleOverTime.Evaluate(time) * (float)this.Fish.FaceLeftSign;
		this.Fish.FlyMovement.Angle = angle;
		fish.Angle = angle;
		this.Fish.UpdateSpriteRotation();
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x00098ABC File Offset: 0x00096CBC
	public override void OnEnter()
	{
		this.m_attackAngle = MoonMath.Angle.AngleFromVector(this.Fish.PositionToPlayerPosition);
		this.Fish.PlayAnimationOnce(this.Fish.Animations.Attack, 0);
		this.m_lastDistance = 0f;
		if (this.Fish.Sounds.Bite)
		{
			this.Fish.Sounds.Bite.Play();
		}
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x00098B3A File Offset: 0x00096D3A
	public override void OnExit()
	{
		if (this.Fish.Sounds.Bite)
		{
			this.Fish.Sounds.Bite.Stop();
		}
	}

	// Token: 0x04001D52 RID: 7506
	private float m_lastDistance;

	// Token: 0x04001D53 RID: 7507
	private float m_lastAngle;

	// Token: 0x04001D54 RID: 7508
	private float m_attackAngle;
}
