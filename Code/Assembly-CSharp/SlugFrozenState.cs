using System;

// Token: 0x020005D1 RID: 1489
public class SlugFrozenState : SlugState
{
	// Token: 0x06002576 RID: 9590 RVA: 0x000A3860 File Offset: 0x000A1A60
	public SlugFrozenState(SlugEnemy slug, TextureAnimationWithTransitions frozen) : base(slug)
	{
		this.Frozen = frozen;
	}

	// Token: 0x0400200D RID: 8205
	public TextureAnimationWithTransitions Frozen;
}
