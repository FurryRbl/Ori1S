using System;

// Token: 0x020002D9 RID: 729
[Category("General")]
public class DestroyCharacterAction : ActionMethod
{
	// Token: 0x06001665 RID: 5733 RVA: 0x00062A0D File Offset: 0x00060C0D
	public override void Perform(IContext context)
	{
		CharacterFactory.Instance.DestroyCharacter();
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x00062A19 File Offset: 0x00060C19
	public override string GetNiceName()
	{
		return "Destroy current character";
	}
}
