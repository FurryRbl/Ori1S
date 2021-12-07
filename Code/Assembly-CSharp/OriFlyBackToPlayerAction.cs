using System;
using Game;

// Token: 0x02000306 RID: 774
[Category("Ori")]
public class OriFlyBackToPlayerAction : ActionWithDuration
{
	// Token: 0x06001711 RID: 5905 RVA: 0x00064086 File Offset: 0x00062286
	public override void Perform(IContext context)
	{
		Characters.Ori.ChangeState(Ori.State.Hovering);
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00064093 File Offset: 0x00062293
	public override void Stop()
	{
	}

	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x06001713 RID: 5907 RVA: 0x00064095 File Offset: 0x00062295
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06001714 RID: 5908 RVA: 0x0006409C File Offset: 0x0006229C
	// (set) Token: 0x06001715 RID: 5909 RVA: 0x000640A3 File Offset: 0x000622A3
	public override float Duration
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}
}
