using System;

// Token: 0x02000322 RID: 802
public class RestoreRecorderToLastCheckpointAction : ActionMethod
{
	// Token: 0x06001778 RID: 6008 RVA: 0x000651C6 File Offset: 0x000633C6
	public override void Perform(IContext context)
	{
		RecorderPlaybackUI.Instance.PreviousKeyframe();
	}
}
