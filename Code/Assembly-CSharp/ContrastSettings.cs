using System;

// Token: 0x020003DE RID: 990
[Serializable]
public class ContrastSettings
{
	// Token: 0x06001B15 RID: 6933 RVA: 0x00073E95 File Offset: 0x00072095
	public ContrastSettings Clone()
	{
		return (ContrastSettings)base.MemberwiseClone();
	}

	// Token: 0x04001789 RID: 6025
	public float Brightness;

	// Token: 0x0400178A RID: 6026
	public float Contrast;
}
