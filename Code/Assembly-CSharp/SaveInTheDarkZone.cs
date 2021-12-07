using System;
using UnityEngine;

// Token: 0x02000471 RID: 1137
public class SaveInTheDarkZone : MonoBehaviour
{
	// Token: 0x06001F56 RID: 8022 RVA: 0x0008A45E File Offset: 0x0008865E
	public void Awake()
	{
		SaveInTheDarkZone.Instance = this;
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0008A466 File Offset: 0x00088666
	public void OnDestroy()
	{
		SaveInTheDarkZone.Instance = null;
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x0008A46E File Offset: 0x0008866E
	public static bool IsInside(Vector3 position)
	{
		return SaveInTheDarkZone.Instance && SaveInTheDarkZone.Instance.CageStructureTool.FindFaceAtPositionFaster(position) != null;
	}

	// Token: 0x04001B05 RID: 6917
	public static SaveInTheDarkZone Instance;

	// Token: 0x04001B06 RID: 6918
	public CageStructureTool CageStructureTool;
}
