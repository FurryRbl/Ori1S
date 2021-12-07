using System;
using UnityEngine;

// Token: 0x0200094D RID: 2381
public class StreamingSoundsDisplay : MonoBehaviour
{
	// Token: 0x06003473 RID: 13427 RVA: 0x000DC549 File Offset: 0x000DA749
	private void Awake()
	{
		this.m_guidText = base.GetComponent<GUIText>();
	}

	// Token: 0x06003474 RID: 13428 RVA: 0x000DC557 File Offset: 0x000DA757
	private void Update()
	{
	}

	// Token: 0x04002F55 RID: 12117
	private GUIText m_guidText;
}
