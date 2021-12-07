using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000111 RID: 273
	public class ParticleEmitter : Component
	{
		// Token: 0x06001141 RID: 4417 RVA: 0x000141A8 File Offset: 0x000123A8
		internal ParticleEmitter()
		{
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001142 RID: 4418
		// (set) Token: 0x06001143 RID: 4419
		public extern bool emit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001144 RID: 4420
		// (set) Token: 0x06001145 RID: 4421
		public extern float minSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001146 RID: 4422
		// (set) Token: 0x06001147 RID: 4423
		public extern float maxSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001148 RID: 4424
		// (set) Token: 0x06001149 RID: 4425
		public extern float minEnergy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600114A RID: 4426
		// (set) Token: 0x0600114B RID: 4427
		public extern float maxEnergy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600114C RID: 4428
		// (set) Token: 0x0600114D RID: 4429
		public extern float minEmission { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600114E RID: 4430
		// (set) Token: 0x0600114F RID: 4431
		public extern float maxEmission { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001150 RID: 4432
		// (set) Token: 0x06001151 RID: 4433
		public extern float emitterVelocityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000141B0 File Offset: 0x000123B0
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x000141C8 File Offset: 0x000123C8
		public Vector3 worldVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_worldVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_worldVelocity(ref value);
			}
		}

		// Token: 0x06001154 RID: 4436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldVelocity(out Vector3 value);

		// Token: 0x06001155 RID: 4437
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_worldVelocity(ref Vector3 value);

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000141D4 File Offset: 0x000123D4
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x000141EC File Offset: 0x000123EC
		public Vector3 localVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localVelocity(ref value);
			}
		}

		// Token: 0x06001158 RID: 4440
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localVelocity(out Vector3 value);

		// Token: 0x06001159 RID: 4441
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localVelocity(ref Vector3 value);

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x000141F8 File Offset: 0x000123F8
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00014210 File Offset: 0x00012410
		public Vector3 rndVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_rndVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rndVelocity(ref value);
			}
		}

		// Token: 0x0600115C RID: 4444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rndVelocity(out Vector3 value);

		// Token: 0x0600115D RID: 4445
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rndVelocity(ref Vector3 value);

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600115E RID: 4446
		// (set) Token: 0x0600115F RID: 4447
		public extern bool useWorldSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001160 RID: 4448
		// (set) Token: 0x06001161 RID: 4449
		public extern bool rndRotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001162 RID: 4450
		// (set) Token: 0x06001163 RID: 4451
		public extern float angularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001164 RID: 4452
		// (set) Token: 0x06001165 RID: 4453
		public extern float rndAngularVelocity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001166 RID: 4454
		// (set) Token: 0x06001167 RID: 4455
		public extern Particle[] particles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001168 RID: 4456
		public extern int particleCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001169 RID: 4457 RVA: 0x0001421C File Offset: 0x0001241C
		public void ClearParticles()
		{
			ParticleEmitter.INTERNAL_CALL_ClearParticles(this);
		}

		// Token: 0x0600116A RID: 4458
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClearParticles(ParticleEmitter self);

		// Token: 0x0600116B RID: 4459 RVA: 0x00014224 File Offset: 0x00012424
		public void Emit()
		{
			this.Emit2((int)Random.Range(this.minEmission, this.maxEmission));
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00014240 File Offset: 0x00012440
		public void Emit(int count)
		{
			this.Emit2(count);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0001424C File Offset: 0x0001244C
		public void Emit(Vector3 pos, Vector3 velocity, float size, float energy, Color color)
		{
			InternalEmitParticleArguments internalEmitParticleArguments = default(InternalEmitParticleArguments);
			internalEmitParticleArguments.pos = pos;
			internalEmitParticleArguments.velocity = velocity;
			internalEmitParticleArguments.size = size;
			internalEmitParticleArguments.energy = energy;
			internalEmitParticleArguments.color = color;
			internalEmitParticleArguments.rotation = 0f;
			internalEmitParticleArguments.angularVelocity = 0f;
			this.Emit3(ref internalEmitParticleArguments);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000142AC File Offset: 0x000124AC
		public void Emit(Vector3 pos, Vector3 velocity, float size, float energy, Color color, float rotation, float angularVelocity)
		{
			InternalEmitParticleArguments internalEmitParticleArguments = default(InternalEmitParticleArguments);
			internalEmitParticleArguments.pos = pos;
			internalEmitParticleArguments.velocity = velocity;
			internalEmitParticleArguments.size = size;
			internalEmitParticleArguments.energy = energy;
			internalEmitParticleArguments.color = color;
			internalEmitParticleArguments.rotation = rotation;
			internalEmitParticleArguments.angularVelocity = angularVelocity;
			this.Emit3(ref internalEmitParticleArguments);
		}

		// Token: 0x0600116F RID: 4463
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit2(int count);

		// Token: 0x06001170 RID: 4464
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit3(ref InternalEmitParticleArguments args);

		// Token: 0x06001171 RID: 4465
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float deltaTime);

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001172 RID: 4466
		// (set) Token: 0x06001173 RID: 4467
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
