using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class AchievementDatabase : MonoBehaviour
{
	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002BD43 File Offset: 0x00029F43
	public ReadOnlyCollection<AchievementAsset> Achievements
	{
		get
		{
			return new ReadOnlyCollection<AchievementAsset>(this.m_achievements);
		}
	}

	// Token: 0x0400084E RID: 2126
	[SerializeField]
	private List<AchievementAsset> m_achievements;
}
