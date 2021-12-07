using System;
using UnityEngine;

// Token: 0x0200092F RID: 2351
public class Stomper : SaveSerialize, ISuspendable, IDynamicGraphicHierarchy
{
	// Token: 0x06003400 RID: 13312 RVA: 0x000DA921 File Offset: 0x000D8B21
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06003401 RID: 13313 RVA: 0x000DA92F File Offset: 0x000D8B2F
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003402 RID: 13314 RVA: 0x000DA940 File Offset: 0x000D8B40
	[ContextMenu("Calculate distance")]
	public void CalculateDistance()
	{
		this.DistanceSet = true;
		Rigidbody rigidbody = base.gameObject.GetComponent<Rigidbody>();
		if (rigidbody == null)
		{
			rigidbody = (base.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody);
		}
		rigidbody.isKinematic = true;
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		this.m_fallDirection = (this.StompDustSpawnTransform.position - base.transform.position).normalized;
		RaycastHit raycastHit;
		if (rigidbody.SweepTest(this.m_fallDirection, out raycastHit))
		{
			this.Distance = raycastHit.distance;
			this.StompDustSpawnTransform.transform.position = base.transform.position + this.m_fallDirection * (raycastHit.distance + base.transform.lossyScale.x / 2f);
		}
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x000DAA2C File Offset: 0x000D8C2C
	private void Start()
	{
		this.m_transform = base.transform;
		this.m_originalPosition = this.m_transform.position;
		Rigidbody rigidbody = base.gameObject.GetComponent<Rigidbody>();
		if (rigidbody == null)
		{
			rigidbody = (base.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody);
		}
		rigidbody.isKinematic = true;
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		this.m_fallDirection = (this.StompDustSpawnTransform.position - base.transform.position).normalized;
		RaycastHit raycastHit;
		if (this.DistanceSet)
		{
			this.m_fall.Max = this.Distance;
		}
		else if (rigidbody.SweepTest(this.m_fallDirection, out raycastHit))
		{
			this.m_fall.Max = raycastHit.distance;
			this.StompDustSpawnTransform.transform.position = base.transform.position + this.m_fallDirection * (raycastHit.distance + base.transform.lossyScale.x / 2f);
		}
		if (this.DamageDealer)
		{
			this.DamageDealer.Activated = false;
		}
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x000DAB70 File Offset: 0x000D8D70
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		float fixedDeltaTime = Time.fixedDeltaTime;
		this.m_time += fixedDeltaTime;
		switch (this.m_state)
		{
		case Stomper.State.Shake:
			this.m_transform.position = this.m_originalPosition + -this.m_fallDirection * this.OnTriggerDecorationCurve.Evaluate(this.m_time / this.ShakeTime);
			if (this.m_time > this.ShakeTime)
			{
				this.ChangeState(Stomper.State.Falling);
			}
			break;
		case Stomper.State.Falling:
			this.m_fall.Speed += this.FallGravity * fixedDeltaTime;
			this.m_fall.Update(fixedDeltaTime);
			if (this.m_fall.IsValueAtEnd)
			{
				this.ChangeState(Stomper.State.Fallen);
				if (this.StompSound)
				{
					this.StompSound.Play();
				}
				if (this.StompImpactDust)
				{
					InstantiateUtility.Instantiate(this.StompImpactDust, this.StompDustSpawnTransform.position, Quaternion.identity);
				}
			}
			if (this.m_fall.Value / this.m_fall.Max > 0.8f && this.DamageDealer)
			{
				this.DamageDealer.Activated = true;
			}
			this.UpdatePosition();
			break;
		case Stomper.State.Fallen:
			if (this.m_time > this.DelayTillRise)
			{
				this.ChangeState(Stomper.State.Raising);
				this.m_fall.Speed = -this.RiseSpeed;
			}
			this.UpdatePosition();
			break;
		case Stomper.State.Raising:
			if (this.m_fall.Speed > -2f)
			{
				this.m_fall.Speed += fixedDeltaTime * this.RiseDeccleration;
			}
			this.m_fall.Update(fixedDeltaTime);
			this.UpdatePosition();
			if (this.m_fall.IsValueAtStart)
			{
				this.ChangeState(Stomper.State.Normal);
			}
			break;
		}
	}

	// Token: 0x06003405 RID: 13317 RVA: 0x000DAD7C File Offset: 0x000D8F7C
	private void UpdatePosition()
	{
		this.m_transform.position = this.m_originalPosition + this.m_fallDirection * this.m_fall.Value;
		if (this.m_state == Stomper.State.Raising && !this.m_hasPlayedResetSound && this.m_fall.Value < -this.m_fall.Speed)
		{
			if (this.StompResetSound && this.StompResetSound)
			{
				this.StompResetSound.Play();
			}
			this.m_hasPlayedResetSound = true;
		}
	}

	// Token: 0x06003406 RID: 13318 RVA: 0x000DAE1C File Offset: 0x000D901C
	private void ChangeState(Stomper.State state)
	{
		this.m_time = 0f;
		switch (state)
		{
		case Stomper.State.Normal:
			if (this.ReelInSound)
			{
				this.ReelInSound.Stop();
			}
			this.m_hasPlayedResetSound = false;
			break;
		case Stomper.State.Falling:
			this.m_fall.Speed = this.StartFallSpeed;
			break;
		case Stomper.State.Raising:
			if (this.DamageDealer)
			{
				this.DamageDealer.Activated = false;
			}
			if (this.ReelInSound)
			{
				this.ReelInSound.Play();
			}
			break;
		}
		this.m_state = state;
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x000DAEDF File Offset: 0x000D90DF
	public void PlayerTouchedTrigger()
	{
		if (this.m_state == Stomper.State.Normal)
		{
			if (this.StompStartSound)
			{
				this.StompStartSound.Play();
			}
			this.ChangeState(Stomper.State.Shake);
		}
	}

	// Token: 0x06003408 RID: 13320 RVA: 0x000DAF0E File Offset: 0x000D910E
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_transform.position = this.m_originalPosition;
			this.ChangeState(Stomper.State.Normal);
			this.m_fall.Value = 0f;
		}
	}

