using System;
using UnityEngine;

// Token: 0x020007A2 RID: 1954
public class Counter : MonoBehaviour
{
	// Token: 0x06002D5B RID: 11611 RVA: 0x000C1EB8 File Offset: 0x000C00B8
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawTextFilled(base.transform, this.Value.ToString(), false);
	}

	// Token: 0x040028E7 RID: 10471
	public float Value;
}
