using System;
using Game;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class PlayerCollisionTrigger : Trigger
{
	// Token: 0x0600194C RID: 6476 RVA: 0x0006CDEF File Offset: 0x0006AFEF
	public void OnTriggerEnter(Collider collider)
	{
		if (this.UseTrigger && Characters.Current != null && collider.gameObject == Characters.Current.GameObject)
		{
			base.DoTrigger(true);
		}
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x0006CE27 File Offset: 0x0006B027
	public new void Awake()
	{
		base.Awake();
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x0006CE48 File Offset: 0x0006B048
	public void FixedUpdate()
	{
		if (this.UseTrigger)
		{
			return;
		}
		if (this.m_position != base.transform.position)
		{
			this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
			this.m_position = base.transform.position;
		}
		if (Characters.Current as Component)
		{
			if (Utility.LineInBox(this.m_bounds, Characters.Current.Position, -Characters.Current.Speed * Time.deltaTime))
			{
				if (!this.m_hasCollided)
				{
					this.m_hasCollided = true;
					base.DoTrigger(true);
				}
			}
			else
			{
				this.m_hasCollided = false;
			}
		}
	}

	// Token: 0x040015BD RID: 5565
	public bool UseTrigger;

	// Token: 0x040015BE RID: 5566
	private Rect m_bounds;

	// Token: 0x040015BF RID: 5567
	private bool m_hasCollided;

	// Token: 0x040015C0 RID: 5568
	private Vector3 m_position;
}
