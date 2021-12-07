using System;
using Game;

// Token: 0x02000335 RID: 821
public class SetCurrentCharacterAction : ActionMethod
{
	// Token: 0x060017C6 RID: 6086 RVA: 0x0006610C File Offset: 0x0006430C
	public override void Perform(IContext context)
	{
		switch (this.Select)
		{
		case SetCurrentCharacterAction.Character.Sein:
			Characters.Current = Characters.Sein;
			break;
		case SetCurrentCharacterAction.Character.Naru:
			Characters.Current = Characters.Naru;
			break;
		case SetCurrentCharacterAction.Character.BabySein:
			Characters.Current = Characters.BabySein;
			break;
		}
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x00066164 File Offset: 0x00064364
	public override string GetNiceName()
	{
		return "Set current character to " + this.Select.ToString();
	}

	// Token: 0x04001471 RID: 5233
	public SetCurrentCharacterAction.Character Select;

	// Token: 0x02000336 RID: 822
	public enum Character
	{
		// Token: 0x04001473 RID: 5235
		Sein,
		// Token: 0x04001474 RID: 5236
		Naru,
		// Token: 0x04001475 RID: 5237
		BabySein
	}
}
