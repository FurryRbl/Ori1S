using System;

// Token: 0x0200053B RID: 1339
public interface IRespawnReciever
{
	// Token: 0x0600233C RID: 9020
	void OnTimedRespawn();

	// Token: 0x0600233D RID: 9021
	void RegisterRespawnDelegate(Action onRespawn);
}
