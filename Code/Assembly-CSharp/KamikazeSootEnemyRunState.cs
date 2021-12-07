using System;

// Token: 0x0200058A RID: 1418
public class KamikazeSootEnemyRunState : KamikazeSootEnemyState
{
	// Token: 0x06002487 RID: 9351 RVA: 0x0009F58B File Offset: 0x0009D78B
	public KamikazeSootEnemyRunState(KamikazeSootEnemy kamikazeSootEnemy) : base(kamikazeSootEnemy)
	{
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x0009F594 File Offset: 0x0009D794
	public override void UpdateState()
	{
		this.KamikazeSootEnemy.FacePlayer();
		this.KamikazeSootEnemy.AccelerateForwards(this.KamikazeSootEnemy.Settings.RunAcceleration, this.KamikazeSootEnemy.Settings.MaxRunSpeed);
		this.KamikazeSootEnemy.ApplyGravity(this.KamikazeSootEnemy.Settings.Gravity, this.KamikazeSootEnemy.Settings.MaxFallSpeed);
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x0009F602 File Offset: 0x0009D802
	public override void OnEnter()
	{
		this.KamikazeSootEnemy.RestartAnimationLoop(this.KamikazeSootEnemy.Animations.Run, 0);
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x0009F620 File Offset: 0x0009D820
	public override void OnExit()
	{
	}
}
