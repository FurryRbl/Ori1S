using System;
using UnityEngine;

// Token: 0x0200085A RID: 2138
public class UberWaterReflection
{
	// Token: 0x0600306E RID: 12398 RVA: 0x000CD722 File Offset: 0x000CB922
	public UberWaterReflection(UberWaterControl control, Renderer renderer, Transform trans)
	{
		this.Control = control;
		this.m_transform = trans;
		this.m_renderer = renderer;
	}

	// Token: 0x0600306F RID: 12399 RVA: 0x000CD740 File Offset: 0x000CB940
	private void UpdateResources()
	{
		int num = Mathf.RoundToInt((float)Camera.current.pixelWidth / 2.5f);
		int num2 = Mathf.RoundToInt((float)Camera.current.pixelHeight / 2.5f);
		if (this.m_texture == null || this.m_texture.width != num || this.m_texture.height != num2)
		{
			if (this.m_texture != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_texture);
			}
			this.m_texture = new RenderTexture(num, num2, 0, RenderTextureFormat.ARGB32);
			this.m_texture.name = "reflectionTex";
		}
		if (this.m_renderCamera == null)
		{
			GameObject gameObject = new GameObject("__WaterRefl__", new Type[]
			{
				typeof(Camera),
				typeof(Skybox)
			});
			this.m_renderCamera = gameObject.GetComponent<Camera>();
			this.m_renderCamera.enabled = false;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			this.m_renderCamera.cullingMask = (1 << LayerMask.NameToLayer("artReflected") | 1 << LayerMask.NameToLayer("artBlurredReflected"));
		}
	}

	// Token: 0x06003070 RID: 12400 RVA: 0x000CD870 File Offset: 0x000CBA70
	public void OnWillRenderObject()
	{
		if (UberWaterReflection.ReflectionRender || this.Control.ReflectResolution == 0)
		{
			return;
		}
		Camera current = Camera.current;
		bool orthographic = current.orthographic;
		if (orthographic)
		{
			return;
		}
		this.UpdateResources();
		UberWaterReflection.ReflectionRender = true;
		Vector3 position = this.m_transform.position;
		Vector3 up = this.m_transform.up;
		this.UpdateCameraModes(current, this.m_renderCamera);
		this.m_planeOffset = -0.01f;
		float w = -Vector3.Dot(up, position) - this.m_planeOffset;
		Vector4 plane = new Vector4(up.x, up.y, up.z, w);
		Matrix4x4 zero = Matrix4x4.zero;
		UberWaterReflection.CalculateReflectionMatrix(ref zero, plane);
		Vector3 position2 = current.transform.position;
		Vector3 position3 = zero.MultiplyPoint(position2);
		this.m_renderCamera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
		float sideSign = (Camera.current.transform.position.y <= this.m_transform.position.y) ? -1f : 1f;
		Vector4 clipPlane = this.CameraSpacePlane(this.m_renderCamera, position, up, sideSign);
		Matrix4x4 projectionMatrix = current.projectionMatrix;
		UberWaterReflection.CalculateObliqueMatrix(ref projectionMatrix, clipPlane);
		this.m_renderCamera.projectionMatrix = projectionMatrix;
		this.m_renderCamera.targetTexture = this.m_texture;
		GL.SetRevertBackfacing(true);
		this.m_renderCamera.transform.position = position3;
		this.m_renderCamera.fieldOfView -= 20f;
		Vector3 eulerAngles = current.transform.eulerAngles;
		this.m_renderCamera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
		this.m_renderCamera.Render();
		this.m_renderCamera.transform.position = position2;
		GL.SetRevertBackfacing(false);
		this.m_renderer.sharedMaterial.SetTexture("_ReflectionTex", this.m_texture);
		this.m_renderCamera.worldToCameraMatrix = current.worldToCameraMatrix;
		UberWaterReflection.ReflectionRender = false;
	}

	// Token: 0x06003071 RID: 12401 RVA: 0x000CDA98 File Offset: 0x000CBC98
	public void OnDestroy()
	{
		UnityEngine.Object.DestroyImmediate(this.m_renderCamera.gameObject);
		UnityEngine.Object.DestroyImmediate(this.m_texture);
	}

	// Token: 0x06003072 RID: 12402 RVA: 0x000CDAB8 File Offset: 0x000CBCB8
	private void UpdateCameraModes(Camera src, Camera dest)
	{
		if (dest == null)
		{
			return;
		}
		dest.clearFlags = CameraClearFlags.Skybox;
		float @float = this.ReflectPlane.GetComponent<Renderer>().sharedMaterial.GetFloat("_ReflStrength");
		dest.backgroundColor = src.backgroundColor / @float;
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
		dest.orthographic = src.orthographic;
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x000CDB4E File Offset: 0x000CBD4E
	private static float Sgn(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x000CDB78 File Offset: 0x000CBD78
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 v = pos + normal * this.m_planeOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x000CDBE4 File Offset: 0x000CBDE4
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(UberWaterReflection.Sgn(clipPlane.x), UberWaterReflection.Sgn(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
	}

	// Token: 0x06003076 RID: 12406 RVA: 0x000CDC94 File Offset: 0x000CBE94
	private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
	}

	// Token: 0x04002BC6 RID: 11206
	public UberWaterControl Control;

	// Token: 0x04002BC7 RID: 11207
	public UberWaterTop ReflectPlane;

	// Token: 0x04002BC8 RID: 11208
	public static bool ReflectionRender;

	// Token: 0x04002BC9 RID: 11209
	private Camera m_renderCamera;

	// Token: 0x04002BCA RID: 11210
	private float m_planeOffset;

	// Token: 0x04002BCB RID: 11211
	private Transform m_transform;

	// Token: 0x04002BCC RID: 11212
	private Renderer m_renderer;

	// Token: 0x04002BCD RID: 11213
	private RenderTexture m_texture;
}
