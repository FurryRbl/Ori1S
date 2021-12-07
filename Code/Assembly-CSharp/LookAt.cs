using System;
using UnityEngine;

// Token: 0x020007A3 RID: 1955
[ExecuteInEditMode]
public class LookAt : MonoBehaviour
{
	// Token: 0x06002D5D RID: 11613 RVA: 0x000C1ED9 File Offset: 0x000C00D9
	private void Start()
	{
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x000C1EDC File Offset: 0x000C00DC
	private void Update()
	{
		base.transform.LookAt(this.Target);
		Color.white.a = 0.3f;
	}

	// Token: 0x040028E8 RID: 10472
	public Transform Target;
}
