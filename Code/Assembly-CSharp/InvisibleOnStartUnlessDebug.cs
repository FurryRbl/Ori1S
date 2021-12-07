using System;
using UnityEngine;

// Token: 0x0200093B RID: 2363
public class InvisibleOnStartUnlessDebug : MonoBehaviour, IDynamicGraphic
{
	// Token: 0x0600343A RID: 13370 RVA: 0x000DB9FF File Offset: 0x000D9BFF
	private void Start()
	{
		if (base.GetComponent<Renderer>())
		{
			base.GetComponent<Renderer>().enabled = false;
		}
	}
}
