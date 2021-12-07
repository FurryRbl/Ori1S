using System;

namespace UnityEngine.UI
{
	// Token: 0x02000062 RID: 98
	internal static class Misc
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00010178 File Offset: 0x0000E378
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						GameObject gameObject = obj as GameObject;
						gameObject.transform.parent = null;
					}
					Object.Destroy(obj);
				}
				else
				{
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000101CC File Offset: 0x0000E3CC
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
				}
				else
				{
					Object.Destroy(obj);
				}
			}
		}
	}
}
