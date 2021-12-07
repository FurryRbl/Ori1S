using System;
using UnityEngine;

// Token: 0x02000851 RID: 2129
public class CrossFadeControl : MonoBehaviour
{
	// Token: 0x06003055 RID: 12373 RVA: 0x000CCB6C File Offset: 0x000CAD6C
	private void Update()
	{
		this.m_currentFrame = this.m_frameCounter % 2;
		this.tweenTime += Time.deltaTime * 0.1f;
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x000CCBA0 File Offset: 0x000CADA0
	private void OnRenderImage(RenderTexture from, RenderTexture to)
	{
		if (this.Buf == null || this.Buf.width != Screen.width || this.Buf.height != Screen.height)
		{
			if (this.Buf != null)
			{
				UnityEngine.Object.DestroyImmediate(this.Buf);
			}
			this.Buf = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
			this.Buf.name = "Buf";
		}
		Graphics.Blit(from, to);
		this.Material.SetColor(ShaderProperties.Color, new Color(1f, 1f, 1f, (this.m_currentFrame != 0) ? (1f - this.tweenTime) : this.tweenTime));
		Graphics.Blit(this.Buf, null, this.Material);
		if (this.m_currentFrame != this.m_prevFrame)
		{
			Graphics.Blit(from, this.Buf);
		}
		Graphics.SetRenderTarget(to);
		this.m_prevFrame = this.m_currentFrame;
		if (this.m_currentFrame == 0)
		{
			base.GetComponent<Camera>().transform.position = this.Target2.transform.position;
			this.ApplySettings(this.settingTo);
		}
		else
		{
			base.GetComponent<Camera>().transform.position = this.Target1.transform.position;
			this.ApplySettings(this.settingFrom);
		}
		this.m_frameCounter++;
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x000CCD2F File Offset: 0x000CAF2F
	private void ApplySettings(Color p0)
	{
		base.GetComponent<Camera>().backgroundColor = p0;
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x000CCD3D File Offset: 0x000CAF3D
	private void OnDestroy()
	{
		UnityEngine.Object.DestroyObject(this.Buf);
	}

	// Token: 0x04002B96 RID: 11158
	public GameObject Target1;

	// Token: 0x04002B97 RID: 11159
	public GameObject Target2;

	// Token: 0x04002B98 RID: 11160
	public Material Material;

	// Token: 0x04002B99 RID: 11161
	public RenderTexture Buf;

	// Token: 0x04002B9A RID: 11162
	private int m_frameCounter;

	// Token: 0x04002B9B RID: 11163
	private int m_currentFrame;

	// Token: 0x04002B9C RID: 11164
	private float tweenTime;

	// Token: 0x04002B9D RID: 11165
	private Color settingFrom = Color.red;

	// Token: 0x04002B9E RID: 11166
	private Color settingTo = Color.green;

	// Token: 0x04002B9F RID: 11167
	private int m_prevFrame;
}
