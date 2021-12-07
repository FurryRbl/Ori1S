using System;
using UnityEngine;

// Token: 0x02000909 RID: 2313
public class LaserPulsator : SaveSerialize, ISuspendable
{
	// Token: 0x06003359 RID: 13145 RVA: 0x000D8969 File Offset: 0x000D6B69
	public new void Awake()
	{
		SuspensionManager.Register(this);
		this.m_time += this.Offset;
	}

	// Token: 0x0600335A RID: 13146 RVA: 0x000D8984 File Offset: 0x000D6B84
	public new void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600335B RID: 13147 RVA: 0x000D898C File Offset: 0x000D6B8C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.deltaTime * this.Speed;
		float num = this.LaserCurve.Evaluate(this.m_time);
		bool activated = this.BlockableLaser.Activated;
		this.BlockableLaser.Activated = (num > 0.5f);
		float num2 = this.AnticipationCurve.Evaluate(this.m_time);
		if (this.m_shouldPlayAnticipation && num2 > 0f && Mathf.Approximately(this.m_previousAnticipationValue, 0f))
		{
			this.BlockableLaser.DoAnticipation();
			this.m_shouldPlayAnticipation = false;
		}
		if (activated != this.BlockableLaser.Activated)
		{
			if (!this.BlockableLaser.Activated)
			{
				this.m_shouldPlayAnticipation = true;
			}
		}
		this.m_previousAnticipationValue = num2;
	}

	// Token: 0x1700081A RID: 2074
	// (get) Token: 0x0600335C RID: 13148 RVA: 0x000D8A72 File Offset: 0x000D6C72
	// (set) Token: 0x0600335D RID: 13149 RVA: 0x000D8A7A File Offset: 0x000D6C7A
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x0600335E RID: 13150 RVA: 0x000D8A83 File Offset: 0x000D6C83
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_time);
	}

	// Token: 0x04002E61 RID: 11873
	public AnimationCurve LaserCurve;

	// Token: 0x04002E62 RID: 11874
	public AnimationCurve AnticipationCurve;

	// Token: 0x04002E63 RID: 11875
	public BlockableLaser BlockableLaser;

	// Token: 0x04002E64 RID: 11876
	public float Offset;

	// Token: 0x04002E65 RID: 11877
	public float Speed = 1f;

	// Token: 0x04002E66 RID: 11878
	private float m_time;

	// Token: 0x04002E67 RID: 11879
	private bool m_shouldPlayAnticipation = true;

	// Token: 0x04002E68 RID: 11880
	private float m_previousAnticipationValue;

	// Token: 0x04002E69 RID: 11881
	private bool m_isSuspended;
}
