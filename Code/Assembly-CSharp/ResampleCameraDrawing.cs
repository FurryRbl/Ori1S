using System;
using UnityEngine;

// Token: 0x02000994 RID: 2452
public class ResampleCameraDrawing : MonoBehaviour
{
	// Token: 0x0600358B RID: 13707 RVA: 0x000E08EC File Offset: 0x000DEAEC
	private void Start()
	{
		Vector2 vector = ResampleCameraDrawing.SCREEN_RESOLUTION * this.DownScaleFactor;
		this.m_downsampledRenderBuffer = new RenderTexture((int)vector.x, (int)vector.y, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
		this.m_downsampledRenderBuffer.name = "downsampledRenderBuffer";
		base.GetComponent<Camera>().targetTexture = this.m_downsampledRenderBuffer;
		GameObject gameObject = new GameObject("SuperSamplingCamera", new Type[]
		{
			typeof(Camera)
		});
		Camera component = gameObject.GetComponent<Camera>();
		gameObject.AddComponent<GUILayer>();
		component.cullingMask = 1 << LayerMask.NameToLayer("resampleBuffer");
		component.depth = base.GetComponent<Camera>().depth;
		component.clearFlags = CameraClearFlags.Depth;
		GameObject gameObject2 = new GameObject("ResampledTexture", new Type[]
		{
			typeof(GUITexture)
		});
		GUITexture component2 = gameObject2.GetComponent<GUITexture>();
		component2.texture = this.m_downsampledRenderBuffer;
		component2.pixelInset = new Rect(ResampleCameraDrawing.SCREEN_RESOLUTION.x / 2f, ResampleCameraDrawing.SCREEN_RESOLUTION.y / 2f, 0f, 0f);
		component2.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		gameObject2.transform.localPosition = new Vector3(0f, 0f, -10f);
		gameObject2.layer = LayerMask.NameToLayer("resampleBuffer");
	}

	// Token: 0x04003022 RID: 12322
	public static Vector2 SCREEN_RESOLUTION = new Vector2(1280f, 720f);

	// Token: 0x04003023 RID: 12323
	public float DownScaleFactor = 0.75f;

	// Token: 0x04003024 RID: 12324
	private RenderTexture m_downsampledRenderBuffer;
}
