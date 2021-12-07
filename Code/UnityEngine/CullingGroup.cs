using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000052 RID: 82
	[StructLayout(LayoutKind.Sequential)]
	public sealed class CullingGroup : IDisposable
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x00004824 File Offset: 0x00002A24
		public CullingGroup()
		{
			this.Init();
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00004834 File Offset: 0x00002A34
		~CullingGroup()
		{
			if (this.m_Ptr != IntPtr.Zero)
			{
				this.FinalizerFailure();
			}
		}

		// Token: 0x06000467 RID: 1127
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00004884 File Offset: 0x00002A84
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000488C File Offset: 0x00002A8C
		public CullingGroup.StateChanged onStateChanged
		{
			get
			{
				return this.m_OnStateChanged;
			}
			set
			{
				this.m_OnStateChanged = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600046A RID: 1130
		// (set) Token: 0x0600046B RID: 1131
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600046C RID: 1132
		// (set) Token: 0x0600046D RID: 1133
		public extern Camera targetCamera { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600046E RID: 1134
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingSpheres(BoundingSphere[] array);

		// Token: 0x0600046F RID: 1135
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingSphereCount(int count);

		// Token: 0x06000470 RID: 1136
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EraseSwapBack(int index);

		// Token: 0x06000471 RID: 1137 RVA: 0x00004898 File Offset: 0x00002A98
		public static void EraseSwapBack<T>(int index, T[] myArray, ref int size)
		{
			size--;
			myArray[index] = myArray[size];
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000048B0 File Offset: 0x00002AB0
		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000048C0 File Offset: 0x00002AC0
		public int QueryIndices(int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(false, distanceIndex, CullingQueryOptions.IgnoreVisibility, result, firstIndex);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000048D0 File Offset: 0x00002AD0
		public int QueryIndices(bool visible, int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, distanceIndex, CullingQueryOptions.Normal, result, firstIndex);
		}

		// Token: 0x06000475 RID: 1141
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex);

		// Token: 0x06000476 RID: 1142
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsVisible(int index);

		// Token: 0x06000477 RID: 1143
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDistance(int index);

		// Token: 0x06000478 RID: 1144
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingDistances(float[] distances);

		// Token: 0x06000479 RID: 1145 RVA: 0x000048E0 File Offset: 0x00002AE0
		public void SetDistanceReferencePoint(Vector3 point)
		{
			CullingGroup.INTERNAL_CALL_SetDistanceReferencePoint(this, ref point);
		}

		// Token: 0x0600047A RID: 1146
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetDistanceReferencePoint(CullingGroup self, ref Vector3 point);

		// Token: 0x0600047B RID: 1147
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDistanceReferencePoint(Transform transform);

		// Token: 0x0600047C RID: 1148 RVA: 0x000048EC File Offset: 0x00002AEC
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* ptr = (CullingGroupEvent*)eventsPtr.ToPointer();
			if (cullingGroup.m_OnStateChanged == null)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				cullingGroup.m_OnStateChanged(ptr[i]);
			}
		}

		// Token: 0x0600047D RID: 1149
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x0600047E RID: 1150
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FinalizerFailure();

		// Token: 0x040000C2 RID: 194
		internal IntPtr m_Ptr;

		// Token: 0x040000C3 RID: 195
		private CullingGroup.StateChanged m_OnStateChanged;

		// Token: 0x0200033E RID: 830
		// (Invoke) Token: 0x06002852 RID: 10322
		public delegate void StateChanged(CullingGroupEvent sphere);
	}
}
