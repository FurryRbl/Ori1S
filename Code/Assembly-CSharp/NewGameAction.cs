using System;
using Game;

// Token: 0x0200085F RID: 2143
public class NewGameAction : ActionMethod
{
	// Token: 0x06003091 RID: 12433 RVA: 0x000CE973 File Offset: 0x000CCB73
	public override void Perform(IContext context)
	{
		Game.Checkpoint.SaveGameData = new SaveGameData();
		GameController.Instance.RequireInitialValues = true;
		GameStateMachine.Instance.SetToGame();
	}
}
