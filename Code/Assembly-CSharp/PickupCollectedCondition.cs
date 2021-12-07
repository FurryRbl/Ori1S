using System;

// Token: 0x02000913 RID: 2323
public class PickupCollectedCondition : Condition
{
	// Token: 0x0600338A RID: 13194 RVA: 0x000D932C File Offset: 0x000D752C
	public override bool Validate(IContext context)
	{
		if (this.Pickup)
		{
			return this.IsCollected == this.Pickup.IsCollected;
		}
		return this.Placeholder && this.IsCollected == this.Placeholder.Collected;
	}

	// Token: 0x04002E8A RID: 11914
	public PickupBase Pickup;

	// Token: 0x04002E8B RID: 11915
	public CollectablePlaceholder Placeholder;

	// Token: 0x04002E8C RID: 11916
	public bool IsCollected;
}
