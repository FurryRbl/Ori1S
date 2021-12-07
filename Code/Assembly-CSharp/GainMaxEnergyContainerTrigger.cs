using System;

// Token: 0x02000368 RID: 872
public class GainMaxEnergyContainerTrigger : Trigger
{
	// Token: 0x060018FF RID: 6399 RVA: 0x0006A9AC File Offset: 0x00068BAC
	public void OnEnable()
	{
		SeinPickupProcessor.OnCollectMaxEnergyContainer = (Action)Delegate.Combine(SeinPickupProcessor.OnCollectMaxEnergyContainer, new Action(this.OnCollectMaxEnergyContainer));
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x0006A9DC File Offset: 0x00068BDC
	public void OnDisable()
	{
		SeinPickupProcessor.OnCollectMaxEnergyContainer = (Action)Delegate.Remove(SeinPickupProcessor.OnCollectMaxEnergyContainer, new Action(this.OnCollectMaxEnergyContainer));
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x0006AA09 File Offset: 0x00068C09
	public void OnCollectMaxEnergyContainer()
	{
		base.DoTrigger(true);
	}
}
