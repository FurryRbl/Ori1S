using System;

// Token: 0x020003EB RID: 1003
[Serializable]
public class VignettingSettings
{
	// Token: 0x06001B69 RID: 7017 RVA: 0x00076376 File Offset: 0x00074576
	public VignettingSettings Clone()
	{
		return (VignettingSettings)base.MemberwiseClone();
	}

	// Token: 0x040017D4 RID: 6100
	public float Intensity = 0.375f;
}
