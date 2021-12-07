using System;

// Token: 0x020002AE RID: 686
public class CounterAction : ActionMethod
{
	// Token: 0x060015B9 RID: 5561 RVA: 0x0006011C File Offset: 0x0005E31C
	public override void Perform(IContext context)
	{
		switch (this.Operation)
		{
		case CounterAction.OperationType.Add:
			this.Counter.Value += this.Value;
			break;
		case CounterAction.OperationType.Subtract:
			this.Counter.Value -= this.Value;
			break;
		case CounterAction.OperationType.Set:
			this.Counter.Value = this.Value;
			break;
		}
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x00060198 File Offset: 0x0005E398
	public override string GetNiceName()
	{
		switch (this.Operation)
		{
		case CounterAction.OperationType.Add:
			return string.Concat(new object[]
			{
				"Add ",
				this.Value,
				" to ",
				ActionHelper.GetName(this.Counter)
			});
		case CounterAction.OperationType.Subtract:
			return string.Concat(new object[]
			{
				"Subtract ",
				this.Value,
				" from ",
				ActionHelper.GetName(this.Counter)
			});
		case CounterAction.OperationType.Set:
			return string.Concat(new object[]
			{
				"Set ",
				ActionHelper.GetName(this.Counter),
				" to ",
				this.Value
			});
		default:
			return base.GetNiceName();
		}
	}

	// Token: 0x040012A1 RID: 4769
	public NumberCounter Counter;

	// Token: 0x040012A2 RID: 4770
	public CounterAction.OperationType Operation;

	// Token: 0x040012A3 RID: 4771
	public int Value;

	// Token: 0x020002AF RID: 687
	public enum OperationType
	{
		// Token: 0x040012A5 RID: 4773
		Add,
		// Token: 0x040012A6 RID: 4774
		Subtract,
		// Token: 0x040012A7 RID: 4775
		Set
	}
}
