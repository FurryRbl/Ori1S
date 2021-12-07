using System;
using UnityEngine;

// Token: 0x0200098E RID: 2446
public class PrewarmParticles : MonoBehaviour
{
	// Token: 0x0600357B RID: 13691 RVA: 0x000E02F4 File Offset: 0x000DE4F4
	[ContextMenu("Save particles")]
	public void Save()
	{
		ParticleEmitter component = base.GetComponent<ParticleEmitter>();
		this.m_legacyParticles = null;
		this.m_particleSystemParticles = null;
		if (component)
		{
			Particle[] particles = component.particles;
			this.m_legacyParticles = new PrewarmParticles.PrewarmLegacyParticle[particles.Length];
			for (int i = 0; i < particles.Length; i++)
			{
				this.m_legacyParticles[i] = new PrewarmParticles.PrewarmLegacyParticle(particles[i]);
			}
		}
		this.CacheParticleSystem();
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x000E036C File Offset: 0x000DE56C
	public void Awake()
	{
		ParticleEmitter component = base.GetComponent<ParticleEmitter>();
		ParticleSystem component2 = base.GetComponent<ParticleSystem>();
		if (component && this.m_legacyParticles != null)
		{
			Particle[] array = new Particle[this.m_legacyParticles.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.m_legacyParticles[i].ToParticle();
			}
			component.particles = array;
		}
		if (component2 && this.m_particleSystemParticles != null)
		{
			ParticleSystem.Particle[] array2 = new ParticleSystem.Particle[this.m_particleSystemParticles.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = this.m_particleSystemParticles[j].ToParticle();
			}
			component2.SetParticles(array2, array2.Length);
		}
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x000E0448 File Offset: 0x000DE648
	private void CacheParticleSystem()
	{
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		if (component)
		{
			component.Simulate(10f);
			int particleCount = component.particleCount;
			ParticleSystem.Particle[] array = new ParticleSystem.Particle[particleCount];
			component.GetParticles(array);
			this.m_particleSystemParticles = new PrewarmParticles.PrewarmParticleSystemParticle[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.m_particleSystemParticles[i] = new PrewarmParticles.PrewarmParticleSystemParticle(array[i]);
			}
		}
	}

	// Token: 0x04003006 RID: 12294
	[SerializeField]
	private PrewarmParticles.PrewarmLegacyParticle[] m_legacyParticles;

	// Token: 0x04003007 RID: 12295
	[SerializeField]
	private PrewarmParticles.PrewarmParticleSystemParticle[] m_particleSystemParticles;

	// Token: 0x0200098F RID: 2447
	[Serializable]
	public class PrewarmLegacyParticle
	{
		// Token: 0x0600357E RID: 13694 RVA: 0x000E04C4 File Offset: 0x000DE6C4
		public PrewarmLegacyParticle(Particle particle)
		{
			this.Position = particle.position;
			this.Velocity = particle.velocity;
			this.Energy = particle.energy;
			this.StartEnergy = particle.startEnergy;
			this.Size = particle.size;
			this.Rotation = particle.rotation;
			this.AngularVelocity = particle.angularVelocity;
			this.Color = particle.color;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000E0540 File Offset: 0x000DE740
		public Particle ToParticle()
		{
			return new Particle
			{
				position = this.Position,
				velocity = this.Velocity,
				energy = this.Energy,
				startEnergy = this.StartEnergy,
				size = this.Size,
				rotation = this.Rotation,
				angularVelocity = this.AngularVelocity,
				color = this.Color
			};
		}

		// Token: 0x04003008 RID: 12296
		public Vector3 Position;

		// Token: 0x04003009 RID: 12297
		public Vector3 Velocity;

		// Token: 0x0400300A RID: 12298
		public float Energy;

		// Token: 0x0400300B RID: 12299
		public float StartEnergy;

		// Token: 0x0400300C RID: 12300
		public float Size;

		// Token: 0x0400300D RID: 12301
		public float Rotation;

		// Token: 0x0400300E RID: 12302
		public float AngularVelocity;

		// Token: 0x0400300F RID: 12303
		public Color Color;
	}

	// Token: 0x02000990 RID: 2448
	[Serializable]
	public class PrewarmParticleSystemParticle
	{
		// Token: 0x06003580 RID: 13696 RVA: 0x000E05C0 File Offset: 0x000DE7C0
		public PrewarmParticleSystemParticle(ParticleSystem.Particle particle)
		{
			this.Position = particle.position;
			this.Velocity = particle.velocity;
			this.Lifetime = particle.lifetime;
			this.StartLifetime = particle.startLifetime;
			this.AxisOfRotation = particle.axisOfRotation;
			this.Size = particle.size;
			this.Rotation = particle.rotation;
			this.AngularVelocity = particle.angularVelocity;
			this.Color = particle.color;
			this.RandomSeed = particle.randomSeed;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000E065C File Offset: 0x000DE85C
		public ParticleSystem.Particle ToParticle()
		{
			return new ParticleSystem.Particle
			{
				position = this.Position,
				velocity = this.Velocity,
				lifetime = this.Lifetime,
				startLifetime = this.StartLifetime,
				axisOfRotation = this.AxisOfRotation,
				size = this.Size,
				rotation = this.Rotation,
				angularVelocity = this.AngularVelocity,
				color = this.Color,
				randomSeed = this.RandomSeed
			};
		}

		// Token: 0x04003010 RID: 12304
		public Vector3 Position;

		// Token: 0x04003011 RID: 12305
		public Vector3 Velocity;

		// Token: 0x04003012 RID: 12306
		public float Lifetime;

		// Token: 0x04003013 RID: 12307
		public float StartLifetime;

		// Token: 0x04003014 RID: 12308
		public float Size;

		// Token: 0x04003015 RID: 12309
		public Vector3 AxisOfRotation;

		// Token: 0x04003016 RID: 12310
		public float Rotation;

		// Token: 0x04003017 RID: 12311
		public float AngularVelocity;

		// Token: 0x04003018 RID: 12312
		public Color Color;

		// Token: 0x04003019 RID: 12313
		public uint RandomSeed;
	}
}
