using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class JumpFlipPlatform : SaveSerialize, ISuspendable
{
	// Token: 0x06001E07 RID: 7687 RVA: 0x00084201 File Offset: 0x00082401
	public void OnValidate()
	{
		this.m_flipPlants = base.GetComponentsInChildren<FlipPlantLogic>();
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x00084210 File Offset: 0x00082410
	public new void Awake()
	{
		base.Awake();
		JumpFlipPlatform.OnSeinJumpEvent = (Action)Delegate.Combine(JumpFlipPlatform.OnSeinJumpEvent, new Action(this.OnPlayerJump));
		JumpFlipPlatform.OnSeinDoubleJumpEvent = (Action)Delegate.Combine(JumpFlipPlatform.OnSeinDoubleJumpEvent, new Action(this.OnPlayerDoubleJump));
		JumpFlipPlatform.OnSeinWallJumpEvent = (Action)Delegate.Combine(JumpFlipPlatform.OnSeinWallJumpEvent, new Action(this.OnPlayerWallJump));
		JumpFlipPlatform.OnSeinChargeJumpEvent = (Action)Delegate.Combine(JumpFlipPlatform.OnSeinChargeJumpEvent, new Action(this.OnPlayerChargeJump));
		this.m_transparancyAnimator = base.GetComponent<LegacyTransparancyAnimator>();
		this.m_collider = base.GetComponent<Collider>();
		this.m_active = this.ShowAtStart;
		SuspensionManager.Register(this);
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x000842D0 File Offset: 0x000824D0
	public new void OnDestroy()
	{
		base.OnDestroy();
		JumpFlipPlatform.OnSeinJumpEvent = (Action)Delegate.Remove(JumpFlipPlatform.OnSeinJumpEvent, new Action(this.OnPlayerJump));
		JumpFlipPlatform.OnSeinDoubleJumpEvent = (Action)Delegate.Remove(JumpFlipPlatform.OnSeinDoubleJumpEvent, new Action(this.OnPlayerDoubleJump));
		JumpFlipPlatform.OnSeinWallJumpEvent = (Action)Delegate.Remove(JumpFlipPlatform.OnSeinWallJumpEvent, new Action(this.OnPlayerWallJump));
		JumpFlipPlatform.OnSeinChargeJumpEvent = (Action)Delegate.Remove(JumpFlipPlatform.OnSeinChargeJumpEvent, new Action(this.OnPlayerChargeJump));
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x0008436C File Offset: 0x0008256C
	public void FixedUpdate()
	{
		if (this.m_transparancyAnimator)
		{
			if ((double)this.m_transparancyAnimator.ValueInCurrentFrame() < 0.5)
			{
				if (this.m_collider.enabled)
				{
					this.m_collider.enabled = false;
				}
			}
			else if (!this.m_collider.enabled)
			{
				this.m_collider.enabled = true;
			}
		}
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x000843E0 File Offset: 0x000825E0
	public void Start()
	{
		if (this.m_transparancyAnimator)
		{
			this.m_transparancyAnimator.DeactivateWhenInvisible = false;
		}
		this.UpdateState();
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x0008440F File Offset: 0x0008260F
	public void Toggle()
	{
		this.m_active = !this.m_active;
		this.UpdateState();
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x00084428 File Offset: 0x00082628
	public void UpdateState()
	{
		if (this.m_active)
		{
			foreach (FlipPlantLogic flipPlantLogic in this.m_flipPlants)
			{
				flipPlantLogic.GoUp();
			}
			if (this.OnActivateAction)
			{
				this.OnActivateAction.Perform(null);
			}
			if (this.OnActivateSoundProvider)
			{
				Sound.Play(this.OnActivateSoundProvider.GetSound(null), base.transform.position, null);
			}
			if (this.m_transparancyAnimator)
			{
				this.m_transparancyAnimator.ContinueForward();
			}
			foreach (LegacyAnimator legacyAnimator in this.Animators)
			{
				if (legacyAnimator)
				{
					legacyAnimator.ContinueForward();
				}
			}
			foreach (BaseAnimator baseAnimator in this.BaseAnimators)
			{
				if (baseAnimator)
				{
					baseAnimator.AnimatorDriver.ContinueForward();
				}
			}
		}
		else
		{
			foreach (FlipPlantLogic flipPlantLogic2 in this.m_flipPlants)
			{
				flipPlantLogic2.GoDown();
			}
			if (this.OnDeactivateAction)
			{
				this.OnDeactivateAction.Perform(null);
			}
			if (this.OnDeactivateSoundProvider)
			{
				Sound.Play(this.OnDeactivateSoundProvider.GetSound(null), base.transform.position, null);
			}
			if (this.m_transparancyAnimator)
			{
				this.m_transparancyAnimator.ContinueBackward();
			}
			foreach (LegacyAnimator legacyAnimator2 in this.Animators)
			{
				if (legacyAnimator2)
				{
					legacyAnimator2.ContinueBackward();
				}
			}
			foreach (BaseAnimator baseAnimator2 in this.BaseAnimators)
			{
				if (baseAnimator2)
				{
					baseAnimator2.AnimatorDriver.ContinueBackwards();
				}
			}
		}
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x000846D0 File Offset: 0x000828D0
	public void OnPlayerWallJump()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.ToggleOnWallJump)
		{
			this.Toggle();
		}
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x00084700 File Offset: 0x00082900
	public void OnPlayerDoubleJump()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.ToggleOnDoubleJump)
		{
			this.Toggle();
		}
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x00084730 File Offset: 0x00082930
	public void OnPlayerJump()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.ToggleOnJump)
		{
			this.Toggle();
		}
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x00084760 File Offset: 0x00082960
	public void OnPlayerChargeJump()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.ToggleOnChargeJump)
		{
			this.Toggle();
		}
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x0008477F File Offset: 0x0008297F
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_active);
		if (ar.Reading)
		{
			this.UpdateState();
		}
	}

	// Token: 0x1700050F RID: 1295
	// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0008479E File Offset: 0x0008299E
	// (set) Token: 0x06001E14 RID: 7700 RVA: 0x000847A6 File Offset: 0x000829A6
	public bool IsSuspended { get; set; }

	// Token: 0x040019D1 RID: 6609
	public static Action OnSeinJumpEvent = delegate()
	{
	};

	// Token: 0x040019D2 RID: 6610
	public static Action OnSeinDoubleJumpEvent = delegate()
	{
	};

	// Token: 0x040019D3 RID: 6611
	public static Action OnSeinWallJumpEvent = delegate()
	{
	};

	// Token: 0x040019D4 RID: 6612
	public static Action OnSeinChargeJumpEvent = delegate()
	{
	};

	// Token: 0x040019D5 RID: 6613
	public bool ShowAtStart;

	// Token: 0x040019D6 RID: 6614
	public bool ToggleOnJump = true;

	// Token: 0x040019D7 RID: 6615
	public bool ToggleOnDoubleJump;

	// Token: 0x040019D8 RID: 6616
	public bool ToggleOnWallJump;

	// Token: 0x040019D9 RID: 6617
	public bool ToggleOnChargeJump = true;

	// Token: 0x040019DA RID: 6618
	private bool m_active;

	// Token: 0x040019DB RID: 6619
	private LegacyTransparancyAnimator m_transparancyAnimator;

	// Token: 0x040019DC RID: 6620
	public List<LegacyAnimator> Animators = new List<LegacyAnimator>();

	// Token: 0x040019DD RID: 6621
	public List<BaseAnimator> BaseAnimators = new List<BaseAnimator>();

	// Token: 0x040019DE RID: 6622
	private Collider m_collider;

	// Token: 0x040019DF RID: 6623
	public static float TimeOfLastAudio;

	// Token: 0x040019E0 RID: 6624
	public ActionMethod OnActivateAction;

	// Token: 0x040019E1 RID: 6625
	public ActionMethod OnDeactivateAction;

	// Token: 0x040019E2 RID: 6626
	public SoundProvider OnActivateSoundProvider;

	// Token: 0x040019E3 RID: 6627
	public SoundProvider OnDeactivateSoundProvider;

	// Token: 0x040019E4 RID: 6628
	[SerializeField]
	private FlipPlantLogic[] m_flipPlants;
}
