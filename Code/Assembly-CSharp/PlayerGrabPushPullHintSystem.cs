using System;
using Game;
using UnityEngine;

// Token: 0x02000449 RID: 1097
public class PlayerGrabPushPullHintSystem : MonoBehaviour, ISeinReceiver
{
	// Token: 0x06001E8C RID: 7820 RVA: 0x00086AB8 File Offset: 0x00084CB8
	public void OnEnterRange(bool bash)
	{
		if (this.m_pressToGrab == null && (Time.realtimeSinceStartup > this.m_timeOfLastPressToPushOrPullMessage + 30f || !this.Sein.PlayerAbilities.HasAbility(AbilityType.WallJump)))
		{
			this.m_pressToGrab = UI.Hints.Show((!bash) ? this.PressToGrabMessage : this.PressToGrabOrBashMessage, HintLayer.HintZone, 3f);
			this.m_timeOfLastPressToPushOrPullMessage = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x00086B38 File Offset: 0x00084D38
	public void OnGrabBlock()
	{
		if (this.m_pressToGrab)
		{
			this.m_pressToGrab.HideMessageScreen();
		}
		if (this.m_pressToPushOrPull == null && (Time.realtimeSinceStartup > this.m_timeOfLastPressToGrabMessage + 30f || !this.Sein.PlayerAbilities.HasAbility(AbilityType.WallJump)))
		{
			this.m_pressToPushOrPull = UI.Hints.Show(this.PressToPushOrPullMessage, HintLayer.HintZone, 3f);
			this.m_timeOfLastPressToGrabMessage = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x00086BC0 File Offset: 0x00084DC0
	public void HidePressToPushOrPull()
	{
		if (this.m_pressToPushOrPull)
		{
			this.m_pressToPushOrPull.HideMessageScreen();
			this.m_pressToPushOrPull = null;
		}
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x00086BF0 File Offset: 0x00084DF0
	public void HidePressToGrab()
	{
		if (this.m_pressToGrab)
		{
			this.m_pressToGrab.HideMessageScreen();
			this.m_pressToGrab = null;
		}
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x00086C20 File Offset: 0x00084E20
	public void FixedUpdate()
	{
		if (!this.Sein.Abilities.GrabBlock.InRange || this.Sein.Abilities.GrabBlock.IsGrabbing)
		{
			this.HidePressToGrab();
		}
		if (!this.Sein.Abilities.GrabBlock.IsGrabbing)
		{
			this.HidePressToPushOrPull();
		}
		if (this.Sein.Abilities.GrabBlock.IsPulling || this.Sein.Abilities.GrabBlock.IsPushing)
		{
			this.HidePressToPushOrPull();
		}
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x00086CC1 File Offset: 0x00084EC1
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x04001A45 RID: 6725
	public const float TIME_BETWEEN_HINTS = 30f;

	// Token: 0x04001A46 RID: 6726
	public SeinCharacter Sein;

	// Token: 0x04001A47 RID: 6727
	public MessageProvider PressToGrabMessage;

	// Token: 0x04001A48 RID: 6728
	public MessageProvider PressToPushOrPullMessage;

	// Token: 0x04001A49 RID: 6729
	public MessageProvider PressToGrabOrBashMessage;

	// Token: 0x04001A4A RID: 6730
	private float m_timeOfLastPressToGrabMessage;

	// Token: 0x04001A4B RID: 6731
	private float m_timeOfLastPressToPushOrPullMessage;

	// Token: 0x04001A4C RID: 6732
	private MessageBox m_pressToGrab;

	// Token: 0x04001A4D RID: 6733
	private MessageBox m_pressToPushOrPull;
}
