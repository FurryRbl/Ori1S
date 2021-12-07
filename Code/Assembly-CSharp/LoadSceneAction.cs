using System;

// Token: 0x020002F8 RID: 760
[Category("System")]
public class LoadSceneAction : ActionMethod
{
	// Token: 0x060016C8 RID: 5832 RVA: 0x000637F7 File Offset: 0x000619F7
	public override void Perform(IContext context)
	{
		if (this.UseSceneInitialValues)
		{
			GameController.Instance.RequireInitialValues = true;
		}
		GoToSceneController.Instance.GoToSceneAsync(this.SceneMetaData, null, this.CreateCheckpoint);
		InstantLoadScenesController.Instance.FreezeIfLoadingScenes();
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x00063830 File Offset: 0x00061A30
	public override string GetNiceName()
	{
		if (this.SceneMetaData != null)
		{
			return "Load scene: " + this.SceneMetaData.SceneName;
		}
		return "Load scene: (null) ";
	}

	// Token: 0x040013A5 RID: 5029
	[NotNull]
	public SceneMetaData SceneMetaData;

	// Token: 0x040013A6 RID: 5030
	public bool CreateCheckpoint;

	// Token: 0x040013A7 RID: 5031
	public bool UseSceneInitialValues;
}
