using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class BashAttackCritical : Suspendable, IPooled
{
	// Token: 0x06000340 RID: 832 RVA: 0x0000D88A File Offset: 0x0000BA8A
	public void OnPoolSpawned()
	{
		this.CurrentState = BashAttackCritical.State.Charging;
		this.m_stateCurrentTime = 0f;
		this.m_suspended = false;
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0000D8A5 File Offset: 0x0000BAA5
	public void ChangeState(BashAttackCritical.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0000D8BC File Offset: 0x0000BABC
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case BashAttackCritical.State.Charging:
			this.UpdateChargingState();
			break;
		case BashAttackCritical.State.Critical:
			this.UpdateCriticalState();
			break;
		case BashAttackCritical.State.Failed:
			this.UpdateFailedState();
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0000D91C File Offset: 0x0000BB1C
	private void UpdateFailedState()
	{
		base.transform.localScale = this.m_localScale;
		base.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MaskTexture", new Vector2(0.5f, 0f));
		if (this.m_stateCurrentTime > this.FailedDuration)
		{
			this.ChangeState(BashAttackCritical.State.Finished);
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0000D978 File Offset: 0x0000BB78
	private void UpdateCriticalState()
	{
		base.transform.localScale = this.m_localScale + Vector3.one * Mathf.Sin(this.m_stateCurrentTime * 6.2831855f / this.ShakePeriod) * this.ShakeAmount;
		base.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MaskTexture", new Vector2(0.5f * (float)(Mathf.RoundToInt(this.m_stateCurrentTime * 15f) % 2), 0f));
		if (this.m_stateCurrentTime > this.CriticalDuration)
		{
			this.ChangeState(BashAttackCritical.State.Failed);
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0000DA1C File Offset: 0x0000BC1C
	private void UpdateChargingState()
	{
		base.transform.localScale = this.m_localScale;
		float num = this.m_stateCurrentTime / this.ChargingDuration;
		base.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MaskTexture", new Vector2(0.5f - num * 0.5f, 0f));
		if (this.m_stateCurrentTime > this.ChargingDuration)
		{
			this.ChangeState(BashAttackCritical.State.Critical);
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0000DA8C File Offset: 0x0000BC8C
	public new void Awake()
	{
		base.Awake();
		this.m_localScale = base.transform.localScale;
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000347 RID: 839 RVA: 0x0000DAA5 File Offset: 0x0000BCA5
	// (set) Token: 0x06000348 RID: 840 RVA: 0x0000DAAD File Offset: 0x0000BCAD
	public override bool IsSuspended
	{
		get
		{
			return this.m_suspended;
		}
		set
		{
			this.m_suspended = value;
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0000DAB6 File Offset: 0x0000BCB6
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x04000268 RID: 616
	public float ChargingDuration;

	// Token: 0x04000269 RID: 617
	public float CriticalDuration;

	// Token: 0x0400026A RID: 618
	public float FailedDuration;

	// Token: 0x0400026B RID: 619
	public float ShakePeriod = 0.2f;

	// Token: 0x0400026C RID: 620
	public float ShakeAmount = 0.5f;

	// Token: 0x0400026D RID: 621
	private Vector3 m_localScale;

	// Token: 0x0400026E RID: 622
	public BashAttackCritical.State CurrentState;

	// Token: 0x0400026F RID: 623
	private bool m_suspended;

	// Token: 0x04000270 RID: 624
	private float m_stateCurrentTime;

	// Token: 0x04000271 RID: 625
	public Texture2D BashAttackArrow;

	// Token: 0x04000272 RID: 626
	public Texture2D RedirectArrow;

	// Token: 0x02000051 RID: 81
	public enum State
	{
		// Token: 0x04000274 RID: 628
		Charging,
		// Token: 0x04000275 RID: 629
		Critical,
		// Token: 0x04000276 RID: 630
		Failed,
		// Token: 0x04000277 RID: 631
		Finished
	}
}
