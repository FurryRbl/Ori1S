using System;
using Game;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class SeeSeinHorizontalyTrigger : MonoBehaviour
{
	// Token: 0x0600195D RID: 6493 RVA: 0x0006D17B File Offset: 0x0006B37B
	private void Start()
	{
		this.m_playerTransform = Characters.Sein.Controller.Transform;
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x0006D194 File Offset: 0x0006B394
	private void FixedUpdate()
	{
		float num = Vector3.Distance(this.m_playerTransform.position, base.transform.position);
		RaycastHit raycastHit = default(RaycastHit);
		if (num < this.TriggerDistance)
		{
			if (Physics.Raycast(new Ray(base.transform.position, (this.m_playerTransform.position - base.transform.position).normalized), out raycastHit, this.TriggerDistance) && raycastHit.collider.CompareTag("Player") && (double)Mathf.Abs(Vector2.Dot(base.GetComponent<PlatformMovement>().LocalSpeed.normalized, (this.m_playerTransform.position - base.transform.position).normalized) - 1f) < 0.1)
			{
				if (!this.m_eventTriggered && this.m_time <= 0f)
				{
					base.SendMessage(this.TriggerName);
					this.m_eventTriggered = true;
					this.TriggerDistance += this.DistanceSmoothFactor;
					this.m_time = this.DelayUntilNextTrigger;
				}
				if (this.OnStayName != string.Empty)
				{
					base.SendMessage(this.OnStayName);
				}
			}
		}
		else if (this.m_eventTriggered)
		{
			base.SendMessage(this.EndTriggerName);
			this.m_eventTriggered = false;
			this.TriggerDistance -= this.DistanceSmoothFactor;
		}
		this.m_time -= Time.fixedDeltaTime;
	}

	// Token: 0x040015CA RID: 5578
	public float DelayUntilNextTrigger;

	// Token: 0x040015CB RID: 5579
	public string TriggerName = "OnNearSein";

	// Token: 0x040015CC RID: 5580
	public float TriggerDistance;

	// Token: 0x040015CD RID: 5581
	public bool TriggerOnEventEnd;

	// Token: 0x040015CE RID: 5582
	public string EndTriggerName = "OnNearSeinEnd";

	// Token: 0x040015CF RID: 5583
	public string OnStayName = "OnSeinNearStay";

	// Token: 0x040015D0 RID: 5584
	public float DistanceSmoothFactor = 1.5f;

	// Token: 0x040015D1 RID: 5585
	private float timer;

	// Token: 0x040015D2 RID: 5586
	private Transform m_playerTransform;

	// Token: 0x040015D3 RID: 5587
	private float m_time;

	// Token: 0x040015D4 RID: 5588
	private bool m_eventTriggered;
}
