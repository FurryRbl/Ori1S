using System;
using Game;

// Token: 0x020002D1 RID: 721
[Category("Camera")]
public class ChangeTargetToCurrentCharacterAction : ActionMethod
{
	// Token: 0x0600164C RID: 5708 RVA: 0x0006263E File Offset: 0x0006083E
	public override void Perform(IContext context)
	{
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x0006264A File Offset: 0x0006084A
	public override string GetNiceName()
	{
		return "Change camera target to current character";
	}
}
