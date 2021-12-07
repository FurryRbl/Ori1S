using System;
using Game;
using UnityEngine;

// Token: 0x020009CA RID: 2506
public class WindVent : SaveSerialize, ISuspendable
{
	// Token: 0x0600369F RID: 13983 RVA: 0x000E5770 File Offset: 0x000E3970
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_currentStateTime += Time.deltaTime;
		switch (this.m_state)
		{
		case WindVent.State.InitialState:
			if (this.StartAutomatically && this.m_currentStateTime >= this.TimeOffset)
			{
				this.StartAnticipation();
			}
			break;
		case WindVent.State.Anticipation:
			if (this.m_currentStateTime >= this.AnticipationDuration)
			{
				this.StartBurst();
			}
			break;
		case WindVent.State.Burst:
			if (this.m_currentStateTime >= this.BurstDuration)
			{
				this.StartCooldown();
			}
			break;
		case WindVent.State.CoolDown:
			if (this.Looping && this.m_currentStateTime >= this.CooldownDuration)
			{
				this.StartAnticipation();
			}
			break;
		}
	}

	// Token: 0x060036A0 RID: 13984 RVA: 0x000E5844 File Offset: 0x000E3A44
	public void StartAnticipation()
	{
		this.ChangeState(WindVent.State.Anticipation);
		if (UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 7f))
		{
			InstantiateUtility.Instantiate(this.AnticipationEffect, base.transform.position, base.transform.rotation);
		}
	}

	// Token: 0x060036A1 RID: 13985 RVA: 0x000E589C File Offset: 0x000E3A9C
	public void StartBurst()
	{
		this.ChangeState(WindVent.State.Burst);
		if (UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 7f))
		{
			InstantiateUtility.Instantiate(this.BurstEffect, base.transform.position, base.transform.rotation);
		}
		if (this.ImpactEffectLocator != null && this.ImpactEffect != null && UI.Cameras.Current.IsOnScreenPadded(this.ImpactEffectLocator.position, 7f))
		{
			InstantiateUtility.Instantiate(this.ImpactEffect, this.ImpactEffectLocator.position, this.ImpactEffectLocator.rotation);
		}
		this.m_hasErrupted = true;
		this.ActivateVentGraphic(true);
	}

	// Token: 0x060036A2 RID: 13986 RVA: 0x000E5962 File Offset: 0x000E3B62
	public void ActivateVentGraphic(bool active)
	{
		if (this.VentGraphic != null)
		{
			this.VentGraphic.SetActive(active);
		}
		this.WindBeam.SetActive(active);
	}

	// Token: 0x060036A3 RID: 13987 RVA: 0x000E5990 File Offset: 0x000E3B90
	public void ChangeState(WindVent.State state)
	{
		this.m_currentStateTime = 0f;
		this.m_state = state;
		switch (state)
		{
		case WindVent.State.InitialState:
			this.KillZone.SetActive(false);
			this.DisableBeamParticleEmission();
			this.WindAnimator.AnimatorDriver.SetForward();
			this.WindAnimator.AnimatorDriver.Stop();
			break;
		case WindVent.State.Anticipation:
			this.KillZone.SetActive(false);
			this.WindAnimator.AnimatorDriver.SetForward();
			this.WindAnimator.AnimatorDriver.Stop();
			this.DisableBeamParticleEmission();
			break;
		case WindVent.State.Burst:
			this.KillZone.SetActive(true);
			this.WindAnimator.AnimatorDriver.RestartForward();
			this.EnableBeamParticleEmission();
			break;
		case WindVent.State.CoolDown:
			this.KillZone.SetActive(false);
			this.WindAnimator.AnimatorDriver.RestartBackwards();
			this.DisableBeamParticleEmission();
			break;
		}
	}

	// Token: 0x060036A4 RID: 13988 RVA: 0x000E5A88 File Offset: 0x000E3C88
	public void StartCooldown()
	{
		this.ChangeState(WindVent.State.CoolDown);
	}

	// Token: 0x060036A5 RID: 13989 RVA: 0x000E5A91 File Offset: 0x000E3C91
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_windBeamParticleSystems = this.WindBeam.GetComponentsInChildren<ParticleSystem>();
	}

	// Token: 0x060036A6 RID: 13990 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
	}

	// Token: 0x060036A7 RID: 13991 RVA: 0x000E5ABE File Offset: 0x000E3CBE
	public void Start()
	{
		this.ChangeState(WindVent.State.InitialState);
		this.ActivateVentGraphic(false);
	}

	// Token: 0x060036A8 RID: 13992 RVA: 0x000E5AD0 File Offset: 0x000E3CD0
	public void EnableBeamParticleEmission()
	{
		for (int i = 0; i < this.m_windBeamParticleSystems.Length; i++)
		{
			ParticleSystem particleSystem = this.m_windBeamParticleSystems[i];
			particleSystem.enableEmission = true;
		}
	}

	// Token: 0x060036A9 RID: 13993 RVA: 0x000E5B08 File Offset: 0x000E3D08
	public void DisableBeamParticleEmission()
	{
		for (int i = 0; i < this.m_windBeamParticleSystems.Length; i++)
		{
			ParticleSystem particleSystem = this.m_windBeamParticleSystems[i];
			particleSystem.enableEmission = false;
		}
	}

	// Token: 0x060036AA RID: 13994 RVA: 0x000E5B40 File Offset: 0x000E3D40
	public override void Serialize(Archive ar)
	{
		this.m_currentStateTime = ar.Serialize(this.m_currentStateTime);
		this.m_state = (WindVent.State)ar.Serialize((int)this.m_state);
		ar.Serialize(ref this.m_hasErrupted);
		if (ar.Reading)
		{
			this.ChangeState(this.m_state);
			this.ActivateVentGraphic(this.m_hasErrupted);
		}
	}

	// Token: 0x1700087B RID: 2171
	// (get) Token: 0x060036AB RID: 13995 RVA: 0x000E5BA0 File Offset: 0x000E3DA0
	// (set) Token: 0x060036AC RID: 13996 RVA: 0x000E5BA8 File Offset: 0x000E3DA8
	public bool IsSuspended { get; set; }

	// Token: 0x04003182 RID: 12674
	public bool StartAutomatically = true;

	// Token: 0x04003183 RID: 12675
	public bool Looping = true;

	// Token: 0x04003184 RID: 12676
	public float TimeOffset;

	// Token: 0x04003185 RID: 12677
	public float AnticipationDuration = 1f;

	// Token: 0x04003186 RID: 12678
	public float BurstDuration = 1f;

	// Token: 0x04003187 RID: 12679
	public float CooldownDuration = 5f;

	// Token: 0x04003188 RID: 12680
	public GameObject AnticipationEffect;

	// Token: 0x04003189 RID: 12681
	public GameObject BurstEffect;

	// Token: 0x0400318A RID: 12682
	public GameObject ImpactEffect;

	// Token: 0x0400318B RID: 12683
	public Transform ImpactEffectLocator;

	// Token: 0x0400318C RID: 12684
	public GameObject VentGraphic;

	// Token: 0x0400318D RID: 12685
	public BaseAnimator WindAnimator;

	// Token: 0x0400318E RID: 12686
	public GameObject WindBeam;

	// Token: 0x0400318F RID: 12687
	public GameObject KillZone;

	// Token: 0x04003190 RID: 12688
	private float m_currentStateTime;

	// Token: 0x04003191 RID: 12689
	private ParticleSystem[] m_windBeamParticleSystems;

	// Token: 0x04003192 RID: 12690
	private bool m_hasErrupted;

	// Token: 0x04003193 RID: 12691
	private WindVent.State m_state;

	// Token: 0x020009CE RID: 2510
	public enum State
	{
		// Token: 0x040031A4 RID: 12708
		InitialState,
		// Token: 0x040031A5 RID: 12709
		Anticipation,
		// Token: 0x040031A6 RID: 12710
		Burst,
		// Token: 0x040031A7 RID: 12711
		CoolDown
	}
}
