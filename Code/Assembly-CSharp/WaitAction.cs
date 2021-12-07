using System;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class WaitAction : PerformingAction, ISuspendable
{
	// Token: 0x060019BA RID: 6586 RVA: 0x0006E271 File Offset: 0x0006C471
	public override void Awake()
	{
		SuspensionManager.Register(this);
		base.Awake();
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x0006E27F File Offset: 0x0006C47F
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x0006E28D File Offset: 0x0006C48D
	public override void Perform(IContext context)
	{
		this.m_remainingTime = this.Duration;
		this.m_context = context;
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x0006E2A4 File Offset: 0x0006C4A4
	public override string GetNiceName()
	{
		if (this.Condition)
		{
			if (this.LastActionFinished)
			{
				if (this.Duration == 0f)
				{
					return "Wait for last action done and " + this.Condition.GetNiceName();
				}
				return string.Concat(new object[]
				{
					"Wait ",
					this.Duration,
					" sec",
					(this.Duration != 1f) ? "s" : string.Empty,
					" after last action done, and ",
					this.Condition.GetNiceName()
				});
			}
			else
			{
				if (this.Duration == 0f)
				{
					return "Wait until " + this.Condition.GetNiceName();
				}
				return string.Concat(new object[]
				{
					"Wait ",
					this.Duration,
					" sec",
					(this.Duration != 1f) ? "s" : string.Empty,
					" after ",
					this.Condition.GetNiceName()
				});
			}
		}
		else
		{
			if (!this.LastActionFinished)
			{
				return string.Concat(new object[]
				{
					"Wait ",
					this.Duration,
					" second",
					(this.Duration != 1f) ? "s" : string.Empty
				});
			}
			if (this.Duration == 0f)
			{
				return "Wait for last action done";
			}
			return string.Concat(new object[]
			{
				"Wait ",
				this.Duration,
				" second",
				(this.Duration != 1f) ? "s" : string.Empty,
				" after last action done"
			});
		}
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x0006E49C File Offset: 0x0006C69C
	public override void Stop()
	{
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x060019BF RID: 6591 RVA: 0x0006E4A0 File Offset: 0x0006C6A0
	public bool OtherStuff
	{
		get
		{
			return (this.LastActionFinished && this.LastAction.IsPerforming) || (this.Condition && !this.Condition.Validate(this.m_context));
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0006E4F2 File Offset: 0x0006C6F2
	public override bool IsPerforming
	{
		get
		{
			return this.m_remainingTime > 0f || this.OtherStuff;
		}
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x0006E510 File Offset: 0x0006C710
	public void FixedUpdate()
	{
		if (this.m_remainingTime > 0f && !this.OtherStuff)
		{
			this.m_remainingTime -= ((!this.IsSuspended) ? Time.deltaTime : 0f);
		}
	}

	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0006E55F File Offset: 0x0006C75F
	// (set) Token: 0x060019C3 RID: 6595 RVA: 0x0006E567 File Offset: 0x0006C767
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

	// Token: 0x04001611 RID: 5649
	public float Duration;

	// Token: 0x04001612 RID: 5650
	public bool LastActionFinished;

	// Token: 0x04001613 RID: 5651
	public Condition Condition;

	// Token: 0x04001614 RID: 5652
	[HideInInspector]
	public PerformingAction LastAction;

	// Token: 0x04001615 RID: 5653
	private float m_time;

	// Token: 0x04001616 RID: 5654
	private float m_remainingTime;

	// Token: 0x04001617 RID: 5655
	private IContext m_context;

	// Token: 0x04001618 RID: 5656
	private bool m_isSuspended;
}
