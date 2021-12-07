using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000162 RID: 354
public class XboxLiveController : MonoBehaviour
{
	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00041D30 File Offset: 0x0003FF30
	public bool IsTrial
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x00041D33 File Offset: 0x0003FF33
	private void Awake()
	{
		XboxLiveController.Instance = this;
		this.InitUsers();
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00041D41 File Offset: 0x0003FF41
	private void Start()
	{
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x00041D44 File Offset: 0x0003FF44
	public void Reset()
	{
		if (this.SessionController != null)
		{
			this.SessionController.Destroy();
		}
		if (this.LeaderboardsUI != null)
		{
			this.LeaderboardsUI.Destroy();
		}
		this.SessionController = null;
		this.LeaderboardsUI = null;
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00041D8C File Offset: 0x0003FF8C
	private void FixedUpdate()
	{
		if (this.LeaderboardsUI != null)
		{
			this.LeaderboardsUI.Update();
		}
		if (this.SessionController != null)
		{
			this.SessionController.Update();
		}
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x00041DC5 File Offset: 0x0003FFC5
	private void ChangeState(XboxLiveController.State state)
	{
		this.m_currentState = state;
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x00041DD0 File Offset: 0x0003FFD0
	public void SetReady()
	{
		this.ChangeState(XboxLiveController.State.Ready);
		if (this.m_onStartPressedCallback != null)
		{
			this.m_onStartPressedCallback();
		}
		this.m_onStartPressedCallback = null;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x00041E01 File Offset: 0x00040001
	public bool IsIdle()
	{
		return this.m_currentState == XboxLiveController.State.Idle;
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x00041E0C File Offset: 0x0004000C
	public bool IsReady()
	{
		return this.m_currentState == XboxLiveController.State.Ready;
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x00041E17 File Offset: 0x00040017
	private bool IsSystemUIVisible()
	{
		return false;
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x00041E1A File Offset: 0x0004001A
	private void RequestSignIn()
	{
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x00041E1C File Offset: 0x0004001C
	public bool IsAnyUserSignedIn(bool onlyOnline)
	{
		return this.m_signedInUserCount > 0 && (!onlyOnline || this.m_signedInUsers[this.m_currentUserIndex].IsUserSignedInOnline);
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x00041E5B File Offset: 0x0004005B
	public int GetCurrentUserIndex()
	{
		return this.m_currentUserIndex;
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x00041E63 File Offset: 0x00040063
	public PlayerState GetPlayerState(int userIndex)
	{
		if (this.m_signedInUsers.ContainsKey(userIndex))
		{
			return this.m_signedInUsers[userIndex];
		}
		return null;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x00041E84 File Offset: 0x00040084
	public bool IsCurrentUserSignedIn(bool onlyOnline)
	{
		return this.IsUserSignedIn(this.GetCurrentUserIndex(), onlyOnline);
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x00041E93 File Offset: 0x00040093
	public bool IsUserSignedIn(int userIndex, bool onlyOnline)
	{
		return this.m_signedInUsers[userIndex].IsSignedIn(onlyOnline);
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x00041EA7 File Offset: 0x000400A7
	public int GetSignedInUserCount()
	{
		return this.m_signedInUserCount;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x00041EAF File Offset: 0x000400AF
	private void OnUserStateChange()
	{
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00041EB4 File Offset: 0x000400B4
	private void InitUsers()
	{
		this.m_signedInUsers.Clear();
		this.m_signedInUserCount = 0;
		for (int i = 0; i < 4; i++)
		{
			this.m_signedInUsers[i] = new PlayerState(i);
		}
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00041EF7 File Offset: 0x000400F7
	private void OnSystemUIVisibilityChange()
	{
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00041EFC File Offset: 0x000400FC
	private void OnControllerStateChange(uint index, bool connected)
	{
		this.m_controllers[(int)index] = connected;
		if (!this.IsControllerConnected(this.GetCurrentUserIndex()))
		{
			UI.Menu.ShowResumeScreen();
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00041F31 File Offset: 0x00040131
	public bool IsControllerConnected(int index)
	{
		return this.m_controllers[index];
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x00041F40 File Offset: 0x00040140
	public bool IsAnyControllerConnected()
	{
		for (int i = 0; i < this.m_controllers.Count; i++)
		{
			if (this.m_controllers[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00041F7D File Offset: 0x0004017D
	private void AskForStorageDevice()
	{
		this.StorageController.AskForStorageDevice((uint)this.GetCurrentUserIndex(), false, 1048576U);
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00041F97 File Offset: 0x00040197
	public void StartPressedOnMainMenu(Action onStartPressedCallback)
	{
		this.m_onStartPressedCallback = onStartPressedCallback;
		this.SetReady();
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x00041FA6 File Offset: 0x000401A6
	public static ulong GetFullOfferID()
	{
		return 6359384330875174913UL;
	}

	// Token: 0x04000B6F RID: 2927
	public static bool IsContentPackage;

	// Token: 0x04000B70 RID: 2928
	public static XboxLiveController Instance;

	// Token: 0x04000B71 RID: 2929
	[HideInInspector]
	public SessionController SessionController;

	// Token: 0x04000B72 RID: 2930
	[HideInInspector]
	public LeaderboardsUI LeaderboardsUI;

	// Token: 0x04000B73 RID: 2931
	[HideInInspector]
	public XboxStorageController StorageController = new XboxStorageController();

	// Token: 0x04000B74 RID: 2932
	private int m_currentUserIndex;

	// Token: 0x04000B75 RID: 2933
	private Dictionary<int, PlayerState> m_signedInUsers = new Dictionary<int, PlayerState>();

	// Token: 0x04000B76 RID: 2934
	private Action m_onStartPressedCallback;

	// Token: 0x04000B77 RID: 2935
	private Dictionary<int, bool> m_controllers = new Dictionary<int, bool>
	{
		{
			0,
			false
		},
		{
			1,
			false
		},
		{
			2,
			false
		},
		{
			3,
			false
		}
	};

	// Token: 0x04000B78 RID: 2936
	private XboxLiveController.State m_currentState;

	// Token: 0x04000B79 RID: 2937
	private int m_signedInUserCount;

	// Token: 0x04000B7A RID: 2938
	public bool IsDebugEnabled;

	// Token: 0x020008B4 RID: 2228
	private enum State
	{
		// Token: 0x04002CE7 RID: 11495
		Idle,
		// Token: 0x04002CE8 RID: 11496
		Start,
		// Token: 0x04002CE9 RID: 11497
		Ready
	}
}
