using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000F5 RID: 245
	public sealed class ParticleSystem : Component
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000EAB RID: 3755
		// (set) Token: 0x06000EAC RID: 3756
		public extern float startDelay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000EAD RID: 3757
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000EAE RID: 3758
		public extern bool isStopped { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000EAF RID: 3759
		public extern bool isPaused { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000EB0 RID: 3760
		// (set) Token: 0x06000EB1 RID: 3761
		public extern bool loop { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000EB2 RID: 3762
		// (set) Token: 0x06000EB3 RID: 3763
		public extern bool playOnAwake { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000EB4 RID: 3764
		// (set) Token: 0x06000EB5 RID: 3765
		public extern float time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000EB6 RID: 3766
		public extern float duration { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000EB7 RID: 3767
		// (set) Token: 0x06000EB8 RID: 3768
		public extern float playbackSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000EB9 RID: 3769
		public extern int particleCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000EBA RID: 3770
		// (set) Token: 0x06000EBB RID: 3771
		[Obsolete("enableEmission property is deprecated. Use emission.enable instead.")]
		public extern bool enableEmission { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000EBC RID: 3772
		// (set) Token: 0x06000EBD RID: 3773
		[Obsolete("emissionRate property is deprecated. Use emission.rate instead.")]
		public extern float emissionRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000EBE RID: 3774
		// (set) Token: 0x06000EBF RID: 3775
		public extern float startSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000EC0 RID: 3776
		// (set) Token: 0x06000EC1 RID: 3777
		public extern float startSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000125D8 File Offset: 0x000107D8
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x000125F0 File Offset: 0x000107F0
		public Color startColor
		{
			get
			{
				Color result;
				this.INTERNAL_get_startColor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_startColor(ref value);
			}
		}

		// Token: 0x06000EC4 RID: 3780
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_startColor(out Color value);

		// Token: 0x06000EC5 RID: 3781
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_startColor(ref Color value);

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000EC6 RID: 3782
		// (set) Token: 0x06000EC7 RID: 3783
		public extern float startRotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x000125FC File Offset: 0x000107FC
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00012614 File Offset: 0x00010814
		public Vector3 startRotation3D
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_startRotation3D(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_startRotation3D(ref value);
			}
		}

		// Token: 0x06000ECA RID: 3786
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_startRotation3D(out Vector3 value);

		// Token: 0x06000ECB RID: 3787
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_startRotation3D(ref Vector3 value);

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000ECC RID: 3788
		// (set) Token: 0x06000ECD RID: 3789
		public extern float startLifetime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000ECE RID: 3790
		// (set) Token: 0x06000ECF RID: 3791
		public extern float gravityModifier { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000ED0 RID: 3792
		// (set) Token: 0x06000ED1 RID: 3793
		public extern int maxParticles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000ED2 RID: 3794
		// (set) Token: 0x06000ED3 RID: 3795
		public extern ParticleSystemSimulationSpace simulationSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000ED4 RID: 3796
		// (set) Token: 0x06000ED5 RID: 3797
		public extern ParticleSystemScalingMode scalingMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000ED6 RID: 3798
		// (set) Token: 0x06000ED7 RID: 3799
		public extern uint randomSeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00012620 File Offset: 0x00010820
		public ParticleSystem.EmissionModule emission
		{
			get
			{
				return new ParticleSystem.EmissionModule(this);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00012628 File Offset: 0x00010828
		public ParticleSystem.ShapeModule shape
		{
			get
			{
				return new ParticleSystem.ShapeModule(this);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00012630 File Offset: 0x00010830
		public ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime
		{
			get
			{
				return new ParticleSystem.VelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00012638 File Offset: 0x00010838
		public ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime
		{
			get
			{
				return new ParticleSystem.LimitVelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00012640 File Offset: 0x00010840
		public ParticleSystem.InheritVelocityModule inheritVelocity
		{
			get
			{
				return new ParticleSystem.InheritVelocityModule(this);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00012648 File Offset: 0x00010848
		public ParticleSystem.ForceOverLifetimeModule forceOverLifetime
		{
			get
			{
				return new ParticleSystem.ForceOverLifetimeModule(this);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00012650 File Offset: 0x00010850
		public ParticleSystem.ColorOverLifetimeModule colorOverLifetime
		{
			get
			{
				return new ParticleSystem.ColorOverLifetimeModule(this);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00012658 File Offset: 0x00010858
		public ParticleSystem.ColorBySpeedModule colorBySpeed
		{
			get
			{
				return new ParticleSystem.ColorBySpeedModule(this);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00012660 File Offset: 0x00010860
		public ParticleSystem.SizeOverLifetimeModule sizeOverLifetime
		{
			get
			{
				return new ParticleSystem.SizeOverLifetimeModule(this);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00012668 File Offset: 0x00010868
		public ParticleSystem.SizeBySpeedModule sizeBySpeed
		{
			get
			{
				return new ParticleSystem.SizeBySpeedModule(this);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00012670 File Offset: 0x00010870
		public ParticleSystem.RotationOverLifetimeModule rotationOverLifetime
		{
			get
			{
				return new ParticleSystem.RotationOverLifetimeModule(this);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00012678 File Offset: 0x00010878
		public ParticleSystem.RotationBySpeedModule rotationBySpeed
		{
			get
			{
				return new ParticleSystem.RotationBySpeedModule(this);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00012680 File Offset: 0x00010880
		public ParticleSystem.ExternalForcesModule externalForces
		{
			get
			{
				return new ParticleSystem.ExternalForcesModule(this);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00012688 File Offset: 0x00010888
		public ParticleSystem.CollisionModule collision
		{
			get
			{
				return new ParticleSystem.CollisionModule(this);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00012690 File Offset: 0x00010890
		public ParticleSystem.SubEmittersModule subEmitters
		{
			get
			{
				return new ParticleSystem.SubEmittersModule(this);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00012698 File Offset: 0x00010898
		public ParticleSystem.TextureSheetAnimationModule textureSheetAnimation
		{
			get
			{
				return new ParticleSystem.TextureSheetAnimationModule(this);
			}
		}

		// Token: 0x06000EE8 RID: 3816
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParticles(ParticleSystem.Particle[] particles, int size);

		// Token: 0x06000EE9 RID: 3817
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetParticles(ParticleSystem.Particle[] particles);

		// Token: 0x06000EEA RID: 3818
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Simulate(ParticleSystem self, float t, bool restart);

		// Token: 0x06000EEB RID: 3819
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Play(ParticleSystem self);

		// Token: 0x06000EEC RID: 3820
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Stop(ParticleSystem self);

		// Token: 0x06000EED RID: 3821
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Pause(ParticleSystem self);

		// Token: 0x06000EEE RID: 3822
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Clear(ParticleSystem self);

		// Token: 0x06000EEF RID: 3823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsAlive(ParticleSystem self);

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000126A0 File Offset: 0x000108A0
		[ExcludeFromDocs]
		public void Simulate(float t, bool withChildren)
		{
			bool restart = true;
			this.Simulate(t, withChildren, restart);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000126B8 File Offset: 0x000108B8
		[ExcludeFromDocs]
		public void Simulate(float t)
		{
			bool restart = true;
			bool withChildren = true;
			this.Simulate(t, withChildren, restart);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000126D4 File Offset: 0x000108D4
		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart)
		{
			this.IterateParticleSystems(withChildren, (ParticleSystem ps) => ParticleSystem.Internal_Simulate(ps, t, restart));
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0001270C File Offset: 0x0001090C
		[ExcludeFromDocs]
		public void Play()
		{
			bool withChildren = true;
			this.Play(withChildren);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00012724 File Offset: 0x00010924
		public void Play([DefaultValue("true")] bool withChildren)
		{
			this.IterateParticleSystems(withChildren, (ParticleSystem ps) => ParticleSystem.Internal_Play(ps));
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0001274C File Offset: 0x0001094C
		[ExcludeFromDocs]
		public void Stop()
		{
			bool withChildren = true;
			this.Stop(withChildren);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00012764 File Offset: 0x00010964
		public void Stop([DefaultValue("true")] bool withChildren)
		{
			this.IterateParticleSystems(withChildren, (ParticleSystem ps) => ParticleSystem.Internal_Stop(ps));
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0001278C File Offset: 0x0001098C
		[ExcludeFromDocs]
		public void Pause()
		{
			bool withChildren = true;
			this.Pause(withChildren);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000127A4 File Offset: 0x000109A4
		public void Pause([DefaultValue("true")] bool withChildren)
		{
			this.IterateParticleSystems(withChildren, (ParticleSystem ps) => ParticleSystem.Internal_Pause(ps));
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000127CC File Offset: 0x000109CC
		[ExcludeFromDocs]
		public void Clear()
		{
			bool withChildren = true;
			this.Clear(withChildren);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000127E4 File Offset: 0x000109E4
		public void Clear([DefaultValue("true")] bool withChildren)
		{
			this.IterateParticleSystems(withChildren, (ParticleSystem ps) => ParticleSystem.Internal_Clear(ps));
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0001280C File Offset: 0x00010A0C
		[ExcludeFromDocs]
		public bool IsAlive()
		{
			bool withChildren = true;
			return this.IsAlive(withChildren);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00012824 File Offset: 0x00010A24
		public bool IsAlive([DefaultValue("true")] bool withChildren)
		{
			return this.IterateParticleSystems(withChildren, (ParticleSystem ps) => ParticleSystem.Internal_IsAlive(ps));
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00012858 File Offset: 0x00010A58
		public void Emit(int count)
		{
			ParticleSystem.INTERNAL_CALL_Emit(this, count);
		}

		// Token: 0x06000EFE RID: 3838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Emit(ParticleSystem self, int count);

		// Token: 0x06000EFF RID: 3839 RVA: 0x00012864 File Offset: 0x00010A64
		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties")]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.startSize = size;
			particle.rotation3D = Vector3.zero;
			particle.angularVelocity3D = Vector3.zero;
			particle.startColor = color;
			particle.randomSeed = 5U;
			this.Internal_EmitOld(ref particle);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x000128D4 File Offset: 0x00010AD4
		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties")]
		public void Emit(ParticleSystem.Particle particle)
		{
			this.Internal_EmitOld(ref particle);
		}

		// Token: 0x06000F01 RID: 3841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_EmitOld(ref ParticleSystem.Particle particle);

		// Token: 0x06000F02 RID: 3842 RVA: 0x000128E0 File Offset: 0x00010AE0
		public void Emit(ParticleSystem.EmitParams emitParams, int count)
		{
			this.Internal_Emit(ref emitParams, count);
		}

		// Token: 0x06000F03 RID: 3843
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Emit(ref ParticleSystem.EmitParams emitParams, int count);

		// Token: 0x06000F04 RID: 3844 RVA: 0x000128EC File Offset: 0x00010AEC
		internal bool IterateParticleSystems(bool recurse, ParticleSystem.IteratorDelegate func)
		{
			bool flag = func(this);
			if (recurse)
			{
				flag |= ParticleSystem.IterateParticleSystemsRecursive(base.transform, func);
			}
			return flag;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00012918 File Offset: 0x00010B18
		private static bool IterateParticleSystemsRecursive(Transform transform, ParticleSystem.IteratorDelegate func)
		{
			bool flag = false;
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				ParticleSystem component = transform2.gameObject.GetComponent<ParticleSystem>();
				if (component != null)
				{
					flag = func(component);
					if (flag)
					{
						break;
					}
					ParticleSystem.IterateParticleSystemsRecursive(transform2, func);
				}
			}
			return flag;
		}

		// Token: 0x020000F6 RID: 246
		public struct Burst
		{
			// Token: 0x06000F0B RID: 3851 RVA: 0x000129DC File Offset: 0x00010BDC
			public Burst(float _time, short _count)
			{
				this.m_Time = _time;
				this.m_MinCount = _count;
				this.m_MaxCount = _count;
			}

			// Token: 0x06000F0C RID: 3852 RVA: 0x000129F4 File Offset: 0x00010BF4
			public Burst(float _time, short _minCount, short _maxCount)
			{
				this.m_Time = _time;
				this.m_MinCount = _minCount;
				this.m_MaxCount = _maxCount;
			}

			// Token: 0x1700037A RID: 890
			// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00012A0C File Offset: 0x00010C0C
			// (set) Token: 0x06000F0E RID: 3854 RVA: 0x00012A14 File Offset: 0x00010C14
			public float time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			// Token: 0x1700037B RID: 891
			// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00012A20 File Offset: 0x00010C20
			// (set) Token: 0x06000F10 RID: 3856 RVA: 0x00012A28 File Offset: 0x00010C28
			public short minCount
			{
				get
				{
					return this.m_MinCount;
				}
				set
				{
					this.m_MinCount = value;
				}
			}

			// Token: 0x1700037C RID: 892
			// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00012A34 File Offset: 0x00010C34
			// (set) Token: 0x06000F12 RID: 3858 RVA: 0x00012A3C File Offset: 0x00010C3C
			public short maxCount
			{
				get
				{
					return this.m_MaxCount;
				}
				set
				{
					this.m_MaxCount = value;
				}
			}

			// Token: 0x040002E6 RID: 742
			private float m_Time;

			// Token: 0x040002E7 RID: 743
			private short m_MinCount;

			// Token: 0x040002E8 RID: 744
			private short m_MaxCount;
		}

		// Token: 0x020000F7 RID: 247
		public struct MinMaxCurve
		{
			// Token: 0x06000F13 RID: 3859 RVA: 0x00012A48 File Offset: 0x00010C48
			public MinMaxCurve(float constant)
			{
				this.m_Mode = ParticleSystemCurveMode.Constant;
				this.m_CurveScalar = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = constant;
			}

			// Token: 0x06000F14 RID: 3860 RVA: 0x00012A88 File Offset: 0x00010C88
			public MinMaxCurve(float scalar, AnimationCurve curve)
			{
				this.m_Mode = ParticleSystemCurveMode.Curve;
				this.m_CurveScalar = scalar;
				this.m_CurveMin = null;
				this.m_CurveMax = curve;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x06000F15 RID: 3861 RVA: 0x00012AC8 File Offset: 0x00010CC8
			public MinMaxCurve(float scalar, AnimationCurve min, AnimationCurve max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoCurves;
				this.m_CurveScalar = scalar;
				this.m_CurveMin = min;
				this.m_CurveMax = max;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x06000F16 RID: 3862 RVA: 0x00012B08 File Offset: 0x00010D08
			public MinMaxCurve(float min, float max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoConstants;
				this.m_CurveScalar = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = min;
				this.m_ConstantMax = max;
			}

			// Token: 0x1700037D RID: 893
			// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00012B44 File Offset: 0x00010D44
			// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00012B4C File Offset: 0x00010D4C
			public ParticleSystemCurveMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x1700037E RID: 894
			// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00012B58 File Offset: 0x00010D58
			// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00012B60 File Offset: 0x00010D60
			public float curveScalar
			{
				get
				{
					return this.m_CurveScalar;
				}
				set
				{
					this.m_CurveScalar = value;
				}
			}

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00012B6C File Offset: 0x00010D6C
			// (set) Token: 0x06000F1C RID: 3868 RVA: 0x00012B74 File Offset: 0x00010D74
			public AnimationCurve curveMax
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00012B80 File Offset: 0x00010D80
			// (set) Token: 0x06000F1E RID: 3870 RVA: 0x00012B88 File Offset: 0x00010D88
			public AnimationCurve curveMin
			{
				get
				{
					return this.m_CurveMin;
				}
				set
				{
					this.m_CurveMin = value;
				}
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00012B94 File Offset: 0x00010D94
			// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00012B9C File Offset: 0x00010D9C
			public float constantMax
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00012BA8 File Offset: 0x00010DA8
			// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00012BB0 File Offset: 0x00010DB0
			public float constantMin
			{
				get
				{
					return this.m_ConstantMin;
				}
				set
				{
					this.m_ConstantMin = value;
				}
			}

			// Token: 0x040002E9 RID: 745
			private ParticleSystemCurveMode m_Mode;

			// Token: 0x040002EA RID: 746
			private float m_CurveScalar;

			// Token: 0x040002EB RID: 747
			private AnimationCurve m_CurveMin;

			// Token: 0x040002EC RID: 748
			private AnimationCurve m_CurveMax;

			// Token: 0x040002ED RID: 749
			private float m_ConstantMin;

			// Token: 0x040002EE RID: 750
			private float m_ConstantMax;
		}

		// Token: 0x020000F8 RID: 248
		public struct MinMaxGradient
		{
			// Token: 0x06000F23 RID: 3875 RVA: 0x00012BBC File Offset: 0x00010DBC
			public MinMaxGradient(Color color)
			{
				this.m_Mode = ParticleSystemGradientMode.Color;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = color;
			}

			// Token: 0x06000F24 RID: 3876 RVA: 0x00012BE8 File Offset: 0x00010DE8
			public MinMaxGradient(Gradient gradient)
			{
				this.m_Mode = ParticleSystemGradientMode.Gradient;
				this.m_GradientMin = null;
				this.m_GradientMax = gradient;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x06000F25 RID: 3877 RVA: 0x00012C18 File Offset: 0x00010E18
			public MinMaxGradient(Color min, Color max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoColors;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = min;
				this.m_ColorMax = max;
			}

			// Token: 0x06000F26 RID: 3878 RVA: 0x00012C40 File Offset: 0x00010E40
			public MinMaxGradient(Gradient min, Gradient max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoGradients;
				this.m_GradientMin = min;
				this.m_GradientMax = max;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00012C70 File Offset: 0x00010E70
			// (set) Token: 0x06000F28 RID: 3880 RVA: 0x00012C78 File Offset: 0x00010E78
			public ParticleSystemGradientMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x17000384 RID: 900
			// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00012C84 File Offset: 0x00010E84
			// (set) Token: 0x06000F2A RID: 3882 RVA: 0x00012C8C File Offset: 0x00010E8C
			public Gradient gradientMax
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00012C98 File Offset: 0x00010E98
			// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00012CA0 File Offset: 0x00010EA0
			public Gradient gradientMin
			{
				get
				{
					return this.m_GradientMin;
				}
				set
				{
					this.m_GradientMin = value;
				}
			}

			// Token: 0x17000386 RID: 902
			// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00012CAC File Offset: 0x00010EAC
			// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00012CB4 File Offset: 0x00010EB4
			public Color colorMax
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x17000387 RID: 903
			// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00012CC0 File Offset: 0x00010EC0
			// (set) Token: 0x06000F30 RID: 3888 RVA: 0x00012CC8 File Offset: 0x00010EC8
			public Color colorMin
			{
				get
				{
					return this.m_ColorMin;
				}
				set
				{
					this.m_ColorMin = value;
				}
			}

			// Token: 0x040002EF RID: 751
			private ParticleSystemGradientMode m_Mode;

			// Token: 0x040002F0 RID: 752
			private Gradient m_GradientMin;

			// Token: 0x040002F1 RID: 753
			private Gradient m_GradientMax;

			// Token: 0x040002F2 RID: 754
			private Color m_ColorMin;

			// Token: 0x040002F3 RID: 755
			private Color m_ColorMax;
		}

		// Token: 0x020000F9 RID: 249
		public struct EmissionModule
		{
			// Token: 0x06000F31 RID: 3889 RVA: 0x00012CD4 File Offset: 0x00010ED4
			internal EmissionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000388 RID: 904
			// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00012CF0 File Offset: 0x00010EF0
			// (set) Token: 0x06000F32 RID: 3890 RVA: 0x00012CE0 File Offset: 0x00010EE0
			public bool enabled
			{
				get
				{
					return ParticleSystem.EmissionModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.EmissionModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000389 RID: 905
			// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00012D10 File Offset: 0x00010F10
			// (set) Token: 0x06000F34 RID: 3892 RVA: 0x00012D00 File Offset: 0x00010F00
			public ParticleSystem.MinMaxCurve rate
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.EmissionModule.GetRate(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.EmissionModule.SetRate(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x1700038A RID: 906
			// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00012D34 File Offset: 0x00010F34
			// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00012D44 File Offset: 0x00010F44
			public ParticleSystemEmissionType type
			{
				get
				{
					return (ParticleSystemEmissionType)ParticleSystem.EmissionModule.GetType(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.EmissionModule.SetType(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x06000F38 RID: 3896 RVA: 0x00012D54 File Offset: 0x00010F54
			public void SetBursts(ParticleSystem.Burst[] bursts)
			{
				ParticleSystem.EmissionModule.SetBursts(this.m_ParticleSystem, bursts, bursts.Length);
			}

			// Token: 0x06000F39 RID: 3897 RVA: 0x00012D68 File Offset: 0x00010F68
			public void SetBursts(ParticleSystem.Burst[] bursts, int size)
			{
				ParticleSystem.EmissionModule.SetBursts(this.m_ParticleSystem, bursts, size);
			}

			// Token: 0x06000F3A RID: 3898 RVA: 0x00012D78 File Offset: 0x00010F78
			public int GetBursts(ParticleSystem.Burst[] bursts)
			{
				return ParticleSystem.EmissionModule.GetBursts(this.m_ParticleSystem, bursts);
			}

			// Token: 0x1700038B RID: 907
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00012D88 File Offset: 0x00010F88
			public int burstCount
			{
				get
				{
					return ParticleSystem.EmissionModule.GetBurstCount(this.m_ParticleSystem);
				}
			}

			// Token: 0x06000F3C RID: 3900
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000F3D RID: 3901
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000F3E RID: 3902
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetType(ParticleSystem system, int value);

			// Token: 0x06000F3F RID: 3903
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetType(ParticleSystem system);

			// Token: 0x06000F40 RID: 3904
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetBurstCount(ParticleSystem system);

			// Token: 0x06000F41 RID: 3905
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetRate(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F42 RID: 3906
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetRate(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F43 RID: 3907
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBursts(ParticleSystem system, ParticleSystem.Burst[] bursts, int size);

			// Token: 0x06000F44 RID: 3908
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetBursts(ParticleSystem system, ParticleSystem.Burst[] bursts);

			// Token: 0x040002F4 RID: 756
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x020000FA RID: 250
		public struct ShapeModule
		{
			// Token: 0x06000F45 RID: 3909 RVA: 0x00012D98 File Offset: 0x00010F98
			internal ShapeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700038C RID: 908
			// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00012DB4 File Offset: 0x00010FB4
			// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00012DA4 File Offset: 0x00010FA4
			public bool enabled
			{
				get
				{
					return ParticleSystem.ShapeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x1700038D RID: 909
			// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00012DC4 File Offset: 0x00010FC4
			// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00012DD4 File Offset: 0x00010FD4
			public ParticleSystemShapeType shapeType
			{
				get
				{
					return (ParticleSystemShapeType)ParticleSystem.ShapeModule.GetShapeType(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetShapeType(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x1700038E RID: 910
			// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00012DE4 File Offset: 0x00010FE4
			// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00012DF4 File Offset: 0x00010FF4
			public bool randomDirection
			{
				get
				{
					return ParticleSystem.ShapeModule.GetRandomDirection(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetRandomDirection(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x1700038F RID: 911
			// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00012E04 File Offset: 0x00011004
			// (set) Token: 0x06000F4D RID: 3917 RVA: 0x00012E14 File Offset: 0x00011014
			public float radius
			{
				get
				{
					return ParticleSystem.ShapeModule.GetRadius(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetRadius(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00012E24 File Offset: 0x00011024
			// (set) Token: 0x06000F4F RID: 3919 RVA: 0x00012E34 File Offset: 0x00011034
			public float angle
			{
				get
				{
					return ParticleSystem.ShapeModule.GetAngle(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetAngle(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000391 RID: 913
			// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00012E44 File Offset: 0x00011044
			// (set) Token: 0x06000F51 RID: 3921 RVA: 0x00012E54 File Offset: 0x00011054
			public float length
			{
				get
				{
					return ParticleSystem.ShapeModule.GetLength(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetLength(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000392 RID: 914
			// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00012E64 File Offset: 0x00011064
			// (set) Token: 0x06000F53 RID: 3923 RVA: 0x00012E74 File Offset: 0x00011074
			public Vector3 box
			{
				get
				{
					return ParticleSystem.ShapeModule.GetBox(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetBox(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000393 RID: 915
			// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00012E84 File Offset: 0x00011084
			// (set) Token: 0x06000F55 RID: 3925 RVA: 0x00012E94 File Offset: 0x00011094
			public ParticleSystemMeshShapeType meshShapeType
			{
				get
				{
					return (ParticleSystemMeshShapeType)ParticleSystem.ShapeModule.GetMeshShapeType(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetMeshShapeType(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x17000394 RID: 916
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00012EA4 File Offset: 0x000110A4
			// (set) Token: 0x06000F57 RID: 3927 RVA: 0x00012EB4 File Offset: 0x000110B4
			public Mesh mesh
			{
				get
				{
					return ParticleSystem.ShapeModule.GetMesh(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetMesh(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000395 RID: 917
			// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00012EC4 File Offset: 0x000110C4
			// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00012ED4 File Offset: 0x000110D4
			public MeshRenderer meshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.GetMeshRenderer(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetMeshRenderer(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000396 RID: 918
			// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00012EE4 File Offset: 0x000110E4
			// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00012EF4 File Offset: 0x000110F4
			public SkinnedMeshRenderer skinnedMeshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.GetSkinnedMeshRenderer(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetSkinnedMeshRenderer(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000397 RID: 919
			// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00012F04 File Offset: 0x00011104
			// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00012F14 File Offset: 0x00011114
			public bool useMeshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.GetUseMeshMaterialIndex(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetUseMeshMaterialIndex(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000398 RID: 920
			// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00012F24 File Offset: 0x00011124
			// (set) Token: 0x06000F5F RID: 3935 RVA: 0x00012F34 File Offset: 0x00011134
			public int meshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.GetMeshMaterialIndex(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetMeshMaterialIndex(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x17000399 RID: 921
			// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00012F44 File Offset: 0x00011144
			// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00012F54 File Offset: 0x00011154
			public bool useMeshColors
			{
				get
				{
					return ParticleSystem.ShapeModule.GetUseMeshColors(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetUseMeshColors(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x1700039A RID: 922
			// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00012F64 File Offset: 0x00011164
			// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00012F74 File Offset: 0x00011174
			public float normalOffset
			{
				get
				{
					return ParticleSystem.ShapeModule.GetNormalOffset(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetNormalOffset(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x1700039B RID: 923
			// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00012F84 File Offset: 0x00011184
			// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00012F94 File Offset: 0x00011194
			public float arc
			{
				get
				{
					return ParticleSystem.ShapeModule.GetArc(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ShapeModule.SetArc(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x06000F66 RID: 3942
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000F67 RID: 3943
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000F68 RID: 3944
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetShapeType(ParticleSystem system, int value);

			// Token: 0x06000F69 RID: 3945
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetShapeType(ParticleSystem system);

			// Token: 0x06000F6A RID: 3946
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetRandomDirection(ParticleSystem system, bool value);

			// Token: 0x06000F6B RID: 3947
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetRandomDirection(ParticleSystem system);

			// Token: 0x06000F6C RID: 3948
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetRadius(ParticleSystem system, float value);

			// Token: 0x06000F6D RID: 3949
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetRadius(ParticleSystem system);

			// Token: 0x06000F6E RID: 3950
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetAngle(ParticleSystem system, float value);

			// Token: 0x06000F6F RID: 3951
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetAngle(ParticleSystem system);

			// Token: 0x06000F70 RID: 3952
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetLength(ParticleSystem system, float value);

			// Token: 0x06000F71 RID: 3953
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetLength(ParticleSystem system);

			// Token: 0x06000F72 RID: 3954 RVA: 0x00012FA4 File Offset: 0x000111A4
			private static void SetBox(ParticleSystem system, Vector3 value)
			{
				ParticleSystem.ShapeModule.INTERNAL_CALL_SetBox(system, ref value);
			}

			// Token: 0x06000F73 RID: 3955
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_SetBox(ParticleSystem system, ref Vector3 value);

			// Token: 0x06000F74 RID: 3956 RVA: 0x00012FB0 File Offset: 0x000111B0
			private static Vector3 GetBox(ParticleSystem system)
			{
				Vector3 result;
				ParticleSystem.ShapeModule.INTERNAL_CALL_GetBox(system, out result);
				return result;
			}

			// Token: 0x06000F75 RID: 3957
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_GetBox(ParticleSystem system, out Vector3 value);

			// Token: 0x06000F76 RID: 3958
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMeshShapeType(ParticleSystem system, int value);

			// Token: 0x06000F77 RID: 3959
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetMeshShapeType(ParticleSystem system);

			// Token: 0x06000F78 RID: 3960
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMesh(ParticleSystem system, Mesh value);

			// Token: 0x06000F79 RID: 3961
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Mesh GetMesh(ParticleSystem system);

			// Token: 0x06000F7A RID: 3962
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMeshRenderer(ParticleSystem system, MeshRenderer value);

			// Token: 0x06000F7B RID: 3963
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern MeshRenderer GetMeshRenderer(ParticleSystem system);

			// Token: 0x06000F7C RID: 3964
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSkinnedMeshRenderer(ParticleSystem system, SkinnedMeshRenderer value);

			// Token: 0x06000F7D RID: 3965
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern SkinnedMeshRenderer GetSkinnedMeshRenderer(ParticleSystem system);

			// Token: 0x06000F7E RID: 3966
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetUseMeshMaterialIndex(ParticleSystem system, bool value);

			// Token: 0x06000F7F RID: 3967
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetUseMeshMaterialIndex(ParticleSystem system);

			// Token: 0x06000F80 RID: 3968
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMeshMaterialIndex(ParticleSystem system, int value);

			// Token: 0x06000F81 RID: 3969
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetMeshMaterialIndex(ParticleSystem system);

			// Token: 0x06000F82 RID: 3970
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetUseMeshColors(ParticleSystem system, bool value);

			// Token: 0x06000F83 RID: 3971
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetUseMeshColors(ParticleSystem system);

			// Token: 0x06000F84 RID: 3972
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetNormalOffset(ParticleSystem system, float value);

			// Token: 0x06000F85 RID: 3973
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetNormalOffset(ParticleSystem system);

			// Token: 0x06000F86 RID: 3974
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetArc(ParticleSystem system, float value);

			// Token: 0x06000F87 RID: 3975
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetArc(ParticleSystem system);

			// Token: 0x040002F5 RID: 757
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x020000FB RID: 251
		public struct VelocityOverLifetimeModule
		{
			// Token: 0x06000F88 RID: 3976 RVA: 0x00012FC8 File Offset: 0x000111C8
			internal VelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00012FE4 File Offset: 0x000111E4
			// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00012FD4 File Offset: 0x000111D4
			public bool enabled
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x1700039D RID: 925
			// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00013004 File Offset: 0x00011204
			// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00012FF4 File Offset: 0x000111F4
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.VelocityOverLifetimeModule.GetX(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.SetX(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x1700039E RID: 926
			// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00013038 File Offset: 0x00011238
			// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00013028 File Offset: 0x00011228
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.VelocityOverLifetimeModule.GetY(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.SetY(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x1700039F RID: 927
			// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0001306C File Offset: 0x0001126C
			// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0001305C File Offset: 0x0001125C
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.VelocityOverLifetimeModule.GetZ(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.SetZ(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003A0 RID: 928
			// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00013090 File Offset: 0x00011290
			// (set) Token: 0x06000F92 RID: 3986 RVA: 0x000130AC File Offset: 0x000112AC
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return (!ParticleSystem.VelocityOverLifetimeModule.GetWorldSpace(this.m_ParticleSystem)) ? ParticleSystemSimulationSpace.Local : ParticleSystemSimulationSpace.World;
				}
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.SetWorldSpace(this.m_ParticleSystem, value == ParticleSystemSimulationSpace.World);
				}
			}

			// Token: 0x06000F93 RID: 3987
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000F94 RID: 3988
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000F95 RID: 3989
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F96 RID: 3990
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F97 RID: 3991
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F98 RID: 3992
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F99 RID: 3993
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F9A RID: 3994
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000F9B RID: 3995
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetWorldSpace(ParticleSystem system, bool value);

			// Token: 0x06000F9C RID: 3996
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetWorldSpace(ParticleSystem system);

			// Token: 0x040002F6 RID: 758
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x020000FC RID: 252
		public struct LimitVelocityOverLifetimeModule
		{
			// Token: 0x06000F9D RID: 3997 RVA: 0x000130C0 File Offset: 0x000112C0
			internal LimitVelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000130DC File Offset: 0x000112DC
			// (set) Token: 0x06000F9E RID: 3998 RVA: 0x000130CC File Offset: 0x000112CC
			public bool enabled
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x000130FC File Offset: 0x000112FC
			// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x000130EC File Offset: 0x000112EC
			public ParticleSystem.MinMaxCurve limitX
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.LimitVelocityOverLifetimeModule.GetX(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetX(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00013130 File Offset: 0x00011330
			// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00013120 File Offset: 0x00011320
			public ParticleSystem.MinMaxCurve limitY
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.LimitVelocityOverLifetimeModule.GetY(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetY(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00013164 File Offset: 0x00011364
			// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00013154 File Offset: 0x00011354
			public ParticleSystem.MinMaxCurve limitZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.LimitVelocityOverLifetimeModule.GetZ(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetZ(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00013198 File Offset: 0x00011398
			// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00013188 File Offset: 0x00011388
			public ParticleSystem.MinMaxCurve limit
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.LimitVelocityOverLifetimeModule.GetMagnitude(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetMagnitude(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x000131BC File Offset: 0x000113BC
			// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x000131CC File Offset: 0x000113CC
			public float dampen
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.GetDampen(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetDampen(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x06000FAA RID: 4010 RVA: 0x000131DC File Offset: 0x000113DC
			// (set) Token: 0x06000FAB RID: 4011 RVA: 0x000131EC File Offset: 0x000113EC
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.GetSeparateAxes(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetSeparateAxes(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000131FC File Offset: 0x000113FC
			// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00013218 File Offset: 0x00011418
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return (!ParticleSystem.LimitVelocityOverLifetimeModule.GetWorldSpace(this.m_ParticleSystem)) ? ParticleSystemSimulationSpace.Local : ParticleSystemSimulationSpace.World;
				}
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.SetWorldSpace(this.m_ParticleSystem, value == ParticleSystemSimulationSpace.World);
				}
			}

			// Token: 0x06000FAE RID: 4014
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000FAF RID: 4015
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000FB0 RID: 4016
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB1 RID: 4017
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB2 RID: 4018
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB3 RID: 4019
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB4 RID: 4020
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB5 RID: 4021
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB6 RID: 4022
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMagnitude(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB7 RID: 4023
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetMagnitude(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FB8 RID: 4024
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetDampen(ParticleSystem system, float value);

			// Token: 0x06000FB9 RID: 4025
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetDampen(ParticleSystem system);

			// Token: 0x06000FBA RID: 4026
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSeparateAxes(ParticleSystem system, bool value);

			// Token: 0x06000FBB RID: 4027
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetSeparateAxes(ParticleSystem system);

			// Token: 0x06000FBC RID: 4028
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetWorldSpace(ParticleSystem system, bool value);

			// Token: 0x06000FBD RID: 4029
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetWorldSpace(ParticleSystem system);

			// Token: 0x040002F7 RID: 759
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x020000FD RID: 253
		public struct InheritVelocityModule
		{
			// Token: 0x06000FBE RID: 4030 RVA: 0x0001322C File Offset: 0x0001142C
			internal InheritVelocityModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00013248 File Offset: 0x00011448
			// (set) Token: 0x06000FBF RID: 4031 RVA: 0x00013238 File Offset: 0x00011438
			public bool enabled
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.InheritVelocityModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00013258 File Offset: 0x00011458
			// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00013268 File Offset: 0x00011468
			public ParticleSystemInheritVelocityMode mode
			{
				get
				{
					return (ParticleSystemInheritVelocityMode)ParticleSystem.InheritVelocityModule.GetMode(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.InheritVelocityModule.SetMode(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00013288 File Offset: 0x00011488
			// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00013278 File Offset: 0x00011478
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.InheritVelocityModule.GetCurve(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.InheritVelocityModule.SetCurve(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x06000FC5 RID: 4037
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000FC6 RID: 4038
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000FC7 RID: 4039
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMode(ParticleSystem system, int value);

			// Token: 0x06000FC8 RID: 4040
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetMode(ParticleSystem system);

			// Token: 0x06000FC9 RID: 4041
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCurve(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FCA RID: 4042
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetCurve(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x040002F8 RID: 760
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x020000FE RID: 254
		public struct ForceOverLifetimeModule
		{
			// Token: 0x06000FCB RID: 4043 RVA: 0x000132AC File Offset: 0x000114AC
			internal ForceOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06000FCD RID: 4045 RVA: 0x000132C8 File Offset: 0x000114C8
			// (set) Token: 0x06000FCC RID: 4044 RVA: 0x000132B8 File Offset: 0x000114B8
			public bool enabled
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ForceOverLifetimeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06000FCF RID: 4047 RVA: 0x000132E8 File Offset: 0x000114E8
			// (set) Token: 0x06000FCE RID: 4046 RVA: 0x000132D8 File Offset: 0x000114D8
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.ForceOverLifetimeModule.GetX(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.ForceOverLifetimeModule.SetX(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0001331C File Offset: 0x0001151C
			// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0001330C File Offset: 0x0001150C
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.ForceOverLifetimeModule.GetY(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.ForceOverLifetimeModule.SetY(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00013350 File Offset: 0x00011550
			// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00013340 File Offset: 0x00011540
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.ForceOverLifetimeModule.GetZ(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.ForceOverLifetimeModule.SetZ(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00013374 File Offset: 0x00011574
			// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00013390 File Offset: 0x00011590
			public ParticleSystemSimulationSpace space
			{
				get
				{
					return (!ParticleSystem.ForceOverLifetimeModule.GetWorldSpace(this.m_ParticleSystem)) ? ParticleSystemSimulationSpace.Local : ParticleSystemSimulationSpace.World;
				}
				set
				{
					ParticleSystem.ForceOverLifetimeModule.SetWorldSpace(this.m_ParticleSystem, value == ParticleSystemSimulationSpace.World);
				}
			}

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000133B4 File Offset: 0x000115B4
			// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x000133A4 File Offset: 0x000115A4
			public bool randomized
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.GetRandomized(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ForceOverLifetimeModule.SetRandomized(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x06000FD8 RID: 4056
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000FD9 RID: 4057
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000FDA RID: 4058
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FDB RID: 4059
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FDC RID: 4060
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FDD RID: 4061
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FDE RID: 4062
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FDF RID: 4063
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06000FE0 RID: 4064
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetWorldSpace(ParticleSystem system, bool value);

			// Token: 0x06000FE1 RID: 4065
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetWorldSpace(ParticleSystem system);

			// Token: 0x06000FE2 RID: 4066
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetRandomized(ParticleSystem system, bool value);

			// Token: 0x06000FE3 RID: 4067
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetRandomized(ParticleSystem system);

			// Token: 0x040002F9 RID: 761
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x020000FF RID: 255
		public struct ColorOverLifetimeModule
		{
			// Token: 0x06000FE4 RID: 4068 RVA: 0x000133C4 File Offset: 0x000115C4
			internal ColorOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000133E0 File Offset: 0x000115E0
			// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000133D0 File Offset: 0x000115D0
			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorOverLifetimeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ColorOverLifetimeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00013400 File Offset: 0x00011600
			// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x000133F0 File Offset: 0x000115F0
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient result = default(ParticleSystem.MinMaxGradient);
					ParticleSystem.ColorOverLifetimeModule.GetColor(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.ColorOverLifetimeModule.SetColor(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x06000FE9 RID: 4073
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000FEA RID: 4074
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000FEB RID: 4075
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetColor(ParticleSystem system, ref ParticleSystem.MinMaxGradient gradient);

			// Token: 0x06000FEC RID: 4076
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetColor(ParticleSystem system, ref ParticleSystem.MinMaxGradient gradient);

			// Token: 0x040002FA RID: 762
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000100 RID: 256
		public struct ColorBySpeedModule
		{
			// Token: 0x06000FED RID: 4077 RVA: 0x00013424 File Offset: 0x00011624
			internal ColorBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00013440 File Offset: 0x00011640
			// (set) Token: 0x06000FEE RID: 4078 RVA: 0x00013430 File Offset: 0x00011630
			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorBySpeedModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ColorBySpeedModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00013460 File Offset: 0x00011660
			// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x00013450 File Offset: 0x00011650
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient result = default(ParticleSystem.MinMaxGradient);
					ParticleSystem.ColorBySpeedModule.GetColor(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.ColorBySpeedModule.SetColor(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00013494 File Offset: 0x00011694
			// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00013484 File Offset: 0x00011684
			public Vector2 range
			{
				get
				{
					return ParticleSystem.ColorBySpeedModule.GetRange(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ColorBySpeedModule.SetRange(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x06000FF4 RID: 4084
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06000FF5 RID: 4085
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06000FF6 RID: 4086
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetColor(ParticleSystem system, ref ParticleSystem.MinMaxGradient gradient);

			// Token: 0x06000FF7 RID: 4087
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetColor(ParticleSystem system, ref ParticleSystem.MinMaxGradient gradient);

			// Token: 0x06000FF8 RID: 4088 RVA: 0x000134A4 File Offset: 0x000116A4
			private static void SetRange(ParticleSystem system, Vector2 value)
			{
				ParticleSystem.ColorBySpeedModule.INTERNAL_CALL_SetRange(system, ref value);
			}

			// Token: 0x06000FF9 RID: 4089
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_SetRange(ParticleSystem system, ref Vector2 value);

			// Token: 0x06000FFA RID: 4090 RVA: 0x000134B0 File Offset: 0x000116B0
			private static Vector2 GetRange(ParticleSystem system)
			{
				Vector2 result;
				ParticleSystem.ColorBySpeedModule.INTERNAL_CALL_GetRange(system, out result);
				return result;
			}

			// Token: 0x06000FFB RID: 4091
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_GetRange(ParticleSystem system, out Vector2 value);

			// Token: 0x040002FB RID: 763
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000101 RID: 257
		public struct SizeOverLifetimeModule
		{
			// Token: 0x06000FFC RID: 4092 RVA: 0x000134C8 File Offset: 0x000116C8
			internal SizeOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06000FFE RID: 4094 RVA: 0x000134E4 File Offset: 0x000116E4
			// (set) Token: 0x06000FFD RID: 4093 RVA: 0x000134D4 File Offset: 0x000116D4
			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.SizeOverLifetimeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x06001000 RID: 4096 RVA: 0x00013504 File Offset: 0x00011704
			// (set) Token: 0x06000FFF RID: 4095 RVA: 0x000134F4 File Offset: 0x000116F4
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.SizeOverLifetimeModule.GetSize(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.SizeOverLifetimeModule.SetSize(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x06001001 RID: 4097
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06001002 RID: 4098
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06001003 RID: 4099
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSize(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001004 RID: 4100
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetSize(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x040002FC RID: 764
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000102 RID: 258
		public struct SizeBySpeedModule
		{
			// Token: 0x06001005 RID: 4101 RVA: 0x00013528 File Offset: 0x00011728
			internal SizeBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06001007 RID: 4103 RVA: 0x00013544 File Offset: 0x00011744
			// (set) Token: 0x06001006 RID: 4102 RVA: 0x00013534 File Offset: 0x00011734
			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.SizeBySpeedModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x06001009 RID: 4105 RVA: 0x00013564 File Offset: 0x00011764
			// (set) Token: 0x06001008 RID: 4104 RVA: 0x00013554 File Offset: 0x00011754
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.SizeBySpeedModule.GetSize(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.SizeBySpeedModule.SetSize(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x0600100B RID: 4107 RVA: 0x00013598 File Offset: 0x00011798
			// (set) Token: 0x0600100A RID: 4106 RVA: 0x00013588 File Offset: 0x00011788
			public Vector2 range
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.GetRange(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.SizeBySpeedModule.SetRange(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x0600100C RID: 4108
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x0600100D RID: 4109
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x0600100E RID: 4110
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSize(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600100F RID: 4111
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetSize(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001010 RID: 4112 RVA: 0x000135A8 File Offset: 0x000117A8
			private static void SetRange(ParticleSystem system, Vector2 value)
			{
				ParticleSystem.SizeBySpeedModule.INTERNAL_CALL_SetRange(system, ref value);
			}

			// Token: 0x06001011 RID: 4113
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_SetRange(ParticleSystem system, ref Vector2 value);

			// Token: 0x06001012 RID: 4114 RVA: 0x000135B4 File Offset: 0x000117B4
			private static Vector2 GetRange(ParticleSystem system)
			{
				Vector2 result;
				ParticleSystem.SizeBySpeedModule.INTERNAL_CALL_GetRange(system, out result);
				return result;
			}

			// Token: 0x06001013 RID: 4115
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_GetRange(ParticleSystem system, out Vector2 value);

			// Token: 0x040002FD RID: 765
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000103 RID: 259
		public struct RotationOverLifetimeModule
		{
			// Token: 0x06001014 RID: 4116 RVA: 0x000135CC File Offset: 0x000117CC
			internal RotationOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x06001016 RID: 4118 RVA: 0x000135E8 File Offset: 0x000117E8
			// (set) Token: 0x06001015 RID: 4117 RVA: 0x000135D8 File Offset: 0x000117D8
			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.RotationOverLifetimeModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06001018 RID: 4120 RVA: 0x00013608 File Offset: 0x00011808
			// (set) Token: 0x06001017 RID: 4119 RVA: 0x000135F8 File Offset: 0x000117F8
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.RotationOverLifetimeModule.GetX(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.RotationOverLifetimeModule.SetX(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x0600101A RID: 4122 RVA: 0x0001363C File Offset: 0x0001183C
			// (set) Token: 0x06001019 RID: 4121 RVA: 0x0001362C File Offset: 0x0001182C
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.RotationOverLifetimeModule.GetY(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.RotationOverLifetimeModule.SetY(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x0600101C RID: 4124 RVA: 0x00013670 File Offset: 0x00011870
			// (set) Token: 0x0600101B RID: 4123 RVA: 0x00013660 File Offset: 0x00011860
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.RotationOverLifetimeModule.GetZ(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.RotationOverLifetimeModule.SetZ(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x0600101D RID: 4125 RVA: 0x00013694 File Offset: 0x00011894
			// (set) Token: 0x0600101E RID: 4126 RVA: 0x000136A4 File Offset: 0x000118A4
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.GetSeparateAxes(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.RotationOverLifetimeModule.SetSeparateAxes(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x0600101F RID: 4127
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06001020 RID: 4128
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06001021 RID: 4129
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001022 RID: 4130
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001023 RID: 4131
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001024 RID: 4132
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001025 RID: 4133
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001026 RID: 4134
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001027 RID: 4135
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSeparateAxes(ParticleSystem system, bool value);

			// Token: 0x06001028 RID: 4136
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetSeparateAxes(ParticleSystem system);

			// Token: 0x040002FE RID: 766
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000104 RID: 260
		public struct RotationBySpeedModule
		{
			// Token: 0x06001029 RID: 4137 RVA: 0x000136B4 File Offset: 0x000118B4
			internal RotationBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x0600102B RID: 4139 RVA: 0x000136D0 File Offset: 0x000118D0
			// (set) Token: 0x0600102A RID: 4138 RVA: 0x000136C0 File Offset: 0x000118C0
			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.RotationBySpeedModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x0600102D RID: 4141 RVA: 0x000136F0 File Offset: 0x000118F0
			// (set) Token: 0x0600102C RID: 4140 RVA: 0x000136E0 File Offset: 0x000118E0
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.RotationBySpeedModule.GetX(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.RotationBySpeedModule.SetX(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x0600102F RID: 4143 RVA: 0x00013724 File Offset: 0x00011924
			// (set) Token: 0x0600102E RID: 4142 RVA: 0x00013714 File Offset: 0x00011914
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.RotationBySpeedModule.GetY(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.RotationBySpeedModule.SetY(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06001031 RID: 4145 RVA: 0x00013758 File Offset: 0x00011958
			// (set) Token: 0x06001030 RID: 4144 RVA: 0x00013748 File Offset: 0x00011948
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.RotationBySpeedModule.GetZ(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.RotationBySpeedModule.SetZ(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06001032 RID: 4146 RVA: 0x0001377C File Offset: 0x0001197C
			// (set) Token: 0x06001033 RID: 4147 RVA: 0x0001378C File Offset: 0x0001198C
			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.GetSeparateAxes(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.RotationBySpeedModule.SetSeparateAxes(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06001035 RID: 4149 RVA: 0x000137AC File Offset: 0x000119AC
			// (set) Token: 0x06001034 RID: 4148 RVA: 0x0001379C File Offset: 0x0001199C
			public Vector2 range
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.GetRange(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.RotationBySpeedModule.SetRange(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x06001036 RID: 4150
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06001037 RID: 4151
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06001038 RID: 4152
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001039 RID: 4153
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetX(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600103A RID: 4154
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600103B RID: 4155
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetY(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600103C RID: 4156
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600103D RID: 4157
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetZ(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600103E RID: 4158
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSeparateAxes(ParticleSystem system, bool value);

			// Token: 0x0600103F RID: 4159
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetSeparateAxes(ParticleSystem system);

			// Token: 0x06001040 RID: 4160 RVA: 0x000137BC File Offset: 0x000119BC
			private static void SetRange(ParticleSystem system, Vector2 value)
			{
				ParticleSystem.RotationBySpeedModule.INTERNAL_CALL_SetRange(system, ref value);
			}

			// Token: 0x06001041 RID: 4161
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_SetRange(ParticleSystem system, ref Vector2 value);

			// Token: 0x06001042 RID: 4162 RVA: 0x000137C8 File Offset: 0x000119C8
			private static Vector2 GetRange(ParticleSystem system)
			{
				Vector2 result;
				ParticleSystem.RotationBySpeedModule.INTERNAL_CALL_GetRange(system, out result);
				return result;
			}

			// Token: 0x06001043 RID: 4163
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_GetRange(ParticleSystem system, out Vector2 value);

			// Token: 0x040002FF RID: 767
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000105 RID: 261
		public struct ExternalForcesModule
		{
			// Token: 0x06001044 RID: 4164 RVA: 0x000137E0 File Offset: 0x000119E0
			internal ExternalForcesModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06001046 RID: 4166 RVA: 0x000137FC File Offset: 0x000119FC
			// (set) Token: 0x06001045 RID: 4165 RVA: 0x000137EC File Offset: 0x000119EC
			public bool enabled
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ExternalForcesModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06001047 RID: 4167 RVA: 0x0001380C File Offset: 0x00011A0C
			// (set) Token: 0x06001048 RID: 4168 RVA: 0x0001381C File Offset: 0x00011A1C
			public float multiplier
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.GetMultiplier(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.ExternalForcesModule.SetMultiplier(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x06001049 RID: 4169
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x0600104A RID: 4170
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x0600104B RID: 4171
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMultiplier(ParticleSystem system, float value);

			// Token: 0x0600104C RID: 4172
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetMultiplier(ParticleSystem system);

			// Token: 0x04000300 RID: 768
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000106 RID: 262
		public struct CollisionModule
		{
			// Token: 0x0600104D RID: 4173 RVA: 0x0001382C File Offset: 0x00011A2C
			internal CollisionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x0600104F RID: 4175 RVA: 0x00013848 File Offset: 0x00011A48
			// (set) Token: 0x0600104E RID: 4174 RVA: 0x00013838 File Offset: 0x00011A38
			public bool enabled
			{
				get
				{
					return ParticleSystem.CollisionModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06001051 RID: 4177 RVA: 0x00013868 File Offset: 0x00011A68
			// (set) Token: 0x06001050 RID: 4176 RVA: 0x00013858 File Offset: 0x00011A58
			public ParticleSystemCollisionType type
			{
				get
				{
					return (ParticleSystemCollisionType)ParticleSystem.CollisionModule.GetType(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetType(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x06001053 RID: 4179 RVA: 0x00013888 File Offset: 0x00011A88
			// (set) Token: 0x06001052 RID: 4178 RVA: 0x00013878 File Offset: 0x00011A78
			public ParticleSystemCollisionMode mode
			{
				get
				{
					return (ParticleSystemCollisionMode)ParticleSystem.CollisionModule.GetMode(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetMode(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06001055 RID: 4181 RVA: 0x000138A8 File Offset: 0x00011AA8
			// (set) Token: 0x06001054 RID: 4180 RVA: 0x00013898 File Offset: 0x00011A98
			public ParticleSystem.MinMaxCurve dampen
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.CollisionModule.GetDampen(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.CollisionModule.SetDampen(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x06001057 RID: 4183 RVA: 0x000138DC File Offset: 0x00011ADC
			// (set) Token: 0x06001056 RID: 4182 RVA: 0x000138CC File Offset: 0x00011ACC
			public ParticleSystem.MinMaxCurve bounce
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.CollisionModule.GetBounce(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.CollisionModule.SetBounce(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x06001059 RID: 4185 RVA: 0x00013910 File Offset: 0x00011B10
			// (set) Token: 0x06001058 RID: 4184 RVA: 0x00013900 File Offset: 0x00011B00
			public ParticleSystem.MinMaxCurve lifetimeLoss
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.CollisionModule.GetEnergyLoss(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.CollisionModule.SetEnergyLoss(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x0600105A RID: 4186 RVA: 0x00013934 File Offset: 0x00011B34
			// (set) Token: 0x0600105B RID: 4187 RVA: 0x00013944 File Offset: 0x00011B44
			public float minKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.GetMinKillSpeed(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetMinKillSpeed(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x0600105C RID: 4188 RVA: 0x00013954 File Offset: 0x00011B54
			// (set) Token: 0x0600105D RID: 4189 RVA: 0x00013968 File Offset: 0x00011B68
			public LayerMask collidesWith
			{
				get
				{
					return ParticleSystem.CollisionModule.GetCollidesWith(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetCollidesWith(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x0600105E RID: 4190 RVA: 0x0001397C File Offset: 0x00011B7C
			// (set) Token: 0x0600105F RID: 4191 RVA: 0x0001398C File Offset: 0x00011B8C
			public bool enableDynamicColliders
			{
				get
				{
					return ParticleSystem.CollisionModule.GetEnableDynamicColliders(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetEnableDynamicColliders(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06001060 RID: 4192 RVA: 0x0001399C File Offset: 0x00011B9C
			// (set) Token: 0x06001061 RID: 4193 RVA: 0x000139AC File Offset: 0x00011BAC
			public bool enableInteriorCollisions
			{
				get
				{
					return ParticleSystem.CollisionModule.GetEnableInteriorCollisions(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetEnableInteriorCollisions(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06001062 RID: 4194 RVA: 0x000139BC File Offset: 0x00011BBC
			// (set) Token: 0x06001063 RID: 4195 RVA: 0x000139CC File Offset: 0x00011BCC
			public int maxCollisionShapes
			{
				get
				{
					return ParticleSystem.CollisionModule.GetMaxCollisionShapes(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetMaxCollisionShapes(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06001065 RID: 4197 RVA: 0x000139EC File Offset: 0x00011BEC
			// (set) Token: 0x06001064 RID: 4196 RVA: 0x000139DC File Offset: 0x00011BDC
			public ParticleSystemCollisionQuality quality
			{
				get
				{
					return (ParticleSystemCollisionQuality)ParticleSystem.CollisionModule.GetQuality(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetQuality(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06001066 RID: 4198 RVA: 0x000139FC File Offset: 0x00011BFC
			// (set) Token: 0x06001067 RID: 4199 RVA: 0x00013A0C File Offset: 0x00011C0C
			public float voxelSize
			{
				get
				{
					return ParticleSystem.CollisionModule.GetVoxelSize(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetVoxelSize(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06001068 RID: 4200 RVA: 0x00013A1C File Offset: 0x00011C1C
			// (set) Token: 0x06001069 RID: 4201 RVA: 0x00013A2C File Offset: 0x00011C2C
			public float radiusScale
			{
				get
				{
					return ParticleSystem.CollisionModule.GetRadiusScale(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetRadiusScale(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x0600106A RID: 4202 RVA: 0x00013A3C File Offset: 0x00011C3C
			// (set) Token: 0x0600106B RID: 4203 RVA: 0x00013A4C File Offset: 0x00011C4C
			public bool sendCollisionMessages
			{
				get
				{
					return ParticleSystem.CollisionModule.GetUsesCollisionMessages(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.CollisionModule.SetUsesCollisionMessages(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x0600106C RID: 4204 RVA: 0x00013A5C File Offset: 0x00011C5C
			public void SetPlane(int index, Transform transform)
			{
				ParticleSystem.CollisionModule.SetPlane(this.m_ParticleSystem, index, transform);
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x00013A6C File Offset: 0x00011C6C
			public Transform GetPlane(int index)
			{
				return ParticleSystem.CollisionModule.GetPlane(this.m_ParticleSystem, index);
			}

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x0600106E RID: 4206 RVA: 0x00013A7C File Offset: 0x00011C7C
			public int maxPlaneCount
			{
				get
				{
					return ParticleSystem.CollisionModule.GetMaxPlaneCount(this.m_ParticleSystem);
				}
			}

			// Token: 0x0600106F RID: 4207
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x06001070 RID: 4208
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x06001071 RID: 4209
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetType(ParticleSystem system, int value);

			// Token: 0x06001072 RID: 4210
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetType(ParticleSystem system);

			// Token: 0x06001073 RID: 4211
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMode(ParticleSystem system, int value);

			// Token: 0x06001074 RID: 4212
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetMode(ParticleSystem system);

			// Token: 0x06001075 RID: 4213
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetDampen(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001076 RID: 4214
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetDampen(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001077 RID: 4215
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBounce(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001078 RID: 4216
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBounce(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x06001079 RID: 4217
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnergyLoss(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600107A RID: 4218
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetEnergyLoss(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x0600107B RID: 4219
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMinKillSpeed(ParticleSystem system, float value);

			// Token: 0x0600107C RID: 4220
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetMinKillSpeed(ParticleSystem system);

			// Token: 0x0600107D RID: 4221
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCollidesWith(ParticleSystem system, int value);

			// Token: 0x0600107E RID: 4222
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetCollidesWith(ParticleSystem system);

			// Token: 0x0600107F RID: 4223
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnableDynamicColliders(ParticleSystem system, bool value);

			// Token: 0x06001080 RID: 4224
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnableDynamicColliders(ParticleSystem system);

			// Token: 0x06001081 RID: 4225
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnableInteriorCollisions(ParticleSystem system, bool value);

			// Token: 0x06001082 RID: 4226
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnableInteriorCollisions(ParticleSystem system);

			// Token: 0x06001083 RID: 4227
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMaxCollisionShapes(ParticleSystem system, int value);

			// Token: 0x06001084 RID: 4228
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetMaxCollisionShapes(ParticleSystem system);

			// Token: 0x06001085 RID: 4229
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetQuality(ParticleSystem system, int value);

			// Token: 0x06001086 RID: 4230
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetQuality(ParticleSystem system);

			// Token: 0x06001087 RID: 4231
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVoxelSize(ParticleSystem system, float value);

			// Token: 0x06001088 RID: 4232
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetVoxelSize(ParticleSystem system);

			// Token: 0x06001089 RID: 4233
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetRadiusScale(ParticleSystem system, float value);

			// Token: 0x0600108A RID: 4234
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetRadiusScale(ParticleSystem system);

			// Token: 0x0600108B RID: 4235
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetUsesCollisionMessages(ParticleSystem system, bool value);

			// Token: 0x0600108C RID: 4236
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetUsesCollisionMessages(ParticleSystem system);

			// Token: 0x0600108D RID: 4237
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetPlane(ParticleSystem system, int index, Transform transform);

			// Token: 0x0600108E RID: 4238
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Transform GetPlane(ParticleSystem system, int index);

			// Token: 0x0600108F RID: 4239
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetMaxPlaneCount(ParticleSystem system);

			// Token: 0x04000301 RID: 769
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000107 RID: 263
		public struct SubEmittersModule
		{
			// Token: 0x06001090 RID: 4240 RVA: 0x00013A8C File Offset: 0x00011C8C
			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x06001092 RID: 4242 RVA: 0x00013AA8 File Offset: 0x00011CA8
			// (set) Token: 0x06001091 RID: 4241 RVA: 0x00013A98 File Offset: 0x00011C98
			public bool enabled
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06001093 RID: 4243 RVA: 0x00013AB8 File Offset: 0x00011CB8
			// (set) Token: 0x06001094 RID: 4244 RVA: 0x00013AC8 File Offset: 0x00011CC8
			public ParticleSystem birth0
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetBirth(this.m_ParticleSystem, 0);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetBirth(this.m_ParticleSystem, 0, value);
				}
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x06001095 RID: 4245 RVA: 0x00013AD8 File Offset: 0x00011CD8
			// (set) Token: 0x06001096 RID: 4246 RVA: 0x00013AE8 File Offset: 0x00011CE8
			public ParticleSystem birth1
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetBirth(this.m_ParticleSystem, 1);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetBirth(this.m_ParticleSystem, 1, value);
				}
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x06001097 RID: 4247 RVA: 0x00013AF8 File Offset: 0x00011CF8
			// (set) Token: 0x06001098 RID: 4248 RVA: 0x00013B08 File Offset: 0x00011D08
			public ParticleSystem collision0
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetCollision(this.m_ParticleSystem, 0);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetCollision(this.m_ParticleSystem, 0, value);
				}
			}

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x06001099 RID: 4249 RVA: 0x00013B18 File Offset: 0x00011D18
			// (set) Token: 0x0600109A RID: 4250 RVA: 0x00013B28 File Offset: 0x00011D28
			public ParticleSystem collision1
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetCollision(this.m_ParticleSystem, 1);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetCollision(this.m_ParticleSystem, 1, value);
				}
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x0600109B RID: 4251 RVA: 0x00013B38 File Offset: 0x00011D38
			// (set) Token: 0x0600109C RID: 4252 RVA: 0x00013B48 File Offset: 0x00011D48
			public ParticleSystem death0
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetDeath(this.m_ParticleSystem, 0);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetDeath(this.m_ParticleSystem, 0, value);
				}
			}

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x0600109D RID: 4253 RVA: 0x00013B58 File Offset: 0x00011D58
			// (set) Token: 0x0600109E RID: 4254 RVA: 0x00013B68 File Offset: 0x00011D68
			public ParticleSystem death1
			{
				get
				{
					return ParticleSystem.SubEmittersModule.GetDeath(this.m_ParticleSystem, 1);
				}
				set
				{
					ParticleSystem.SubEmittersModule.SetDeath(this.m_ParticleSystem, 1, value);
				}
			}

			// Token: 0x0600109F RID: 4255
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x060010A0 RID: 4256
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x060010A1 RID: 4257
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBirth(ParticleSystem system, int index, ParticleSystem value);

			// Token: 0x060010A2 RID: 4258
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystem GetBirth(ParticleSystem system, int index);

			// Token: 0x060010A3 RID: 4259
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCollision(ParticleSystem system, int index, ParticleSystem value);

			// Token: 0x060010A4 RID: 4260
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystem GetCollision(ParticleSystem system, int index);

			// Token: 0x060010A5 RID: 4261
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetDeath(ParticleSystem system, int index, ParticleSystem value);

			// Token: 0x060010A6 RID: 4262
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystem GetDeath(ParticleSystem system, int index);

			// Token: 0x04000302 RID: 770
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000108 RID: 264
		public struct TextureSheetAnimationModule
		{
			// Token: 0x060010A7 RID: 4263 RVA: 0x00013B78 File Offset: 0x00011D78
			internal TextureSheetAnimationModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00013B94 File Offset: 0x00011D94
			// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00013B84 File Offset: 0x00011D84
			public bool enabled
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.GetEnabled(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetEnabled(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x060010AB RID: 4267 RVA: 0x00013BB4 File Offset: 0x00011DB4
			// (set) Token: 0x060010AA RID: 4266 RVA: 0x00013BA4 File Offset: 0x00011DA4
			public int numTilesX
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.GetNumTilesX(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetNumTilesX(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x060010AD RID: 4269 RVA: 0x00013BD4 File Offset: 0x00011DD4
			// (set) Token: 0x060010AC RID: 4268 RVA: 0x00013BC4 File Offset: 0x00011DC4
			public int numTilesY
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.GetNumTilesY(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetNumTilesY(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x060010AF RID: 4271 RVA: 0x00013BF4 File Offset: 0x00011DF4
			// (set) Token: 0x060010AE RID: 4270 RVA: 0x00013BE4 File Offset: 0x00011DE4
			public ParticleSystemAnimationType animation
			{
				get
				{
					return (ParticleSystemAnimationType)ParticleSystem.TextureSheetAnimationModule.GetAnimationType(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetAnimationType(this.m_ParticleSystem, (int)value);
				}
			}

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00013C14 File Offset: 0x00011E14
			// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00013C04 File Offset: 0x00011E04
			public bool useRandomRow
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.GetUseRandomRow(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetUseRandomRow(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00013C34 File Offset: 0x00011E34
			// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00013C24 File Offset: 0x00011E24
			public ParticleSystem.MinMaxCurve frameOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve result = default(ParticleSystem.MinMaxCurve);
					ParticleSystem.TextureSheetAnimationModule.GetFrameOverTime(this.m_ParticleSystem, ref result);
					return result;
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetFrameOverTime(this.m_ParticleSystem, ref value);
				}
			}

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00013C68 File Offset: 0x00011E68
			// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00013C58 File Offset: 0x00011E58
			public int cycleCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.GetCycleCount(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetCycleCount(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00013C88 File Offset: 0x00011E88
			// (set) Token: 0x060010B6 RID: 4278 RVA: 0x00013C78 File Offset: 0x00011E78
			public int rowIndex
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.GetRowIndex(this.m_ParticleSystem);
				}
				set
				{
					ParticleSystem.TextureSheetAnimationModule.SetRowIndex(this.m_ParticleSystem, value);
				}
			}

			// Token: 0x060010B8 RID: 4280
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetEnabled(ParticleSystem system, bool value);

			// Token: 0x060010B9 RID: 4281
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetEnabled(ParticleSystem system);

			// Token: 0x060010BA RID: 4282
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetNumTilesX(ParticleSystem system, int value);

			// Token: 0x060010BB RID: 4283
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetNumTilesX(ParticleSystem system);

			// Token: 0x060010BC RID: 4284
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetNumTilesY(ParticleSystem system, int value);

			// Token: 0x060010BD RID: 4285
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetNumTilesY(ParticleSystem system);

			// Token: 0x060010BE RID: 4286
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetAnimationType(ParticleSystem system, int value);

			// Token: 0x060010BF RID: 4287
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetAnimationType(ParticleSystem system);

			// Token: 0x060010C0 RID: 4288
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetUseRandomRow(ParticleSystem system, bool value);

			// Token: 0x060010C1 RID: 4289
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool GetUseRandomRow(ParticleSystem system);

			// Token: 0x060010C2 RID: 4290
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetFrameOverTime(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x060010C3 RID: 4291
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetFrameOverTime(ParticleSystem system, ref ParticleSystem.MinMaxCurve curve);

			// Token: 0x060010C4 RID: 4292
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCycleCount(ParticleSystem system, int value);

			// Token: 0x060010C5 RID: 4293
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetCycleCount(ParticleSystem system);

			// Token: 0x060010C6 RID: 4294
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetRowIndex(ParticleSystem system, int value);

			// Token: 0x060010C7 RID: 4295
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetRowIndex(ParticleSystem system);

			// Token: 0x04000303 RID: 771
			private ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000109 RID: 265
		public struct Particle
		{
			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00013C98 File Offset: 0x00011E98
			// (set) Token: 0x060010C9 RID: 4297 RVA: 0x00013CA0 File Offset: 0x00011EA0
			public Vector3 position
			{
				get
				{
					return this.m_Position;
				}
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x060010CA RID: 4298 RVA: 0x00013CAC File Offset: 0x00011EAC
			// (set) Token: 0x060010CB RID: 4299 RVA: 0x00013CB4 File Offset: 0x00011EB4
			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
				set
				{
					this.m_Velocity = value;
				}
			}

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x060010CC RID: 4300 RVA: 0x00013CC0 File Offset: 0x00011EC0
			// (set) Token: 0x060010CD RID: 4301 RVA: 0x00013CC8 File Offset: 0x00011EC8
			public float lifetime
			{
				get
				{
					return this.m_Lifetime;
				}
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x060010CE RID: 4302 RVA: 0x00013CD4 File Offset: 0x00011ED4
			// (set) Token: 0x060010CF RID: 4303 RVA: 0x00013CDC File Offset: 0x00011EDC
			public float startLifetime
			{
				get
				{
					return this.m_StartLifetime;
				}
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00013CE8 File Offset: 0x00011EE8
			// (set) Token: 0x060010D1 RID: 4305 RVA: 0x00013CF0 File Offset: 0x00011EF0
			public float startSize
			{
				get
				{
					return this.m_StartSize;
				}
				set
				{
					this.m_StartSize = value;
				}
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00013CFC File Offset: 0x00011EFC
			// (set) Token: 0x060010D3 RID: 4307 RVA: 0x00013D04 File Offset: 0x00011F04
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_AxisOfRotation;
				}
				set
				{
					this.m_AxisOfRotation = value;
				}
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00013D10 File Offset: 0x00011F10
			// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00013D24 File Offset: 0x00011F24
			public float rotation
			{
				get
				{
					return this.m_Rotation.z * 57.29578f;
				}
				set
				{
					this.m_Rotation = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00013D44 File Offset: 0x00011F44
			// (set) Token: 0x060010D7 RID: 4311 RVA: 0x00013D58 File Offset: 0x00011F58
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Rotation * 57.29578f;
				}
				set
				{
					this.m_Rotation = value * 0.017453292f;
				}
			}

			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00013D6C File Offset: 0x00011F6C
			// (set) Token: 0x060010D9 RID: 4313 RVA: 0x00013D80 File Offset: 0x00011F80
			public float angularVelocity
			{
				get
				{
					return this.m_AngularVelocity.z * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity.z = value * 0.017453292f;
				}
			}

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x060010DA RID: 4314 RVA: 0x00013D94 File Offset: 0x00011F94
			// (set) Token: 0x060010DB RID: 4315 RVA: 0x00013DA8 File Offset: 0x00011FA8
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_AngularVelocity * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = value * 0.017453292f;
				}
			}

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x060010DC RID: 4316 RVA: 0x00013DBC File Offset: 0x00011FBC
			// (set) Token: 0x060010DD RID: 4317 RVA: 0x00013DC4 File Offset: 0x00011FC4
			public Color32 startColor
			{
				get
				{
					return this.m_StartColor;
				}
				set
				{
					this.m_StartColor = value;
				}
			}

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x060010DE RID: 4318 RVA: 0x00013DD0 File Offset: 0x00011FD0
			// (set) Token: 0x060010DF RID: 4319 RVA: 0x00013DE4 File Offset: 0x00011FE4
			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.")]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(this.m_RandomSeed), 0);
				}
				set
				{
					this.m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00013DF8 File Offset: 0x00011FF8
			// (set) Token: 0x060010E1 RID: 4321 RVA: 0x00013E00 File Offset: 0x00012000
			public uint randomSeed
			{
				get
				{
					return this.m_RandomSeed;
				}
				set
				{
					this.m_RandomSeed = value;
				}
			}

			// Token: 0x060010E2 RID: 4322 RVA: 0x00013E0C File Offset: 0x0001200C
			public float GetCurrentSize(ParticleSystem system)
			{
				return ParticleSystem.Particle.GetCurrentSize(system, ref this);
			}

			// Token: 0x060010E3 RID: 4323 RVA: 0x00013E18 File Offset: 0x00012018
			public Color32 GetCurrentColor(ParticleSystem system)
			{
				return ParticleSystem.Particle.GetCurrentColor(system, ref this);
			}

			// Token: 0x060010E4 RID: 4324
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetCurrentSize(ParticleSystem system, ref ParticleSystem.Particle particle);

			// Token: 0x060010E5 RID: 4325 RVA: 0x00013E24 File Offset: 0x00012024
			private static Color32 GetCurrentColor(ParticleSystem system, ref ParticleSystem.Particle particle)
			{
				Color32 result;
				ParticleSystem.Particle.INTERNAL_CALL_GetCurrentColor(system, ref particle, out result);
				return result;
			}

			// Token: 0x060010E6 RID: 4326
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void INTERNAL_CALL_GetCurrentColor(ParticleSystem system, ref ParticleSystem.Particle particle, out Color32 value);

			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00013E3C File Offset: 0x0001203C
			// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00013E44 File Offset: 0x00012044
			[Obsolete("size property is deprecated. Use startSize or GetCurrentSize() instead.")]
			public float size
			{
				get
				{
					return this.m_StartSize;
				}
				set
				{
					this.m_StartSize = value;
				}
			}

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00013E50 File Offset: 0x00012050
			// (set) Token: 0x060010EA RID: 4330 RVA: 0x00013E58 File Offset: 0x00012058
			[Obsolete("color property is deprecated. Use startColor or GetCurrentColor() instead.")]
			public Color32 color
			{
				get
				{
					return this.m_StartColor;
				}
				set
				{
					this.m_StartColor = value;
				}
			}

			// Token: 0x04000304 RID: 772
			private Vector3 m_Position;

			// Token: 0x04000305 RID: 773
			private Vector3 m_Velocity;

			// Token: 0x04000306 RID: 774
			private Vector3 m_AnimatedVelocity;

			// Token: 0x04000307 RID: 775
			private Vector3 m_InitialVelocity;

			// Token: 0x04000308 RID: 776
			private Vector3 m_AxisOfRotation;

			// Token: 0x04000309 RID: 777
			private Vector3 m_Rotation;

			// Token: 0x0400030A RID: 778
			private Vector3 m_AngularVelocity;

			// Token: 0x0400030B RID: 779
			private float m_StartSize;

			// Token: 0x0400030C RID: 780
			private Color32 m_StartColor;

			// Token: 0x0400030D RID: 781
			private uint m_RandomSeed;

			// Token: 0x0400030E RID: 782
			private float m_Lifetime;

			// Token: 0x0400030F RID: 783
			private float m_StartLifetime;

			// Token: 0x04000310 RID: 784
			private float m_EmitAccumulator0;

			// Token: 0x04000311 RID: 785
			private float m_EmitAccumulator1;
		}

		// Token: 0x0200010A RID: 266
		public struct EmitParams
		{
			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x060010EB RID: 4331 RVA: 0x00013E64 File Offset: 0x00012064
			// (set) Token: 0x060010EC RID: 4332 RVA: 0x00013E74 File Offset: 0x00012074
			public Vector3 position
			{
				get
				{
					return this.m_Particle.position;
				}
				set
				{
					this.m_Particle.position = value;
					this.m_PositionSet = true;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x060010ED RID: 4333 RVA: 0x00013E8C File Offset: 0x0001208C
			// (set) Token: 0x060010EE RID: 4334 RVA: 0x00013E9C File Offset: 0x0001209C
			public Vector3 velocity
			{
				get
				{
					return this.m_Particle.velocity;
				}
				set
				{
					this.m_Particle.velocity = value;
					this.m_VelocitySet = true;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x060010EF RID: 4335 RVA: 0x00013EB4 File Offset: 0x000120B4
			// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00013EC4 File Offset: 0x000120C4
			public float startLifetime
			{
				get
				{
					return this.m_Particle.startLifetime;
				}
				set
				{
					this.m_Particle.startLifetime = value;
					this.m_StartLifetimeSet = true;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00013EDC File Offset: 0x000120DC
			// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00013EEC File Offset: 0x000120EC
			public float startSize
			{
				get
				{
					return this.m_Particle.startSize;
				}
				set
				{
					this.m_Particle.startSize = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00013F04 File Offset: 0x00012104
			// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00013F14 File Offset: 0x00012114
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_Particle.axisOfRotation;
				}
				set
				{
					this.m_Particle.axisOfRotation = value;
					this.m_AxisOfRotationSet = true;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00013F2C File Offset: 0x0001212C
			// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00013F3C File Offset: 0x0001213C
			public float rotation
			{
				get
				{
					return this.m_Particle.rotation;
				}
				set
				{
					this.m_Particle.rotation = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00013F54 File Offset: 0x00012154
			// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00013F64 File Offset: 0x00012164
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Particle.rotation3D;
				}
				set
				{
					this.m_Particle.rotation3D = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00013F7C File Offset: 0x0001217C
			// (set) Token: 0x060010FA RID: 4346 RVA: 0x00013F8C File Offset: 0x0001218C
			public float angularVelocity
			{
				get
				{
					return this.m_Particle.angularVelocity;
				}
				set
				{
					this.m_Particle.angularVelocity = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x060010FB RID: 4347 RVA: 0x00013FA4 File Offset: 0x000121A4
			// (set) Token: 0x060010FC RID: 4348 RVA: 0x00013FB4 File Offset: 0x000121B4
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_Particle.angularVelocity3D;
				}
				set
				{
					this.m_Particle.angularVelocity3D = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x060010FD RID: 4349 RVA: 0x00013FCC File Offset: 0x000121CC
			// (set) Token: 0x060010FE RID: 4350 RVA: 0x00013FDC File Offset: 0x000121DC
			public Color32 startColor
			{
				get
				{
					return this.m_Particle.startColor;
				}
				set
				{
					this.m_Particle.startColor = value;
					this.m_StartColorSet = true;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x060010FF RID: 4351 RVA: 0x00013FF4 File Offset: 0x000121F4
			// (set) Token: 0x06001100 RID: 4352 RVA: 0x00014004 File Offset: 0x00012204
			public uint randomSeed
			{
				get
				{
					return this.m_Particle.randomSeed;
				}
				set
				{
					this.m_Particle.randomSeed = value;
					this.m_RandomSeedSet = true;
				}
			}

			// Token: 0x06001101 RID: 4353 RVA: 0x0001401C File Offset: 0x0001221C
			public void ResetPosition()
			{
				this.m_PositionSet = false;
			}

			// Token: 0x06001102 RID: 4354 RVA: 0x00014028 File Offset: 0x00012228
			public void ResetVelocity()
			{
				this.m_VelocitySet = false;
			}

			// Token: 0x06001103 RID: 4355 RVA: 0x00014034 File Offset: 0x00012234
			public void ResetAxisOfRotation()
			{
				this.m_AxisOfRotationSet = false;
			}

			// Token: 0x06001104 RID: 4356 RVA: 0x00014040 File Offset: 0x00012240
			public void ResetRotation()
			{
				this.m_RotationSet = false;
			}

			// Token: 0x06001105 RID: 4357 RVA: 0x0001404C File Offset: 0x0001224C
			public void ResetAngularVelocity()
			{
				this.m_AngularVelocitySet = false;
			}

			// Token: 0x06001106 RID: 4358 RVA: 0x00014058 File Offset: 0x00012258
			public void ResetStartSize()
			{
				this.m_StartSizeSet = false;
			}

			// Token: 0x06001107 RID: 4359 RVA: 0x00014064 File Offset: 0x00012264
			public void ResetStartColor()
			{
				this.m_StartColorSet = false;
			}

			// Token: 0x06001108 RID: 4360 RVA: 0x00014070 File Offset: 0x00012270
			public void ResetRandomSeed()
			{
				this.m_RandomSeedSet = false;
			}

			// Token: 0x06001109 RID: 4361 RVA: 0x0001407C File Offset: 0x0001227C
			public void ResetStartLifetime()
			{
				this.m_StartLifetimeSet = false;
			}

			// Token: 0x04000312 RID: 786
			internal ParticleSystem.Particle m_Particle;

			// Token: 0x04000313 RID: 787
			internal bool m_PositionSet;

			// Token: 0x04000314 RID: 788
			internal bool m_VelocitySet;

			// Token: 0x04000315 RID: 789
			internal bool m_AxisOfRotationSet;

			// Token: 0x04000316 RID: 790
			internal bool m_RotationSet;

			// Token: 0x04000317 RID: 791
			internal bool m_AngularVelocitySet;

			// Token: 0x04000318 RID: 792
			internal bool m_StartSizeSet;

			// Token: 0x04000319 RID: 793
			internal bool m_StartColorSet;

			// Token: 0x0400031A RID: 794
			internal bool m_RandomSeedSet;

			// Token: 0x0400031B RID: 795
			internal bool m_StartLifetimeSet;
		}

		// Token: 0x02000344 RID: 836
		// (Invoke) Token: 0x0600286A RID: 10346
		internal delegate bool IteratorDelegate(ParticleSystem ps);
	}
}
