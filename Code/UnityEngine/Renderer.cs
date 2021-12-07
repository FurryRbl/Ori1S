using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	public class Renderer : Component
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D0 RID: 464
		// (set) Token: 0x060001D1 RID: 465
		internal extern Transform staticBatchRootTransform { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D2 RID: 466
		internal extern int staticBatchIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060001D3 RID: 467
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetSubsetIndex(int index, int subSetIndexForMaterial);

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D4 RID: 468
		public extern bool isPartOfStaticBatch { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00002FA8 File Offset: 0x000011A8
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_worldToLocalMatrix(out result);
				return result;
			}
		}

		// Token: 0x060001D6 RID: 470
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldToLocalMatrix(out Matrix4x4 value);

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00002FC0 File Offset: 0x000011C0
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_localToWorldMatrix(out result);
				return result;
			}
		}

		// Token: 0x060001D8 RID: 472
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localToWorldMatrix(out Matrix4x4 value);

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D9 RID: 473
		// (set) Token: 0x060001DA RID: 474
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001DB RID: 475
		// (set) Token: 0x060001DC RID: 476
		public extern ShadowCastingMode shadowCastingMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001DD RID: 477
		// (set) Token: 0x060001DE RID: 478
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Property castShadows has been deprecated. Use shadowCastingMode instead.")]
		public extern bool castShadows { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001DF RID: 479
		// (set) Token: 0x060001E0 RID: 480
		public extern bool receiveShadows { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E1 RID: 481
		// (set) Token: 0x060001E2 RID: 482
		public extern Material material { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E3 RID: 483
		// (set) Token: 0x060001E4 RID: 484
		public extern Material sharedMaterial { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E5 RID: 485
		// (set) Token: 0x060001E6 RID: 486
		public extern Material[] materials { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001E7 RID: 487
		// (set) Token: 0x060001E8 RID: 488
		public extern Material[] sharedMaterials { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00002FD8 File Offset: 0x000011D8
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x060001EA RID: 490
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EB RID: 491
		// (set) Token: 0x060001EC RID: 492
		public extern int lightmapIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001ED RID: 493
		// (set) Token: 0x060001EE RID: 494
		public extern int realtimeLightmapIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00003008 File Offset: 0x00001208
		public Vector4 lightmapScaleOffset
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_lightmapScaleOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_lightmapScaleOffset(ref value);
			}
		}

		// Token: 0x060001F1 RID: 497
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lightmapScaleOffset(out Vector4 value);

		// Token: 0x060001F2 RID: 498
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lightmapScaleOffset(ref Vector4 value);

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00003014 File Offset: 0x00001214
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000302C File Offset: 0x0000122C
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_realtimeLightmapScaleOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_realtimeLightmapScaleOffset(ref value);
			}
		}

		// Token: 0x060001F5 RID: 501
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_realtimeLightmapScaleOffset(out Vector4 value);

		// Token: 0x060001F6 RID: 502
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_realtimeLightmapScaleOffset(ref Vector4 value);

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F7 RID: 503
		public extern bool isVisible { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001F8 RID: 504
		// (set) Token: 0x060001F9 RID: 505
		public extern bool useLightProbes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FA RID: 506
		// (set) Token: 0x060001FB RID: 507
		public extern Transform probeAnchor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001FC RID: 508
		// (set) Token: 0x060001FD RID: 509
		public extern ReflectionProbeUsage reflectionProbeUsage { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060001FE RID: 510
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPropertyBlock(MaterialPropertyBlock properties);

		// Token: 0x060001FF RID: 511
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetPropertyBlock(MaterialPropertyBlock dest);

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000200 RID: 512
		// (set) Token: 0x06000201 RID: 513
		public extern string sortingLayerName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000202 RID: 514
		// (set) Token: 0x06000203 RID: 515
		public extern int sortingLayerID { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000204 RID: 516
		// (set) Token: 0x06000205 RID: 517
		public extern int sortingOrder { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000206 RID: 518
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetClosestReflectionProbesInternal(object result);

		// Token: 0x06000207 RID: 519 RVA: 0x00003038 File Offset: 0x00001238
		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			this.GetClosestReflectionProbesInternal(result);
		}
	}
}
