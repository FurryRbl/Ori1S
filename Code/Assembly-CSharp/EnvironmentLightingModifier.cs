using System;
using UnityEngine;

// Token: 0x020007ED RID: 2029
[UberShaderOrder(UberShaderOrder.CharacterLighting)]
[CustomShaderModifier("Environment Lighting")]
[ExecuteInEditMode]
[UberShaderCategory(UberShaderCategory.Lighting)]
public class EnvironmentLightingModifier : EnvironmentShadingModifier
{
	// Token: 0x06002E8F RID: 11919 RVA: 0x000C5619 File Offset: 0x000C3819
	private void OnEnable()
	{
		this.m_randomOffset = UnityEngine.Random.value;
	}

	// Token: 0x06002E90 RID: 11920 RVA: 0x000C5628 File Offset: 0x000C3828
	protected override void BindNow(EnvironmentLight characterLight, int index, bool curLight)
	{
		characterLight.BindLightToMaterial(base.BindMaterial, this.m_randomOffset, index);
		if (index == 0 && curLight)
		{
			EnvironmentLightTexture mainLight = characterLight.MainLight;
			EnvironmentLightTexture bounceLight = characterLight.BounceLight;
			Vector4 vector = mainLight.GetTurbVec(this.m_randomOffset);
			Vector4 vector2 = mainLight.GetTurbScaleVec();
			Vector4 vector3 = bounceLight.GetTurbVec(this.m_randomOffset);
			Vector4 vector4 = bounceLight.GetTurbScaleVec();
			if (this.FadeLight != null)
			{
				EnvironmentLightTexture mainLight2 = this.FadeLight.MainLight;
				EnvironmentLightTexture bounceLight2 = this.FadeLight.BounceLight;
				vector = Vector4.Lerp(vector, mainLight2.GetTurbVec(this.m_randomOffset), base.CurFade);
				vector2 = Vector4.Lerp(vector2, mainLight2.GetTurbScaleVec(), base.CurFade);
				vector3 = Vector4.Lerp(vector3, bounceLight2.GetTurbVec(this.m_randomOffset), base.CurFade);
				vector4 = Vector4.Lerp(vector4, bounceLight2.GetTurbScaleVec(), base.CurFade);
			}
			base.BindMaterial.SetVector(EnvironmentLightingModifier.s_turbSetMain, vector);
			base.BindMaterial.SetVector(EnvironmentLightingModifier.s_turbSetBounce, vector3);
			base.BindMaterial.SetVector(EnvironmentLightingModifier.s_turbScaleMain, vector2);
			base.BindMaterial.SetVector(EnvironmentLightingModifier.s_turbScaleBounce, vector4);
		}
	}

	// Token: 0x06002E91 RID: 11921 RVA: 0x000C575D File Offset: 0x000C395D
	protected override void UpdateBaseBind()
	{
		this.UpdateFilterBind();
	}

	// Token: 0x06002E92 RID: 11922 RVA: 0x000C5765 File Offset: 0x000C3965
	protected override void ClearBind(int num)
	{
		EnvironmentLight.ClearBind(base.BindMaterial, num);
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x000C5774 File Offset: 0x000C3974
	private void UpdateFilterBind()
	{
		if (base.Filter.sharedMesh == null)
		{
			return;
		}
		Bounds bounds = base.Filter.sharedMesh.bounds;
		Vector3 lossyScale = base.Filter.transform.lossyScale;
		Vector4 zero = Vector4.zero;
		zero.x = bounds.center.x * lossyScale.x;
		zero.y = bounds.center.y * lossyScale.y;
		zero.z = 1f / Mathf.Abs(bounds.size.x * lossyScale.x);
		zero.w = 1f / Mathf.Abs(bounds.size.y * lossyScale.y);
		base.BindMaterial.SetVector("_CharacterLightSettings", zero);
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x000C5862 File Offset: 0x000C3A62
	public override string GetBaseShaderProperties()
	{
		return base.ShaderlabString("_CharacterLightSettings", "Vector", "(0, 0, 0, 0)");
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x000C587C File Offset: 0x000C3A7C
	public override void SetProperties()
	{
		this.MainStrength.Set("_CharLightStrengthMain", base.AttachedToShaderBlock);
		this.BounceStrength.Set("_CharLightStrengthBounce", base.AttachedToShaderBlock);
	}

	// Token: 0x040029C7 RID: 10695
	public UberShaderFloat MainStrength = new UberShaderFloat(1f);

	// Token: 0x040029C8 RID: 10696
	public UberShaderFloat BounceStrength = new UberShaderFloat(1f);

	// Token: 0x040029C9 RID: 10697
	private float m_randomOffset;

	// Token: 0x040029CA RID: 10698
	private static string s_turbSetMain = EnvironmentLight.MainLightName + "TurbSettings";

	// Token: 0x040029CB RID: 10699
	private static string s_turbSetBounce = EnvironmentLight.BounceLightName + "TurbSettings";

	// Token: 0x040029CC RID: 10700
	private static string s_turbScaleMain = EnvironmentLight.MainLightName + "TurbScale";

	// Token: 0x040029CD RID: 10701
	private static string s_turbScaleBounce = EnvironmentLight.BounceLightName + "TurbScale";
}
