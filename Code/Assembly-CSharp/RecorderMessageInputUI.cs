using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class RecorderMessageInputUI : MonoBehaviour
{
	// Token: 0x06001022 RID: 4130 RVA: 0x00049974 File Offset: 0x00047B74
	public void Start()
	{
		this.Enter();
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0004997C File Offset: 0x00047B7C
	private void OnDestroy()
	{
		this.Exit(RecorderMessageInputUI.ExitType.Cancel);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x00049985 File Offset: 0x00047B85
	private void Enter()
	{
		if (this.OnEnterAction)
		{
			this.OnEnterAction.Perform(null);
		}
		GameController.FreezeFixedUpdate = true;
		SuspensionManager.GetSuspendables(this.m_suspendables, base.gameObject);
		SuspensionManager.SuspendExcluding(this.m_suspendables);
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x000499C8 File Offset: 0x00047BC8
	private void Exit(RecorderMessageInputUI.ExitType exitType)
	{
		this.ExitReason = exitType;
		this.m_exitStarted = true;
		this.OnExit();
		if (this.OnExitAction)
		{
			this.OnExitAction.Perform(null);
		}
		SuspensionManager.ResumeExcluding(this.m_suspendables);
		this.m_suspendables.Clear();
		GameController.FreezeFixedUpdate = false;
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x00049A28 File Offset: 0x00047C28
	private void HandleExitEvents()
	{
		if (Event.current.keyCode == KeyCode.Return && !Event.current.shift)
		{
			this.Exit(RecorderMessageInputUI.ExitType.OK);
		}
		if (Event.current.keyCode == KeyCode.Escape)
		{
			this.Exit(RecorderMessageInputUI.ExitType.Cancel);
		}
	}

	// Token: 0x04000D37 RID: 3383
	public string Text;

	// Token: 0x04000D38 RID: 3384
	public ActionMethod OnEnterAction;

	// Token: 0x04000D39 RID: 3385
	public ActionMethod OnExitAction;

	// Token: 0x04000D3A RID: 3386
	public Action OnExit = delegate()
	{
	};

	// Token: 0x04000D3B RID: 3387
	private bool m_exitStarted;

	// Token: 0x04000D3C RID: 3388
	private bool m_shouldFocusOnInputText = true;

	// Token: 0x04000D3D RID: 3389
	private readonly HashSet<ISuspendable> m_suspendables = new HashSet<ISuspendable>();

	// Token: 0x04000D3E RID: 3390
	public RecorderMessageInputUI.ExitType ExitReason;

	// Token: 0x020001A5 RID: 421
	public enum ExitType
	{
		// Token: 0x04000D41 RID: 3393
		OK,
		// Token: 0x04000D42 RID: 3394
		Cancel
	}
}
