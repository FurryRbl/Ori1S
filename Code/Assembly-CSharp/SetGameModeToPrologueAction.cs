using System;
using Core;

// Token: 0x02000860 RID: 2144
public class SetGameModeToPrologueAction : ActionMethod
{
	// Token: 0x06003093 RID: 12435 RVA: 0x000CE99C File Offset: 0x000CCB9C
	public override void Perform(IContext context)
	{
		GameController.Instance.RequireInitialValues = true;
		GameStateMachine.Instance.SetToPrologue();
		RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation("sunkenGladesRunaway");
		if (sceneInformation != null)
		{
			Scenes.Manager.PreloadScene(sceneInformation);
		}
	}
}
