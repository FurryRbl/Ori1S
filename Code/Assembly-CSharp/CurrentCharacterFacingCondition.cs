using System;
using Game;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class CurrentCharacterFacingCondition : Condition
{
	// Token: 0x06001529 RID: 5417 RVA: 0x0005E5AB File Offset: 0x0005C7AB
	public override bool Validate(IContext context)
	{
		if (Characters.Current as Component)
		{
			Characters.Current.FaceLeft = this.FaceLeft;
		}
		return false;
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x0005E5D4 File Offset: 0x0005C7D4
	public override string GetNiceName()
	{
		return "Character facing " + ((!this.FaceLeft) ? "right" : "left");
	}

	// Token: 0x04001251 RID: 4689
	public bool FaceLeft;
}
