using System;
using Game;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class MessageBoxVisibility : MonoBehaviour, ISuspendable
{
	// Token: 0x17000210 RID: 528
	// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002ABB0 File Offset: 0x00028DB0
	public bool Visible
	{
		get
		{
			return this.m_time > 0.01f;
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0002ABC0 File Offset: 0x00028DC0
	public void Awake()
	{
		this.m_originalScale = base.transform.localScale;
		Events.Scheduler.OnGameLanguageChange.Add(new Action(this.Recache));
		Events.Scheduler.OnGameControlSchemeChange.Add(new Action(this.Recache));
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0002AC14 File Offset: 0x00028E14
	public void OnDestroy()
	{
		Events.Scheduler.OnGameLanguageChange.Remove(new Action(this.Recache));
		Events.Scheduler.OnGameControlSchemeChange.Remove(new Action(this.Recache));
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0002AC4C File Offset: 0x00028E4C
	public void Recache()
	{
		this.SetOpacity(1f);
		this.Cache();
		this.Advance();
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0002AC68 File Offset: 0x00028E68
	public void Cache()
	{
		this.m_renderers = base.GetComponentsInChildren<Renderer>();
		this.m_rendererAlphas = new float[this.m_renderers.Length];
		for (int i = 0; i < this.m_renderers.Length; i++)
		{
			Material sharedMaterial = this.m_renderers[i].sharedMaterial;
			if (sharedMaterial.HasProperty("_Color"))
			{
				this.m_rendererAlphas[i] = this.m_renderers[i].sharedMaterial.GetColor(ShaderProperties.Color).a;
			}
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0002ACF4 File Offset: 0x00028EF4
	public void Start()
	{
		if (this.m_timeSpeed == 0f)
		{
			this.ShowMessageScreen();
		}
		this.Cache();
		this.Advance();
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0002AD23 File Offset: 0x00028F23
	public Vector3 Flatten(Vector3 v)
	{
		return new Vector3(v.x, v.y, 1f);
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0002AD40 File Offset: 0x00028F40
	public void Advance()
	{
		float deltaTime = Time.deltaTime;
		this.m_time = Mathf.Clamp(this.m_time + this.m_timeSpeed * deltaTime, 0f, 1f);
		if (this.m_timeSpeed > 0f)
		{
			base.transform.localScale = this.Flatten(this.ScaleIn.Evaluate(this.m_time) * this.m_originalScale);
			this.SetOpacity(this.OpacityIn.Evaluate(this.m_time));
		}
		else
		{
			base.transform.localScale = this.Flatten(this.ScaleOut.Evaluate(this.m_time) * this.m_originalScale);
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
		if (this.m_time == 0f)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002AE8E File Offset: 0x0002908E
	// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0002AE96 File Offset: 0x00029096
	public bool IsSuspended { get; set; }

	// Token: 0x060009C4 RID: 2500 RVA: 0x0002AE9F File Offset: 0x0002909F
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.Advance();
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0002AEB3 File Offset: 0x000290B3
	public void ShowMessageScreen()
	{
		this.m_timeSpeed = 1f / this.TransitionInDuration;
		this.m_delayTime = this.WaitDuration;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0002AED3 File Offset: 0x000290D3
	public void HideMessageScreen()
	{
		this.m_timeSpeed = -1f / this.TransitionOutDuration;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0002AEE7 File Offset: 0x000290E7
	public void HideMessageScreenImmediately()
	{
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0002AEF4 File Offset: 0x000290F4
	public void ResetWaitDuration()
	{
		this.ShowMessageScreen();
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0002AEFC File Offset: 0x000290FC
	public void ResetWaitDuration(float waitDuration)
	{
		this.ShowMessageScreen();
		if (waitDuration == 0f)
		{
			waitDuration = 0.05f;
		}
		this.m_delayTime = waitDuration;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0002AF1D File Offset: 0x0002911D
	public void HideImmediately()
	{
		this.SetOpacity(0f);
		this.m_time = 0f;
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0002AF38 File Offset: 0x00029138
	private void SetOpacity(float opacity)
	{
		for (int i = 0; i < this.m_renderers.Length; i++)
		{
			if (this.m_renderers[i])
			{
				Material material = this.m_renderers[i].material;
				if (material.HasProperty(ShaderProperties.Color))
				{
					Color color = this.m_renderers[i].material.GetColor(ShaderProperties.Color);
					color.a = this.m_rendererAlphas[i] * opacity;
					material.SetColor(ShaderProperties.Color, color);
				}
			}
		}
	}

	// Token: 0x04000807 RID: 2055
	public float TransitionInDuration;

	// Token: 0x04000808 RID: 2056
	public float TransitionOutDuration;

	// Token: 0x04000809 RID: 2057
	public float WaitDuration;

	// Token: 0x0400080A RID: 2058
	public AnimationCurve ScaleIn;

	// Token: 0x0400080B RID: 2059
	public AnimationCurve OpacityIn;

	// Token: 0x0400080C RID: 2060
	public AnimationCurve ScaleOut;

	// Token: 0x0400080D RID: 2061
	public AnimationCurve OpacityOut;

	// Token: 0x0400080E RID: 2062
	private float m_time;

	// Token: 0x0400080F RID: 2063
	private float m_timeSpeed;

	// Token: 0x04000810 RID: 2064
	private float m_delayTime;

	// Token: 0x04000811 RID: 2065
	private Vector3 m_originalScale;

	// Token: 0x04000812 RID: 2066
	private Renderer[] m_renderers;

	// Token: 0x04000813 RID: 2067
	private float[] m_rendererAlphas;
}
