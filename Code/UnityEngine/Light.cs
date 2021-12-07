using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000C7 RID: 199
	public sealed class Light : Behaviour
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BFF RID: 3071
		// (set) Token: 0x06000C00 RID: 3072
		public extern LightType type { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0000F908 File Offset: 0x0000DB08
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0000F920 File Offset: 0x0000DB20
		public Color color
		{
			get
			{
				Color result;
				this.INTERNAL_get_color(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x06000C03 RID: 3075
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06000C04 RID: 3076
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C05 RID: 3077
		// (set) Token: 0x06000C06 RID: 3078
		public extern float intensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C07 RID: 3079
		// (set) Token: 0x06000C08 RID: 3080
		public extern float bounceIntensity { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C09 RID: 3081
		// (set) Token: 0x06000C0A RID: 3082
		public extern LightShadows shadows { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C0B RID: 3083
		// (set) Token: 0x06000C0C RID: 3084
		public extern float shadowStrength { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C0D RID: 3085
		// (set) Token: 0x06000C0E RID: 3086
		public extern float shadowBias { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C0F RID: 3087
		// (set) Token: 0x06000C10 RID: 3088
		public extern float shadowNormalBias { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C11 RID: 3089
		// (set) Token: 0x06000C12 RID: 3090
		public extern float shadowNearPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C13 RID: 3091
		// (set) Token: 0x06000C14 RID: 3092
		[Obsolete("Shadow softness is removed in Unity 5.0+")]
		public extern float shadowSoftness { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C15 RID: 3093
		// (set) Token: 0x06000C16 RID: 3094
		[Obsolete("Shadow softness is removed in Unity 5.0+")]
		public extern float shadowSoftnessFade { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C17 RID: 3095
		// (set) Token: 0x06000C18 RID: 3096
		public extern float range { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C19 RID: 3097
		// (set) Token: 0x06000C1A RID: 3098
		public extern float spotAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C1B RID: 3099
		// (set) Token: 0x06000C1C RID: 3100
		public extern float cookieSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C1D RID: 3101
		// (set) Token: 0x06000C1E RID: 3102
		public extern Texture cookie { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C1F RID: 3103
		// (set) Token: 0x06000C20 RID: 3104
		public extern Flare flare { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C21 RID: 3105
		// (set) Token: 0x06000C22 RID: 3106
		public extern LightRenderMode renderMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C23 RID: 3107
		// (set) Token: 0x06000C24 RID: 3108
		public extern bool alreadyLightmapped { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C25 RID: 3109
		// (set) Token: 0x06000C26 RID: 3110
		public extern int cullingMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000C27 RID: 3111
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddCommandBuffer(LightEvent evt, CommandBuffer buffer);

		// Token: 0x06000C28 RID: 3112
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer);

		// Token: 0x06000C29 RID: 3113
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(LightEvent evt);

		// Token: 0x06000C2A RID: 3114
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		// Token: 0x06000C2B RID: 3115
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CommandBuffer[] GetCommandBuffers(LightEvent evt);

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C2C RID: 3116
		public extern int commandBufferCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C2D RID: 3117
		// (set) Token: 0x06000C2E RID: 3118
		public static extern int pixelLightCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000C2F RID: 3119
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Light[] GetLights(LightType type, int layer);

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0000F92C File Offset: 0x0000DB2C
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0000F934 File Offset: 0x0000DB34
		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0000F938 File Offset: 0x0000DB38
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x0000F940 File Offset: 0x0000DB40
		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0000F944 File Offset: 0x0000DB44
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0000F948 File Offset: 0x0000DB48
		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
	}
}
