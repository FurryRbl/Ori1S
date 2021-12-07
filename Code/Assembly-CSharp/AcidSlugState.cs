using System;

// Token: 0x020005B5 RID: 1461
public class AcidSlugState : SlugState
{
	// Token: 0x06002533 RID: 9523 RVA: 0x000A26E8 File Offset: 0x000A08E8
	public AcidSlugState(AcidSlugEnemy slug) : base(slug)
	{
		this.AcidSlugEnemy = slug;
	}

	// Token: 0x04001FBD RID: 8125
	protected AcidSlugEnemy AcidSlugEnemy;
}
