using System;
using UnityEngine;

// Token: 0x020005F3 RID: 1523
public class UberSpawnManager : MonoBehaviour
{
	// Token: 0x06002632 RID: 9778 RVA: 0x000A7AF7 File Offset: 0x000A5CF7
	private void Awake()
	{
		UberSpawnManager.Instance = this;
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x000A7AFF File Offset: 0x000A5CFF
	public bool GetSpawnOk()
	{
		return this.m_budget > 0f;
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x000A7B0E File Offset: 0x000A5D0E
	private void FixedUpdate()
	{
		this.m_budget = 1f;
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x000A7B1B File Offset: 0x000A5D1B
	public void StartSpawn()
	{
		this.m_startSpawn = Time.realtimeSinceStartup;
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x000A7B28 File Offset: 0x000A5D28
	public void StopSpawn()
	{
		float num = (Time.realtimeSinceStartup - this.m_startSpawn) * 1000f;
		this.m_budget -= num;
	}

	// Token: 0x040020D1 RID: 8401
	private const float c_frameBudget = 1f;

	// Token: 0x040020D2 RID: 8402
	public static UberSpawnManager Instance;

	// Token: 0x040020D3 RID: 8403
	private float m_budget = 1f;

	// Token: 0x040020D4 RID: 8404
	private float m_startSpawn;
}
