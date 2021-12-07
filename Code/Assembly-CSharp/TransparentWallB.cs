using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000923 RID: 2339
public class TransparentWallB : SaveSerialize, ISuspendable
{
	// Token: 0x060033CE RID: 13262 RVA: 0x000DA137 File Offset: 0x000D8337
	public TransparentWallB()
	{
		this.IsSuspended = false;
	}

	// Token: 0x060033CF RID: 13263 RVA: 0x000DA146 File Offset: 0x000D8346
	public new void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x060033D0 RID: 13264 RVA: 0x000DA14E File Offset: 0x000D834E
	public new void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060033D1 RID: 13265 RVA: 0x000DA156 File Offset: 0x000D8356
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_hasBeenShown);
	}

	// Token: 0x1700082D RID: 2093
	// (get) Token: 0x060033D2 RID: 13266 RVA: 0x000DA164 File Offset: 0x000D8364
	public float SenseTime
	{
		get
		{
			return this.Animator.Duration / 2f;
		}
	}

	// Token: 0x060033D3 RID: 13267 RVA: 0x000DA178 File Offset: 0x000D8378
	public void Start()
	{
		AnimatorDriver animatorDriver = this.Animator.AnimatorDriver;
		if (this.WallVisible)
		{
			this.Animator.Initialize();
			animatorDriver.GoToEnd();
		}
		else if (this.HasSense)
		{
			this.Animator.Initialize();
			animatorDriver.CurrentTime = this.SenseTime;
			animatorDriver.Pause();
			animatorDriver.Sample();
		}
		else
		{
			this.Animator.Initialize();
			animatorDriver.GoToStart();
		}
	}

	// Token: 0x060033D4 RID: 13268 RVA: 0x000DA1F6 File Offset: 0x000D83F6
	public void OnTriggerEnter(Collider other)
	{
		this.OnEnterTrigger(other);
		this.OnTrigger(other);
	}

	// Token: 0x060033D5 RID: 13269 RVA: 0x000DA206 File Offset: 0x000D8406
	public void OnTriggerStay(Collider other)
	{
		this.OnTrigger(other);
	}

	// Token: 0x060033D6 RID: 13270 RVA: 0x000DA210 File Offset: 0x000D8410
	private void OnEnterTrigger(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (!this.m_hasBeenShown)
			{
				if (SeinTransparentWallHandler.Instance)
				{
					Sound.Play(SeinTransparentWallHandler.Instance.EnterTransparentWallFirstTimeSoundProvider.GetSound(null), base.transform.position, null);
				}
			}
			else if (SeinTransparentWallHandler.Instance)
			{
				Sound.Play(SeinTransparentWallHandler.Instance.EnterTransparentWallSoundProvider.GetSound(null), base.transform.position, null);
			}
		}
	}

	// Token: 0x060033D7 RID: 13271 RVA: 0x000DA2A4 File Offset: 0x000D84A4
	public void OnTrigger(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			this.m_beingTriggered = true;
			if (!this.m_hasBeenShown)
			{
				this.m_hasBeenShown = true;
				AchievementsLogic.Instance.RevealTransparentWall();
			}
		}
	}

	// Token: 0x060033D8 RID: 13272 RVA: 0x000DA2EC File Offset: 0x000D84EC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		AnimatorDriver animatorDriver = this.Animator.AnimatorDriver;
		if (this.WallVisible)
		{
			if (animatorDriver.IsReversed || !animatorDriver.IsPlaying)
			{
				animatorDriver.SetForward();
				animatorDriver.Resume();
			}
		}
		else if (this.m_lastVisiable)
		{
			animatorDriver.SetBackwards();
			animatorDriver.Resume();
			if (SeinTransparentWallHandler.Instance)
			{
				Sound.Play(SeinTransparentWallHandler.Instance.LeaveTransparentWallSoundProvider.GetSound(null), base.transform.position, null);
			}
		}
		this.m_lastVisiable = this.WallVisible;
		if (animatorDriver.CurrentTime < this.SenseTime && this.HasSense)
		{
			animatorDriver.Pause();
			animatorDriver.CurrentTime = this.SenseTime;
			animatorDriver.Sample();
		}
		this.m_beingTriggered = false;
	}

	// Token: 0x1700082E RID: 2094
	// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000DA3D4 File Offset: 0x000D85D4
	public bool HasSense
	{
		get
		{
			return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.Sense.HasAbility;
		}
	}

	// Token: 0x1700082F RID: 2095
	// (get) Token: 0x060033DA RID: 13274 RVA: 0x000DA407 File Offset: 0x000D8607
	public bool WallVisible
	{
		get
		{
			return this.m_beingTriggered;
		}
	}

	// Token: 0x17000830 RID: 2096
	// (get) Token: 0x060033DB RID: 13275 RVA: 0x000DA40F File Offset: 0x000D860F
	// (set) Token: 0x060033DC RID: 13276 RVA: 0x000DA417 File Offset: 0x000D8617
	public bool IsSuspended { get; set; }

	// Token: 0x04002ECA RID: 11978
	private bool m_hasBeenShown;

	// Token: 0x04002ECB RID: 11979
	private bool m_lastVisiable;

	// Token: 0x04002ECC RID: 11980
	private bool m_beingTriggered;

	// Token: 0x04002ECD RID: 11981
	public BaseAnimator Animator;
}
