using System;
using Game;
using UnityEngine;

// Token: 0x0200033A RID: 826
[Category("Hints")]
internal class ShowAbilityMessageAction : ActionMethod
{
	// Token: 0x060017CF RID: 6095 RVA: 0x00066301 File Offset: 0x00064501
	public override void Perform(IContext context)
	{
		UI.MessageController.ShowAbilityMessage(this.AbilityMessage, this.Avatar);
	}

	// Token: 0x0400147F RID: 5247
	[NotNull]
	public MessageProvider AbilityMessage;

	// Token: 0x04001480 RID: 5248
	public GameObject Avatar;
}
