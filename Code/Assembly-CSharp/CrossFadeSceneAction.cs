using System;
using Game;

// Token: 0x020002D5 RID: 725
[Category("System")]
public class CrossFadeSceneAction : ActionMethod
{
	// Token: 0x0600165A RID: 5722 RVA: 0x00062908 File Offset: 0x00060B08
	public override void Perform(IContext context)
	{
		GoToSceneController.Instance.StartInScene = this.SceneMetaData.SceneMoonGuid;
		UI.Cameras.System.CrossFadeManager.PerformCrossFade(this.SceneMetaData, this.Duration);
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x00062948 File Offset: 0x00060B48
	public override string GetNiceName()
	{
		if (this.SceneMetaData != null)
		{
			return "Cross fade scene: " + this.SceneMetaData.SceneName;
		}
		return "Cross fade scene: unknown";
	}

	// Token: 0x04001354 RID: 4948
	[NotNull]
	public SceneMetaData SceneMetaData;

	// Token: 0x04001355 RID: 4949
	public float Duration;
}
