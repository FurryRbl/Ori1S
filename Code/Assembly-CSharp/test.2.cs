using System;
using UnityEngine;

// Token: 0x020009EB RID: 2539
public class test : MonoBehaviour
{
	// Token: 0x0600372B RID: 14123 RVA: 0x000E77EF File Offset: 0x000E59EF
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			Application.LoadLevel(1);
		}
	}
}
