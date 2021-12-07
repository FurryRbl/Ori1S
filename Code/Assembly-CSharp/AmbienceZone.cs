using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200069D RID: 1693
[ExecuteInEditMode]
public class AmbienceZone : MonoBehaviour
{
	// Token: 0x1700068B RID: 1675
	// (get) Token: 0x060028FA RID: 10490 RVA: 0x000B13D3 File Offset: 0x000AF5D3
	// (set) Token: 0x060028FB RID: 10491 RVA: 0x000B13DB File Offset: 0x000AF5DB
	public bool Activated { get; private set; }

	// Token: 0x060028FC RID: 10492 RVA: 0x000B13E4 File Offset: 0x000AF5E4
	public void Awake()
	{
		this.Bounds = new Rect
		{
			width = base.transform.localScale.x,
			height = base.transform.localScale.y,
			center = base.transform.position
		};
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x000B144B File Offset: 0x000AF64B
	public void SetSoundProvider(SoundProvider soundProvider)
	{
		this.SoundProvider = soundProvider;
		if (this.Activated)
		{
			this.DeactiveAmbienceZone();
			this.ActivateAmbienceZone();
		}
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x000B146B File Offset: 0x000AF66B
	public void OnDestroy()
	{
		if (Application.isPlaying)
		{
			this.DeactiveAmbienceZone();
		}
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000B147D File Offset: 0x000AF67D
	public void OnEnable()
	{
		AmbienceZone.All.Add(this);
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000B148A File Offset: 0x000AF68A
	public void OnDisable()
	{
		AmbienceZone.All.Remove(this);
		if (Application.isPlaying)
		{
			this.DeactiveAmbienceZone();
		}
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x000B14A8 File Offset: 0x000AF6A8
	public void ActivateAmbienceZone()
	{
		this.m_ambienceLayer = new Ambience.Layer(this.SoundProvider, this.FadeInDuration, this.FadeOutDuration, 0);
		Ambience.AddAmbienceLayer(this.m_ambienceLayer);
		this.Activated = true;
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x000B14E5 File Offset: 0x000AF6E5
	public void DeactiveAmbienceZone()
	{
		Ambience.RemoveAmbienceLayer(this.m_ambienceLayer);
		this.m_ambienceLayer = null;
		this.Activated = false;
	}

	// Token: 0x04002489 RID: 9353
	public static List<AmbienceZone> All = new List<AmbienceZone>();

	// Token: 0x0400248A RID: 9354
	public SoundProvider SoundProvider;

	// Token: 0x0400248B RID: 9355
	public float FadeInDuration = 3f;

	// Token: 0x0400248C RID: 9356
	public float FadeOutDuration = 3f;

	// Token: 0x0400248D RID: 9357
	private Ambience.Layer m_ambienceLayer;

	// Token: 0x0400248E RID: 9358
	public Rect Bounds;
}
