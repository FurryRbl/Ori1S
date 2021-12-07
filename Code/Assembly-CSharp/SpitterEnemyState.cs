using System;

// Token: 0x020005E8 RID: 1512
public abstract class SpitterEnemyState : GroundEnemyState
{
	// Token: 0x06002609 RID: 9737 RVA: 0x000A6C8B File Offset: 0x000A4E8B
	public SpitterEnemyState(SpitterEnemy enemy) : base(enemy)
	{
		this.SpitterEnemy = enemy;
	}

	// Token: 0x0400208A RID: 8330
	protected SpitterEnemy SpitterEnemy;
}
