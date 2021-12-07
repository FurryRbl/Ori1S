using System;
using UnityEngine;

// Token: 0x020007F3 RID: 2035
[UberShaderOrder(UberShaderOrder.CharacterTint)]
[CustomShaderModifier("Environment Tint")]
[ExecuteInEditMode]
[UberShaderCategory(UberShaderCategory.Lighting)]
public class EnvironmentTintModifier : EnvironmentShadingModifier
{
	// Token: 0x06002EB9 RID: 11961 RVA: 0x000C610F File Offset: 0x000C430F
	private int GetEnvTintColorID()
	{
		if (this.m_envTintColorID == 0)
		{
			this.m_envTintColorID = Shader.PropertyToID("_EnvTintColor");
		}
		return this.m_envTintColorID;
	}

	// Token: 0x06002EBA RID: 11962 RVA: 0x000C6134 File Offset: 0x000C4334
	protected override void BindNow(EnvironmentLight characterLight, int index, bool curLight)
	{
		if (index == 0 && curLight)
		{
			Color color = characterLight.GetColorForChannel(this.Channel);
			if (this.FadeLight != null)
			{
				color = Color.Lerp(color, this.FadeLight.GetColorForChannel(this.Channel), base.CurFade);
			}
			base.BindMaterial.SetColor(this.GetEnvTintColorID(), color);
		}
	}

	// Token: 0x06002EBB RID: 11963 RVA: 0x000C619C File Offset: 0x000C439C
	protected override void ClearBind(int num)
	{
		if (num == 0)
		{
			base.BindMaterial.SetColor(this.GetEnvTintColorID(), Color.white);
		}
	}

	// Token: 0x06002EBC RID: 11964 RVA: 0x000C61C5 File Offset: 0x000C43C5
	protected override void UpdateBaseBind()
	{
	}

	// Token: 0x06002EBD RID: 11965 RVA: 0x000C61C7 File Offset: 0x000C43C7
	public override string GetBaseShaderProperties()
	{
		return base.ShaderlabString("_EnvTintColor", "Color", "(1, 1, 1, 1)");
	}

	// Token: 0x06002EBE RID: 11966 RVA: 0x000C61DE File Offset: 0x000C43DE
	public override void SetProperties()
	{
		this.TintStrength.Set("_EnvTintStrength", base.AttachedToShaderBlock);
	}

	// Token: 0x040029EB RID: 10731
	public EnvironmentLight.Channel Channel;

	// Token: 0x040029EC RID: 10732
	public UberShaderFloat TintStrength = new UberShaderFloat(1f);

	// Token: 0x040029ED RID: 10733
	private int m_envTintColorID;
}
