using System;

// Token: 0x020002C7 RID: 711
[Category("Lever")]
public class ChangeLeverDirectionAction : ActionWithDuration
{
	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06001609 RID: 5641 RVA: 0x00061865 File Offset: 0x0005FA65
	// (set) Token: 0x0600160A RID: 5642 RVA: 0x0006186D File Offset: 0x0005FA6D
	public override float Duration { get; set; }

	// Token: 0x0600160B RID: 5643 RVA: 0x00061876 File Offset: 0x0005FA76
	public override void Perform(IContext context)
	{
		this.Lever.SetLeverDirection(this.NewLeverDirection);
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x00061889 File Offset: 0x0005FA89
	public override void Stop()
	{
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x0600160D RID: 5645 RVA: 0x0006188B File Offset: 0x0005FA8B
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x040012FB RID: 4859
	[NotNull]
	public Lever Lever;

	// Token: 0x040012FC RID: 4860
	public Lever.LeverDirections NewLeverDirection;
}
