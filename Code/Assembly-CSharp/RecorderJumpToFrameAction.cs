using System;

// Token: 0x0200031B RID: 795
public class RecorderJumpToFrameAction : ActionMethod
{
	// Token: 0x06001761 RID: 5985 RVA: 0x00064E29 File Offset: 0x00063029
	public override void Perform(IContext context)
	{
		RecorderPlaybackUI.Instance.JumpToFrame(this.FrameIndex);
	}

	// Token: 0x04001414 RID: 5140
	public int FrameIndex;
}
