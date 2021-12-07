using System;
using UnityEngine;

// Token: 0x020009DF RID: 2527
public class RollingRockParticlesController : MonoBehaviour
{
	// Token: 0x060036FA RID: 14074 RVA: 0x000E6B44 File Offset: 0x000E4D44
	public void Start()
	{
		if (this.Emitter)
		{
			this.m_emitterMin = this.Emitter.minEmission;
			this.m_emitterMax = this.Emitter.maxEmission;
		}
		if (this.ParticleSystem)
		{
			this.m_emitterRate = this.ParticleSystem.emissionRate;
		}
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		if (!(this.Emitter == null) || this.ParticleSystem == null)
		{
		}
	}

	// Token: 0x060036FB RID: 14075 RVA: 0x000E6BD4 File Offset: 0x000E4DD4
	public void FixedUpdate()
	{
		float num = Mathf.Clamp01(Mathf.InverseLerp(this.MinVelocity, this.MaxVelocity, this.m_rigidbody.velocity.magnitude));
		if (this.Emitter)
		{
			this.Emitter.minEmission = num * this.m_emitterMin;
			this.Emitter.maxEmission = num * this.m_emitterMax;
			this.Emitter.emit = (this.m_startTime + 0.1f > Time.time);
		}
		if (this.ParticleSystem)
		{
			this.ParticleSystem.emissionRate = num * this.m_emitterRate;
			this.ParticleSystem.enableEmission = (this.m_startTime + 0.1f > Time.time);
		}
	}

	// Token: 0x060036FC RID: 14076 RVA: 0x000E6CA0 File Offset: 0x000E4EA0
	public void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.GetComponent<SeinCharacter>())
		{
			return;
		}
		ContactPoint[] contacts = collision.contacts;
		if (contacts.Length == 0)
		{
			return;
		}
		Vector3 vector = Vector3.zero;
		foreach (ContactPoint contactPoint in contacts)
		{
			vector += contactPoint.point;
		}
		vector /= (float)contacts.Length;
		if (this.Emitter)
		{
			this.Emitter.transform.position = vector;
		}
		if (this.ParticleSystem)
		{
			this.ParticleSystem.transform.position = vector;
		}
		this.m_startTime = Time.time;
	}

	// Token: 0x040031E7 RID: 12775
	public ParticleEmitter Emitter;

	// Token: 0x040031E8 RID: 12776
	public ParticleSystem ParticleSystem;

	// Token: 0x040031E9 RID: 12777
	private float m_emitterMax;

	// Token: 0x040031EA RID: 12778
	private float m_emitterMin;

	// Token: 0x040031EB RID: 12779
	private float m_emitterRate;

	// Token: 0x040031EC RID: 12780
	public float MinVelocity;

	// Token: 0x040031ED RID: 12781
	public float MaxVelocity;

	// Token: 0x040031EE RID: 12782
	private Rigidbody m_rigidbody;

	// Token: 0x040031EF RID: 12783
	private float m_startTime;
}
