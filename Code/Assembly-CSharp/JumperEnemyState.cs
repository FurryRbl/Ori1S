using System;

// Token: 0x02000550 RID: 1360
public abstract class JumperEnemyState : GroundEnemyState
{
	// Token: 0x06002385 RID: 9093 RVA: 0x0009B285 File Offset: 0x00099485
	public JumperEnemyState(JumperEnemy groundEnemy) : base(groundEnemy)
	{
		this.JumperEnemy = groundEnemy;
	}

	// Token: 0x04001DC7 RID: 7623
	protected JumperEnemy JumperEnemy;
}
