using System;

// Token: 0x020003EC RID: 1004
[Serializable]
public class DesaturationSettings
{
	// Token: 0x06001B6B RID: 7019 RVA: 0x0007638B File Offset: 0x0007458B
	public DesaturationSettings Clone()
	{
		return (DesaturationSettings)base.MemberwiseClone();
	}

	// Token: 0x040017D5 RID: 6101
	public float Amount;
}
