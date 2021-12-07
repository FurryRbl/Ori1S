using System;
using UnityEngine;

// Token: 0x02000960 RID: 2400
[ExecuteInEditMode]
public class DisableScale : MonoBehaviour
{
	// Token: 0x060034CC RID: 13516 RVA: 0x000DD81A File Offset: 0x000DBA1A
	private void Start()
	{
		if (Application.isPlaying)
		{
			UnityEngine.Object.DestroyObject(this);
		}
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x000DD82C File Offset: 0x000DBA2C
	private void Update()
	{
		base.transform.localScale = Vector3.one;
	}
}
