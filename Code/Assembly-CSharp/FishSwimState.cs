using System;
using UnityEngine;

// Token: 0x02000528 RID: 1320
public class FishSwimState : FishState
{
	// Token: 0x0600230B RID: 8971 RVA: 0x000999A9 File Offset: 0x00097BA9
	public FishSwimState(FishEnemy fish) : base(fish)
	{
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x000999B4 File Offset: 0x00097BB4
	public override void UpdateState()
	{
		Vector2 delta = this.Fish.PositionToPlayerPosition;
		float num = MoonMath.Angle.AngleFromVector(delta);
		if (this.m_lockAnimationTime <= 0f)
		{
			this.Fish.Animation.PlayLoop(this.Fish.Animations.Swim.GetAnimation(this.Fish.BendValue), 0, null, true);
			if (Vector3.Dot(MoonMath.Angle.VectorFromAngle(this.Fish.Angle), MoonMath.Angle.VectorFromAngle(num)) < 0f)
			{
				this.Fish.Angle += 180f;
				this.Fish.FaceLeft = !this.Fish.FaceLeft;
				this.Fish.PlayAnimationOnce(this.Fish.Animations.SwimFlipVertical, 0);
				this.m_lockAnimationTime = this.Fish.Animations.SwimFlipVertical.Animation.Duration;
			}
			else
			{
				bool flag = this.Fish.FlyMovement.VelocityX < 0f;
				if (flag != this.Fish.FaceLeft)
				{
					this.Fish.FaceLeft = flag;
					this.Fish.PlayAnimationOnce(this.Fish.Animations.SwimFlipHorizontal, 0);
					this.m_lockAnimationTime = this.Fish.Animations.SwimFlipHorizontal.Animation.Duration;
				}
			}
		}
		else
		{
			this.m_lockAnimationTime -= Time.deltaTime;
		}
		this.Fish.Angle = Mathf.MoveTowardsAngle(this.Fish.Angle, num, this.Fish.Settings.SwimTurnSpeed * Time.deltaTime);
		float d = this.Fish.Settings.SwimSpeed * this.Fish.Settings.SwimSpeedOverTime.Evaluate(base.CurrentStateTime);
		this.Fish.ApplySoftSpeed(d * this.Fish.AngleAsVector);
		this.Fish.UpdateSpriteRotation();
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x00099BCC File Offset: 0x00097DCC
	public override void OnEnter()
	{
		this.m_lockAnimationTime = 0f;
		this.Fish.PlaySound(this.Fish.Sounds.Alert);
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x00099BFF File Offset: 0x00097DFF
	public override void OnExit()
	{
	}

	// Token: 0x04001D90 RID: 7568
	private float m_lockAnimationTime;
}
