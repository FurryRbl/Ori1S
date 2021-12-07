using System;

namespace Game
{
	// Token: 0x020003D1 RID: 977
	public static class ScrollLocks
	{
		// Token: 0x06001AD9 RID: 6873 RVA: 0x00073598 File Offset: 0x00071798
		public static void Register(CameraScrollLock cameraScrollLock)
		{
			ScrollLocks.All.Add(cameraScrollLock);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000735A5 File Offset: 0x000717A5
		public static void Unregister(CameraScrollLock cameraScrollLock)
		{
			ScrollLocks.All.Remove(cameraScrollLock);
		}

		// Token: 0x0400175B RID: 5979
		public static AllContainer<CameraScrollLock> All = new AllContainer<CameraScrollLock>();
	}
}
