using System;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class SoundMessage : MonoBehaviour
{
	// Token: 0x060010E2 RID: 4322 RVA: 0x0004D304 File Offset: 0x0004B504
	private void OnGUI()
	{
		SoundMessage.GUIDrawRect(new Rect(200f, 0f, 800f, 300f), new Color(255f, 0f, 255f));
		GUI.Label(new Rect(300f, 70f, 650f, 250f), this.text);
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0004D368 File Offset: 0x0004B568
	public static void GUIDrawRect(Rect position, Color color)
	{
		if (SoundMessage._staticRectTexture == null)
		{
			SoundMessage._staticRectTexture = new Texture2D(1, 1);
		}
		if (SoundMessage._staticRectStyle == null)
		{
			SoundMessage._staticRectStyle = new GUIStyle();
		}
		SoundMessage._staticRectTexture.SetPixel(0, 0, color);
		SoundMessage._staticRectTexture.Apply();
		SoundMessage._staticRectStyle.normal.background = SoundMessage._staticRectTexture;
		GUI.Box(position, GUIContent.none, SoundMessage._staticRectStyle);
	}

	// Token: 0x04000EA7 RID: 3751
	public string text;

	// Token: 0x04000EA8 RID: 3752
	private static Texture2D _staticRectTexture;

	// Token: 0x04000EA9 RID: 3753
	private static GUIStyle _staticRectStyle;
}
