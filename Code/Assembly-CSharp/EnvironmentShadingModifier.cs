using System;
using UnityEngine;

// Token: 0x020007EE RID: 2030
[ExecuteInEditMode]
public abstract class EnvironmentShadingModifier : UberShaderModifier
{
	// Token: 0x1700077B RID: 1915
	// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000C58BD File Offset: 0x000C3ABD
	private float FadeTime
	{
		get
		{
			return Application.isPlaying ? Time.time : Time.realtimeSinceStartup;
		}
	}

	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000C58D8 File Offset: 0x000C3AD8
	protected float CurFade
	{
		get
		{
			return Mathf.Clamp01(this.m_fadeTime - this.FadeTime);
		}
	}

	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000C58EC File Offset: 0x000C3AEC
	private Rect RendererRect
	{
		get
		{
			Bounds bounds = base.Renderer.bounds;
			return new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);
		}
	}

	// Token: 0x06002E9A RID: 11930
	protected abstract void UpdateBaseBind();

	// Token: 0x06002E9B RID: 11931 RVA: 0x000C5948 File Offset: 0x000C3B48
	private void Update()
	{
		if (EnvironmentLightingManager.Instance == null)
		{
			return;
		}
		this.m_frame++;
		if (this.m_frame % 2 == 0)
		{
			this.UpdateBaseBind();
			Rect rendererRect = this.RendererRect;
			EnvironmentLight characterLightAtPos = EnvironmentLightingManager.Instance.GetCharacterLightAtPos(rendererRect);
			if (this.LastLight == null)
			{
				this.LastLight = characterLightAtPos;
			}
			if (characterLightAtPos != this.LastLight)
			{
				this.StartFade(this.LastLight);
				this.LastLight = characterLightAtPos;
			}
			if (this.FadeLight != null)
			{
				float num = this.CurFade;
				if (num < 0f)
				{
					this.FadeLight = null;
					base.BindMaterial.SetFloat("_Fade", 0f);
					this.ClearBind(1);
				}
				else
				{
					num = Mathf.SmoothStep(0f, 1f, num);
					base.BindMaterial.SetFloat("_Fade", num);
					this.BindNow(this.FadeLight, 1, false);
				}
			}
			else
			{
				base.BindMaterial.SetFloat("_Fade", 0f);
			}
			if (characterLightAtPos != null)
			{
				this.BindNow(characterLightAtPos, 0, true);
			}
			else
			{
				this.ClearBind(0);
				this.ClearBind(1);
			}
		}
	}

	// Token: 0x06002E9C RID: 11932
	protected abstract void BindNow(EnvironmentLight light, int index, bool curLight);

	// Token: 0x06002E9D RID: 11933
	protected abstract void ClearBind(int num);

	// Token: 0x06002E9E RID: 11934 RVA: 0x000C5A93 File Offset: 0x000C3C93
	private void StartFade(EnvironmentLight from)
	{
		this.FadeLight = from;
		this.m_fadeTime = this.FadeTime + 0.7f;
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x000C5AAE File Offset: 0x000C3CAE
	public override bool DoStrip()
	{
		return false;
	}

	// Token: 0x040029CE RID: 10702
	private const float c_fadeDuration = 0.7f;

	// Token: 0x040029CF RID: 10703
	protected EnvironmentLight LastLight;

	// Token: 0x040029D0 RID: 10704
	protected EnvironmentLight FadeLight;

	// Token: 0x040029D1 RID: 10705
	private float m_fadeTime;

	// Token: 0x040029D2 RID: 10706
	private int m_frame;
}
