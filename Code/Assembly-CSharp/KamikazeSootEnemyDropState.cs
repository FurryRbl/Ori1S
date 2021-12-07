using System;

// Token: 0x02000580 RID: 1408
public class KamikazeSootEnemyDropState : KamikazeSootEnemyState
{
	// Token: 0x06002456 RID: 9302 RVA: 0x0009E9E0 File Offset: 0x0009CBE0
	public KamikazeSootEnemyDropState(KamikazeSootEnemy kamikazeSootEnemy) : base(kamikazeSootEnemy)
	{
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x0009E9EC File Offset: 0x0009CBEC
	public override void UpdateState()
	{
		this.KamikazeSootEnemy.ApplyGravity(this.KamikazeSootEnemy.Settings.Gravity, this.KamikazeSootEnemy.Settings.MaxFallSpeed);
		this.KamikazeSootEnemy.Decelerate(this.KamikazeSootEnemy.Settings.Decceleration);
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x0009EA3F File Offset: 0x0009CC3F
	public override void OnEnter()
	{
		this.KamikazeSootEnemy.PlayAnimationOnce(this.KamikazeSootEnemy.Animations.Drop, 0);
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x0009EA5D File Offset: 0x0009CC5D
	public override void OnExit()
	{
	}
}
