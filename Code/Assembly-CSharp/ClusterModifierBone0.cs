using System;
using UnityEngine;

// Token: 0x020007EA RID: 2026
[UberShaderCategory(UberShaderCategory.Animation)]
[CustomShaderModifier("Cluster Animation Bone 0")]
[ExecuteInEditMode]
[UberShaderOrder(UberShaderOrder.ClusterAnimBone0)]
public class ClusterModifierBone0 : ClusterModifier
{
	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000C555D File Offset: 0x000C375D
	protected override string BoneName
	{
		get
		{
			return "Bone0";
		}
	}

	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000C5564 File Offset: 0x000C3764
	protected override bool ControlMask
	{
		get
		{
			return true;
		}
	}
}
