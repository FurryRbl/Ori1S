using System;
using Game;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class CollisionStayTrigger : MonoBehaviour
{
	// Token: 0x060018EA RID: 6378 RVA: 0x0006A5A8 File Offset: 0x000687A8
	public void Awake()
	{
		this.m_hasCollider = base.GetComponent<Collider>();
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x0006A5DC File Offset: 0x000687DC
	public void FixedUpdate()
	{
		if (this.m_hasCollider)
		{
			return;
		}
		if (Characters.Current as Component)
		{
			if (this.m_bounds.Contains(Characters.Current.Position))
			{
				if (!this.m_isInside)
				{
					this.TurnOn();
				}
			}
			else if (this.m_isInside)
			{
				this.TurnOff();
			}
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x0006A64C File Offset: 0x0006884C
	public void TurnOn()
	{
		this.m_isInside = true;
		foreach (LegacyAnimator legacyAnimator in this.Animators)
		{
			if (legacyAnimator != null)
			{
				legacyAnimator.ContinueForward();
			}
		}
		foreach (BaseAnimator baseAnimator in this.BaseAnimators)
		{
			if (baseAnimator != null)
			{
				baseAnimator.AnimatorDriver.ContinueForward();
			}
		}
		if (this.EnterSound)
		{
			this.EnterSound.Play();
		}
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x0006A6F4 File Offset: 0x000688F4
	public void TurnOff()
	{
		this.m_isInside = false;
		foreach (LegacyAnimator legacyAnimator in this.Animators)
		{
			if (legacyAnimator != null)
			{
				legacyAnimator.ContinueBackward();
			}
		}
		foreach (BaseAnimator baseAnimator in this.BaseAnimators)
		{
			if (baseAnimator != null)
			{
				baseAnimator.AnimatorDriver.ContinueBackwards();
			}
		}
		if (this.ExitSound)
		{
			this.ExitSound.Play();
		}
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x0006A79C File Offset: 0x0006899C
	public void OnTriggerEnter(Collider collider)
	{
		if (this.Filter.Valid(collider.gameObject) && this.Condition.Validate(null))
		{
			this.TurnOn();
		}
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x0006A7CB File Offset: 0x000689CB
	public void OnTriggerExit(Collider collider)
	{
		if (this.Filter.Valid(collider.gameObject))
		{
			this.TurnOff();
		}
	}

	// Token: 0x04001556 RID: 5462
	public GameObjectFilter Filter;

	// Token: 0x04001557 RID: 5463
	public Condition Condition;

	// Token: 0x04001558 RID: 5464
	public LegacyAnimator[] Animators = new LegacyAnimator[0];

	// Token: 0x04001559 RID: 5465
	public BaseAnimator[] BaseAnimators = new BaseAnimator[0];

	// Token: 0x0400155A RID: 5466
	public SoundSource EnterSound;

	// Token: 0x0400155B RID: 5467
	public SoundSource ExitSound;

	// Token: 0x0400155C RID: 5468
	private Rect m_bounds;

	// Token: 0x0400155D RID: 5469
	private bool m_hasCollider;

	// Token: 0x0400155E RID: 5470
	private bool m_isInside;
}
