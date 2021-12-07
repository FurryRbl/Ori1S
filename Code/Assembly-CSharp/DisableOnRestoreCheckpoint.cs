using System;
using Game;
using UnityEngine;

// Token: 0x0200095F RID: 2399
public class DisableOnRestoreCheckpoint : MonoBehaviour
{
	// Token: 0x060034C8 RID: 13512 RVA: 0x000DD7D4 File Offset: 0x000DB9D4
	private void Awake()
	{
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x000DD7EC File Offset: 0x000DB9EC
	private void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x000DD804 File Offset: 0x000DBA04
	private void OnRestoreCheckpoint()
	{
		base.gameObject.SetActive(false);
	}
}
