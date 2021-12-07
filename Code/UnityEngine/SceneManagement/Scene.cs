using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020000DF RID: 223
	public struct Scene
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00012348 File Offset: 0x00010548
		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00012350 File Offset: 0x00010550
		public bool IsValid()
		{
			return Scene.IsValidInternal(this.handle);
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00012360 File Offset: 0x00010560
		public string path
		{
			get
			{
				return Scene.GetPathInternal(this.handle);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00012370 File Offset: 0x00010570
		public string name
		{
			get
			{
				return Scene.GetNameInternal(this.handle);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00012380 File Offset: 0x00010580
		public bool isLoaded
		{
			get
			{
				return Scene.GetIsLoadedInternal(this.handle);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00012390 File Offset: 0x00010590
		public int buildIndex
		{
			get
			{
				return Scene.GetBuildIndexInternal(this.handle);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x000123A0 File Offset: 0x000105A0
		public bool isDirty
		{
			get
			{
				return Scene.GetIsDirtyInternal(this.handle);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000123B0 File Offset: 0x000105B0
		public int rootCount
		{
			get
			{
				return Scene.GetRootCountInternal(this.handle);
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000123C0 File Offset: 0x000105C0
		public GameObject[] GetRootGameObjects()
		{
			List<GameObject> list = new List<GameObject>(this.rootCount);
			this.GetRootGameObjects(list);
			return list.ToArray();
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000123E8 File Offset: 0x000105E8
		public void GetRootGameObjects(List<GameObject> rootGameObjects)
		{
			if (rootGameObjects.Capacity < this.rootCount)
			{
				rootGameObjects.Capacity = this.rootCount;
			}
			rootGameObjects.Clear();
			if (!this.IsValid())
			{
				throw new ArgumentException("The scene is invalid.");
			}
			if (!this.isLoaded)
			{
				throw new ArgumentException("The scene is not loaded.");
			}
			if (this.rootCount == 0)
			{
				return;
			}
			Scene.GetRootGameObjectsInternal(this.handle, rootGameObjects);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0001245C File Offset: 0x0001065C
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00012464 File Offset: 0x00010664
		public override bool Equals(object other)
		{
			if (!(other is Scene))
			{
				return false;
			}
			Scene scene = (Scene)other;
			return this.handle == scene.handle;
		}

		// Token: 0x06000E78 RID: 3704
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidInternal(int sceneHandle);

		// Token: 0x06000E79 RID: 3705
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetPathInternal(int sceneHandle);

		// Token: 0x06000E7A RID: 3706
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetNameInternal(int sceneHandle);

		// Token: 0x06000E7B RID: 3707
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsLoadedInternal(int sceneHandle);

		// Token: 0x06000E7C RID: 3708
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsDirtyInternal(int sceneHandle);

		// Token: 0x06000E7D RID: 3709
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBuildIndexInternal(int sceneHandle);

		// Token: 0x06000E7E RID: 3710
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRootCountInternal(int sceneHandle);

		// Token: 0x06000E7F RID: 3711
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootGameObjectsInternal(int sceneHandle, object resultRootList);

		// Token: 0x06000E80 RID: 3712 RVA: 0x00012494 File Offset: 0x00010694
		public static bool operator ==(Scene lhs, Scene rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000124A8 File Offset: 0x000106A8
		public static bool operator !=(Scene lhs, Scene rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x04000289 RID: 649
		private int m_Handle;
	}
}
