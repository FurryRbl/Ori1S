using System;
using Core;
using UnityEngine;

// Token: 0x0200091D RID: 2333
public class SequencePlatformB : SaveSerialize, ISuspendable
{
	// Token: 0x060033BB RID: 13243 RVA: 0x000D9BE4 File Offset: 0x000D7DE4
	public void SetToAppear(float delay)
	{
		this.m_delayTillVisible = delay;
		if (this.m_delayTillVisible == 0f)
		{
			this.Activated = true;
		}
	}

	// Token: 0x1700082A RID: 2090
	// (get) Token: 0x060033BD RID: 13245 RVA: 0x000D9DB1 File Offset: 0x000D7FB1
	// (set) Token: 0x060033BC RID: 13244 RVA: 0x000D9C04 File Offset: 0x000D7E04
	public bool Activated
	{
		get
		{
			return this.m_activated;
		}
		set
		{
			this.m_activated = value;
			if (this.m_activated)
			{
				foreach (FlipPlantLogic flipPlantLogic in this.m_flipPlants)
				{
					flipPlantLogic.GoUp();
				}
				foreach (BaseAnimator baseAnimator in this.BaseAnimators)
				{
					baseAnimator.AnimatorDriver.ContinueForward();
				}
				this.m_animatingFloat.Speed = 1f;
				if (this.OnActivateSoundProvider)
				{
					Sound.Play(this.OnActivateSoundProvider.GetSound(null), base.transform.position, null);
				}
				if (this.OnActivatedSettings.TriggerNextPlatform && this.NextPlatform)
				{
					this.NextPlatform.SetToAppear(this.OnActivatedSettings.DelayToNextPlatformAppear);
				}
			}
			else
			{
				foreach (FlipPlantLogic flipPlantLogic2 in this.m_flipPlants)
				{
					flipPlantLogic2.GoDown();
				}
				foreach (BaseAnimator baseAnimator2 in this.BaseAnimators)
				{
					baseAnimator2.AnimatorDriver.ContinueBackwards();
				}
				this.m_animatingFloat.Speed = -1f;
				this.m_touchedTime = 0f;
				this.m_activatedTime = 0f;
				this.m_touched = false;
				if (this.OnDeactivateSoundProvider)
				{
					Sound.Play(this.OnDeactivateSoundProvider.GetSound(null), base.transform.position, null);
				}
			}
		}
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x000D9DBC File Offset: 0x000D7FBC
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_flipPlants = base.GetComponentsInChildren<FlipPlantLogic>();
		this.m_colliders = base.GetComponentsInChildren<Collider>();
		this.m_animatingFloat.Min = 0f;
		this.m_animatingFloat.Max = this.ColliderSettings.ActivateDelay + this.ColliderSettings.DeactivateDelay;
	}

	// Token: 0x060033BF RID: 13247 RVA: 0x000D9E1F File Offset: 0x000D801F
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060033C0 RID: 13248 RVA: 0x000D9E30 File Offset: 0x000D8030
	public void Start()
	{
		if (this.VisibleAtStart)
		{
			this.m_animatingFloat.Value = this.m_animatingFloat.Max;
			this.Activated = true;
		}
		else
		{
			this.m_animatingFloat.Value = 0f;
			this.Activated = false;
		}
	}

