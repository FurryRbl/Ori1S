using System;
using Game;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class NearSeinTrigger : MonoBehaviour
{
	// Token: 0x06001915 RID: 6421 RVA: 0x0006B2D1 File Offset: 0x000694D1
	[UberBuildMethod]
	private void ProvideComponents()
	{
		this.m_recievers = base.gameObject.FindComponents<INearSeinReceiver>();
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x0006B2E4 File Offset: 0x000694E4
	public void Start()
	{
		this.ProvideComponents();
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x0006B2EC File Offset: 0x000694EC
	public void FixedUpdate()
	{
		float num = float.PositiveInfinity;
		if (Characters.Sein)
		{
			num = Vector3.Distance(Characters.Sein.Position, base.transform.position);
		}
		if (!this.m_eventTriggered && this.m_time <= 0f)
		{
			if (num < this.TriggerDistance)
			{
				foreach (INearSeinReceiver nearSeinReceiver in this.m_recievers)
				{
					nearSeinReceiver.OnNearSeinEnter();
				}
				this.m_eventTriggered = true;
				this.TriggerDistance += this.DistanceSmoothFactor;
				this.m_time = this.DelayUntilNextTrigger;
			}
		}
		else if (this.m_eventTriggered && num >= this.TriggerDistance)
		{
			foreach (INearSeinReceiver nearSeinReceiver2 in this.m_recievers)
			{
				nearSeinReceiver2.OnNearSeinExit();
			}
			this.m_eventTriggered = false;
			this.TriggerDistance -= this.DistanceSmoothFactor;
		}
		if (num < this.TriggerDistance)
		{
			foreach (INearSeinReceiver nearSeinReceiver3 in this.m_recievers)
			{
				nearSeinReceiver3.OnSeinNearStay();
			}
		}
		this.m_time -= Time.fixedDeltaTime;
	}

	// Token: 0x0400157D RID: 5501
	public float DelayUntilNextTrigger;

	// Token: 0x0400157E RID: 5502
	public float TriggerDistance;

	// Token: 0x0400157F RID: 5503
	public bool TriggerOnEventEnd;

	// Token: 0x04001580 RID: 5504
	public float DistanceSmoothFactor = 1.5f;

	// Token: 0x04001581 RID: 5505
	private float m_time;

	// Token: 0x04001582 RID: 5506
	private bool m_eventTriggered;

	// Token: 0x04001583 RID: 5507
	[SerializeField]
	private Component[] m_recievers;
}
