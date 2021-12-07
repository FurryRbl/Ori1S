using System;

namespace UnityEngine
{
	// Token: 0x020000C4 RID: 196
	[Flags]
	public enum HideFlags
	{
		// Token: 0x0400025A RID: 602
		None = 0,
		// Token: 0x0400025B RID: 603
		HideInHierarchy = 1,
		// Token: 0x0400025C RID: 604
		HideInInspector = 2,
		// Token: 0x0400025D RID: 605
		DontSaveInEditor = 4,
		// Token: 0x0400025E RID: 606
		NotEditable = 8,
		// Token: 0x0400025F RID: 607
		DontSaveInBuild = 16,
		// Token: 0x04000260 RID: 608
		DontUnloadUnusedAsset = 32,
		// Token: 0x04000261 RID: 609
		DontSave = 52,
		// Token: 0x04000262 RID: 610
		HideAndDontSave = 61
	}
}
