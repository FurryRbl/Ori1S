using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032D RID: 813
[Category("Messaging")]
public class SendMessageCurveAction : ActionWithDuration
{
	// Token: 0x060017AF RID: 6063 RVA: 0x00065DA0 File Offset: 0x00063FA0
	public override void Perform(IContext context)
	{
		for (int i = 0; i < this.Targets.Length; i++)
		{
			base.StartCoroutine(this.Perform(i));
		}
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x00065DD4 File Offset: 0x00063FD4
	public IEnumerator Perform(int index)
	{
		GameObject target = this.Targets[index];
		float delay = this.Delay.Evaluate((float)index);
		for (float t = 0f; t < delay; t += ((!this.IsSuspended) ? Time.deltaTime : 0f))
		{
			yield return new WaitForFixedUpdate();
		}
		switch (this.TargetMessageType)
		{
		case SendMessageCurveAction.MessageType.Send:
			target.SendMessage(this.Message);
			break;
		case SendMessageCurveAction.MessageType.Broadcast:
			target.BroadcastMessage(this.Message);
			break;
		case SendMessageCurveAction.MessageType.SendUpwards:
			target.SendMessageUpwards(this.Message);
			break;
		}
		yield break;
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x00065DFD File Offset: 0x00063FFD
	public override void Stop()
	{
	}

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00065DFF File Offset: 0x00063FFF
	public override bool IsPerforming
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00065E02 File Offset: 0x00064002
	// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00065E09 File Offset: 0x00064009
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

	// Token: 0x04001452 RID: 5202
	public GameObject[] Targets;

	// Token: 0x04001453 RID: 5203
	public AnimationCurve Delay;

	// Token: 0x04001454 RID: 5204
	public string Message;

	// Token: 0x04001455 RID: 5205
	public SendMessageCurveAction.MessageType TargetMessageType;

	// Token: 0x0200032E RID: 814
	public enum MessageType
	{
		// Token: 0x04001457 RID: 5207
		Send,
		// Token: 0x04001458 RID: 5208
		Broadcast,
		// Token: 0x04001459 RID: 5209
		SendUpwards
	}
}
