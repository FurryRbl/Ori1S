using System;
using Game;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class SeeSeinTrigger : MonoBehaviour
{
	// Token: 0x06001960 RID: 6496 RVA: 0x0006D35C File Offset: 0x0006B55C
	[UberBuildMethod]
	private void ProvideComponents()
	{
		this.m_recievers = base.gameObject.FindComponents<INearSeinReceiver>();
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x0006D36F File Offset: 0x0006B56F
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x0006D37D File Offset: 0x0006B57D
	public void Start()
	{
		this.ProvideComponents();
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x0006D388 File Offset: 0x0006B588
	public bool Raycast(Vector3 origin, Vector3 target, int index = 0)
	{
		if (index == 3)
		{
			return false;
		}
		RaycastHit raycastHit;
		if (!Physics.Linecast(origin, target, out raycastHit, this.LayerMask))
		{
			return true;
		}
		if (raycastHit.collider.CompareTag("Player"))
		{
			return true;
		}
		if (Vector3.Dot(Vector3.up, raycastHit.normal) > Mathf.Cos(1.0471976f))
		{
			Vector3 vector = raycastHit.point + Vector3.up * 5f;
			return !Physics.Linecast(raycastHit.point, vector, out raycastHit, this.LayerMask) && this.Raycast(vector, target, index + 1);
		}
		return false;
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x0006D43C File Offset: 0x0006B63C
	public void FixedUpdate()
	{
		if (Characters.Current == null)
		{
			return;
		}
		float num = this.TriggerDistance + ((!this.m_eventTriggered) ? 0f : this.DistanceSmoothFactor);
		Vector3 vector = this.m_transform.position + this.RayStartOffset;
		Vector2 vector2 = Characters.Current.Position;
		bool flag = MoonMath.Vector.Distance(vector, vector2) < num;
		if (flag)
		{
			this.m_frame++;
			if (this.m_frame % 10 == 0)
			{
				this.m_canSeeSein = this.Raycast(vector, vector2, 0);
			}
		}
		if (flag && this.m_canSeeSein)
		{
			if (!this.m_eventTriggered && this.m_time <= 0f)
			{
				foreach (INearSeinReceiver nearSeinReceiver in this.m_recievers)
				{
					nearSeinReceiver.OnNearSeinEnter();
				}
				this.m_eventTriggered = true;
				this.m_time = this.DelayUntilNextTrigger;
			}
			if (this.m_eventTriggered)
			{
				foreach (INearSeinReceiver nearSeinReceiver2 in this.m_recievers)
				{
					nearSeinReceiver2.OnSeinNearStay();
				}
			}
		}
		else if (this.m_eventTriggered)
		{
			this.m_eventTriggered = false;
			foreach (INearSeinReceiver nearSeinReceiver3 in this.m_recievers)
			{
				nearSeinReceiver3.OnNearSeinExit();
			}
		}
		this.m_time -= Time.fixedDeltaTime;
	}

	// Token: 0x040015D5 RID: 5589
	public float DelayUntilNextTrigger;

	// Token: 0x040015D6 RID: 5590
	public float TriggerDistance;

	// Token: 0x040015D7 RID: 5591
	public float DistanceSmoothFactor = 1.5f;

	// Token: 0x040015D8 RID: 5592
	public LayerMask LayerMask = -1;

	// Token: 0x040015D9 RID: 5593
	private float m_time;

	// Token: 0x040015DA RID: 5594
	private bool m_eventTriggered;

	// Token: 0x040015DB RID: 5595
	[HideInInspector]
	[SerializeField]
	private Component[] m_recievers;

	// Token: 0x040015DC RID: 5596
	public Vector2 RayStartOffset;

	// Token: 0x040015DD RID: 5597
	private Transform m_transform;

	// Token: 0x040015DE RID: 5598
	private bool m_canSeeSein;

	// Token: 0x040015DF RID: 5599
	private Ray m_lastRay;

	// Token: 0x040015E0 RID: 5600
	private int m_frame;
}
