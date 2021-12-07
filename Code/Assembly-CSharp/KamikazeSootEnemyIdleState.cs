using System;

// Token: 0x02000581 RID: 1409
public class KamikazeSootEnemyIdleState : KamikazeSootEnemyState
{
	// Token: 0x0600245A RID: 9306 RVA: 0x0009EA5F File Offset: 0x0009CC5F
	public KamikazeSootEnemyIdleState(KamikazeSootEnemy kamikazeSootEnemy) : base(kamikazeSootEnemy)
	{
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x0009EA68 File Offset: 0x0009CC68
	public override void UpdateState()
	{
		this.KamikazeSootEnemy.ApplyGravity(this.KamikazeSootEnemy.Settings.Gravity, this.KamikazeSootEnemy.Settings.MaxFallSpeed);
		if (this.KamikazeSootEnemy.PlatformMovement.IsOnGround)
		{
			this.KamikazeSootEnemy.Decelerate(this.KamikazeSootEnemy.Settings.Decceleration);
		}
		else
		{
			this.KamikazeSootEnemy.Decelerate(this.KamikazeSootEnemy.Settings.AirDeceleration);
		}
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x0009EAF0 File Offset: 0x0009CCF0
	public override void OnEnter()
	{
		this.KamikazeSootEnemy.RestartAnimationLoop(this.KamikazeSootEnemy.Animations.Idle, 0);
		if (this.KamikazeSootEnemy.IdleSound)
		{
			this.KamikazeSootEnemy.IdleSound.Play();
		}
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x0009EB3E File Offset: 0x0009CD3E
	public override void OnExit()
	{
		if (this.KamikazeSootEnemy.IdleSound)
		{
			this.KamikazeSootEnemy.IdleSound.Stop();
		}
	}
}
