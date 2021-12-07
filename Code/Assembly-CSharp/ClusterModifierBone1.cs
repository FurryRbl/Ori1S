using System;
using UnityEngine;

// Token: 0x020007EB RID: 2027
[CustomShaderModifier("Cluster Animation Bone 1")]
[UberShaderOrder(UberShaderOrder.ClusterAnimBone1)]
[UberShaderCategory(UberShaderCategory.Animation)]
[ExecuteInEditMode]
public class ClusterModifierBone1 : ClusterModifier
{
	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000C556F File Offset: 0x000C376F
	protected override string BoneName
	{
		get
		{
			return "Bone1";
		}
	}
}
