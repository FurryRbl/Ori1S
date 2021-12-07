using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000717 RID: 1815
public class SceneCollection : ScriptableObject
{
	// Token: 0x06002B0B RID: 11019 RVA: 0x000B8260 File Offset: 0x000B6460
	[ContextMenu("Cleanup")]
	public void Cleanup()
	{
		HashSet<SceneMetaData> hashSet = new HashSet<SceneMetaData>();
		foreach (SceneMetaData sceneMetaData in this.SceneMetaDatas)
		{
			if (sceneMetaData)
			{
				hashSet.Add(sceneMetaData);
			}
		}
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x000B82CC File Offset: 0x000B64CC
	public SceneMetaData FindMetaData(string name)
	{
		foreach (SceneMetaData sceneMetaData in this.SceneMetaDatas)
		{
			if (!(sceneMetaData == null))
			{
				if (sceneMetaData.SceneName == name)
				{
					return sceneMetaData;
				}
			}
		}
		return null;
	}

	// Token: 0x04002658 RID: 9816
	public List<SceneMetaData> SceneMetaDatas = new List<SceneMetaData>();
}
