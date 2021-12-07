using System;
using UnityEngine;

// Token: 0x02000569 RID: 1385
public class TextureBasedOnHealth : MonoBehaviour
{
	// Token: 0x060023F2 RID: 9202 RVA: 0x0009CE58 File Offset: 0x0009B058
	public void Awake()
	{
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x0009CE68 File Offset: 0x0009B068
	public void FixedUpdate()
	{
		int num = Mathf.RoundToInt(this.HealthToIndex.Evaluate(this.Entity.DamageReciever.Health / this.Entity.DamageReciever.MaxHealth));
		Texture texture = this.Textures[num];
		if (texture != this.m_renderer.material.GetTexture(ShaderProperties.MainTexture))
		{
			this.m_renderer.material.SetTexture(ShaderProperties.MainTexture, texture);
		}
	}

	// Token: 0x04001E15 RID: 7701
	public Entity Entity;

	// Token: 0x04001E16 RID: 7702
	public Texture[] Textures;

	// Token: 0x04001E17 RID: 7703
	public AnimationCurve HealthToIndex;

	// Token: 0x04001E18 RID: 7704
	private Renderer m_renderer;
}
