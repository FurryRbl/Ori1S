using System;
using Game;

// Token: 0x02000344 RID: 836
public class ShowWispTextAction : PerformingAction
{
	// Token: 0x060017ED RID: 6125 RVA: 0x00066A58 File Offset: 0x00064C58
	public override void Perform(IContext context)
	{
		this.m_messageBox = UI.MessageController.ShowPickupMessage(DeathWispsManager.Instance.WispMessage, DeathWispsManager.Instance.WispIcon);
		if (this.m_messageBox)
		{
			this.m_messageBox.OnMessageScreenHide += this.OnFinishedReading;
			SuspensionManager.SuspendExcluding(this.m_messageBox.GetSuspendables());
		}
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x00066AC0 File Offset: 0x00064CC0
	public void OnFinishedReading()
	{
		if (this.m_messageBox)
		{
			this.m_messageBox.OnMessageScreenHide -= this.OnFinishedReading;
		}
		SuspensionManager.ResumeAll();
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x00066AF9 File Offset: 0x00064CF9
	public override void Stop()
	{
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00066AFB File Offset: 0x00064CFB
	public override bool IsPerforming
	{
		get
		{
			return this.m_messageBox;
		}
	}

	// Token: 0x040014A5 RID: 5285
	private MessageBox m_messageBox;
}
