using System;
using Game;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class DestroyOnScrollLock : MonoBehaviour
{
	// Token: 0x06001F64 RID: 8036 RVA: 0x0008A587 File Offset: 0x00088787
	public void Awake()
	{
		Game.Checkpoint.Events.OnScrollLockPassed.Add(new Action(this.OnScrollLockPassed));
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x0008A59F File Offset: 0x0008879F
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnScrollLockPassed.Remove(new Action(this.OnScrollLockPassed));
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x0008A5B7 File Offset: 0x000887B7
	public void OnScrollLockPassed()
	{
		InstantiateUtility.Destroy(base.gameObject);
	}
}
