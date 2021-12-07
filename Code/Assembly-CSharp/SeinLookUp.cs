using System;
using Game;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class SeinLookUp : CharacterState, ISeinReceiver
{
	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00087261 File Offset: 0x00085461
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00087273 File Offset: 0x00085473
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x00087285 File Offset: 0x00085485
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.LookUp = this;
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x000872A0 File Offset: 0x000854A0
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		bool flag = true;
		if (this.Sein.Abilities.Lever && this.Sein.Abilities.Lever.IsUsingLever)
		{
			flag = false;
		}
		if (this.Sein.Abilities.StandingOnEdge && this.Sein.Abilities.StandingOnEdge.StandingOnEdge)
		{
			flag = false;
		}
		if (this.Sein.Controller.IsBashing)
		{
			flag = false;
		}
		if (flag)
		{
			if (this.Sein.Input.Up.Pressed && this.Sein.Controller.CanMove)
			{
				this.m_isLookingUp = true;
				if (this.ShouldLookUpAnimationKeepPlaying())
				{
					this.Sein.Animation.PlayLoop(this.LookUpAnimation, 9, new Func<bool>(this.ShouldLookUpAnimationKeepPlaying), false);
				}
				this.m_lookDelay += Time.deltaTime;
			}
			else if (this.Sein.Input.Down.Pressed && this.Sein.Controller.CanMove)
			{
				this.m_isLookingDown = true;
				this.m_lookDelay += Time.deltaTime;
			}
			else if (this.m_height == 0f)
			{
				this.m_lookDelay = 0f;
			}
		}
		if (!this.PlatformMovement.IsOnGround || this.PlatformMovement.MovingHorizontally)
		{
			this.m_isLookingDown = false;
			this.m_isLookingUp = false;
		}
		if (this.Sein.Input.Up.Released)
		{
			this.m_isLookingUp = false;
		}
		if (this.Sein.Input.Down.Released)
		{
			this.m_isLookingDown = false;
		}
		if (!flag)
		{
			this.m_isLookingDown = false;
			this.m_isLookingUp = false;
		}
		Vector3 additiveDefaultOffset = UI.Cameras.Current.OffsetController.AdditiveDefaultOffset;
		if (this.m_lookDelay > this.LookDelay)
		{
			if (this.m_height >= 0f)
			{
				this.m_height = Mathf.Clamp01(this.m_height + (float)((!this.m_isLookingUp) ? -1 : 1) * Time.deltaTime / this.LookUpDuration);
			}
			if (this.m_height <= 0f)
			{
				this.m_height = Mathf.Clamp(this.m_height - (float)((!this.m_isLookingDown) ? -1 : 1) * Time.deltaTime / this.LookUpDuration, -1f, 0f);
			}
		}
		additiveDefaultOffset.y = Mathf.Lerp(this.m_height * this.LookUpHeight, additiveDefaultOffset.y, Mathf.Pow(this.Drag, Time.deltaTime));
		UI.Cameras.Current.OffsetController.AdditiveDefaultOffset = additiveDefaultOffset;
		base.UpdateCharacterState();
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x000875A8 File Offset: 0x000857A8
	public bool ShouldLookUpAnimationKeepPlaying()
	{
		return !this.Sein.Abilities.Idle.IsOnSlope && this.m_isLookingUp;
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x000875D8 File Offset: 0x000857D8
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_height);
		ar.Serialize(ref this.m_isLookingDown);
		ar.Serialize(ref this.m_isLookingUp);
		ar.Serialize(ref this.m_lookDelay);
		base.Serialize(ar);
	}

	// Token: 0x04001A7D RID: 6781
	public float Drag = 0.2f;

	// Token: 0x04001A7E RID: 6782
	public float LookDelay = 1f;

	// Token: 0x04001A7F RID: 6783
	public TextureAnimationWithTransitions LookUpAnimation;

	// Token: 0x04001A80 RID: 6784
	public float LookUpDuration;

	// Token: 0x04001A81 RID: 6785
	public float LookUpHeight;

	// Token: 0x04001A82 RID: 6786
	public SeinCharacter Sein;

	// Token: 0x04001A83 RID: 6787
	private float m_height;

	// Token: 0x04001A84 RID: 6788
	private bool m_isLookingDown;

	// Token: 0x04001A85 RID: 6789
	private bool m_isLookingUp;

	// Token: 0x04001A86 RID: 6790
	private float m_lookDelay;
}
