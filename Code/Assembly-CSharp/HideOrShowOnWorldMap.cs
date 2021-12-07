using System;
using Game;
using UnityEngine;

// Token: 0x02000887 RID: 2183
public class HideOrShowOnWorldMap : MonoBehaviour
{
	// Token: 0x06003127 RID: 12583 RVA: 0x000D16EC File Offset: 0x000CF8EC
	public void Start()
	{
		Color color = base.GetComponent<Renderer>().material.GetColor(ShaderProperties.Color);
		if (World.CurrentArea.IsHidden(base.transform.position + this.Offset))
		{
			color.a = 0f;
		}
		base.GetComponent<Renderer>().material.SetColor(ShaderProperties.Color, color);
	}

	// Token: 0x06003128 RID: 12584 RVA: 0x000D175C File Offset: 0x000CF95C
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position + base.transform.position, 0.1f);
	}

	// Token: 0x04002C7A RID: 11386
	public Vector2 Offset;
}
