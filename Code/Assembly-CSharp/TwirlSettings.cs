using System;

// Token: 0x020003EE RID: 1006
[Serializable]
public class TwirlSettings
{
	// Token: 0x06001B6F RID: 7023 RVA: 0x00076433 File Offset: 0x00074633
	public TwirlSettings Clone()
	{
		return (TwirlSettings)base.MemberwiseClone();
	}

	// Token: 0x040017D9 RID: 6105
	public float Strength;

	// Token: 0x040017DA RID: 6106
	public float PosVariation = 0.15f;
}
