using System;
using UnityEngine;

// Token: 0x0200064C RID: 1612
public class LightTorchZone : MonoBehaviour
{
	// Token: 0x06002774 RID: 10100 RVA: 0x000ABD52 File Offset: 0x000A9F52
	public void Awake()
	{
		LightTorchZone.Instance = this;
	}

	// Token: 0x06002775 RID: 10101 RVA: 0x000ABD5A File Offset: 0x000A9F5A
	public void OnDestroy()
	{
		LightTorchZone.Instance = null;
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x000ABD62 File Offset: 0x000A9F62
	public static bool IsInside(Vector3 position)
	{
		return LightTorchZone.Instance && LightTorchZone.Instance.CageStructureTool.FindFaceAtPositionFaster(position) != null;
	}

	// Token: 0x04002213 RID: 8723
	public static LightTorchZone Instance;

	// Token: 0x04002214 RID: 8724
	public CageStructureTool CageStructureTool;
}
