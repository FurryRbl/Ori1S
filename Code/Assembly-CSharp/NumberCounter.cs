using System;

// Token: 0x020002B0 RID: 688
public class NumberCounter : SaveSerialize
{
	// Token: 0x060015BC RID: 5564 RVA: 0x00060279 File Offset: 0x0005E479
	public override void Serialize(Archive ar)
	{
		ar.Serialize(this.Value);
	}

	// Token: 0x040012A8 RID: 4776
	public int Value;
}
