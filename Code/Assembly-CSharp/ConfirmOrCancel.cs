using System;
using Core;
using UnityEngine;

// Token: 0x02000705 RID: 1797
public class ConfirmOrCancel : MonoBehaviour
{
	// Token: 0x1400003E RID: 62
	// (add) Token: 0x06002AAF RID: 10927 RVA: 0x000B6F1C File Offset: 0x000B511C
	// (remove) Token: 0x06002AB0 RID: 10928 RVA: 0x000B6F35 File Offset: 0x000B5135
	public event Action OnConfirm;

	// Token: 0x06002AB1 RID: 10929 RVA: 0x000B6F4E File Offset: 0x000B514E
	private bool CanCopyOrDelete()
	{
		return true;
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x000B6F54 File Offset: 0x000B5154
	public void FixedUpdate()
	{
		if (Core.Input.ActionButtonA.OnPressed && this.CanCopyOrDelete())
		{
			Core.Input.ActionButtonA.Used = true;
			if (this.OnConfirm != null)
			{
				this.OnConfirm();
			}
			if (this.OnConfirmAction)
			{
				this.OnConfirmAction.Perform(null);
			}
			base.enabled = false;
		}
		if (Core.Input.Cancel.OnPressed)
		{
			Core.Input.Cancel.Used = true;
			if (this.OnCancel != null)
			{
				this.OnCancel();
			}
			if (this.OnCancelAction)
			{
				this.OnConfirmAction.Perform(null);
			}
			base.enabled = false;
		}
	}

	// Token: 0x040025FA RID: 9722
	public Action OnCancel;

	// Token: 0x040025FB RID: 9723
	public ActionMethod OnConfirmAction;

	// Token: 0x040025FC RID: 9724
	public ActionMethod OnCancelAction;
}
