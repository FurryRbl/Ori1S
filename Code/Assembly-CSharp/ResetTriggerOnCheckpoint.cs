using System;
using Game;
using UnityEngine;

// Token: 0x02000995 RID: 2453
public class ResetTriggerOnCheckpoint : MonoBehaviour
{
	// Token: 0x0600358D RID: 13709 RVA: 0x000E0A63 File Offset: 0x000DEC63
	public void Awake()
	{
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x000E0A7B File Offset: 0x000DEC7B
	public void Start()
	{
		this.m_trigger = base.GetComponent<Trigger>();
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x000E0A89 File Offset: 0x000DEC89
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x000E0AA1 File Offset: 0x000DECA1
	private void OnRestoreCheckpoint()
	{
		this.m_trigger.Active = true;
	}

	// Token: 0x04003025 RID: 12325
	private Trigger m_trigger;
}
