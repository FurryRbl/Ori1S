using System;

// Token: 0x02000587 RID: 1415
public abstract class KamikazeSootEnemyState : GroundEnemyState
{
	// Token: 0x06002483 RID: 9347 RVA: 0x0009F46C File Offset: 0x0009D66C
	public KamikazeSootEnemyState(KamikazeSootEnemy kamikazeSootEnemy) : base(kamikazeSootEnemy)
	{
		this.KamikazeSootEnemy = kamikazeSootEnemy;
	}

	// Token: 0x04001EC0 RID: 7872
	protected KamikazeSootEnemy KamikazeSootEnemy;
}
