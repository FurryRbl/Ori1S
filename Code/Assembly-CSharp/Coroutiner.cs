using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200095C RID: 2396
public class Coroutiner : MonoBehaviour
{
	// Token: 0x1700084C RID: 2124
	// (get) Token: 0x060034BF RID: 13503 RVA: 0x000DD6C9 File Offset: 0x000DB8C9
	public static Coroutiner Instantce
	{
		get
		{
			return Coroutiner.m_instance;
		}
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x000DD6D0 File Offset: 0x000DB8D0
	private void Awake()
	{
		Coroutiner.m_instance = this;
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x000DD6D8 File Offset: 0x000DB8D8
	public void RegisterCoroutine(IEnumerator coroutine)
	{
		this.m_coroutines.Add(coroutine);
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x000DD6E8 File Offset: 0x000DB8E8
	private void Update()
	{
		if (this.m_coroutines.Count == 0)
		{
			return;
		}
		base.StartCoroutine(this.m_coroutines[0]);
		this.m_coroutines.RemoveAt(0);
	}

	// Token: 0x04002F8B RID: 12171
	private static Coroutiner m_instance;

	// Token: 0x04002F8C RID: 12172
	private List<IEnumerator> m_coroutines = new List<IEnumerator>();
}
