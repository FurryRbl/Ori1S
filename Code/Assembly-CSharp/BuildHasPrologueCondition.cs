using System;
using Core;

// Token: 0x0200027B RID: 635
public class BuildHasPrologueCondition : Condition
{
	// Token: 0x06001503 RID: 5379 RVA: 0x0005E32C File Offset: 0x0005C52C
	public override bool Validate(IContext context)
	{
		foreach (RuntimeSceneMetaData runtimeSceneMetaData in Scenes.Manager.AllScenes)
		{
			if (runtimeSceneMetaData.Scene == "swallowsNestStartOfLeafJourney")
			{
				return true;
			}
		}
		return false;
	}
}
