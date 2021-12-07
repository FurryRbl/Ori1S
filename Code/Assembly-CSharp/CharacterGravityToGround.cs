using System;
using Game;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class CharacterGravityToGround : CharacterState
{
	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00081CFF File Offset: 0x0007FEFF
	public CharacterGravity CharacterGravity
	{
		get
		{
			return this.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x170004FC RID: 1276
	// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00081D0C File Offset: 0x0007FF0C
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x00081D19 File Offset: 0x0007FF19
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x00081D24 File Offset: 0x0007FF24
	public override void UpdateCharacterState()
	{
		if (base.Active)
		{
			Vector3 feetPosition = this.PlatformMovement.FeetPosition;
			float num = this.PlatformMovement.GravityAngle;
			if (this.PlatformMovement.IsOnGround)
			{
				Collider groundCollider = this.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider;
				if (groundCollider)
				{
					if (this.m_groundCollider != groundCollider)
					{
						this.m_groundCollider = groundCollider;
						this.m_gravityToGroundSurface = groundCollider.GetComponent<GravityToGroundSurface>();
					}
					if (this.m_gravityToGroundSurface != null)
					{
						if (!this.m_gravityToGroundSurface.RequiresNightberry || (this.m_gravityToGroundSurface.RequiresNightberry && Items.NightBerry && Items.NightBerry.IsCarried))
						{
							num = this.PlatformMovement.GroundAngle;
							if ((this.PlatformMovement as PlatformMovementRigidbodyMoonCharacterControllerPenetrate).GroundNormalIsValid)
							{
								this.PlatformMovement.GravityAngle = (this.CharacterGravity.BaseSettings.GravityAngle = num);
								this.PlatformMovement.transform.eulerAngles = new Vector3(0f, 0f, num);
								this.PlatformMovement.FeetPosition = feetPosition;
							}
						}
					}
					else
					{
						num = Mathf.Round(num / 90f) * 90f;
						if (num != this.CharacterGravity.BaseSettings.GravityAngle)
						{
							this.PlatformMovement.GravityAngle = (this.CharacterGravity.BaseSettings.GravityAngle = num);
							this.PlatformMovement.transform.eulerAngles = new Vector3(0f, 0f, num);
						}
					}
				}
			}
			else
			{
				num = Mathf.Round(num / 90f) * 90f;
				if (num != this.CharacterGravity.BaseSettings.GravityAngle)
				{
					this.PlatformMovement.GravityAngle = (this.CharacterGravity.BaseSettings.GravityAngle = num);
					this.PlatformMovement.transform.eulerAngles = new Vector3(0f, 0f, num);
				}
			}
		}
	}

	// Token: 0x04001985 RID: 6533
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x04001986 RID: 6534
	private Collider m_groundCollider;

	// Token: 0x04001987 RID: 6535
	private GravityToGroundSurface m_gravityToGroundSurface;
}
