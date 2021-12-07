using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000173 RID: 371
	public sealed class NavMeshObstacle : Behaviour
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060017C4 RID: 6084
		// (set) Token: 0x060017C5 RID: 6085
		public extern float height { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060017C6 RID: 6086
		// (set) Token: 0x060017C7 RID: 6087
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0001830C File Offset: 0x0001650C
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x00018324 File Offset: 0x00016524
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x060017CA RID: 6090
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x060017CB RID: 6091
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060017CC RID: 6092
		// (set) Token: 0x060017CD RID: 6093
		public extern bool carving { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060017CE RID: 6094
		// (set) Token: 0x060017CF RID: 6095
		public extern bool carveOnlyStationary { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060017D0 RID: 6096
		// (set) Token: 0x060017D1 RID: 6097
		public extern float carvingMoveThreshold { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060017D2 RID: 6098
		// (set) Token: 0x060017D3 RID: 6099
		public extern float carvingTimeToStationary { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060017D4 RID: 6100
		// (set) Token: 0x060017D5 RID: 6101
		public extern NavMeshObstacleShape shape { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00018330 File Offset: 0x00016530
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x00018348 File Offset: 0x00016548
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_center(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x060017D8 RID: 6104
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x060017D9 RID: 6105
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00018354 File Offset: 0x00016554
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0001836C File Offset: 0x0001656C
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_size(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x060017DC RID: 6108
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x060017DD RID: 6109
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x060017DE RID: 6110
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void FitExtents();
	}
}
