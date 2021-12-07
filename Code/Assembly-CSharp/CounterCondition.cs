using System;

// Token: 0x020002B1 RID: 689
public class CounterCondition : Condition
{
	// Token: 0x060015BE RID: 5566 RVA: 0x00060290 File Offset: 0x0005E490
	public override bool Validate(IContext context)
	{
		return LogicUtility.Compare((float)this.Counter.Value, this.Value, this.Comparison);
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000602AF File Offset: 0x0005E4AF
	public override string GetNiceName()
	{
		return LogicUtility.GetComparisonNiceName(ActionHelper.GetName(this.Counter), this.Value.ToString(), this.Comparison);
	}

	// Token: 0x040012A9 RID: 4777
	public NumberCounter Counter;

	// Token: 0x040012AA RID: 4778
	public LogicUtility.ComparisonType Comparison;

	// Token: 0x040012AB RID: 4779
	public float Value;
}
