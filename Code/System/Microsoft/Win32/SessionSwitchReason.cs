using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers used to represent the type of a session switch event.</summary>
	// Token: 0x02000019 RID: 25
	public enum SessionSwitchReason
	{
		/// <summary>A session has been connected from the console.</summary>
		// Token: 0x04000040 RID: 64
		ConsoleConnect = 1,
		/// <summary>A session has been disconnected from the console.</summary>
		// Token: 0x04000041 RID: 65
		ConsoleDisconnect,
		/// <summary>A session has been connected from a remote connection.</summary>
		// Token: 0x04000042 RID: 66
		RemoteConnect,
		/// <summary>A session has been disconnected from a remote connection.</summary>
		// Token: 0x04000043 RID: 67
		RemoteDisconnect,
		/// <summary>A user has logged on to a session.</summary>
		// Token: 0x04000044 RID: 68
		SessionLogon,
		/// <summary>A user has logged off from a session.</summary>
		// Token: 0x04000045 RID: 69
		SessionLogoff,
		/// <summary>A session has been locked.</summary>
		// Token: 0x04000046 RID: 70
		SessionLock,
		/// <summary>A session has been unlocked.</summary>
		// Token: 0x04000047 RID: 71
		SessionUnlock,
		/// <summary>A session has changed its status to or from remote controlled mode.</summary>
		// Token: 0x04000048 RID: 72
		SessionRemoteControl
	}
}
