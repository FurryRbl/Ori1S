using System;
using CatlikeCoding.TextBox;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class ShowSpiritTreeTextAction : ActionMethod
{
	// Token: 0x060017E2 RID: 6114 RVA: 0x0006661C File Offset: 0x0006481C
	public override void Perform(IContext context)
	{
		Vector3 position = (!this.Target) ? new Vector3(0f, 0f, 0f) : this.Target.position;
		GameObject gameObject = UI.MessageController.ShowSpiritTreeTextMessage(this.Message, position);
		if (this.Voice)
		{
			SoundPlayer soundPlayer = Sound.Play(this.Voice.GetSound(null), position, null);
			if (soundPlayer)
			{
				soundPlayer.DestroyOnRestart = true;
				soundPlayer.PauseOnSuspend = true;
			}
		}
		if (!gameObject)
		{
			return;
		}
		gameObject.transform.localScale *= this.Scale;
		if (this.ShowAsUi)
		{
			TextBox componentInChildren = gameObject.GetComponentInChildren<TextBox>();
			if (componentInChildren)
			{
				componentInChildren.width = 10f / this.Scale;
			}
		}
		float num = this.Duration / 5f * 4f;
		num *= 1.2f;
		LimitedLifetime component = gameObject.GetComponent<LimitedLifetime>();
		component.SetRemainingLifetime(num * 1.2f);
		BaseAnimator[] componentsInChildren = gameObject.GetComponentsInChildren<BaseAnimator>();
		foreach (BaseAnimator baseAnimator in componentsInChildren)
		{
			baseAnimator.Speed = 1f / (num / 4f);
		}
		SpiritTreeTextLocationController componentInChildren2 = gameObject.GetComponentInChildren<SpiritTreeTextLocationController>();
		if (componentInChildren2)
		{
			componentInChildren2.ScreenPosition = this.ScreenPosition;
			componentInChildren2.ScreenWeight = this.ScreenWeight;
		}
		if (this.ShowAsUi)
		{
			if (componentInChildren2)
			{
				componentInChildren2.enabled = false;
				componentInChildren2.ScreenWeight = 0f;
			}
			gameObject.gameObject.layer = LayerMask.NameToLayer("ui");
			foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>())
			{
				transform.gameObject.layer = LayerMask.NameToLayer("ui");
			}
		}
		else
		{
			componentInChildren2.StartScrolling();
		}
	}

	// Token: 0x04001496 RID: 5270
	public Transform Target;

	// Token: 0x04001497 RID: 5271
	public MessageProvider Message;

	// Token: 0x04001498 RID: 5272
	public SoundProvider Voice;

	// Token: 0x04001499 RID: 5273
	public float Duration = 5f;

	// Token: 0x0400149A RID: 5274
	public float Scale = 1f;

	// Token: 0x0400149B RID: 5275
	public bool ShowAsUi;

	// Token: 0x0400149C RID: 5276
	public Vector2 ScreenPosition = new Vector2(0.5f, 0.5f);

	// Token: 0x0400149D RID: 5277
	public float ScreenWeight;
}
