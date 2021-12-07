using System;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class DamageTypeCondition : Condition
{
	// Token: 0x06001535 RID: 5429 RVA: 0x0005E714 File Offset: 0x0005C914
	public override bool Validate(IContext context)
	{
		if (context is IDamageContext)
		{
			IDamageContext damageContext = context as IDamageContext;
			Damage damage = damageContext.Damage;
			if (this.ValidateDamageType)
			{
				bool flag = false;
				for (int i = 0; i < this.Allowed.Length; i++)
				{
					DamageType damageType = this.Allowed[i];
					if (damage.Type == damageType)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return (!this.ValidateDamageDirection || MoonMath.Normal.WithinDegrees(damage.Force, this.DamageDirection, this.DamageDirectionWithinDegrees)) && (!this.ValidateDamageAmount || LogicUtility.Compare(damage.Amount, this.DamageAmount, this.DamageAmountComparison));
		}
		return false;
	}

	// Token: 0x0400125A RID: 4698
	public bool ValidateDamageType;

	// Token: 0x0400125B RID: 4699
	public DamageType[] Allowed;

	// Token: 0x0400125C RID: 4700
	public bool ValidateDamageDirection;

	// Token: 0x0400125D RID: 4701
	public Vector2 DamageDirection;

	// Token: 0x0400125E RID: 4702
	public float DamageDirectionWithinDegrees;

	// Token: 0x0400125F RID: 4703
	public bool ValidateDamageAmount;

	// Token: 0x04001260 RID: 4704
	public LogicUtility.ComparisonType DamageAmountComparison;

	// Token: 0x04001261 RID: 4705
	public float DamageAmount;
}
