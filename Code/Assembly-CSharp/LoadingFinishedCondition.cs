using System;
using Core;

// Token: 0x02000298 RID: 664
public class LoadingFinishedCondition : Condition
{
	// Token: 0x0600156A RID: 5482 RVA: 0x0005F100 File Offset: 0x0005D300
	public override bool Validate(IContext context)
	{
		if (this.SceneMetaData)
		{
			return !Scenes.Manager.IsLoadingScene(this.SceneMetaData.SeinPlaceholderPosition);
		}
		return !Scenes.Manager.IsLoadingScenes;
	}

	// Token: 0x04001285 RID: 4741
	public SceneMetaData SceneMetaData;
}
