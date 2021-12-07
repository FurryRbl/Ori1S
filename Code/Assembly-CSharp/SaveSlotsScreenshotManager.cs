using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000703 RID: 1795
public class SaveSlotsScreenshotManager : MonoBehaviour
{
	// Token: 0x06002AA8 RID: 10920 RVA: 0x000B6DD4 File Offset: 0x000B4FD4
	public void Awake()
	{
		SaveSlotsScreenshotManager.Instance = this;
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x000B6DDC File Offset: 0x000B4FDC
	public void OnDestroy()
	{
		if (SaveSlotsScreenshotManager.Instance == this)
		{
			SaveSlotsScreenshotManager.Instance = null;
		}
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x000B6DF4 File Offset: 0x000B4FF4
	public Texture FindScreenshot(string areaName)
	{
		foreach (SaveSlotsScreenshotManager.ScreenshotPair screenshotPair in this.Screenshots)
		{
			if (screenshotPair.Area.AreaIdentifier == areaName)
			{
				return screenshotPair.Texture;
			}
		}
		return null;
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x000B6E6C File Offset: 0x000B506C
	public string FindAreaName(string areaName)
	{
		foreach (SaveSlotsScreenshotManager.ScreenshotPair screenshotPair in this.Screenshots)
		{
			if (screenshotPair.Area.AreaIdentifier == areaName)
			{
				return screenshotPair.Area.LowerAreaName.GetMessages().First<MessageDescriptor>().Message;
			}
		}
		return string.Empty;
	}

	// Token: 0x040025F8 RID: 9720
	public static SaveSlotsScreenshotManager Instance;

	// Token: 0x040025F9 RID: 9721
	public List<SaveSlotsScreenshotManager.ScreenshotPair> Screenshots = new List<SaveSlotsScreenshotManager.ScreenshotPair>();

	// Token: 0x0200070C RID: 1804
	[Serializable]
	public class ScreenshotPair
	{
		// Token: 0x04002649 RID: 9801
		public Texture Texture;

		// Token: 0x0400264A RID: 9802
		public GameWorldArea Area;
	}
}
