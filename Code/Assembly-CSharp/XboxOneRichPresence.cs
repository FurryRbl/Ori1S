using System;
using System.Collections.Generic;
using Game;

// Token: 0x020008C7 RID: 2247
public class XboxOneRichPresence
{
	// Token: 0x170007F4 RID: 2036
	// (get) Token: 0x06003214 RID: 12820 RVA: 0x000D444C File Offset: 0x000D264C
	// (set) Token: 0x06003215 RID: 12821 RVA: 0x000D4453 File Offset: 0x000D2653
	public static bool EnableRichPresence { get; set; }

	// Token: 0x06003216 RID: 12822 RVA: 0x000D445B File Offset: 0x000D265B
	public static bool SetPresence(XboxOneRichPresence.RichPresence presence, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06003217 RID: 12823 RVA: 0x000D445E File Offset: 0x000D265E
	public static bool SetPresence(int userId, XboxOneRichPresence.RichPresence presence, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06003218 RID: 12824 RVA: 0x000D4464 File Offset: 0x000D2664
	public static void UpdateRichPresence()
	{
		if (!XboxOne.Ready)
		{
			return;
		}
		XboxOneRichPresence.UpdateAllRichPresenceStrings();
		if (!XboxOneSession.IsHighResources)
		{
			return;
		}
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.StartScreen)
		{
			return;
		}
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.TitleScreen)
		{
			XboxOneRichPresence.SetPresence(XboxOneRichPresence.RichPresence.Playing, null, null);
			return;
		}
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.Game && GameWorld.Instance && World.CurrentArea != null && World.CurrentArea.Area)
		{
			XboxOneRichPresence.SendLevelStart(World.CurrentArea.Area.AreaIdentifier);
			XboxOneRichPresence.SetPresence(XboxOneRichPresence.RichPresence.PlayingLevel, null, null);
			return;
		}
		XboxOneRichPresence.SetPresence(XboxOneRichPresence.RichPresence.Playing, null, null);
	}

	// Token: 0x06003219 RID: 12825 RVA: 0x000D451C File Offset: 0x000D271C
	public static bool SendLevelStart(string area)
	{
		return false;
	}

	// Token: 0x0600321A RID: 12826 RVA: 0x000D4520 File Offset: 0x000D2720
	private static XboxOneRichPresence.Level MapAreaNameToLevel(string areaName)
	{
		switch (areaName)
		{
		case "sunkenGlades":
			return XboxOneRichPresence.Level.SunkenGlades;
		case "valleyOfTheWind":
			return XboxOneRichPresence.Level.ValleyOfTheWind;
		case "hollowGrove":
			return XboxOneRichPresence.Level.HollowGrove;
		case "thornfeltSwamp":
			return XboxOneRichPresence.Level.ThornfeltSwamp;
		case "moonGrotto":
			return XboxOneRichPresence.Level.MoonGrotto;
		case "ginsoTree":
			return XboxOneRichPresence.Level.GinsoTree;
		case "mistyWoods":
			return XboxOneRichPresence.Level.MistyWoods;
		case "forlornRuins":
			return XboxOneRichPresence.Level.ForlornRuins;
		case "sorrowPass":
			return XboxOneRichPresence.Level.SorrowPass;
		case "mountHoru":
			return XboxOneRichPresence.Level.MountHoru;
		case "mangrove":
			return XboxOneRichPresence.Level.BlackRootBurrows;
		}
		return XboxOneRichPresence.Level.SunkenGlades;
	}

	// Token: 0x0600321B RID: 12827 RVA: 0x000D4636 File Offset: 0x000D2836
	public static void UpdateAllRichPresenceStrings()
	{
	}

	// Token: 0x0600321C RID: 12828 RVA: 0x000D4638 File Offset: 0x000D2838
	public static void SendGameProgress(string UserId, Guid PlayerSessionId, float CompletionPercent)
	{
	}

	// Token: 0x04002D0B RID: 11531
	public static Dictionary<int, XboxOneRichPresence.RichPresence> CurrentRichPresence = new Dictionary<int, XboxOneRichPresence.RichPresence>();

	// Token: 0x04002D0C RID: 11532
	private static XboxOneRichPresence.Level m_currentLevel;

	// Token: 0x020008C9 RID: 2249
	public enum RichPresence
	{
		// Token: 0x04002D1A RID: 11546
		Null,
		// Token: 0x04002D1B RID: 11547
		PlayingLevel,
		// Token: 0x04002D1C RID: 11548
		Playing,
		// Token: 0x04002D1D RID: 11549
		Menus,
		// Token: 0x04002D1E RID: 11550
		Idle
	}

	// Token: 0x020008CA RID: 2250
	public enum Level
	{
		// Token: 0x04002D20 RID: 11552
		Null,
		// Token: 0x04002D21 RID: 11553
		SunkenGlades,
		// Token: 0x04002D22 RID: 11554
		ThornfeltSwamp,
		// Token: 0x04002D23 RID: 11555
		MoonGrotto,
		// Token: 0x04002D24 RID: 11556
		GinsoTree,
		// Token: 0x04002D25 RID: 11557
		MistyWoods,
		// Token: 0x04002D26 RID: 11558
		ForlornRuins,
		// Token: 0x04002D27 RID: 11559
		SorrowPass,
		// Token: 0x04002D28 RID: 11560
		MountHoru,
		// Token: 0x04002D29 RID: 11561
		ValleyOfTheWind,
		// Token: 0x04002D2A RID: 11562
		HollowGrove,
		// Token: 0x04002D2B RID: 11563
		BlackRootBurrows
	}
}
