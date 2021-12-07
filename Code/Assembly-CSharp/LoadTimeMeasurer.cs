using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class LoadTimeMeasurer : MonoBehaviour
{
	// Token: 0x06001FE4 RID: 8164 RVA: 0x0008BE74 File Offset: 0x0008A074
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		TimeMeasurerHelper.TakeTimestamp();
		if (this.TestSceneAtIndexOne)
		{
			this.asyncOp = Application.LoadLevelAsync(1);
		}
		else
		{
			this.asyncOp = Application.LoadLevelAsync(this.TestSceneName);
		}
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x0008BEBC File Offset: 0x0008A0BC
	private void Update()
	{
		if (this.asyncOp == null)
		{
			return;
		}
		if (this.asyncOp.progress == 1f)
		{
			LoadTimeMeasurer.loadTime = TimeMeasurerHelper.GetTimePast();
			this.asyncOp = null;
		}
	}

	// Token: 0x04001B7E RID: 7038
	public bool TestSceneAtIndexOne;

	// Token: 0x04001B7F RID: 7039
	public string TestSceneName;

	// Token: 0x04001B80 RID: 7040
	private AsyncOperation asyncOp;

	// Token: 0x04001B81 RID: 7041
	public static double loadTime;
}
