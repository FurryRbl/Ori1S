using System;

// Token: 0x02000386 RID: 902
public interface IFreezable
{
	// Token: 0x1400002E RID: 46
	// (add) Token: 0x060019A2 RID: 6562
	// (remove) Token: 0x060019A3 RID: 6563
	event Action OnFreezeEvent;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x060019A4 RID: 6564
	// (remove) Token: 0x060019A5 RID: 6565
	event Action OnUnfreezeEvent;

	// Token: 0x060019A6 RID: 6566
	bool CanBeFrozen();

	// Token: 0x060019A7 RID: 6567
	void Freeze();

	// Token: 0x060019A8 RID: 6568
	void Unfreeze();

	// Token: 0x060019A9 RID: 6569
	bool IsFrozen();
}
