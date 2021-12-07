using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Token: 0x020008C1 RID: 2241
public class XboxOneAchievements
{
	// Token: 0x170007E3 RID: 2019
	// (get) Token: 0x060031CC RID: 12748 RVA: 0x000D3750 File Offset: 0x000D1950
	// (set) Token: 0x060031CD RID: 12749 RVA: 0x000D3757 File Offset: 0x000D1957
	public static Action<Achievement> OnAchievementUnlocked { get; set; }

	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x060031CE RID: 12750 RVA: 0x000D375F File Offset: 0x000D195F
	// (set) Token: 0x060031CF RID: 12751 RVA: 0x000D3766 File Offset: 0x000D1966
	public static Action<Challenge> OnChallengeUnlocked { get; set; }

	// Token: 0x170007E5 RID: 2021
	// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000D376E File Offset: 0x000D196E
	public static bool AchievementsFetched
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170007E6 RID: 2022
	// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000D3771 File Offset: 0x000D1971
	public static ReadOnlyCollection<Achievement> UnlockedAchievements
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170007E7 RID: 2023
	// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000D3774 File Offset: 0x000D1974
	public static ReadOnlyCollection<Achievement> LockedAchievements
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170007E8 RID: 2024
	// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000D3777 File Offset: 0x000D1977
	public static ReadOnlyCollection<Achievement> SecretAchievements
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170007E9 RID: 2025
	// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000D377A File Offset: 0x000D197A
	public static ReadOnlyCollection<Challenge> UnlockedChallenges
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170007EA RID: 2026
	// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000D377D File Offset: 0x000D197D
	public static ReadOnlyCollection<Challenge> LockedChallenges
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170007EB RID: 2027
	// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000D3780 File Offset: 0x000D1980
	public static ReadOnlyCollection<Challenge> SecretChallenges
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170007EC RID: 2028
	// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000D3783 File Offset: 0x000D1983
	// (set) Token: 0x060031D8 RID: 12760 RVA: 0x000D378A File Offset: 0x000D198A
	public static bool EnableAchievements { get; set; }

	// Token: 0x060031D9 RID: 12761 RVA: 0x000D3792 File Offset: 0x000D1992
	public static bool UpdateAchievements(Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x060031DA RID: 12762 RVA: 0x000D3795 File Offset: 0x000D1995
	public static bool AwardAchievement(AchievementAsset achievement, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x04002CF5 RID: 11509
	private static ReadOnlyCollection<Challenge> s_emptyChallenges = new List<Challenge>().AsReadOnly();

	// Token: 0x04002CF6 RID: 11510
	private static ReadOnlyCollection<Achievement> s_emptyAchievements = new List<Achievement>().AsReadOnly();
}
