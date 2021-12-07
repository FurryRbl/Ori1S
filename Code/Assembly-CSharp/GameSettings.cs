using System;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class GameSettings : MonoBehaviour
{
	// Token: 0x0600035F RID: 863 RVA: 0x0000E255 File Offset: 0x0000C455
	public void Awake()
	{
		GameSettings.Instance = this;
		this.ApplyDefaultValues();
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000360 RID: 864 RVA: 0x0000E263 File Offset: 0x0000C463
	// (set) Token: 0x06000361 RID: 865 RVA: 0x0000E26B File Offset: 0x0000C46B
	public bool DamageTextEnabled
	{
		get
		{
			return this.m_damageTextEnabled;
		}
		set
		{
			this.m_damageTextEnabled = value;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000362 RID: 866 RVA: 0x0000E274 File Offset: 0x0000C474
	// (set) Token: 0x06000363 RID: 867 RVA: 0x0000E27C File Offset: 0x0000C47C
	public bool MotionBlurEnabled
	{
		get
		{
			return this.m_motionBlurEnabled;
		}
		set
		{
			this.m_motionBlurEnabled = value;
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000364 RID: 868 RVA: 0x0000E285 File Offset: 0x0000C485
	// (set) Token: 0x06000365 RID: 869 RVA: 0x0000E28D File Offset: 0x0000C48D
	public float VibrationStrength
	{
		get
		{
			return this.m_vibrationStrength;
		}
		set
		{
			this.m_vibrationStrength = value;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06000366 RID: 870 RVA: 0x0000E296 File Offset: 0x0000C496
	// (set) Token: 0x06000367 RID: 871 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
	public bool Vsync
	{
		get
		{
			return QualitySettings.vSyncCount != 0;
		}
		set
		{
			QualitySettings.vSyncCount = ((!value) ? 0 : 1);
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000368 RID: 872 RVA: 0x0000E2B7 File Offset: 0x0000C4B7
	// (set) Token: 0x06000369 RID: 873 RVA: 0x0000E2CA File Offset: 0x0000C4CA
	public Vector2 Resolution
	{
		get
		{
			return new Vector2((float)Screen.width, (float)Screen.height);
		}
		set
		{
			Screen.SetResolution((int)value.x, (int)value.y, this.Fullscreen);
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x0600036A RID: 874 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
	public Vector2 RenderedResolution
	{
		get
		{
			Camera camera = UI.Cameras.Current.Camera;
			return new Vector2((float)camera.pixelWidth, (float)camera.pixelHeight);
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x0600036B RID: 875 RVA: 0x0000E313 File Offset: 0x0000C513
	// (set) Token: 0x0600036C RID: 876 RVA: 0x0000E31B File Offset: 0x0000C51B
	public Language Language
	{
		get
		{
			return this.m_language;
		}
		set
		{
			if (value == Language.Chinese)
			{
				this.m_language = Language.Chinese;
			}
			else
			{
				this.m_language = value;
			}
			Events.Scheduler.OnGameLanguageChange.Call();
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x0600036D RID: 877 RVA: 0x0000E346 File Offset: 0x0000C546
	// (set) Token: 0x0600036E RID: 878 RVA: 0x0000E34D File Offset: 0x0000C54D
	public bool Fullscreen
	{
		get
		{
			return Screen.fullScreen;
		}
		set
		{
			Screen.fullScreen = value;
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x0600036F RID: 879 RVA: 0x0000E355 File Offset: 0x0000C555
	// (set) Token: 0x06000370 RID: 880 RVA: 0x0000E35D File Offset: 0x0000C55D
	public float MusicVolume
	{
		get
		{
			return this.m_musicVolume;
		}
		set
		{
			this.m_musicVolume = value;
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000371 RID: 881 RVA: 0x0000E366 File Offset: 0x0000C566
	// (set) Token: 0x06000372 RID: 882 RVA: 0x0000E36E File Offset: 0x0000C56E
	public float AmbienceVolume
	{
		get
		{
			return this.m_ambienceVolume;
		}
		set
		{
			this.m_ambienceVolume = value;
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06000373 RID: 883 RVA: 0x0000E377 File Offset: 0x0000C577
	// (set) Token: 0x06000374 RID: 884 RVA: 0x0000E37F File Offset: 0x0000C57F
	public float SoundEffectsVolume
	{
		get
		{
			return this.m_soundEffectsVolume;
		}
		set
		{
			this.m_soundEffectsVolume = value;
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06000375 RID: 885 RVA: 0x0000E388 File Offset: 0x0000C588
	// (set) Token: 0x06000376 RID: 886 RVA: 0x0000E390 File Offset: 0x0000C590
	public float Brightness
	{
		get
		{
			return this.m_brightness;
		}
		set
		{
			this.m_brightness = value;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000377 RID: 887 RVA: 0x0000E399 File Offset: 0x0000C599
	// (set) Token: 0x06000378 RID: 888 RVA: 0x0000E3A1 File Offset: 0x0000C5A1
	public float Contrast
	{
		get
		{
			return this.m_contrast;
		}
		set
		{
			this.m_contrast = value;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000379 RID: 889 RVA: 0x0000E3AA File Offset: 0x0000C5AA
	// (set) Token: 0x0600037A RID: 890 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
	public ControlScheme CurrentControlScheme
	{
		get
		{
			return this.m_currentControlSchemes;
		}
		set
		{
			if (this.m_currentControlSchemes != value)
			{
				this.m_currentControlSchemes = value;
				PlayerInput.Instance.RefreshControlScheme();
				Events.Scheduler.OnGameControlSchemeChange.Call();
			}
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x0600037B RID: 891 RVA: 0x0000E3ED File Offset: 0x0000C5ED
	// (set) Token: 0x0600037C RID: 892 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
	public ControlScheme KeyboardScheme
	{
		get
		{
			return this.m_keyboardControlSchemes;
		}
		set
		{
			if (this.m_keyboardControlSchemes != value)
			{
				this.m_keyboardControlSchemes = value;
				Events.Scheduler.OnGameControlSchemeChange.Call();
			}
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x0600037D RID: 893 RVA: 0x0000E427 File Offset: 0x0000C627
	// (set) Token: 0x0600037E RID: 894 RVA: 0x0000E430 File Offset: 0x0000C630
	public KeyboardLayout KeyboardLayout
	{
		get
		{
			return this.m_keyboardLayout;
		}
		set
		{
			if (this.m_keyboardLayout != value)
			{
				this.m_keyboardLayout = value;
				PlayerInput.Instance.RefreshControlScheme();
				Events.Scheduler.OnGameControlSchemeChange.Call();
			}
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x0600037F RID: 895 RVA: 0x0000E469 File Offset: 0x0000C669
	// (set) Token: 0x06000380 RID: 896 RVA: 0x0000E471 File Offset: 0x0000C671
	public UnlockedCutscenes UnlockedCutscenes
	{
		get
		{
			return this.m_unlockedCutscenes;
		}
		set
		{
			if (this.m_unlockedCutscenes != value)
			{
				this.m_unlockedCutscenes = value;
			}
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000381 RID: 897 RVA: 0x0000E486 File Offset: 0x0000C686
	// (set) Token: 0x06000382 RID: 898 RVA: 0x0000E48E File Offset: 0x0000C68E
	public bool OneLifeModeUnlocked
	{
		get
		{
			return this.m_oneLifeModeUnlocked;
		}
		set
		{
			this.m_oneLifeModeUnlocked = value;
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0000E497 File Offset: 0x0000C697
	public bool CutsceneUnlocked(UnlockedCutscenes cutscene)
	{
		return DebugMenuB.UnlockAllCutscenes || this.m_unlockedCutscenes >= cutscene;
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000384 RID: 900 RVA: 0x0000E4B1 File Offset: 0x0000C6B1
	public bool IsTrialAchievementsDirty
	{
		get
		{
			return this.m_isTrialAchievementsDirty;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000385 RID: 901 RVA: 0x0000E4B9 File Offset: 0x0000C6B9
	// (set) Token: 0x06000386 RID: 902 RVA: 0x0000E4C1 File Offset: 0x0000C6C1
	public bool Achieved_CompletePrologue
	{
		get
		{
			return this.m_achieved_completePrologue;
		}
		set
		{
			this.m_achieved_completePrologue = value;
			this.m_isTrialAchievementsDirty = true;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000387 RID: 903 RVA: 0x0000E4D1 File Offset: 0x0000C6D1
	// (set) Token: 0x06000388 RID: 904 RVA: 0x0000E4D9 File Offset: 0x0000C6D9
	public bool Achieved_ReachSpiritTree
	{
		get
		{
			return this.m_achieved_reachSpiritTree;
		}
		set
		{
			this.m_achieved_reachSpiritTree = value;
			this.m_isTrialAchievementsDirty = true;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000389 RID: 905 RVA: 0x0000E4E9 File Offset: 0x0000C6E9
	// (set) Token: 0x0600038A RID: 906 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
	public bool Achieved_ActivateFirstSkill
	{
		get
		{
			return this.m_achieved_activateFirstSkill;
		}
		set
		{
			this.m_achieved_activateFirstSkill = value;
			this.m_isTrialAchievementsDirty = true;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x0600038B RID: 907 RVA: 0x0000E501 File Offset: 0x0000C701
	// (set) Token: 0x0600038C RID: 908 RVA: 0x0000E509 File Offset: 0x0000C709
	public bool Achieved_FindSecret
	{
		get
		{
			return this.m_achieved_findSecret;
		}
		set
		{
			this.m_achieved_findSecret = value;
			this.m_isTrialAchievementsDirty = true;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x0600038D RID: 909 RVA: 0x0000E519 File Offset: 0x0000C719
	// (set) Token: 0x0600038E RID: 910 RVA: 0x0000E521 File Offset: 0x0000C721
	public bool Achieved_FindMapStone
	{
		get
		{
			return this.m_achieved_findMapStone;
		}
		set
		{
			this.m_achieved_findMapStone = value;
			this.m_isTrialAchievementsDirty = true;
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x0000E534 File Offset: 0x0000C734
	public void SaveToWriter(BinaryWriter writer)
	{
		writer.Write(6);
		writer.Write(this.DamageTextEnabled);
		writer.Write(this.MotionBlurEnabled);
		writer.Write(this.VibrationStrength);
		writer.Write(this.MusicVolume);
		writer.Write(this.AmbienceVolume);
		writer.Write(this.SoundEffectsVolume);
		writer.Write(this.Vsync);
		writer.Write((int)this.Language);
		writer.Write((int)this.CurrentControlScheme);
		writer.Write((int)this.KeyboardScheme);
		writer.Write(this.Brightness);
		writer.Write(this.Contrast);
		writer.Write((int)this.KeyboardLayout);
		writer.Write((int)this.UnlockedCutscenes);
		writer.Write(this.m_oneLifeModeUnlocked);
		writer.Write(this.m_achieved_completePrologue);
		writer.Write(this.m_achieved_reachSpiritTree);
		writer.Write(this.m_achieved_activateFirstSkill);
		writer.Write(this.m_achieved_findSecret);
		writer.Write(this.m_achieved_findMapStone);
		writer.Write(this.Fullscreen);
		writer.Write((int)this.Resolution.x);
		writer.Write((int)this.Resolution.y);
		this.m_isTrialAchievementsDirty = false;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0000E678 File Offset: 0x0000C878
	public void LoadFromReader(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		this.DamageTextEnabled = reader.ReadBoolean();
		this.MotionBlurEnabled = reader.ReadBoolean();
		this.VibrationStrength = reader.ReadSingle();
		this.MusicVolume = Mathf.Clamp01(reader.ReadSingle());
		this.AmbienceVolume = Mathf.Clamp01(reader.ReadSingle());
		this.SoundEffectsVolume = Mathf.Clamp01(reader.ReadSingle());
		this.Vsync = reader.ReadBoolean();
		Language language = (Language)reader.ReadInt32();
		this.Language = language;
		this.CurrentControlScheme = (ControlScheme)reader.ReadInt32();
		this.KeyboardScheme = (ControlScheme)reader.ReadInt32();
		this.Brightness = reader.ReadSingle();
		this.Contrast = reader.ReadSingle();
		if (num > 1)
		{
			this.KeyboardLayout = (KeyboardLayout)reader.ReadInt32();
		}
		if (num > 2)
		{
			this.UnlockedCutscenes = (UnlockedCutscenes)reader.ReadInt32();
		}
		if (num > 3)
		{
			this.m_oneLifeModeUnlocked = reader.ReadBoolean();
		}
		if (num > 4)
		{
			this.m_achieved_completePrologue = reader.ReadBoolean();
			this.m_achieved_reachSpiritTree = reader.ReadBoolean();
			this.m_achieved_activateFirstSkill = reader.ReadBoolean();
			this.m_achieved_findSecret = reader.ReadBoolean();
			this.m_achieved_findMapStone = reader.ReadBoolean();
		}
		if (num > 5)
		{
			bool flag = reader.ReadBoolean();
			int num2 = reader.ReadInt32();
			int num3 = reader.ReadInt32();
		}
		this.m_isTrialAchievementsDirty = false;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
	public void ApplyDefaultValues()
	{
		this.m_damageTextEnabled = false;
		this.m_motionBlurEnabled = true;
		this.m_vibrationStrength = 1f;
		this.m_musicVolume = 1f;
		this.m_ambienceVolume = 1f;
		this.m_soundEffectsVolume = 1f;
		this.m_brightness = 0f;
		this.m_contrast = 0f;
		this.m_unlockedCutscenes = UnlockedCutscenes.None;
		this.m_currentControlSchemes = ControlScheme.Controller;
		this.m_keyboardControlSchemes = ControlScheme.KeyboardAndMouse;
		this.m_keyboardLayout = KeyboardLayout.QWERTY;
		this.m_unlockedCutscenes = UnlockedCutscenes.None;
		this.m_isTrialAchievementsDirty = false;
		this.ApplySystemLanguage();
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0000E85D File Offset: 0x0000CA5D
	public void ApplySystemLanguage()
	{
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0000E860 File Offset: 0x0000CA60
	public void LoadSettings()
	{
		if (File.Exists(this.PCSettingsPath))
		{
			using (BinaryReader binaryReader = new BinaryReader(File.Open(this.PCSettingsPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
			{
				this.LoadFromReader(binaryReader);
			}
		}
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0000E8BC File Offset: 0x0000CABC
	public void SaveSettings()
	{
		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(this.PCSettingsPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
		{
			this.SaveToWriter(binaryWriter);
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000395 RID: 917 RVA: 0x0000E908 File Offset: 0x0000CB08
	public string PCSettingsPath
	{
		get
		{
			return Path.Combine(OutputFolder.PlayerDataFolderPath, "settings.bin");
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0000E91C File Offset: 0x0000CB1C
	public void SetControlScheme(ControlScheme controller)
	{
		this.CurrentControlScheme = controller;
		if (this.CurrentControlScheme == ControlScheme.Keyboard)
		{
			this.KeyboardScheme = ControlScheme.Keyboard;
		}
		if (this.CurrentControlScheme == ControlScheme.KeyboardAndMouse)
		{
			this.KeyboardScheme = ControlScheme.KeyboardAndMouse;
		}
	}

	// Token: 0x04000293 RID: 659
	public const int DATA_VERSION = 6;

	// Token: 0x04000294 RID: 660
	public static GameSettings Instance;

	// Token: 0x04000295 RID: 661
	private bool m_isTrialAchievementsDirty;

	// Token: 0x04000296 RID: 662
	[SerializeField]
	private bool m_damageTextEnabled;

	// Token: 0x04000297 RID: 663
	[SerializeField]
	private bool m_motionBlurEnabled = true;

	// Token: 0x04000298 RID: 664
	[SerializeField]
	private float m_vibrationStrength = 1f;

	// Token: 0x04000299 RID: 665
	[SerializeField]
	private Language m_language;

	// Token: 0x0400029A RID: 666
	[SerializeField]
	private float m_musicVolume = 1f;

	// Token: 0x0400029B RID: 667
	[SerializeField]
	private float m_ambienceVolume = 1f;

	// Token: 0x0400029C RID: 668
	[SerializeField]
	private float m_soundEffectsVolume = 1f;

	// Token: 0x0400029D RID: 669
	[SerializeField]
	private float m_brightness;

	// Token: 0x0400029E RID: 670
	[SerializeField]
	private float m_contrast;

	// Token: 0x0400029F RID: 671
	[SerializeField]
	private UnlockedCutscenes m_unlockedCutscenes;

	// Token: 0x040002A0 RID: 672
	[SerializeField]
	private bool m_oneLifeModeUnlocked;

	// Token: 0x040002A1 RID: 673
	[SerializeField]
	private ControlScheme m_currentControlSchemes;

	// Token: 0x040002A2 RID: 674
	[SerializeField]
	private ControlScheme m_keyboardControlSchemes = ControlScheme.KeyboardAndMouse;

	// Token: 0x040002A3 RID: 675
	[SerializeField]
	private KeyboardLayout m_keyboardLayout;

	// Token: 0x040002A4 RID: 676
	[SerializeField]
	private bool m_achieved_completePrologue;

	// Token: 0x040002A5 RID: 677
	[SerializeField]
	private bool m_achieved_reachSpiritTree;

	// Token: 0x040002A6 RID: 678
	[SerializeField]
	private bool m_achieved_activateFirstSkill;

	// Token: 0x040002A7 RID: 679
	[SerializeField]
	private bool m_achieved_findSecret;

	// Token: 0x040002A8 RID: 680
	[SerializeField]
	private bool m_achieved_findMapStone;
}
