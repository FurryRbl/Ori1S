using System;
using Game;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class PlayerCollisionStayTrigger : Trigger
{
	// Token: 0x06001949 RID: 6473 RVA: 0x0006CD6D File Offset: 0x0006AF6D
	public new void Awake()
	{
		base.Awake();
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x0006CD8C File Offset: 0x0006AF8C
	public void FixedUpdate()
	{
		if (Characters.Current as Component && Utility.LineInBox(this.m_bounds, Characters.Current.Position, -Characters.Current.Speed * Time.deltaTime))
		{
			base.DoTrigger(true);
		}
	}

	// Token: 0x040015BC RID: 5564
	private Rect m_bounds;
}
