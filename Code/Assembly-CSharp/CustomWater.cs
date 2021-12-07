using System;
using UnityEngine;

// Token: 0x02000932 RID: 2354
public class CustomWater : MonoBehaviour
{
	// Token: 0x06003410 RID: 13328 RVA: 0x000DAFEC File Offset: 0x000D91EC
	private void Start()
	{
		int width = (int)((float)Screen.width * this.textureSizeMultiplier);
		int height = (int)((float)Screen.height * this.textureSizeMultiplier);
		RenderTexture renderTexture = new RenderTexture(width, height, 24);
		renderTexture.name = "CustomWater";
		renderTexture.format = RenderTextureFormat.ARGB32;
		renderTexture.wrapMode = TextureWrapMode.Clamp;
		renderTexture.useMipMap = false;
		base.GetComponent<Renderer>().material.SetTexture("ReflectionTexture", renderTexture);
		this.reflectionCamera = new GameObject("reflectionCamera").AddComponent<Camera>();
		this.reflectionCamera.targetTexture = renderTexture;
	}

	// Token: 0x06003411 RID: 13329 RVA: 0x000DB078 File Offset: 0x000D9278
	private void OnWillRenderObject()
	{
		if (!base.enabled || !base.GetComponent<Renderer>() || !base.GetComponent<Renderer>().sharedMaterial || !base.GetComponent<Renderer>().enabled)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current)
		{
			return;
		}
		if (this.m_insideWater)
		{
			return;
		}
		this.m_insideWater = true;
		base.GetComponent<Renderer>().material.SetVector("uvScaling", new Vector4(base.transform.localScale.x / this.wavesSize, base.transform.localScale.z / this.wavesSize, 0f, 0f));
		this.uvOffset += new Vector2(1f, 1f) * this.wavesSpeed * Time.deltaTime;
		base.GetComponent<Renderer>().material.SetVector("uvOffset", new Vector4(this.uvOffset.x, this.uvOffset.y, 0f, 0f));
		base.GetComponent<Renderer>().material.SetFloat("wavesHeight", this.wavesHeight);
		this.reflectionCamera.ResetWorldToCameraMatrix();
		this.reflectionCamera.enabled = false;
		this.reflectionCamera.transform.position = current.transform.position;
		this.reflectionCamera.transform.rotation = current.transform.rotation;
		this.reflectionCamera.aspect = current.aspect;
		Vector3 vector = base.transform.rotation * Vector3.up;
		float w = -Vector3.Dot(vector, base.transform.position) + this.clipPlaneOffset;
		Vector4 plane = new Vector4(vector.x, vector.y, vector.z, w);
		Matrix4x4 rhs = this.CalculateReflectionMatrix(plane);
		this.reflectionCamera.worldToCameraMatrix = this.reflectionCamera.worldToCameraMatrix * rhs;
		Vector4 clipPlane = this.CameraSpacePlane(this.reflectionCamera, base.transform.position, vector, 1f);
		Matrix4x4 projectionMatrix = current.projectionMatrix;
		CustomWater.CalculateObliqueMatrix(ref projectionMatrix, clipPlane);
		this.reflectionCamera.projectionMatrix = projectionMatrix;
		GL.SetRevertBackfacing(true);
		this.reflectionCamera.Render();
		GL.SetRevertBackfacing(false);
		this.m_insideWater = false;
	}

	// Token: 0x06003412 RID: 13330 RVA: 0x000DB2FC File Offset: 0x000D94FC
	private Matrix4x4 CalculateReflectionMatrix(Vector4 plane)
	{
		Matrix4x4 result;
		result.m00 = 1f - 2f * plane[0] * plane[0];
		result.m01 = -2f * plane[0] * plane[1];
		result.m02 = -2f * plane[0] * plane[2];
		result.m03 = -2f * plane[3] * plane[0];
		result.m10 = -2f * plane[1] * plane[0];
		result.m11 = 1f - 2f * plane[1] * plane[1];
		result.m12 = -2f * plane[1] * plane[2];
		result.m13 = -2f * plane[3] * plane[1];
		result.m20 = -2f * plane[2] * plane[0];
		result.m21 = -2f * plane[2] * plane[1];
		result.m22 = 1f - 2f * plane[2] * plane[2];
		result.m23 = -2f * plane[3] * plane[2];
		result.m30 = 0f;
		result.m31 = 0f;
		result.m32 = 0f;
		result.m33 = 1f;
		return result;
	}

	// Token: 0x06003413 RID: 13331 RVA: 0x000DB4B4 File Offset: 0x000D96B4
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 v = pos + normal * this.clipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x000DB51F File Offset: 0x000D971F
	private static float sgn(float a)
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

	// Token: 0x06003415 RID: 13333 RVA: 0x000DB548 File Offset: 0x000D9748
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(CustomWater.sgn(clipPlane.x), CustomWater.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
	}

	// Token: 0x04002F0E RID: 12046
	private Camera reflectionCamera;

	// Token: 0x04002F0F RID: 12047
	private RenderTexture reflectionRenderTexture;

	// Token: 0x04002F10 RID: 12048
	private Vector2 uvOffset;

	// Token: 0x04002F11 RID: 12049
	public float textureSizeMultiplier = 0.25f;

	// Token: 0x04002F12 RID: 12050
	public float clipPlaneOffset = -0.07f;

	// Token: 0x04002F13 RID: 12051
	public float wavesSize = 10f;

	// Token: 0x04002F14 RID: 12052
	public float wavesHeight = 0.05f;

	// Token: 0x04002F15 RID: 12053
	public float wavesSpeed = 0.005f;

	// Token: 0x04002F16 RID: 12054
	private bool m_insideWater;
}
