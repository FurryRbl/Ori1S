using System;
using Core;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
[ExecuteInEditMode]
public class SceneSettingsTransitionZone : MonoBehaviour
{
	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00076A84 File Offset: 0x00074C84
	public SceneSettings FromSettings
	{
		get
		{
			return this.m_fromSettings;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00076A8C File Offset: 0x00074C8C
	public SceneSettings ToSettings
	{
		get
		{
			return this.m_toSettings;
		}
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00076A94 File Offset: 0x00074C94
	public bool IsReady
	{
		get
		{
			return this.m_fromSettings != null && this.m_toSettings != null;
		}
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x00076AB0 File Offset: 0x00074CB0
	public float CalculateTweenValue(Vector2 postion)
	{
		switch (this.m_direction)
		{
		case SceneSettingsTransitionZone.Direction.TransitionLeft:
			return Mathf.InverseLerp(this.m_boundingRect.xMin, this.m_boundingRect.xMax, postion.x);
		case SceneSettingsTransitionZone.Direction.TransitionRight:
			return Mathf.InverseLerp(this.m_boundingRect.xMax, this.m_boundingRect.xMin, postion.x);
		case SceneSettingsTransitionZone.Direction.TransitionUp:
			return Mathf.InverseLerp(this.m_boundingRect.yMax, this.m_boundingRect.yMin, postion.y);
		case SceneSettingsTransitionZone.Direction.TransitionDown:
			return Mathf.InverseLerp(this.m_boundingRect.yMin, this.m_boundingRect.yMax, postion.y);
		default:
			return 0f;
		}
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x00076B70 File Offset: 0x00074D70
	public void UpdateSettings()
	{
		SceneRoot sceneRoot = SceneRoot.FindFromTransform(base.transform);
		if (sceneRoot == null)
		{
			return;
		}
		if (Application.isPlaying)
		{
			for (int i = 0; i < Scenes.Manager.ActiveScenes.Count; i++)
			{
				SceneManagerScene sceneManagerScene = Scenes.Manager.ActiveScenes[i];
				if (!(sceneManagerScene.SceneRoot == sceneRoot))
				{
					if (!sceneManagerScene.MetaData.DependantScene)
					{
						if (sceneManagerScene.IsLoadingComplete)
						{
							for (int j = 0; j < sceneManagerScene.MetaData.SceneBoundaries.Count; j++)
							{
								Rect rect = sceneManagerScene.MetaData.SceneBoundaries[j];
								if (rect.Overlaps(this.m_boundingRect))
								{
									if (rect.Contains(new Vector2(this.m_boundingRect.xMax, this.m_boundingRect.center.y)))
									{
										this.m_direction = SceneSettingsTransitionZone.Direction.TransitionRight;
									}
									else if (rect.Contains(new Vector2(this.m_boundingRect.xMin, this.m_boundingRect.center.y)))
									{
										this.m_direction = SceneSettingsTransitionZone.Direction.TransitionLeft;
									}
									else if (rect.Contains(new Vector2(this.m_boundingRect.center.x, this.m_boundingRect.yMax)))
									{
										this.m_direction = SceneSettingsTransitionZone.Direction.TransitionUp;
									}
									else
									{
										if (!rect.Contains(new Vector2(this.m_boundingRect.center.x, this.m_boundingRect.yMin)))
										{
											goto IL_1BF;
										}
										this.m_direction = SceneSettingsTransitionZone.Direction.TransitionDown;
									}
									this.m_toSettings = sceneRoot.SceneSettings.GetSettings;
									this.m_fromSettings = sceneManagerScene.SceneRoot.SceneSettings.GetSettings;
									return;
								}
								IL_1BF:;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x00076D70 File Offset: 0x00074F70
	public void OnEnable()
	{
		this.m_boundingRect = new Rect
		{
			width = Mathf.Abs(base.transform.lossyScale.x) + 0.02f,
			height = Mathf.Abs(base.transform.lossyScale.y) + 0.02f,
			center = base.transform.position
		};
		SceneSettingsTransitionZone.All.Add(this);
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x00076DF8 File Offset: 0x00074FF8
	public void OnDisable()
	{
		SceneSettingsTransitionZone.All.Remove(this);
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x00076E05 File Offset: 0x00075005
	public bool IsInside(Vector3 position)
	{
		return this.m_boundingRect.Contains(position);
	}

	// Token: 0x040017F8 RID: 6136
	public static AllContainer<SceneSettingsTransitionZone> All = new AllContainer<SceneSettingsTransitionZone>();

	// Token: 0x040017F9 RID: 6137
	private Rect m_boundingRect;

	// Token: 0x040017FA RID: 6138
	private SceneSettings m_fromSettings;

	// Token: 0x040017FB RID: 6139
	private SceneSettings m_toSettings;

	// Token: 0x040017FC RID: 6140
	private SceneSettingsTransitionZone.Direction m_direction;

	// Token: 0x020003F8 RID: 1016
	public enum Direction
	{
		// Token: 0x040017FE RID: 6142
		TransitionLeft,
		// Token: 0x040017FF RID: 6143
		TransitionRight,
		// Token: 0x04001800 RID: 6144
		TransitionUp,
		// Token: 0x04001801 RID: 6145
		TransitionDown
	}
}
