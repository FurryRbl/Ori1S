using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000175 RID: 373
	[StructLayout(LayoutKind.Sequential)]
	public sealed class NavMeshPath
	{
		// Token: 0x060017DF RID: 6111
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern NavMeshPath();

		// Token: 0x060017E0 RID: 6112
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DestroyNavMeshPath();

		// Token: 0x060017E1 RID: 6113 RVA: 0x00018378 File Offset: 0x00016578
		~NavMeshPath()
		{
			this.DestroyNavMeshPath();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x060017E2 RID: 6114
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetCornersNonAlloc(Vector3[] results);

		// Token: 0x060017E3 RID: 6115
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector3[] CalculateCornersInternal();

		// Token: 0x060017E4 RID: 6116
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearCornersInternal();

		// Token: 0x060017E5 RID: 6117 RVA: 0x000183C0 File Offset: 0x000165C0
		public void ClearCorners()
		{
			this.ClearCornersInternal();
			this.m_corners = null;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000183D0 File Offset: 0x000165D0
		private void CalculateCorners()
		{
			if (this.m_corners == null)
			{
				this.m_corners = this.CalculateCornersInternal();
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000183EC File Offset: 0x000165EC
		public Vector3[] corners
		{
			get
			{
				this.CalculateCorners();
				return this.m_corners;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060017E8 RID: 6120
		public extern NavMeshPathStatus status { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0400040B RID: 1035
		internal IntPtr m_Ptr;

		// Token: 0x0400040C RID: 1036
		internal Vector3[] m_corners;
	}
}
