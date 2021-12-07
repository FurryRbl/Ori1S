using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
[AddComponentMenu("AVPro Windows Media/Material Apply")]
public class AVProWindowsMediaMaterialApply : MonoBehaviour
{
	// Token: 0x06001266 RID: 4710 RVA: 0x0005373C File Offset: 0x0005193C
	private static void CreateTexture()
	{
		AVProWindowsMediaMaterialApply._blackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false, false);
		AVProWindowsMediaMaterialApply._blackTexture.name = "AVProWindowsMedia-BlackTexture";
		AVProWindowsMediaMaterialApply._blackTexture.filterMode = FilterMode.Point;
		AVProWindowsMediaMaterialApply._blackTexture.wrapMode = TextureWrapMode.Clamp;
		AVProWindowsMediaMaterialApply._blackTexture.SetPixel(0, 0, Color.black);
		AVProWindowsMediaMaterialApply._blackTexture.Apply(false, true);
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x0005379A File Offset: 0x0005199A
	private void OnDestroy()
	{
		this._defaultTexture = null;
		if (AVProWindowsMediaMaterialApply._blackTexture != null)
		{
			UnityEngine.Object.Destroy(AVProWindowsMediaMaterialApply._blackTexture);
			AVProWindowsMediaMaterialApply._blackTexture = null;
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000537C3 File Offset: 0x000519C3
	private void Start()
	{
		if (AVProWindowsMediaMaterialApply._blackTexture == null)
		{
			AVProWindowsMediaMaterialApply.CreateTexture();
		}
		if (this._defaultTexture == null)
		{
			this._defaultTexture = AVProWindowsMediaMaterialApply._blackTexture;
		}
		this.Update();
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000537FC File Offset: 0x000519FC
	private void Update()
	{
		if (this._movie != null)
		{
			if (this._movie.OutputTexture != null)
			{
				this.ApplyMapping(this._movie.OutputTexture);
			}
			else
			{
				this.ApplyMapping(this._defaultTexture);
			}
		}
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x00053854 File Offset: 0x00051A54
	private void ApplyMapping(Texture texture)
	{
		if (this._material != null)
		{
			if (string.IsNullOrEmpty(this._textureName))
			{
				this._material.mainTexture = texture;
			}
			else
			{
				this._material.SetTexture(this._textureName, texture);
			}
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000538A5 File Offset: 0x00051AA5
	public void OnDisable()
	{
		this.ApplyMapping(null);
	}

	// Token: 0x04000FA6 RID: 4006
	public Material _material;

	// Token: 0x04000FA7 RID: 4007
	public AVProWindowsMediaMovie _movie;

	// Token: 0x04000FA8 RID: 4008
	public string _textureName;

	// Token: 0x04000FA9 RID: 4009
	public Texture2D _defaultTexture;

	// Token: 0x04000FAA RID: 4010
	private static Texture2D _blackTexture;
}
