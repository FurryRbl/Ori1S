using System;

// Token: 0x0200059A RID: 1434
public abstract class RammingEnemyState : GroundEnemyState
{
	// Token: 0x060024B7 RID: 9399 RVA: 0x000A0162 File Offset: 0x0009E362
	public RammingEnemyState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
		this.RammingEnemy = rammingEnemy;
	}

	// Token: 0x04001F01 RID: 7937
	public RammingEnemy RammingEnemy;
}
