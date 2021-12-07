using System;
using UnityEngine;

// Token: 0x0200078D RID: 1933
public class UberExplosionActor : MonoBehaviour, IPooled
{
	// Token: 0x06002CD5 RID: 11477 RVA: 0x000C00FF File Offset: 0x000BE2FF
	private void Start()
	{
		if (this.ExplodeAtStart)
		{
			this.ExplodeThis();
		}
	}

	// Token: 0x06002CD6 RID: 11478 RVA: 0x000C0114 File Offset: 0x000BE314
	public void ExplodeThis()
	{
		UberInteractionManager.Instance.Explode(base.transform.position, this.OutwardSpeed, this.ExplodeStrength, this.Radius);
	}

	// Token: 0x06002CD7 RID: 11479 RVA: 0x000C0148 File Offset: 0x000BE348
	public void OnPoolSpawned()
	{
	}

	// Token: 0x04002888 RID: 10376
	public bool ExplodeAtStart = true;

	// Token: 0x04002889 RID: 10377
	public float OutwardSpeed = 100f;

	// Token: 0x0400288A RID: 10378
	[UberShaderVectorDisplay("Rotation Str", "Light Str", "Water Str", "Punch Str")]
	public Vector4 ExplodeStrength = Vector4.one;

	// Token: 0x0400288B RID: 10379
	public float Radius = 10f;
}
