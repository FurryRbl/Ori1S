using System;

// Token: 0x020009C6 RID: 2502
public class RisingWaterTrigger : SaveSerialize
{
	// Token: 0x06003696 RID: 13974 RVA: 0x000E5590 File Offset: 0x000E3790
	private void FixedUpdate()
	{
		if (!this.m_active)
		{
			return;
		}
		if (RisingWater.Available && RisingWater.Position.y >= base.transform.position.y)
		{
			this.m_active = false;
			this.Action.Perform(null);
		}
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x000E55EB File Offset: 0x000E37EB
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_active);
	}

	// Token: 0x04003179 RID: 12665
	private bool m_active = true;

	// Token: 0x0400317A RID: 12666
	[NotNull]
	public ActionMethod Action;
}
