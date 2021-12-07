using System;
using UnityEngine;

// Token: 0x020008BE RID: 2238
public class ChallengesUI : MonoBehaviour
{
	// Token: 0x170007DF RID: 2015
	// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000D362B File Offset: 0x000D182B
	private static ChallengesUI Instance
	{
		get
		{
			if (ChallengesUI.s_instance == null)
			{
				ChallengesUI.s_instance = UnityEngine.Object.FindObjectOfType<ChallengesUI>();
			}
			return ChallengesUI.s_instance;
		}
	}

	// Token: 0x060031C3 RID: 12739 RVA: 0x000D364C File Offset: 0x000D184C
	private static Rect PushDown(ref Rect rect, float offset)
	{
		rect.Set(rect.x, rect.y + offset, rect.width, rect.height);
		return rect;
	}

	// Token: 0x04002CF3 RID: 11507
	private static ChallengesUI s_instance;
}
