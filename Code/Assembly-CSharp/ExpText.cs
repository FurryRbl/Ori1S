using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x02000531 RID: 1329
public class ExpText : Suspendable, IPooled
{
	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x06002321 RID: 8993 RVA: 0x00099F93 File Offset: 0x00098193
	// (set) Token: 0x06002322 RID: 8994 RVA: 0x00099F9B File Offset: 0x0009819B
	public override bool IsSuspended { get; set; }

	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x06002323 RID: 8995 RVA: 0x00099FA4 File Offset: 0x000981A4
	// (set) Token: 0x06002324 RID: 8996 RVA: 0x00099FAC File Offset: 0x000981AC
	public int Amount
	{
		get
		{
			return this.m_amount;
		}
		set
		{
			this.m_amount = value;
			TextBox componentInChildren = base.GetComponentInChildren<TextBox>();
			if (this.m_amount > 0)
			{
				componentInChildren.SetText("<yellow>+" + value + "</>");
			}
			else
			{
				componentInChildren.SetText("0");
			}
			componentInChildren.RenderText();
			this.m_time = 0f;
			this.ScaleShake.Restart();
			this.TransparencyAnimator.Initialize();
			this.TransparencyAnimator.AnimatorDriver.Restart();
		}
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x0009A035 File Offset: 0x00098235
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.deltaTime;
		if (this.m_time > 1f)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x0009A070 File Offset: 0x00098270
	public void OnPoolSpawned()
	{
		this.m_time = 0f;
	}

	// Token: 0x04001D9B RID: 7579
	public LegacyScaleAnimator ScaleShake;

	// Token: 0x04001D9C RID: 7580
	public TransparencyAnimator TransparencyAnimator;

	// Token: 0x04001D9D RID: 7581
	private float m_time;

	// Token: 0x04001D9E RID: 7582
	[PooledSafe]
	private int m_amount;
}
