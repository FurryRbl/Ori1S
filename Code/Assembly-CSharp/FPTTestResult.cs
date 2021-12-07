using System;
using System.Collections.Generic;

// Token: 0x0200074D RID: 1869
public class FPTTestResult
{
	// Token: 0x04002782 RID: 10114
	public string SceneName = string.Empty;

	// Token: 0x04002783 RID: 10115
	public float SceneLoadTime = -1f;

	// Token: 0x04002784 RID: 10116
	public float SceneUnloadTime = -1f;

	// Token: 0x04002785 RID: 10117
	public DateTime DateTime = DateTime.Now;

	// Token: 0x04002786 RID: 10118
	public int ActiveScenes;

	// Token: 0x04002787 RID: 10119
	public int LoadedScenes;

	// Token: 0x04002788 RID: 10120
	public List<FPSSampleData> SampleList = new List<FPSSampleData>();
}
