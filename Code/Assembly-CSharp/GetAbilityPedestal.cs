using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x020008E7 RID: 2279
public class GetAbilityPedestal : SaveSerialize
{
	// Token: 0x17000813 RID: 2067
	// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000D6D38 File Offset: 0x000D4F38
	public bool SeinInRange
	{
		get
		{
			return !(Characters.Sein == null) && Vector3.Distance(this.m_transform.position, Characters.Sein.Position) < this.Radius;
		}
	}

	// Token: 0x060032D4 RID: 13012 RVA: 0x000D6D79 File Offset: 0x000D4F79
	private void ChangeState(GetAbilityPedestal.States state)
	{
		if (this.CurrentState == GetAbilityPedestal.States.InRange)
		{
			this.ExitInRangeState();
		}
		this.CurrentState = state;
	}

	// Token: 0x060032D5 RID: 13013 RVA: 0x000D6D94 File Offset: 0x000D4F94
	public void UpdateStates()
	{
		GetAbilityPedestal.States currentState = this.CurrentState;
		if (currentState != GetAbilityPedestal.States.OutOfRange)
		{
			if (currentState == GetAbilityPedestal.States.InRange)
			{
				this.UpdateInRangeState();
			}
		}
		else
		{
			this.UpdateOutOfRange();
		}
	}

	// Token: 0x060032D6 RID: 13014 RVA: 0x000D6DD0 File Offset: 0x000D4FD0
	private void UpdateOutOfRange()
	{
		if (this.SeinInRange)
		{
			this.ChangeState(GetAbilityPedestal.States.InRange);
		}
	}

	// Token: 0x060032D7 RID: 13015 RVA: 0x000D6DE4 File Offset: 0x000D4FE4
	private void ExitInRangeState()
	{
		if (this.m_message != null)
		{
			this.m_message.HideMessageScreen();
		}
	}

	// Token: 0x060032D8 RID: 13016 RVA: 0x000D6E04 File Offset: 0x000D5004
	public void UpdateInRangeState()
	{
		if (Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
		{
			if (this.m_message == null && !SeinUI.DebugHideUI)
			{
				this.m_message = UI.Hints.Show(this.PressUpToActivatePedestalMessage, HintLayer.Gameplay, float.PositiveInfinity);
			}
			if (!Characters.Sein.IsSuspended && Characters.Sein.Controller.CanMove && Core.Input.SpiritFlame.OnPressed)
			{
				Core.Input.SpiritFlame.Used = true;
				this.ActivatePedestal();
				return;
			}
		}
		if (!this.SeinInRange)
		{
			this.ChangeState(GetAbilityPedestal.States.OutOfRange);
		}
	}

	// Token: 0x060032D9 RID: 13017 RVA: 0x000D6EB2 File Offset: 0x000D50B2
	public void FixedUpdate()
	{
		this.UpdateStates();
	}

	// Token: 0x060032DA RID: 13018 RVA: 0x000D6EBC File Offset: 0x000D50BC
	public void ActivatePedestal()
	{
		base.StartCoroutine(this.MoveSeinToCenterSmoothly());
		if (Characters.Sein.Abilities.Carry && Characters.Sein.Abilities.Carry.CurrentCarryable != null)
		{
			Characters.Sein.Abilities.Carry.CurrentCarryable.Drop();
		}
		Characters.Sein.Mortality.Health.RestoreAllHealth();
		Characters.Sein.Energy.RestoreAllEnergy();
		Characters.Sein.Controller.PlayAnimation(this.GetAbilityAnimation);
		Characters.Sein.PlayerAbilities.SetAbility(this.Ability, true);
		this.ChangeState(GetAbilityPedestal.States.Completed);
		this.ActivatePedestalSequence.Perform(null);
	}

	// Token: 0x060032DB RID: 13019 RVA: 0x000D6F84 File Offset: 0x000D5184
	public IEnumerator MoveSeinToCenterSmoothly()
	{
		PlatformMovement seinPlatformMovement = Characters.Sein.PlatformBehaviour.PlatformMovement;
		for (int i = 0; i < 10; i++)
		{
			seinPlatformMovement.PositionX = Mathf.Lerp(seinPlatformMovement.PositionX, base.transform.position.x, 0.2f);
			yield return new WaitForFixedUpdate();
		}
		seinPlatformMovement.PositionX = base.transform.position.x;
		yield break;
	}

	// Token: 0x060032DC RID: 13020 RVA: 0x000D6FA0 File Offset: 0x000D51A0
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			int state = ar.Serialize(0);
			this.ChangeState((GetAbilityPedestal.States)state);
		}
		else
		{
			ar.Serialize((int)this.CurrentState);
		}
	}

	// Token: 0x060032DD RID: 13021 RVA: 0x000D6FD9 File Offset: 0x000D51D9
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
	}

	// Token: 0x060032DE RID: 13022 RVA: 0x000D6FED File Offset: 0x000D51ED
	public override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x04002DD0 RID: 11728
	public GetAbilityPedestal.States CurrentState;

	// Token: 0x04002DD1 RID: 11729
	public AbilityType Ability;

	// Token: 0x04002DD2 RID: 11730
	public PerformingAction ActivatePedestalSequence;

	// Token: 0x04002DD3 RID: 11731
	public float ActivationDuration = 6f;

	// Token: 0x04002DD4 RID: 11732
	public TextureAnimationWithTransitions GetAbilityAnimation;

	// Token: 0x04002DD5 RID: 11733
	public Texture2D PressUpToActivatePedestal;

	// Token: 0x04002DD6 RID: 11734
	public MessageProvider PressUpToActivatePedestalMessage;

	// Token: 0x04002DD7 RID: 11735
	private MessageBox m_message;

	// Token: 0x04002DD8 RID: 11736
	public float Radius = 1.5f;

	// Token: 0x04002DD9 RID: 11737
	private Transform m_transform;

	// Token: 0x020008E8 RID: 2280
	public enum States
	{
		// Token: 0x04002DDB RID: 11739
		OutOfRange,
		// Token: 0x04002DDC RID: 11740
		InRange,
		// Token: 0x04002DDD RID: 11741
		Completed
	}
}
