using System;
using UnityEngine;

// Token: 0x02000733 RID: 1843
public class MixerSnapshotZone : MonoBehaviour
{
	// Token: 0x170006E8 RID: 1768
	// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000BA360 File Offset: 0x000B8560
	public Bounds Bounds
	{
		get
		{
			Vector3 localScale = base.transform.localScale;
			localScale.z = 1000f;
			return new Bounds(base.transform.position, localScale);
		}
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x000BA396 File Offset: 0x000B8596
	private void OnEnable()
	{
		this.m_isCurrentlyActive = false;
		MixerManager.Instance.RegisterSnapshotZone(this);
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x000BA3AC File Offset: 0x000B85AC
	private void OnDisable()
	{
		MixerManager.Instance.DeregisterSnapshotZone(this);
		if (this.m_isCurrentlyActive)
		{
			this.Snapshot.FadeOut();
		}
		this.m_isCurrentlyActive = false;
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x000BA3E4 File Offset: 0x000B85E4
	public void UpdateSnapshotZoneState(bool isZoneActive)
	{
		if (this.Snapshot == null)
		{
			return;
		}
		if (this.m_isCurrentlyActive != isZoneActive)
		{
			if (isZoneActive)
			{
				this.Snapshot.FadeIn();
			}
			else
			{
				this.Snapshot.FadeOut();
			}
		}
		this.m_isCurrentlyActive = isZoneActive;
	}

	// Token: 0x04002728 RID: 10024
	public MixerSnapshot Snapshot;

	// Token: 0x04002729 RID: 10025
	private bool m_isCurrentlyActive;
}
