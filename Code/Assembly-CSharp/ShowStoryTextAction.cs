using System;
using Game;

// Token: 0x02000343 RID: 835
public class ShowStoryTextAction : PerformingAction
{
	// Token: 0x060017E8 RID: 6120 RVA: 0x00066964 File Offset: 0x00064B64
	public override void Perform(IContext context)
	{
		if (this.FreezeGame)
		{
			SuspensionManager.SuspendAll();
		}
		if (this.StoryMessage == null)
		{
			return;
		}
		this.m_messageBox = UI.MessageController.ShowStoryMessage(this.StoryMessage);
		if (this.m_messageBox)
		{
			this.m_messageBox.OnMessageScreenHide += this.OnMessageScreenHide;
			Characters.Ori.StartTwinkle();
		}
		else if (this.FreezeGame)
		{
			SuspensionManager.ResumeAll();
		}
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x000669F0 File Offset: 0x00064BF0
	public void OnMessageScreenHide()
	{
		if (this.FreezeGame)
		{
			SuspensionManager.ResumeAll();
		}
		if (this.m_messageBox)
		{
			this.m_messageBox.OnMessageScreenHide -= this.OnMessageScreenHide;
		}
		Characters.Ori.StopTwinkle();
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x00066A3E File Offset: 0x00064C3E
	public override void Stop()
	{
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x060017EB RID: 6123 RVA: 0x00066A40 File Offset: 0x00064C40
	public override bool IsPerforming
	{
		get
		{
			return this.m_messageBox;
		}
	}

	// Token: 0x040014A2 RID: 5282
	[NotNull]
	public MessageProvider StoryMessage;

	// Token: 0x040014A3 RID: 5283
	private MessageBox m_messageBox;

	// Token: 0x040014A4 RID: 5284
	public bool FreezeGame;
}
