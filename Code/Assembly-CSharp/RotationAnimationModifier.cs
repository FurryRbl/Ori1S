using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D2 RID: 2002
[UberShaderOrder(UberShaderOrder.RotationAnimator)]
[CustomShaderModifier("Rotation Animator")]
[UberShaderCategory(UberShaderCategory.Animation)]
public class RotationAnimationModifier : UberShaderModifier, IAnimatedGraphic, IAnimationVertex
{
	// Token: 0x06002DF3 RID: 11763 RVA: 0x000C3C6C File Offset: 0x000C1E6C
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.RotationCurve.CurveScale *= strength;
		this.RotationCurve.TimeScale *= speed;
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x000C3CA0 File Offset: 0x000C1EA0
	public override IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield return "_RotationCurve";
		yield break;
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x000C3CBC File Offset: 0x000C1EBC
	public override void SetProperties()
	{
		this.RotationCurve.Set("_RotationCurve", base.AttachedToShaderBlock);
		this.Offset.Mode = UberShaderVector.ScalingMode.Offset;
		this.Offset.Set("_Offset", base.AttachedToShaderBlock);
		this.Mask.Set("_RotationAnimMask", base.AttachedToShaderBlock);
		this.Mask.IsVertexTexture = true;
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x000C3D23 File Offset: 0x000C1F23
	public override void Randomize()
	{
		this.RotationCurve.TimeOffset = UnityEngine.Random.value;
	}

	// Token: 0x0400297B RID: 10619
	public UberShaderCurve RotationCurve = new UberShaderCurve(0f);

	// Token: 0x0400297C RID: 10620
	public UberShaderTexture Mask = new UberShaderTexture();

	// Token: 0x0400297D RID: 10621
	[UberShaderVectorDisplay("Offset", "Unused")]
	public UberShaderVector Offset = new UberShaderVector();
}
