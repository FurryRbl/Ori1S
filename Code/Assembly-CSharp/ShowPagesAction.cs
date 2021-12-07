using System;
using Game;
using UnityEngine;

// Token: 0x0200033E RID: 830
[Category("Hints")]
internal class ShowPagesAction : PerformingAction
{
	// Token: 0x060017DA RID: 6106 RVA: 0x00066434 File Offset: 0x00064634
	public override void Perform(IContext context)
	{
		if (this.Condition && !this.Condition.Validate(context))
		{
			return;
		}
		GameController.Instance.LockInput = true;
		ShowPagesAction.MessageType type = this.Type;
		if (type != ShowPagesAction.MessageType.Ability)
		{
			if (type == ShowPagesAction.MessageType.Pickup)
			{
				this.m_message = UI.MessageController.ShowPickupMessage(this.Message, this.Avatar);
			}
		}
		else
		{
			this.m_message = UI.MessageController.ShowAbilityMessage(this.Message, this.Avatar);
		}
		if (this.m_message)
		{
			this.m_message.OnMessageScreenHide += this.OnFinishedReading;
			if (this.FreezeTime)
			{
				SuspensionManager.SuspendExcluding(this.m_message.GetSuspendables());
			}
		}
		else
		{
			this.OnFinishedReading();
		}
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x00066518 File Offset: 0x00064718
	public void OnFinishedReading()
	{
		if (this.m_message)
		{
			this.m_message.OnMessageScreenHide -= this.OnFinishedReading;
		}
		this.m_message = null;
		GameController.Instance.LockInput = false;
		SuspensionManager.ResumeAll();
		if (this.ActionOnFinishedReading)
		{
			this.ActionOnFinishedReading.Perform(null);
		}
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x0006657F File Offset: 0x0006477F
	public override void Stop()
	{
		GameController.Instance.LockInput = false;
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x060017DD RID: 6109 RVA: 0x0006658C File Offset: 0x0006478C
	public override bool IsPerforming
	{
		get
		{
			return this.m_message != null;
		}
	}

	// Token: 0x0400148A RID: 5258
	public ActionMethod ActionOnFinishedReading;

	// Token: 0x0400148B RID: 5259
	public bool FreezeTime = true;

	// Token: 0x0400148C RID: 5260
	public Texture2D[] Pages;

	// Token: 0x0400148D RID: 5261
	public Condition Condition;

	// Token: 0x0400148E RID: 5262
	public MessageProvider Message;

	// Token: 0x0400148F RID: 5263
	public GameObject Avatar;

	// Token: 0x04001490 RID: 5264
	public ShowPagesAction.MessageType Type;

	// Token: 0x04001491 RID: 5265
	private MessageBox m_message;

	// Token: 0x0200033F RID: 831
	public enum MessageType
	{
		// Token: 0x04001493 RID: 5267
		Ability,
		// Token: 0x04001494 RID: 5268
		Pickup
	}
}
