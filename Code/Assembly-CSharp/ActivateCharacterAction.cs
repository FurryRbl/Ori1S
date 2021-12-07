using System;
using Game;

// Token: 0x020002B4 RID: 692
public class ActivateCharacterAction : ActionMethod
{
	// Token: 0x060015CA RID: 5578 RVA: 0x000604C0 File Offset: 0x0005E6C0
	public override void Perform(IContext context)
	{
		if (Characters.Current != null)
		{
			Characters.Current.Activate(this.Active);
		}
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x000604DC File Offset: 0x0005E6DC
	public override string GetNiceName()
	{
		return (!this.Active) ? "Deactivate Character" : "Activate Character";
	}

	// Token: 0x040012B2 RID: 4786
	public bool Active = true;
}
