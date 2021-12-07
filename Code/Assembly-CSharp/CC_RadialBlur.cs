using System;
using UnityEngine;

// Token: 0x02000221 RID: 545
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Colorful/Radial Blur")]
public class CC_RadialBlur : MonoBehaviour
{
	// Token: 0x060012AC RID: 4780 RVA: 0x000552F0 File Offset: 0x000534F0
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x00055304 File Offset: 0x00053504
	private bool CheckShader()
	{
		return this._currentShader && this._currentShader.isSupported;
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x0005532C File Offset: 0x0005352C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.material.SetFloat("amount", this.amount);
		this._material.SetVector("center", this.center);
		if (!this.CheckShader())
		{
			Graphics.Blit(source, destination);
			return;
		}
		Graphics.Blit(source, destination, this._material);
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x060012AF RID: 4783 RVA: 0x0005538C File Offset: 0x0005358C
	private Material material
	{
		get
		{
			if (this.quality == 0)
			{
				this._currentShader = this.shaderLow;
			}
			else if (this.quality == 1)
			{
				this._currentShader = this.shaderMed;
			}
			else if (this.quality == 2)
			{
				this._currentShader = this.shaderHigh;
			}
			if (this._material == null)
			{
				this._material = new Material(this._currentShader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			else
			{
				this._material.shader = this._currentShader;
			}
			return this._material;
		}
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x00055435 File Offset: 0x00053635
	private void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x04001012 RID: 4114
	public float amount = 0.1f;

	// Token: 0x04001013 RID: 4115
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x04001014 RID: 4116
	public int quality = 1;

	// Token: 0x04001015 RID: 4117
	public Shader shaderLow;

	// Token: 0x04001016 RID: 4118
	public Shader shaderMed;

	// Token: 0x04001017 RID: 4119
	public Shader shaderHigh;

	// Token: 0x04001018 RID: 4120
	private Shader _currentShader;

	// Token: 0x04001019 RID: 4121
	private Material _material;
}
