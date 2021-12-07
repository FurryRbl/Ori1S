using System;
using UnityEngine;

// Token: 0x0200086E RID: 2158
public class VideoDescriptor : ScriptableObject
{
	// Token: 0x060030C2 RID: 12482 RVA: 0x000CF54B File Offset: 0x000CD74B
	public string GetFullVideoOGVPath()
	{
		return this.GetVideoPath() + ".ogv";
	}

	// Token: 0x060030C3 RID: 12483 RVA: 0x000CF55D File Offset: 0x000CD75D
	public string GetEditorAssetVideoOGVPath()
	{
		return "Assets\\video\\videoAssets\\" + this.RelativeVideoPath + ".ogv";
	}

	// Token: 0x060030C4 RID: 12484 RVA: 0x000CF574 File Offset: 0x000CD774
	public string GetVideoWMVPath()
	{
		return this.GetVideoPath() + ".wmv";
	}

	// Token: 0x060030C5 RID: 12485 RVA: 0x000CF586 File Offset: 0x000CD786
	public static string GetStreamingFolderName()
	{
		return "\\StreamingAssets";
	}

	// Token: 0x060030C6 RID: 12486 RVA: 0x000CF590 File Offset: 0x000CD790
	public string GetVideoPath()
	{
		string dataPath = Application.dataPath;
		return dataPath + this.GetStreamingVideoPath();
	}

	// Token: 0x060030C7 RID: 12487 RVA: 0x000CF5B4 File Offset: 0x000CD7B4
	public string GetStreamingVideoPath()
	{
		string str = VideoDescriptor.GetStreamingFolderName();
		str += "\\";
		return str + this.RelativeVideoPath;
	}

	// Token: 0x04002C0A RID: 11274
	private const string m_streamingFolderNameXbox = "\\Raw";

	// Token: 0x04002C0B RID: 11275
	private const string m_streamingFolderNameWinStandalone = "\\StreamingAssets";

	// Token: 0x04002C0C RID: 11276
	[HideInInspector]
	public MovieTexture MovieTexture;

	// Token: 0x04002C0D RID: 11277
	public string RelativeVideoPath = string.Empty;
}
