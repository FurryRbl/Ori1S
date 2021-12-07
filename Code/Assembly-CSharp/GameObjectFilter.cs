using System;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class GameObjectFilter : MonoBehaviour
{
	// Token: 0x060018B2 RID: 6322 RVA: 0x00069DC8 File Offset: 0x00067FC8
	public virtual bool Valid(GameObject gameObject)
	{
		return gameObject && gameObject.activeInHierarchy;
	}
}
