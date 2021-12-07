using System;

// Token: 0x02000819 RID: 2073
[CustomShaderModifier("Worldmap")]
[UberShaderOrder(UberShaderOrder.Worldmap)]
[UberShaderCategory(UberShaderCategory.Utility)]
public class WorldmapModifier : UberShaderModifier
{
	// Token: 0x06002FA8 RID: 12200 RVA: 0x000C9F74 File Offset: 0x000C8174
	public override void SetProperties()
	{
		this.WorldMapMaskA.Set("_MapMaskTextureA", base.AttachedToShaderBlock);
		this.WorldMapMaskB.Set("_MapMaskTextureB", base.AttachedToShaderBlock);
		this.Fade.Set("_MapFade", base.AttachedToShaderBlock);
	}

	// Token: 0x04002AC8 RID: 10952
	public UberShaderTexture WorldMapMaskA = new UberShaderTexture();

	// Token: 0x04002AC9 RID: 10953
	public UberShaderTexture WorldMapMaskB = new UberShaderTexture();

	// Token: 0x04002ACA RID: 10954
	public UberShaderFloat Fade = new UberShaderFloat();
}
