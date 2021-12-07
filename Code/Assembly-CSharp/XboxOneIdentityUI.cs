using System;
using Game;
using UnityEngine;

// Token: 0x020008C8 RID: 2248
public class XboxOneIdentityUI : MonoBehaviour
{
	// Token: 0x0600321F RID: 12831 RVA: 0x000D4644 File Offset: 0x000D2844
	public void OnEnable()
	{
		XboxOneIdentityUI.IsVisible = true;
	}

	// Token: 0x06003220 RID: 12832 RVA: 0x000D464C File Offset: 0x000D284C
	public void OnDisable()
	{
		XboxOneIdentityUI.IsVisible = false;
	}

	// Token: 0x06003221 RID: 12833 RVA: 0x000D4654 File Offset: 0x000D2854
	public void Update()
	{
		if (UI.MainMenuVisible || !XboxOneUsers.HasCurrentUser)
		{
			this.Group.SetActive(false);
			return;
		}
		this.Group.SetActive(true);
		if (XboxOneUsers.CurrentUserPicture && this.DisplayPicture.material.mainTexture != XboxOneUsers.CurrentUserPicture)
		{
			this.DisplayPicture.material.mainTexture = XboxOneUsers.CurrentUserPicture;
		}
		string currentUserHandle = XboxOneUsers.CurrentUserHandle;
		if (currentUserHandle != this.m_username)
		{
			this.m_username = currentUserHandle;
			this.Username.SetMessage(new MessageDescriptor(this.m_username));
		}
		if (XboxOneLive.LiveOnline)
		{
			this.OnlineStatusGroup.SetActive(false);
		}
		else
		{
			this.OnlineStatusGroup.SetActive(true);
			MessageProvider messageProvider = (!XboxOneLive.Online) ? this.OfflineMessageProvider : this.LiveUnavailableMessageProvider;
			if (this.OnlineStatus.MessageProvider != messageProvider)
			{
				this.OnlineStatus.SetMessageProvider(messageProvider);
			}
		}
	}

	// Token: 0x04002D0F RID: 11535
	public static bool IsVisible;

	// Token: 0x04002D10 RID: 11536
	[NotNull]
	public MessageBox Username;

	// Token: 0x04002D11 RID: 11537
	[NotNull]
	public MessageBox SwitchProfile;

	// Token: 0x04002D12 RID: 11538
	[NotNull]
	public MessageBox OnlineStatus;

	// Token: 0x04002D13 RID: 11539
	[NotNull]
	public Renderer DisplayPicture;

	// Token: 0x04002D14 RID: 11540
	[NotNull]
	public GameObject Group;

	// Token: 0x04002D15 RID: 11541
	[NotNull]
	public GameObject OnlineStatusGroup;

	// Token: 0x04002D16 RID: 11542
	[NotNull]
	public MessageProvider LiveUnavailableMessageProvider;

	// Token: 0x04002D17 RID: 11543
	[NotNull]
	public MessageProvider OfflineMessageProvider;

	// Token: 0x04002D18 RID: 11544
	private string m_username;
}
