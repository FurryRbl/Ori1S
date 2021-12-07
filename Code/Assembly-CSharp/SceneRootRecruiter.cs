using System;
using Core;
using UnityEngine;

// Token: 0x0200072A RID: 1834
public class SceneRootRecruiter : MonoBehaviour
{
	// Token: 0x06002B32 RID: 11058 RVA: 0x000B9378 File Offset: 0x000B7578
	public void Awake()
	{
		if (this.SceneRoot == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (Scenes.Manager)
		{
			Scenes.Manager.Register(this.SceneRoot);
		}
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x040026DD RID: 9949
	public SceneRoot SceneRoot;
}
