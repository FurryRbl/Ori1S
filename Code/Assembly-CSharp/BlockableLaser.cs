using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020002B6 RID: 694
[ExecuteInEditMode]
public class BlockableLaser : MonoBehaviour, ISuspendable
{
	// Token: 0x170003DD RID: 989
	// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0006058E File Offset: 0x0005E78E
	// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00060598 File Offset: 0x0005E798
	public bool Activated
	{
		get
		{
			return this.m_activated;
		}
		set
		{
			if (this.m_activated == value)
			{
				return;
			}
			this.m_activated = value;
			if (this.m_activated)
			{
				this.OnActivated();
			}
			else
			{
				this.OnDeactivate();
			}
		}
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000605D8 File Offset: 0x0005E7D8
	private void OnActivated()
	{
		if (this.m_damageCollider)
		{
			this.m_damageCollider.enabled = true;
		}
		this.LaserBeam.BeamEngageAnimator.AnimatorDriver.RestartForward();
		if (UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 5f))
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.LaserBeam.BurstEffect, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.transform;
		}
		for (int i = 0; i < this.LaserBeam.BeamParticleSystems.Length; i++)
		{
			ParticleSystem particleSystem = this.LaserBeam.BeamParticleSystems[i];
			particleSystem.enableEmission = true;
		}
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000606A8 File Offset: 0x0005E8A8
	private void OnDeactivate()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_loopSoundPlayer))
		{
			this.m_loopSoundPlayer.FadeOut(0.2f, true);
		}
		if (this.m_damageCollider)
		{
			this.m_damageCollider.enabled = false;
		}
		this.DispatchOfTheLastImpactLoopEffect();
		this.m_previousHitCollider = null;
		this.m_lastImpactPosition = new Vector3(1E+11f, 1E+11f, 0f);
		this.LaserBeam.BeamEngageAnimator.AnimatorDriver.RestartBackwards();
		if (UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 5f))
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.LaserBeam.StopEffect, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.transform;
		}
		for (int i = 0; i < this.LaserBeam.BeamParticleSystems.Length; i++)
		{
			ParticleSystem particleSystem = this.LaserBeam.BeamParticleSystems[i];
			particleSystem.enableEmission = false;
		}
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000607C0 File Offset: 0x0005E9C0
	public void DoAnticipation()
	{
		if (UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 5f))
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.LaserBeam.AnticipationEffect, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.transform;
		}
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x00060829 File Offset: 0x0005EA29
	private void OnEnable()
	{
		if (!Application.isPlaying)
		{
			this.m_damageCollider = this.LaserBeam.GetComponentInChildren<Collider>();
		}
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x00060846 File Offset: 0x0005EA46
	private void OnDisable()
	{
		this.DispatchOfTheLastImpactLoopEffect();
		if (!InstantiateUtility.IsDestroyed(this.m_loopSoundPlayer))
		{
			this.m_loopSoundPlayer.FadeOut(0.2f, true);
		}
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x00060870 File Offset: 0x0005EA70
	private void Awake()
	{
		if (this.LaserBeam)
		{
			this.m_laserBeamTilingAdjusters = this.LaserBeam.gameObject.GetComponentsInChildren<TextureTilingAdjuster>();
		}
		else
		{
			this.m_laserBeamTilingAdjusters = new TextureTilingAdjuster[0];
		}
		this.m_activated = true;
		this.m_damageCollider = this.LaserBeam.GetComponentInChildren<Collider>();
		this.CurrentLaserLength = 0f;
		SuspensionManager.Register(this);
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000608DD File Offset: 0x0005EADD
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x000608E8 File Offset: 0x0005EAE8
	private void DealLaserDamage(GameObject hitObject)
	{
		IDamageReciever damageReciever = hitObject.FindComponentInChildren<IDamageReciever>();
		if (damageReciever == null)
		{
			return;
		}
		Vector2 vector = hitObject.transform.position - base.transform.position;
		Damage damage = new Damage((float)this.LaserBeam.DamageAmount, vector.normalized, base.transform.position, this.LaserBeam.LaserDamageType, base.gameObject);
		damageReciever.OnRecieveDamage(damage);
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x00060960 File Offset: 0x0005EB60
	private void FixedUpdate()
	{
		if (this.m_isSuspended)
		{
			return;
		}
		this.PerformLaserLogic();
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x00060974 File Offset: 0x0005EB74
	private void PerformLaserLogic()
	{
		if (!this.Activated)
		{
			return;
		}
		Vector3 vector;
		Vector3 v;
		float num;
		Collider collider;
		this.PerformLaserRaycast(out vector, out v, out num, out collider);
		bool flag = false;
		Vector3 v2 = base.transform.TransformDirection(this.LaserDirection);
		if (Application.isPlaying)
		{
			bool flag2 = this.IsLaserOnScreen(vector);
			if (flag2 != this.m_wasVisibleOnScreen)
			{
				this.LaserBeam.gameObject.SetActive(flag2);
			}
			this.m_wasVisibleOnScreen = flag2;
			if (!flag2)
			{
				this.DispatchOfTheLastImpactLoopEffect();
				this.m_lastImpactPosition = vector;
				this.m_previousHitCollider = collider;
				return;
			}
			if (collider != this.m_previousHitCollider)
			{
				if (collider != null)
				{
					this.DealLaserDamage(collider.gameObject);
					flag = (Vector3.Distance(this.m_lastImpactPosition, vector) > 1f);
				}
			}
			else if (collider != null)
			{
				flag = (Vector3.Distance(this.m_lastImpactPosition, vector) > 5f);
			}
			if (collider && collider.CompareTag("Player"))
			{
				flag = false;
			}
			if (flag || (InstantiateUtility.IsDestroyed(this.m_laserImpactLoopEffect) && collider != null))
			{
				Vector3 position = vector;
				if (UI.Cameras.Current.IsOnScreenPadded(position, 5f))
				{
					GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.LaserBeam.ImpactEffect, position, Quaternion.identity);
					gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(v) + 90f);
				}
			}
			if (collider == null)
			{
				this.DispatchOfTheLastImpactLoopEffect();
			}
			else
			{
				if (InstantiateUtility.IsDestroyed(this.m_laserImpactLoopEffect) && UI.Cameras.Current.IsOnScreenPadded(vector, 5f))
				{
					this.m_laserImpactLoopEffect = (GameObject)InstantiateUtility.Instantiate(this.LaserBeam.ImpactPointLoopEffect, vector, Quaternion.identity);
					this.m_laserImpactLoopEffect.GetComponentsInChildren<ParticleSystem>(this.s_particleSystemList);
					for (int i = 0; i < this.s_particleSystemList.Count; i++)
					{
						ParticleSystem particleSystem = this.s_particleSystemList[i];
						particleSystem.enableEmission = true;
					}
					this.s_particleSystemList.Clear();
				}
				if (!InstantiateUtility.IsDestroyed(this.m_laserImpactLoopEffect))
				{
					this.m_laserImpactLoopEffect.transform.position = vector;
					this.m_laserImpactLoopEffect.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(v) + 90f);
				}
			}
			if (collider)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (attachedRigidbody && !attachedRigidbody.CompareTag("Player"))
				{
					attachedRigidbody.AddForceSafe(v2.normalized * this.LaserBeamForce);
				}
			}
		}
		Vector3 localScale = this.LaserBeam.BeamLenghtScaleTransform.localScale;
		localScale.y = num;
		this.LaserBeam.BeamLenghtScaleTransform.localScale = localScale;
		this.CurrentLaserLength = num;
		this.BeamRotationPivot.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(v2) - 90f);
		foreach (TextureTilingAdjuster textureTilingAdjuster in this.m_laserBeamTilingAdjusters)
		{
			textureTilingAdjuster.ScaleMultiplier = new Vector2(1f, num / this.LaserBeam.LaserBeamSizeToTilingRatio);
		}
		this.m_lastImpactPosition = vector;
		this.m_previousHitCollider = collider;
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x00060D14 File Offset: 0x0005EF14
	private void PerformLaserRaycast(out Vector3 laserEndPoint, out Vector3 hitNormal, out float bestDistance, out Collider hitCollider)
	{
		Vector3 vector = base.transform.TransformDirection(this.LaserDirection);
		Vector3 position = base.transform.position;
		laserEndPoint = position + vector * this.LaserMaxDistance;
		hitCollider = null;
		hitNormal = Vector3.up;
		bestDistance = this.LaserMaxDistance;
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(base.transform.position, vector), out raycastHit, this.LaserMaxDistance, this.BlockingLayers.value))
		{
			laserEndPoint = raycastHit.point;
			bestDistance = raycastHit.distance + 0.04f;
			hitCollider = raycastHit.collider;
			hitNormal = raycastHit.normal;
		}
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00060DD0 File Offset: 0x0005EFD0
	private bool IsLaserOnScreen(Vector3 endPoint)
	{
		Vector3 position = base.transform.position;
		GameplayCamera current = UI.Cameras.Current;
		if (current == null)
		{
			return true;
		}
		float padding = 6f;
		return current.IsOnScreenPadded(Vector3.Lerp(endPoint, position, 0f), padding) || current.IsOnScreenPadded(Vector3.Lerp(endPoint, position, 0.2f), padding) || current.IsOnScreenPadded(Vector3.Lerp(endPoint, position, 0.4f), padding) || current.IsOnScreenPadded(Vector3.Lerp(endPoint, position, 0.6f), padding) || current.IsOnScreenPadded(Vector3.Lerp(endPoint, position, 0.8f), padding) || current.IsOnScreenPadded(Vector3.Lerp(endPoint, position, 1f), padding);
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x060015DE RID: 5598 RVA: 0x00060E91 File Offset: 0x0005F091
	// (set) Token: 0x060015DF RID: 5599 RVA: 0x00060E99 File Offset: 0x0005F099
	public float CurrentLaserLength { get; private set; }

	// Token: 0x060015E0 RID: 5600 RVA: 0x00060EA4 File Offset: 0x0005F0A4
	private void DispatchOfTheLastImpactLoopEffect()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_laserImpactLoopEffect))
		{
			foreach (ParticleSystem particleSystem in this.m_laserImpactLoopEffect.GetComponentsInChildren<ParticleSystem>())
			{
				particleSystem.enableEmission = false;
			}
			foreach (SpeedBasedEmissionRateMultiplier speedBasedEmissionRateMultiplier in this.m_laserImpactLoopEffect.GetComponentsInChildren<SpeedBasedEmissionRateMultiplier>())
			{
				speedBasedEmissionRateMultiplier.HaltEmission();
			}
			this.m_laserImpactLoopEffect.GetComponent<BaseAnimator>().AnimatorDriver.RestartForward();
			InstantiateUtility.Destroy(this.m_laserImpactLoopEffect, 3f);
			this.m_laserImpactLoopEffect = null;
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00060F49 File Offset: 0x0005F149
	// (set) Token: 0x060015E2 RID: 5602 RVA: 0x00060F51 File Offset: 0x0005F151
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x040012B5 RID: 4789
	public float LaserMaxDistance = 5f;

	// Token: 0x040012B6 RID: 4790
	public LayerMask BlockingLayers;

	// Token: 0x040012B7 RID: 4791
	public Vector3 LaserDirection = Vector3.up;

	// Token: 0x040012B8 RID: 4792
	public float LaserBeamForce = 100f;

	// Token: 0x040012B9 RID: 4793
	public LaserBeam LaserBeam;

	// Token: 0x040012BA RID: 4794
	public Transform BeamRotationPivot;

	// Token: 0x040012BB RID: 4795
	private Collider m_damageCollider;

	// Token: 0x040012BC RID: 4796
	private readonly List<ParticleSystem> s_particleSystemList = new List<ParticleSystem>();

	// Token: 0x040012BD RID: 4797
	private TextureTilingAdjuster[] m_laserBeamTilingAdjusters;

	// Token: 0x040012BE RID: 4798
	private bool m_activated;

	// Token: 0x040012BF RID: 4799
	private bool m_isSuspended;

	// Token: 0x040012C0 RID: 4800
	private SoundPlayer m_loopSoundPlayer;

	// Token: 0x040012C1 RID: 4801
	private Collider m_previousHitCollider;

	// Token: 0x040012C2 RID: 4802
	private Vector3 m_lastImpactPosition;

	// Token: 0x040012C3 RID: 4803
	private GameObject m_laserImpactLoopEffect;

	// Token: 0x040012C4 RID: 4804
	private bool m_wasVisibleOnScreen = true;
}
