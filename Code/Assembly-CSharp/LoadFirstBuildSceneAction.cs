using System;

// Token: 0x020002F6 RID: 758
[Category("System")]
public class LoadFirstBuildSceneAction : ActionMethod
{
	// Token: 0x060016C3 RID: 5827 RVA: 0x0006376C File Offset: 0x0006196C
	public override void Perform(IContext context)
	{
		if (this.SceneMetaData == null)
		{
			return;
		}
		GoToSceneController.Instance.GoToScene(this.SceneMetaData, null, true);
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000637A0 File Offset: 0x000619A0
	public override string GetNiceName()
	{
		if (this.SceneMetaData == null)
		{
			return "Load scene: Null";
		}
		return "Load scene:" + this.SceneMetaData.SceneName;
	}

	// Token: 0x040013A4 RID: 5028
	public SceneMetaData SceneMetaData;
}
