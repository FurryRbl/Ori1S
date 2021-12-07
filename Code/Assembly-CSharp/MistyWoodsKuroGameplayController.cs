using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009D9 RID: 2521
public class MistyWoodsKuroGameplayController : SaveSerialize, ISuspendable
{
	// Token: 0x1700087F RID: 2175
	// (get) Token: 0x060036DA RID: 14042 RVA: 0x000E6327 File Offset: 0x000E4527
	public bool IsHidden
	{
		get
		{
			return this.m_currentState == MistyWoodsKuroGameplayController.State.Hidden || this.m_currentState == MistyWoodsKuroGameplayController.State.HiddenInDanger;
		}
	}

	// Token: 0x060036DB RID: 14043 RVA: 0x000E6340 File Offset: 0x000E4540
	public void ChangeState(MistyWoodsKuroGameplayController.State state)
	{
		this.m_currentState = state;
		this.m_currentTime = 0f;
	}

	// Token: 0x060036DC RID: 14044 RVA: 0x000E6354 File Offset: 0x000E4554
	public override void Awake()
	{
		SuspensionManager.Register(this);
		this.m_zones = base.GetComponentsInChildren<CatAndMouseKuroLandZone>();
	}

	// Token: 0x060036DD RID: 14045 RVA: 0x000E6368 File Offset: 0x000E4568
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060036DE RID: 14046 RVA: 0x000E6370 File Offset: 0x000E4570
	public void KillPlayer()
	{
		this.MistyWoodsKuroController.KillPlayer();
		this.ChangeState(MistyWoodsKuroGameplayController.State.Killed);
		Vector3 position = Characters.Sein.Position;
		foreach (CatAndMouseKuroLandZone catAndMouseKuroLandZone in this.m_zones)
		{
			if (catAndMouseKuroLandZone.Bounds.Contains(position))
			{
				catAndMouseKuroLandZone.Animator.gameObject.SetActive(true);
				catAndMouseKuroLandZone.Animator.Initialize();
				catAndMouseKuroLandZone.Animator.AnimatorDriver.Restart();
				if (this.LandKillSound)
				{
					Sound.Play(this.LandKillSound.GetSound(null), base.transform.position, null);
				}
				return;
			}
		}
		if (this.KuroFlyAttack)
		{
			UnityEngine.Object.Instantiate(this.KuroFlyAttack, Characters.Sein.Position, Quaternion.identity);
			if (this.FlyKillSound)
			{
				Sound.Play(this.FlyKillSound.GetSound(null), base.transform.position, null);
			}
		}
	}

