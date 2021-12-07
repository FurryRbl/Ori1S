using System;

// Token: 0x0200094F RID: 2383
public class ValueWithMaxValueSerializer : SaveSerialize
{
	// Token: 0x06003478 RID: 13432 RVA: 0x000DC5C0 File Offset: 0x000DA7C0
	public override void Serialize(Archive ar)
	{
		this.ValueWithMinMax.Value = ar.Serialize(this.ValueWithMinMax.Value);
	}

	// Token: 0x04002F57 RID: 12119
	public ValueWithMaxValue ValueWithMinMax;
}
