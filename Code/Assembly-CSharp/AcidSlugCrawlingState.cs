using System;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
public class AcidSlugCrawlingState : AcidSlugState
{
	// Token: 0x0600252E RID: 9518 RVA: 0x000A2498 File Offset: 0x000A0698
	public AcidSlugCrawlingState(AcidSlugEnemy slug, SoundSource sound, SoundSource moveSound) : base(slug)
	{
		this.Sound = sound;
		this.moveSound = moveSound;
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x000A24C8 File Offset: 0x000A06C8
	public override void UpdateState()
	{
		AcidSlugEnemySettings settings = this.AcidSlugEnemy.Settings;
		this.Slug.Movement.Speed = (float)((!this.Slug.SpriteMirror.FaceLeft) ? 1 : -1) * settings.WalkSpeed * settings.WalkSpeedMultiplier.Evaluate(this.Slug.SpriteAnimator.CurrentAnimationTime);
		this.Slug.Animation.PlayLoop(this.AcidSlugEnemy.Animations.Crawling, 0, null, false);
		this.UpdateAcidDrop();
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x000A255C File Offset: 0x000A075C
	public override void OnExit()
	{
		if (this.Sound)
		{
			this.Sound.Play();
		}
		if (this.moveSound)
		{
			this.moveSound.StopAndFadeOut(0.2f);
		}
		this.Slug.Movement.Speed = 0f;
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x000A25B9 File Offset: 0x000A07B9
	public override void OnEnter()
	{
		if (this.moveSound)
		{
			this.moveSound.Play();
		}
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x000A25D8 File Offset: 0x000A07D8
	public void UpdateAcidDrop()
	{
		if (this.AcidSlugEnemy.Settings.AcidDripRate <= 0f)
		{
			return;
		}
		if (Vector3.Dot(Vector3.down, this.Slug.Rotation * Vector3.up) > 0f)
		{
			this.m_acidDripRemainingTime -= Time.deltaTime;
			if (this.m_acidDripRemainingTime <= 0f)
			{
				this.m_acidDripRemainingTime = 1f / this.AcidSlugEnemy.Settings.AcidDripRate;
				this.AcidSlugEnemy.PlaySound(this.AcidSlugEnemy.Settings.AcidDripSoundProvider);
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.AcidSlugEnemy.Settings.AcidDrip, this.Slug.transform.position, Quaternion.identity);
				gameObject.transform.parent = this.Slug.transform.parent;
				Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), this.Slug.DamageDealer.GetComponent<Collider>());
			}
		}
	}

	// Token: 0x04001FB9 RID: 8121
	private float m_acidDripRemainingTime = 0.1f;

	// Token: 0x04001FBA RID: 8122
	private float m_acidTrailMarkRemainingTime;

	// Token: 0x04001FBB RID: 8123
	private SoundSource Sound;

	// Token: 0x04001FBC RID: 8124
	private SoundSource moveSound;
}
