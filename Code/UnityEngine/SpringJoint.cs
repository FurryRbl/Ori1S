using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200012F RID: 303
	public sealed class SpringJoint : Joint
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600136A RID: 4970
		// (set) Token: 0x0600136B RID: 4971
		public extern float spring { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600136C RID: 4972
		// (set) Token: 0x0600136D RID: 4973
		public extern float damper { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600136E RID: 4974
		// (set) Token: 0x0600136F RID: 4975
		public extern float minDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001370 RID: 4976
		// (set) Token: 0x06001371 RID: 4977
		public extern float maxDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001372 RID: 4978
		// (set) Token: 0x06001373 RID: 4979
		public extern float tolerance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
