using System;
using Game;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class ShowTextBoxes : PerformingAction
{
	// Token: 0x060014CF RID: 5327 RVA: 0x0005D9CC File Offset: 0x0005BBCC
	public override void Perform(IContext context)
	{
		if (this.textSetupGameObject != null)
		{
			Vector3 position = this.Position;
			if (this.PositionAtPlayer)
			{
				position = UI.Cameras.Current.Target.position;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(this.textSetupGameObject, position, base.transform.rotation) as GameObject;
			this.Message = gameObject.GetComponentInChildren<TextBoxMessage>();
		}
		this.Message.transform.position += this.MessageBoxOffset;
		this.Message.Initialize();
		foreach (string text in this.Strings)
		{
			this.Message.AddLine(text);
		}
		this.Message.ReadNextLine();
		this.Message.StartWriting();
		this.Message.OnCompleteEvent += this.OnComplete;
		if (this.PauseSequence)
		{
			this.PauseSequence.Pause++;
		}
		if (this.Voice == ShowTextBoxes.VoiceType.Ori)
		{
			Characters.Ori.StartTwinkle();
		}
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x0005DAF8 File Offset: 0x0005BCF8
	public void OnComplete()
	{
		if (this.PauseSequence)
		{
			this.PauseSequence.Pause--;
		}
		this.Message.OnCompleteEvent -= this.OnComplete;
		if (this.Voice == ShowTextBoxes.VoiceType.Ori)
		{
			Characters.Ori.StopTwinkle();
		}
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x0005DB54 File Offset: 0x0005BD54
	public override void Stop()
	{
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0005DB56 File Offset: 0x0005BD56
	public override bool IsPerforming
	{
		get
		{
			return !this.Message.IsInactive;
		}
	}

	// Token: 0x0400120F RID: 4623
	public TextBoxMessage Message;

	// Token: 0x04001210 RID: 4624
	public Vector3 MessageBoxOffset;

	// Token: 0x04001211 RID: 4625
	public GameObject textSetupGameObject;

	// Token: 0x04001212 RID: 4626
	public Vector3 Position;

	// Token: 0x04001213 RID: 4627
	public bool PositionAtPlayer;

	// Token: 0x04001214 RID: 4628
	public string[] Strings;

	// Token: 0x04001215 RID: 4629
	public TimedActionSequence PauseSequence;

	// Token: 0x04001216 RID: 4630
	public ShowTextBoxes.VoiceType Voice;

	// Token: 0x0200026F RID: 623
	public enum VoiceType
	{
		// Token: 0x04001218 RID: 4632
		Ori,
		// Token: 0x04001219 RID: 4633
		SpiritTree
	}
}
