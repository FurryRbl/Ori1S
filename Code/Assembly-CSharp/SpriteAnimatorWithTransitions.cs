using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[ExecuteInEditMode]
public class SpriteAnimatorWithTransitions : Suspendable, IPooled, IInScene, IDynamicGraphicHierarchy
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000301 RID: 769 RVA: 0x0000CBFA File Offset: 0x0000ADFA
	// (remove) Token: 0x06000302 RID: 770 RVA: 0x0000CC13 File Offset: 0x0000AE13
	public event Action<TextureAnimation> OnAnimationEndEvent = delegate(TextureAnimation A_0)
	{
	};

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06000303 RID: 771 RVA: 0x0000CC2C File Offset: 0x0000AE2C
	// (remove) Token: 0x06000304 RID: 772 RVA: 0x0000CC45 File Offset: 0x0000AE45
	public event Action<TextureAnimation> OnAnimationLoopEvent = delegate(TextureAnimation A_0)
	{
	};

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000305 RID: 773 RVA: 0x0000CC5E File Offset: 0x0000AE5E
	public bool IsTransitionPlaying
	{
		get
		{
			return this.m_transition != null;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000306 RID: 774 RVA: 0x0000CC6C File Offset: 0x0000AE6C
	protected Renderer Renderer
	{
		get
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000307 RID: 775 RVA: 0x0000CC91 File Offset: 0x0000AE91
	public bool AnimationEnded
	{
		get
		{
			return this.m_animator.AnimationEnded && !this.IsTransitionPlaying;
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0000CCAF File Offset: 0x0000AEAF
	private void InitBinder()
	{
		this.m_binder = new AtlasSpriteTextureBinder(this.MeshSettings, this.UseSpriteSpaceUvs, this.m_mesh);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000CCD0 File Offset: 0x0000AED0
	public void OnEnable()
	{
		try
		{
			if (!Application.isPlaying)
			{
				this.OnEditorEnable();
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000CD08 File Offset: 0x0000AF08
	public new void Awake()
	{
		base.Awake();
		this.m_mesh = new Mesh
		{
			name = "atlasSpriteTexture"
		};
		this.m_meshFilter = base.GetComponent<MeshFilter>();
		this.InitBinder();
		this.SetAnimation(this.DefaultAnimation, false);
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000CD54 File Offset: 0x0000AF54
	public void OnEditorEnable()
	{
		if (this.m_editorEnabled)
		{
			return;
		}
		this.m_editorEnabled = true;
		this.m_mesh = new Mesh
		{
			name = "atlasSpriteTexture"
		};
		this.m_mesh.hideFlags = HideFlags.DontSave;
		this.InitBinder();
		if (this.DefaultAnimation != null)
		{
			this.m_animator.SetAnimation(this.DefaultAnimation.Animation, true);
			this.ChangeMainTextureToAnimatorTexture();
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000CDCD File Offset: 0x0000AFCD
	public void OnDisable()
	{
		if (!Application.isPlaying)
		{
			this.OnEditorDisable();
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
	public new void OnDestroy()
	{
		base.OnDestroy();
		if (Application.isPlaying && this.m_mesh)
		{
			UnityEngine.Object.DestroyObject(this.m_mesh);
			this.m_mesh = null;
		}
		if (this.m_madeMaterial)
		{
			UnityEngine.Object.DestroyObject(this.Renderer.material);
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000CE3C File Offset: 0x0000B03C
	public void OnEditorDisable()
	{
		if (this.m_mesh)
		{
			UnityEngine.Object.DestroyImmediate(this.m_mesh);
			this.m_mesh = null;
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000CE6B File Offset: 0x0000B06B
	public void Start()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.ChangeMainTextureToAnimatorTexture();
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0000CE80 File Offset: 0x0000B080
	private void UpdateCurrentBinding()
	{
		float time = this.m_animator.Time;
		if (this.m_animator.AnimationEnded && this.IsTransitionPlaying)
		{
			this.m_transition = null;
			this.m_animator.SetAnimation(this.CurrentTextureAnimationTransitions.Animation, true);
		}
		this.m_animator.Advance(Time.deltaTime);
		this.ChangeMainTextureToAnimatorTexture();
		if (this.m_animator.AnimationEnded)
		{
			this.OnAnimationEndEvent(this.m_animator.Animation);
		}
		if (this.m_animator.Time < time && this.m_animator.Animation.Loop)
		{
			this.OnAnimationLoopEvent(this.m_animator.Animation);
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0000CF4A File Offset: 0x0000B14A
	public void FixedUpdate()
	{
		if (this.m_isSuspended)
		{
			return;
		}
		if (this.m_animator.Animation == null)
		{
			return;
		}
		this.UpdateCurrentBinding();
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000312 RID: 786 RVA: 0x0000CF75 File Offset: 0x0000B175
	public TextureAnimator TextureAnimator
	{
		get
		{
			return this.m_animator;
		}
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0000CF80 File Offset: 0x0000B180
	public void ChangeMainTextureToAnimatorTexture()
	{
		if (this.m_animator.Animation == null)
		{
			return;
		}
		Atlas atlas;
		AtlasSpriteTexture textureAtTime = this.m_animator.Animation.GetTextureAtTime(this.m_animator.Time, out atlas);
		if (this.m_lastTexture != textureAtTime)
		{
			this.m_lastTexture = textureAtTime;
			Material material;
			if (Application.isPlaying)
			{
				if (this.IsInScene)
				{
					material = this.Renderer.sharedMaterial;
				}
				else
				{
					material = this.Renderer.material;
					this.m_madeMaterial = true;
				}
			}
			else
			{
				material = this.Renderer.sharedMaterial;
			}
			this.m_binder.BindTo(this.m_meshFilter, material, atlas, atlas.ScreenMode, textureAtTime);
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000314 RID: 788 RVA: 0x0000D039 File Offset: 0x0000B239
	// (set) Token: 0x06000315 RID: 789 RVA: 0x0000D046 File Offset: 0x0000B246
	public float CurrentAnimationTime
	{
		get
		{
			return this.m_animator.Time;
		}
		set
		{
			this.m_animator.Time = value;
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000316 RID: 790 RVA: 0x0000D054 File Offset: 0x0000B254
	public TextureAnimation CurrentAnimation
	{
		get
		{
			return this.m_animator.Animation;
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0000D061 File Offset: 0x0000B261
	[ContextMenu("What is the current texture animator")]
	public void PrintCurrentTextureAnimator()
	{
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0000D063 File Offset: 0x0000B263
	[ContextMenu("What is the current texture with transitions animator")]
	public void PrintCurrentTextureAnimatorWithTranstion()
	{
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000319 RID: 793 RVA: 0x0000D065 File Offset: 0x0000B265
	// (set) Token: 0x0600031A RID: 794 RVA: 0x0000D06D File Offset: 0x0000B26D
	public bool IsInScene
	{
		get
		{
			return this.m_isInScene;
		}
		set
		{
			this.m_isInScene = value;
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D076 File Offset: 0x0000B276
	// (set) Token: 0x0600031C RID: 796 RVA: 0x0000D07E File Offset: 0x0000B27E
	public override bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000D088 File Offset: 0x0000B288
	public void SetAnimation(TextureAnimationWithTransitions textureAnimationWithTransitions, bool ignoreIfSameAnimation = false)
	{
		if (textureAnimationWithTransitions == null)
		{
			return;
		}
		TextureAnimationWithTransitions.TextureAnimationPair textureAnimationPair = null;
		int currentFrame = 0;
		if (this.m_animator.Animation != null)
		{
			currentFrame = (int)this.m_animator.Frame;
		}
		if (this.IsTransitionPlaying && this.m_animator.Animation != null && this.m_animator.Animation.FrameGuids.Count > 0 && this.m_animator.Frame < (float)this.m_transition.CrossoverFrame)
		{
			textureAnimationPair = textureAnimationWithTransitions.GetTransition(currentFrame, this.PreviousTextureAnimationTransitions, this.m_animator.Animation, this.Flip);
		}
		else if (this.m_animator.Animation != null)
		{
			textureAnimationPair = textureAnimationWithTransitions.GetTransition(currentFrame, this.CurrentTextureAnimationTransitions, this.m_animator.Animation, this.Flip);
		}
		bool resetTime = true;
		if (this.PreviousTextureAnimationTransitions == this && this.IsTransitionPlaying && textureAnimationPair != null)
		{
			this.m_animator.Time = Mathf.Max(0f, this.m_animator.AnimationDuration - this.m_animator.Time * 3f) / 3f / this.m_animator.AnimationDuration * textureAnimationPair.TransitionAnimation.Duration;
			resetTime = false;
		}
		if (textureAnimationPair != null)
		{
			if (!ignoreIfSameAnimation || textureAnimationPair.TransitionAnimation != this.m_animator.Animation)
			{
				this.m_animator.SetAnimation(textureAnimationPair.TransitionAnimation, resetTime);
			}
			this.m_transition = textureAnimationPair;
		}
		else if (!ignoreIfSameAnimation || textureAnimationWithTransitions.Animation != this.m_animator.Animation)
		{
			this.m_animator.SetAnimation(textureAnimationWithTransitions.Animation, true);
			this.m_transition = null;
		}
		this.PreviousTextureAnimationTransitions = this.CurrentTextureAnimationTransitions;
		this.CurrentTextureAnimationTransitions = textureAnimationWithTransitions;
		this.ChangeMainTextureToAnimatorTexture();
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D288 File Offset: 0x0000B488
	private Atlas SettingsAtlas
	{
		get
		{
			if (this.DefaultAnimation == null)
			{
				return null;
			}
			if (this.DefaultAnimation.Animation == null)
			{
				return null;
			}
			foreach (Atlas atlas in this.DefaultAnimation.Animation.Atlases)
			{
				if (atlas != null)
				{
					return atlas;
				}
			}
			return null;
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0000D328 File Offset: 0x0000B528
	public UberScreenMode GetExternalUberScreenMode()
	{
		Atlas settingsAtlas = this.SettingsAtlas;
		return (!(settingsAtlas == null)) ? settingsAtlas.ScreenMode : UberScreenMode.None;
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0000D354 File Offset: 0x0000B554
	public float GetUberTweakValue()
	{
		Atlas settingsAtlas = this.SettingsAtlas;
		return (!(settingsAtlas == null)) ? settingsAtlas.UberScreenTweak : 0f;
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0000D384 File Offset: 0x0000B584
	public bool DoesProvideAtlas()
	{
		return this.SettingsAtlas != null;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0000D392 File Offset: 0x0000B592
	public void SetDirty()
	{
		this.ChangeMainTextureToAnimatorTexture();
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0000D39A File Offset: 0x0000B59A
	public void OnPoolSpawned()
	{
		this.m_binder.OnPoolSpawned();
	}

	// Token: 0x0400023C RID: 572
	public bool UseSpriteSpaceUvs;

	// Token: 0x0400023D RID: 573
	private bool m_useSpriteSpaceUvs;

	// Token: 0x0400023E RID: 574
	private TextureAnimationWithTransitions.TextureAnimationPair m_transition;

	// Token: 0x0400023F RID: 575
	private Mesh m_mesh;

	// Token: 0x04000240 RID: 576
	private Renderer m_renderer;

	// Token: 0x04000241 RID: 577
	private bool m_editorEnabled;

	// Token: 0x04000242 RID: 578
	public AnimationMeshingSettings MeshSettings = new AnimationMeshingSettings();

	// Token: 0x04000243 RID: 579
	private AtlasSpriteTextureBinder m_binder;

	// Token: 0x04000244 RID: 580
	private bool m_madeMaterial;

	// Token: 0x04000245 RID: 581
	private float m_lastTime = -1f;

	// Token: 0x04000246 RID: 582
	private AtlasSpriteTexture m_lastTexture;

	// Token: 0x04000247 RID: 583
	public TextureAnimationWithTransitions DefaultAnimation;

	// Token: 0x04000248 RID: 584
	[HideInInspector]
	public TextureAnimationWithTransitions CurrentTextureAnimationTransitions;

	// Token: 0x04000249 RID: 585
	[HideInInspector]
	public TextureAnimationWithTransitions PreviousTextureAnimationTransitions;

	// Token: 0x0400024A RID: 586
	private readonly TextureAnimator m_animator = new TextureAnimator();

	// Token: 0x0400024B RID: 587
	[HideInInspector]
	public bool Flip;

	// Token: 0x0400024C RID: 588
	[SerializeField]
	private bool m_isInScene;

	// Token: 0x0400024D RID: 589
	private bool m_isSuspended;

	// Token: 0x0400024E RID: 590
	private MeshFilter m_meshFilter;
}
