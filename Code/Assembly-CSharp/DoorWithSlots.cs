using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020008E0 RID: 2272
public class DoorWithSlots : SaveSerialize
{
	// Token: 0x060032AD RID: 12973 RVA: 0x000D5E29 File Offset: 0x000D4029
	public void OnValidate()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x060032AE RID: 12974 RVA: 0x000D5E38 File Offset: 0x000D4038
	public override void Awake()
	{
		base.Awake();
		this.m_opensOnLeftSide = (this.m_transform.TransformPoint(Vector3.right).x < this.m_transform.position.x);
	}

	// Token: 0x060032AF RID: 12975 RVA: 0x000D5E80 File Offset: 0x000D4080
	public void Highlight()
	{
		if (this.OriTarget)
		{
			Characters.Ori.MoveOriToPosition(this.OriTarget.position, this.OriDuration);
		}
		else
		{
			Characters.Ori.MoveOriToPosition(this.m_transform.position, this.OriDuration);
		}
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.AddLock("doorWithSlots");
		}
		Characters.Ori.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Characters.Ori.EnableHoverWobbling = false;
		Characters.Ori.InsideDoor = true;
		if (this.m_hint == null)
		{
			this.m_hint = UI.Hints.Show(this.HintMessage, HintLayer.HintZone, 600f);
		}
		if (this.OnOriEnterSoundProvider)
		{
			Sound.Play(this.OnOriEnterSoundProvider.GetSound(null), this.m_transform.position, null);
		}
	}

	// Token: 0x060032B0 RID: 12976 RVA: 0x000D5F8C File Offset: 0x000D418C
	public void Unhighlight()
	{
		Characters.Ori.ChangeState(Ori.State.Hovering);
		Characters.Ori.EnableHoverWobbling = true;
		Characters.Ori.InsideDoor = false;
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.RemoveLock("doorWithSlots");
		}
		if (this.m_hint)
		{
			this.m_hint.HideMessageScreen();
		}
		if (this.OnOriExitSoundProvider)
		{
			Sound.Play(this.OnOriExitSoundProvider.GetSound(null), this.m_transform.position, null);
		}
	}

	// Token: 0x060032B1 RID: 12977 RVA: 0x000D6038 File Offset: 0x000D4238
	public void RestoreOrbs()
	{
		if (this.NumberOfOrbsUsed > 0 && this.RestoreLeafsSoundProvider)
		{
			Sound.Play(this.RestoreLeafsSoundProvider.GetSound(null), this.m_transform.position, null);
		}
		Characters.Sein.Inventory.CollectKeystones(this.NumberOfOrbsUsed);
		this.NumberOfOrbsUsed = 0;
	}

	// Token: 0x060032B2 RID: 12978 RVA: 0x000D609C File Offset: 0x000D429C
	public void OnDisable()
	{
		if (!Characters.Sein)
		{
			return;
		}
		if (this.CurrentState == DoorWithSlots.State.Highlighted)
		{
			this.RestoreOrbs();
			this.Unhighlight();
		}
	}

	// Token: 0x060032B3 RID: 12979 RVA: 0x000D60D4 File Offset: 0x000D42D4
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_slotsPending);
		ar.Serialize(ref this.NumberOfOrbsUsed);
		ar.Serialize(ref this.m_slotsFilled);
		if (ar.Reading && this.CurrentState == DoorWithSlots.State.Highlighted)
		{
			this.Unhighlight();
			this.CurrentState = DoorWithSlots.State.Normal;
		}
		this.CurrentState = (DoorWithSlots.State)ar.Serialize((int)this.CurrentState);
		if (ar.Reading && this.CurrentState == DoorWithSlots.State.Highlighted)
		{
			this.RestoreOrbs();
			this.CurrentState = DoorWithSlots.State.Normal;
		}
		if (this.m_openDoorSound)
		{
			this.m_openDoorSound.FadeOut(0.5f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_openDoorSound.gameObject);
			this.m_openDoorSound = null;
		}
		if (ar.Reading && this.CurrentState == DoorWithSlots.State.Opened)
		{
			this.m_checkItOpened = true;
		}
	}

	// Token: 0x1700080D RID: 2061
	// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000D61BA File Offset: 0x000D43BA
	public float DistanceToSein
	{
		get
		{
			return Vector3.Distance(this.m_transform.position, Characters.Sein.Position);
		}
	}

	// Token: 0x1700080E RID: 2062
	// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000D61D8 File Offset: 0x000D43D8
	public bool OriHasTargets
	{
		get
		{
			SeinSpiritFlameTargetting spiritFlameTargetting = Characters.Sein.Abilities.SpiritFlameTargetting;
			return spiritFlameTargetting && spiritFlameTargetting.ClosestAttackables.Count > 0;
		}
	}

	// Token: 0x1700080F RID: 2063
	// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000D6210 File Offset: 0x000D4410
	public bool SeinInRange
	{
		get
		{
			return !this.OriHasTargets && this.DistanceToSein <= this.Radius && (!this.m_opensOnLeftSide || this.m_transform.position.x >= Characters.Sein.Position.x) && (this.m_opensOnLeftSide || this.m_transform.position.x <= Characters.Sein.Position.x);
		}
	}

	// Token: 0x060032B7 RID: 12983 RVA: 0x000D62AC File Offset: 0x000D44AC
	public void FixedUpdate()
	{
		switch (this.CurrentState)
		{
		case DoorWithSlots.State.Normal:
			if (this.SeinInRange && !this.OriHasTargets && Characters.Sein.Controller.CanMove)
			{
				this.Highlight();
				this.CurrentState = DoorWithSlots.State.Highlighted;
			}
			break;
		case DoorWithSlots.State.Highlighted:
			if (!this.SeinInRange)
			{
				this.RestoreOrbs();
				this.Unhighlight();
				this.CurrentState = DoorWithSlots.State.Normal;
			}
			if (!Characters.Sein.Controller.CanMove)
			{
				this.RestoreOrbs();
				this.Unhighlight();
				this.CurrentState = DoorWithSlots.State.Normal;
				return;
			}
			if (Characters.Sein.Controller.CanMove && !Characters.Sein.IsSuspended && Core.Input.SpiritFlame.OnPressed)
			{
				if (Characters.Sein.Inventory.Keystones == 0 && this.NumberOfOrbsRequired > this.NumberOfOrbsUsed)
				{
					this.OnFailAction.Perform(null);
					UI.SeinUI.ShakeKeystones();
					if (this.NotEnoughLeafsSoundProvider)
					{
						Sound.Play(this.NotEnoughLeafsSoundProvider.GetSound(null), this.m_transform.position, null);
					}
				}
				if (Characters.Sein.Inventory.Keystones > 0 && this.NumberOfOrbsUsed < this.NumberOfOrbsRequired)
				{
					this.NumberOfOrbsUsed++;
					Characters.Sein.Inventory.SpendKeystones(1);
					if (this.PlaceLeafSoundSoundProvider)
					{
						Sound.Play(this.PlaceLeafSoundSoundProvider.GetSound(null), this.m_transform.position, null);
					}
				}
				if (this.NumberOfOrbsUsed == this.NumberOfOrbsRequired)
				{
					this.OnOpenedAction.Perform(null);
					this.Unhighlight();
					this.CurrentState = DoorWithSlots.State.Opened;
					if (this.OpenDoorSoundProvider)
					{
						this.m_openDoorSound = Sound.Play(this.OpenDoorSoundProvider.GetSound(null), this.m_transform.position, delegate()
						{
							this.m_openDoorSound = null;
						});
						this.m_openDoorSound.PauseOnSuspend = true;
					}
				}
			}
			break;
		case DoorWithSlots.State.Opened:
			if (this.m_checkItOpened)
			{
				this.m_checkItOpened = false;
				this.MakeSureItsAtEnd(base.transform.FindChild("doorPieces/doorLeft"));
				this.MakeSureItsAtEnd(base.transform.FindChild("doorPieces/doorRight"));
			}
			break;
		}
	}

	// Token: 0x060032B8 RID: 12984 RVA: 0x000D6524 File Offset: 0x000D4724
	private void MakeSureItsAtEnd(Transform c)
	{
		if (c == null)
		{
			return;
		}
		LegacyTranslateAnimator component = c.GetComponent<LegacyTranslateAnimator>();
		if (component.CurrentTime <= 0f && component.Stopped)
		{
			component.StopAndSampleAtEnd();
		}
	}

	// Token: 0x04002D91 RID: 11665
	public Transform OriTarget;

	// Token: 0x04002D92 RID: 11666
	public Color OriHoverColor;

	// Token: 0x04002D93 RID: 11667
	[HideInInspector]
	[SerializeField]
	private Transform m_transform;

	// Token: 0x04002D94 RID: 11668
	private int m_slotsPending;

	// Token: 0x04002D95 RID: 11669
	private int m_slotsFilled;

	// Token: 0x04002D96 RID: 11670
	public ActionMethod OnOpenedAction;

	// Token: 0x04002D97 RID: 11671
	public ActionMethod OnFailAction;

	// Token: 0x04002D98 RID: 11672
	public int NumberOfOrbsRequired;

	// Token: 0x04002D99 RID: 11673
	public int NumberOfOrbsUsed;

	// Token: 0x04002D9A RID: 11674
	public SoundProvider PlaceLeafSoundSoundProvider;

	// Token: 0x04002D9B RID: 11675
	public SoundProvider NotEnoughLeafsSoundProvider;

	// Token: 0x04002D9C RID: 11676
	public SoundProvider OpenDoorSoundProvider;

	// Token: 0x04002D9D RID: 11677
	public SoundProvider RestoreLeafsSoundProvider;

	// Token: 0x04002D9E RID: 11678
	public SoundProvider OnOriEnterSoundProvider;

	// Token: 0x04002D9F RID: 11679
	public SoundProvider OnOriExitSoundProvider;

	// Token: 0x04002DA0 RID: 11680
	public float OriDuration = 1f;

	// Token: 0x04002DA1 RID: 11681
	public float Radius = 10f;

	// Token: 0x04002DA2 RID: 11682
	public MessageProvider HintMessage;

	// Token: 0x04002DA3 RID: 11683
	public CameraShakeAsset DoorKeyInsertShake;

	// Token: 0x04002DA4 RID: 11684
	public ControllerShakeAsset DoorKeyInsertControllerShake;

	// Token: 0x04002DA5 RID: 11685
	private MessageBox m_hint;

	// Token: 0x04002DA6 RID: 11686
	private bool m_opensOnLeftSide;

	// Token: 0x04002DA7 RID: 11687
	public DoorWithSlots.State CurrentState;

	// Token: 0x04002DA8 RID: 11688
	private bool m_checkItOpened;

	// Token: 0x04002DA9 RID: 11689
	private SoundPlayer m_openDoorSound;

	// Token: 0x020008E1 RID: 2273
	public enum State
	{
		// Token: 0x04002DAB RID: 11691
		Normal,
		// Token: 0x04002DAC RID: 11692
		Highlighted,
		// Token: 0x04002DAD RID: 11693
		Opened
	}
}
