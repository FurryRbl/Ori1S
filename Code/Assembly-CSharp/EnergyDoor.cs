using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
public class EnergyDoor : SaveSerialize
{
	// Token: 0x060032BB RID: 12987 RVA: 0x000D6598 File Offset: 0x000D4798
	public void OnValidate()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x060032BC RID: 12988 RVA: 0x000D65A6 File Offset: 0x000D47A6
	public override void Awake()
	{
		base.Awake();
	}

	// Token: 0x060032BD RID: 12989 RVA: 0x000D65B0 File Offset: 0x000D47B0
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
			Characters.Sein.Abilities.SpiritFlame.AddLock("energyDoor");
		}
		Characters.Ori.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Characters.Ori.EnableHoverWobbling = false;
		if (this.m_hint == null)
		{
			this.m_hint = UI.Hints.Show(this.HintMessage, HintLayer.HintZone, 3f);
		}
		if (this.OnOriEnterSoundProvider)
		{
			Sound.Play(this.OnOriEnterSoundProvider.GetSound(null), this.m_transform.position, null);
		}
	}

	// Token: 0x060032BE RID: 12990 RVA: 0x000D66B0 File Offset: 0x000D48B0
	public void Unhighlight()
	{
		Characters.Ori.ChangeState(Ori.State.Hovering);
		Characters.Ori.EnableHoverWobbling = true;
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.RemoveLock("energyDoor");
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

	// Token: 0x060032BF RID: 12991 RVA: 0x000D6750 File Offset: 0x000D4950
	public void RestoreOrbs()
	{
		if (this.AmountOfEnergyUsed > 0 && this.RestoreSoundProvider)
		{
			Sound.Play(this.RestoreSoundProvider.GetSound(null), this.m_transform.position, null);
		}
		if (Characters.Sein)
		{
			Characters.Sein.Energy.Gain((float)this.AmountOfEnergyUsed);
		}
		this.AmountOfEnergyUsed = 0;
	}

	// Token: 0x060032C0 RID: 12992 RVA: 0x000D67C3 File Offset: 0x000D49C3
	public void OnDisable()
	{
		if (this.CurrentState == EnergyDoor.State.Highlighted)
		{
			this.RestoreOrbs();
			this.Unhighlight();
		}
	}

	// Token: 0x060032C1 RID: 12993 RVA: 0x000D67E0 File Offset: 0x000D49E0
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_slotsPending);
		ar.Serialize(ref this.AmountOfEnergyUsed);
		ar.Serialize(ref this.m_slotsFilled);
		if (ar.Reading && this.CurrentState == EnergyDoor.State.Highlighted)
		{
			this.Unhighlight();
			this.CurrentState = EnergyDoor.State.Normal;
		}
		this.CurrentState = (EnergyDoor.State)ar.Serialize((int)this.CurrentState);
		if (ar.Reading && this.CurrentState == EnergyDoor.State.Highlighted)
		{
			this.RestoreOrbs();
			this.CurrentState = EnergyDoor.State.Normal;
		}
	}

	// Token: 0x17000810 RID: 2064
	// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000D686B File Offset: 0x000D4A6B
	public float DistanceToSein
	{
		get
		{
			return Vector3.Distance(this.m_transform.position, Characters.Sein.Position);
		}
	}

	// Token: 0x17000811 RID: 2065
	// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000D6888 File Offset: 0x000D4A88
	public bool OriHasTargets
	{
		get
		{
			SeinSpiritFlameTargetting spiritFlameTargetting = Characters.Sein.Abilities.SpiritFlameTargetting;
			return spiritFlameTargetting && spiritFlameTargetting.ClosestAttackables.Count > 0;
		}
	}

	// Token: 0x17000812 RID: 2066
	// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000D68C0 File Offset: 0x000D4AC0
	public bool SeinInRange
	{
		get
		{
			return !this.OriHasTargets && this.DistanceToSein <= this.Radius;
		}
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x000D68E3 File Offset: 0x000D4AE3
	public void RegisterSlot(EnergyDoorSlot slot)
	{
		this.m_slots.Add(slot);
	}

	// Token: 0x060032C6 RID: 12998 RVA: 0x000D68F4 File Offset: 0x000D4AF4
	public void UpdateSlots()
	{
		foreach (EnergyDoorSlot energyDoorSlot in this.m_slots)
		{
			energyDoorSlot.Refresh();
		}
	}

	// Token: 0x060032C7 RID: 12999 RVA: 0x000D6950 File Offset: 0x000D4B50
	public void FixedUpdate()
	{
		if (!Characters.Sein)
		{
			return;
		}
		EnergyDoor.State currentState = this.CurrentState;
		if (currentState != EnergyDoor.State.Normal)
		{
			if (currentState == EnergyDoor.State.Highlighted)
			{
				if (!this.SeinInRange)
				{
					this.RestoreOrbs();
					this.Unhighlight();
					this.CurrentState = EnergyDoor.State.Normal;
				}
				if (!Characters.Sein.Controller.CanMove)
				{
					this.RestoreOrbs();
					this.Unhighlight();
					this.CurrentState = EnergyDoor.State.Normal;
					return;
				}
				if (Characters.Sein.Controller.CanMove && !Characters.Sein.IsSuspended && Core.Input.SpiritFlame.OnPressed)
				{
					if (Characters.Sein.Energy.Current < 1f && this.AmountOfEnergyRequired > this.AmountOfEnergyUsed)
					{
						this.OnFailAction.Perform(null);
						Characters.Sein.Energy.NotifyOutOfEnergy();
					}
					if (Characters.Sein.Energy.Current >= 1f && this.AmountOfEnergyUsed < this.AmountOfEnergyRequired)
					{
						this.AmountOfEnergyUsed++;
						Characters.Sein.Energy.Spend(1f);
						this.UpdateSlots();
						if (this.PlaceSlotSoundProvider)
						{
							Sound.Play(this.PlaceSlotSoundProvider.GetSound(null), this.m_transform.position, null);
						}
					}
					if (this.AmountOfEnergyUsed == this.AmountOfEnergyRequired)
					{
						this.OnOpenedAction.Perform(null);
						this.Unhighlight();
						this.CurrentState = EnergyDoor.State.Opened;
						if (this.ActivateSoundProvider)
						{
							Sound.Play(this.ActivateSoundProvider.GetSound(null), this.m_transform.position, null);
						}
					}
				}
			}
		}
		else if (this.SeinInRange && !this.OriHasTargets && Characters.Sein.Controller.CanMove)
		{
			this.Highlight();
			this.CurrentState = EnergyDoor.State.Highlighted;
		}
	}

	// Token: 0x04002DAE RID: 11694
	public Transform OriTarget;

	// Token: 0x04002DAF RID: 11695
	[HideInInspector]
	[SerializeField]
	private Transform m_transform;

	// Token: 0x04002DB0 RID: 11696
	private int m_slotsPending;

	// Token: 0x04002DB1 RID: 11697
	private int m_slotsFilled;

	// Token: 0x04002DB2 RID: 11698
	public ActionMethod OnOpenedAction;

	// Token: 0x04002DB3 RID: 11699
	public ActionMethod OnFailAction;

	// Token: 0x04002DB4 RID: 11700
	public int AmountOfEnergyRequired;

	// Token: 0x04002DB5 RID: 11701
	public int AmountOfEnergyUsed;

	// Token: 0x04002DB6 RID: 11702
	public SoundProvider PlaceSlotSoundProvider;

	// Token: 0x04002DB7 RID: 11703
	public SoundProvider ActivateSoundProvider;

	// Token: 0x04002DB8 RID: 11704
	public SoundProvider RestoreSoundProvider;

	// Token: 0x04002DB9 RID: 11705
	public SoundProvider OnOriEnterSoundProvider;

	// Token: 0x04002DBA RID: 11706
	public SoundProvider OnOriExitSoundProvider;

	// Token: 0x04002DBB RID: 11707
	public float OriDuration = 1f;

	// Token: 0x04002DBC RID: 11708
	public float Radius = 10f;

	// Token: 0x04002DBD RID: 11709
	public Texture2D HintTexture;

	// Token: 0x04002DBE RID: 11710
	public MessageProvider HintMessage;

	// Token: 0x04002DBF RID: 11711
	private MessageBox m_hint;

	// Token: 0x04002DC0 RID: 11712
	public EnergyDoor.State CurrentState;

	// Token: 0x04002DC1 RID: 11713
	private List<EnergyDoorSlot> m_slots = new List<EnergyDoorSlot>();

	// Token: 0x020008E3 RID: 2275
	public enum State
	{
		// Token: 0x04002DC3 RID: 11715
		Normal,
		// Token: 0x04002DC4 RID: 11716
		Highlighted,
		// Token: 0x04002DC5 RID: 11717
		Opened
	}
}
