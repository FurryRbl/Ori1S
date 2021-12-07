using System;
using UnityEngine;

// Token: 0x020001A2 RID: 418
public class SceneManagerScene
{
	// Token: 0x06001013 RID: 4115 RVA: 0x00049804 File Offset: 0x00047A04
	public SceneManagerScene(SceneRoot sceneRoot, RuntimeSceneMetaData sceneMetaData)
	{
		this.SceneRoot = sceneRoot;
		this.MetaData = sceneMetaData;
		if (this.MetaData == null)
		{
		}
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00049838 File Offset: 0x00047A38
	public SceneManagerScene(RuntimeSceneMetaData metaData)
	{
		this.MetaData = metaData;
		this.TimeOfLoad = Time.realtimeSinceStartup;
		if (this.MetaData == null)
		{
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06001015 RID: 4117 RVA: 0x0004986F File Offset: 0x00047A6F
	public bool IsLoadingComplete
	{
		get
		{
			return this.CurrentState != SceneManagerScene.State.Loading && this.CurrentState != SceneManagerScene.State.LoadingCancelled;
		}
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x0004988C File Offset: 0x00047A8C
	public void ChangeState(SceneManagerScene.State state)
	{
		this.CurrentState = state;
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06001017 RID: 4119 RVA: 0x00049895 File Offset: 0x00047A95
	public bool UnityIsLoading
	{
		get
		{
			return this.CurrentState == SceneManagerScene.State.Loading || this.CurrentState == SceneManagerScene.State.LoadingCancelled;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06001018 RID: 4120 RVA: 0x000498AF File Offset: 0x00047AAF
	public bool IsVisible
	{
		get
		{
			return this.CurrentState == SceneManagerScene.State.Loaded || this.CurrentState == SceneManagerScene.State.Disabling;
		}
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06001019 RID: 4121 RVA: 0x000498C9 File Offset: 0x00047AC9
	public bool IsTitleScreen
	{
		get
		{
			return this.MetaData.Scene == "titleScreenSwallowsNest";
		}
	}

	// Token: 0x04000D2D RID: 3373
	public SceneRoot SceneRoot;

	// Token: 0x04000D2E RID: 3374
	public RuntimeSceneMetaData MetaData;

	// Token: 0x04000D2F RID: 3375
	public bool HasStartBeenCalled;

	// Token: 0x04000D30 RID: 3376
	public SceneManagerScene.State CurrentState = SceneManagerScene.State.Loading;

	// Token: 0x04000D31 RID: 3377
	public float UnloadTime;

	// Token: 0x04000D32 RID: 3378
	public float TimeOfLoad;

	// Token: 0x04000D33 RID: 3379
	public float LoadingTime;

	// Token: 0x04000D34 RID: 3380
	public bool PreventUnloading;

	// Token: 0x04000D35 RID: 3381
	public bool KeepLoadedForCheckpoint;

	// Token: 0x02000718 RID: 1816
	public enum State
	{
		// Token: 0x0400265A RID: 9818
		Disabling,
		// Token: 0x0400265B RID: 9819
		Disabled,
		// Token: 0x0400265C RID: 9820
		Loading,
		// Token: 0x0400265D RID: 9821
		LoadingCancelled,
		// Token: 0x0400265E RID: 9822
		Loaded
	}
}
