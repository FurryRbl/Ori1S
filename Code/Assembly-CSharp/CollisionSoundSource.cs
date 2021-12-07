using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class CollisionSoundSource : MonoBehaviour, IPooled
{
	// Token: 0x060010B7 RID: 4279 RVA: 0x0004C691 File Offset: 0x0004A891
	public void OnPoolSpawned()
	{
		this.m_collisionCount = 0;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0004C69A File Offset: 0x0004A89A
	private void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_collisionReciever = base.GetComponent<Collider>();
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0004C6B4 File Offset: 0x0004A8B4
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == Characters.Current.GameObject)
		{
			return;
		}
		if (this.CollisionSound != null)
		{
			SoundDescriptor soundForCollision = this.CollisionSound.GetSoundForCollision(collision, new CollisionContext(collision, this.m_collisionReciever));
			if (soundForCollision != null)
			{
				Sound.Play(soundForCollision, base.transform.position, null);
			}
		}
		Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;
		if (!attachedRigidbody || attachedRigidbody.isKinematic)
		{
			this.m_collisionCount++;
			if (this.ContinuousSound && !this.ContinuousSound.IsPlaying)
			{
				this.ContinuousSound.Play();
			}
		}
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0004C77C File Offset: 0x0004A97C
	public void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == Characters.Current.GameObject)
		{
			return;
		}
		if (!collision.collider.attachedRigidbody || collision.collider.attachedRigidbody.isKinematic)
		{
			this.m_collisionCount--;
			if (this.ContinuousSound && this.m_collisionCount <= 0)
			{
				this.m_collisionCount = 0;
				this.ContinuousSound.StopAndFadeOut(0f);
			}
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0004C810 File Offset: 0x0004AA10
	private void FixedUpdate()
	{
		if (this.ContinuousSound)
		{
			this.ContinuousSound.SetVolumeMultiplier(this.m_rigidbody.velocity.magnitude / 5f);
		}
	}

	// Token: 0x04000E21 RID: 3617
	public CollisionBasedSoundProvider CollisionSound;

	// Token: 0x04000E22 RID: 3618
	public SoundSource ContinuousSound;

	// Token: 0x04000E23 RID: 3619
	private int m_collisionCount;

	// Token: 0x04000E24 RID: 3620
	private Rigidbody m_rigidbody;

	// Token: 0x04000E25 RID: 3621
	private Collider m_collisionReciever;
}
