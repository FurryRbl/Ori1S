using System;

// Token: 0x020002D6 RID: 726
public class DamageTextAction : ActionMethod
{
	// Token: 0x0600165D RID: 5725 RVA: 0x00062989 File Offset: 0x00060B89
	public override void Perform(IContext context)
	{
		this.DamageTextSource.SpawnDamageText(context);
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x00062997 File Offset: 0x00060B97
	public override string GetNiceName()
	{
		return "Spawn damage text from " + ActionHelper.GetName(this.DamageTextSource);
	}

	// Token: 0x04001356 RID: 4950
	[NotNull]
	public DamageTextSpawner DamageTextSource;
}
