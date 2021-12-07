using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020008D9 RID: 2265
public class SpiritLantern : SaveSerialize, IDamageReciever, IAttackable, IBashAttackable
{
	// Token: 0x06003273 RID: 12915 RVA: 0x000D5326 File Offset: 0x000D3526
	public override void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x06003274 RID: 12916 RVA: 0x000D5334 File Offset: 0x000D3534
	public void OnEnable()
	{
		Targets.Attackables.Add(this);
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x000D5341 File Offset: 0x000D3541
	public void OnDisable()
	{
		Targets.Attackables.Remove(this);
	}

	// Token: 0x17000807 RID: 2055
	// (get) Token: 0x06003276 RID: 12918 RVA: 0x000D534F File Offset: 0x000D354F
	// (set) Token: 0x06003277 RID: 12919 RVA: 0x000D5357 File Offset: 0x000D3557
	public bool Activated
	{
		get
		{
			return this.m_activated;
		}
		set
		{
			this.m_activated = value;
			this.Energy.SetActive(this.m_activated);
		}
	}

	// Token: 0x17000808 RID: 2056
	// (get) Token: 0x06003278 RID: 12920 RVA: 0x000D5371 File Offset: 0x000D3571
	// (set) Token: 0x06003279 RID: 12921 RVA: 0x000D53A4 File Offset: 0x000D35A4
	public Color OuterGlowColor
	{
		get
		{
			return (!this.OuterGlow) ? Color.black : this.OuterGlow.sharedMaterial.GetColor(ShaderProperties.Color);
		}
		set
		{
			if (this.OuterGlow)
			{
				this.OuterGlow.sharedMaterial.SetColor(ShaderProperties.Color, value);
			}
		}
	}

	// Token: 0x17000809 RID: 2057
	// (get) Token: 0x0600327A RID: 12922 RVA: 0x000D53D7 File Offset: 0x000D35D7
	// (set) Token: 0x0600327B RID: 12923 RVA: 0x000D5408 File Offset: 0x000D3608
	public Color SparkParticlesColor
	{
		get
		{
			return (!this.SparkParticles) ? Color.black : this.SparkParticles.sharedMaterial.GetColor(ShaderProperties.Color);
		}
		set
		{
			if (this.SparkParticles)
			{
				this.SparkParticles.sharedMaterial.SetColor(ShaderProperties.Color, value);
			}
		}
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x000D543C File Offset: 0x000D363C
	public void Start()
	{
		this.m_originalOuterGlowColor = this.OuterGlowColor;
		this.m_originalSparkParticlesColor = this.SparkParticlesColor;
		this.Activated = this.ActivatedOnStart;
	}

	// Token: 0x0600327D RID: 12925 RVA: 0x000D546D File Offset: 0x000D366D
	public void FixedUpdate()
	{
		this.UpdateHighlightColor();
	}

	// Token: 0x0600327E RID: 12926 RVA: 0x000D5478 File Offset: 0x000D3678
	public void UpdateHighlightColor()
	{
		Color b = (!this.m_isBashHighlighted) ? this.m_originalOuterGlowColor : this.OuterGlowBashColor;
		Color b2 = (!this.m_isBashHighlighted) ? this.m_originalSparkParticlesColor : this.SparkParticlesBashColor;
		if (Utility.ColorDiff(this.m_lastOuterGlowColor, b) > 0.05f)
		{
			this.OuterGlowColor = (this.m_lastOuterGlowColor = Color.Lerp(this.m_lastOuterGlowColor, b, 0.5f));
		}
		if (Mathf.Abs(this.m_lastSparkParticlesColor.grayscale - b2.grayscale) > 0.05f)
		{
			this.SparkParticlesColor = (this.m_lastSparkParticlesColor = Color.Lerp(this.m_lastSparkParticlesColor, b2, 0.5f));
		}
	}

	// Token: 0x1700080A RID: 2058
	// (get) Token: 0x0600327F RID: 12927 RVA: 0x000D5537 File Offset: 0x000D3737
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x06003280 RID: 12928 RVA: 0x000D5544 File Offset: 0x000D3744
	public bool IsDead()
	{
		return false;
	}

	// Token: 0x06003281 RID: 12929 RVA: 0x000D5547 File Offset: 0x000D3747
	public bool CanBeChargeFlamed()
	{
		return false;
	}

	// Token: 0x06003282 RID: 12930 RVA: 0x000D554A File Offset: 0x000D374A
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x06003283 RID: 12931 RVA: 0x000D554D File Offset: 0x000D374D
	public bool CanBeGrenaded()
	{
		return false;
	}

	// Token: 0x06003284 RID: 12932 RVA: 0x000D5550 File Offset: 0x000D3750
	public bool CanBeStomped()
	{
		return false;
	}

	// Token: 0x06003285 RID: 12933 RVA: 0x000D5553 File Offset: 0x000D3753
	public bool CanBeBashed()
	{
		return this.Activated;
	}

	// Token: 0x06003286 RID: 12934 RVA: 0x000D555B File Offset: 0x000D375B
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x06003287 RID: 12935 RVA: 0x000D555E File Offset: 0x000D375E
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x06003288 RID: 12936 RVA: 0x000D5561 File Offset: 0x000D3761
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x06003289 RID: 12937 RVA: 0x000D5564 File Offset: 0x000D3764
	public void OnRecieveDamage(Damage damage)
	{
		Sound.Play(this.OnBashSoundProvider.GetSound(null), base.transform.position, null);
		if (this.OnAttackAction)
		{
			this.OnAttackAction.Perform(null);
		}
	}

	// Token: 0x0600328A RID: 12938 RVA: 0x000D55AB File Offset: 0x000D37AB
	public void OnEnterBash()
	{
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x000D55B0 File Offset: 0x000D37B0
	public void OnBashHighlight()
	{
		this.m_isBashHighlighted = true;
		Sound.Play(this.OnEnterBashRangeSoundProvider.GetSound(null), base.transform.position, null);
	}

	// Token: 0x0600328C RID: 12940 RVA: 0x000D55E2 File Offset: 0x000D37E2
	public void OnBashDehighlight()
	{
		this.m_isBashHighlighted = false;
		this.SparkParticlesColor = this.m_originalSparkParticlesColor;
	}

	// Token: 0x1700080B RID: 2059
	// (get) Token: 0x0600328D RID: 12941 RVA: 0x000D55F7 File Offset: 0x000D37F7
	public int BashPriority
	{
		get
		{
			return 50;
		}
	}

	// Token: 0x0600328E RID: 12942 RVA: 0x000D55FC File Offset: 0x000D37FC
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.Activated = ar.Serialize(true);
		}
		else
		{
			ar.Serialize(true);
		}
	}

	// Token: 0x04002D5D RID: 11613
	public ActionMethod OnAttackAction;

	// Token: 0x04002D5E RID: 11614
	public Color OuterGlowBashColor;

	// Token: 0x04002D5F RID: 11615
	public Color SparkParticlesBashColor;

	// Token: 0x04002D60 RID: 11616
	private Color m_originalOuterGlowColor;

	// Token: 0x04002D61 RID: 11617
	private Color m_originalSparkParticlesColor;

	// Token: 0x04002D62 RID: 11618
	public Renderer OuterGlow;

	// Token: 0x04002D63 RID: 11619
	public ParticleRenderer SparkParticles;

	// Token: 0x04002D64 RID: 11620
	public SoundProvider OnBashSoundProvider;

	// Token: 0x04002D65 RID: 11621
	public SoundProvider OnEnterBashRangeSoundProvider;

	// Token: 0x04002D66 RID: 11622
	public bool ActivatedOnStart = true;

	// Token: 0x04002D67 RID: 11623
	private bool m_activated;

	// Token: 0x04002D68 RID: 11624
	public GameObject Energy;

	// Token: 0x04002D69 RID: 11625
	private Transform m_transform;

	// Token: 0x04002D6A RID: 11626
	private Color m_lastOuterGlowColor;

	// Token: 0x04002D6B RID: 11627
	private Color m_lastSparkParticlesColor;

	// Token: 0x04002D6C RID: 11628
	private bool m_isBashHighlighted;
}
