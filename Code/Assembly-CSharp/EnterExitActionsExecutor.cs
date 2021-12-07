using System;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class EnterExitActionsExecutor : MonoBehaviour
{
	// Token: 0x1400002D RID: 45
	// (add) Token: 0x060018A1 RID: 6305 RVA: 0x00069B21 File Offset: 0x00067D21
	// (remove) Token: 0x060018A2 RID: 6306 RVA: 0x00069B3A File Offset: 0x00067D3A
	public event Action OnExitActionFinished = delegate()
	{
	};

	// Token: 0x060018A3 RID: 6307 RVA: 0x00069B54 File Offset: 0x00067D54
	private void FixedUpdate()
	{
		bool isPerforming = this.ExitAction.IsPerforming;
		if (this.m_shouldPerformEnter && !isPerforming)
		{
			this.m_shouldPerformEnter = false;
			this.PerformEnterAction();
		}
		else if (this.m_shouldPerformExit && !this.EnterAction.IsPerforming)
		{
			this.m_shouldPerformExit = false;
			this.PerformExitAction();
		}
		if (this.m_exitActionWasPerforming && !isPerforming)
		{
			this.m_exitActionWasPerforming = false;
			this.OnExitActionFinished();
		}
		this.m_exitActionWasPerforming = isPerforming;
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x00069BE4 File Offset: 0x00067DE4
	public void EnterTrigger()
	{
		this.m_shouldPerformExit = false;
		this.m_shouldPerformEnter = false;
		if (this.ExitAction && this.ExitAction.IsPerforming)
		{
			if (this.WaitForExitActionToFinish)
			{
				this.m_shouldPerformEnter = true;
				return;
			}
			this.ExitAction.Stop();
		}
		this.PerformEnterAction();
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x00069C44 File Offset: 0x00067E44
	public void ExitTrigger()
	{
		this.m_shouldPerformExit = false;
		this.m_shouldPerformEnter = false;
		if (this.EnterAction && this.EnterAction.IsPerforming)
		{
			if (this.WaitForEnterActionToFinish)
			{
				this.m_shouldPerformExit = true;
				return;
			}
			this.EnterAction.Stop();
		}
		this.PerformExitAction();
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x00069CA3 File Offset: 0x00067EA3
	private void PerformEnterAction()
	{
		this.EnterAction.Perform(null);
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x00069CB1 File Offset: 0x00067EB1
	private void PerformExitAction()
	{
		this.ExitAction.Perform(null);
	}

	// Token: 0x0400152E RID: 5422
	public PerformingAction EnterAction;

	// Token: 0x0400152F RID: 5423
	public PerformingAction ExitAction;

	// Token: 0x04001530 RID: 5424
	public bool WaitForEnterActionToFinish;

	// Token: 0x04001531 RID: 5425
	public bool WaitForExitActionToFinish;

	// Token: 0x04001532 RID: 5426
	private bool m_shouldPerformEnter;

	// Token: 0x04001533 RID: 5427
	private bool m_shouldPerformExit;

	// Token: 0x04001534 RID: 5428
	private bool m_exitActionWasPerforming;
}
