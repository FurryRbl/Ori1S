using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000731 RID: 1841
public class MixerManager : MonoBehaviour
{
	// Token: 0x06002B47 RID: 11079 RVA: 0x000B97EB File Offset: 0x000B79EB
	public void RegisterSnapshotZone(MixerSnapshotZone mixerSnapshotZone)
	{
		this.m_snapshotZones.Add(mixerSnapshotZone);
	}

	// Token: 0x06002B48 RID: 11080 RVA: 0x000B97F9 File Offset: 0x000B79F9
	public void DeregisterSnapshotZone(MixerSnapshotZone mixerSnapshotZone)
	{
		this.m_snapshotZones.Remove(mixerSnapshotZone);
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x000B9808 File Offset: 0x000B7A08
	public void Awake()
	{
		MixerManager.s_manager = this;
	}

	// Token: 0x170006E7 RID: 1767
	// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000B9810 File Offset: 0x000B7A10
	public static MixerManager Instance
	{
		get
		{
			return MixerManager.s_manager;
		}
	}

	// Token: 0x06002B4B RID: 11083 RVA: 0x000B9817 File Offset: 0x000B7A17
	public void RegisterActiveSnapshot(MixerSnapshot snapshot)
	{
		if (!this.m_currentlyActiveSnapshots.Contains(snapshot))
		{
			this.m_currentlyActiveSnapshots.Add(snapshot);
		}
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x000B9838 File Offset: 0x000B7A38
	public void FixedUpdate()
	{
		bool flag = UI.MainMenuVisible || ResumeGameController.IsGameSuspended;
		if (flag != this.m_wasInUI)
		{
			if (flag)
			{
				this.UISnapshot.FadeIn();
			}
			else
			{
				this.UISnapshot.FadeOut();
			}
		}
		this.m_wasInUI = flag;
		this.UpdateMixerSnapshotZones();
		this.UpdateMixerSettingsBasedOnActiveSnapshots();
	}

	// Token: 0x06002B4D RID: 11085 RVA: 0x000B9898 File Offset: 0x000B7A98
	private void UpdateMixerSettingsBasedOnActiveSnapshots()
	{
		this.m_settings.Reset();
		for (int i = 0; i < this.m_currentlyActiveSnapshots.Count; i++)
		{
			MixerSnapshot mixerSnapshot = this.m_currentlyActiveSnapshots[i];
			mixerSnapshot.UpdateMixerSnapshotState(Time.fixedDeltaTime);
			this.m_settings.MultiplyBlendWith(mixerSnapshot.SnapshotSettings, mixerSnapshot.Weight);
		}
		this.m_settings.MultiplyBlendWith(this.ModulatingSnapshot.SnapshotSettings, 1f);
		this.m_currentlyActiveSnapshots.RemoveAll(MixerManager.CachedIsSnapshotInactivePredicate);
		this.m_settings.Music = this.m_settings.Music * Mathf.Log10(GameSettings.Instance.MusicVolume * 9f + 1f);
		this.m_settings.SoundEffects = this.m_settings.SoundEffects * Mathf.Log10(GameSettings.Instance.SoundEffectsVolume * 9f + 1f);
		AudioMixer masterMixer = MixerManager.GetMasterMixer();
		this.m_settings.ApplyGroupSettingsToMixer(masterMixer);
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x000B9994 File Offset: 0x000B7B94
	private void UpdateMixerSnapshotZones()
	{
		Vector3 cameraPositionForSampling = UI.Cameras.Current.CameraPositionForSampling;
		SceneRoot sceneRoot = Scenes.Manager.FindLoadedSceneRootFromPosition(cameraPositionForSampling);
		MixerSnapshot mixerSnapshot = null;
		if (sceneRoot != null)
		{
			mixerSnapshot = sceneRoot.SceneSettings.DefaultMixerSnapshot;
		}
		if (mixerSnapshot == null)
		{
			mixerSnapshot = this.DefaultSceneSnapshot;
		}
		if (mixerSnapshot != this.m_currentSceneMixerSnapshot)
		{
			if (this.m_currentSceneMixerSnapshot != null)
			{
				this.m_currentSceneMixerSnapshot.FadeOut();
			}
			if (mixerSnapshot != null)
			{
				mixerSnapshot.FadeIn();
			}
		}
		this.m_currentSceneMixerSnapshot = mixerSnapshot;
		for (int i = 0; i < this.m_snapshotZones.Count; i++)
		{
			MixerSnapshotZone mixerSnapshotZone = this.m_snapshotZones[i];
			mixerSnapshotZone.UpdateSnapshotZoneState(mixerSnapshotZone.Bounds.Contains(cameraPositionForSampling));
		}
	}

	// Token: 0x06002B4F RID: 11087 RVA: 0x000B9A6D File Offset: 0x000B7C6D
	public static AudioMixer GetMasterMixer()
	{
		if (MixerManager.s_cachedMasterMixer == null)
		{
			MixerManager.s_cachedMasterMixer = (AudioMixer)Resources.Load("masterMixer", typeof(AudioMixer));
		}
		return MixerManager.s_cachedMasterMixer;
	}

	// Token: 0x06002B50 RID: 11088 RVA: 0x000B9AA4 File Offset: 0x000B7CA4
	public static AudioMixerGroup GetMixerGroup(MixerGroupType group)
	{
		AudioMixerGroup audioMixerGroup;
		if (!MixerManager.s_typeToGroup.TryGetValue(group, out audioMixerGroup))
		{
			if (group != MixerGroupType.Foley)
			{
				if (group != MixerGroupType.Footsteps)
				{
					if (group != MixerGroupType.EnemiesAttack)
					{
						if (group != MixerGroupType.EnemiesFoley)
						{
							if (group != MixerGroupType.AmbienceQuad)
							{
								if (group != MixerGroupType.AmbiencePoint)
								{
									if (group != MixerGroupType.Attacks)
									{
										if (group != MixerGroupType.Destruction)
										{
											if (group != MixerGroupType.UI)
											{
												if (group != MixerGroupType.SpiritTree)
												{
													if (group != MixerGroupType.Sein)
													{
														if (group != MixerGroupType.Doors)
														{
															if (group != MixerGroupType.Cutscenes)
															{
																if (group != MixerGroupType.Props)
																{
																	if (group != MixerGroupType.Collectibles)
																	{
																		if (group != MixerGroupType.MusicStingers)
																		{
																			if (group != MixerGroupType.MusicLoops)
																			{
																				audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("Master")[0];
																			}
																			else
																			{
																				audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("loops")[0];
																			}
																		}
																		else
																		{
																			audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("stingers")[0];
																		}
																	}
																	else
																	{
																		audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("collectibles")[0];
																	}
																}
																else
																{
																	audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("props")[0];
																}
															}
															else
															{
																audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("cutscenes")[0];
															}
														}
														else
														{
															audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("doors")[0];
														}
													}
													else
													{
														audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("sein")[0];
													}
												}
												else
												{
													audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("spiritTree")[0];
												}
											}
											else
											{
												audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("ui")[0];
											}
										}
										else
										{
											audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("destruction")[0];
										}
									}
									else
									{
										audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("attacks")[0];
									}
								}
								else
								{
									audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("ambiencePoint")[0];
								}
							}
							else
							{
								audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("ambienceQuad")[0];
							}
						}
						else
						{
							audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("enemiesFoley")[0];
						}
					}
					else
					{
						audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("enemiesAttack")[0];
					}
				}
				else
				{
					audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("footsteps")[0];
				}
			}
			else
			{
				audioMixerGroup = MixerManager.GetMasterMixer().FindMatchingGroups("foley")[0];
			}
			MixerManager.s_typeToGroup.Add(group, audioMixerGroup);
		}
		return audioMixerGroup;
	}

	// Token: 0x06002B51 RID: 11089 RVA: 0x000B9D2A File Offset: 0x000B7F2A
	public static void WarmUpResource()
	{
		MixerManager.GetMasterMixer();
	}

	// Token: 0x04002704 RID: 9988
	public MixerSnapshot DefaultSceneSnapshot;

	// Token: 0x04002705 RID: 9989
	public MixerSnapshot UISnapshot;

	// Token: 0x04002706 RID: 9990
	public MixerSnapshot ModulatingSnapshot;

	// Token: 0x04002707 RID: 9991
	private MixerGroupSettings m_currentMixerGroupSettings;

	// Token: 0x04002708 RID: 9992
	private bool m_wasInUI;

	// Token: 0x04002709 RID: 9993
	private static readonly Predicate<MixerSnapshot> CachedIsSnapshotInactivePredicate = (MixerSnapshot snapshot) => snapshot.State == MixerSnapshot.MixerSnapshotState.Inactive;

	// Token: 0x0400270A RID: 9994
	private static Dictionary<MixerGroupType, AudioMixerGroup> s_typeToGroup = new Dictionary<MixerGroupType, AudioMixerGroup>();

	// Token: 0x0400270B RID: 9995
	private List<MixerSnapshot> m_currentlyActiveSnapshots = new List<MixerSnapshot>(10);

	// Token: 0x0400270C RID: 9996
	private static AudioMixer s_cachedMasterMixer = null;

	// Token: 0x0400270D RID: 9997
	private static MixerManager s_manager;

	// Token: 0x0400270E RID: 9998
	private MixerGroupSettings m_settings = default(MixerGroupSettings);

	// Token: 0x0400270F RID: 9999
	private List<MixerSnapshotZone> m_snapshotZones = new List<MixerSnapshotZone>(5);

	// Token: 0x04002710 RID: 10000
	private MixerSnapshot m_currentSceneMixerSnapshot;
}
