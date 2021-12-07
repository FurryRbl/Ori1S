using System;
using UnityEngine;

// Token: 0x020008AA RID: 2218
public class IsRendererVisible : Condition
{
	// Token: 0x06003189 RID: 12681 RVA: 0x000D321B File Offset: 0x000D141B
	public override bool Validate(IContext context)
	{
		return this.Renderer.isVisible == this.Visible;
	}

	// Token: 0x04002CC5 RID: 11461
	public bool Visible = true;

	// Token: 0x04002CC6 RID: 11462
	[NotNull]
	public Renderer Renderer;
}