	// Token: 0x17000834 RID: 2100
	// (get) Token: 0x06003409 RID: 13321 RVA: 0x000DAF43 File Offset: 0x000D9143
	// (set) Token: 0x0600340A RID: 13322 RVA: 0x000DAF4B File Offset: 0x000D914B
	public bool IsSuspended { get; set; }

	// Token: 0x04002EED RID: 12013
	public float StartFallSpeed;

	// Token: 0x04002EEE RID: 12014
	public float FallGravity = 50f;

	// Token: 0x04002EEF RID: 12015
	public float RiseDeccleration = 5f;

	// Token: 0x04002EF0 RID: 12016
	public float RiseSpeed = 5f;

	// Token: 0x04002EF1 RID: 12017
	public float DelayTillRise = 2f;

	// Token: 0x04002EF2 RID: 12018
	public float ShakeTime = 0.5f;

	// Token: 0x04002EF3 RID: 12019
	public SoundSource StompSound;

	// Token: 0x04002EF4 RID: 12020
	public SoundSource StompStartSound;

	// Token: 0x04002EF5 RID: 12021
	public SoundSource StompResetSound;

	// Token: 0x04002EF6 RID: 12022
	public SoundSource ReelInSound;

	// Token: 0x04002EF7 RID: 12023
	public GameObject StompImpactDust;

	// Token: 0x04002EF8 RID: 12024
	public Transform StompDustSpawnTransform;

	// Token: 0x04002EF9 RID: 12025
	public DamageDealer DamageDealer;

	// Token: 0x04002EFA RID: 12026
	private Vector3 m_fallDirection;

	// Token: 0x04002EFB RID: 12027
	private Stomper.State m_state;

	// Token: 0x04002EFC RID: 12028
	private float m_time;

	// Token: 0x04002EFD RID: 12029
	private AnimatingFloat m_fall = new AnimatingFloat();

	// Token: 0x04002EFE RID: 12030
	private bool m_hasPlayedResetSound;

	// Token: 0x04002EFF RID: 12031
	private Vector3 m_originalPosition;

	// Token: 0x04002F00 RID: 12032
	private Transform m_transform;

	// Token: 0x04002F01 RID: 12033
	public AnimationCurve OnTriggerDecorationCurve;

	// Token: 0x04002F02 RID: 12034
	public float Distance;

	// Token: 0x04002F03 RID: 12035
	public bool DistanceSet;

	// Token: 0x02000930 RID: 2352
	private enum State
	{
		// Token: 0x04002F06 RID: 12038
		Normal,
		// Token: 0x04002F07 RID: 12039
		Shake,
		// Token: 0x04002F08 RID: 12040
		Falling,
		// Token: 0x04002F09 RID: 12041
		Fallen,
		// Token: 0x04002F0A RID: 12042
		Raising
	}
}
