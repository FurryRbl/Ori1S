using System;

// Token: 0x020007C9 RID: 1993
public class FloatCageMetaData : CageFaceMetaData<float>
{
	// Token: 0x06002DD5 RID: 11733 RVA: 0x000C373A File Offset: 0x000C193A
	public override void Serialize(ref float worldMapAreaState, Archive ar)
	{
		ar.Serialize(ref worldMapAreaState);
	}
}
