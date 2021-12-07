using System;

// Token: 0x020004EB RID: 1259
public class AssetUtility
{
	// Token: 0x06002209 RID: 8713 RVA: 0x00094B44 File Offset: 0x00092D44
	public static string GetAssetName(string assetPath)
	{
		if (assetPath.Contains("/"))
		{
			return assetPath.Substring(assetPath.LastIndexOf("/") + 1, assetPath.LastIndexOf(".") - assetPath.LastIndexOf("/") - 1);
		}
		return assetPath.Substring(assetPath.LastIndexOf("\\") + 1, assetPath.LastIndexOf(".") - assetPath.LastIndexOf("\\") - 1);
	}
}
