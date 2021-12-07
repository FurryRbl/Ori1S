using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007F5 RID: 2037
[ExecuteInEditMode]
[UberShaderCategory(UberShaderCategory.Interaction)]
[UberShaderOrder(UberShaderOrder.InteractionLight)]
[CustomShaderModifier("Interaction Light Modifier")]
public class InteractionLightModifier : UberInteractionModifier
{
	// Token: 0x06002EC1 RID: 11969 RVA: 0x000C626C File Offset: 0x000C446C
	private void Reset()
	{
		this.CurveType = UberInteractionManager.InteractionCurveType.Light;
	}

	// Token: 0x06002EC2 RID: 11970 RVA: 0x000C6275 File Offset: 0x000C4475
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.Strength *= strength;
	}

	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000C6285 File Offset: 0x000C4485
	public override string InteractionName
	{
		get
		{
			return InteractionLightModifier.s_name;
		}
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x000C628C File Offset: 0x000C448C
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

	// Token: 0x06002EC5 RID: 11973 RVA: 0x000C62AF File Offset: 0x000C44AF
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x000C62B4 File Offset: 0x000C44B4
	protected override float GetStrength(Vector3 velocity, Vector4 strengthVal)
	{
		float num = Mathf.Pow(velocity.magnitude, UberInteractionManager.Instance.MagnitudePower);
		return strengthVal.y * this.Strength * num;
	}

	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000C62E8 File Offset: 0x000C44E8
	protected override UberInteractionManager.PropertyIDCache PropertyCache
	{
		get
		{
			return UberInteractionManager.GetCachedPropertyID(ref InteractionLightModifier.s_cache, InteractionLightModifier.s_name);
		}
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x000C62FC File Offset: 0x000C44FC
	public override void SetProperties()
	{
		this.LightColor.Set("_InteractionColorLight", base.AttachedToShaderBlock);
		this.Tint.Set("_InteractionTintLight", base.AttachedToShaderBlock);
		this.Settings.Set("_InteractionSettingsLight", base.AttachedToShaderBlock);
		this.LightMask.Set("_InteractionLightMask", base.AttachedToShaderBlock);
		this.LightMask.IsVertexTexture = true;
	}

	// Token: 0x040029F3 RID: 10739
	public UberShaderTexture LightMask = new UberShaderTexture();

	// Token: 0x040029F4 RID: 10740
	public UberShaderColor LightColor = new UberShaderColor(Color.white);

	// Token: 0x040029F5 RID: 10741
	public UberShaderColor Tint = new UberShaderColor(Color.gray);

	// Token: 0x040029F6 RID: 10742
	public float Strength = 1f;

	// Token: 0x040029F7 RID: 10743
	[UberShaderVectorDisplay("Waviness X", "Waviness Y", "Faloff", "Speed")]
	public UberShaderVector Settings = new UberShaderVector(2f, 2f, 6f, 3f);

	// Token: 0x040029F8 RID: 10744
	private static string s_name = "Light";

	// Token: 0x040029F9 RID: 10745
	private static UberInteractionManager.PropertyIDCache s_cache;
}
