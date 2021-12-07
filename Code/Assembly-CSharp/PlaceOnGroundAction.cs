using System;
using Game;

// Token: 0x0200030D RID: 781
[Category("General")]
public class PlaceOnGroundAction : ActionMethod
{
	// Token: 0x06001732 RID: 5938 RVA: 0x0006446F File Offset: 0x0006266F
	public override void Perform(IContext context)
	{
		Characters.Current.PlaceOnGround();
	}
}
