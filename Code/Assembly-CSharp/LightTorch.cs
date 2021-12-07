using System;
using Core;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x02000649 RID: 1609
public class LightTorch : SaveSerialize, ISuspendable
{
	// Token: 0x17000641 RID: 1601
	// (get) Token: 0x0600275B RID: 10075 RVA: 0x000AB4F1 File Offset: 0x000A96F1
	public bool IsChasing
	{
		get
		{
			return this.m_isChasing;
		}
	}

	// Token: 0x17000642 RID: 1602
	// (get) Token: 0x0600275C RID: 10076 RVA: 0x000AB4F9 File Offset: 0x000A96F9
	public bool IsCarried
	{
		get
		{
			return this.m_carryableRigidBody.IsCarried;
		}
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x000AB508 File Offset: 0x000A9708
	public override void Awake()
	{
		base.Awake();
		Items.LightTorch = this;
		Game.Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		this.m_carryableRigidBody = base.GetComponent<CarryableRigidBody>();
		this.m_collider = base.GetComponent<Collider>();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		SuspensionManager.Register(this);
		this.SetToNormal();
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x000AB56C File Offset: 0x000A976C
	public override void OnDestroy()
	{
		base.OnDestroy();
		Game.Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x000AB595 File Offset: 0x000A9795
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_found);
		if (ar.Reading)
		{
			this.ChangeToNormalImmediate();
		}
	}

	// Token: 0x17000643 RID: 1603
	// (get) Token: 0x06002760 RID: 10080 RVA: 0x000AB5B4 File Offset: 0x000A97B4
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x17000644 RID: 1604
	// (get) Token: 0x06002761 RID: 10081 RVA: 0x000AB5C1 File Offset: 0x000A97C1
	public bool SeinInsideTorchZone
	{
		get
		{
			return Characters.Sein.IsOnGround && LightTorchZone.IsInside(Characters.Sein.Position);
		}
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x000AB5E4 File Offset: 0x000A97E4
	private void OnGameReset()
	{
		InstantiateUtility.Destroy(base.gameObject);
		Items.LightTorch = null;
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x000AB5F8 File Offset: 0x000A97F8
	private void SetToNormal()
	{
		this.m_isChasing = false;
		this.m_collider.enabled = true;
		this.m_rigidbody.useGravity = true;
		this.ParticlesGroup.SetActive(false);
		this.LightSource.SetActive(true);
		this.m_carryableRigidBody.FreezeWhenOffscreen = true;
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x000AB648 File Offset: 0x000A9848
	private void ChangeToNormalImmediate()
	{
		this.TransparencyAnimator.Initialize();
		this.TransparencyAnimator.AnimatorDriver.Pause();
		this.TransparencyAnimator.AnimatorDriver.GoToStart();
		this.FadeAnimator.Initialize();
		this.FadeAnimator.AnimatorDriver.Pause();
		this.FadeAnimator.AnimatorDriver.GoToStart();
		this.SetToNormal();
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x000AB6B4 File Offset: 0x000A98B4
	private void ChangeToNormal()
	{
		this.m_lockFadeAnimatorRemainingTime = 2.5f;
		this.TransparencyAnimator.Initialize();
		this.TransparencyAnimator.AnimatorDriver.ContinueBackwards();
		this.SetToNormal();
		if (this.AppearEffect)
		{
			InstantiateUtility.Instantiate(this.AppearEffect, this.Position, Quaternion.identity);
		}
		if (this.AppearSound)
		{
			Sound.Play(this.AppearSound.GetSound(null), base.transform.position, null);
		}
		if (this.MagicMovementSound)
		{
			this.MagicMovementSound.Stop();
		}
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x000AB760 File Offset: 0x000A9960
	private void ChangeToChase()
	{
		this.TransparencyAnimator.AnimatorDriver.ContinueForward();
		this.m_isChasing = true;
		this.m_collider.enabled = false;
		this.m_rigidbody.useGravity = false;
		this.ParticlesGroup.SetActive(true);
		this.LightSource.SetActive(false);
		this.m_carryableRigidBody.FreezeWhenOffscreen = false;
		this.m_rigidbody.isKinematic = false;
		this.m_chaseTime = 0f;
		if (this.DisappearEffect)
		{
			InstantiateUtility.Instantiate(this.DisappearEffect, this.Position, Quaternion.identity);
		}
		if (this.DisappearSound)
		{
			Sound.Play(this.DisappearSound.GetSound(null), base.transform.position, null);
		}
		if (this.MagicMovementSound)
		{
			this.MagicMovementSound.Play();
		}
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000AB848 File Offset: 0x000A9A48
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (Characters.Sein == null)
		{
			return;
		}
		if (this.m_lockFadeAnimatorRemainingTime > 0f)
		{
			this.m_lockFadeAnimatorRemainingTime -= Time.deltaTime;
		}
		Vector3 position = Characters.Sein.Position;
		if (this.IsCarried)
		{
			this.m_found = true;
			this.FadeAnimator.AnimatorDriver.ContinueBackwards();
		}
		else if (Sein.World.Events.DarknessLifted)
		{
			this.FadeAnimator.AnimatorDriver.Pause();
			this.FadeAnimator.AnimatorDriver.GoToStart();
		}
		else if (this.m_found)
		{
			if (this.m_isChasing)
			{
				if ((Characters.Ori.Position - base.transform.position + Vector3.up).magnitude < this.StopChaseRange && this.m_rigidbody.velocity.magnitude < this.StopChaseVelocity && base.transform.position.y > Characters.Sein.Position.y && this.SeinInsideTorchZone)
				{
					this.ChangeToNormal();
					return;
				}
				if (ForceLightTorchStopChasingZone.IsInside(position))
				{
					this.ChangeToNormal();
					return;
				}
				this.UpdateChasing();
			}
			else
			{
				if (Vector3.Distance(this.m_carryableRigidBody.Position, position) > this.MinDistance && SpiritLightDarknessZone.IsInsideDarknessZone(position) && !ForceLightTorchStopChasingZone.IsInside(position) && this.m_lockFadeAnimatorRemainingTime <= 0f)
				{
					this.FadeAnimator.AnimatorDriver.ContinueForward();
				}
				else
				{
					this.FadeAnimator.AnimatorDriver.ContinueBackwards();
				}
				bool flag = Mathf.Approximately(this.FadeAnimator.AnimatorDriver.CurrentTime, this.FadeAnimator.AnimatorDriver.Duration);
				if (flag)
				{
					this.ChangeToChase();
				}
			}
		}
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x000ABA54 File Offset: 0x000A9C54
	private void UpdateChasing()
	{
		Vector3 b = Characters.Ori.Position - base.transform.position + Vector3.up;
		b = b.normalized * Mathf.Min(b.magnitude, this.DeltaOverTime.Evaluate(this.m_chaseTime));
		float magnitude = b.magnitude;
		this.m_chaseTime += Time.deltaTime;
		float num = this.DragOverDistance.Evaluate(magnitude);
		this.m_rigidbody.velocity += b.normalized * this.ForceOverDistance.Evaluate(magnitude) * this.ForceOverTime.Evaluate(this.m_chaseTime) * Time.deltaTime;
		this.m_rigidbody.velocity += this.UpForceOverTime.Evaluate(this.m_chaseTime) * Time.deltaTime * Vector3.up;
		this.m_rigidbody.velocity *= 1f - num;
		this.m_rigidbody.WakeUp();
		MoonDebug.DrawCircle(base.transform.position + b, 0.5f, Color.yellow);
	}

	// Token: 0x17000645 RID: 1605
	// (get) Token: 0x06002769 RID: 10089 RVA: 0x000ABBA4 File Offset: 0x000A9DA4
	// (set) Token: 0x0600276A RID: 10090 RVA: 0x000ABBAC File Offset: 0x000A9DAC
	public bool IsSuspended { get; set; }

	// Token: 0x040021F2 RID: 8690
	public GameObject AppearEffect;

	// Token: 0x040021F3 RID: 8691
	public GameObject DisappearEffect;

	// Token: 0x040021F4 RID: 8692
	public SoundProvider AppearSound;

	// Token: 0x040021F5 RID: 8693
	public SoundProvider DisappearSound;

	// Token: 0x040021F6 RID: 8694
	public SoundSource MagicMovementSound;

	// Token: 0x040021F7 RID: 8695
	public BaseAnimator FadeAnimator;

	// Token: 0x040021F8 RID: 8696
	public BaseAnimator TransparencyAnimator;

	// Token: 0x040021F9 RID: 8697
	public AnimationCurve ForceOverDistance;

	// Token: 0x040021FA RID: 8698
	public AnimationCurve ForceOverTime;

	// Token: 0x040021FB RID: 8699
	public AnimationCurve DragOverDistance;

	// Token: 0x040021FC RID: 8700
	public AnimationCurve UpForceOverTime;

	// Token: 0x040021FD RID: 8701
	public AnimationCurve DeltaOverTime;

	// Token: 0x040021FE RID: 8702
	public float MinDistance = 10f;

	// Token: 0x040021FF RID: 8703
	public float StopChaseRange = 2f;

	// Token: 0x04002200 RID: 8704
	public float StopChaseVelocity = 1f;

	// Token: 0x04002201 RID: 8705
	public GameObject ParticlesGroup;

	// Token: 0x04002202 RID: 8706
	public GameObject LightSource;

	// Token: 0x04002203 RID: 8707
	private CarryableRigidBody m_carryableRigidBody;

	// Token: 0x04002204 RID: 8708
	private bool m_isChasing;

	// Token: 0x04002205 RID: 8709
	private Collider m_collider;

	// Token: 0x04002206 RID: 8710
	private bool m_found;

	// Token: 0x04002207 RID: 8711
	private int m_check;

	// Token: 0x04002208 RID: 8712
	private Rigidbody m_rigidbody;

	// Token: 0x04002209 RID: 8713
	private float m_chaseTime;

	// Token: 0x0400220A RID: 8714
	private float m_lockFadeAnimatorRemainingTime;
}
