using System;
using UnityEngine;

// Token: 0x0200079E RID: 1950
[Serializable]
public class UberShaderTexture : UberShaderTextureBase
{
	// Token: 0x06002D42 RID: 11586 RVA: 0x000C1ACB File Offset: 0x000BFCCB
	public override void BindProperties()
	{
		base.BindTexture(this.MainBindId, this.m_texture);
		base.BindBase();
	}

	// Token: 0x06002D43 RID: 11587 RVA: 0x000C1AE8 File Offset: 0x000BFCE8
	public void SpeedupScroll(float speed)
	{
		this.ProTextureScroll.x = this.ProTextureScroll.x * speed;
		this.ProTextureScroll.y = this.ProTextureScroll.y * speed;
		this.BindProperties();
	}

	// Token: 0x040028DD RID: 10461
	[SerializeField]
	private Texture2D m_texture;

	// Token: 0x040028DE RID: 10462
	[NonSerialized]
	public bool IsVertexTexture;
}
