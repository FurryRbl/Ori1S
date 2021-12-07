using System;
using Core;
using UnityEngine;

// Token: 0x02000475 RID: 1141
public class DestroyWhenOutsideActiveBoundaries : MonoBehaviour
{
	// Token: 0x06001F68 RID: 8040 RVA: 0x0008A5CC File Offset: 0x000887CC
	public void FixedUpdate()
	{
		this.m_index++;
		if (this.m_index == 5)
		{
			this.m_index = 0;
			if (!Scenes.Manager.SceneVisibleAtPosition(base.transform.position))
			{
				InstantiateUtility.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001B09 RID: 6921
	private int m_index;
}
