using System;
using Game;

// Token: 0x020008A2 RID: 2210
public class WorldMapSetObjectiveTextAction : ActionMethod
{
	// Token: 0x06003175 RID: 12661 RVA: 0x000D3018 File Offset: 0x000D1218
	public override void Perform(IContext context)
	{
		if (!this.ObjectiveText)
		{
			GameWorld.Instance.ObjectiveText = null;
			return;
		}
		GameWorld.Instance.ObjectiveText = this.ObjectiveText;
		this.NewObjectiveMessageProvider.SetMessage(this.ObjectiveText);
		if (this.NewObjectiveMessageProvider)
		{
			UI.Hints.Show(this.NewObjectiveMessageProvider, HintLayer.GameSaved, 3f);
		}
	}

	// Token: 0x04002CB7 RID: 11447
	public MessageProvider ObjectiveText;

	// Token: 0x04002CB8 RID: 11448
	public NewObjectiveMessageProvider NewObjectiveMessageProvider;
}
