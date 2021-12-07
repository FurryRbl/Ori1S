using System;

// Token: 0x0200031E RID: 798
[Category("System")]
public class RegisterToSaveSceneManagerAction : ActionMethod
{
	// Token: 0x06001772 RID: 6002 RVA: 0x00065111 File Offset: 0x00063311
	public override void Perform(IContext context)
	{
		this.SaveSceneManager.AddSaveObject(this.Target, MoonGuid.Empty);
	}

	// Token: 0x04001418 RID: 5144
	public SaveSceneManager SaveSceneManager;

	// Token: 0x04001419 RID: 5145
	public SaveSerialize Target;
}
