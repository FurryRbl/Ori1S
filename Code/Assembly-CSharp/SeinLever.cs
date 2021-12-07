using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class SeinLever : CharacterState, ISeinReceiver
{
	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00079686 File Offset: 0x00077886
	public bool IsUsingLever
	{
		get
		{
			return this.m_lever != null && this.m_lever.IsGrabbed;
		}
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x000796A7 File Offset: 0x000778A7
	private void ShowGrabHint()
	{
		if (this.PressToGrabMessage && !GameController.Instance.InputLocked)
		{
			this.m_pressToGrabHint = UI.Hints.Show(this.PressToGrabMessage, HintLayer.HintZone, 3f);
		}
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x000796DF File Offset: 0x000778DF
	private void HideGrabHint()
	{
		if (this.m_pressToGrabHint)
		{
			this.m_pressToGrabHint.HideMessageScreen();
		}
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x000796FC File Offset: 0x000778FC
	private void ShowLeftRightHint()
	{
		if (this.PressLeftRightMessage && !GameController.Instance.InputLocked)
		{
			this.m_pressLeftRightHint = UI.Hints.Show(this.PressLeftRightMessage, HintLayer.HintZone, 3f);
		}
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x00079734 File Offset: 0x00077934
	private void HideLeftRightHint()
	{
		if (this.m_pressLeftRightHint)
		{
			this.m_pressLeftRightHint.HideMessageScreen();
		}
	}

	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00079751 File Offset: 0x00077951
	// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0007976D File Offset: 0x0007796D
	public bool FaceLeft
	{
		get
		{
			return this.m_sein.PlatformBehaviour.LeftRightMovement.SpriteMirror.FaceLeft;
		}
		set
		{
			this.m_sein.PlatformBehaviour.LeftRightMovement.SpriteMirror.FaceLeft = value;
		}
	}

	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0007978A File Offset: 0x0007798A
	public bool InputLocked
	{
		get
		{
			return this.m_lever != null && this.m_lever.IsGrabbed;
		}
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x000797AB File Offset: 0x000779AB
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000797B4 File Offset: 0x000779B4
	public void EnterLever(Lever lever)
	{
		this.m_lever = lever;
		this.m_lever.OnEnterLever();
		if (this.m_lever.CanBeGrabbed)
		{
			this.ShowGrabHint();
		}
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000797E9 File Offset: 0x000779E9
	public void ExitLever()
	{
		this.m_lever.OnExitLever();
		this.m_lever = null;
		this.HideGrabHint();
		this.HideLeftRightHint();
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x00079809 File Offset: 0x00077A09
	public override void OnExit()
	{
		if (this.m_lever)
		{
			this.ExitLever();
		}
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x00079821 File Offset: 0x00077A21
	public override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x00079829 File Offset: 0x00077A29
	public override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x00079831 File Offset: 0x00077A31
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			if (this.m_lever != null)
			{
				this.m_lever.OnExitLever();
			}
			this.m_moveSeinPosition = false;
			this.m_lever = null;
		}
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x00079868 File Offset: 0x00077A68
	public override void UpdateCharacterState()
	{
		Vector3 position = this.m_sein.Position;
		if (GameController.Instance.LockInputByAction)
		{
			return;
		}
		if (this.m_lever)
		{
			if (this.m_lever.IsGrabbed)
			{
				if (Core.Input.Glide.Released)
				{
					this.ReleaseLever();
				}
				else
				{
					this.UpdateLeverDirection();
				}
			}
			else
			{
				float num = Vector3.Distance(position, this.m_lever.Position);
				if (this.m_lever.InRange && num > this.m_lever.Radius)
				{
					this.ExitLever();
				}
				else if (this.m_lever.InRange && Core.Input.Glide.OnPressed && !Core.Input.Glide.Used && this.m_lever.CanBeGrabbed && (!this.m_lever.NeedsToBeOnGround || this.m_sein.PlatformBehaviour.PlatformMovement.IsOnGround) && !this.m_sein.Controller.IsCarrying)
				{
					Core.Input.Glide.Used = true;
					this.GrabLever();
				}
			}
		}
		else
		{
			for (int i = 0; i < Lever.All.Count; i++)
			{
				Lever lever = Lever.All[i];
				float num2 = Vector3.Distance(position, lever.Position);
				if (num2 < lever.Radius)
				{
					this.EnterLever(lever);
				}
			}
		}
		if (this.m_moveSeinPosition && this.m_lever)
		{
			Vector3 vector = this.m_lever.Position - this.m_sein.PlatformBehaviour.PlatformMovement.FeetPosition;
			vector += this.m_sein.PlatformBehaviour.PlatformMovement.GroundBinormal * this.m_seinTargetPositionX;
			vector = MoonMath.Angle.Unrotate(vector, this.m_sein.PlatformBehaviour.PlatformMovement.GroundAngle);
			vector.y = 0f;
			vector.x *= 0.5f;
			vector = MoonMath.Angle.Rotate(vector, this.m_sein.PlatformBehaviour.PlatformMovement.GroundAngle);
			vector.z = 0f;
			this.m_sein.Position += vector;
		}
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x00079AF0 File Offset: 0x00077CF0
	public void PlayLeftAnimation()
	{
		this.m_sein.Animation.PlayLoop((!this.FaceLeft) ? this.Animations.LeverLeft : this.Animations.LeverRight, 200, new Func<bool>(this.m_lever.PlayLeverAnimation), false);
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x00079B4C File Offset: 0x00077D4C
	public void PlayMiddleAnimation()
	{
		this.m_sein.Animation.PlayLoop(this.Animations.LeverMiddle, 200, new Func<bool>(this.m_lever.PlayLeverAnimation), false);
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x00079B8C File Offset: 0x00077D8C
	public void PlayRightAnimation()
	{
		this.m_sein.Animation.PlayLoop((!this.FaceLeft) ? this.Animations.LeverRight : this.Animations.LeverLeft, 200, new Func<bool>(this.m_lever.PlayLeverAnimation), false);
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x00079BE7 File Offset: 0x00077DE7
	public void PushLeverMiddle()
	{
		this.m_lever.OnPushLeverMiddle();
		this.PlayMiddleAnimation();
		this.HideLeftRightHint();
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x00079C00 File Offset: 0x00077E00
	public void PushLeverLeft()
	{
		this.m_lever.OnPushLeverLeft();
		this.PlayLeftAnimation();
		this.HideLeftRightHint();
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x00079C19 File Offset: 0x00077E19
	public void PushLeverRight()
	{
		this.m_lever.OnPushLeverRight();
		this.PlayRightAnimation();
		this.HideLeftRightHint();
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x00079C34 File Offset: 0x00077E34
	public void GrabLever()
	{
		this.m_lever.OnGrabLever();
		this.m_moveSeinPosition = true;
		bool flag = Vector3.Dot(this.m_sein.PlatformBehaviour.PlatformMovement.GroundBinormal, this.m_sein.PlatformBehaviour.PlatformMovement.Position - this.m_lever.Position) < 0f;
		this.m_seinTargetPositionX = this.m_lever.SeinPositionOffset * (float)((!flag) ? 1 : -1);
		this.FaceLeft = !flag;
		switch (this.m_lever.Direction)
		{
		case Lever.LeverDirections.Left:
			this.PlayLeftAnimation();
			break;
		case Lever.LeverDirections.Middle:
			this.PlayMiddleAnimation();
			break;
		case Lever.LeverDirections.Right:
			this.PlayRightAnimation();
			break;
		}
		this.HideGrabHint();
		this.ShowLeftRightHint();
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x00079D14 File Offset: 0x00077F14
	public void ReleaseLever()
	{
		this.m_lever.OnReleaseLever();
		this.m_moveSeinPosition = false;
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x00079D28 File Offset: 0x00077F28
	private void UpdateLeverDirection()
	{
		switch (this.m_lever.Direction)
		{
		case Lever.LeverDirections.Left:
			this.UpdateLeft();
			break;
		case Lever.LeverDirections.Middle:
			this.UpdateMiddle();
			break;
		case Lever.LeverDirections.Right:
			this.UpdateRight();
			break;
		}
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x00079D7C File Offset: 0x00077F7C
	private void UpdateMiddle()
	{
		Lever.LeverMode leverType = this.m_lever.LeverType;
		if (leverType != Lever.LeverMode.LeftMiddleRightSpring)
		{
			if (leverType == Lever.LeverMode.LeftMiddleRightStay)
			{
				if (Core.Input.Left.OnPressed)
				{
					this.PushLeverLeft();
				}
				else if (Core.Input.Right.OnPressed)
				{
					this.PushLeverRight();
				}
			}
		}
		else if (Core.Input.Left.Pressed)
		{
			this.PushLeverLeft();
		}
		else if (Core.Input.Right.Pressed)
		{
			this.PushLeverRight();
		}
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x00079E10 File Offset: 0x00078010
	private void UpdateLeft()
	{
		switch (this.m_lever.LeverType)
		{
		case Lever.LeverMode.LeftRightGrab:
			if (Core.Input.Right.Pressed)
			{
				this.PushLeverRight();
			}
			break;
		case Lever.LeverMode.LeftMiddleRightSpring:
			if (Core.Input.Right.Pressed)
			{
				this.PushLeverRight();
			}
			else if (Core.Input.Left.Released || !this.m_lever.CanLeverLeft())
			{
				this.PushLeverMiddle();
			}
			break;
		case Lever.LeverMode.LeftMiddleRightStay:
			if (Core.Input.Right.OnPressed || !this.m_lever.CanLeverLeft())
			{
				this.PushLeverMiddle();
			}
			break;
		}
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x00079ED4 File Offset: 0x000780D4
	private void UpdateRight()
	{
		switch (this.m_lever.LeverType)
		{
		case Lever.LeverMode.LeftRightToggle:
			if (Core.Input.Glide.Released)
			{
				this.ReleaseLever();
			}
			break;
		case Lever.LeverMode.LeftRightGrab:
			if (Core.Input.Left.Pressed)
			{
				this.PushLeverLeft();
			}
			break;
		case Lever.LeverMode.LeftMiddleRightSpring:
			if (Core.Input.Left.OnPressed)
			{
				this.PushLeverLeft();
			}
			else if (Core.Input.Right.Released || !this.m_lever.CanLeverRight())
			{
				this.PushLeverMiddle();
			}
			break;
		case Lever.LeverMode.LeftMiddleRightStay:
			if (Core.Input.Left.OnPressed || !this.m_lever.CanLeverRight())
			{
				this.PushLeverMiddle();
			}
			break;
		}
	}

	// Token: 0x04001867 RID: 6247
	public SeinLever.LeverAnimations Animations;

	// Token: 0x04001868 RID: 6248
	public MessageProvider PressToGrabMessage;

	// Token: 0x04001869 RID: 6249
	public MessageProvider PressLeftRightMessage;

	// Token: 0x0400186A RID: 6250
	private Lever m_lever;

	// Token: 0x0400186B RID: 6251
	private bool m_moveSeinPosition;

	// Token: 0x0400186C RID: 6252
	private SeinCharacter m_sein;

	// Token: 0x0400186D RID: 6253
	private float m_seinTargetPositionX;

	// Token: 0x0400186E RID: 6254
	private MessageBox m_pressToGrabHint;

	// Token: 0x0400186F RID: 6255
	private MessageBox m_pressLeftRightHint;

	// Token: 0x020008ED RID: 2285
	[Serializable]
	public class LeverAnimations
	{
		// Token: 0x04002DF5 RID: 11765
		public TextureAnimationWithTransitions LeverLeft;

		// Token: 0x04002DF6 RID: 11766
		public TextureAnimationWithTransitions LeverMiddle;

		// Token: 0x04002DF7 RID: 11767
		public TextureAnimationWithTransitions LeverRight;
	}
}
