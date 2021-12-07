using System;
using UnityEngine;

// Token: 0x02000964 RID: 2404
public class DontDestroyOnLoad : MonoBehaviour
{
	// Token: 0x060034D7 RID: 13527 RVA: 0x000DDF3A File Offset: 0x000DC13A
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
	}
}
