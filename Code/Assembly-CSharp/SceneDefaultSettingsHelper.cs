using System;
using Core;
using UnityEngine;

// Token: 0x020003E4 RID: 996
public class SceneDefaultSettingsHelper
{
	// Token: 0x06001B31 RID: 6961 RVA: 0x00074FB2 File Offset: 0x000731B2
	public SceneDefaultSettingsHelper(float duration)
	{
		this.m_tweenDuration = duration;
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x00074FCC File Offset: 0x000731CC
	private SceneRoot SceneRootFromPosition(Vector3 position)
	{
		if (Application.isPlaying && Scenes.Manager)
		{
			return Scenes.Manager.FindLoadedSceneRootFromPosition(position);
		}
		return null;
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x00075000 File Offset: 0x00073200
	private SceneRoot GetScene(Vector3 position)
	{
		if (Application.isPlaying)
		{
			foreach (SceneManagerScene sceneManagerScene in Scenes.Manager.ActiveScenes)
			{
				if (sceneManagerScene.IsLoadingComplete)
				{
					if (!sceneManagerScene.MetaData.DependantScene)
					{
						if (sceneManagerScene.MetaData.IsInsideSceneBounds(position))
						{
							return sceneManagerScene.SceneRoot;
						}
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x000750A4 File Offset: 0x000732A4
	private bool CalculateTweenValueBasedOnPaddingZone(Vector3 position, out float value, out SceneRoot otherScene)
	{
		if (Application.isPlaying)
		{
			SceneManagerScene sceneManagerScene = null;
			foreach (SceneManagerScene sceneManagerScene2 in Scenes.Manager.ActiveScenes)
			{
				if (sceneManagerScene2.CurrentState == SceneManagerScene.State.Loaded || sceneManagerScene2.CurrentState == SceneManagerScene.State.Disabled)
				{
					if (!sceneManagerScene2.MetaData.DependantScene)
					{
						if (sceneManagerScene2.MetaData.IsInsideSceneBounds(position))
						{
							sceneManagerScene = sceneManagerScene2;
						}
					}
				}
			}
			if (sceneManagerScene != null)
			{
				foreach (SceneManagerScene sceneManagerScene3 in Scenes.Manager.ActiveScenes)
				{
					if (!sceneManagerScene3.MetaData.DependantScene)
					{
						if (sceneManagerScene3.CurrentState == SceneManagerScene.State.Loaded || sceneManagerScene3.CurrentState == SceneManagerScene.State.Disabled)
						{
							int count = sceneManagerScene3.MetaData.ScenePaddingBoundaries.Count;
							for (int i = 0; i < count; i++)
							{
								Rect rect = sceneManagerScene3.MetaData.ScenePaddingBoundaries[i];
								if (rect.Contains(position))
								{
									int count2 = sceneManagerScene.MetaData.SceneBoundaries.Count;
									for (int j = 0; j < count2; j++)
									{
										Rect rect2 = sceneManagerScene.MetaData.SceneBoundaries[j];
										if (Mathf.Abs(rect2.xMin - rect.xMin) <= 1f)
										{
											otherScene = sceneManagerScene3.SceneRoot;
											value = 1f - Mathf.InverseLerp(rect.xMax, rect2.xMin, position.x) * 0.5f;
											return true;
										}
										if (Mathf.Abs(rect2.xMax - rect.xMax) <= 1f)
										{
											otherScene = sceneManagerScene3.SceneRoot;
											value = 1f - Mathf.InverseLerp(rect.xMin, rect2.xMax, position.x) * 0.5f;
											return true;
										}
										if (Mathf.Abs(rect2.yMin - rect.yMin) <= 1f)
										{
											otherScene = sceneManagerScene3.SceneRoot;
											value = 1f - Mathf.InverseLerp(rect.yMax, rect2.yMin, position.y) * 0.5f;
											return true;
										}
										if (Mathf.Abs(rect2.yMax - rect.yMax) <= 1f)
										{
											otherScene = sceneManagerScene3.SceneRoot;
											value = 1f - Mathf.InverseLerp(rect.yMin, rect2.yMax, position.y) * 0.5f;
											return true;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		otherScene = null;
		value = 1f;
		return false;
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x000753CC File Offset: 0x000735CC
	public void Advance(Vector3 position, float timeDelta)
	{
		SceneRoot sceneRoot = this.SceneRootFromPosition(position);
		SceneSettings sceneSettings = this.FromSettings;
		SceneSettings sceneSettings2 = this.ToSettings;
		float num = 1f;
		if (sceneRoot && sceneRoot.SceneSettings && sceneRoot.SceneSettings.GetSettings != this.ToSettings)
		{
			sceneSettings = this.ToSettings;
			sceneSettings2 = sceneRoot.SceneSettings.GetSettings;
		}
		for (int i = 0; i < SceneSettingsTransitionZone.All.Count; i++)
		{
			SceneSettingsTransitionZone sceneSettingsTransitionZone = SceneSettingsTransitionZone.All[i];
			if (sceneSettingsTransitionZone.IsInside(position))
			{
				if (!sceneSettingsTransitionZone.IsReady)
				{
					sceneSettingsTransitionZone.UpdateSettings();
				}
				if (sceneSettingsTransitionZone.IsReady)
				{
					num = sceneSettingsTransitionZone.CalculateTweenValue(position);
					sceneSettings = sceneSettingsTransitionZone.FromSettings;
					sceneSettings2 = sceneSettingsTransitionZone.ToSettings;
				}
			}
		}
		if (sceneSettings == this.ToSettings)
		{
			this.TweenTime = 1f - this.TweenTime;
		}
		else if (sceneSettings2 == this.FromSettings)
		{
			this.TweenTime = 1f - this.TweenTime;
		}
		this.FromSettings = sceneSettings;
		this.ToSettings = sceneSettings2;
		if (this.m_tweenDuration == 0f)
		{
			this.TweenTime = num;
		}
		else
		{
			this.TweenTime = Mathf.MoveTowards(this.TweenTime, num, timeDelta / this.m_tweenDuration);
		}
		if (this.HasFromSettings && !this.HasToSettings)
		{
			this.TweenTime = 0f;
		}
		if (!this.HasFromSettings && this.HasToSettings)
		{
			this.TweenTime = 1f;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06001B36 RID: 6966 RVA: 0x00075577 File Offset: 0x00073777
	// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0007557F File Offset: 0x0007377F
	public SceneSettings FromSettings
	{
		get
		{
			return this.m_fromSettings;
		}
		set
		{
			this.m_fromSettings = value;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00075588 File Offset: 0x00073788
	// (set) Token: 0x06001B39 RID: 6969 RVA: 0x00075590 File Offset: 0x00073790
	public SceneSettings ToSettings
	{
		get
		{
			return this.m_toSettings;
		}
		set
		{
			this.m_toSettings = value;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00075599 File Offset: 0x00073799
	public bool HasFromSettings
	{
		get
		{
			return this.m_fromSettings != null;
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000755A7 File Offset: 0x000737A7
	public bool HasToSettings
	{
		get
		{
			return this.m_toSettings != null;
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x06001B3C RID: 6972 RVA: 0x000755B5 File Offset: 0x000737B5
	// (set) Token: 0x06001B3D RID: 6973 RVA: 0x000755BD File Offset: 0x000737BD
	public float TweenTime { get; private set; }

	// Token: 0x040017AE RID: 6062
	private SceneSettings m_fromSettings;

	// Token: 0x040017AF RID: 6063
	private SceneSettings m_toSettings;

	// Token: 0x040017B0 RID: 6064
	private float m_tweenDuration = 1f;
}
