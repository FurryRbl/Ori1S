using System;
using Core;
using UnityEngine;

// Token: 0x0200067C RID: 1660
public class InteractiveMessageBox : MonoBehaviour, ISuspendable
{
	// Token: 0x06002855 RID: 10325 RVA: 0x000AEE02 File Offset: 0x000AD002
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06002856 RID: 10326 RVA: 0x000AEE0A File Offset: 0x000AD00A
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x1700066C RID: 1644
	// (get) Token: 0x06002857 RID: 10327 RVA: 0x000AEE12 File Offset: 0x000AD012
	public bool OnButtonPressed
	{
		get
		{
			return (Core.Input.SpiritFlame.OnPressed && !Core.Input.SpiritFlame.Used) || Core.Input.Jump.OnPressed;
		}
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x000AEE40 File Offset: 0x000AD040
	public void UpdateWriting()
	{
		if (this.MessageBox.FinishedWriting || this.OnButtonPressed)
		{
			Core.Input.SpiritFlame.Used = true;
			Core.Input.Jump.Used = true;
			this.MessageBox.FinishWriting();
			if (this.MessageFinishedSound)
			{
				this.MessageFinishedSound.Play();
			}
			if (this.ButtonAnimator)
			{
				this.ButtonAnimator.AnimatorDriver.RestartForward();
			}
			this.ChangeState(InteractiveMessageBox.State.Completed);
		}
		if (this.CanCancel && Core.Input.Cancel.OnPressedNotUsed)
		{
			Core.Input.Cancel.Used = true;
			this.EndMessageBox();
		}
	}

	// Token: 0x06002859 RID: 10329 RVA: 0x000AEEF8 File Offset: 0x000AD0F8
	public void EndMessageBox()
	{
		if (this.CloseMessageBoxSound)
		{
			this.CloseMessageBoxSound.Play();
		}
		this.MessageBox.HideMessageScreen();
		GameController.Instance.LockInput = false;
		this.ChangeState(InteractiveMessageBox.State.Inactive);
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x000AEF40 File Offset: 0x000AD140
	public void UpdateComplete()
	{
		if (this.OnButtonPressed && this.m_remainingWaitTime <= 0f)
		{
			Core.Input.SpiritFlame.Used = true;
			Core.Input.Jump.Used = true;
			if (this.ButtonAnimator)
			{
				this.ButtonAnimator.AnimatorDriver.RestartBackwards();
			}
			if (this.MessageBox.IsLastMessage)
			{
				this.EndMessageBox();
			}
			else
			{
				this.MessageBox.NextMessage();
				if (this.NextMessageSound)
				{
					this.NextMessageSound.Play();
				}
				this.ChangeState(InteractiveMessageBox.State.Writing);
			}
		}
		if (this.CanCancel && Core.Input.Cancel.OnPressedNotUsed)
		{
			Core.Input.Cancel.Used = true;
			this.EndMessageBox();
		}
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x000AF011 File Offset: 0x000AD211
	public void Start()
	{
		this.ChangeState(InteractiveMessageBox.State.Writing);
		this.m_remainingWaitTime = this.WaitTimeFirstMessage;
		GameController.Instance.LockInput = true;
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x000AF031 File Offset: 0x000AD231
	public void ChangeState(InteractiveMessageBox.State state)
	{
		this.m_state = state;
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x000AF03C File Offset: 0x000AD23C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		InteractiveMessageBox.State state = this.m_state;
		if (state != InteractiveMessageBox.State.Writing)
		{
			if (state == InteractiveMessageBox.State.Completed)
			{
				this.UpdateComplete();
			}
		}
		else
		{
			this.UpdateWriting();
		}
		this.m_remainingWaitTime -= Time.deltaTime;
	}

	// Token: 0x1700066D RID: 1645
	// (get) Token: 0x0600285E RID: 10334 RVA: 0x000AF096 File Offset: 0x000AD296
	// (set) Token: 0x0600285F RID: 10335 RVA: 0x000AF09E File Offset: 0x000AD29E
	public bool IsSuspended { get; set; }

	// Token: 0x040023D6 RID: 9174
	public MessageBox MessageBox;

	// Token: 0x040023D7 RID: 9175
	private InteractiveMessageBox.State m_state;

	// Token: 0x040023D8 RID: 9176
	public GameObject Button;

	// Token: 0x040023D9 RID: 9177
	public BaseAnimator ButtonAnimator;

	// Token: 0x040023DA RID: 9178
	public float WaitTimeFirstMessage = 0.1f;

	// Token: 0x040023DB RID: 9179
	private float m_remainingWaitTime;

	// Token: 0x040023DC RID: 9180
	public SoundSource MessageFinishedSound;

	// Token: 0x040023DD RID: 9181
	public SoundSource NextMessageSound;

	// Token: 0x040023DE RID: 9182
	public SoundSource CloseMessageBoxSound;

	// Token: 0x040023DF RID: 9183
	public bool CanCancel;

	// Token: 0x0200067D RID: 1661
	public enum State
	{
		// Token: 0x040023E2 RID: 9186
		Writing,
		// Token: 0x040023E3 RID: 9187
		Completed,
		// Token: 0x040023E4 RID: 9188
		Inactive
	}
}
