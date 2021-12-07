using System;

// Token: 0x020000AD RID: 173
public class GameScheduler
{
	// Token: 0x040005B1 RID: 1457
	public UberDelegate<SceneRoot> OnSceneRootLoadEarlyStart = new UberDelegate<SceneRoot>();

	// Token: 0x040005B2 RID: 1458
	public UberDelegate<SceneRoot> OnSceneStartLateBeforeSerialize = new UberDelegate<SceneRoot>();

	// Token: 0x040005B3 RID: 1459
	public UberDelegate<SceneRoot> OnSceneStartLateAfterSerialize = new UberDelegate<SceneRoot>();

	// Token: 0x040005B4 RID: 1460
	public UberDelegate<SceneRoot> OnSceneRootPreEnabled = new UberDelegate<SceneRoot>();

	// Token: 0x040005B5 RID: 1461
	public UberDelegate<SceneRoot> OnSceneRootEnabledAfterSerialize = new UberDelegate<SceneRoot>();

	// Token: 0x040005B6 RID: 1462
	public UberDelegate<SceneRoot> OnSceneRootDisabled = new UberDelegate<SceneRoot>();

	// Token: 0x040005B7 RID: 1463
	public UberDelegate OnGameAwake = new UberDelegate();

	// Token: 0x040005B8 RID: 1464
	public UberDelegate OnGameStart = new UberDelegate();

	// Token: 0x040005B9 RID: 1465
	public UberDelegate OnGameStartLate = new UberDelegate();

	// Token: 0x040005BA RID: 1466
	public UberDelegate OnGameSerializeLoad = new UberDelegate();

	// Token: 0x040005BB RID: 1467
	public UberDelegate OnPassThroughScrollLock = new UberDelegate();

	// Token: 0x040005BC RID: 1468
	public UberDelegate OnGameFixedUpdate = new UberDelegate();

	// Token: 0x040005BD RID: 1469
	public UberDelegate OnGameFixedUpdateLate = new UberDelegate();

	// Token: 0x040005BE RID: 1470
	public UberDelegate OnPlayerDeath = new UberDelegate();

	// Token: 0x040005BF RID: 1471
	public UberDelegate OnMenuOpen = new UberDelegate();

	// Token: 0x040005C0 RID: 1472
	public UberDelegate OnMenuClose = new UberDelegate();

	// Token: 0x040005C1 RID: 1473
	public UberDelegate OnGameReset = new UberDelegate();

	// Token: 0x040005C2 RID: 1474
	public UberDelegate OnGameLanguageChange = new UberDelegate();

	// Token: 0x040005C3 RID: 1475
	public UberDelegate OnGameControlSchemeChange = new UberDelegate();
}
