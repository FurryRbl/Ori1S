using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009B7 RID: 2487
public class CatAndMouseRoomDController : MonoBehaviour, ISuspendable
{
	// Token: 0x0600363A RID: 13882 RVA: 0x000E38C4 File Offset: 0x000E1AC4
	public void Awake()
	{
		SuspensionManager.Register(this);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		this.m_originalPosition = this.KuroSprite.transform.position;
	}

	// Token: 0x0600363B RID: 13883 RVA: 0x000E3903 File Offset: 0x000E1B03
	public void Start()
	{
		this.KuroSprite.gameObject.SetActive(true);
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x000E3916 File Offset: 0x000E1B16
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x000E3934 File Offset: 0x000E1B34
	public void OnRestoreCheckpoint()
	{
		this.AttackSpriteAnimator.AnimatorDriver.Stop();
		this.m_runningSequence = false;
		this.m_time = 0f;
		this.KuroSprite.transform.position = this.m_originalPosition;
		this.m_escaped = false;
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x000E3980 File Offset: 0x000E1B80
	public void FixedUpdate()
	{
		if (this.m_runningSequence)
		{
			if (!this.IsSuspended)
			{
				this.m_time += Time.fixedDeltaTime;
			}
			Vector3 vector = UI.Cameras.Current.Camera.transform.position + this.PositionToCameraOffset;
			Vector3 position = Characters.Sein.Position;
			if (position.x > this.MaxPositionX)
			{
				position.x = this.MaxPositionX;
			}
			if (this.m_escaped && position.x > this.MaxPositionEscapedX)
			{
				position.x = this.MaxPositionEscapedX;
			}
			this.m_playerPosition = Vector3.Lerp(this.m_playerPosition, position, 0.2f);
			float t = this.PositionYToCameraWeightCurve.Evaluate(this.m_time);
			float num = Mathf.Lerp(this.m_originalPosition.x, this.m_playerPosition.x, this.PositionXToPlayerWeightCurve.Evaluate(this.m_time));
			num = Mathf.Lerp(num, vector.x, this.PositionXToCameraWeightCurve.Evaluate(this.m_time));
			this.KuroSprite.transform.position = new Vector3(num, Mathf.Lerp(this.m_originalPosition.y, vector.y, t), Mathf.Lerp(this.m_originalPosition.z, vector.z, t));
			if (this.m_time > this.CenterLayerTime)
			{
				UberShaderRenderQueue.SetRenderQueueExplicit(this.KuroSprite.gameObject, -20f);
			}
			else
			{
				UberShaderRenderQueue.SetRenderQueueExplicit(this.KuroSprite.gameObject, 10f);
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

	// Token: 0x0600363F RID: 13887 RVA: 0x000E3B58 File Offset: 0x000E1D58
	public void StartSequence()
	{
		if (this.AttackSoundProvider)
		{
			Sound.Play(this.AttackSoundProvider.GetSound(null), base.transform.position, null);
		}
		this.AttackSpriteAnimator.AnimatorDriver.Restart();
		this.m_runningSequence = true;
		this.m_time = 0f;
		this.m_playerPosition = Characters.Sein.Position;
		this.KuroSprite.transform.position = this.m_originalPosition;
	}

	// Token: 0x06003640 RID: 13888 RVA: 0x000E3BDB File Offset: 0x000E1DDB
	public void Attack()
	{
		this.Kill();
	}

	// Token: 0x06003641 RID: 13889 RVA: 0x000E3BE3 File Offset: 0x000E1DE3
	public void Escaped()
	{
		this.m_escaped = true;
	}

	// Token: 0x06003642 RID: 13890 RVA: 0x000E3BEC File Offset: 0x000E1DEC
	private void Kill()
	{
		IDamageReciever damageReciever = Characters.Sein.gameObject.FindComponentInChildren<IDamageReciever>();
		if (damageReciever != null)
		{
			Damage damage = new Damage(10000f, Vector3.up, Characters.Sein.Position, DamageType.Lava, base.gameObject);
			damageReciever.OnRecieveDamage(damage);
		}
	}

	// Token: 0x17000873 RID: 2163
	// (get) Token: 0x06003643 RID: 13891 RVA: 0x000E3C3C File Offset: 0x000E1E3C
	// (set) Token: 0x06003644 RID: 13892 RVA: 0x000E3C44 File Offset: 0x000E1E44
	public bool IsSuspended { get; set; }

	// Token: 0x040030D3 RID: 12499
	public BaseAnimator AttackSpriteAnimator;

	// Token: 0x040030D4 RID: 12500
	public Renderer KuroSprite;

	// Token: 0x040030D5 RID: 12501
	public AnimationCurve PositionXToCameraWeightCurve;

	// Token: 0x040030D6 RID: 12502
	public AnimationCurve PositionXToPlayerWeightCurve;

	// Token: 0x040030D7 RID: 12503
	public AnimationCurve PositionYToCameraWeightCurve;

	// Token: 0x040030D8 RID: 12504
	public Vector3 PositionToCameraOffset;

	// Token: 0x040030D9 RID: 12505
	public SoundProvider AttackSoundProvider;

	// Token: 0x040030DA RID: 12506
	private Vector3 m_originalPosition;

	// Token: 0x040030DB RID: 12507
	private bool m_runningSequence;

	// Token: 0x040030DC RID: 12508
	private float m_time;

	// Token: 0x040030DD RID: 12509
	public float CenterLayerTime = 4f;

	// Token: 0x040030DE RID: 12510
	public float SequenceTime = 5f;

	// Token: 0x040030DF RID: 12511
	public float MaxPositionX;

	// Token: 0x040030E0 RID: 12512
	public float MaxPositionEscapedX = 154f;

	// Token: 0x040030E1 RID: 12513
	public ActionMethod FinishAction;

	// Token: 0x040030E2 RID: 12514
	private Vector3 m_playerPosition;

	// Token: 0x040030E3 RID: 12515
	private bool m_escaped;
}
