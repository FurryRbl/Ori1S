using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class AchievementsController : MonoBehaviour
{
	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000490 RID: 1168 RVA: 0x00012A4A File Offset: 0x00010C4A
	public static ReadOnlyCollection<Achievement> UnlockedAchievements
	{
		get
		{
			return new ReadOnlyCollection<Achievement>(new List<Achievement>());
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000491 RID: 1169 RVA: 0x00012A56 File Offset: 0x00010C56
	public static ReadOnlyCollection<Achievement> LockedAchievements
	{
		get
		{
			return new ReadOnlyCollection<Achievement>(new List<Achievement>());
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000492 RID: 1170 RVA: 0x00012A62 File Offset: 0x00010C62
	public static ReadOnlyCollection<Achievement> SecretAchievements
	{
		get
		{
			return new ReadOnlyCollection<Achievement>(new List<Achievement>());
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000493 RID: 1171 RVA: 0x00012A6E File Offset: 0x00010C6E
	public static ReadOnlyCollection<Challenge> UnlockedChallenges
	{
		get
		{
			return new ReadOnlyCollection<Challenge>(new List<Challenge>());
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000494 RID: 1172 RVA: 0x00012A7A File Offset: 0x00010C7A
	public static ReadOnlyCollection<Challenge> LockedChallenges
	{
		get
		{
			return new ReadOnlyCollection<Challenge>(new List<Challenge>());
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000495 RID: 1173 RVA: 0x00012A86 File Offset: 0x00010C86
	public static ReadOnlyCollection<Challenge> SecretChallenges
	{
		get
		{
			return new ReadOnlyCollection<Challenge>(new List<Challenge>());
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00012A92 File Offset: 0x00010C92
	public void Init()
	{
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00012A94 File Offset: 0x00010C94
	public void FixedUpdate()
	{
		if (AchievementsController.m_hintText != string.Empty)
		{
			this.AchievementGainedTestMessageProvider.SetText("Gained Achievement: " + AchievementsController.m_hintText);
			UI.Hints.Show(this.AchievementGainedTestMessageProvider, HintLayer.HintZone, 3f);
			AchievementsController.m_hintText = string.Empty;
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00012AEB File Offset: 0x00010CEB
	public void Destroy()
	{
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00012AF0 File Offset: 0x00010CF0
	public static void AwardAchievement(AchievementAsset achievement)
	{
		if (CheatsHandler.DebugWasEnabled)
		{
			return;
		}
		if (!Steamworks.EnableAchievements)
		{
			return;
		}
		if (achievement == null)
		{
			return;
		}
		if (DebugMenuB.ShowAchievementHint)
		{
			AchievementsController.m_hintText = achievement.Name;
		}
		achievement.IsEarnt = true;
		SteamTelemetry.StringData stringData = new SteamTelemetry.StringData(achievement.XboxOneIdentifier);
		SteamTelemetry.Instance.Send(TelemetryEvent.Achievement, stringData.ToString());
		if (!Steamworks.Ready)
		{
			return;
		}
		Steamworks.SteamInterface.Stats.SetAchievement(achievement.SteamIdentifier);
		Steamworks.SteamInterface.Stats.StoreStats();
	}

	// Token: 0x040003B5 RID: 949
	public AchievementsTestMessageProvider AchievementGainedTestMessageProvider;

	// Token: 0x040003B6 RID: 950
	private static string m_hintText = string.Empty;
}
