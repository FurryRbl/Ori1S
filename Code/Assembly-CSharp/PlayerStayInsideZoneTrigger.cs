using System;
using Game;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class PlayerStayInsideZoneTrigger : MonoBehaviour
{
	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x06001954 RID: 6484 RVA: 0x0006CF58 File Offset: 0x0006B158
	public bool IsInside
	{
		get
		{
			return !(Characters.Sein == null) && this.m_bounds.Contains(Characters.Sein.Position);
		}
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x0006CF8C File Offset: 0x0006B18C
	public void FixedUpdate()
	{
		if (this.IsInside && this.m_wasInside && !this.m_hasPlayed)
		{
			this.m_time += Time.deltaTime;
			if (this.m_time >= this.TimeToTake)
			{
				this.m_time = 0f;
				this.m_hasPlayed = true;
				if (this.Action)
				{
					this.Action.Perform(null);
				}
			}
		}
		else
		{
			this.m_time = 0f;
			this.m_hasPlayed = false;
		}
		this.m_wasInside = this.IsInside;
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x0006D02E File Offset: 0x0006B22E
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x040015C1 RID: 5569
	private bool m_wasInside;

	// Token: 0x040015C2 RID: 5570
	private Rect m_bounds;

	// Token: 0x040015C3 RID: 5571
	private bool m_hasPlayed;

	// Token: 0x040015C4 RID: 5572
	public ActionMethod Action;

	// Token: 0x040015C5 RID: 5573
	public float TimeToTake;

	// Token: 0x040015C6 RID: 5574
	private float m_time;
}
