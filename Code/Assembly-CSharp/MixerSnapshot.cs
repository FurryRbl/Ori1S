using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class MixerSnapshot : ScriptableObject
{
	// Token: 0x170001BA RID: 442
	// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002185C File Offset: 0x0001FA5C
	public float Weight
	{
		get
		{
			switch (this.m_state)
			{
			case MixerSnapshot.MixerSnapshotState.FadingIn:
				return this.m_fadeTime / this.FadeInTime;
			case MixerSnapshot.MixerSnapshotState.FadingOut:
				return 1f - this.m_fadeTime / this.FadeOutTime;
			case MixerSnapshot.MixerSnapshotState.Active:
				return 1f;
			}
			return 0f;
		}
	}

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x060007CD RID: 1997 RVA: 0x000218B8 File Offset: 0x0001FAB8
	public MixerSnapshot.MixerSnapshotState State
	{
		get
		{
			return this.m_state;
		}
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000218C0 File Offset: 0x0001FAC0
	public void FadeIn()
	{
		MixerManager.Instance.RegisterActiveSnapshot(this);
		if (this.m_state == MixerSnapshot.MixerSnapshotState.FadingOut)
		{
			this.m_fadeTime = this.Weight * this.FadeInTime;
		}
		else
		{
			this.m_fadeTime = 0f;
		}
		this.m_state = MixerSnapshot.MixerSnapshotState.FadingIn;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0002190E File Offset: 0x0001FB0E
	public void FadeOut()
	{
		if (this.m_state == MixerSnapshot.MixerSnapshotState.FadingIn)
		{
			this.m_fadeTime = (1f - this.Weight) * this.FadeOutTime;
		}
		else
		{
			this.m_fadeTime = 0f;
		}
		this.m_state = MixerSnapshot.MixerSnapshotState.FadingOut;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002194C File Offset: 0x0001FB4C
	public void UpdateMixerSnapshotState(float timeDelta)
	{
		this.m_fadeTime += timeDelta;
		if (this.Weight >= 1f)
		{
			this.m_state = MixerSnapshot.MixerSnapshotState.Active;
		}
		else if (this.Weight <= 0f)
		{
			this.m_state = MixerSnapshot.MixerSnapshotState.Inactive;
		}
	}

	// Token: 0x04000642 RID: 1602
	public float FadeInTime = 1f;

	// Token: 0x04000643 RID: 1603
	public float FadeOutTime = 2f;

	// Token: 0x04000644 RID: 1604
	public MixerGroupSettings SnapshotSettings;

	// Token: 0x04000645 RID: 1605
	[NonSerialized]
	private float m_fadeTime;

	// Token: 0x04000646 RID: 1606
	[NonSerialized]
	private MixerSnapshot.MixerSnapshotState m_state = MixerSnapshot.MixerSnapshotState.Inactive;

	// Token: 0x02000734 RID: 1844
	public enum MixerSnapshotState
	{
		// Token: 0x0400272B RID: 10027
		FadingIn,
		// Token: 0x0400272C RID: 10028
		FadingOut,
		// Token: 0x0400272D RID: 10029
		Inactive,
		// Token: 0x0400272E RID: 10030
		Active
	}
}
