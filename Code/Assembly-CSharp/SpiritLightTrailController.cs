using System;
using UnityEngine;

// Token: 0x0200065A RID: 1626
public class SpiritLightTrailController : MonoBehaviour
{
	// Token: 0x060027AF RID: 10159 RVA: 0x000AC89C File Offset: 0x000AAA9C
	private void Update()
	{
		bool enabled = SpiritLightDarknessZone.IsInsideDarknessZone(base.transform.position);
		this.UberTrail.enabled = enabled;
	}

	// Token: 0x0400224F RID: 8783
	public UberGhostTrail UberTrail;
}
