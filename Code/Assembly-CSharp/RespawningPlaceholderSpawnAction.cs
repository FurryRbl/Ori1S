using System;

// Token: 0x0200055C RID: 1372
public class RespawningPlaceholderSpawnAction : ActionMethod
{
	// Token: 0x060023CB RID: 9163 RVA: 0x0009C99C File Offset: 0x0009AB9C
	public override void Perform(IContext context)
	{
		this.Placeholder.Spawn();
	}

	// Token: 0x04001DFC RID: 7676
	public RespawningPlaceholder Placeholder;
}
