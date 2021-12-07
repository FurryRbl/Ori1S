using System;

// Token: 0x02000922 RID: 2338
public class SpawnProjectileAction : ActionMethod
{
	// Token: 0x060033CD RID: 13261 RVA: 0x000DA119 File Offset: 0x000D8319
	public override void Perform(IContext context)
	{
		if (this.Spawner)
		{
			this.Spawner.SpawnProjectile();
		}
	}

	// Token: 0x04002EC9 RID: 11977
	[NotNull]
	public ProjectileSpawner Spawner;
}
