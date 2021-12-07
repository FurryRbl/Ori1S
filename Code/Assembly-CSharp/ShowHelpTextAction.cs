using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class ShowHelpTextAction : PerformingAction
{
	// Token: 0x060017D3 RID: 6099 RVA: 0x00066340 File Offset: 0x00064540
	public override void Perform(IContext context)
	{
		SuspensionManager.SuspendAll();
		MessageBox messageBox = UI.MessageController.ShowHelpMessage(this.HelpMessage, this.Avatar);
		if (messageBox)
		{
			messageBox.OnMessageScreenHide += this.OnMessageScreenHide;
		}
		else
		{
			SuspensionManager.ResumeAll();
		}
		if (this.OpenSound)
		{
			Sound.Play(this.OpenSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x000663BE File Offset: 0x000645BE
	public void OnMessageScreenHide()
	{
		SuspensionManager.ResumeAll();
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x000663C5 File Offset: 0x000645C5
	public override void Stop()
	{
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000663C7 File Offset: 0x000645C7
	public override bool IsPerforming
	{
		get
		{
			return this.m_messageBox;
		}
	}

	// Token: 0x04001482 RID: 5250
	[NotNull]
	public MessageProvider HelpMessage;

	// Token: 0x04001483 RID: 5251
	[NotNull]
	public GameObject Avatar;

	// Token: 0x04001484 RID: 5252
	public SoundProvider OpenSound;

	// Token: 0x04001485 RID: 5253
	private MessageBox m_messageBox;
}
