using System;
using System.Collections.Generic;

// Token: 0x020004A8 RID: 1192
[Serializable]
public class GoToSequenceData
{
	// Token: 0x04001BA5 RID: 7077
	public string SequenceName = string.Empty;

	// Token: 0x04001BA6 RID: 7078
	public string HelpText = string.Empty;

	// Token: 0x04001BA7 RID: 7079
	public SceneMetaData Scene;

	// Token: 0x04001BA8 RID: 7080
	public List<string> TriggerStrings = new List<string>();

	// Token: 0x04001BA9 RID: 7081
	public string SceneName = string.Empty;
}
