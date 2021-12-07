using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200069C RID: 1692
public class AmbienceListener : MonoBehaviour
{
	// Token: 0x060028F7 RID: 10487 RVA: 0x000B1288 File Offset: 0x000AF488
	public void FixedUpdate()
	{
		Vector3 cameraPositionForSampling = UI.Cameras.Current.CameraPositionForSampling;
		if (Scenes.Manager)
		{
			SceneRoot sceneRoot = Scenes.Manager.FindLoadedSceneRootFromPosition(cameraPositionForSampling);
			if (sceneRoot && sceneRoot != this.m_lastSceneRoot)
			{
				if (this.m_lastAmbienceLayer != null)
				{
					Ambience.RemoveAmbienceLayer(this.m_lastAmbienceLayer);
					this.m_lastAmbienceLayer = null;
				}
				SceneSettingsComponent sceneSettings = sceneRoot.SceneSettings;
				if (sceneSettings && sceneSettings.DefaultAmbience)
				{
					this.m_lastAmbienceLayer = new Ambience.Layer(sceneSettings.DefaultAmbience, AmbienceListener.FadeInDuration, AmbienceListener.FadeOutDuration, 0);
					Ambience.AddAmbienceLayer(this.m_lastAmbienceLayer);
				}
				this.m_lastSceneRoot = sceneRoot;
			}
		}
		for (int i = 0; i < AmbienceZone.All.Count; i++)
		{
			AmbienceZone ambienceZone = AmbienceZone.All[i];
			if (ambienceZone.Bounds.Contains(cameraPositionForSampling))
			{
				if (!ambienceZone.Activated)
				{
					ambienceZone.ActivateAmbienceZone();
				}
			}
			else if (ambienceZone.Activated)
			{
				ambienceZone.DeactiveAmbienceZone();
			}
		}
	}

	// Token: 0x04002485 RID: 9349
	private SceneRoot m_lastSceneRoot;

	// Token: 0x04002486 RID: 9350
	private Ambience.Layer m_lastAmbienceLayer;

	// Token: 0x04002487 RID: 9351
	public static float FadeInDuration = 2f;

	// Token: 0x04002488 RID: 9352
	public static float FadeOutDuration = 2f;
}
