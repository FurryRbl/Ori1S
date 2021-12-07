using System;
using Game;
using UnityEngine;

// Token: 0x02000314 RID: 788
[Category("Sein")]
public class PlayerFaceDirectionAction : ActionMethod
{
	// Token: 0x06001753 RID: 5971 RVA: 0x00064CBC File Offset: 0x00062EBC
	public override void Perform(IContext context)
	{
		bool faceLeft = this.FaceLeft;
		if (this.Target)
		{
			faceLeft = (this.Target.position.x < Characters.Current.Position.x);
		}
		Characters.Current.FaceLeft = faceLeft;
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x00064D14 File Offset: 0x00062F14
	public override string GetNiceName()
	{
		if (this.Target)
		{
			return "Face player towards target";
		}
		return "Face player to the " + ((!this.FaceLeft) ? "right" : "left");
	}

	// Token: 0x0400140C RID: 5132
	public bool FaceLeft;

	// Token: 0x0400140D RID: 5133
	public Transform Target;
}
