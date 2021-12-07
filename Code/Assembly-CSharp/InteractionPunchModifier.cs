using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FA RID: 2042
[UberShaderOrder(UberShaderOrder.InteractionPunch)]
[UberShaderCategory(UberShaderCategory.Interaction)]
[ExecuteInEditMode]
[CustomShaderModifier("Interaction Punch Modifier")]
public class InteractionPunchModifier : UberInteractionModifier
{
	// Token: 0x1700078E RID: 1934
	// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000C7108 File Offset: 0x000C5308
	protected override float OverrideDifficulty
	{
		get
		{
			return 0.5f;
		}
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x000C710F File Offset: 0x000C530F
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.Strength *= strength;
	}

	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000C711F File Offset: 0x000C531F
	public override string InteractionName
	{
		get
		{
			return InteractionPunchModifier.s_name;
		}
	}

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000C7126 File Offset: 0x000C5326
	protected override UberInteractionManager.PropertyIDCache PropertyCache
	{
		get
		{
			return UberInteractionManager.GetCachedPropertyID(ref InteractionPunchModifier.s_cache, InteractionPunchModifier.s_name);
		}
	}

	// Token: 0x06002EFE RID: 12030 RVA: 0x000C7138 File Offset: 0x000C5338
	public override IEnumerable<string> GetKeywordsForShader()
	{
		foreach (string key in base.GetKeywordsForShader())
		{
			yield return key;
		}
		if (base.HasCageMesh)
		{
			yield return "_CustomMesh";
		}
		yield break;
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x000C715B File Offset: 0x000C535B
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002F00 RID: 12032 RVA: 0x000C7160 File Offset: 0x000C5360
	protected override float GetStrength(Vector3 velocity, Vector4 strengthVal)
	{
		float num = Mathf.Pow(velocity.magnitude, UberInteractionManager.Instance.MagnitudePower);
		return num * this.Strength * strengthVal.x * 0.1f;
	}

	// Token: 0x06002F01 RID: 12033 RVA: 0x000C719C File Offset: 0x000C539C
	public override void SetProperties()
	{
		this.Mask.Set("_InteractionPunchDistortMask", base.AttachedToShaderBlock);
		this.Mask.IsVertexTexture = true;
		this.Settings.Set("_InteractionSettingsPunch", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A1D RID: 10781
	public UberShaderTexture Mask = new UberShaderTexture();

	// Token: 0x04002A1E RID: 10782
	public float Strength = 1f;

	// Token: 0x04002A1F RID: 10783
	[UberShaderVectorDisplay("Waviness x", "Waviness y", "Faloff Size", "Speed")]
	public UberShaderVector Settings = new UberShaderVector(0.1f, 0.1f, 3f, 1f);

	// Token: 0x04002A20 RID: 10784
	private static string s_name = "Punch";

	// Token: 0x04002A21 RID: 10785
	private static UberInteractionManager.PropertyIDCache s_cache;
}
