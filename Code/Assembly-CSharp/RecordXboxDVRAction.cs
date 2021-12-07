using System;

// Token: 0x02000319 RID: 793
public class RecordXboxDVRAction : ActionMethod
{
	// Token: 0x0600175E RID: 5982 RVA: 0x00064DC8 File Offset: 0x00062FC8
	public override void Perform(IContext context)
	{
		XboxOneDVRManager.RecordPastDelayed(this.DelayRecording, this.ClipLength - this.DelayRecording, this.GetXboxOneDRVID(this.ClipID));
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x00064DFC File Offset: 0x00062FFC
	private string GetXboxOneDRVID(RecordXboxDVRAction.XboxOneDVRID id)
	{
		if (id != RecordXboxDVRAction.XboxOneDVRID.EscapeBoulder)
		{
			return "0";
		}
		return XboxOneDVR.ESCAPED_BOULDER_ID;
	}

	// Token: 0x0400140F RID: 5135
	public RecordXboxDVRAction.XboxOneDVRID ClipID;

	// Token: 0x04001410 RID: 5136
	public float ClipLength = 30f;

	// Token: 0x04001411 RID: 5137
	public float DelayRecording;

	// Token: 0x0200031A RID: 794
	public enum XboxOneDVRID
	{
		// Token: 0x04001413 RID: 5139
		EscapeBoulder
	}
}
