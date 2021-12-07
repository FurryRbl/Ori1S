using System;
using Core;
using UnityEngine;

// Token: 0x020000EA RID: 234
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SpriteAnimator : BaseAnimator
{
	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06000936 RID: 2358 RVA: 0x00027A9A File Offset: 0x00025C9A
	// (remove) Token: 0x06000937 RID: 2359 RVA: 0x00027AB3 File Offset: 0x00025CB3
	public event Action OnAnimationEndEvent = delegate()
	{
	};

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000938 RID: 2360 RVA: 0x00027ACC File Offset: 0x00025CCC
	public TextureAnimator TextureAnimator
	{
		get
		{
			return this.m_animator;
		}
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00027AD4 File Offset: 0x00025CD4
	private void InitBinder()
	{
		this.m_binder = new AtlasSpriteTextureBinder(this.MeshSettings, this.UseSpriteSpaceUvs, this.m_mesh);
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00027AF3 File Offset: 0x00025CF3
	public void OnEnable()
	{
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00027AF5 File Offset: 0x00025CF5
	public override void OnPoolSpawned()
	{
		this.HasAnimationEndedTriggered = false;
		this.PlaybackTime = 0f;
		base.OnPoolSpawned();
		this.m_binder.OnPoolSpawned();
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00027B1A File Offset: 0x00025D1A
	public new void Awake()
	{
		this.InitializeMesh();
		this.InitBinder();
		base.Awake();
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00027B30 File Offset: 0x00025D30
	public new void Start()
	{
		this.HasAnimationEndedTriggered = false;
		try
		{
			base.Start();
			if (this.PlayAtStart)
			{
				base.AnimatorDriver.IsPlaying = true;
			}
			base.Initialize();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00027B84 File Offset: 0x00025D84
	public void InitializeMesh()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_meshFilter = base.GetComponent<MeshFilter>();
		if (this.m_mesh == null)
		{
			this.m_mesh = new Mesh
			{
				name = "atlasSpriteTexture"
			};
			this.m_meshFilter.sharedMesh = this.m_mesh;
			if (!Application.isPlaying)
			{
				this.m_mesh.hideFlags = HideFlags.DontSave;
			}
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00027BFC File Offset: 0x00025DFC
	public new void OnDestroy()
	{
		base.OnDestroy();
		if (this.m_mesh)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.DestroyObject(this.m_mesh);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			this.m_mesh = null;
		}
		if (this.m_madeMaterial)
		{
			UnityEngine.Object.DestroyObject(this.m_renderer.sharedMaterial);
		}
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00027C68 File Offset: 0x00025E68
	public void DoSerialize(Archive ar)
	{
		base.AnimatorDriver.CurrentTime = ar.Serialize(base.AnimatorDriver.CurrentTime);
		this.HasAnimationEndedTriggered = ar.Serialize(this.HasAnimationEndedTriggered);
		base.AnimatorDriver.IsPlaying = ar.Serialize(base.AnimatorDriver.IsPlaying);
		if (ar.Reading)
		{
			this.TextureAnimator.Time = base.AnimatorDriver.CurrentTime;
			this.ChangeMainTextureToAnimatorTexture();
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000941 RID: 2369 RVA: 0x00027CE6 File Offset: 0x00025EE6
	// (set) Token: 0x06000942 RID: 2370 RVA: 0x00027CEE File Offset: 0x00025EEE
	public bool HasAnimationEndedTriggered { get; set; }

	// Token: 0x06000943 RID: 2371 RVA: 0x00027CF8 File Offset: 0x00025EF8
	public override void CacheOriginals()
	{
		if (this.m_mesh == null)
		{
			this.InitializeMesh();
		}
		this.SetAnimation(this.DefaultAnimation, true);
		this.ChangeMainTextureToAnimatorTexture();
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00027D2F File Offset: 0x00025F2F
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.PlaybackTime = value;
		this.m_animator.Time = this.PlaybackTime;
		this.ChangeMainTextureToAnimatorTexture();
		this.m_lastTime = value;
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00027D5F File Offset: 0x00025F5F
	public override void RestoreToOriginalState()
	{
		this.SampleValue(0f, true);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00027D70 File Offset: 0x00025F70
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.m_animator.AnimationEnded)
		{
			if (!this.HasAnimationEndedTriggered)
			{
				this.HasAnimationEndedTriggered = true;
				this.OnAnimationEndEvent();
				if (this.AnimationEndAction)
				{
					this.AnimationEndAction.Perform(null);
				}
				if (this.DestroyTargetOnAnimationEnd)
				{
					InstantiateUtility.Destroy(this.DestroyTargetOnAnimationEnd);
				}
			}
		}
		else
		{
			this.HasAnimationEndedTriggered = false;
		}
		if (this.HideWhenNotPlaying)
		{
			bool flag = !Mathf.Approximately(this.m_animator.Time, 0f) && !Mathf.Approximately(this.m_animator.Time, this.Duration);
			bool enabled = true;
			if (!flag)
			{
				this.m_hideCooldown -= Time.deltaTime;
			}
			else
			{
				this.m_hideCooldown = 0.06666667f;
			}
			if (this.m_hideCooldown <= 0f)
			{
				enabled = false;
			}
			this.m_renderer.enabled = enabled;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000947 RID: 2375 RVA: 0x00027E7D File Offset: 0x0002607D
	public bool AnimationEnded
	{
		get
		{
			return this.m_animator.AnimationEnded;
		}
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00027E8C File Offset: 0x0002608C
	public void ChangeMainTextureToAnimatorTexture()
	{
		if (!this.m_animator.Animation)
		{
			return;
		}
		Atlas atlas;
		AtlasSpriteTexture textureAtTime = this.m_animator.Animation.GetTextureAtTime(this.m_animator.Time, out atlas);
		if (textureAtTime == null || atlas == null || this.m_binder == null || textureAtTime == this.m_currentAtlasSpriteTexture)
		{
			return;
		}
		this.m_currentAtlasSpriteTexture = textureAtTime;
		Material material;
		if (!Application.isPlaying || this.IsInScene)
		{
			material = this.m_renderer.sharedMaterial;
		}
		else
		{
			material = this.m_renderer.material;
			this.m_madeMaterial = true;
		}
		if (this.m_mesh)
		{
			this.m_binder.BindTo(this.m_meshFilter, material, atlas, atlas.ScreenMode, textureAtTime);
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000949 RID: 2377 RVA: 0x00027F64 File Offset: 0x00026164
	private Atlas SettingsAtlas
	{
		get
		{
			foreach (Atlas atlas in this.DefaultAnimation.Atlases)
			{
				if (atlas != null)
				{
					return atlas;
				}
			}
			return null;
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00027FD4 File Offset: 0x000261D4
	public UberScreenMode GetExternalUberScreenMode()
	{
		if (this.DefaultAnimation == null)
		{
			return UberScreenMode.None;
		}
		Atlas settingsAtlas = this.SettingsAtlas;
		if (settingsAtlas == null)
		{
			return UberScreenMode.None;
		}
		return settingsAtlas.ScreenMode;
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00028010 File Offset: 0x00026210
	public float GetUberTweakValue()
	{
		if (this.DefaultAnimation == null)
		{
			return 0f;
		}
		Atlas settingsAtlas = this.SettingsAtlas;
		if (settingsAtlas == null)
		{
			return 0f;
		}
		return settingsAtlas.UberScreenTweak;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00028054 File Offset: 0x00026254
	public bool DoesProvideAtlas()
	{
		return this.DefaultAnimation != null && this.SettingsAtlas != null;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00028084 File Offset: 0x00026284
	public void SetAnimation(TextureAnimation textureAnimation, bool resetTime = true)
	{
		if (resetTime)
		{
			base.AnimatorDriver.CurrentTime = 0f;
		}
		this.m_animator.OnAnimationStart = new Action(this.AnimationStart);
		this.m_animator.SetAnimation(textureAnimation, resetTime);
		this.ChangeMainTextureToAnimatorTexture();
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x0600094E RID: 2382 RVA: 0x000280D4 File Offset: 0x000262D4
	public override float Duration
	{
		get
		{
			if (this.m_animator.Animation == null)
			{
				return 0f;
			}
			return base.AnimationCurveTimeToTime(this.m_animator.Animation.Duration);
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00028114 File Offset: 0x00026314
	public void AnimationStart()
	{
		if (Application.isPlaying && this.AnimationStartSound)
		{
			Sound.Play(this.AnimationStartSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000950 RID: 2384 RVA: 0x00028159 File Offset: 0x00026359
	public override bool IsLooping
	{
		get
		{
			return !(this.m_animator.Animation == null) && this.m_animator.Animation.Loop;
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000951 RID: 2385 RVA: 0x00028184 File Offset: 0x00026384
	public TextureAnimation CurrentAnimation
	{
		get
		{
			return (!Application.isPlaying) ? this.DefaultAnimation : this.m_animator.Animation;
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x000281B1 File Offset: 0x000263B1
	public void SetDirty()
	{
		this.ChangeMainTextureToAnimatorTexture();
	}

	// Token: 0x0400076F RID: 1903
	private const float c_hideCooldownStart = 0.06666667f;

	// Token: 0x04000770 RID: 1904
	public bool HideWhenNotPlaying;

	// Token: 0x04000771 RID: 1905
	public bool UseSpriteSpaceUvs;

	// Token: 0x04000772 RID: 1906
	public AnimationMeshingSettings MeshSettings = new AnimationMeshingSettings();

	// Token: 0x04000773 RID: 1907
	private AtlasSpriteTextureBinder m_binder;

	// Token: 0x04000774 RID: 1908
	private bool m_useSpriteSpaceUvs;

	// Token: 0x04000775 RID: 1909
	private Renderer m_renderer;

	// Token: 0x04000776 RID: 1910
	private bool m_editorEnabled;

	// Token: 0x04000777 RID: 1911
	private Mesh m_mesh;

	// Token: 0x04000778 RID: 1912
	[NotNull]
	public TextureAnimation DefaultAnimation;

	// Token: 0x04000779 RID: 1913
	public GameObject DestroyTargetOnAnimationEnd;

	// Token: 0x0400077A RID: 1914
	[PooledSafe]
	private TextureAnimator m_animator = new TextureAnimator();

	// Token: 0x0400077B RID: 1915
	public SoundProvider AnimationStartSound;

	// Token: 0x0400077C RID: 1916
	public ActionMethod AnimationEndAction;

	// Token: 0x0400077D RID: 1917
	[HideInInspector]
	public float PlaybackTime;

	// Token: 0x0400077E RID: 1918
	private MeshFilter m_meshFilter;

	// Token: 0x0400077F RID: 1919
	private bool m_madeMaterial;

	// Token: 0x04000780 RID: 1920
	private float m_hideCooldown = 0.06666667f;

	// Token: 0x04000781 RID: 1921
	private AtlasSpriteTexture m_currentAtlasSpriteTexture;

	// Token: 0x04000782 RID: 1922
	private float m_lastTime = -1f;

	// Token: 0x04000783 RID: 1923
	private TextureAnimation m_lastAnimation;
}
