using System;
using UnityEngine;

// Token: 0x020007BF RID: 1983
[ExecuteInEditMode]
public class CageStructureToolFill : MonoBehaviour, IStrippable
{
	// Token: 0x06002DB1 RID: 11697 RVA: 0x000C2FA0 File Offset: 0x000C11A0
	public void OnModified()
	{
		this.UpdateMesh();
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x000C2FA8 File Offset: 0x000C11A8
	public void UpdateMesh()
	{
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x000C2FAA File Offset: 0x000C11AA
	public bool DoStrip()
	{
		return true;
	}
}
