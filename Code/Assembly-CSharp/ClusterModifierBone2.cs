using System;
using UnityEngine;

// Token: 0x020007EC RID: 2028
[ExecuteInEditMode]
[UberShaderOrder(UberShaderOrder.ClusterAnimBone2)]
[UberShaderCategory(UberShaderCategory.Animation)]
[CustomShaderModifier("Cluster Animation Bone 2")]
public class ClusterModifierBone2 : ClusterModifier
{
	// Token: 0x1700077A RID: 1914
	// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000C557E File Offset: 0x000C377E
	protected override string BoneName
	{
		get
		{
			return "Bone2";
		}
	}
}
