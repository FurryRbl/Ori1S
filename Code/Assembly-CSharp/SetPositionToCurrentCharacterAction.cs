using System;
using Game;
using UnityEngine;

// Token: 0x02000339 RID: 825
[Category("General")]
public class SetPositionToCurrentCharacterAction : ActionMethod
{
	// Token: 0x060017CD RID: 6093 RVA: 0x0006628C File Offset: 0x0006448C
	public override void Perform(IContext context)
	{
		Vector3 position = this.Target.position;
		if (this.UseX)
		{
			position.x = Characters.Current.Position.x;
		}
		if (this.UseY)
		{
			position.y = Characters.Current.Position.y;
		}
		this.Target.position = position;
	}

	// Token: 0x0400147C RID: 5244
	[NotNull]
	public Transform Target;

	// Token: 0x0400147D RID: 5245
	public bool UseX = true;

	// Token: 0x0400147E RID: 5246
	public bool UseY = true;
}
