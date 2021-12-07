using System;
using Game;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class DestroyOnRestoreCheckpoint : MonoBehaviour
{
	// Token: 0x06001029 RID: 4137 RVA: 0x00049A7E File Offset: 0x00047C7E
	public void Awake()
	{
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x00049A9B File Offset: 0x00047C9B
	public void OnDestroy()
	{
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x00049AB8 File Offset: 0x00047CB8
	public void OnRestoreCheckpoint()
	{
		if (base.enabled)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}
}
