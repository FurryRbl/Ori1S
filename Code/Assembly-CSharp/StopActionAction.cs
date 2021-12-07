using System;

// Token: 0x02000353 RID: 851
[Category("Sequence")]
public class StopActionAction : ActionWithDuration
{
	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x06001850 RID: 6224 RVA: 0x000686BF File Offset: 0x000668BF
	// (set) Token: 0x06001851 RID: 6225 RVA: 0x000686C7 File Offset: 0x000668C7
	public override float Duration { get; set; }

	// Token: 0x06001852 RID: 6226 RVA: 0x000686D0 File Offset: 0x000668D0
	public override void Perform(IContext context)
	{
		this.SequenceToRun.Stop();
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x000686DD File Offset: 0x000668DD
	public override void Stop()
	{
	}

	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x06001854 RID: 6228 RVA: 0x000686DF File Offset: 0x000668DF
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x040014E4 RID: 5348
	[NotNull]
	public ActionWithDuration SequenceToRun;
}
