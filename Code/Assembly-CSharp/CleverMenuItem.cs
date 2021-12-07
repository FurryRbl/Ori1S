using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class CleverMenuItem : MonoBehaviour
{
	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06000A5C RID: 2652 RVA: 0x0002CDC5 File Offset: 0x0002AFC5
	// (remove) Token: 0x06000A5D RID: 2653 RVA: 0x0002CDDE File Offset: 0x0002AFDE
	public event Action HighlightCallback = delegate()
	{
	};

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x06000A5E RID: 2654 RVA: 0x0002CDF7 File Offset: 0x0002AFF7
	// (remove) Token: 0x06000A5F RID: 2655 RVA: 0x0002CE10 File Offset: 0x0002B010
	public event Action UnhighlightCallback = delegate()
	{
	};

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06000A60 RID: 2656 RVA: 0x0002CE29 File Offset: 0x0002B029
	// (remove) Token: 0x06000A61 RID: 2657 RVA: 0x0002CE42 File Offset: 0x0002B042
	public event Action PressedCallback = delegate()
	{
	};

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002CE5B File Offset: 0x0002B05B
	// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002CE63 File Offset: 0x0002B063
	public Transform Transform { get; set; }

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002CE6C File Offset: 0x0002B06C
	public bool IsVisible
	{
		get
		{
			return !this.Visible || this.Visible.Validate(null);
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002CE90 File Offset: 0x0002B090
	public bool IsActivated
	{
		get
		{
			if (this.Activated == null)
			{
				return base.gameObject.activeInHierarchy;
			}
			return base.gameObject.activeInHierarchy && this.Activated.Validate(null);
		}
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0002CEDC File Offset: 0x0002B0DC
	public bool IsPerforming()
	{
		PerformingAction performingAction = this.Pressed as PerformingAction;
		return performingAction != null && performingAction.IsPerforming;
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0002CF0C File Offset: 0x0002B10C
	public void Awake()
	{
		this.Transform = base.transform;
		this.TweenColor(this.Transition.NormalColor);
		this.m_tweenLastColor = this.m_tweenNextColor;
		this.m_colorID = Shader.PropertyToID(this.Transition.ColorName);
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0002CF58 File Offset: 0x0002B158
	public void Start()
	{
		this.m_renderers = null;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0002CF64 File Offset: 0x0002B164
	public void OnHighlight()
	{
		this.m_isHighlighted = true;
		if (this.Highlight)
		{
			this.Highlight.Perform(null);
		}
		this.HighlightCallback();
		this.TweenColor(this.Transition.HighlightedColor);
		if (this.HighlightAnimator)
		{
			this.HighlightAnimator.AnimatorDriver.ContinueForward();
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
	public bool IsHighlighted
	{
		get
		{
			return this.m_isHighlighted;
		}
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
	public void OnUnhighlight()
	{
		this.m_isHighlighted = false;
		if (this.Unhighlight)
		{
			this.Unhighlight.Perform(null);
		}
		this.UnhighlightCallback();
		this.TweenColor(this.Transition.NormalColor);
		if (this.HighlightAnimator)
		{
			this.HighlightAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0002D044 File Offset: 0x0002B244
	public void OnPressed()
	{
		if (!this.IsActivated)
		{
			return;
		}
		if (this.Pressed)
		{
			this.Pressed.Perform(null);
		}
		this.PressedCallback();
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0002D07C File Offset: 0x0002B27C
	public void TweenColor(Color color)
	{
		if (!this.AnimateColors)
		{
			return;
		}
		this.m_tweenLastColor = this.m_tweenNextColor;
		this.m_tweenNextColor = color;
		this.m_tweenPlay = true;
		this.m_tweenTime = 0f;
		this.m_renderers = null;
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
	public void FixedUpdate()
	{
		if (this.m_tweenPlay)
		{
			this.m_tweenTime += Time.deltaTime / this.Transition.TweenDuration;
			if (this.m_tweenTime > 1f)
			{
				this.m_tweenPlay = false;
				this.m_tweenTime = 1f;
			}
			this.ApplyColors();
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0002D124 File Offset: 0x0002B324
	public void ApplyColors()
	{
		if (this.m_renderers == null)
		{
			this.m_renderers = ((!this.ColorTarget) ? base.GetComponentsInChildren<Renderer>() : this.ColorTarget.GetComponentsInChildren<Renderer>());
		}
		Color materialColor = Color.Lerp(this.m_tweenLastColor, this.m_tweenNextColor, this.m_tweenTime);
		materialColor.a *= this.m_parentOpacity * this.m_opacity;
		this.MaterialColor = materialColor;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0002D1A2 File Offset: 0x0002B3A2
	public void SetParentOpacity(float opacity)
	{
		this.m_parentOpacity = opacity;
		this.ApplyColors();
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0002D1B1 File Offset: 0x0002B3B1
	public void SetOpacity(float opacity)
	{
		this.m_opacity = opacity;
		this.ApplyColors();
	}

	// Token: 0x17000239 RID: 569
	// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0002D1C0 File Offset: 0x0002B3C0
	public Color MaterialColor
	{
		set
		{
			if (this.m_renderers == null)
			{
				return;
			}
			foreach (Renderer renderer in this.m_renderers)
			{
				if (renderer)
				{
					renderer.material.SetColor(this.m_colorID, value);
				}
			}
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002D215 File Offset: 0x0002B415
	public Vector3 Position
	{
		get
		{
			return this.Transform.position;
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0002D224 File Offset: 0x0002B424
	public void RefreshVisible()
	{
		if (this.Visible)
		{
			base.gameObject.SetActive(this.Visible.Validate(null));
		}
		else
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0002D26C File Offset: 0x0002B46C
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(base.transform.position + this.Center, this.Size);
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002D2A4 File Offset: 0x0002B4A4
	public Rect Bounds
	{
		get
		{
			if (this.m_cachedPosition != this.Transform.position)
			{
				this.m_boundsInitialized = false;
				this.m_cachedPosition = this.Transform.position;
			}
			if (!this.m_boundsInitialized)
			{
				this.m_boundsInitialized = true;
				this.m_bounds = new Rect
				{
					width = this.Size.x,
					height = this.Size.y,
					center = this.Position + this.Center
				};
			}
			return this.m_bounds;
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0002D351 File Offset: 0x0002B551
	public void SetBounds(Rect rect)
	{
		this.m_boundsInitialized = true;
		this.m_bounds = rect;
		this.m_cachedPosition = this.Transform.position;
	}

	// Token: 0x04000876 RID: 2166
	public ActionMethod Highlight;

	// Token: 0x04000877 RID: 2167
	public ActionMethod Unhighlight;

	// Token: 0x04000878 RID: 2168
	public ActionMethod Pressed;

	// Token: 0x04000879 RID: 2169
	public Condition Activated;

	// Token: 0x0400087A RID: 2170
	public Condition Visible;

	// Token: 0x0400087B RID: 2171
	private Color m_tweenLastColor;

	// Token: 0x0400087C RID: 2172
	private Color m_tweenNextColor;

	// Token: 0x0400087D RID: 2173
	public Vector2 Size;

	// Token: 0x0400087E RID: 2174
	public Vector2 Center;

	// Token: 0x0400087F RID: 2175
	public BaseAnimator HighlightAnimator;

	// Token: 0x04000880 RID: 2176
	public GameObject ColorTarget;

	// Token: 0x04000881 RID: 2177
	public CleverMenuItem.TransitionSettings Transition;

	// Token: 0x04000882 RID: 2178
	public bool AnimateColors = true;

	// Token: 0x04000883 RID: 2179
	public float Space;

	// Token: 0x04000884 RID: 2180
	private bool m_isHighlighted;

	// Token: 0x04000885 RID: 2181
	private float m_tweenTime;

	// Token: 0x04000886 RID: 2182
	private bool m_tweenPlay;

	// Token: 0x04000887 RID: 2183
	private bool m_boundsInitialized;

	// Token: 0x04000888 RID: 2184
	private Rect m_bounds;

	// Token: 0x04000889 RID: 2185
	private float m_parentOpacity = 1f;

	// Token: 0x0400088A RID: 2186
	private float m_opacity = 1f;

	// Token: 0x0400088B RID: 2187
	private Renderer[] m_renderers;

	// Token: 0x0400088C RID: 2188
	private Vector3 m_cachedPosition;

	// Token: 0x0400088D RID: 2189
	private int m_colorID;

	// Token: 0x02000476 RID: 1142
	[Serializable]
	public class TransitionSettings
	{
		// Token: 0x04001B0A RID: 6922
		public string ColorName = "_Color";

		// Token: 0x04001B0B RID: 6923
		public Color NormalColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);

		// Token: 0x04001B0C RID: 6924
		public Color HighlightedColor = Color.white / 2f;

		// Token: 0x04001B0D RID: 6925
		public Color DisabledColor = Color.white / 2f;

		// Token: 0x04001B0E RID: 6926
		public float TweenDuration = 0.2f;
	}
}
