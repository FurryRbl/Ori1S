using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020006A7 RID: 1703
public class MusicListener : MonoBehaviour
{
	// Token: 0x06002929 RID: 10537 RVA: 0x000B1BB4 File Offset: 0x000AFDB4
	public void FixedUpdate()
	{
		Vector3 cameraPositionForSampling = UI.Cameras.Current.CameraPositionForSampling;
		if (Scenes.Manager)
		{
			SceneRoot sceneRoot = Scenes.Manager.FindLoadedSceneRootFromPosition(cameraPositionForSampling);
			if (sceneRoot != null && sceneRoot != this.m_lastSceneRoot)
			{
				if (this.m_lastMusicLayer != null)
				{
					Music.RemoveMusicLayer(this.m_lastMusicLayer);
					this.m_lastMusicLayer = null;
				}
				SceneSettingsComponent sceneSettings = sceneRoot.SceneSettings;
				if (sceneSettings && sceneSettings.DefaultMusic)
				{
					this.m_lastMusicLayer = new Music.Layer(sceneSettings.DefaultMusic, MusicListener.FadeInDuration, MusicListener.FadeOutDuration);
					Music.AddMusicLayer(this.m_lastMusicLayer);
				}
				this.m_lastSceneRoot = sceneRoot;
			}
		}
		for (int i = 0; i < MusicZone.All.Count; i++)
		{
			MusicZone musicZone = MusicZone.All[i];
			if (musicZone.Bounds.Contains(cameraPositionForSampling))
			{
				if (!musicZone.Activated)
				{
					musicZone.ActivateMusicZone();
				}
			}
			else if (musicZone.Activated)
			{
				musicZone.DeactiveMusicZone();
			}
		}
	}

	// Token: 0x040024B1 RID: 9393
	private SceneRoot m_lastSceneRoot;

	// Token: 0x040024B2 RID: 9394
	private Music.Layer m_lastMusicLayer;

	// Token: 0x040024B3 RID: 9395
	public static float FadeInDuration = 2f;

	// Token: 0x040024B4 RID: 9396
	public static float FadeOutDuration = 2f;
}
