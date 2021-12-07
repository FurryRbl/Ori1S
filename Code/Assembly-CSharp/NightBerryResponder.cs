using System;
using Game;
using UnityEngine;

// Token: 0x020008FB RID: 2299
public class NightBerryResponder : MonoBehaviour
{
	// Token: 0x06003324 RID: 13092 RVA: 0x000D7A58 File Offset: 0x000D5C58
	private void FixedUpdate()
	{
		NightBerry nightBerry = Items.NightBerry;
		if (!nightBerry)
		{
			return;
		}
		this.m_frame++;
		if (this.m_frame % 2 == 0)
		{
			if (MoonMath.Vector.Distance(base.transform.position, nightBerry.Position) < nightBerry.SafeFromDamageRadius)
			{
				if (!this.m_isInRadius)
				{
					this.m_isInRadius = true;
					this.OnEnterNightBerryAura.Perform(null);
				}
			}
			else if (this.m_isInRadius)
			{
				this.m_isInRadius = false;
				this.OnExitNightBerryAura.Perform(null);
			}
		}
	}

	// Token: 0x04002E23 RID: 11811
	public ActionMethod OnEnterNightBerryAura;

	// Token: 0x04002E24 RID: 11812
	public ActionMethod OnExitNightBerryAura;

	// Token: 0x04002E25 RID: 11813
	private bool m_isInRadius;

	// Token: 0x04002E26 RID: 11814
	private int m_frame;
}
