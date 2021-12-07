using System;
using System.Collections.Generic;
using Core;
using Sein.World;
using UnityEngine;

// Token: 0x02000446 RID: 1094
[ExecuteInEditMode]
public class FloatZone : MonoBehaviour, ISuspendable
{
	// Token: 0x06001E7B RID: 7803 RVA: 0x0008694C File Offset: 0x00084B4C
	public void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.BoundingRect = new Rect
		{
			width = base.transform.lossyScale.x,
			height = base.transform.lossyScale.y,
			center = base.transform.position
		};
		if (this.RequiresWindRestored && !Events.WindRestored)
		{
			base.gameObject.SetActive(false);
		}
		SuspensionManager.Register(this);
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x000869E5 File Offset: 0x00084BE5
	public void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x000869F8 File Offset: 0x00084BF8
	public void OnEnable()
	{
		FloatZone.All.Add(this);
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x00086A05 File Offset: 0x00084C05
	public void OnDisable()
	{
		FloatZone.All.Remove(this);
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x00086A14 File Offset: 0x00084C14
	public void FixedUpdate()
	{
		if (this.LoopSoundProvider == null)
		{
			return;
		}
		if (this.m_soundDescriptor == null)
		{
			this.m_soundDescriptor = this.LoopSoundProvider.GetSound(null);
		}
		if (this.m_lastLoopSound == null)
		{
			this.m_lastLoopSound = Sound.Play(this.m_soundDescriptor, base.transform.position, delegate()
			{
				this.m_lastLoopSound = null;
			});
		}
		else
		{
			this.m_soundDescriptor = null;
		}
	}

	// Token: 0x1700052A RID: 1322
	// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00086A95 File Offset: 0x00084C95
	// (set) Token: 0x06001E81 RID: 7809 RVA: 0x00086A9D File Offset: 0x00084C9D
	public bool IsSuspended { get; set; }

	// Token: 0x04001A36 RID: 6710
	public static List<FloatZone> All = new List<FloatZone>();

	// Token: 0x04001A37 RID: 6711
	public bool RequiresWindRestored;

	// Token: 0x04001A38 RID: 6712
	public Varying2DSoundProvider LoopSoundProvider;

	// Token: 0x04001A39 RID: 6713
	public float Acceleration = 30f;

	// Token: 0x04001A3A RID: 6714
	public float Deceleration = 1000f;

	// Token: 0x04001A3B RID: 6715
	public float TooFastDeceleration = 1000f;

	// Token: 0x04001A3C RID: 6716
	public float DesiredSpeed = 30f;

	// Token: 0x04001A3D RID: 6717
	public Rect BoundingRect;

	// Token: 0x04001A3E RID: 6718
	private SoundPlayer m_lastLoopSound;

	// Token: 0x04001A3F RID: 6719
	private SoundDescriptor m_soundDescriptor;
}
