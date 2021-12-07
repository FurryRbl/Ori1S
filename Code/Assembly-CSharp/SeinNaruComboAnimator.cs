using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class SeinNaruComboAnimator : MonoBehaviour, IUberAtlasExternal
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x060002DD RID: 733 RVA: 0x0000BDA9 File Offset: 0x00009FA9
	private AtlasSpriteTexture NaruCurrentTexture
	{
		get
		{
			return Characters.Naru.PlatformBehaviour.Visuals.Animation.Animator.TextureAnimator.Texture;
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000BDD0 File Offset: 0x00009FD0
	public void Awake()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_meshFilter = base.GetComponent<MeshFilter>();
		this.m_mesh = new Mesh
		{
			name = "atlasSpriteTexture"
		};
		this.m_meshFilter.sharedMesh = this.m_mesh;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000BE1E File Offset: 0x0000A01E
	public void OnDestroy()
	{
		if (this.m_mesh)
		{
			UnityEngine.Object.DestroyObject(this.m_mesh);
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000BE3C File Offset: 0x0000A03C
	public void Start()
	{
		for (int i = 0; i < this.Atlases.Length; i++)
		{
			Atlas atlas = this.Atlases[i];
			if (!(atlas == null))
			{
				List<AtlasSpriteTexture> spriteTextures = atlas.SpriteTextures;
				foreach (AtlasSpriteTexture atlasSpriteTexture in spriteTextures)
				{
					this.m_nameToFrames.Add(atlasSpriteTexture.Name, new SeinNaruComboAnimator.AtlasAndSpriteTexture
					{
						Atlas = atlas,
						AtlasSpriteTexture = atlasSpriteTexture
					});
				}
			}
		}
		this.m_binder = new AtlasSpriteTextureBinder(AnimationMeshingSettings.Default, this.CorrectUvs, this.m_mesh);
		this.UpdateSpriteFrame();
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000BF14 File Offset: 0x0000A114
	public void UpdateSpriteFrame()
	{
		if (!this.Naru.SeinNaruComboEnabled)
		{
			if (this.m_renderer.enabled)
			{
				this.m_renderer.enabled = false;
			}
			return;
		}
		AtlasSpriteTexture naruCurrentTexture = this.NaruCurrentTexture;
		if (naruCurrentTexture == null)
		{
			return;
		}
		this.m_binder.UpdateSpriceSpaceUv(this.CorrectUvs);
		SeinNaruComboAnimator.AtlasAndSpriteTexture atlasAndSpriteTexture;
		if (this.m_nameToFrames.TryGetValue(naruCurrentTexture.Name, out atlasAndSpriteTexture))
		{
			this.m_binder.BindTo(this.m_meshFilter, this.m_renderer.sharedMaterial, atlasAndSpriteTexture.Atlas, atlasAndSpriteTexture.Atlas.ScreenMode, atlasAndSpriteTexture.AtlasSpriteTexture);
			this.m_renderer.enabled = true;
		}
		else
		{
			this.m_renderer.enabled = false;
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
	public void FixedUpdate()
	{
		this.UpdateSpriteFrame();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
	public void Update()
	{
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
	private Atlas SettingsAtlas
	{
		get
		{
			if (this.Atlases == null || this.Atlases.Length == 0)
			{
				return null;
			}
			foreach (Atlas atlas in this.Atlases)
			{
				if (atlas != null)
				{
					return atlas;
				}
			}
			return null;
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0000C03C File Offset: 0x0000A23C
	public UberScreenMode GetExternalUberScreenMode()
	{
		Atlas settingsAtlas = this.SettingsAtlas;
		if (settingsAtlas == null)
		{
			return UberScreenMode.None;
		}
		return settingsAtlas.ScreenMode;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0000C064 File Offset: 0x0000A264
	public float GetUberTweakValue()
	{
		Atlas settingsAtlas = this.SettingsAtlas;
		if (settingsAtlas == null)
		{
			return 0f;
		}
		return settingsAtlas.UberScreenTweak;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000C090 File Offset: 0x0000A290
	public bool DoesProvideAtlas()
	{
		return this.SettingsAtlas != null;
	}

	// Token: 0x0400020D RID: 525
	public Atlas[] Atlases;

	// Token: 0x0400020E RID: 526
	public Naru Naru;

	// Token: 0x0400020F RID: 527
	public bool CorrectUvs;

	// Token: 0x04000210 RID: 528
	private readonly Dictionary<string, SeinNaruComboAnimator.AtlasAndSpriteTexture> m_nameToFrames = new Dictionary<string, SeinNaruComboAnimator.AtlasAndSpriteTexture>();

	// Token: 0x04000211 RID: 529
	private MeshFilter m_meshFilter;

	// Token: 0x04000212 RID: 530
	private Renderer m_renderer;

	// Token: 0x04000213 RID: 531
	private Mesh m_mesh;

	// Token: 0x04000214 RID: 532
	private AtlasSpriteTextureBinder m_binder;

	// Token: 0x02000044 RID: 68
	private struct AtlasAndSpriteTexture
	{
		// Token: 0x04000215 RID: 533
		public Atlas Atlas;

		// Token: 0x04000216 RID: 534
		public AtlasSpriteTexture AtlasSpriteTexture;
	}
}
