using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200026A RID: 618
[ExecuteInEditMode]
public class MusicZone : MonoBehaviour
{
	// Token: 0x170003AB RID: 939
	// (get) Token: 0x060014BA RID: 5306 RVA: 0x0005D78B File Offset: 0x0005B98B
	// (set) Token: 0x060014BB RID: 5307 RVA: 0x0005D793 File Offset: 0x0005B993
	public bool Activated { get; set; }

	// Token: 0x060014BC RID: 5308 RVA: 0x0005D79C File Offset: 0x0005B99C
	public void Awake()
	{
		this.Bounds = new Rect
		{
			width = base.transform.localScale.x,
			height = base.transform.localScale.y,
			center = base.transform.position
		};
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x0005D803 File Offset: 0x0005BA03
	public void SetSoundProvider(SoundProvider soundProvider)
	{
		this.SoundProvider = soundProvider;
		if (this.Activated)
		{
			this.DeactiveMusicZone();
			this.ActivateMusicZone();
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x0005D823 File Offset: 0x0005BA23
	public void OnDestroy()
	{
		if (Application.isPlaying)
		{
			this.DeactiveMusicZone();
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0005D835 File Offset: 0x0005BA35
	public void OnEnable()
	{
		MusicZone.All.Add(this);
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x0005D842 File Offset: 0x0005BA42
	public void OnDisable()
	{
		MusicZone.All.Remove(this);
		if (Application.isPlaying)
		{
			this.DeactiveMusicZone();
		}
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x0005D860 File Offset: 0x0005BA60
	public void ActivateMusicZone()
	{
		this.m_musicLayer = new Music.Layer(this.SoundProvider, this.FadeInDuration, this.FadeOutDuration);
		Music.AddMusicLayer(this.m_musicLayer);
		this.Activated = true;
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x0005D891 File Offset: 0x0005BA91
	public void DeactiveMusicZone()
	{
		Music.RemoveMusicLayer(this.m_musicLayer);
		this.m_musicLayer = null;
		this.Activated = false;
	}

	// Token: 0x04001206 RID: 4614
	public static List<MusicZone> All = new List<MusicZone>();

	// Token: 0x04001207 RID: 4615
	public SoundProvider SoundProvider;

	// Token: 0x04001208 RID: 4616
	public float FadeInDuration = 3f;

	// Token: 0x04001209 RID: 4617
	public float FadeOutDuration = 3f;

	// Token: 0x0400120A RID: 4618
	private Music.Layer m_musicLayer;

	// Token: 0x0400120B RID: 4619
	public Rect Bounds;
}
