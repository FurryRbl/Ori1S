using System;
using Core;
using UnityEngine;

// Token: 0x02000948 RID: 2376
public class SceneNameDisplay : MonoBehaviour
{
	// Token: 0x06003466 RID: 13414 RVA: 0x000DC25C File Offset: 0x000DA45C
	public void FixedUpdate()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.m_guiText == null)
		{
			this.m_guiText = base.GetComponent<GUIText>();
		}
		if (this.m_guiText == null || Scenes.Manager == null || Scenes.Manager.CurrentScene == null)
		{
			return;
		}
		string scene = Scenes.Manager.CurrentScene.Scene;
		if (this.m_guiText.text != scene)
		{
			this.m_guiText.text = scene;
		}
	}

	// Token: 0x04002F46 RID: 12102
	private GUIText m_guiText;
}
