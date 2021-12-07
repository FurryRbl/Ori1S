using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200069E RID: 1694
public class AmbienceZoneB : MonoBehaviour
{
	// Token: 0x06002904 RID: 10500 RVA: 0x000B1520 File Offset: 0x000AF720
	public void FixedUpdate()
	{
		Vector3 position = Characters.Current.Position;
		position.z = 0f;
		Bounds bounds = new Bounds(base.transform.position, base.transform.lossyScale);
		if (bounds.Contains(position))
		{
			if (!this.m_activated)
			{
				this.m_activated = true;
				this.ActivateAmbienceZone();
			}
		}
		else if (this.m_activated)
		{
			this.DeactiveAmbienceZone();
			this.m_activated = false;
		}
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x000B15A3 File Offset: 0x000AF7A3
	public void SetSoundProvider(SoundProvider soundProvider)
	{
		this.SoundProvider = soundProvider;
		if (this.m_activated)
		{
			this.DeactiveAmbienceZone();
			this.ActivateAmbienceZone();
		}
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x000B15C3 File Offset: 0x000AF7C3
	public void OnDestroy()
	{
		this.DeactiveAmbienceZone();
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x000B15CB File Offset: 0x000AF7CB
	public void OnDisable()
	{
		this.DeactiveAmbienceZone();
	}

	// Token: 0x06002908 RID: 10504 RVA: 0x000B15D3 File Offset: 0x000AF7D3
	public void ActivateAmbienceZone()
	{
		this.PlaySound();
		if (this.m_soundPlayer)
		{
			this.m_soundPlayer.FadeIn(this.FadeInDuration, false);
		}
		this.m_activated = true;
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x000B1604 File Offset: 0x000AF804
	public void DeactiveAmbienceZone()
	{
		if (this.m_soundPlayer)
		{
			this.m_soundPlayer.FadeOut(this.FadeOutDuration, true);
		}
		this.m_activated = false;
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x000B1630 File Offset: 0x000AF830
	public void PlaySound()
	{
		if (this.m_nullifyDelegate == null)
		{
			this.m_nullifyDelegate = delegate()
			{
				this.m_soundPlayer = null;
			};
		}
		if (this.m_soundPlayer)
		{
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
		}
		this.m_soundPlayer = Sound.PlayLooping(this.SoundProvider.GetSound(null), this.Target.position, this.m_nullifyDelegate);
	}

	// Token: 0x04002490 RID: 9360
	public SoundProvider SoundProvider;

	// Token: 0x04002491 RID: 9361
	public Transform Target;

	// Token: 0x04002492 RID: 9362
	public float FadeInDuration = 3f;

	// Token: 0x04002493 RID: 9363
	public float FadeOutDuration = 3f;

	// Token: 0x04002494 RID: 9364
	private bool m_activated;

	// Token: 0x04002495 RID: 9365
	private SoundPlayer m_soundPlayer;

	// Token: 0x04002496 RID: 9366
	private Action m_nullifyDelegate;
}
