using System;
using UnityEngine;

// Token: 0x020008B2 RID: 2226
public class SessionController
{
	// Token: 0x060031A0 RID: 12704 RVA: 0x000D347A File Offset: 0x000D167A
	public void Init()
	{
		this.DebugLog("Init");
	}

	// Token: 0x060031A1 RID: 12705 RVA: 0x000D3487 File Offset: 0x000D1687
	public SessionController.State GetCurrentState()
	{
		return this.m_currentState;
	}

	// Token: 0x060031A2 RID: 12706 RVA: 0x000D348F File Offset: 0x000D168F
	public void Update()
	{
		this.m_stateCurrentTime += Time.fixedDeltaTime;
	}

	// Token: 0x060031A3 RID: 12707 RVA: 0x000D34A3 File Offset: 0x000D16A3
	private void ChangeState(SessionController.State state)
	{
		this.m_currentState = state;
		this.m_stateCurrentTime = 0f;
	}

	// Token: 0x060031A4 RID: 12708 RVA: 0x000D34B7 File Offset: 0x000D16B7
	public void Destroy()
	{
		if (this.ShouldFlush())
		{
			this.RealFlush();
		}
	}

	// Token: 0x060031A5 RID: 12709 RVA: 0x000D34CA File Offset: 0x000D16CA
	private bool ShouldFlush()
	{
		return this.m_writtenCount > 5 || this.m_shouldFlush;
	}

	// Token: 0x060031A6 RID: 12710 RVA: 0x000D34E1 File Offset: 0x000D16E1
	public void Flush()
	{
		this.DebugLog("Flush");
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x000D34EE File Offset: 0x000D16EE
	private void RealFlush()
	{
		this.DebugLog("RealFlush");
		this.m_shouldFlush = false;
		this.m_writtenCount = 0;
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x000D3509 File Offset: 0x000D1709
	public bool IsIdle()
	{
		this.DebugLog("IsIdle");
		return false;
	}

	// Token: 0x060031A9 RID: 12713 RVA: 0x000D3517 File Offset: 0x000D1717
	private void DebugLog(string str)
	{
	}

	// Token: 0x04002CDC RID: 11484
	private SessionController.State m_currentState;

	// Token: 0x04002CDD RID: 11485
	private float m_stateCurrentTime;

	// Token: 0x04002CDE RID: 11486
	private int m_writtenCount;

	// Token: 0x04002CDF RID: 11487
	private bool m_shouldFlush;

	// Token: 0x020008B3 RID: 2227
	public enum State
	{
		// Token: 0x04002CE1 RID: 11489
		Init,
		// Token: 0x04002CE2 RID: 11490
		Starting,
		// Token: 0x04002CE3 RID: 11491
		Joining,
		// Token: 0x04002CE4 RID: 11492
		Idle,
		// Token: 0x04002CE5 RID: 11493
		Busy
	}
}
