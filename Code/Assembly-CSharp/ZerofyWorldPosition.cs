using System;
using UnityEngine;

// Token: 0x020009AB RID: 2475
[ExecuteInEditMode]
public class ZerofyWorldPosition : MonoBehaviour
{
	// Token: 0x060035E7 RID: 13799 RVA: 0x000E247C File Offset: 0x000E067C
	private void Start()
	{
		base.transform.position = new Vector3(0f, 0f, (!this.ShouldAffectZ) ? base.transform.position.z : 0f);
	}

	// Token: 0x04003081 RID: 12417
	public bool ShouldAffectZ = true;
}
