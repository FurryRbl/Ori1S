using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200004E RID: 78
	[SelectionBase]
	public sealed class LODGroup : Component
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00004738 File Offset: 0x00002938
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00004750 File Offset: 0x00002950
		public Vector3 localReferencePoint
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localReferencePoint(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localReferencePoint(ref value);
			}
		}

		// Token: 0x0600044A RID: 1098
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localReferencePoint(out Vector3 value);

		// Token: 0x0600044B RID: 1099
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localReferencePoint(ref Vector3 value);

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600044C RID: 1100
		// (set) Token: 0x0600044D RID: 1101
		public extern float size { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600044E RID: 1102
		public extern int lodCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600044F RID: 1103
		// (set) Token: 0x06000450 RID: 1104
		public extern LODFadeMode fadeMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000451 RID: 1105
		// (set) Token: 0x06000452 RID: 1106
		public extern bool animateCrossFading { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000453 RID: 1107
		// (set) Token: 0x06000454 RID: 1108
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000455 RID: 1109
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		// Token: 0x06000456 RID: 1110
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern LOD[] GetLODs();

		// Token: 0x06000457 RID: 1111 RVA: 0x0000475C File Offset: 0x0000295C
		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			this.SetLODs(lods);
		}

		// Token: 0x06000458 RID: 1112
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLODs(LOD[] lods);

		// Token: 0x06000459 RID: 1113
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceLOD(int index);

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600045A RID: 1114
		// (set) Token: 0x0600045B RID: 1115
		public static extern float crossFadeAnimationDuration { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