	// Token: 0x1700082B RID: 2091
	// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000D9E81 File Offset: 0x000D8081
	private bool CollidersShouldDisable
	{
		get
		{
			return this.m_animatingFloat.Value < this.ColliderSettings.ActivateDelay;
		}
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x000D9E9C File Offset: 0x000D809C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_delayTillVisible > 0f)
		{
			this.m_delayTillVisible -= Time.deltaTime;
			if (!this.Activated && this.m_delayTillVisible <= 0f)
			{
				this.m_delayTillVisible = 0f;
				this.Activated = true;
			}
		}
		if (this.m_touched && this.m_activated)
		{
			this.m_touchedTime += Time.deltaTime;
			if (this.OnTouchSettings.Vanish && this.m_touchedTime > this.OnTouchSettings.DelayToVanish)
			{
				this.Activated = false;
			}
		}
		if (this.m_activated)
		{
			this.m_activatedTime += Time.deltaTime;
			if (this.OnActivatedSettings.Vanish && this.m_activatedTime > this.OnActivatedSettings.DelayToVanish)
			{
				this.Activated = false;
			}
		}
		this.m_animatingFloat.Update(Time.deltaTime);
		foreach (Collider collider in this.m_colliders)
		{
			if (collider.isTrigger != this.CollidersShouldDisable)
			{
				collider.isTrigger = this.CollidersShouldDisable;
			}
		}
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x000D9FF0 File Offset: 0x000D81F0
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player") && !this.m_touched)
		{
			this.m_touched = true;
			if (this.OnTouchSettings.TriggerNextPlatform && this.NextPlatform)
			{
				this.NextPlatform.SetToAppear(this.OnTouchSettings.DelayToNextPlatformAppear);
			}
		}
	}

	// Token: 0x060033C4 RID: 13252 RVA: 0x000DA05C File Offset: 0x000D825C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_activated);
		ar.Serialize(ref this.m_activatedTime);
		ar.Serialize(ref this.m_touched);
		ar.Serialize(ref this.m_touchedTime);
		ar.Serialize(ref this.m_delayTillVisible);
		this.m_animatingFloat.Serialize(ar);
		if (ar.Reading)
		{
			this.Activated = this.m_activated;
		}
	}

	// Token: 0x1700082C RID: 2092
	// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000DA0C8 File Offset: 0x000D82C8
	// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000DA0D0 File Offset: 0x000D82D0
	public bool IsSuspended { get; set; }

	// Token: 0x04002EAC RID: 11948
	public bool VisibleAtStart;

	// Token: 0x04002EAD RID: 11949
	public SequencePlatformB.SequencePlatformOnTouchSettings OnTouchSettings;

	// Token: 0x04002EAE RID: 11950
	public SequencePlatformB.SequencePlatformOnActivatedSettings OnActivatedSettings;

	// Token: 0x04002EAF RID: 11951
	public SequencePlatformB.SequencePlatformColliderSettings ColliderSettings;

	// Token: 0x04002EB0 RID: 11952
	public SequencePlatformB NextPlatform;

	// Token: 0x04002EB1 RID: 11953
	public SoundProvider OnActivateSoundProvider;

	// Token: 0x04002EB2 RID: 11954
	public SoundProvider OnDeactivateSoundProvider;

	// Token: 0x04002EB3 RID: 11955
	private FlipPlantLogic[] m_flipPlants;

	// Token: 0x04002EB4 RID: 11956
	public BaseAnimator[] BaseAnimators;

	// Token: 0x04002EB5 RID: 11957
	private Collider[] m_colliders;

	// Token: 0x04002EB6 RID: 11958
	private readonly AnimatingFloat m_animatingFloat = new AnimatingFloat();

	// Token: 0x04002EB7 RID: 11959
	private bool m_touched;

	// Token: 0x04002EB8 RID: 11960
	private bool m_activated;

	// Token: 0x04002EB9 RID: 11961
	private float m_activatedTime;

	// Token: 0x04002EBA RID: 11962
	private float m_touchedTime;

	// Token: 0x04002EBB RID: 11963
	private float m_delayTillVisible;

	// Token: 0x0200091F RID: 2335
	[Serializable]
	public class SequencePlatformOnTouchSettings
	{
		// Token: 0x04002EBF RID: 11967
		public bool Vanish;

		// Token: 0x04002EC0 RID: 11968
		public float DelayToVanish;

		// Token: 0x04002EC1 RID: 11969
		public bool TriggerNextPlatform;

		// Token: 0x04002EC2 RID: 11970
		public float DelayToNextPlatformAppear;
	}

	// Token: 0x02000920 RID: 2336
	[Serializable]
	public class SequencePlatformOnActivatedSettings
	{
		// Token: 0x04002EC3 RID: 11971
		public bool Vanish;

		// Token: 0x04002EC4 RID: 11972
		public float DelayToVanish;

		// Token: 0x04002EC5 RID: 11973
		public bool TriggerNextPlatform;

		// Token: 0x04002EC6 RID: 11974
		public float DelayToNextPlatformAppear;
	}

	// Token: 0x02000921 RID: 2337
	[Serializable]
	public class SequencePlatformColliderSettings
	{
		// Token: 0x04002EC7 RID: 11975
		public float ActivateDelay = 0.5f;

		// Token: 0x04002EC8 RID: 11976
		public float DeactivateDelay = 0.5f;
	}
}
