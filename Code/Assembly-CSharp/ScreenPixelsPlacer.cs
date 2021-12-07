using System;
using UnityEngine;

// Token: 0x02000999 RID: 2457
[ExecuteInEditMode]
public class ScreenPixelsPlacer : MonoBehaviour
{
	// Token: 0x17000864 RID: 2148
	// (get) Token: 0x0600359F RID: 13727 RVA: 0x000E0C9C File Offset: 0x000DEE9C
	public Camera Camera
	{
		get
		{
			if (this.m_camera == null)
			{
				this.m_camera = GameObject.Find("guiCamera").GetComponent<Camera>();
			}
			return this.m_camera;
		}
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x000E0CD8 File Offset: 0x000DEED8
	[ContextMenu("Set size to texture size")]
	public void ContextMenu()
	{
		Texture mainTexture = base.GetComponent<Renderer>().sharedMaterial.mainTexture;
		this.PixelSize.width = (float)mainTexture.width;
		this.PixelSize.height = (float)mainTexture.height;
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x000E0D1C File Offset: 0x000DEF1C
	public void Update()
	{
		if (this.Refresh && !this.m_wasRefresh)
		{
			Vector3 localScale = base.transform.localScale;
			localScale.z = 0f;
			Vector3 vector = this.Camera.WorldToScreenPoint(base.transform.position - localScale * 0.5f);
			Vector3 vector2 = this.Camera.WorldToScreenPoint(base.transform.position + localScale * 0.5f);
			this.PixelSize.xMin = Mathf.Round(vector.x);
			this.PixelSize.yMin = Mathf.Round(vector.y);
			this.PixelSize.xMax = Mathf.Round(vector2.x);
			this.PixelSize.yMax = Mathf.Round(vector2.y);
		}
		if (this.Refresh)
		{
			Vector3 vector3 = this.Camera.WorldToScreenPoint(base.transform.position);
			Vector3 vector4 = this.Camera.ScreenToWorldPoint(new Vector3(this.PixelSize.xMin - 0.5f, this.PixelSize.yMin + 0.5f, vector3.z));
			Vector3 vector5 = this.Camera.ScreenToWorldPoint(new Vector3(this.PixelSize.xMax - 0.5f, this.PixelSize.yMax + 0.5f, vector3.z));
			base.transform.position = (vector4 + vector5) / 2f;
			Vector3 right = this.Camera.transform.right;
			Vector3 up = this.Camera.transform.up;
			Vector3 rhs = vector5 - vector4;
			base.transform.localScale = new Vector3(Vector3.Dot(right, rhs), Vector3.Dot(up, rhs));
		}
		this.m_wasRefresh = this.Refresh;
	}

	// Token: 0x0400302A RID: 12330
	public Rect PixelSize;

	// Token: 0x0400302B RID: 12331
	private Camera m_camera;

	// Token: 0x0400302C RID: 12332
	private bool m_wasRefresh;

	// Token: 0x0400302D RID: 12333
	public bool Refresh;
}
