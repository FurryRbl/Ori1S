using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	public sealed class LightProbes : Object
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00003B58 File Offset: 0x00001D58
		public static void GetInterpolatedProbe(Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe)
		{
			LightProbes.INTERNAL_CALL_GetInterpolatedProbe(ref position, renderer, out probe);
		}

		// Token: 0x060002B3 RID: 691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetInterpolatedProbe(ref Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe);

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002B4 RID: 692
		public extern Vector3[] positions { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002B5 RID: 693
		// (set) Token: 0x060002B6 RID: 694
		public extern SphericalHarmonicsL2[] bakedProbes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002B7 RID: 695
		public extern int count { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B8 RID: 696
		public extern int cellCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060002B9 RID: 697 RVA: 0x00003B64 File Offset: 0x00001D64
		[Obsolete("GetInterpolatedLightProbe has been deprecated. Please use the static GetInterpolatedProbe instead.", true)]
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00003B68 File Offset: 0x00001D68
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00003B70 File Offset: 0x00001D70
		[Obsolete("coefficients property has been deprecated. Please use bakedProbes instead.", true)]
		public float[] coefficients
		{
			get
			{
				return new float[0];
			}
			set
			{
			}
		}
	}
}
