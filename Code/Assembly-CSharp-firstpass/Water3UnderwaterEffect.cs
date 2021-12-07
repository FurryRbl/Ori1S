using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[ExecuteInEditMode]
public class Water3UnderwaterEffect : MonoBehaviour
{
	// Token: 0x0600007B RID: 123 RVA: 0x00006080 File Offset: 0x00004280
	private void Start()
	{
		if (this.m_UnderwaterShader)
		{
			this.m_UnderwaterMaterial = new Material(this.m_UnderwaterShader);
			this.m_UnderwaterMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000060BC File Offset: 0x000042BC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.m_BlendColor = Water3Manager.Instance().GetMaterialColor("_RefrColorDepth");
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
		temporary.name = "water3UnderwaterEffect";
		this.m_UnderwaterMaterial.SetColor("_DepthColor", this.m_BlendColor);
		this.m_UnderwaterMaterial.SetFloat("_UnderwaterColorFade", this.m_UnderwaterColorFade);
		this.m_UnderwaterMaterial.SetVector("offsets", new Vector4(1f, 0f, 0f, 0f));
		Graphics.Blit(source, temporary, this.m_UnderwaterMaterial, 0);
		this.m_UnderwaterMaterial.SetVector("offsets", new Vector4(0f, 1f, 0f, 0f));
		Graphics.Blit(temporary, destination, this.m_UnderwaterMaterial, 0);
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x04000094 RID: 148
	public float m_UnderwaterColorFade = 0.125f;

	// Token: 0x04000095 RID: 149
	public Shader m_UnderwaterShader;

	// Token: 0x04000096 RID: 150
	public Water3 m_Water;

	// Token: 0x04000097 RID: 151
	public Color m_BlendColor = Color.blue;

	// Token: 0x04000098 RID: 152
	private Material m_UnderwaterMaterial;
}
