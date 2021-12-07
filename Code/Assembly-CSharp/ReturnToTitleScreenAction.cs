using System;

// Token: 0x0200015C RID: 348
public class ReturnToTitleScreenAction : ActionMethod
{
	// Token: 0x06000E16 RID: 3606 RVA: 0x0004196A File Offset: 0x0003FB6A
	public override void Perform(IContext context)
	{
		GameController.Instance.RestartGame();
	}
}
