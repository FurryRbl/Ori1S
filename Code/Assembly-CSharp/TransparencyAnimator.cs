using System;
using System.Collections.Generic;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class TransparencyAnimator : BaseAnimator
{
	// Token: 0x06000426 RID: 1062 RVA: 0x000113E8 File Offset: 0x0000F5E8
	// Note: this type is marked as 'beforefieldinit'.
	static TransparencyAnimator()
	{
		bool[] array = new bool[3];
		array[0] = true;
		array[1] = true;
		TransparencyAnimator.s_disableRenderer = array;
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0001142C File Offset: 0x0000F62C
	[ContextMenu("Print out renderer data")]
	public void PrintOutRendererData()
	{
		foreach (TransparencyAnimator.RendererData rendererData in this.m_rendererData)
		{
			if (rendererData.Renderer != null)
			{
			}
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x00011494 File Offset: 0x0000F694
	private int PropertyId
	{
		get
		{
			if (TransparencyAnimator.s_propIds == null)
			{
				TransparencyAnimator.s_propIds = new int[TransparencyAnimator.s_propNames.Length];
				for (int i = 0; i < TransparencyAnimator.s_propNames.Length; i++)
				{
					TransparencyAnimator.s_propIds[i] = Shader.PropertyToID(TransparencyAnimator.s_propNames[i]);
				}
			}
			return TransparencyAnimator.s_propIds[(int)this.Mode];
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x000114F3 File Offset: 0x0000F6F3
	private bool UseSharedMaterial
	{
		get
		{
			return (this.IsInScene && !this.m_forceUseRendererMaterial) || !Application.isPlaying;
		}
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00011516 File Offset: 0x0000F716
	public new void Awake()
	{
		this.m_forceUseRendererMaterial = (base.GetComponentInChildren<TextBox>() != null);
		base.Awake();
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00011530 File Offset: 0x0000F730
	private bool CanBeAnimated(Renderer r)
	{
		return !(r.sharedMaterial == null) && r.sharedMaterial.HasProperty("_Color") && r.GetComponent<UberGhostTrail>() == null;
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00011574 File Offset: 0x0000F774
	public override void CacheOriginals()
	{
		this.m_rendererData.Clear();
		this.m_renderers.Clear();
		this.AddChild(base.transform);
		if (this.AnimateChildren)
		{
			this.AddChildren(base.transform);
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x000115BC File Offset: 0x0000F7BC
	private void AddChild(Transform child)
	{
		Renderer component = child.GetComponent<Renderer>();
		if (component && this.CanBeAnimated(component) && !this.m_renderers.Contains(component))
		{
			this.m_rendererData.Add(new TransparencyAnimator.RendererData(component, this.PropertyId));
			this.m_renderers.Add(component);
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0001161C File Offset: 0x0000F81C
	private void AddChildren(Transform childTransform)
	{
		int childCount = childTransform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = childTransform.GetChild(i);
			TransparencyAnimator component = child.GetComponent<TransparencyAnimator>();
			if (component != null)
			{
				this.m_childTransparencyAnimators.Add(component);
			}
			else
			{
				CleverMenuItem component2 = child.GetComponent<CleverMenuItem>();
				if (component2 != null && component2.AnimateColors)
				{
					if (this.m_cleverMenuItems == null)
					{
						this.m_cleverMenuItems = new List<CleverMenuItem>();
					}
					this.m_cleverMenuItems.Add(component2);
				}
				this.AddChild(child);
				this.AddChildren(child);
			}
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000116C0 File Offset: 0x0000F8C0
	public static void Register(Transform child)
	{
		Transform parent = child.parent;
		while (parent)
		{
			TransparencyAnimator component = parent.GetComponent<TransparencyAnimator>();
			if (component && component.AnimateChildren)
			{
				component.ManuallyRegister(child);
				break;
			}
			parent = parent.parent;
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00011714 File Offset: 0x0000F914
	private void ManuallyRegister(Transform child)
	{
		if (!base.IsInitialized)
		{
			return;
		}
		TransparencyAnimator component = child.GetComponent<TransparencyAnimator>();
		if (component)
		{
			this.m_childTransparencyAnimators.Add(component);
			return;
		}
		CleverMenuItem component2 = child.GetComponent<CleverMenuItem>();
		if (component2 != null && component2.AnimateColors)
		{
			if (this.m_cleverMenuItems == null)
			{
				this.m_cleverMenuItems = new List<CleverMenuItem>();
			}
			this.m_cleverMenuItems.Add(component2);
			return;
		}
		this.AddChild(child);
		this.AddChildren(child);
		this.ApplyTransparency(true);
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x000117A4 File Offset: 0x0000F9A4
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.m_opacity = this.AnimationCurve.Evaluate(value);
		this.ApplyTransparency(false);
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x000117D4 File Offset: 0x0000F9D4
	public void ApplyTransparency(bool force = true)
	{
		float finalOpacity = this.FinalOpacity;
		if (!Mathf.Approximately(this.m_lastFinalOpacity, finalOpacity) || force)
		{
			this.m_lastFinalOpacity = finalOpacity;
			for (int i = 0; i < this.m_rendererData.Count; i++)
			{
				this.m_rendererData[i].SetRendererAlpha((int)this.Mode, this.PropertyId, this.UseSharedMaterial, finalOpacity);
			}
			for (int j = 0; j < this.m_childTransparencyAnimators.Count; j++)
			{
				this.m_childTransparencyAnimators[j].SetParentOpacity(finalOpacity);
			}
			if (this.m_cleverMenuItems != null)
			{
				for (int k = 0; k < this.m_cleverMenuItems.Count; k++)
				{
					this.m_cleverMenuItems[k].SetParentOpacity(finalOpacity);
				}
			}
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000118B4 File Offset: 0x0000FAB4
	public void SetParentOpacity(float opacity)
	{
		if (!Mathf.Approximately(opacity, this.m_parentOpacity))
		{
			this.m_parentOpacity = opacity;
			if (base.IsInitialized)
			{
				this.ApplyTransparency(true);
			}
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000434 RID: 1076 RVA: 0x000118EB File Offset: 0x0000FAEB
	public float FinalOpacity
	{
		get
		{
			return this.m_opacity * this.m_parentOpacity;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000435 RID: 1077 RVA: 0x000118FA File Offset: 0x0000FAFA
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00011910 File Offset: 0x0000FB10
	public override void RestoreToOriginalState()
	{
		this.m_parentOpacity = 1f;
		this.m_opacity = 1f;
		for (int i = 0; i < this.m_childTransparencyAnimators.Count; i++)
		{
			this.m_childTransparencyAnimators[i].RestoreToOriginalState();
		}
		for (int j = 0; j < this.m_rendererData.Count; j++)
		{
			this.m_rendererData[j].SetRendererAlpha((int)this.Mode, this.PropertyId, this.UseSharedMaterial, 1f);
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000437 RID: 1079 RVA: 0x000119A7 File Offset: 0x0000FBA7
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x0400037C RID: 892
	private static string[] s_propNames = new string[]
	{
		"_Color",
		"_MaskDissolveColor",
		"_AdditiveLayerColor"
	};

	// Token: 0x0400037D RID: 893
	private static bool[] s_disableRenderer;

	// Token: 0x0400037E RID: 894
	private static int[] s_propIds;

	// Token: 0x0400037F RID: 895
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04000380 RID: 896
	public bool AnimateChildren;

	// Token: 0x04000381 RID: 897
	public TransparencyAnimator.AnimateMode Mode;

	// Token: 0x04000382 RID: 898
	[PooledSafe]
	private readonly List<TransparencyAnimator.RendererData> m_rendererData = new List<TransparencyAnimator.RendererData>(4);

	// Token: 0x04000383 RID: 899
	[PooledSafe]
	private readonly List<TransparencyAnimator> m_childTransparencyAnimators = new List<TransparencyAnimator>(4);

	// Token: 0x04000384 RID: 900
	[PooledSafe]
	private List<CleverMenuItem> m_cleverMenuItems;

	// Token: 0x04000385 RID: 901
	private bool m_forceUseRendererMaterial;

	// Token: 0x04000386 RID: 902
	private float m_parentOpacity = 1f;

	// Token: 0x04000387 RID: 903
	private float m_opacity = 1f;

	// Token: 0x04000388 RID: 904
	[PooledSafe]
	private readonly HashSet<Renderer> m_renderers = new HashSet<Renderer>();

	// Token: 0x04000389 RID: 905
	private float m_lastFinalOpacity = 123456790f;

	// Token: 0x02000781 RID: 1921
	public enum AnimateMode
	{
		// Token: 0x04002857 RID: 10327
		Color,
		// Token: 0x04002858 RID: 10328
		Dissolve,
		// Token: 0x04002859 RID: 10329
		Additive
	}

	// Token: 0x02000782 RID: 1922
	private struct RendererData
	{
		// Token: 0x06002C9B RID: 11419 RVA: 0x000BF5F0 File Offset: 0x000BD7F0
		public RendererData(Renderer renderer, int id)
		{
			this.Renderer = renderer;
			this.OriginalAlpha = renderer.sharedMaterial.GetColor(id).a;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000BF620 File Offset: 0x000BD820
		public void SetRendererAlpha(int mode, int propertyID, bool useSharedMaterial, float value)
		{
			if (this.Renderer == null || this.Renderer.sharedMaterial == null)
			{
				return;
			}
			if (TransparencyAnimator.s_disableRenderer[mode])
			{
				this.Renderer.enabled = (value > 0.01f);
			}
			float a = value * this.OriginalAlpha;
			Material material = (!useSharedMaterial) ? this.Renderer.material : this.Renderer.sharedMaterial;
			Color color = material.GetColor(propertyID);
			color.a = a;
			material.SetColor(propertyID, color);
		}

		// Token: 0x0400285A RID: 10330
		public readonly float OriginalAlpha;

		// Token: 0x0400285B RID: 10331
		public readonly Renderer Renderer;
	}
}
