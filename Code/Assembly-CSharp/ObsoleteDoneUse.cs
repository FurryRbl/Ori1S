using System;
using UnityEngine;

// Token: 0x02000988 RID: 2440
public class ObsoleteDoneUse : MonoBehaviour
{
	// Token: 0x06003563 RID: 13667 RVA: 0x000DFE21 File Offset: 0x000DE021
	private void Awake()
	{
		Application.Quit();
	}

	// Token: 0x04002FFA RID: 12282
	public string Message;
}
