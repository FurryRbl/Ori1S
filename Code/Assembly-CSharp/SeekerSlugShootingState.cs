using System;
using UnityEngine;

// Token: 0x020005CE RID: 1486
public class SeekerSlugShootingState : SlugState
{
	// Token: 0x0600256F RID: 9583 RVA: 0x000A3630 File Offset: 0x000A1830
	public SeekerSlugShootingState(SlugEnemy slug, SlugDirectionalAnimation animation, GameObject spikePrefab, SoundSource shootingSound) : base(slug)
	{
		this.m_animation = new SlugDirectionalAnimationPicker(animation);
		this.m_spikePrefab = spikePrefab;
		this.m_shootingSound = shootingSound;
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x000A365F File Offset: 0x000A185F
	public void SetSettings(float projectileSpeed, float numberOfShots, float delayBetweenShots)
	{
		this.m_numberOfShots = numberOfShots;
		this.m_delayBetweenShots = delayBetweenShots;
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x000A3670 File Offset: 0x000A1870
	public override void OnEnter()
	{
		this.Slug.Animation.Play(this.m_animation.PickAnimation(this.Slug.Movement.Up, this.Slug.FaceLeft), 0, null);
		this.m_shotCount = 0;
		this.m_timeSinceLastShot = this.m_delayBetweenShots;
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x000A36CC File Offset: 0x000A18CC
	public override void UpdateState()
	{
		if ((float)this.m_shotCount >= this.m_numberOfShots)
		{
			return;
		}
		if (this.m_timeSinceLastShot > this.m_delayBetweenShots)
		{
			this.m_timeSinceLastShot = 0f;
			this.m_shotCount++;
			this.Shoot();
		}
		if (!this.Slug.IsSuspended)
		{
			this.m_timeSinceLastShot += Time.deltaTime;
		}
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x000A3740 File Offset: 0x000A1940
	public void Shoot()
	{
		if (this.m_spikePrefab)
		{
			if (this.m_shootingSound)
			{
				this.m_shootingSound.Play();
			}
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.m_spikePrefab, this.Slug.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.Slug.transform.parent;
			Projectile component = gameObject.GetComponent<Projectile>();
			float num;
			Vector2 vector;
			PhysicsHelper.CalculateArcTrajectory(component.Gravity, this.Slug.PositionToPlayerPosition, out num, out vector, (2f + (float)this.m_shotCount * 0.5f) * Mathf.Max(0f, Vector3.Dot(this.Slug.Movement.Up, Vector3.up)));
			component.Direction = vector.normalized;
			component.Speed = vector.magnitude;
			component.Owner = this.Slug.DamageReciever.gameObject;
		}
	}

	// Token: 0x04002005 RID: 8197
	private readonly SlugDirectionalAnimationPicker m_animation;

	// Token: 0x04002006 RID: 8198
	private readonly GameObject m_spikePrefab;

	// Token: 0x04002007 RID: 8199
	private readonly SoundSource m_shootingSound;

	// Token: 0x04002008 RID: 8200
	private float m_numberOfShots;

	// Token: 0x04002009 RID: 8201
	private float m_delayBetweenShots;

	// Token: 0x0400200A RID: 8202
	private float m_timeSinceLastShot;

	// Token: 0x0400200B RID: 8203
	private int m_shotCount;
}
