using System;
using Game;
using UnityEngine;

// Token: 0x020008F3 RID: 2291
public class MistTorch : MonoBehaviour
{
	// Token: 0x06003306 RID: 13062 RVA: 0x000D7460 File Offset: 0x000D5660
	public void Awake()
	{
		Items.MistTorch = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06003307 RID: 13063 RVA: 0x000D7483 File Offset: 0x000D5683
	public void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06003308 RID: 13064 RVA: 0x000D74A0 File Offset: 0x000D56A0
	public void OnGameReset()
	{
		InstantiateUtility.Destroy(base.gameObject);
		Items.MistTorch = null;
	}
}
