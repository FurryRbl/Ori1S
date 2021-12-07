using System;

// Token: 0x02000582 RID: 1410
public class KamikazeSootEnemyAlertState : KamikazeSootEnemyState
{
	// Token: 0x0600245E RID: 9310 RVA: 0x0009EB65 File Offset: 0x0009CD65
	public KamikazeSootEnemyAlertState(KamikazeSootEnemy kamikazeSootEnemy) : base(kamikazeSootEnemy)
	{
	}

	// Token: 0x0600245F RID: 9311 RVA: 0x0009EB70 File Offset: 0x0009CD70
	public override void UpdateState()
	{
		this.KamikazeSootEnemy.FacePlayer();
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

	// Token: 0x06002460 RID: 9312 RVA: 0x0009EC04 File Offset: 0x0009CE04
	public override void OnEnter()
	{
		this.KamikazeSootEnemy.PlayAnimationOnce(this.KamikazeSootEnemy.Animations.Alert, 0);
		if (this.KamikazeSootEnemy.AlertSound)
		{
			this.KamikazeSootEnemy.AlertSound.Play();
		}
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x0009EC52 File Offset: 0x0009CE52
	public override void OnExit()
	{
	}
}
