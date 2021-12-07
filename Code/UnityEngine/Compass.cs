using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C2 RID: 194
	public sealed class Compass
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B60 RID: 2912
		public extern float magneticHeading { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B61 RID: 2913
		public extern float trueHeading { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B62 RID: 2914
		public extern float headingAccuracy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public Vector3 rawVector
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_rawVector(out result);
				return result;
			}
		}

		// Token: 0x06000B64 RID: 2916
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rawVector(out Vector3 value);

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B65 RID: 2917
		public extern double timestamp { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B66 RID: 2918
		// (set) Token: 0x06000B67 RID: 2919
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
