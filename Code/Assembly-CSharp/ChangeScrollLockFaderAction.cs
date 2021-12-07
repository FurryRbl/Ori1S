using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
[Category("Obsolete")]
public class ChangeScrollLockFaderAction : ActionMethod
{
	// Token: 0x06001639 RID: 5689 RVA: 0x00062248 File Offset: 0x00060448
	public override void Perform(IContext context)
	{
		if (this.ScrollLock)
		{
			this.ScrollLock.Fader = this.ScrollLockFader;
		}
	}

	// Token: 0x04001339 RID: 4921
	public GameObject ScrollLockFader;

	// Token: 0x0400133A RID: 4922
	public CameraScrollLock ScrollLock;
}
