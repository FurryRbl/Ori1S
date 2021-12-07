using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
[AddComponentMenu("AVPro Windows Media/IMGUI Display")]
[ExecuteInEditMode]
public class AVProWindowsMediaGUIDisplay : MonoBehaviour
{
	// Token: 0x0600121D RID: 4637 RVA: 0x000531A4 File Offset: 0x000513A4
	public void OnGUI()
	{
		if (this._movie == null)
		{
			return;
		}
		if (this._movie.OutputTexture != null && (!this._alphaBlend || this._color.a > 0f))
		{
			GUI.depth = this._depth;
			GUI.color = this._color;
			Rect rect = this.GetRect();
			Material conversionMaterial = this._movie.MovieInstance.GetConversionMaterial();
			if (conversionMaterial == null)
			{
				if (this._movie.MovieInstance.RequiresFlipY)
				{
					GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0f, rect.y + rect.height / 2f));
				}
				GUI.DrawTexture(rect, this._movie.OutputTexture, this._scaleMode, this._alphaBlend);
			}
			else if (Event.current.type.Equals(EventType.Repaint))
			{
			}
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x000532BC File Offset: 0x000514BC
	public Rect GetRect()
	{
		Rect result;
		if (this._fullScreen)
		{
			result = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		}
		else
		{
			result = new Rect(this._x * (float)(Screen.width - 1), this._y * (float)(Screen.height - 1), this._width * (float)Screen.width, this._height * (float)Screen.height);
		}
		return result;
	}

	// Token: 0x04000F82 RID: 3970
	public AVProWindowsMediaMovie _movie;

	// Token: 0x04000F83 RID: 3971
	public ScaleMode _scaleMode = ScaleMode.ScaleToFit;

	// Token: 0x04000F84 RID: 3972
	public Color _color = Color.white;

	// Token: 0x04000F85 RID: 3973
	public bool _alphaBlend;

	// Token: 0x04000F86 RID: 3974
	public bool _fullScreen = true;

	// Token: 0x04000F87 RID: 3975
	public int _depth;

	// Token: 0x04000F88 RID: 3976
	public float _x;

	// Token: 0x04000F89 RID: 3977
	public float _y;

	// Token: 0x04000F8A RID: 3978
	public float _width = 1f;

	// Token: 0x04000F8B RID: 3979
	public float _height = 1f;
}
