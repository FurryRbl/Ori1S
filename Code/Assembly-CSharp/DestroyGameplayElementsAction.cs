using System;
using Game;

// Token: 0x020002DA RID: 730
[Category("Sein")]
public class DestroyGameplayElementsAction : ActionMethod
{
	// Token: 0x06001668 RID: 5736 RVA: 0x00062A28 File Offset: 0x00060C28
	public override void Perform(IContext context)
	{
		SaveSceneManager.Master.Save(Game.Checkpoint.SaveGameData.Master);
		GameController.Instance.RemoveGameplayObjects();
	}
}
