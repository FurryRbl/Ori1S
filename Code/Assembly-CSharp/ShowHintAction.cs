using System;
using Game;
using UnityEngine;

// Token: 0x0200033D RID: 829
[Category("Hints")]
internal class ShowHintAction : ActionMethod
{
	// Token: 0x060017D8 RID: 6104 RVA: 0x000663F0 File Offset: 0x000645F0
	public override void Perform(IContext context)
	{
		if (this.HintMessage)
		{
			UI.Hints.Show(this.HintMessage, HintLayer.HintZone, this.Duration);
		}
	}

	// Token: 0x04001486 RID: 5254
	[NotNull]
	public Texture2D HintTexture;

	// Token: 0x04001487 RID: 5255
	public MessageProvider HintMessage;

	// Token: 0x04001488 RID: 5256
	public HintLayer Layer = HintLayer.HintZone;

	// Token: 0x04001489 RID: 5257
	public float Duration = 1f;
}
