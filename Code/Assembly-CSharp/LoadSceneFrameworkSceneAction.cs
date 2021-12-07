using System;

// Token: 0x020002F9 RID: 761
[Category("System")]
public class LoadSceneFrameworkSceneAction : ActionMethod
{
	// Token: 0x060016CB RID: 5835 RVA: 0x00063874 File Offset: 0x00061A74
	public override void Perform(IContext context)
	{
		if (this.SceneMetaData == null)
		{
			return;
		}
		GoToSceneController.Instance.GoToSceneAsync(this.SceneMetaData, null, true);
		InstantLoadScenesController.Instance.FreezeIfLoadingScenes();
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000638B0 File Offset: 0x00061AB0
	public override string GetNiceName()
	{
		if (this.SceneMetaData == null)
		{
			return "Load scene: Null";
		}
		return "Load scene: " + this.SceneMetaData.SceneName;
	}

	// Token: 0x040013A8 RID: 5032
	[NotNull]
	public SceneMetaData SceneMetaData;
}
