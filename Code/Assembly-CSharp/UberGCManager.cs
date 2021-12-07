using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000824 RID: 2084
public static class UberGCManager
{
	// Token: 0x06002FBE RID: 12222 RVA: 0x000CA95C File Offset: 0x000C8B5C
	public static void OnGameStart()
	{
		object[] array = new object[128];
		for (int i = 0; i < 128; i++)
		{
			array[i] = new byte[1024];
		}
		Events.Scheduler.OnGameFixedUpdateLate.Add(new Action(UberGCManager.Update));
	}

	// Token: 0x06002FBF RID: 12223 RVA: 0x000CA9B2 File Offset: 0x000C8BB2
	public static void CollectProactiveFull()
	{
		Scenes.Manager.DestroyManager.DestroyAll();
		UberGCManager.CollectResourcesIfNeeded();
		GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
	}

	// Token: 0x170007A6 RID: 1958
	// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000CA9D3 File Offset: 0x000C8BD3
	private static float TimeSinceUnload
	{
		get
		{
			return Time.realtimeSinceStartup - UberGCManager.s_lastUnload;
		}
	}

	// Token: 0x06002FC1 RID: 12225 RVA: 0x000CA9E0 File Offset: 0x000C8BE0
	public static void CollectResourcesIfNeeded()
	{
		if (UberGCManager.TimeSinceUnload > 10f)
		{
			AsyncOperation asyncOperation = Resources.UnloadUnusedAssets();
			asyncOperation.priority = 0;
			UberGCManager.s_lastUnload = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06002FC2 RID: 12226 RVA: 0x000CAA13 File Offset: 0x000C8C13
	private static void CollectResourcesIfOutOfMem()
	{
	}

	// Token: 0x06002FC3 RID: 12227 RVA: 0x000CAA18 File Offset: 0x000C8C18
	private static void Update()
	{
		if (Time.realtimeSinceStartup - UberGCManager.s_lastCheck > 2.5f)
		{
			UberGCManager.s_lastCheck = Time.realtimeSinceStartup;
			UberGCManager.CollectResourcesIfOutOfMem();
		}
	}

	// Token: 0x04002AF9 RID: 11001
	private static float s_lastUnload;

	// Token: 0x04002AFA RID: 11002
	private static float s_lastCheck;
}
