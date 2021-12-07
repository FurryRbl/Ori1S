using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class OriInterestTriggerB : SaveSerialize
{
	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x0600186C RID: 6252 RVA: 0x00068B52 File Offset: 0x00066D52
	public bool OnButtonPressed
	{
		get
		{
			return Core.Input.SpiritFlame.OnPressed && !Core.Input.SpiritFlame.Used;
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x0600186D RID: 6253 RVA: 0x00068B73 File Offset: 0x00066D73
	public Rect Bounds
	{
		get
		{
			return Utility.RectFromBounds(Utility.BoundsFromTransform(this.m_transform));
		}
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x00068B85 File Offset: 0x00066D85
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x0600186F RID: 6255 RVA: 0x00068B9C File Offset: 0x00066D9C
	public bool SeinInRange
	{
		get
		{
			return this.Bounds.Contains(Characters.Sein.Position) && !this.OriHasTargets && (!this.Condition || this.Condition.Validate(null));
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x06001870 RID: 6256 RVA: 0x00068BFC File Offset: 0x00066DFC
	public bool OriHasTargets
	{
		get
		{
			SeinSpiritFlameTargetting spiritFlameTargetting = Characters.Sein.Abilities.SpiritFlameTargetting;
			return spiritFlameTargetting && spiritFlameTargetting.ClosestAttackables.Count > 0;
		}
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x00068C34 File Offset: 0x00066E34
	public void FixedUpdate()
	{
		if (!Characters.Sein)
		{
			return;
		}
		if (Characters.Sein.IsSuspended)
		{
			return;
		}
		switch (this.CurrentState)
		{
		case OriInterestTriggerB.State.Normal:
			if (this.SeinInRange && (!this.RunOnce || !this.m_activated))
			{
				this.Highlight();
				this.CurrentState = OriInterestTriggerB.State.Highlighted;
			}
			break;
		case OriInterestTriggerB.State.Highlighted:
			if (this.HintMessage != null && this.m_hint == null)
			{
				this.m_hint = UI.Hints.Show(this.HintMessage, HintLayer.HintZone, 3f);
			}
			if (this.m_hint)
			{
				this.m_hint.Visibility.ResetWaitDuration();
			}
			if (Characters.Sein.Controller.CanMove && this.OnButtonPressed && Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
			{
				Core.Input.SpiritFlame.Used = true;
				if (this.ActivateAction)
				{
					if (this.m_hint)
					{
						this.m_hint.HideMessageScreen();
					}
					this.ActivateAction.Perform(null);
					this.m_activated = true;
					this.CurrentState = OriInterestTriggerB.State.Activated;
				}
			}
			if (!this.SeinInRange)
			{
				this.Unhighlight();
				this.CurrentState = OriInterestTriggerB.State.Normal;
			}
			break;
		case OriInterestTriggerB.State.Activated:
			if (this.AlreadyActivatedAction && Characters.Sein.Controller.CanMove && this.OnButtonPressed && Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
			{
				this.AlreadyActivatedAction.Perform(null);
			}
			if (!this.SeinInRange)
			{
				this.Unhighlight();
				this.CurrentState = OriInterestTriggerB.State.Normal;
			}
			break;
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x00068E24 File Offset: 0x00067024
	public void Highlight()
	{
		this.m_isHighlighted = true;
		if (this.OriTarget)
		{
			Characters.Ori.MoveOriToPosition(this.OriTarget.position, this.OriDuration);
		}
		if (Characters.Ori.OnHighlightInterestZoneSound)
		{
			Sound.Play(Characters.Ori.OnHighlightInterestZoneSound.GetSound(null), Characters.Ori.transform.position, null);
		}
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.AddLock("oriInterestTrigger");
		}
		Characters.Ori.GetComponent<Rigidbody>().velocity = Vector3.zero;
		if (this.HighlightAction)
		{
			this.HighlightAction.Perform(null);
		}
		if (this.IsSlot)
		{
			Characters.Ori.EnableHoverWobbling = false;
		}
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x00068F18 File Offset: 0x00067118
	public void Unhighlight()
	{
		if (this.OriTarget)
		{
			Characters.Ori.ChangeState(Ori.State.Hovering);
		}
		if (Characters.Ori.OnUnhighlightInterestZoneSound)
		{
			Sound.Play(Characters.Ori.OnUnhighlightInterestZoneSound.GetSound(null), Characters.Ori.transform.position, null);
		}
		if (Characters.Sein && Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.RemoveLock("oriInterestTrigger");
		}
		if (this.UnhighlightAction)
		{
			this.UnhighlightAction.Perform(null);
		}
		if (this.IsSlot)
		{
			Characters.Ori.EnableHoverWobbling = true;
		}
		if (this.m_hint)
		{
			this.m_hint.HideMessageScreen();
		}
		this.m_isHighlighted = false;
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x0006900F File Offset: 0x0006720F
	public void OnDisable()
	{
		if (this.m_isHighlighted)
		{
			this.Unhighlight();
		}
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x00069022 File Offset: 0x00067222
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_activated);
	}

	// Token: 0x040014F7 RID: 5367
	public ActionMethod HighlightAction;

	// Token: 0x040014F8 RID: 5368
	public ActionMethod UnhighlightAction;

	// Token: 0x040014F9 RID: 5369
	public ActionMethod ActivateAction;

	// Token: 0x040014FA RID: 5370
	public ActionMethod AlreadyActivatedAction;

	// Token: 0x040014FB RID: 5371
	public Condition Condition;

	// Token: 0x040014FC RID: 5372
	public bool RunOnce;

	// Token: 0x040014FD RID: 5373
	public bool IsSlot;

	// Token: 0x040014FE RID: 5374
	public MessageProvider HintMessage;

	// Token: 0x040014FF RID: 5375
	public float OriDuration = 0.5f;

	// Token: 0x04001500 RID: 5376
	public Transform OriTarget;

	// Token: 0x04001501 RID: 5377
	private MessageBox m_hint;

	// Token: 0x04001502 RID: 5378
	private Transform m_transform;

	// Token: 0x04001503 RID: 5379
	private bool m_activated;

	// Token: 0x04001504 RID: 5380
	private bool m_isHighlighted;

	// Token: 0x04001505 RID: 5381
	public OriInterestTriggerB.State CurrentState;

	// Token: 0x02000373 RID: 883
	public enum State
	{
		// Token: 0x040015B9 RID: 5561
		Normal,
		// Token: 0x040015BA RID: 5562
		Highlighted,
		// Token: 0x040015BB RID: 5563
		Activated
	}
}
