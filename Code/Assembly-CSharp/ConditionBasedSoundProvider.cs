using System;

// Token: 0x020001CD RID: 461
public class ConditionBasedSoundProvider : SoundProvider
{
	// Token: 0x060010BD RID: 4285 RVA: 0x0004C859 File Offset: 0x0004AA59
	public override SoundDescriptor GetSound(IContext context)
	{
		if (this.Condition.Validate(context))
		{
			return this.TrueSoundProvider.GetSound(context);
		}
		return this.FalseSoundProvider.GetSound(context);
	}

	// Token: 0x04000E26 RID: 3622
	public SoundProvider TrueSoundProvider;

	// Token: 0x04000E27 RID: 3623
	public SoundProvider FalseSoundProvider;

	// Token: 0x04000E28 RID: 3624
	public Condition Condition;
}
