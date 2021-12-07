using System;
using UnityEngine;

// Token: 0x02000583 RID: 1411
public class KamikazeSootEnemyRollingState : KamikazeSootEnemyState
{
	// Token: 0x06002462 RID: 9314 RVA: 0x0009EC54 File Offset: 0x0009CE54
	public KamikazeSootEnemyRollingState(KamikazeSootEnemy kamikazeSootEnemy) : base(kamikazeSootEnemy)
	{
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x0009EC60 File Offset: 0x0009CE60
	public override void UpdateState()
	{
		this.KamikazeSootEnemy.PlayAnimationLoop(this.KamikazeSootEnemy.Animations.Rolling, 0);
		if (!this.KamikazeSootEnemy.OutOfRange())
		{
			this.KamikazeSootEnemy.FacePlayer();
		}
		Vector2 local = this.KamikazeSootEnemy.RollingMovement.WorldToGround(this.KamikazeSootEnemy.RollingMovement.Speed);
		if (this.KamikazeSootEnemy.RollingMovement.Ground.IsOn)
		{
			if ((float)this.KamikazeSootEnemy.FaceLeftSign != Mathf.Sign(local.x))
			{
				local.x = MoonMath.Movement.DecelerateSpeed(local.x, this.KamikazeSootEnemy.Settings.RollDecceleration);
			}
			local.x = MoonMath.Movement.AccelerateSpeed(local.x, this.KamikazeSootEnemy.Settings.RollAcceleration, this.KamikazeSootEnemy.Settings.MaxRollSpeed, this.KamikazeSootEnemy.FaceLeft);
		}
		else
		{
			local.x = MoonMath.Movement.DecelerateSpeed(local.x, this.KamikazeSootEnemy.Settings.AirDeceleration);
			local.y = MoonMath.Movement.ApplyGravity(local.y, this.KamikazeSootEnemy.Settings.Gravity, this.KamikazeSootEnemy.Settings.MaxFallSpeed);
		}
		this.KamikazeSootEnemy.RollingMovement.Speed = this.KamikazeSootEnemy.RollingMovement.GroundToWorld(local);
		if (this.KamikazeSootEnemy.RollingSound)
		{
			if (this.KamikazeSootEnemy.RollingMovement.IsOnGround)
			{
				this.m_timeOffGround = 0f;
				if (!this.KamikazeSootEnemy.RollingSound.IsPlaying)
				{
					this.KamikazeSootEnemy.PlaySound(this.KamikazeSootEnemy.RollingSound);
				}
			}
			else
			{
				this.m_timeOffGround += Time.deltaTime;
				if (this.m_timeOffGround > 0.3f && this.KamikazeSootEnemy.RollingSound.IsPlaying)
				{
					this.KamikazeSootEnemy.RollingSound.StopAndFadeOut(0.3f);
				}
			}
		}
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x0009EE94 File Offset: 0x0009D094
	public override void OnEnter()
	{
		this.KamikazeSootEnemy.PlatformMovement.enabled = false;
		this.KamikazeSootEnemy.RollingMovement.enabled = true;
		this.KamikazeSootEnemy.PlaySound(this.KamikazeSootEnemy.RollingSound);
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x0009EEDC File Offset: 0x0009D0DC
	public override void OnExit()
	{
		this.KamikazeSootEnemy.PlatformMovement.enabled = true;
		this.KamikazeSootEnemy.RollingMovement.enabled = false;
		this.KamikazeSootEnemy.StopSound(this.KamikazeSootEnemy.RollingSound);
	}

	// Token: 0x04001E99 RID: 7833
	private float m_timeOffGround;
}
