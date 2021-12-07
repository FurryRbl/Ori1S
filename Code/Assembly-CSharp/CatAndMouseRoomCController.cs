using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009B6 RID: 2486
public class CatAndMouseRoomCController : MonoBehaviour, ISuspendable
{
	// Token: 0x0600362E RID: 13870 RVA: 0x000E34B8 File Offset: 0x000E16B8
	public void Awake()
	{
		SuspensionManager.Register(this);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		this.m_originalPosition = this.KuroSprite.transform.position;
	}

	// Token: 0x0600362F RID: 13871 RVA: 0x000E34F8 File Offset: 0x000E16F8
	public void Start()
	{
		this.KuroSprite.gameObject.SetActive(true);
		this.KuroSurfaceSprite.gameObject.SetActive(false);
	}

	// Token: 0x06003630 RID: 13872 RVA: 0x000E3527 File Offset: 0x000E1727
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06003631 RID: 13873 RVA: 0x000E3548 File Offset: 0x000E1748
	public void OnRestoreCheckpoint()
	{
		this.AttackSpriteAnimator.AnimatorDriver.Stop();
		this.m_runningSequence = false;
		this.m_time = 0f;
		this.KuroSprite.transform.position = this.m_originalPosition;
		this.KuroSurfaceSprite.transform.position = this.m_originalPosition;
		this.KuroSprite.gameObject.SetActive(true);
		this.KuroSurfaceSprite.gameObject.SetActive(false);
		this.m_kuroWillFlyOverSurface = false;
	}

	// Token: 0x06003632 RID: 13874 RVA: 0x000E35CC File Offset: 0x000E17CC
	public void FixedUpdate()
	{
		if (this.m_runningSequence)
		{
			if (!this.IsSuspended)
			{
				this.m_time += Time.fixedDeltaTime;
			}
			Vector3 b = UI.Cameras.Current.Camera.transform.position + this.PositionToCameraOffset;
			if (b.y < this.MaxVertical)
			{
				b.y = this.MaxVertical;
			}
			this.KuroSprite.transform.position = Vector3.Lerp(this.m_originalPosition, b, this.PositionToCameraWeightCurve.Evaluate(this.m_time));
			this.KuroSurfaceSprite.transform.position = Vector3.Lerp(this.m_originalPosition, b, this.PositionToCameraWeightCurve.Evaluate(this.m_time));
			if (this.m_time > this.CenterLayerTime)
			{
				UberShaderRenderQueue.SetRenderQueueExplicit(this.KuroSprite.gameObject, -20f);
				UberShaderRenderQueue.SetRenderQueueExplicit(this.KuroSurfaceSprite.gameObject, -20f);
			}
			else
			{
				UberShaderRenderQueue.SetRenderQueueExplicit(this.KuroSprite.gameObject, 10f);
				UberShaderRenderQueue.SetRenderQueueExplicit(this.KuroSurfaceSprite.gameObject, 10f);
			}
			if (this.m_time > this.SurfaceTime && this.m_kuroWillFlyOverSurface && !this.KuroSurfaceSprite.gameObject.activeSelf)
			{
				this.KuroSprite.gameObject.SetActive(false);
				this.KuroSurfaceSprite.gameObject.SetActive(true);
				this.AttackSpriteSurfaceAnimator.AnimatorDriver.Restart();
			}
			if (this.m_time > this.SequenceTime)
			{
				this.m_runningSequence = false;
				if (this.FinishAction)
				{
					this.FinishAction.Perform(null);
				}
			}
		}
	}

	// Token: 0x06003633 RID: 13875 RVA: 0x000E379C File Offset: 0x000E199C
	public void StartSequence()
	{
		if (this.AttackSoundProvider)
		{
			Sound.Play(this.AttackSoundProvider.GetSound(null), base.transform.position, null);
		}
		this.AttackSpriteAnimator.AnimatorDriver.Restart();
		this.m_runningSequence = true;
		this.m_time = 0f;
		this.KuroSprite.transform.position = this.m_originalPosition;
		this.KuroSurfaceSprite.transform.position = this.m_originalPosition;
	}

	// Token: 0x06003634 RID: 13876 RVA: 0x000E3825 File Offset: 0x000E1A25
	public void Attack()
	{
		this.Kill();
	}

	// Token: 0x06003635 RID: 13877 RVA: 0x000E382D File Offset: 0x000E1A2D
	public void Escaped()
	{
		this.m_kuroWillFlyOverSurface = true;
	}

	// Token: 0x06003636 RID: 13878 RVA: 0x000E3838 File Offset: 0x000E1A38
	private void Kill()
	{
		IDamageReciever damageReciever = Characters.Sein.gameObject.FindComponentInChildren<IDamageReciever>();
		if (damageReciever != null)
		{
			Damage damage = new Damage(10000f, Vector3.up, Characters.Sein.Position, DamageType.Lava, base.gameObject);
			damageReciever.OnRecieveDamage(damage);
		}
	}

	// Token: 0x17000872 RID: 2162
	// (get) Token: 0x06003637 RID: 13879 RVA: 0x000E3888 File Offset: 0x000E1A88
	// (set) Token: 0x06003638 RID: 13880 RVA: 0x000E3890 File Offset: 0x000E1A90
	public bool IsSuspended { get; set; }

	// Token: 0x040030C2 RID: 12482
	public BaseAnimator AttackSpriteSurfaceAnimator;

	// Token: 0x040030C3 RID: 12483
	public BaseAnimator AttackSpriteAnimator;

	// Token: 0x040030C4 RID: 12484
	public Renderer KuroSprite;

	// Token: 0x040030C5 RID: 12485
	public Renderer KuroSurfaceSprite;

	// Token: 0x040030C6 RID: 12486
	public AnimationCurve PositionToCameraWeightCurve;

	// Token: 0x040030C7 RID: 12487
	public Vector3 PositionToCameraOffset;

	// Token: 0x040030C8 RID: 12488
	public SoundProvider AttackSoundProvider;

	// Token: 0x040030C9 RID: 12489
	private Vector3 m_originalPosition;

	// Token: 0x040030CA RID: 12490
	private bool m_runningSequence;

	// Token: 0x040030CB RID: 12491
	private float m_time;

	// Token: 0x040030CC RID: 12492
	public float CenterLayerTime = 4f;

	// Token: 0x040030CD RID: 12493
	public float SequenceTime = 5f;

	// Token: 0x040030CE RID: 12494
	public float MaxVertical;

	// Token: 0x040030CF RID: 12495
	public float SurfaceTime;

	// Token: 0x040030D0 RID: 12496
	private bool m_kuroWillFlyOverSurface;

	// Token: 0x040030D1 RID: 12497
	public ActionMethod FinishAction;
}
