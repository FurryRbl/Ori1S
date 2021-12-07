using System;

// Token: 0x02000133 RID: 307
public class Telemetry
{
	// Token: 0x06000C5C RID: 3164 RVA: 0x00038680 File Offset: 0x00036880
	public Telemetry()
	{
		Telemetry.SoulLinkCountStat = new Telemetry.IncrementStat("Award_CreateLotsOfSoulLinks", "soulLinkCountStat");
		Telemetry.CollectedEnergyCountStat = new Telemetry.IncrementStat("Award_CollectLotsOfEnergy", "energyCollectedCount");
		Telemetry.EnemiesKilledByStompCountStat = new Telemetry.IncrementStat("Award_StompFiftyEnemies", "enemiesKilledByStomp");
		Telemetry.EnemiesKilledBySpiritFlameCountStat = new Telemetry.IncrementStat("Award_SpiritFlameFiveHundredEnemies", "enemiesKilledBySpiritFlame");
		Telemetry.EnemiesKilledByChargeFlameCountStat = new Telemetry.IncrementStat("Award_ChargeFlameHundredEnemies", "enemiesKilledByChargeFlame");
		Telemetry.EnemiesKilledByReflectedBashCountStat = new Telemetry.IncrementStat("Award_KillManyWithBashRedirect", "enemiesKilledByReflectedBashProjectiles");
		Telemetry.MapStonesCollectedCountStat = new Telemetry.IncrementStat("Award_FindAllMapStones", "mapStonesCount");
		Telemetry.SecretsRevealedCountStat = new Telemetry.IncrementStat("Award_FindAllSecrets", "secretsCount");
		Telemetry.EnemiesKilledByGrenadeCountStat = new Telemetry.IncrementStat("Award_Kill50EnemiesWithGrenade", "secretsCount----");
		Telemetry.ResolutionWidth = new Telemetry.IntStat("Settings_ResolutionWidthEvent", Telemetry.s_steamString);
		Telemetry.ResolutionHeight = new Telemetry.IntStat("Settings_ResolutionHeightEvent", Telemetry.s_steamString);
		Telemetry.FullScreen = new Telemetry.IntStat("Settings_FullScreenEvent", Telemetry.s_steamString);
		Telemetry.SoundVolume = new Telemetry.FloatStat("Settings_SoundVolumeEvent", Telemetry.s_steamString);
		Telemetry.MusicVolume = new Telemetry.FloatStat("Settings_MusicVolumeEvent", Telemetry.s_steamString);
		Telemetry.Vibration = new Telemetry.IntStat("Settings_VibrationEvent", Telemetry.s_steamString);
		Telemetry.MotionBlur = new Telemetry.IntStat("Settings_MotionBlurEvent", Telemetry.s_steamString);
		Telemetry.DamageText = new Telemetry.IntStat("Settings_DamageTextEvent", Telemetry.s_steamString);
		Telemetry.Gamma = new Telemetry.IntStat("Settings_GammaEvent", Telemetry.s_steamString);
		Telemetry.VSync = new Telemetry.IntStat("Settings_VSyncEvent", Telemetry.s_steamString);
		Telemetry.Language = new Telemetry.IntStat("Settings_LanguageEvent", Telemetry.s_steamString);
		Telemetry.Brightness = new Telemetry.IntStat("Settings_BrightnessEvent", Telemetry.s_steamString);
		Telemetry.Contrast = new Telemetry.IntStat("Settings_ContrastEvent", Telemetry.s_steamString);
		Telemetry.DeathsHeroStat = new Telemetry.IntStat("HeroStats_Deaths", string.Empty);
		Telemetry.TimeHeroStat = new Telemetry.StringStat("HeroStats_Time", string.Empty);
		Telemetry.CompletionHeroStat = new Telemetry.StringStat("HeroStats_Completion", string.Empty);
		Telemetry.InterMediaMinutesPlayed = new Telemetry.IntStat("InterMedia_TimePlayedMinutes", string.Empty);
		Telemetry.SystemLanguage = new Telemetry.IntStat("Telemetry_SystemLanguageEvent", Telemetry.s_steamString);
		Telemetry.SystemLanguage.SendData((int)GameSettings.Instance.Language);
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000388D0 File Offset: 0x00036AD0
	public static void SendSettings()
	{
		Telemetry.ResolutionWidth.SendData((int)GameSettings.Instance.Resolution.x);
		Telemetry.ResolutionHeight.SendData((int)GameSettings.Instance.Resolution.y);
		Telemetry.FullScreen.SendData((!GameSettings.Instance.Fullscreen) ? 0 : 1);
		Telemetry.SoundVolume.SendData(GameSettings.Instance.SoundEffectsVolume);
		Telemetry.MusicVolume.SendData(GameSettings.Instance.MusicVolume);
		Telemetry.Vibration.SendData((int)(2f * GameSettings.Instance.VibrationStrength));
		Telemetry.MotionBlur.SendData((!GameSettings.Instance.MotionBlurEnabled) ? 0 : 1);
		Telemetry.DamageText.SendData((!GameSettings.Instance.DamageTextEnabled) ? 0 : 1);
		Telemetry.VSync.SendData((!GameSettings.Instance.Vsync) ? 0 : 1);
		Telemetry.Language.SendData((int)GameSettings.Instance.Language);
		Telemetry.Brightness.SendData((int)(GameSettings.Instance.Brightness * 1000f));
		Telemetry.Contrast.SendData((int)(GameSettings.Instance.Contrast * 1000f));
	}

	// Token: 0x04000A22 RID: 2594
	public static Telemetry.IncrementStat SoulLinkCountStat;

	// Token: 0x04000A23 RID: 2595
	public static Telemetry.IncrementStat CollectedEnergyCountStat;

	// Token: 0x04000A24 RID: 2596
	public static Telemetry.IncrementStat EnemiesKilledByStompCountStat;

	// Token: 0x04000A25 RID: 2597
	public static Telemetry.IncrementStat EnemiesKilledBySpiritFlameCountStat;

	// Token: 0x04000A26 RID: 2598
	public static Telemetry.IncrementStat EnemiesKilledByChargeFlameCountStat;

	// Token: 0x04000A27 RID: 2599
	public static Telemetry.IncrementStat EnemiesKilledByReflectedBashCountStat;

	// Token: 0x04000A28 RID: 2600
	public static Telemetry.IncrementStat MapStonesCollectedCountStat;

	// Token: 0x04000A29 RID: 2601
	public static Telemetry.IncrementStat SecretsRevealedCountStat;

	// Token: 0x04000A2A RID: 2602
	public static Telemetry.IncrementStat EnemiesKilledByGrenadeCountStat;

	// Token: 0x04000A2B RID: 2603
	public static Telemetry.IntStat ResolutionWidth;

	// Token: 0x04000A2C RID: 2604
	public static Telemetry.IntStat ResolutionHeight;

	// Token: 0x04000A2D RID: 2605
	public static Telemetry.IntStat FullScreen;

	// Token: 0x04000A2E RID: 2606
	public static Telemetry.FloatStat SoundVolume;

	// Token: 0x04000A2F RID: 2607
	public static Telemetry.FloatStat MusicVolume;

	// Token: 0x04000A30 RID: 2608
	public static Telemetry.IntStat Vibration;

	// Token: 0x04000A31 RID: 2609
	public static Telemetry.IntStat MotionBlur;

	// Token: 0x04000A32 RID: 2610
	public static Telemetry.IntStat DamageText;

	// Token: 0x04000A33 RID: 2611
	public static Telemetry.IntStat Gamma;

	// Token: 0x04000A34 RID: 2612
	public static Telemetry.IntStat VSync;

	// Token: 0x04000A35 RID: 2613
	public static Telemetry.IntStat Language;

	// Token: 0x04000A36 RID: 2614
	public static Telemetry.IntStat Brightness;

	// Token: 0x04000A37 RID: 2615
	public static Telemetry.IntStat Contrast;

	// Token: 0x04000A38 RID: 2616
	public static Telemetry.IntStat DeathsHeroStat;

	// Token: 0x04000A39 RID: 2617
	public static Telemetry.StringStat TimeHeroStat;

	// Token: 0x04000A3A RID: 2618
	public static Telemetry.StringStat CompletionHeroStat;

	// Token: 0x04000A3B RID: 2619
	public static Telemetry.IntStat SystemLanguage;

	// Token: 0x04000A3C RID: 2620
	public static Telemetry.IntStat InterMediaMinutesPlayed;

	// Token: 0x04000A3D RID: 2621
	private static string s_steamString = "steam";

	// Token: 0x02000441 RID: 1089
	public class IntStat : Telemetry.Stat
	{
		// Token: 0x06001E56 RID: 7766 RVA: 0x00085F3D File Offset: 0x0008413D
		public IntStat(string xboxOneIdentifier, string steamIdentifier) : base(xboxOneIdentifier, steamIdentifier)
		{
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00085F47 File Offset: 0x00084147
		public void SendData(int value)
		{
			this.m_intValue = value;
			base.SafeSend();
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00085F56 File Offset: 0x00084156
		protected override bool Send()
		{
			Steamworks.SteamInterface.Stats.SetStat(this.m_steamIdentifier, (float)this.m_intValue);
			return true;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00085F76 File Offset: 0x00084176
		protected override bool SteamSend()
		{
			return true;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00085F7C File Offset: 0x0008417C
		protected override bool SendStatisticsTelemetry()
		{
			SteamTelemetry.Data data = new SteamTelemetry.Data();
			data.ExtraData = string.Concat(new object[]
			{
				", \"intStat\": \"",
				this.m_xboxOneIdentifier,
				"\", \"value\": ",
				this.m_intValue
			});
			SteamTelemetry.Instance.Send(TelemetryEvent.AchievementData, data.ToString());
			return true;
		}

		// Token: 0x04001A1B RID: 6683
		private int m_intValue;
	}

	// Token: 0x02000742 RID: 1858
	public abstract class Stat
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x000BB37C File Offset: 0x000B957C
		protected Stat()
		{
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000BB39C File Offset: 0x000B959C
		protected Stat(string xboxOneIdentifier, string steamIdentifier)
		{
			this.m_xboxOneIdentifier = xboxOneIdentifier;
			this.m_steamIdentifier = steamIdentifier;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000BB3D3 File Offset: 0x000B95D3
		protected void SendData()
		{
			this.SafeSend();
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000BB3DC File Offset: 0x000B95DC
		protected void SafeSend()
		{
			if (CheatsHandler.DebugWasEnabled)
			{
				return;
			}
			if (!Steamworks.Ready)
			{
				return;
			}
			if (this.m_xboxOneIdentifier != string.Empty && this.m_steamIdentifier == Telemetry.s_steamString)
			{
				this.SteamSend();
				Steamworks.SteamInterface.Stats.StoreStats();
				return;
			}
			if (this.m_steamIdentifier == string.Empty)
			{
				return;
			}
			this.Send();
			Steamworks.SteamInterface.Stats.StoreStats();
			this.SendStatisticsTelemetry();
		}

		// Token: 0x06002BA2 RID: 11170
		protected abstract bool Send();

		// Token: 0x06002BA3 RID: 11171
		protected abstract bool SteamSend();

		// Token: 0x06002BA4 RID: 11172
		protected abstract bool SendStatisticsTelemetry();

		// Token: 0x0400275E RID: 10078
		protected string m_xboxOneIdentifier = string.Empty;

		// Token: 0x0400275F RID: 10079
		protected string m_steamIdentifier = string.Empty;
	}

	// Token: 0x02000743 RID: 1859
	public class Int64Stat : Telemetry.Stat
	{
		// Token: 0x06002BA5 RID: 11173 RVA: 0x000BB475 File Offset: 0x000B9675
		public Int64Stat(string xboxOneIdentifier, string steamIdentifier) : base(xboxOneIdentifier, steamIdentifier)
		{
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000BB47F File Offset: 0x000B967F
		public void SendData(long value)
		{
			this.m_int64Value = value;
			base.SafeSend();
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000BB48E File Offset: 0x000B968E
		protected override bool Send()
		{
			return true;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000BB491 File Offset: 0x000B9691
		protected override bool SteamSend()
		{
			return true;
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000BB494 File Offset: 0x000B9694
		protected override bool SendStatisticsTelemetry()
		{
			return true;
		}

		// Token: 0x04002760 RID: 10080
		private long m_int64Value;
	}

	// Token: 0x02000744 RID: 1860
	public class FloatStat : Telemetry.Stat
	{
		// Token: 0x06002BAA RID: 11178 RVA: 0x000BB497 File Offset: 0x000B9697
		public FloatStat(string xboxOneIdentifier, string steamIdentifier) : base(xboxOneIdentifier, steamIdentifier)
		{
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000BB4A1 File Offset: 0x000B96A1
		public void SendData(float value)
		{
			this.m_floatValue = value;
			base.SafeSend();
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000BB4B0 File Offset: 0x000B96B0
		protected override bool Send()
		{
			Steamworks.SteamInterface.Stats.SetStat(this.m_steamIdentifier, this.m_floatValue);
			return true;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000BB4CF File Offset: 0x000B96CF
		protected override bool SteamSend()
		{
			return true;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000BB4D4 File Offset: 0x000B96D4
		protected override bool SendStatisticsTelemetry()
		{
			SteamTelemetry.Data data = new SteamTelemetry.Data();
			data.ExtraData = ", \"floatStat\": " + this.m_xboxOneIdentifier + ", \"value\": " + this.m_floatValue.ToString("0.00");
			SteamTelemetry.Instance.Send(TelemetryEvent.AchievementData, data.ToString());
			return true;
		}

		// Token: 0x04002761 RID: 10081
		private float m_floatValue;
	}

	// Token: 0x02000745 RID: 1861
	public class IncrementStat : Telemetry.Stat
	{
		// Token: 0x06002BAF RID: 11183 RVA: 0x000BB528 File Offset: 0x000B9728
		public IncrementStat(string xboxOneIdentifier, string steamIdentifier) : base(xboxOneIdentifier, steamIdentifier)
		{
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000BB532 File Offset: 0x000B9732
		public new void SendData()
		{
			base.SafeSend();
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000BB53A File Offset: 0x000B973A
		public void SendData(float data)
		{
			this.m_floatValue = data;
			base.SafeSend();
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000BB549 File Offset: 0x000B9749
		protected override bool Send()
		{
			Steamworks.SteamInterface.Stats.SetStat(this.m_steamIdentifier, this.m_floatValue);
			return true;
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000BB568 File Offset: 0x000B9768
		protected override bool SteamSend()
		{
			return true;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000BB56B File Offset: 0x000B976B
		protected override bool SendStatisticsTelemetry()
		{
			return true;
		}

		// Token: 0x04002762 RID: 10082
		private float m_floatValue;
	}

	// Token: 0x02000746 RID: 1862
	public class StringStat : Telemetry.Stat
	{
		// Token: 0x06002BB5 RID: 11189 RVA: 0x000BB56E File Offset: 0x000B976E
		public StringStat(string xboxOneIdentifier, string steamIdentifier) : base(xboxOneIdentifier, steamIdentifier)
		{
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000BB578 File Offset: 0x000B9778
		public void SendData(string value)
		{
			this.m_stringValue = value;
			base.SafeSend();
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000BB587 File Offset: 0x000B9787
		protected override bool Send()
		{
			if (this.m_steamIdentifier != string.Empty)
			{
			}
			return true;
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000BB59F File Offset: 0x000B979F
		protected override bool SteamSend()
		{
			return true;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000BB5A2 File Offset: 0x000B97A2
		protected override bool SendStatisticsTelemetry()
		{
			return true;
		}

		// Token: 0x04002763 RID: 10083
		private string m_stringValue;
	}
}
