using System;
using UnityEngine;

// Token: 0x020008D7 RID: 2263
public class Reflector : MonoBehaviour
{
	// Token: 0x06003262 RID: 12898 RVA: 0x000D51BB File Offset: 0x000D33BB
	public void OnTriggerEnter(Collider collider)
	{
		this.OnCollision(collider.gameObject);
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x000D51C9 File Offset: 0x000D33C9
	public void OnTriggerStay(Collider collider)
	{
		this.OnCollision(collider.gameObject);
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x000D51D7 File Offset: 0x000D33D7
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision.gameObject);
	}

	// Token: 0x06003265 RID: 12901 RVA: 0x000D51E5 File Offset: 0x000D33E5
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision.gameObject);
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x000D51F4 File Offset: 0x000D33F4
	public void OnCollision(GameObject collidingGameObject)
	{
		IReflectable reflectable = collidingGameObject.FindComponent<IReflectable>();
		if (reflectable as Component != null && reflectable.CanBeReflected(this.MaximumReflectableDamage) && reflectable.LastReflector != base.gameObject)
		{
			reflectable.OnGrabbed();
			if (this.ShouldScale)
			{
				collidingGameObject.transform.localScale *= this.ScaleFactor;
			}
			float num = reflectable.Speed;
			if (this.ShouldIncreaceSpeed)
			{
				num *= this.SpeedFactor;
			}
			reflectable.LastReflector = base.gameObject;
			reflectable.OnReleased(num, reflectable.Direction * -1f);
		}
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x000D52AB File Offset: 0x000D34AB
	public void OnStick(SpiritFlameProjectile spiritFlameProjectile)
	{
	}

	// Token: 0x04002D57 RID: 11607
	public bool ShouldIncreaceSpeed;

	// Token: 0x04002D58 RID: 11608
	public bool ShouldScale;

	// Token: 0x04002D59 RID: 11609
	public float SpeedFactor = 1.5f;

	// Token: 0x04002D5A RID: 11610
	public float ScaleFactor = 1.5f;

	// Token: 0x04002D5B RID: 11611
	public float MaximumReflectableDamage = 15f;
}
