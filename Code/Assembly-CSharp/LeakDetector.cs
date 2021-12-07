using System;
using System.IO;
using UnityEngine;

// Token: 0x0200049A RID: 1178
public class LeakDetector : MonoBehaviour
{
	// Token: 0x06001FDF RID: 8159 RVA: 0x0008BDE1 File Offset: 0x00089FE1
	private void Start()
	{
		if (UnityEngine.Object.FindObjectsOfType(typeof(LeakDetector)).Length > 1)
		{
			UnityEngine.Object.DestroyImmediate(this);
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x0008BE06 File Offset: 0x0008A006
	private void Update()
	{
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x0008BE08 File Offset: 0x0008A008
	public static string GetTexturePath(string textureName)
	{
		if (LeakDetector.m_assetPaths == null)
		{
			LeakDetector.m_assetPaths = Directory.GetFiles(Directory.GetCurrentDirectory());
		}
		int num = 0;
		foreach (string text in LeakDetector.m_assetPaths)
		{
			if (text.Contains(textureName))
			{
				num++;
			}
		}
		if (num > 1)
		{
		}
		return string.Empty;
	}

	// Token: 0x04001B74 RID: 7028
	private int ObjectsCount;

	// Token: 0x04001B75 RID: 7029
	private int TextureCount;

	// Token: 0x04001B76 RID: 7030
	private int AudioClipCount;

	// Token: 0x04001B77 RID: 7031
	private int MeshCount;

	// Token: 0x04001B78 RID: 7032
	private int MaterialCount;

	// Token: 0x04001B79 RID: 7033
	private int GameObjectCount;

	// Token: 0x04001B7A RID: 7034
	private int ComponentCount;

	// Token: 0x04001B7B RID: 7035
	private float m_nextSample;

	// Token: 0x04001B7C RID: 7036
	private float m_sampleInterval = 0.2f;

	// Token: 0x04001B7D RID: 7037
	public static string[] m_assetPaths;
}
