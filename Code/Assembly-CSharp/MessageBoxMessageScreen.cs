using System;
using UnityEngine;

// Token: 0x0200066D RID: 1645
public class MessageBoxMessageScreen : Suspendable
{
	// Token: 0x1700065E RID: 1630
	// (get) Token: 0x06002806 RID: 10246 RVA: 0x000ADE23 File Offset: 0x000AC023
	public bool Visible
	{
		get
		{
			return this.m_time > 0.01f;
		}
	}

	// Token: 0x06002807 RID: 10247 RVA: 0x000ADE34 File Offset: 0x000AC034
	private new void Awake()
	{
		base.Awake();
		this.m_originalScale = base.transform.localScale;
		this.m_fadingOut = true;
		this.m_renderer = (base.GetComponentInChildren(typeof(Renderer)) as Renderer);
		this.m_guiTexture = (base.GetComponentInChildren(typeof(GUITexture)) as GUITexture);
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x000ADE95 File Offset: 0x000AC095
	private void Start()
	{
		if (this.m_timeSpeed == 0f)
		{
			this.ShowMessageScreen();
		}
		this.FixedUpdate();
	}

	// Token: 0x1700065F RID: 1631
	// (get) Token: 0x06002809 RID: 10249 RVA: 0x000ADEB3 File Offset: 0x000AC0B3
	// (set) Token: 0x0600280A RID: 10250 RVA: 0x000ADEBB File Offset: 0x000AC0BB
	public override bool IsSuspended { get; set; }

	// Token: 0x0600280B RID: 10251 RVA: 0x000ADEC4 File Offset: 0x000AC0C4
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			this.SetOpacity(0f);
			return;
		}
		float deltaTime = Time.deltaTime;
		this.m_time = Mathf.Clamp(this.m_time + this.m_timeSpeed * deltaTime, 0f, 1f);
		if (this.m_timeSpeed > 0f)
		{
			base.transform.localScale = this.ScaleIn.Evaluate(this.m_time) * this.m_originalScale;
			this.SetOpacity(this.OpacityIn.Evaluate(this.m_time));
		}
		else
		{
			base.transform.localScale = this.ScaleOut.Evaluate(this.m_time) * this.m_originalScale;
			this.SetOpacity(this.OpacityOut.Evaluate(this.m_time));
		}
		if (this.m_time == 0f || this.m_time == 1f)
		{
			this.m_timeSpeed = 0f;
		}
		if (this.m_time == 1f)
		{
			this.m_delayTime -= deltaTime;
			if (this.m_delayTime < 0f)
			{
				this.HideMessageScreen();
			}
		}
		if (this.m_fadingOut && this.m_time == 0f)
		{
			UnityEngine.Object.DestroyObject(base.gameObject);
		}
	}

	// Token: 0x0600280C RID: 10252 RVA: 0x000AE028 File Offset: 0x000AC228
	public void ShowMessageScreen()
	{
		this.m_timeSpeed = 1f / this.TransitionInDuration;
		this.m_delayTime = this.WaitDuration;
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x000AE048 File Offset: 0x000AC248
	public void HideMessageScreen()
	{
		this.m_timeSpeed = -1f / this.TransitionOutDuration;
		this.m_fadingOut = true;
	}

	// Token: 0x0600280E RID: 10254 RVA: 0x000AE063 File Offset: 0x000AC263
	public void ResetWaitDuration()
	{
		this.m_delayTime = this.WaitDuration;
	}

	// Token: 0x0600280F RID: 10255 RVA: 0x000AE071 File Offset: 0x000AC271
	public void ResetWaitDuration(float waitDuration)
	{
		if (waitDuration == 0f)
		{
			waitDuration = 0.05f;
		}
		this.m_delayTime = waitDuration;
	}

	// Token: 0x06002810 RID: 10256 RVA: 0x000AE08C File Offset: 0x000AC28C
	public void HideImmediately()
	{
		this.SetOpacity(0f);
		this.m_time = 0f;
		this.m_fadingOut = true;
	}

	// Token: 0x06002811 RID: 10257 RVA: 0x000AE0AC File Offset: 0x000AC2AC
	private void SetOpacity(float opacity)
	{
		if (this.m_renderer)
		{
			Material material = this.m_renderer.material;
			Color color = Color.white;
			int tintColor = ShaderProperties.TintColor;
			int color2 = ShaderProperties.Color;
			if (material.HasProperty(tintColor))
			{
				color = material.GetColor(tintColor);
			}
			if (material.HasProperty(color2))
			{
				color = material.GetColor(color2);
			}
			color.a = opacity;
			if (material.HasProperty(tintColor))
			{
				material.SetColor(tintColor, color);
			}
			if (material.HasProperty(color2))
			{
				material.SetColor(color2, color);
			}
		}
		else if (this.m_guiTexture)
		{
			Color color3 = this.m_guiTexture.color;
			color3.a = opacity;
			this.m_guiTexture.color = color3;
		}
	}

	// Token: 0x06002812 RID: 10258 RVA: 0x000AE178 File Offset: 0x000AC378
	public void SetTexture(Texture2D texture)
	{
		if (this.m_renderer)
		{
			UberShaderAPI.SetMainTexture(this.m_renderer, texture, true);
		}
		else if (this.m_guiTexture)
		{
			this.m_guiTexture.texture = texture;
		}
	}

	// Token: 0x0400229C RID: 8860
	public AnimationCurve ScaleIn;

	// Token: 0x0400229D RID: 8861
	public AnimationCurve OpacityIn;

	// Token: 0x0400229E RID: 8862
	public AnimationCurve ScaleOut;

	// Token: 0x0400229F RID: 8863
	public AnimationCurve OpacityOut;

	// Token: 0x040022A0 RID: 8864
	public float TransitionInDuration;

	// Token: 0x040022A1 RID: 8865
	public float TransitionOutDuration;

	// Token: 0x040022A2 RID: 8866
	public float WaitDuration = 3f;

	// Token: 0x040022A3 RID: 8867
	private float m_time;

	// Token: 0x040022A4 RID: 8868
	private float m_timeSpeed;

	// Token: 0x040022A5 RID: 8869
	private float m_delayTime;

	// Token: 0x040022A6 RID: 8870
	private bool m_fadingOut;

	// Token: 0x040022A7 RID: 8871
	private Vector3 m_originalScale;

	// Token: 0x040022A8 RID: 8872
	private Renderer m_renderer;

	// Token: 0x040022A9 RID: 8873
	private GUITexture m_guiTexture;
}
