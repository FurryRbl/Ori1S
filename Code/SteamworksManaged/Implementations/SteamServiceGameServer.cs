using System;
using System.Collections.Generic;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000052 RID: 82
	internal abstract class SteamServiceGameServer : SteamService
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00004508 File Offset: 0x00002708
		internal new static Dictionary<CallbackID, NativeCallback> Callbacks
		{
			get
			{
				return ServicesGameServer.Instance.Callbacks;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00004514 File Offset: 0x00002714
		internal new static Dictionary<ResultID, NativeResultCallback> Results
		{
			get
			{
				return ServicesGameServer.Instance.ResultCallbacks;
			}
		}
	}
}
