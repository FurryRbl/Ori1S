using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class ColorVariationManager : MonoBehaviour
{
	// Token: 0x0600088D RID: 2189 RVA: 0x00024CE5 File Offset: 0x00022EE5
	public void Awake()
	{
		ColorVariationManager.Instance = this;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00024CED File Offset: 0x00022EED
	public void OnDestroy()
	{
		ColorVariationManager.Instance = null;
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00024CF5 File Offset: 0x00022EF5
	public void Register(ColorVariation colorVariation)
	{
		this.m_colorVariations.Add(colorVariation);
		this.m_currentSceneMetaDataGUID = MoonGuid.Empty;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00024D0E File Offset: 0x00022F0E
	public void Unregister(ColorVariation colorVariation)
	{
		this.m_colorVariations.Remove(colorVariation);
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00024D1C File Offset: 0x00022F1C
	public void FixedUpdate()
	{
		if (!UI.Cameras.Current)
		{
			return;
		}
		Vector3 cameraPositionForSampling = UI.Cameras.Current.CameraPositionForSampling;
		if (Scenes.Manager)
		{
			RuntimeSceneMetaData currentScene = Scenes.Manager.CurrentScene;
			if (currentScene == null)
			{
				return;
			}
			MoonGuid sceneMoonGuid = currentScene.SceneMoonGuid;
			if (this.m_currentSceneMetaDataGUID != sceneMoonGuid)
			{
				this.m_currentSceneMetaDataGUID = sceneMoonGuid;
				for (int i = 0; i < this.m_colorVariations.Count; i++)
				{
					this.m_colorVariations[i].Hide();
				}
				for (int j = 0; j < this.m_colorVariations.Count; j++)
				{
					if (this.m_colorVariations[j].MetaDataGUID == this.m_currentSceneMetaDataGUID)
					{
						this.m_colorVariations[j].Show();
						return;
					}
				}
				RuntimeSceneMetaData runtimeSceneMetaData = Scenes.Manager.FindRuntimeSceneMetaData(currentScene.SceneMoonGuid);
				for (int k = 0; k < this.m_colorVariations.Count; k++)
				{
					for (int l = 0; l < runtimeSceneMetaData.IncludedScenes.Count; l++)
					{
						if (runtimeSceneMetaData.IncludedScenes[l] == this.m_colorVariations[k].MetaDataGUID)
						{
							this.m_colorVariations[k].Show();
							return;
						}
					}
				}
			}
		}
	}

	// Token: 0x040006D7 RID: 1751
	public static ColorVariationManager Instance;

	// Token: 0x040006D8 RID: 1752
	private readonly AllContainer<ColorVariation> m_colorVariations = new AllContainer<ColorVariation>();

	// Token: 0x040006D9 RID: 1753
	private MoonGuid m_currentSceneMetaDataGUID;
}
