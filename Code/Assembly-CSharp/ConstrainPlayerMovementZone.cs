using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class ConstrainPlayerMovementZone : MonoBehaviour
{
	// Token: 0x06001B9F RID: 7071 RVA: 0x00076E44 File Offset: 0x00075044
	private void OnTriggerEnter(Collider collider)
	{
		SeinCharacter component = collider.GetComponent<SeinCharacter>();
		if (component != null)
		{
			this.m_sein = component;
			this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeedX = 0f;
		}
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x00076E88 File Offset: 0x00075088
	private void OnTriggerExit(Collider collider)
	{
		SeinCharacter component = collider.GetComponent<SeinCharacter>();
		if (component != null && component == this.m_sein)
		{
			this.m_sein = null;
		}
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x00076EC0 File Offset: 0x000750C0
	private void Start()
	{
		Characters.Sein.PlatformBehaviour.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x00076EF0 File Offset: 0x000750F0
	private void OnDestroy()
	{
		Characters.Sein.PlatformBehaviour.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x00076F20 File Offset: 0x00075120
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.m_sein)
		{
			if (this.ConstrainLeft && Core.Input.Horizontal < 0f)
			{
				settings.LockInput = true;
			}
			if (this.ConstrainRight && Core.Input.Horizontal > 0f)
			{
				settings.LockInput = true;
			}
		}
	}

	// Token: 0x04001807 RID: 6151
	private SeinCharacter m_sein;

	// Token: 0x04001808 RID: 6152
	public bool ConstrainLeft;

	// Token: 0x04001809 RID: 6153
	public bool ConstrainRight;
}
