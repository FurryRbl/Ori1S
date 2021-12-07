using System;
using UnityEngine;

// Token: 0x020008DD RID: 2269
public class Checkpoint : MonoBehaviour
{
	// Token: 0x0600329E RID: 12958 RVA: 0x000D5ADE File Offset: 0x000D3CDE
	public void Awake()
	{
	}

	// Token: 0x0600329F RID: 12959 RVA: 0x000D5AE0 File Offset: 0x000D3CE0
	public virtual bool CanCreateCheckpoint()
	{
		return this.m_shouldAcceptRecievers && (!this.Condition || this.Condition.Validate(null));
	}

	// Token: 0x060032A0 RID: 12960 RVA: 0x000D5B13 File Offset: 0x000D3D13
	public void FixedUpdate()
	{
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x000D5B15 File Offset: 0x000D3D15
	public void OnCheckpointCreated()
	{
		this.m_shouldAcceptRecievers = false;
	}

	// Token: 0x04002D86 RID: 11654
	public Condition Condition;

	// Token: 0x04002D87 RID: 11655
	public Vector2 RespawnPosition = Vector2.zero;

	// Token: 0x04002D88 RID: 11656
	private bool m_shouldAcceptRecievers;
}
