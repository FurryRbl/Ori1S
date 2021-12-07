using System;

// Token: 0x020005CF RID: 1487
public class StarSlugState : SlugState
{
	// Token: 0x06002574 RID: 9588 RVA: 0x000A3847 File Offset: 0x000A1A47
	public StarSlugState(StarSlugEnemy slug) : base(slug)
	{
		this.StarSlugEnemy = slug;
	}

	// Token: 0x0400200C RID: 8204
	protected StarSlugEnemy StarSlugEnemy;
}
