using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class XboxOne : MonoBehaviour
{
	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x000351B5 File Offset: 0x000333B5
	public static bool Ready
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000351B8 File Offset: 0x000333B8
	public static bool ControllerReady
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000BFA RID: 3066 RVA: 0x000351BB File Offset: 0x000333BB
	public static uint TitleId
	{
		get
		{
			throw new Exception("Access TitleId on Xbox One only.");
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x000351C7 File Offset: 0x000333C7
	public static bool Help()
	{
		return false;
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x000351CA File Offset: 0x000333CA
	public static void ErrorHelp(string context, uint errorID)
	{
	}

	// Token: 0x040009AE RID: 2478
	private static uint m_titleId;

	// Token: 0x020008C0 RID: 2240
	private class DirectXTex
	{
	}
}
