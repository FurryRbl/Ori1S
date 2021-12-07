using System;

// Token: 0x0200030B RID: 779
[Category("Sequence")]
public class PerformActionAction : ActionWithDuration
{
	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x0600172A RID: 5930 RVA: 0x00064420 File Offset: 0x00062620
	// (set) Token: 0x0600172B RID: 5931 RVA: 0x00064428 File Offset: 0x00062628
	public override float Duration { get; set; }

	// Token: 0x0600172C RID: 5932 RVA: 0x00064431 File Offset: 0x00062631
	public override void Perform(IContext context)
	{
		this.SequenceToRun.Perform(context);
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x0006443F File Offset: 0x0006263F
	public override void Stop()
	{
		this.SequenceToRun.Stop();
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x0600172E RID: 5934 RVA: 0x0006444C File Offset: 0x0006264C
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x040013E6 RID: 5094
	[NotNull]
	public ActionWithDuration SequenceToRun;
}
