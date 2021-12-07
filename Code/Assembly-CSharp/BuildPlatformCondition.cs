using System;

// Token: 0x0200027C RID: 636
public class BuildPlatformCondition : Condition
{
	// Token: 0x06001505 RID: 5381 RVA: 0x0005E3AC File Offset: 0x0005C5AC
	public override bool Validate(IContext context)
	{
		return this.Windows && (!this.Demo || GameController.Instance.IsDemo) && (!this.FullGame || !GameController.Instance.IsTrial);
	}

	// Token: 0x04001243 RID: 4675
	public bool Windows;

	// Token: 0x04001244 RID: 4676
	public bool Xbox360;

	// Token: 0x04001245 RID: 4677
	public bool XboxOne;

	// Token: 0x04001246 RID: 4678
	public bool WindowsTen;

	// Token: 0x04001247 RID: 4679
	public bool Demo;

	// Token: 0x04001248 RID: 4680
	public bool FullGame;
}
