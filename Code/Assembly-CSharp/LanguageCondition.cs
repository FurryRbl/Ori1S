using System;

// Token: 0x02000297 RID: 663
public class LanguageCondition : Condition
{
	// Token: 0x06001568 RID: 5480 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
	public override bool Validate(IContext context)
	{
		return GameSettings.Instance.Language == this.Language;
	}

	// Token: 0x04001284 RID: 4740
	public Language Language;
}
