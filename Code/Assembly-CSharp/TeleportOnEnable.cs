using System;
using UnityEngine;

// Token: 0x02000944 RID: 2372
public class TeleportOnEnable : MonoBehaviour
{
	// Token: 0x06003458 RID: 13400 RVA: 0x000DBFA4 File Offset: 0x000DA1A4
	public void OnEnable()
	{
		base.transform.position = this.TeleportTarget.position;
	}

	// Token: 0x04002F40 RID: 12096
	public Transform TeleportTarget;
}
