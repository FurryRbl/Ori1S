using System;
using UnityEngine;

// Token: 0x02000213 RID: 531
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public class CC_Base : MonoBehaviour
{
	// Token: 0x0600128E RID: 4750 RVA: 0x0005482C File Offset: 0x00052A2C
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

	// Token: 0x1700034E RID: 846
	// (get) Token: 0x0600128F RID: 4751 RVA: 0x00054872 File Offset: 0x00052A72
	protected Material material
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.shader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this._material;
		}
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x000548A9 File Offset: 0x00052AA9
	protected virtual void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x04000FD3 RID: 4051
	public Shader shader;

	// Token: 0x04000FD4 RID: 4052
	protected Material _material;
}
