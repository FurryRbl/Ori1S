using System;
using System.Collections.Generic;

namespace ManagedSteam.Implementations
{
	// Token: 0x0200000D RID: 13
	internal abstract class SteamService
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002567 File Offset: 0x00000767
		internal SteamService()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000256F File Offset: 0x0000076F
		internal static Dictionary<CallbackID, NativeCallback> Callbacks
		{
			get
			{
				return Steam.Instance.Callbacks;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000257B File Offset: 0x0000077B
		internal static Dictionary<ResultID, NativeResultCallback> Results
		{
			get
			{
				return Steam.Instance.ResultCallbacks;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002587 File Offset: 0x00000787
		internal void CheckIfUsable()
		{
			Steam.Instance.CheckIfUsable();
			this.CheckIfUsableInternal();
		}

		// Token: 0x06000025 RID: 37
		internal abstract void CheckIfUsableInternal();

		// Token: 0x06000026 RID: 38
		internal abstract void ReleaseManagedResources();

		// Token: 0x06000027 RID: 39
		internal abstract void InvokeEvents();

		// Token: 0x06000028 RID: 40 RVA: 0x0000259C File Offset: 0x0000079C
		internal static void InvokeEvents<T>(List<SteamService.Result<T>> values, ResultEvent<T> eventList) where T : struct
		{
			if (eventList != null)
			{
				foreach (SteamService.Result<T> result in values)
				{
					eventList(result.Data, result.Flag);
				}
			}
			values.Clear();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002600 File Offset: 0x00000800
		internal static void InvokeEvents<T>(List<T> values, CallbackEvent<T> eventList) where T : struct
		{
			if (eventList != null)
			{
				foreach (T value in values)
				{
					eventList(value);
				}
			}
			values.Clear();
		}

		// Token: 0x0200000E RID: 14
		internal struct Result<T> where T : struct
		{
			// Token: 0x0600002A RID: 42 RVA: 0x00002658 File Offset: 0x00000858
			public Result(T data, bool flag)
			{
				this.Data = data;
				this.Flag = flag;
			}

			// Token: 0x04000033 RID: 51
			public T Data;

			// Token: 0x04000034 RID: 52
			public bool Flag;
		}
	}
}
