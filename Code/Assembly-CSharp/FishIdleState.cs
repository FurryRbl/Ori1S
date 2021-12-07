using System;
using UnityEngine;

// Token: 0x02000527 RID: 1319
public class FishIdleState : FishState
{
	// Token: 0x06002306 RID: 8966 RVA: 0x00099712 File Offset: 0x00097912
	public FishIdleState(FishEnemy fish) : base(fish)
	{
	}

	// Token: 0x170005F7 RID: 1527
	// (get) Token: 0x06002307 RID: 8967 RVA: 0x0009971B File Offset: 0x0009791B
	public Vector3 WanderTargetPosition
	{
		get
		{
			if (this.Fish.WanderTarget)
			{
				return this.Fish.WanderTarget.position;
			}
			return this.Fish.StartPosition;
		}
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x00099750 File Offset: 0x00097950
	public override void UpdateState()
	{
		Vector2 delta = this.WanderTargetPosition - this.Fish.Position;
		float num = MoonMath.Angle.AngleFromVector(delta);
		if (this.m_lockAnimationTime <= 0f)
		{
			this.Fish.Animation.PlayLoop(this.Fish.Animations.Idle.GetAnimation(this.Fish.BendValue), 0, null, true);
			if (Vector3.Dot(MoonMath.Angle.VectorFromAngle(this.Fish.Angle), MoonMath.Angle.VectorFromAngle(num)) < 0f)
			{
				this.Fish.Angle += 180f;
				this.Fish.FaceLeft = !this.Fish.FaceLeft;
				this.Fish.PlayAnimationOnce(this.Fish.Animations.IdleFlipVertical, 0);
				this.m_lockAnimationTime = this.Fish.Animations.IdleFlipVertical.Animation.Duration;
			}
			else
			{
				bool flag = this.Fish.FlyMovement.VelocityX < 0f;
				if (flag != this.Fish.FaceLeft)
				{
					this.Fish.FaceLeft = flag;
					this.Fish.PlayAnimationOnce(this.Fish.Animations.IdleFlipHorizontal, 0);
					this.m_lockAnimationTime = this.Fish.Animations.IdleFlipHorizontal.Animation.Duration;
				}
			}
		}
		else
		{
			this.m_lockAnimationTime -= Time.deltaTime;
		}
		this.Fish.Angle = Mathf.MoveTowardsAngle(this.Fish.Angle, num, this.Fish.Settings.IdleTurnSpeed * Time.deltaTime);
		float d = this.Fish.Settings.IdleSpeedOverDistance.Evaluate(delta.magnitude) * this.Fish.Settings.IdleSpeed;
		this.Fish.ApplySoftSpeed(d * this.Fish.AngleAsVector);
		this.Fish.UpdateSpriteRotation();
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x00099974 File Offset: 0x00097B74
	public override void OnEnter()
	{
		this.m_lockAnimationTime = 0f;
		this.Fish.PlaySound(this.Fish.Sounds.Swim);
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x000999A7 File Offset: 0x00097BA7
	public override void OnExit()
	{
	}

	// Token: 0x04001D8F RID: 7567
	private float m_lockAnimationTime;
}
