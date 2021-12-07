using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009B5 RID: 2485
public class CatAndMouseRoomAController : MonoBehaviour
{
	// Token: 0x06003625 RID: 13861 RVA: 0x000E3180 File Offset: 0x000E1380
	public void Start()
	{
		if (Application.isPlaying)
		{
			Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
			this.m_originalLocalScale = this.AttackSpriteAnimator.transform.localScale;
		}
	}

	// Token: 0x06003626 RID: 13862 RVA: 0x000E31C3 File Offset: 0x000E13C3
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06003627 RID: 13863 RVA: 0x000E31DC File Offset: 0x000E13DC
	public void OnRestoreCheckpoint()
	{
		this.AttackSpriteAnimator.gameObject.SetActive(false);
		base.transform.localScale = this.m_originalLocalScale;
	}

	// Token: 0x06003628 RID: 13864 RVA: 0x000E320C File Offset: 0x000E140C
	private void Update()
	{
		this.AttackSpriteAnimator.DefaultAnimation = this.AttackTextureAnimation;
		this.AttackSpriteAnimator.SetAnimation(this.AttackTextureAnimation, true);
		this.EscapedSpriteAnimator.DefaultAnimation = this.EscapedTextureAnimation;
		this.EscapedSpriteAnimator.SetAnimation(this.EscapedTextureAnimation, true);
	}

	// Token: 0x06003629 RID: 13865 RVA: 0x000E3260 File Offset: 0x000E1460
	public void Attack()
	{
		this.AttackSpriteAnimator.gameObject.SetActive(true);
		this.AttackSpriteAnimator.SetAnimation(this.AttackTextureAnimation, true);
		this.AttackSpriteAnimator.AnimatorDriver.Restart();
		if (this.AttackSoundProvider)
		{
			Sound.Play(this.AttackSoundProvider.GetSound(null), base.transform.position, null);
		}
		Vector3 position = this.AttackSpriteAnimator.transform.position;
		if (this.AttackAtSeinPositionX)
		{
			position.x = Characters.Sein.Position.x;
		}
		if (this.AttackAtSeinPositionY)
		{
			position.y = Characters.Sein.Position.y;
		}
		position.y += this.KuroSpriteYOffSet;
		this.AttackSpriteAnimator.transform.position = position;
		if (this.FaceLeftRightBasedOnRoomCenter)
		{
			this.AttackSpriteAnimator.transform.localScale = this.m_originalLocalScale;
			if (Characters.Sein.Position.x > this.RoomCenter.position.x)
			{
				this.AttackSpriteAnimator.transform.localScale = new Vector3(-this.m_originalLocalScale.x, this.m_originalLocalScale.y, this.m_originalLocalScale.z);
			}
		}
		this.Kill();
	}

	// Token: 0x0600362A RID: 13866 RVA: 0x000E33D8 File Offset: 0x000E15D8
	public void Escaped()
	{
		if (this.EscapedSoundProvider)
		{
			Sound.Play(this.EscapedSoundProvider.GetSound(null), base.transform.position, null);
		}
		this.EscapedSpriteAnimator.gameObject.SetActive(true);
		this.EscapedSpriteAnimator.SetAnimation(this.EscapedTextureAnimation, true);
		this.EscapedSpriteAnimator.AnimatorDriver.Restart();
	}

	// Token: 0x0600362B RID: 13867 RVA: 0x000E3448 File Offset: 0x000E1648
	private void Kill()
	{
		IDamageReciever damageReciever = Characters.Sein.gameObject.FindComponentInChildren<IDamageReciever>();
		if (damageReciever != null)
		{
			Damage damage = new Damage(10000f, Vector3.up, Characters.Sein.Position, DamageType.Lava, base.gameObject);
			damageReciever.OnRecieveDamage(damage);
		}
	}

	// Token: 0x0600362C RID: 13868 RVA: 0x000E3498 File Offset: 0x000E1698
	private void Away()
	{
	}

	// Token: 0x040030B5 RID: 12469
	public SpriteAnimator AttackSpriteAnimator;

	// Token: 0x040030B6 RID: 12470
	public SpriteAnimator EscapedSpriteAnimator;

	// Token: 0x040030B7 RID: 12471
	public TextureAnimation AttackTextureAnimation;

	// Token: 0x040030B8 RID: 12472
	public TextureAnimation EscapedTextureAnimation;

	// Token: 0x040030B9 RID: 12473
	public TextureAnimation FlyAwayTextureAnimation;

	// Token: 0x040030BA RID: 12474
	public SoundProvider AttackSoundProvider;

	// Token: 0x040030BB RID: 12475
	public SoundProvider EscapedSoundProvider;

	// Token: 0x040030BC RID: 12476
	public Transform RoomCenter;

	// Token: 0x040030BD RID: 12477
	public float KuroSpriteYOffSet = 10f;

	// Token: 0x040030BE RID: 12478
	public bool AttackAtSeinPositionX = true;

	// Token: 0x040030BF RID: 12479
	public bool AttackAtSeinPositionY = true;

	// Token: 0x040030C0 RID: 12480
	public bool FaceLeftRightBasedOnRoomCenter = true;

	// Token: 0x040030C1 RID: 12481
	private Vector3 m_originalLocalScale;
}
