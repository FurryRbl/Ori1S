using System;
using Game;

// Token: 0x0200029F RID: 671
public class SafeToShowWispTextCondition : Condition
{
	// Token: 0x06001598 RID: 5528 RVA: 0x0005FE7C File Offset: 0x0005E07C
	public override bool Validate(IContext context)
	{
		if (Characters.Sein.Controller.IsSwimming)
		{
			return false;
		}
		if (Characters.Sein.Mortality.DamageReciever.IsInvinsible)
		{
			this.m_keepFalse = 4;
			return false;
		}
		return Characters.Sein.Active && this.m_keepFalse <= 0 && Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround;
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x0005FEF4 File Offset: 0x0005E0F4
	public void FixedUpdate()
	{
		if (this.m_keepFalse > 0)
		{
			this.m_keepFalse--;
		}
	}

	// Token: 0x04001297 RID: 4759
	private int m_keepFalse;
}
