using System;

// Token: 0x0200004B RID: 75
[Serializable]
public class AnimationMeshingSettings
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000337 RID: 823 RVA: 0x0000D69B File Offset: 0x0000B89B
	public static AnimationMeshingSettings Default
	{
		get
		{
			return new AnimationMeshingSettings();
		}
	}

	// Token: 0x04000259 RID: 601
	public int XDivisions = 1;

	// Token: 0x0400025A RID: 602
	public int YDivisions = 1;
}
