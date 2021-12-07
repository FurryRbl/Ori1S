using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Core;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class DebugMenuB : SaveSerialize
{
	// Token: 0x06000B2B RID: 2859 RVA: 0x00031630 File Offset: 0x0002F830
	public static void MakeDebugMenuExist()
	{
		if (DebugMenuB.Instance == null)
		{
			GameObject gameObject = Resources.Load<GameObject>("debugMenu");
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			Utility.DontAssociateWithAnyScene(gameObject2);
			gameObject2.name = gameObject.name;
		}
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00031674 File Offset: 0x0002F874
	public static void ToggleDebugMenu()
	{
		DebugMenuB.MakeDebugMenuExist();
		if (DebugMenuB.Active)
		{
			if (DebugMenuB.Instance)
			{
				DebugMenuB.Instance.HideDebugMenu();
			}
		}
		else if (DebugMenuB.Instance)
		{
			DebugMenuB.Instance.ShowDebugMenu();
		}
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x000316C7 File Offset: 0x0002F8C7
	public void ShowDebugMenu()
	{
		if (DebugMenuB.Active)
		{
			return;
		}
		DebugMenuB.Active = true;
		DebugMenuB.SuspendGameplay();
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x000316DF File Offset: 0x0002F8DF
	public void HideDebugMenu()
	{
		if (!DebugMenuB.Active)
		{
			return;
		}
		DebugMenuB.Active = false;
		DebugMenuB.ResumeGameplay();
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000316F7 File Offset: 0x0002F8F7
	public void Start()
	{
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x000316F9 File Offset: 0x0002F8F9
	private static void SuspendGameplay()
	{
		SuspensionManager.GetSuspendables(DebugMenuB.SuspendablesToIgnoreForGameplay, UI.Cameras.Current.GameObject);
		SuspensionManager.SuspendExcluding(DebugMenuB.SuspendablesToIgnoreForGameplay);
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00031719 File Offset: 0x0002F919
	private static void ResumeGameplay()
	{
		SuspensionManager.ResumeExcluding(DebugMenuB.SuspendablesToIgnoreForGameplay);
		DebugMenuB.SuspendablesToIgnoreForGameplay.Clear();
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00031730 File Offset: 0x0002F930
	public override void Awake()
	{
		DebugMenuB.Instance = this;
		if (this.ImportantLevels.Count > 0)
		{
		}
		base.Awake();
		DebugMenuB.Style = this.Skin.FindStyle("debugMenuItem");
		DebugMenuB.SelectedStyle = this.Skin.FindStyle("selectedDebugMenuItem");
		DebugMenuB.PressedStyle = this.Skin.FindStyle("pressedDebugMenuItem");
		DebugMenuB.DebugMenuStyle = this.Skin.FindStyle("debugMenu");
		DebugMenuB.StyleEnabled = this.Skin.FindStyle("debugMenuItemEnabled");
		DebugMenuB.StyleDisabled = this.Skin.FindStyle("debugMenuItemDisabled");
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x000317D8 File Offset: 0x0002F9D8
	public bool ReinstantiateOri()
	{
		Vector3 position = Characters.Current.Position;
		CharacterFactory.Instance.DestroyCharacter();
		CharacterFactory.Instance.SpawnCharacter(CharacterFactory.Characters.Sein, null, position, null);
		LateStartHook.AddLateStartMethod(delegate
		{
			Characters.Sein.Position = position;
		});
		return true;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x0003182C File Offset: 0x0002FA2C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_cursorIndex);
		ar.Serialize(ref this.m_showGumoSequences);
		ar.Serialize(ref this.m_gumoSequencesCursorIndex);
		ar.Serialize(ref DebugMenuB.DebugControlsEnabled);
		ar.Serialize(ref DebugMenuB.MuteMusic);
		ar.Serialize(ref DebugMenuB.MuteAmbience);
		ar.Serialize(ref DebugMenuB.MuteSoundEffects);
		if (ar.Reading)
		{
			bool flag = false;
			ar.Serialize(ref flag);
			if (flag == DebugMenuB.Active)
			{
				return;
			}
			DebugMenuB.Active = flag;
			if (DebugMenuB.Active)
			{
				this.ShowDebugMenu();
			}
			else
			{
				this.HideDebugMenu();
			}
		}
		else
		{
			ar.Serialize(ref DebugMenuB.Active);
		}
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x000318DB File Offset: 0x0002FADB
	public bool SendLeaderboard()
	{
		LeaderboardsController.UploadScores();
		return true;
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x000318E3 File Offset: 0x0002FAE3
	public bool LoadTestScene()
	{
		GoToSceneController.Instance.GoToScene(this.TestScene, null, false);
		return true;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x000318F8 File Offset: 0x0002FAF8
	[Conditional("DEVELOPMENT_BUILD")]
	public void SendOneSteamTelemetry()
	{
		this.SendSteamTelemetry(1);
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00031901 File Offset: 0x0002FB01
	[Conditional("DEVELOPMENT_BUILD")]
	public void SendTenSteamTelemetry()
	{
		this.SendSteamTelemetry(10);
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0003190C File Offset: 0x0002FB0C
	public void SendSteamTelemetry(int repetition)
	{
		for (int i = 0; i < repetition; i++)
		{
			SteamTelemetry.StringData stringData = new SteamTelemetry.StringData("test #" + i);
			SteamTelemetry.Instance.Send(TelemetryEvent.Test, stringData.ToString());
		}
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00031958 File Offset: 0x0002FB58
	public bool DisableArt()
	{
		if (this.m_art == null)
		{
			this.m_art = (from a in UnityEngine.Object.FindObjectsOfType<GameObject>()
			where a.name == "art"
			select a).ToArray<GameObject>();
			foreach (GameObject gameObject in this.m_art)
			{
				if (gameObject)
				{
					gameObject.SetActive(false);
				}
			}
		}
		else
		{
			foreach (GameObject gameObject2 in this.m_art)
			{
				if (gameObject2)
				{
					gameObject2.SetActive(true);
				}
			}
			this.m_art = null;
		}
		return true;
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00031A1C File Offset: 0x0002FC1C
	public bool PrintReadableTextures()
	{
		using (StreamWriter streamWriter = new StreamWriter("texturesYouCanWriteTo.txt"))
		{
			foreach (Texture2D texture2D in Resources.FindObjectsOfTypeAll(typeof(Texture2D)))
			{
				try
				{
					texture2D.GetPixel(0, 0);
					streamWriter.WriteLine(texture2D.name);
				}
				catch (Exception ex)
				{
				}
			}
		}
		return true;
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00031AB4 File Offset: 0x0002FCB4
	public bool DisableEnemies()
	{
		if (this.m_enemies == null)
		{
			this.m_enemies = (from a in UnityEngine.Object.FindObjectsOfType<GameObject>()
			where a.name == "enemies"
			select a).ToArray<GameObject>();
			foreach (GameObject gameObject in this.m_enemies)
			{
				if (gameObject)
				{
					gameObject.SetActive(false);
				}
			}
		}
		else
		{
			foreach (GameObject gameObject2 in this.m_enemies)
			{
				if (gameObject2)
				{
					gameObject2.SetActive(true);
				}
			}
			this.m_enemies = null;
		}
		return true;
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00031B78 File Offset: 0x0002FD78
	public bool DisableAllParticles()
	{
		if (this.m_particleSystems == null)
		{
			this.m_particleSystems = new List<GameObject>();
			ParticleSystem[] array = UnityEngine.Object.FindObjectsOfType<ParticleSystem>();
			foreach (ParticleSystem particleSystem in array)
			{
				if (particleSystem)
				{
					particleSystem.gameObject.SetActive(false);
					this.m_particleSystems.Add(particleSystem.gameObject);
				}
			}
			ParticleEmitter[] array3 = UnityEngine.Object.FindObjectsOfType<ParticleEmitter>();
			foreach (ParticleEmitter particleEmitter in array3)
			{
				if (particleEmitter)
				{
					particleEmitter.gameObject.SetActive(false);
					this.m_particleSystems.Add(particleEmitter.gameObject);
				}
			}
			InstantiateUtility.DisableParticles = true;
		}
		else
		{
			ParticleSystem[] array5 = UnityEngine.Object.FindObjectsOfType<ParticleSystem>();
			foreach (ParticleSystem particleSystem2 in array5)
			{
				if (particleSystem2)
				{
					particleSystem2.gameObject.SetActive(true);
					this.m_particleSystems.Add(particleSystem2.gameObject);
				}
			}
			ParticleEmitter[] array7 = UnityEngine.Object.FindObjectsOfType<ParticleEmitter>();
			foreach (ParticleEmitter particleEmitter2 in array7)
			{
				if (particleEmitter2)
				{
					particleEmitter2.gameObject.SetActive(true);
					this.m_particleSystems.Add(particleEmitter2.gameObject);
				}
			}
			this.m_particleSystems = null;
			InstantiateUtility.DisableParticles = false;
		}
		return true;
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00031D00 File Offset: 0x0002FF00
	private void BuildMenu()
	{
		this.MenuWidth = (float)Screen.width - this.MenuTopLeftX * 2f;
		this.MenuHeight = (float)Screen.height - this.MenuTopLeftY * 2f - this.VerticalSpace - 30f;
		DebugMenuB.ShouldShowOnlySelectedItem = false;
		this.m_menuList.Clear();
		List<IDebugMenuItem> list = new List<IDebugMenuItem>();
		list.Add(new ActionDebugMenuItem("Save", new Func<bool>(this.SaveGame)));
		list.Add(new ActionDebugMenuItem("Load", new Func<bool>(this.LoadGame)));
		list.Add(new ActionDebugMenuItem("Restore Checkpoint", new Func<bool>(this.RestoreCheckpoint)));
		list.Add(new ActionDebugMenuItem("Instantiate Ori", new Func<bool>(this.ReinstantiateOri)));
		list.Add(new ActionDebugMenuItem("Activate teleporters", new Func<bool>(TeleporterController.ActivateAll)));
		list.Add(new ActionDebugMenuItem("Unlock Difficulties", new Func<bool>(this.UnlockDifficulties)));
		if (SkipCutsceneController.Instance.SkippingAvailable)
		{
			list.Add(new ActionDebugMenuItem("Skipping Available", new Func<bool>(this.SkipAction)));
		}
		list.Add(new BoolDebugMenuItem("Cheats", new Func<bool>(this.CheatsGetter), new Action<bool>(this.CheatsSetter)));
		list.Add(new BoolDebugMenuItem("Disable Sound", () => Sound.AllSoundsDisabled, delegate(bool val)
		{
			Sound.AllSoundsDisabled = val;
		}));
		list.Add(new BoolDebugMenuItem("Unlock Cutscenes", () => DebugMenuB.UnlockAllCutscenes, delegate(bool value)
		{
			DebugMenuB.UnlockAllCutscenes = value;
		}));
		list.Add(new BoolDebugMenuItem("Frame Performance Monitor", () => FramePerformanceMonitor.Enabled, delegate(bool val)
		{
			SceneFrameworkPerformanceMonitor.Enabled = val;
			FramePerformanceMonitor.Enabled = val;
		}));
		list.Add(new BoolDebugMenuItem("Binary Profiler Log", () => BinaryProfilerLogMaker.Enabled, delegate(bool val)
		{
			BinaryProfilerLogMaker.Enabled = val;
		}));
		list.Add(new BoolDebugMenuItem("Leaked Objects Detector", () => LeakedSceneObjectDetector.Enabled, delegate(bool val)
		{
			LeakedSceneObjectDetector.Enabled = val;
		}));
		list.Add(new BoolDebugMenuItem("UberShader Detector", () => UberShaderDetector.Enabled, delegate(bool val)
		{
			UberShaderDetector.Enabled = val;
		}));
		list.Add(new BoolDebugMenuItem("Debug Controls", new Func<bool>(this.DebugControlsGetter), new Action<bool>(this.DebugControlsSetter)));
		list.Add(new BoolDebugMenuItem("Debug text", new Func<bool>(this.DebugTextGetter), new Action<bool>(this.DebugTextSetter)));
		list.Add(new BoolDebugMenuItem("Scene Framework", new Func<bool>(this.DebugSceneFrameworkGetter), new Action<bool>(this.DebugSceneFrameworkSetter)));
		list.Add(new BoolDebugMenuItem("Xbox Controller", new Func<bool>(this.DebugXboxControllerGetter), new Action<bool>(this.DebugXboxControllerSetter)));
		list.Add(new BoolDebugMenuItem("Visual Log", new Func<bool>(this.VisualLogGetter), new Action<bool>(this.VisualLogSetter)));
		list.Add(new BoolDebugMenuItem("Log Callback Hook", new Func<bool>(this.LogCallbackHookGetter), new Action<bool>(this.LogCallbackHookSetter)));
		list.Add(new BoolDebugMenuItem("Fixed Update Sync Debug", new Func<bool>(this.FixedUpdateSyncGetter), new Action<bool>(this.FixedUpdateSyncSetter)));
		list.Add(new ActionDebugMenuItem("Print Object report", new Func<bool>(YouCanLeaveYourHatOn.DebugMenuPrintReport)));
		list.Add(new ActionDebugMenuItem("Disable particles", new Func<bool>(this.DisableAllParticles)));
		list.Add(new ActionDebugMenuItem("Disable art", new Func<bool>(this.DisableArt)));
		list.Add(new ActionDebugMenuItem("Disable enemies", new Func<bool>(this.DisableEnemies)));
		list.Add(new ActionDebugMenuItem("Print readable textures", new Func<bool>(this.PrintReadableTextures)));
		list.Add(new GarbageRunner());
		list.Add(new ActionDebugMenuItem("Reset Steam Stats", new Func<bool>(this.ResetSteamStats)));
		list.Add(new ActionDebugMenuItem("Reset Input Lock", new Func<bool>(this.ResetInputLock)));
		if (Characters.Sein)
		{
			list.Add(new ActionDebugMenuItem("Reset berry position", new Func<bool>(this.ResetNightBerryPosition)));
			list.Add(new ActionDebugMenuItem("Teleport Nightberry", new Func<bool>(this.TeleportNightberry)));
			list.Add(new ActionDebugMenuItem("Visit all spots in current area", new Func<bool>(this.VisitAllAreas)));
		}
		list.Add(new BoolDebugMenuItem("See Achievement Hint", new Func<bool>(this.AchievementHintGetter), new Action<bool>(this.AchievementHintSetter)));
		list.Add(new ActionDebugMenuItem("Gumo Sequences", new Func<bool>(this.GumoSequencesAction)));
		list.Add(new ActionDebugMenuItem("Quit", new Func<bool>(this.Quit)));
		List<IDebugMenuItem> list2 = new List<IDebugMenuItem>();
		list2.Add(new TimeScaleDebugMenuItem("Time Scale"));
		list2.Add(new ZoomDebugMenuItem("Zoom"));
		list2.Add(new GlobalDebugQuadScaleMenuItem("Quad scale"));
		list2.Add(new BoolDebugMenuItem("Super Slow Motion", () => this.m_superSlowMotion, delegate(bool val)
		{
			this.m_superSlowMotion = val;
			Time.timeScale = ((!val) ? 1f : 0.25f);
		}));
		list2.Add(new BoolDebugMenuItem("Sync fixed update", () => SyncFramesTest.EnableSync, delegate(bool val)
		{
			SyncFramesTest.EnableSync = val;
		}));
		list2.Add(new BoolDebugMenuItem("force fixed update", () => SyncFramesTest.EnabledForceFixedUpdate, delegate(bool val)
		{
			SyncFramesTest.EnabledForceFixedUpdate = val;
		}));
		if (Characters.Sein)
		{
			list2.Add(new SeinLevelUpDownDebugMenuItem("Level"));
			list2.Add(new SeinSkillUpDownDebugMenuItem("Skill Points"));
			list2.Add(new LeafsDebugMenuItem("Door Leafs"));
			list2.Add(new MapStonesDebugMenuItem("Map Stones"));
			list2.Add(new HealthDebugMenuItem("Health"));
			list2.Add(new MaxHealthDebugMenuItem("Max Health"));
			list2.Add(new EnergyDebugMenuItem("Energy"));
			list2.Add(new MaxEnergyDebugMenuItem("Max Energy"));
		}
		MonoBehaviour[] array = (MonoBehaviour[])UnityEngine.Object.FindObjectsOfType(typeof(MonoBehaviour));
		foreach (MonoBehaviour monoBehaviour in array)
		{
			IDebugMenuToggleable debugMenuToggleable = monoBehaviour as IDebugMenuToggleable;
			if (debugMenuToggleable != null)
			{
				list2.Add(new DebugMenuTogglerItem(debugMenuToggleable));
			}
		}
		list2.Add(new BoolDebugMenuItem("Deactivate Darkness", new Func<bool>(this.DeactivateDarknessGetter), new Action<bool>(this.DeactivateDarknessSetter)));
		list2.Add(new BoolDebugMenuItem("Camera", new Func<bool>(this.CameraEnabledGetter), new Action<bool>(this.CameraEnabledSetter)));
		list2.Add(new BoolDebugMenuItem("Music", new Func<bool>(this.DebugMuteMusicGetter), new Action<bool>(this.DebugMuteMusicSetter)));
		list2.Add(new BoolDebugMenuItem("Ambience", new Func<bool>(this.DebugMuteAmbienceGetter), new Action<bool>(this.DebugMuteAmbienceSetter)));
		list2.Add(new BoolDebugMenuItem("Sound Effects", new Func<bool>(this.DebugMuteSoundEffectsGetter), new Action<bool>(this.DebugMuteSoundEffectsSetter)));
		list2.Add(new BoolDebugMenuItem("Sound Log", new Func<bool>(this.ShowSoundLogGetter), new Action<bool>(this.ShowSoundLogSetter)));
		list2.Add(new BoolDebugMenuItem("Pink Boxes", new Func<bool>(this.ShowPinkBoxesGetter), new Action<bool>(this.ShowPinkBoxesSetter)));
		list2.Add(new BoolDebugMenuItem("UI", new Func<bool>(this.SeinUIGetter), new Action<bool>(this.SeinUISetter)));
		list2.Add(new BoolDebugMenuItem("Damage Text", new Func<bool>(this.SeinDamageTextGetter), new Action<bool>(this.SeinDamageTextSetter)));
		list2.Add(new BoolDebugMenuItem("120fps Physics", new Func<bool>(this.HighFPSPhysicsGetter), new Action<bool>(this.HighFPSPhysicsSetter)));
		list2.Add(new BoolDebugMenuItem("Invincibility", new Func<bool>(this.SeinInvincibilityGetter), new Action<bool>(this.SeinInvincibilitySetter)));
		list2.Add(new BoolDebugMenuItem("Replay Engine", new Func<bool>(this.ReplayEngineActiveGetter), new Action<bool>(this.ReplayEngineActiveSetter)));
		list2.Add(new ActionDebugMenuItem("Send leaderboard", new Func<bool>(this.SendLeaderboard)));
		list2.Add(new ActionDebugMenuItem("Load Test Scene", new Func<bool>(this.LoadTestScene)));
		list2.Add(new BoolDebugMenuItem("Auto send leaderboard", () => LeaderboardsController.AutoUpload, delegate(bool v)
		{
			LeaderboardsController.AutoUpload = v;
		}));
		List<IDebugMenuItem> list3 = new List<IDebugMenuItem>();
		foreach (string text in this.ImportantLevelsNames)
		{
			bool flag = false;
			foreach (RuntimeSceneMetaData runtimeSceneMetaData in Scenes.Manager.AllScenes)
			{
				if (runtimeSceneMetaData.Scene == text)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				list3.Add(new ActionDebugMenuItem(text, new Func<bool>(this.GoToScene))
				{
					HelpText = "Press X or A to invoke teleport"
				});
			}
		}
		List<IDebugMenuItem> list4 = new List<IDebugMenuItem>();
		foreach (WorldEvents worldEvent in this.m_worldEvents)
		{
			list4.Add(new DebugMenuWorldEventActionMenuItem(worldEvent));
		}
		List<IDebugMenuItem> list5 = new List<IDebugMenuItem>();
		List<IDebugMenuItem> list6 = new List<IDebugMenuItem>();
		if (Characters.Sein)
		{
			list5.Add(new BoolDebugMenuItem("All Abilities", new Func<bool>(AbilityDebugMenuItems.AllAbilitiesGetter), new Action<bool>(AbilityDebugMenuItems.AllAbilitiesSetter)));
			list5.Add(new BoolDebugMenuItem("Bash", new Func<bool>(AbilityDebugMenuItems.BashGetter), new Action<bool>(AbilityDebugMenuItems.BashSetter)));
			list5.Add(new BoolDebugMenuItem("Wall Jump", new Func<bool>(AbilityDebugMenuItems.WallJumpGetter), new Action<bool>(AbilityDebugMenuItems.WallJumpSetter)));
			list5.Add(new BoolDebugMenuItem("Stomp", new Func<bool>(AbilityDebugMenuItems.StompGetter), new Action<bool>(AbilityDebugMenuItems.StompSetter)));
			list5.Add(new BoolDebugMenuItem("Double Jump", new Func<bool>(AbilityDebugMenuItems.DoubleJumpGetter), new Action<bool>(AbilityDebugMenuItems.DoubleJumpSetter)));
			list5.Add(new BoolDebugMenuItem("Charge Jump", new Func<bool>(AbilityDebugMenuItems.ChargeJumpGetter), new Action<bool>(AbilityDebugMenuItems.ChargeJumpSetter)));
			list5.Add(new BoolDebugMenuItem("Climb", new Func<bool>(AbilityDebugMenuItems.ClimbGetter), new Action<bool>(AbilityDebugMenuItems.ClimbSetter)));
			list5.Add(new BoolDebugMenuItem("Glide", new Func<bool>(AbilityDebugMenuItems.GlideGetter), new Action<bool>(AbilityDebugMenuItems.GlideSetter)));
			list5.Add(new BoolDebugMenuItem("Spirit Flame", new Func<bool>(AbilityDebugMenuItems.SpiritFlameGetter), new Action<bool>(AbilityDebugMenuItems.SpiritFlameSetter)));
			list5.Add(new BoolDebugMenuItem("Charge Flame", new Func<bool>(AbilityDebugMenuItems.ChargeFlameGetter), new Action<bool>(AbilityDebugMenuItems.ChargeFlameSetter)));
			list5.Add(new BoolDebugMenuItem("Dash", new Func<bool>(AbilityDebugMenuItems.DashGetter), new Action<bool>(AbilityDebugMenuItems.DashSetter)));
			list5.Add(new BoolDebugMenuItem("Grenade", new Func<bool>(AbilityDebugMenuItems.GrenadeGetter), new Action<bool>(AbilityDebugMenuItems.GrenadeSetter)));
			list6.Add(new BoolDebugMenuItem("Quick Flame", new Func<bool>(AbilityDebugMenuItems.QuickFlameGetter), new Action<bool>(AbilityDebugMenuItems.QuickFlameSetter)));
			list6.Add(new BoolDebugMenuItem("Spark Flame", new Func<bool>(AbilityDebugMenuItems.SparkFlameGetter), new Action<bool>(AbilityDebugMenuItems.SparkFlameSetter)));
			list6.Add(new BoolDebugMenuItem("Split Flame", new Func<bool>(AbilityDebugMenuItems.SplitFlameUpgradeGetter), new Action<bool>(AbilityDebugMenuItems.SplitFlameUpgradeSetter)));
			list6.Add(new BoolDebugMenuItem("Cinder Flame", new Func<bool>(AbilityDebugMenuItems.CinderFlameGetter), new Action<bool>(AbilityDebugMenuItems.CinderFlameSetter)));
			list6.Add(new BoolDebugMenuItem("Rapid Fire", new Func<bool>(AbilityDebugMenuItems.RapidFireGetter), new Action<bool>(AbilityDebugMenuItems.RapidFireSetter)));
			list6.Add(new BoolDebugMenuItem("Ultra Split Flame", new Func<bool>(AbilityDebugMenuItems.UltraSplitFlameGetter), new Action<bool>(AbilityDebugMenuItems.UltraSplitFlameSetter)));
			list6.Add(new BoolDebugMenuItem("Water Breath", new Func<bool>(AbilityDebugMenuItems.WaterBreathGetter), new Action<bool>(AbilityDebugMenuItems.WaterBreathSetter)));
			list6.Add(new BoolDebugMenuItem("Magnet", new Func<bool>(AbilityDebugMenuItems.MagnetGetter), new Action<bool>(AbilityDebugMenuItems.MagnetSetter)));
			list6.Add(new BoolDebugMenuItem("Ultra Magnet", new Func<bool>(AbilityDebugMenuItems.UltraMagnetGetter), new Action<bool>(AbilityDebugMenuItems.UltraMagnetSetter)));
			list6.Add(new BoolDebugMenuItem("Soul Efficiency", new Func<bool>(AbilityDebugMenuItems.SoulEfficiencyGetter), new Action<bool>(AbilityDebugMenuItems.SoulEfficiencySetter)));
			list6.Add(new BoolDebugMenuItem("Charge Flame Blast", new Func<bool>(AbilityDebugMenuItems.ChargeFlameBlastGetter), new Action<bool>(AbilityDebugMenuItems.ChargeFlameBlastSetter)));
			list6.Add(new BoolDebugMenuItem("Double Jump Upgrade", new Func<bool>(AbilityDebugMenuItems.DoubleJumpUpgradeGetter), new Action<bool>(AbilityDebugMenuItems.DoubleJumpUpgradeSetter)));
			list6.Add(new BoolDebugMenuItem("Bash Upgrade", new Func<bool>(AbilityDebugMenuItems.BashUpgradeGetter), new Action<bool>(AbilityDebugMenuItems.BashUpgradeSetter)));
			list6.Add(new BoolDebugMenuItem("Ultra Defense", new Func<bool>(AbilityDebugMenuItems.UltraDefenseGetter), new Action<bool>(AbilityDebugMenuItems.UltraDefenseSetter)));
			list6.Add(new BoolDebugMenuItem("Health Efficiency", new Func<bool>(AbilityDebugMenuItems.HealthEfficiencyGetter), new Action<bool>(AbilityDebugMenuItems.HealthEfficiencySetter)));
			list6.Add(new BoolDebugMenuItem("Sense", new Func<bool>(AbilityDebugMenuItems.SenseGetter), new Action<bool>(AbilityDebugMenuItems.SenseSetter)));
			list6.Add(new BoolDebugMenuItem("Stomp Upgrade", new Func<bool>(AbilityDebugMenuItems.StompUpgradeGetter), new Action<bool>(AbilityDebugMenuItems.StompUpgradeSetter)));
			list6.Add(new BoolDebugMenuItem("Map Markers", new Func<bool>(AbilityDebugMenuItems.MapMarkersGetter), new Action<bool>(AbilityDebugMenuItems.MapMarkersSetter)));
			list6.Add(new BoolDebugMenuItem("Energy Efficiency", new Func<bool>(AbilityDebugMenuItems.EnergyEfficiencyGetter), new Action<bool>(AbilityDebugMenuItems.EnergyEfficiencySetter)));
			list6.Add(new BoolDebugMenuItem("Health Markers", new Func<bool>(AbilityDebugMenuItems.HealthMarkersGetter), new Action<bool>(AbilityDebugMenuItems.HealthMarkersSetter)));
			list6.Add(new BoolDebugMenuItem("Energy Markers", new Func<bool>(AbilityDebugMenuItems.EnergyMarkersGetter), new Action<bool>(AbilityDebugMenuItems.EnergyMarkersSetter)));
			list6.Add(new BoolDebugMenuItem("Ability Markers", new Func<bool>(AbilityDebugMenuItems.AbilityMarkersGetter), new Action<bool>(AbilityDebugMenuItems.AbilityMarkersSetter)));
			list6.Add(new BoolDebugMenuItem("Rekindle", new Func<bool>(AbilityDebugMenuItems.RekindleGetter), new Action<bool>(AbilityDebugMenuItems.RekindleSetter)));
			list6.Add(new BoolDebugMenuItem("Regroup", new Func<bool>(AbilityDebugMenuItems.RegroupGetter), new Action<bool>(AbilityDebugMenuItems.RegroupSetter)));
			list6.Add(new BoolDebugMenuItem("Charge Flame Efficiency", new Func<bool>(AbilityDebugMenuItems.ChargeFlameEfficiencyGetter), new Action<bool>(AbilityDebugMenuItems.ChargeFlameEfficiencySetter)));
			list6.Add(new BoolDebugMenuItem("Ultra Soul Flame", new Func<bool>(AbilityDebugMenuItems.UltraSoulFlameGetter), new Action<bool>(AbilityDebugMenuItems.UltraSoulFlameSetter)));
			list6.Add(new BoolDebugMenuItem("Grenade Upgrade", new Func<bool>(AbilityDebugMenuItems.GrenadeUpgradeGetter), new Action<bool>(AbilityDebugMenuItems.GrenadeUpgradeSetter)));
			list6.Add(new BoolDebugMenuItem("Charge Dash", new Func<bool>(AbilityDebugMenuItems.ChargeDashGetter), new Action<bool>(AbilityDebugMenuItems.ChargeDashSetter)));
			list6.Add(new BoolDebugMenuItem("Air Dash", new Func<bool>(AbilityDebugMenuItems.AirDashGetter), new Action<bool>(AbilityDebugMenuItems.AirDashSetter)));
			list6.Add(new BoolDebugMenuItem("Grenade Efficiency", new Func<bool>(AbilityDebugMenuItems.GrenadeEfficiencyGetter), new Action<bool>(AbilityDebugMenuItems.GrenadeEfficiencySetter)));
		}
		List<IDebugMenuItem> list7 = new List<IDebugMenuItem>();
		if (!XboxLiveController.IsContentPackage)
		{
			list7.Add(new ActionDebugMenuItem("Start FPS Test 0", new Func<bool>(this.StartFPSTest0)));
			list7.Add(new ActionDebugMenuItem("Start FPS Test 60", new Func<bool>(this.StartFPSTest60)));
			list7.Add(new ActionDebugMenuItem("Start FPS Test 120", new Func<bool>(this.StartFPSTest120)));
			list7.Add(new ActionDebugMenuItem("Start FPS Test 180", new Func<bool>(this.StartFPSTest180)));
			list7.Add(new ActionDebugMenuItem("Start FPS Test 240", new Func<bool>(this.StartFPSTest240)));
			list7.Add(new BoolDebugMenuItem("Override Misty Woods Conditions", () => SceneFPSTest.OVERRIDE_MISTYWOODS_CONDITION, delegate(bool val)
			{
				SceneFPSTest.OVERRIDE_MISTYWOODS_CONDITION = val;
			}));
			list7.Add(new BoolDebugMenuItem("FPS Test Reverse IsCutscene", () => SceneFPSTest.HACK_REVERSE_ISCUTSCENE, delegate(bool val)
			{
				SceneFPSTest.HACK_REVERSE_ISCUTSCENE = val;
			}));
			list7.Add(new BoolDebugMenuItem("Screenshot", () => SceneFPSTest.SHOULD_CREATE_SCREENSHOT, delegate(bool val)
			{
				SceneFPSTest.SHOULD_CREATE_SCREENSHOT = val;
			}));
			list7.Add(new BoolDebugMenuItem("Memory Report", () => SceneFPSTest.SHOULD_CREATE_MEMORY_REPORT, delegate(bool val)
			{
				SceneFPSTest.SHOULD_CREATE_MEMORY_REPORT = val;
			}));
			list7.Add(new BoolDebugMenuItem("Basic Sample", () => SceneFPSTest.SHOULD_RUN_SAMPLE, delegate(bool val)
			{
				SceneFPSTest.SHOULD_RUN_SAMPLE = val;
			}));
			list7.Add(new BoolDebugMenuItem("No Camera", () => SceneFPSTest.SHOULD_RUN_CPU_SAMPLE, delegate(bool val)
			{
				SceneFPSTest.SHOULD_RUN_CPU_SAMPLE = val;
			}));
			list7.Add(new BoolDebugMenuItem("Quad Scale 0", () => SceneFPSTest.SHOULD_RUN_CPU_B_SAMPLE, delegate(bool val)
			{
				SceneFPSTest.SHOULD_RUN_CPU_B_SAMPLE = val;
			}));
			list7.Add(new BoolDebugMenuItem("Draw Debug UI", () => SceneFPSTest.DRAW_DEBUG_UI, delegate(bool val)
			{
				SceneFPSTest.DRAW_DEBUG_UI = val;
			}));
		}
		list7.Add(new BoolDebugMenuItem("Streaming Install Debug Override", new Func<bool>(this.StreamingInstallDebugGetter), new Action<bool>(this.StreamingInstallDebugSetter)));
		list7.Add(new ActionDebugMenuItem("Break Telem URL", new Func<bool>(this.BreakSteamTelemetryURL)));
		list7.Add(new ActionDebugMenuItem("Set Telemetry to UPF", new Func<bool>(this.SetSteamTelemetryURLToUPF)));
		List<IDebugMenuItem> list8 = new List<IDebugMenuItem>();
		list8.Add(new BoolDebugMenuItem("Clean Water", new Func<bool>(this.CleanWaterGetter), new Action<bool>(this.CleanWaterSetter)));
		list8.Add(new BoolDebugMenuItem("Wind Released", new Func<bool>(this.WindReleasedGetter), new Action<bool>(this.WindReleasedSetter)));
		list8.Add(new BoolDebugMenuItem("Gumo Free", new Func<bool>(this.GumoFreeGetter), new Action<bool>(this.GumFreeSetter)));
		list8.Add(new BoolDebugMenuItem("Forlorn Energy Restored", new Func<bool>(this.ForlornEnergyRestoredGetter), new Action<bool>(this.ForlornEnergyRestoredSetter)));
		list8.Add(new BoolDebugMenuItem("Mist Lifted", new Func<bool>(this.MistLiftedGetter), new Action<bool>(this.MistLiftedSetter)));
		list8.Add(new BoolDebugMenuItem("Ginso Key", new Func<bool>(this.GinsoKeyGetter), new Action<bool>(this.GinsoKeySetter)));
		list8.Add(new BoolDebugMenuItem("Forlorn Ruins Key", new Func<bool>(this.ForlornRuinsKeyGetter), new Action<bool>(this.ForlornRuinsKeySetter)));
		list8.Add(new BoolDebugMenuItem("Horu Key", new Func<bool>(this.HoruKeyGetter), new Action<bool>(this.HoruKeySetter)));
		list8.Add(new BoolDebugMenuItem("Darkness Lifted", new Func<bool>(this.DarknessLiftedGetter), new Action<bool>(this.DarknessLiftedSetter)));
		if (GameController.Instance.IsTrial)
		{
			list5.Clear();
			list6.Clear();
			list7.Clear();
			list8.Clear();
		}
		if (list.Count > 0)
		{
			this.m_menuList.Add(list);
		}
		if (list2.Count > 0)
		{
			this.m_menuList.Add(list2);
		}
		if (list3.Count > 0)
		{
			this.m_menuList.Add(list3);
		}
		if (list4.Count > 0)
		{
			this.m_menuList.Add(list4);
		}
		if (list5.Count > 0)
		{
			this.m_menuList.Add(list5);
		}
		if (list6.Count > 0)
		{
			this.m_menuList.Add(list6);
		}
		if (list7.Count > 0)
		{
			this.m_menuList.Add(list7);
		}
		if (list8.Count > 0)
		{
			this.m_menuList.Add(list8);
		}
		this.m_showGumoSequences = false;
		int num = 8;
		this.m_gumoSequencesMenuList.Clear();
		List<IDebugMenuItem> list9 = new List<IDebugMenuItem>();
		List<IDebugMenuItem> list10 = new List<IDebugMenuItem>();
		List<IDebugMenuItem> list11 = new List<IDebugMenuItem>();
		List<IDebugMenuItem> list12 = new List<IDebugMenuItem>();
		List<IDebugMenuItem> list13 = new List<IDebugMenuItem>();
		foreach (GoToSequenceData goToSequenceData in this.GumoSequence)
		{
			if (goToSequenceData.Scene)
			{
			}
			if (list9.Count < num)
			{
				list9.Add(new GoToSequenceMenuItem(goToSequenceData));
			}
			else if (list10.Count < num)
			{
				list10.Add(new GoToSequenceMenuItem(goToSequenceData));
			}
			else if (list11.Count < num)
			{
				list11.Add(new GoToSequenceMenuItem(goToSequenceData));
			}
			else if (list12.Count < num)
			{
				list12.Add(new GoToSequenceMenuItem(goToSequenceData));
			}
			else
			{
				list13.Add(new GoToSequenceMenuItem(goToSequenceData));
			}
		}
		if (list9.Count > 0)
		{
			this.m_gumoSequencesMenuList.Add(list9);
		}
		if (list10.Count > 0)
		{
			this.m_gumoSequencesMenuList.Add(list10);
		}
		if (list11.Count > 0)
		{
			this.m_gumoSequencesMenuList.Add(list11);
		}
		if (list12.Count > 0)
		{
			this.m_gumoSequencesMenuList.Add(list12);
		}
		if (list13.Count > 0)
		{
			this.m_gumoSequencesMenuList.Add(list13);
		}
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00033684 File Offset: 0x00031884
	private bool UnlockDifficulties()
	{
		GameSettings.Instance.OneLifeModeUnlocked = true;
		GameSettings.Instance.SaveSettings();
		return true;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0003369C File Offset: 0x0003189C
	private bool BreakSteamTelemetryURL()
	{
		SteamTelemetry.URL = "http://www.ssodifjsoifj.com";
		return true;
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x000336A9 File Offset: 0x000318A9
	private bool SetSteamTelemetryURLToUPF()
	{
		SteamTelemetry.URL = "http://www.upf.co.il/steamTelemetryTest.php";
		return true;
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x000336B6 File Offset: 0x000318B6
	private bool VisitAllAreas()
	{
		World.CurrentArea.VisitAllAreas();
		World.CurrentArea.UpdateCompletionAmount();
		return true;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x000336CD File Offset: 0x000318CD
	private bool StreamingInstallDebugGetter()
	{
		return DebugMenuB.IsFullyInstalledDebugOverride;
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000336D4 File Offset: 0x000318D4
	private void StreamingInstallDebugSetter(bool value)
	{
		DebugMenuB.IsFullyInstalledDebugOverride = value;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x000336DC File Offset: 0x000318DC
	private bool FixedUpdateSyncGetter()
	{
		return FixedUpdateSyncTracker.Enable;
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x000336E3 File Offset: 0x000318E3
	private void FixedUpdateSyncSetter(bool value)
	{
		FixedUpdateSyncTracker.Enable = value;
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x000336EB File Offset: 0x000318EB
	private bool HighFPSPhysicsGetter()
	{
		return this.m_highFPSPhysics;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000336F4 File Offset: 0x000318F4
	private void HighFPSPhysicsSetter(bool value)
	{
		this.m_highFPSPhysics = value;
		if (value)
		{
			Time.fixedDeltaTime = 0.008333334f;
		}
		else
		{
			Time.fixedDeltaTime = 0.016666668f;
		}
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00033727 File Offset: 0x00031927
	private bool LimitPhysicsIterationGetter()
	{
		return Mathf.Round(Time.maximumDeltaTime * 100f) == Mathf.Round(1.6666667f);
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00033745 File Offset: 0x00031945
	private void LimitPhysicsIterationSetter(bool obj)
	{
		Time.maximumDeltaTime = ((!obj) ? 0.033333335f : 0.016666668f);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x00033761 File Offset: 0x00031961
	private bool StartFPSTest0()
	{
		SceneFPSTest.SetupTheTest();
		return true;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00033769 File Offset: 0x00031969
	private bool StartFPSTest60()
	{
		SceneFPSTest.CurrentSceneMetaDataIndex = 60;
		SceneFPSTest.SetupTheTest();
		return true;
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00033778 File Offset: 0x00031978
	private bool StartFPSTest120()
	{
		SceneFPSTest.CurrentSceneMetaDataIndex = 120;
		SceneFPSTest.SetupTheTest();
		return true;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00033787 File Offset: 0x00031987
	private bool StartFPSTest180()
	{
		SceneFPSTest.CurrentSceneMetaDataIndex = 180;
		SceneFPSTest.SetupTheTest();
		return true;
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x00033799 File Offset: 0x00031999
	private bool StartFPSTest240()
	{
		SceneFPSTest.CurrentSceneMetaDataIndex = 240;
		SceneFPSTest.SetupTheTest();
		return true;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x000337AB File Offset: 0x000319AB
	private bool ResetSteamStats()
	{
		if (Steamworks.Ready)
		{
			Steamworks.SteamInterface.Stats.ResetAllStats(true);
		}
		return true;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x000337C9 File Offset: 0x000319C9
	private void ForlornRuinsKeySetter(bool obj)
	{
		Keys.ForlornRuins = obj;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x000337D1 File Offset: 0x000319D1
	private bool ForlornRuinsKeyGetter()
	{
		return Keys.ForlornRuins;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x000337D8 File Offset: 0x000319D8
	private void GinsoKeySetter(bool obj)
	{
		Keys.GinsoTree = obj;
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x000337E0 File Offset: 0x000319E0
	private bool GinsoKeyGetter()
	{
		return Keys.GinsoTree;
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x000337E7 File Offset: 0x000319E7
	private void HoruKeySetter(bool obj)
	{
		Keys.MountHoru = obj;
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x000337EF File Offset: 0x000319EF
	private bool HoruKeyGetter()
	{
		return Keys.MountHoru;
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x000337F6 File Offset: 0x000319F6
	public void AddWorldEvent(WorldEvents worldEvent)
	{
		if (this.m_worldEvents.Contains(worldEvent))
		{
			return;
		}
		this.m_worldEvents.Add(worldEvent);
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00033816 File Offset: 0x00031A16
	private bool WindReleasedGetter()
	{
		return Sein.World.Events.WindRestored;
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0003381D File Offset: 0x00031A1D
	private void WindReleasedSetter(bool released)
	{
		Sein.World.Events.WindRestored = released;
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00033825 File Offset: 0x00031A25
	private bool DarknessLiftedGetter()
	{
		return Sein.World.Events.DarknessLifted;
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0003382C File Offset: 0x00031A2C
	private void DarknessLiftedSetter(bool isDarknessLifted)
	{
		Sein.World.Events.DarknessLifted = isDarknessLifted;
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00033834 File Offset: 0x00031A34
	private bool LoadGame()
	{
		if (!GameController.Instance.SaveGameController.PerformLoad())
		{
		}
		return true;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0003384B File Offset: 0x00031A4B
	private bool SkipAction()
	{
		SkipCutsceneController.Instance.SkipCutscene();
		return true;
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00033858 File Offset: 0x00031A58
	private bool SaveGame()
	{
		this.HideDebugMenu();
		GameController.Instance.CreateCheckpoint();
		GameController.Instance.SaveGameController.PerformSave();
		return true;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00033885 File Offset: 0x00031A85
	private void GumFreeSetter(bool obj)
	{
		Sein.World.Events.GumoFree = obj;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0003388D File Offset: 0x00031A8D
	private bool GumoFreeGetter()
	{
		return Sein.World.Events.GumoFree;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00033894 File Offset: 0x00031A94
	private void ForlornEnergyRestoredSetter(bool obj)
	{
		Sein.World.Events.GravityActivated = obj;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0003389C File Offset: 0x00031A9C
	private bool ForlornEnergyRestoredGetter()
	{
		return Sein.World.Events.GravityActivated;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x000338A3 File Offset: 0x00031AA3
	private void MistLiftedSetter(bool value)
	{
		Sein.World.Events.MistLifted = value;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000338AB File Offset: 0x00031AAB
	private bool MistLiftedGetter()
	{
		return Sein.World.Events.MistLifted;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x000338B2 File Offset: 0x00031AB2
	private void SeinUISetter(bool obj)
	{
		SeinUI.DebugHideUI = obj;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x000338BA File Offset: 0x00031ABA
	private void SeinDamageTextSetter(bool obj)
	{
		GameSettings.Instance.DamageTextEnabled = obj;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000338C7 File Offset: 0x00031AC7
	private bool CameraEnabledGetter()
	{
		return UI.Cameras.Current.Camera.enabled;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000338D8 File Offset: 0x00031AD8
	private void CameraEnabledSetter(bool obj)
	{
		UI.Cameras.Current.Camera.enabled = obj;
		if (obj)
		{
			return;
		}
		this.HideDebugMenu();
		Graphics.SetRenderTarget(UI.Cameras.Current.GetComponent<Camera>().targetTexture);
		GL.Clear(true, true, Color.black);
		Graphics.SetRenderTarget(null);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00033928 File Offset: 0x00031B28
	private bool DebugMuteMusicGetter()
	{
		return DebugMenuB.MuteMusic;
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x0003392F File Offset: 0x00031B2F
	private void DebugMuteMusicSetter(bool value)
	{
		DebugMenuB.MuteMusic = value;
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00033937 File Offset: 0x00031B37
	private bool DebugMuteAmbienceGetter()
	{
		return DebugMenuB.MuteAmbience;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x0003393E File Offset: 0x00031B3E
	private void DebugMuteAmbienceSetter(bool value)
	{
		DebugMenuB.MuteAmbience = value;
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00033946 File Offset: 0x00031B46
	private bool DebugMuteSoundEffectsGetter()
	{
		return DebugMenuB.MuteSoundEffects;
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x0003394D File Offset: 0x00031B4D
	private void DebugMuteSoundEffectsSetter(bool value)
	{
		DebugMenuB.MuteSoundEffects = value;
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00033955 File Offset: 0x00031B55
	private bool SeinUIGetter()
	{
		return SeinUI.DebugHideUI;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0003395C File Offset: 0x00031B5C
	private bool SeinDamageTextGetter()
	{
		return GameSettings.Instance.DamageTextEnabled;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00033968 File Offset: 0x00031B68
	private bool SeinInvincibilityGetter()
	{
		return Characters.Sein && Characters.Sein.Mortality.DamageReciever && Characters.Sein.Mortality.DamageReciever.IsImmortal;
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000339B4 File Offset: 0x00031BB4
	private void SeinInvincibilitySetter(bool newValue)
	{
		if (Characters.Sein)
		{
			Characters.Sein.Mortality.DamageReciever.IsImmortal = newValue;
		}
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x000339E5 File Offset: 0x00031BE5
	private bool ReplayEngineActiveGetter()
	{
		return Recorder.Instance && Recorder.Instance.Active;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00033A06 File Offset: 0x00031C06
	private void ReplayEngineActiveSetter(bool newValue)
	{
		if (Recorder.Instance)
		{
			Recorder.Instance.Active = newValue;
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00033A22 File Offset: 0x00031C22
	private bool GumoSequencesAction()
	{
		this.m_showGumoSequences = true;
		return false;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00033A2C File Offset: 0x00031C2C
	private bool AchievementHintGetter()
	{
		return DebugMenuB.ShowAchievementHint;
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00033A33 File Offset: 0x00031C33
	private void AchievementHintSetter(bool newValue)
	{
		DebugMenuB.ShowAchievementHint = newValue;
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00033A3B File Offset: 0x00031C3B
	private bool CleanWaterGetter()
	{
		return Sein.World.Events.WaterPurified;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00033A42 File Offset: 0x00031C42
	private void CleanWaterSetter(bool newValue)
	{
		Sein.World.Events.WaterPurified = newValue;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00033A4A File Offset: 0x00031C4A
	private void CheatsSetter(bool arg)
	{
		CheatsHandler.Instance.DebugEnabled = arg;
		if (!CheatsHandler.Instance.DebugEnabled)
		{
			DebugMenuB.ToggleDebugMenu();
		}
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00033A6B File Offset: 0x00031C6B
	private bool CheatsGetter()
	{
		return CheatsHandler.Instance.DebugEnabled;
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00033A77 File Offset: 0x00031C77
	private void DebugControlsSetter(bool arg)
	{
		DebugMenuB.DebugControlsEnabled = arg;
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00033A7F File Offset: 0x00031C7F
	private bool DebugControlsGetter()
	{
		return DebugMenuB.DebugControlsEnabled;
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00033A86 File Offset: 0x00031C86
	private void DebugTextSetter(bool arg)
	{
		DebugGUIText.Enabled = arg;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00033A8E File Offset: 0x00031C8E
	private bool DebugTextGetter()
	{
		return DebugGUIText.Enabled;
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00033A95 File Offset: 0x00031C95
	private void DebugSceneFrameworkSetter(bool arg)
	{
		this.m_showSceneFrameworkDebug = arg;
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x00033A9E File Offset: 0x00031C9E
	private bool DebugSceneFrameworkGetter()
	{
		return this.m_showSceneFrameworkDebug;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00033AA6 File Offset: 0x00031CA6
	private bool VisualLogGetter()
	{
		return VisualLog.Instance != null;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00033AB4 File Offset: 0x00031CB4
	private void VisualLogSetter(bool arg)
	{
		if (arg == (VisualLog.Instance != null))
		{
			return;
		}
		if (arg)
		{
			base.gameObject.AddComponent<VisualLog>();
		}
		else
		{
			VisualLog.Disable();
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00033AEF File Offset: 0x00031CEF
	private bool LogCallbackHookGetter()
	{
		return LogCallbackHandler.Instance != null;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00033AFC File Offset: 0x00031CFC
	private void LogCallbackHookSetter(bool arg)
	{
		if (LogCallbackHandler.Instance == null)
		{
			LogCallbackHandler.Instance = new LogCallbackHandler();
		}
		else
		{
			LogCallbackHandler.Instance.RemoveHandler();
		}
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00033B21 File Offset: 0x00031D21
	private void DebugXboxControllerSetter(bool arg)
	{
		if (XboxLiveController.Instance)
		{
			XboxLiveController.Instance.IsDebugEnabled = arg;
		}
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00033B3D File Offset: 0x00031D3D
	private bool DebugXboxControllerGetter()
	{
		return XboxLiveController.Instance && XboxLiveController.Instance.IsDebugEnabled;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00033B5A File Offset: 0x00031D5A
	private void UnloadUnusedSetter(bool arg)
	{
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00033B62 File Offset: 0x00031D62
	private bool UnloadUnusedGetter()
	{
		return true;
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00033B65 File Offset: 0x00031D65
	private bool ShowSoundLogGetter()
	{
		return Sound.IsSoundLogEnabled;
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00033B6C File Offset: 0x00031D6C
	private void ShowSoundLogSetter(bool arg)
	{
		Sound.IsSoundLogEnabled = arg;
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00033B74 File Offset: 0x00031D74
	private bool ShowPinkBoxesGetter()
	{
		return Sound.IsPinkBoxesEnabled;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x00033B7B File Offset: 0x00031D7B
	private void ShowPinkBoxesSetter(bool arg)
	{
		Sound.IsPinkBoxesEnabled = arg;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00033B83 File Offset: 0x00031D83
	private bool DeactivateDarknessGetter()
	{
		return SpiritLightVisualAffectorManager.DeactivateLightMechanics;
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00033B8A File Offset: 0x00031D8A
	private void DeactivateDarknessSetter(bool arg)
	{
		SpiritLightVisualAffectorManager.DeactivateLightMechanics = arg;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00033B92 File Offset: 0x00031D92
	public bool ResetInputLock()
	{
		this.HideDebugMenu();
		GameController.Instance.ResetInputLocks();
		SuspensionManager.ResumeAll();
		return true;
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00033BAC File Offset: 0x00031DAC
	public bool TeleportNightberry()
	{
		if (Items.NightBerry)
		{
			Items.NightBerry.transform.position = Characters.Sein.Position;
			Items.NightBerry.SetToDropMode();
		}
		else
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.NightberryPlaceholder, Characters.Sein.Position, Quaternion.identity) as GameObject;
			InstantiateUtility.Destroy(gameObject);
		}
		return true;
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00033C17 File Offset: 0x00031E17
	private void Initialize()
	{
		this.BuildMenu();
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00033C20 File Offset: 0x00031E20
	public void HandleQuickQuit()
	{
		if (DebugMenuB.DebugControlsEnabled && Core.Input.ChargeJump.IsPressed && Core.Input.Bash.IsPressed && Core.Input.SoulFlame.IsPressed && !TestSetManager.IsPerformingTests)
		{
			try
			{
				InstantLoadScenesController.Instance.LogState();
			}
			catch (Exception ex)
			{
			}
			Application.Quit();
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00033C98 File Offset: 0x00031E98
	public void Update()
	{
		if (CheatsHandler.Instance && !CheatsHandler.Instance.DebugEnabled && !CheatsHandler.DebugAlwaysEnabled)
		{
			return;
		}
		this.HandleQuickQuit();
		if (DebugMenuB.Active)
		{
			if (!this.m_lastDebugMenuActiveState)
			{
				this.Initialize();
			}
			this.m_menuList[(int)this.m_cursorIndex.x][(int)this.m_cursorIndex.y].OnSelectedUpdate();
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00033D1C File Offset: 0x00031F1C
	private void ResetHold()
	{
		this.m_holdRemainingTime = 0.4f;
		this.m_holdDelayDuration = 0.04f;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00033D34 File Offset: 0x00031F34
	public void FixedUpdate()
	{
		if (CheatsHandler.Instance && !CheatsHandler.Instance.DebugEnabled && !CheatsHandler.DebugAlwaysEnabled)
		{
			return;
		}
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		if (MoonInput.GetKeyDown(KeyCode.N))
		{
			this.DisableEnemies();
		}
		if (!DebugMenuB.Active && !Recorder.IsPlaying && !DebugMenu.DashOrGrenadeEnabled)
		{
			if (Core.Input.LeftShoulder.IsPressed && DebugMenuB.DebugControlsEnabled)
			{
				Time.timeScale = this.FastForwardTimeScale;
			}
			else if (Core.Input.LeftShoulder.WasPressed)
			{
				Time.timeScale = 1f;
			}
		}
		if (DebugMenuB.Active)
		{
			if (this.m_showGumoSequences)
			{
				if (Core.Input.SoulFlame.OnPressed)
				{
					this.m_showGumoSequences = false;
				}
				if (Core.Input.Down.OnPressed)
				{
					this.m_gumoSequencesCursorIndex.y = this.m_gumoSequencesCursorIndex.y + 1f;
				}
				if (Core.Input.Up.OnPressed)
				{
					this.m_gumoSequencesCursorIndex.y = this.m_gumoSequencesCursorIndex.y - 1f;
				}
				if (Core.Input.Left.OnPressed)
				{
					this.m_gumoSequencesCursorIndex.x = this.m_gumoSequencesCursorIndex.x - 1f;
				}
				if (Core.Input.Right.OnPressed)
				{
					this.m_gumoSequencesCursorIndex.x = this.m_gumoSequencesCursorIndex.x + 1f;
				}
				if (this.m_gumoSequencesCursorIndex.x == -1f)
				{
					this.m_gumoSequencesCursorIndex.x = (float)(this.m_gumoSequencesMenuList.Count - 1);
				}
				if (this.m_gumoSequencesCursorIndex.y == -1f)
				{
					this.m_gumoSequencesCursorIndex.y = (float)(this.m_gumoSequencesMenuList[(int)this.m_gumoSequencesCursorIndex.x].Count - 1);
				}
				if (this.m_gumoSequencesCursorIndex.x == (float)this.m_gumoSequencesMenuList.Count)
				{
					this.m_gumoSequencesCursorIndex.x = 0f;
				}
				if (this.m_gumoSequencesCursorIndex.y == (float)this.m_gumoSequencesMenuList[(int)this.m_gumoSequencesCursorIndex.x].Count)
				{
					this.m_gumoSequencesCursorIndex.y = 0f;
				}
				if (this.m_gumoSequencesCursorIndex != this.m_lastGumoSequencesIndex)
				{
					this.m_gumoSequencesMenuList[(int)this.m_gumoSequencesCursorIndex.x][(int)this.m_gumoSequencesCursorIndex.y].OnSelected();
					this.m_lastGumoSequencesIndex = this.m_gumoSequencesCursorIndex;
				}
				this.m_gumoSequencesMenuList[(int)this.m_gumoSequencesCursorIndex.x][(int)this.m_gumoSequencesCursorIndex.y].OnSelectedFixedUpdate();
			}
			else
			{
				if (!this.m_lastDebugMenuActiveState)
				{
					this.Initialize();
				}
				if (Core.Input.Down.OnPressed)
				{
					this.ResetHold();
					this.m_cursorIndex.y = this.m_cursorIndex.y + 1f;
				}
				if (Core.Input.Up.OnPressed)
				{
					this.ResetHold();
					this.m_cursorIndex.y = this.m_cursorIndex.y - 1f;
				}
				if (Core.Input.Left.OnPressed)
				{
					this.ResetHold();
					this.m_cursorIndex.x = this.m_cursorIndex.x - 1f;
				}
				if (Core.Input.Right.OnPressed)
				{
					this.ResetHold();
					this.m_cursorIndex.x = this.m_cursorIndex.x + 1f;
				}
				if (Core.Input.Left.Pressed || Core.Input.Right.Pressed || Core.Input.Up.Pressed || Core.Input.Down.Pressed)
				{
					this.m_holdRemainingTime -= Time.deltaTime;
					if (this.m_holdRemainingTime < 0f)
					{
						this.m_holdRemainingTime = this.m_holdDelayDuration;
						if (Core.Input.Left.Pressed)
						{
							this.m_cursorIndex.x = this.m_cursorIndex.x - 1f;
						}
						if (Core.Input.Right.Pressed)
						{
							this.m_cursorIndex.x = this.m_cursorIndex.x + 1f;
						}
						if (Core.Input.Down.Pressed)
						{
							this.m_cursorIndex.y = this.m_cursorIndex.y + 1f;
						}
						if (Core.Input.Up.Pressed)
						{
							this.m_cursorIndex.y = this.m_cursorIndex.y - 1f;
						}
					}
				}
				if (this.m_cursorIndex.x < 0f)
				{
					this.m_cursorIndex.x = (float)(this.m_menuList.Count - 1);
				}
				if (this.m_cursorIndex.x > (float)(this.m_menuList.Count - 1))
				{
					this.m_cursorIndex.x = 0f;
				}
				if (this.m_cursorIndex.y < 0f)
				{
					this.m_cursorIndex.y = (float)(this.m_menuList[(int)this.m_cursorIndex.x].Count - 1);
				}
				if (Core.Input.Left.OnPressed || Core.Input.Right.OnPressed)
				{
					if (this.m_cursorIndex.y > (float)(this.m_menuList[(int)this.m_cursorIndex.x].Count - 1))
					{
						this.m_cursorIndex.y = (float)(this.m_menuList[(int)this.m_cursorIndex.x].Count - 1);
					}
				}
				else if (this.m_cursorIndex.y > (float)(this.m_menuList[(int)this.m_cursorIndex.x].Count - 1))
				{
					this.m_cursorIndex.y = 0f;
				}
				if (this.m_cursorIndex != this.m_lastIndex)
				{
					this.m_menuList[(int)this.m_cursorIndex.x][(int)this.m_cursorIndex.y].OnSelected();
					this.m_lastIndex = this.m_cursorIndex;
					DebugMenuB.ShouldShowOnlySelectedItem = false;
				}
				this.m_menuList[(int)this.m_cursorIndex.x][(int)this.m_cursorIndex.y].OnSelectedFixedUpdate();
			}
		}
		this.m_lastDebugMenuActiveState = DebugMenuB.Active;
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x000343A8 File Offset: 0x000325A8
	public void OnGUI()
	{
		if (DebugMenuB.Active)
		{
			GUILayout.BeginArea(new Rect((float)(Screen.width - 150), (float)(Screen.height - 50), 150f, 50f));
			GUILayout.Label("BuildID: " + this.BuildID, new GUILayoutOption[0]);
			GUILayout.EndArea();
			if (this.m_showGumoSequences)
			{
				GUILayout.BeginArea(new Rect(this.MenuTopLeftX, this.MenuTopLeftY, this.MenuWidth, this.MenuHeight), GUIContent.none, DebugMenuB.DebugMenuStyle);
				int num = 0;
				foreach (List<IDebugMenuItem> list in this.m_gumoSequencesMenuList)
				{
					int num2 = 0;
					foreach (IDebugMenuItem debugMenuItem in list)
					{
						Vector2 vector = new Vector2(this.HorizontalSpace * (float)num, this.VerticalSpace * (float)num2);
						bool b = new Vector2((float)num, (float)num2) == this.m_gumoSequencesCursorIndex;
						debugMenuItem.Draw(new Rect(vector.x, vector.y, this.HorizontalSpace, this.VerticalSpace), b);
						num2++;
					}
					num++;
				}
				GUILayout.EndArea();
				GUI.Label(new Rect(this.MenuTopLeftX, this.MenuTopLeftY + this.MenuHeight, this.MenuWidth, 30f), this.m_gumoSequencesMenuList[(int)this.m_gumoSequencesCursorIndex.x][(int)this.m_gumoSequencesCursorIndex.y].HelpText, DebugMenuB.DebugMenuStyle);
			}
			else
			{
				if (this.m_menuList.Count == 0)
				{
					return;
				}
				if (!DebugMenuB.ShouldShowOnlySelectedItem)
				{
					GUILayout.BeginArea(new Rect(this.MenuTopLeftX, this.MenuTopLeftY, this.MenuWidth, this.MenuHeight), GUIContent.none, DebugMenuB.DebugMenuStyle);
				}
				else
				{
					GUILayout.BeginArea(new Rect(this.MenuTopLeftX, this.MenuTopLeftY, this.MenuWidth, this.MenuHeight), GUIContent.none);
				}
				int num3 = 0;
				foreach (List<IDebugMenuItem> list2 in this.m_menuList)
				{
					int num4 = 0;
					foreach (IDebugMenuItem debugMenuItem2 in list2)
					{
						Vector2 vector2 = new Vector2((float)this.GetColPosition(num3), this.VerticalSpace * (float)num4);
						bool flag = new Vector2((float)num3, (float)num4) == this.m_cursorIndex;
						if (!DebugMenuB.ShouldShowOnlySelectedItem || flag)
						{
							debugMenuItem2.Draw(new Rect(vector2.x, vector2.y, (float)this.ColumnsWidth[num3], this.VerticalSpace), flag);
						}
						num4++;
					}
					num3++;
				}
				GUILayout.EndArea();
				if (!DebugMenuB.ShouldShowOnlySelectedItem)
				{
					GUI.Label(new Rect(this.MenuTopLeftX, this.MenuTopLeftY + this.MenuHeight, this.MenuWidth, 30f), this.m_menuList[(int)this.m_cursorIndex.x][(int)this.m_cursorIndex.y].HelpText, DebugMenuB.DebugMenuStyle);
				}
			}
		}
		else if (this.m_showSceneFrameworkDebug)
		{
			Scenes.Manager.DrawScenesManagerDebugData();
		}
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00034794 File Offset: 0x00032994
	private int GetColPosition(int index)
	{
		int num = 0;
		for (int i = 0; i < index; i++)
		{
			num += this.ColumnsWidth[i];
		}
		return num;
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x000347C5 File Offset: 0x000329C5
	private bool CreateCheckpoint()
	{
		this.HideDebugMenu();
		GameController.Instance.CreateCheckpoint();
		return true;
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x000347D8 File Offset: 0x000329D8
	private bool RestoreCheckpoint()
	{
		GameController.Instance.RestoreCheckpoint(null);
		return true;
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x000347E8 File Offset: 0x000329E8
	private bool GoToScene()
	{
		base.StartCoroutine(this.GoToScene(this.m_menuList[(int)this.m_cursorIndex.x][(int)this.m_cursorIndex.y].Text));
		return true;
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00034830 File Offset: 0x00032A30
	private bool FaderBAction()
	{
		UI.Fader.Fade(0.5f, 0.5f, 0.5f, null, null);
		return true;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00034850 File Offset: 0x00032A50
	public IEnumerator GoToScene(string sceneName)
	{
		RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation(sceneName);
		Scenes.Manager.AutoLoadingUnloading = false;
		Scenes.Manager.UnloadAllScenes();
		Scenes.Manager.DestroyManager.DestroyAll();
		SuspensionManager.SuspendAll();
		while (Scenes.Manager.ResourcesNeedUnloading)
		{
			yield return new WaitForFixedUpdate();
		}
		SuspensionManager.ResumeAll();
		GameController.Instance.ResetStateForDebugMenuGoToScene();
		GoToSceneController.Instance.GoToScene(sceneInformation, null, true);
		DebugMenuB.ToggleDebugMenu();
		yield break;
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00034874 File Offset: 0x00032A74
	private bool ResetNightBerryPosition()
	{
		if (Items.NightBerry == null)
		{
			return false;
		}
		Items.NightBerry.transform.position = Characters.Sein.PlatformBehaviour.PlatformMovement.Position;
		return true;
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x000348B7 File Offset: 0x00032AB7
	private bool Quit()
	{
		Application.Quit();
		return true;
	}

	// Token: 0x0400093A RID: 2362
	public const float HOLD_DELAY = 0.4f;

	// Token: 0x0400093B RID: 2363
	public const float HOLD_FAST_DELAY = 0.04f;

	// Token: 0x0400093C RID: 2364
	public static DebugMenuB Instance = null;

	// Token: 0x0400093D RID: 2365
	private readonly List<WorldEvents> m_worldEvents = new List<WorldEvents>();

	// Token: 0x0400093E RID: 2366
	private readonly List<List<IDebugMenuItem>> m_menuList = new List<List<IDebugMenuItem>>();

	// Token: 0x0400093F RID: 2367
	private readonly List<List<IDebugMenuItem>> m_gumoSequencesMenuList = new List<List<IDebugMenuItem>>();

	// Token: 0x04000940 RID: 2368
	public List<SceneMetaData> ImportantLevels = new List<SceneMetaData>();

	// Token: 0x04000941 RID: 2369
	public List<string> ImportantLevelsNames = new List<string>();

	// Token: 0x04000942 RID: 2370
	public GUISkin Skin;

	// Token: 0x04000943 RID: 2371
	public static GUIStyle SelectedStyle;

	// Token: 0x04000944 RID: 2372
	public static GUIStyle Style;

	// Token: 0x04000945 RID: 2373
	public static GUIStyle PressedStyle;

	// Token: 0x04000946 RID: 2374
	public static GUIStyle DebugMenuStyle;

	// Token: 0x04000947 RID: 2375
	public static GUIStyle StyleEnabled;

	// Token: 0x04000948 RID: 2376
	public static GUIStyle StyleDisabled;

	// Token: 0x04000949 RID: 2377
	public SceneMetaData TestScene;

	// Token: 0x0400094A RID: 2378
	public static bool UnlockAllCutscenes = false;

	// Token: 0x0400094B RID: 2379
	public static bool MuteMusic = false;

	// Token: 0x0400094C RID: 2380
	public static bool MuteAmbience = false;

	// Token: 0x0400094D RID: 2381
	public static bool MuteSoundEffects = false;

	// Token: 0x0400094E RID: 2382
	public float VerticalSpace = 25f;

	// Token: 0x0400094F RID: 2383
	public float HorizontalSpace = 150f;

	// Token: 0x04000950 RID: 2384
	private Vector2 m_cursorIndex;

	// Token: 0x04000951 RID: 2385
	private Vector2 m_gumoSequencesCursorIndex;

	// Token: 0x04000952 RID: 2386
	public MessageProvider ReplayGotResetMessageProvider;

	// Token: 0x04000953 RID: 2387
	public int BuildID;

	// Token: 0x04000954 RID: 2388
	public float MenuTopLeftX = 200f;

	// Token: 0x04000955 RID: 2389
	public float MenuTopLeftY = 70f;

	// Token: 0x04000956 RID: 2390
	public float MenuWidth = 900f;

	// Token: 0x04000957 RID: 2391
	public float MenuHeight = 600f;

	// Token: 0x04000958 RID: 2392
	private bool m_showSceneFrameworkDebug;

	// Token: 0x04000959 RID: 2393
	public static bool ShowAchievementHint = false;

	// Token: 0x0400095A RID: 2394
	private bool m_showGumoSequences;

	// Token: 0x0400095B RID: 2395
	private bool m_superSlowMotion;

	// Token: 0x0400095C RID: 2396
	public List<int> ColumnsWidth = new List<int>();

	// Token: 0x0400095D RID: 2397
	public List<GoToSequenceData> GumoSequence = new List<GoToSequenceData>();

	// Token: 0x0400095E RID: 2398
	public static bool IsFullyInstalledDebugOverride = false;

	// Token: 0x0400095F RID: 2399
	private static readonly HashSet<ISuspendable> SuspendablesToIgnoreForGameplay = new HashSet<ISuspendable>();

	// Token: 0x04000960 RID: 2400
	private long value = 2L;

	// Token: 0x04000961 RID: 2401
	private List<GameObject> m_particleSystems;

	// Token: 0x04000962 RID: 2402
	private GameObject[] m_art;

	// Token: 0x04000963 RID: 2403
	private GameObject[] m_enemies;

	// Token: 0x04000964 RID: 2404
	private bool m_highFPSPhysics;

	// Token: 0x04000965 RID: 2405
	public GameObject NightberryPlaceholder;

	// Token: 0x04000966 RID: 2406
	private bool m_lastDebugMenuActiveState;

	// Token: 0x04000967 RID: 2407
	private Vector2 m_lastIndex;

	// Token: 0x04000968 RID: 2408
	private Vector2 m_lastGumoSequencesIndex;

	// Token: 0x04000969 RID: 2409
	public static bool Active = false;

	// Token: 0x0400096A RID: 2410
	public static bool DebugControlsEnabled = false;

	// Token: 0x0400096B RID: 2411
	public float FastForwardTimeScale = 3f;

	// Token: 0x0400096C RID: 2412
	private float m_holdDelayDuration;

	// Token: 0x0400096D RID: 2413
	private float m_holdRemainingTime;

	// Token: 0x0400096E RID: 2414
	public static bool ShouldShowOnlySelectedItem = false;
}