	// Token: 0x060036DF RID: 14047 RVA: 0x000E6480 File Offset: 0x000E4680
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!Characters.Sein.Controller.CanMove)
		{
			return;
		}
		this.m_currentTime += Time.deltaTime;
		bool flag = MistyWoodsKuroGameplayHideZone.PositionInside(Characters.Current.Position);
		switch (this.m_currentState)
		{
		case MistyWoodsKuroGameplayController.State.Hidden:
			if (!flag)
			{
				this.ChangeState(MistyWoodsKuroGameplayController.State.Visible);
				this.OnVisible();
			}
			break;
		case MistyWoodsKuroGameplayController.State.Visible:
			if (flag)
			{
				if (this.MistyWoodsKuroController.IsHunting)
				{
					this.ChangeState(MistyWoodsKuroGameplayController.State.HiddenInDanger);
				}
				else
				{
					this.ChangeState(MistyWoodsKuroGameplayController.State.Hidden);
					this.OnHide();
				}
			}
			else if (this.m_currentTime > this.TimeToHide || this.InstaKillZonesContain(Characters.Current.Position))
			{
				this.KillPlayer();
			}
			break;
		case MistyWoodsKuroGameplayController.State.HiddenInDanger:
			if (!flag)
			{
				this.KillPlayer();
			}
			if (this.m_currentTime > 1f)
			{
				this.ChangeState(MistyWoodsKuroGameplayController.State.Hidden);
				this.OnHide();
			}
			break;
		}
	}

	// Token: 0x060036E0 RID: 14048 RVA: 0x000E65A0 File Offset: 0x000E47A0
	public void OnHide()
	{
		this.MistyWoodsKuroController.OnHide();
		if (this.OnHideAction)
		{
			this.OnHideAction.Perform(null);
		}
		if (this.VisibilityAnimator)
		{
			this.VisibilityAnimator.AnimatorDriver.ContinueBackwards();
		}
		if (this.m_previousSound)
		{
			this.m_previousSound.FadeOut(this.HiddenSoundFadeOutDuration, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_previousSound.gameObject);
			this.m_previousSound = null;
		}
		this.m_previousSound = Sound.Play(this.HiddenSoundProvider.GetSound(null), base.transform.position, delegate()
		{
			this.m_previousSound = null;
		});
	}

	// Token: 0x060036E1 RID: 14049 RVA: 0x000E6660 File Offset: 0x000E4860
	public void OnVisible()
	{
		this.MistyWoodsKuroController.OnVisible();
		if (this.OnVisibleAction)
		{
			this.OnVisibleAction.Perform(null);
		}
		if (this.VisibilityAnimator)
		{
			this.VisibilityAnimator.AnimatorDriver.ContinueForward();
		}
		if (this.m_previousSound)
		{
			this.m_previousSound.FadeOut(this.VisibleSoundFadeOutDuration, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_previousSound.gameObject);
			this.m_previousSound = null;
		}
		this.m_previousSound = Sound.Play(this.VisibleSoundProvider.GetSound(null), base.transform.position, delegate()
		{
			this.m_previousSound = null;
		});
	}

	// Token: 0x060036E2 RID: 14050 RVA: 0x000E6720 File Offset: 0x000E4920
	public bool InstaKillZonesContain(Vector2 position)
	{
		for (int i = 0; i < this.InstaKillZones.Length; i++)
		{
			Transform transform = this.InstaKillZones[i];
			Rect rect = default(Rect);
			rect.width = transform.lossyScale.x;
			rect.height = transform.lossyScale.y;
			rect.center = transform.position;
			if (rect.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060036E3 RID: 14051 RVA: 0x000E67A4 File Offset: 0x000E49A4
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.ChangeState((MistyWoodsKuroGameplayController.State)ar.Serialize(0));
		}
		else
		{
			ar.Serialize((int)this.m_currentState);
		}
	}

	// Token: 0x17000880 RID: 2176
	// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000E67DB File Offset: 0x000E49DB
	// (set) Token: 0x060036E5 RID: 14053 RVA: 0x000E67E3 File Offset: 0x000E49E3
	public bool IsSuspended { get; set; }

	// Token: 0x040031C1 RID: 12737
	public MistyWoodsKuroController MistyWoodsKuroController;

	// Token: 0x040031C2 RID: 12738
	public ActionMethod OnHideAction;

	// Token: 0x040031C3 RID: 12739
	public ActionMethod OnVisibleAction;

	// Token: 0x040031C4 RID: 12740
	public BaseAnimator VisibilityAnimator;

	// Token: 0x040031C5 RID: 12741
	public float HiddenSoundFadeOutDuration = 2f;

	// Token: 0x040031C6 RID: 12742
	public SoundProvider HiddenSoundProvider;

	// Token: 0x040031C7 RID: 12743
	public float VisibleSoundFadeOutDuration = 2f;

	// Token: 0x040031C8 RID: 12744
	public SoundProvider VisibleSoundProvider;

	// Token: 0x040031C9 RID: 12745
	public float TimeToHide = 2.5f;

	// Token: 0x040031CA RID: 12746
	private SoundPlayer m_previousSound;

	// Token: 0x040031CB RID: 12747
	private float m_currentTime;

	// Token: 0x040031CC RID: 12748
	public SoundProvider LandKillSound;

	// Token: 0x040031CD RID: 12749
	public SoundProvider FlyKillSound;

	// Token: 0x040031CE RID: 12750
	public GameObject KuroFlyAttack;

	// Token: 0x040031CF RID: 12751
	private CatAndMouseKuroLandZone[] m_zones;

	// Token: 0x040031D0 RID: 12752
	public Transform[] InstaKillZones;

	// Token: 0x040031D1 RID: 12753
	private MistyWoodsKuroGameplayController.State m_currentState;

	// Token: 0x020009DA RID: 2522
	public enum State
	{
		// Token: 0x040031D4 RID: 12756
		Hidden,
		// Token: 0x040031D5 RID: 12757
		Visible,
		// Token: 0x040031D6 RID: 12758
		HiddenInDanger,
		// Token: 0x040031D7 RID: 12759
		Killed
	}
}
