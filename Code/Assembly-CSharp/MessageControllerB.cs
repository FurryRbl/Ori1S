using System;
using Game;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class MessageControllerB : MonoBehaviour
{
	// Token: 0x1700020A RID: 522
	// (get) Token: 0x0600098E RID: 2446 RVA: 0x0002A1DB File Offset: 0x000283DB
	public bool AnyAbilityPickupStoryMessagesVisible
	{
		get
		{
			return this.m_currentMessageBox;
		}
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0002A1E8 File Offset: 0x000283E8
	public GameObject ShowMessageBox(GameObject messageBoxPrefab, MessageProvider messageProvider, Vector3 position, float duration = 3f)
	{
		if (messageProvider == null)
		{
			return null;
		}
		if (SeinUI.DebugHideUI)
		{
			return null;
		}
		GameObject gameObject = InstantiateUtility.Instantiate(messageBoxPrefab, position, Quaternion.identity) as GameObject;
		MessageBox componentInChildren = gameObject.GetComponentInChildren<MessageBox>();
		if (componentInChildren.Visibility)
		{
			componentInChildren.SetWaitDuration(duration);
		}
		componentInChildren.SetMessageProvider(messageProvider);
		return gameObject;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0002A248 File Offset: 0x00028448
	public MessageBox ShowHintMessage(MessageProvider messageProvider, Vector3 position, float duration = 3f)
	{
		GameObject gameObject = this.ShowMessageBox(this.HintMessage, messageProvider, position, duration);
		return (!gameObject) ? null : gameObject.GetComponentInChildren<MessageBox>();
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0002A27C File Offset: 0x0002847C
	public MessageBox ShowMessageBoxB(GameObject messageBoxPrefab, MessageProvider messageProvider, Vector3 position, float duration = 3f)
	{
		GameObject gameObject = this.ShowMessageBox(messageBoxPrefab, messageProvider, position, duration);
		return (!gameObject) ? null : gameObject.GetComponentInChildren<MessageBox>();
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0002A2AC File Offset: 0x000284AC
	public MessageBox ShowAreaMessage(MessageProvider messageProvider)
	{
		this.m_currentMessageBox = this.ShowMessageBoxB(this.AreaMessage, messageProvider, Vector3.zero, 3f);
		return this.m_currentMessageBox;
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0002A2D4 File Offset: 0x000284D4
	public MessageBox ShowAbilityMessage(MessageProvider messageProvider, GameObject avatar)
	{
		UI.Hints.HideExistingHint();
		MessageBox messageBox = this.ShowMessageBoxB(this.AbilityMessage, messageProvider, new Vector3(0f, 2f), float.PositiveInfinity);
		if (messageBox && avatar)
		{
			messageBox.SetAvatar(avatar);
		}
		this.m_currentMessageBox = messageBox;
		return messageBox;
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0002A330 File Offset: 0x00028530
	public MessageBox ShowPickupMessage(MessageProvider messageProvider, GameObject avatar)
	{
		UI.Hints.HideExistingHint();
		MessageBox messageBox = this.ShowMessageBoxB(this.PickupMessage, messageProvider, new Vector3(0f, 2f), float.PositiveInfinity);
		if (messageBox && avatar)
		{
			messageBox.SetAvatar(avatar);
		}
		this.m_currentMessageBox = messageBox;
		return messageBox;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0002A38C File Offset: 0x0002858C
	public MessageBox ShowStoryMessage(MessageProvider messageProvider)
	{
		UI.Hints.HideExistingHint();
		MessageBox messageBox = this.ShowMessageBoxB(this.StoryMessage, messageProvider, Vector3.zero, float.PositiveInfinity);
		this.m_currentMessageBox = messageBox;
		return messageBox;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0002A3C0 File Offset: 0x000285C0
	public MessageBox ShowHelpMessage(MessageProvider messageProvider, GameObject avatar)
	{
		UI.Hints.HideExistingHint();
		MessageBox messageBox = this.ShowMessageBoxB(this.HelpMessage, messageProvider, Vector3.zero, float.PositiveInfinity);
		if (messageBox && avatar)
		{
			messageBox.SetAvatar(avatar);
		}
		return messageBox;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0002A408 File Offset: 0x00028608
	public GameObject ShowSpiritTreeTextMessage(MessageProvider messageProvider, Vector3 position)
	{
		return this.ShowMessageBox(this.SpiritTreeText, messageProvider, position, 0f);
	}

	// Token: 0x040007E5 RID: 2021
	public float DefaultDuration;

	// Token: 0x040007E6 RID: 2022
	public GameObject AreaMessage;

	// Token: 0x040007E7 RID: 2023
	public GameObject AbilityMessage;

	// Token: 0x040007E8 RID: 2024
	public GameObject HintMessage;

	// Token: 0x040007E9 RID: 2025
	public GameObject PickupMessage;

	// Token: 0x040007EA RID: 2026
	public GameObject StoryMessage;

	// Token: 0x040007EB RID: 2027
	public GameObject HelpMessage;

	// Token: 0x040007EC RID: 2028
	public GameObject SpiritTreeText;

	// Token: 0x040007ED RID: 2029
	private MessageBox m_currentMessageBox;
}
