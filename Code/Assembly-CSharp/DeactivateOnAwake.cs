using System;
using UnityEngine;

// Token: 0x02000939 RID: 2361
public class DeactivateOnAwake : MonoBehaviour
{
	// Token: 0x06003436 RID: 13366 RVA: 0x000DB9CA File Offset: 0x000D9BCA
	private void Awake()
	{
		base.gameObject.SetActive(false);
	}
}
