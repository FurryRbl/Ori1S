using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200072B RID: 1835
[ExecuteInEditMode]
public class SceneScreenshotManager : MonoBehaviour
{
	// Token: 0x040026DE RID: 9950
	private const string ShowScreenshotsString = "Screenshot.ShowScreenshots";

	// Token: 0x040026DF RID: 9951
	public static bool ShouldDesaturateScreenshots;

	// Token: 0x040026E0 RID: 9952
	public static SceneScreenshotManager Instance;

	// Token: 0x040026E1 RID: 9953
	public static bool FilterBuildSettingsScenes;

	// Token: 0x040026E2 RID: 9954
	public static bool FilterScenes;

	// Token: 0x040026E3 RID: 9955
	public static List<string> AreaSceneFilter = new List<string>();

	// Token: 0x040026E4 RID: 9956
	public static Dictionary<string, bool> Areas = new Dictionary<string, bool>();
}
