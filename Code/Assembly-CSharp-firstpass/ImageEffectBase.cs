using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public class ImageEffectBase : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00002D38 File Offset: 0x00000F38
	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000027 RID: 39 RVA: 0x00002D80 File Offset: 0x00000F80
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.shader);
				this.m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002DB8 File Offset: 0x00000FB8
	protected virtual void OnDisable()
	{
		if (this.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x04000022 RID: 34
	public Shader shader;

	// Token: 0x04000023 RID: 35
	private Material m_Material;
}
