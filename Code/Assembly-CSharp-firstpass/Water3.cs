using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000014 RID: 20
[ExecuteInEditMode]
public class Water3 : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x000048A0 File Offset: 0x00002AA0
	public void Start()
	{
		if (this.m_Water3Material)
		{
			base.GetComponent<Renderer>().sharedMaterial = this.m_Water3Material;
		}
		this.m_IsDirty = false;
		this.m_WaterManager = Water3Manager.Instance();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000048D8 File Offset: 0x00002AD8
	public Mesh GetMesh()
	{
		if (Application.isPlaying)
		{
			return ((MeshFilter)base.GetComponent(typeof(MeshFilter))).mesh;
		}
		return ((MeshFilter)base.GetComponent(typeof(MeshFilter))).sharedMesh;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004924 File Offset: 0x00002B24
	public bool IsUnderwater(Camera cam)
	{
		return cam.transform.position.y + this.m_UnderwaterCheckOffset < base.transform.position.y;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000496C File Offset: 0x00002B6C
	public void OnWillRenderObject()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!base.GetComponent<Renderer>().sharedMaterial)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current)
		{
			return;
		}
		if (Water3.s_InsideWater)
		{
			return;
		}
		Water3.s_InsideWater = true;
		bool flag = false;
		this.m_HardwareWaterSupport = this.FindHardwareWaterSupport();
		Water3.WaterMode waterMode = this.GetWaterMode();
		Camera camera;
		this.CreateWaterObjects(current, out camera);
		Vector3 position = base.transform.position;
		Vector3 up = base.transform.up;
		int pixelLightCount = QualitySettings.pixelLightCount;
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = 0;
		}
		this.UpdateCameraModes(current, camera);
		if (waterMode >= Water3.WaterMode.FastAndNoRefraction)
		{
			if (this.IsUnderwater(current))
			{
				Water3UnderwaterEffect water3UnderwaterEffect = (Water3UnderwaterEffect)current.gameObject.GetComponent(typeof(Water3UnderwaterEffect));
				if (water3UnderwaterEffect)
				{
					water3UnderwaterEffect.enabled = true;
					water3UnderwaterEffect.m_Water = this;
				}
				flag = true;
			}
			else
			{
				Water3UnderwaterEffect water3UnderwaterEffect2 = (Water3UnderwaterEffect)current.gameObject.GetComponent(typeof(Water3UnderwaterEffect));
				if (water3UnderwaterEffect2 && water3UnderwaterEffect2.enabled)
				{
					water3UnderwaterEffect2.enabled = false;
				}
				if (this.realtime2DReflection)
				{
					float w = -Vector3.Dot(up, position) - this.m_ClipPlaneOffset;
					Vector4 plane = new Vector4(up.x, up.y, up.z, w);
					Matrix4x4 zero = Matrix4x4.zero;
					Water3.CalculateReflectionMatrix(ref zero, plane);
					Vector3 position2 = current.transform.position;
					Vector3 position3 = zero.MultiplyPoint(position2);
					camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
					Vector4 clipPlane = this.CameraSpacePlane(camera, position, up, 1f);
					Matrix4x4 projectionMatrix = current.projectionMatrix;
					Water3.CalculateObliqueMatrix(ref projectionMatrix, clipPlane);
					camera.projectionMatrix = projectionMatrix;
					camera.depthTextureMode = DepthTextureMode.None;
					camera.renderingPath = RenderingPath.Forward;
					camera.cullingMask = (-17 & this.m_ReflectLayers.value);
					camera.targetTexture = this.m_ReflectionTexture;
					GL.SetRevertBackfacing(true);
					camera.transform.position = position3;
					Vector3 eulerAngles = current.transform.eulerAngles;
					camera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
					camera.Render();
					camera.transform.position = position2;
					GL.SetRevertBackfacing(false);
					base.GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
				}
			}
		}
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = pixelLightCount;
		}
		if (this.lightTransform)
		{
			base.GetComponent<Renderer>().sharedMaterial.SetVector("_WorldLightDir", this.lightTransform.forward);
		}
		if (!this.m_DepthTexturesSupported)
		{
			this.autoEdgeBlend = false;
		}
		if (flag)
		{
			if (current)
			{
				current.depthTextureMode |= DepthTextureMode.Depth;
			}
			base.GetComponent<Renderer>().sharedMaterial.shader.maximumLOD = 50;
		}
		else if (waterMode == Water3.WaterMode.Indie)
		{
			base.GetComponent<Renderer>().sharedMaterial.shader.maximumLOD = 100;
		}
		else
		{
			if (waterMode == Water3.WaterMode.Optimized)
			{
				base.GetComponent<Renderer>().sharedMaterial.shader.maximumLOD = 400;
			}
			else
			{
				base.GetComponent<Renderer>().sharedMaterial.shader.maximumLOD = ((waterMode <= Water3.WaterMode.FastAndNoRefraction) ? 300 : 500);
			}
			if (this.autoEdgeBlend && current)
			{
				current.depthTextureMode |= DepthTextureMode.Depth;
			}
		}
		Water3.s_InsideWater = false;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004D20 File Offset: 0x00002F20
	private void OnDisable()
	{
		if (this.m_ReflectionTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			this.m_ReflectionTexture = null;
		}
		foreach (object obj in this.m_ReflectionCameras)
		{
			UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)obj).Value).gameObject);
		}
		this.m_ReflectionCameras.Clear();
		if (this.m_IsDirty)
		{
			Vector3[] vertices = this.GetMesh().vertices;
			Vector3[] normals = this.GetMesh().normals;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = new Vector3(vertices[i].x, 0f, vertices[i].z);
				normals[i] = new Vector3(0f, 1f, 0f);
			}
			this.GetMesh().vertices = vertices;
			this.GetMesh().normals = normals;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00004E74 File Offset: 0x00003074
	public Vector3 GetNormalAt(Vector3 pos, float scale = 1f)
	{
		Vector3 heightOffsetAt = this.GetHeightOffsetAt(pos + new Vector3(-scale, 0f, 0f));
		Vector3 heightOffsetAt2 = this.GetHeightOffsetAt(pos + new Vector3(-scale, 0f, scale));
		Vector3 heightOffsetAt3 = this.GetHeightOffsetAt(pos + new Vector3(0f, 0f, 0f));
		Vector3 lhs = heightOffsetAt - heightOffsetAt2;
		Vector3 rhs = heightOffsetAt - heightOffsetAt3;
		Vector3 result = Vector3.Cross(lhs, rhs);
		result.Normalize();
		return result;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004F00 File Offset: 0x00003100
	public Vector3 GetHeightOffsetAt(Vector3 pos)
	{
		Vector3 vector = new Vector3(pos.x, pos.y, pos.z);
		vector = base.transform.InverseTransformPoint(vector);
		float num = base.transform.position.x / base.transform.localScale.x;
		float num2 = base.transform.position.z / base.transform.localScale.z;
		Vector4 materialVector = Water3Manager.Instance().GetMaterialVector("_Displacement");
		Vector4 materialVector2 = Water3Manager.Instance().GetMaterialVector("_DisplacementXz");
		vector.x += num;
		vector.z += num2;
		float x = materialVector.x;
		float z = materialVector.z;
		float y = materialVector.y;
		float w = materialVector.w;
		float num3 = Mathf.Sin(x * vector.x + Water3Manager.Instance().m_Timer * z);
		float num4 = Mathf.Sin(y * vector.z + Water3Manager.Instance().m_Timer * w);
		vector.y = 0f + materialVector2.x / base.transform.localScale.x * num3 + materialVector2.z / base.transform.localScale.z * num4;
		vector.y += Water3Manager.Instance().GetDisplaceMeshAmountAt(vector, base.transform);
		vector.x -= num;
		vector.z -= num2;
		vector = base.transform.TransformPoint(vector);
		return vector;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000050C4 File Offset: 0x000032C4
	private void Update()
	{
		if (!base.GetComponent<Renderer>())
		{
			return;
		}
		Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
		if (!sharedMaterial)
		{
			return;
		}
		this.m_IsDirty |= Water3Manager.Instance().DisplaceMesh(this.GetMesh(), base.transform);
		Vector3 v = new Vector3(this.m_DistortParams.x, this.m_DistortParams.y, this.m_DistortParams.z);
		sharedMaterial.SetVector("_DistortParams", v);
		sharedMaterial.SetVector("_InvFadeParemeter", new Vector4(this.m_InvFade, this.m_InvFadeFoam, this.m_InvFadeDepthFade, this.m_FadeExp));
		sharedMaterial.SetVector("_ShoreTiling", new Vector4(this.m_ShoreTilingBumpA.x, this.m_ShoreTilingBumpA.y, this.m_ShoreTilingBumpB.x, this.m_ShoreTilingBumpB.y));
		sharedMaterial.SetFloat("_Shininess", this.m_Shininess);
		sharedMaterial.SetVector("_FoamWaveParams", new Vector4(this.m_FoamWaveParams.x, this.m_FoamWaveParams.y, this.m_FoamWaveParams.z, 0f));
		Vector4 vector = new Vector4(this.m_WaveSpeedBumpA.x, this.m_WaveSpeedBumpA.y, this.m_WaveSpeedBumpB.x, this.m_WaveSpeedBumpB.y);
		Vector4 vector2 = new Vector4(this.m_WaveScale, this.m_WaveScale, this.m_WaveScale * 0.4f, this.m_WaveScale * 0.45f);
		double num = (double)Time.timeSinceLevelLoad / 20.0;
		Vector4 vector3 = new Vector4((float)Math.IEEERemainder((double)(vector.x * vector2.x) * num, 1.0), (float)Math.IEEERemainder((double)(vector.y * vector2.y) * num, 1.0), (float)Math.IEEERemainder((double)(vector.z * vector2.z) * num, 1.0), (float)Math.IEEERemainder((double)(vector.w * vector2.w) * num, 1.0));
		sharedMaterial.SetVector("_WaveOffset", vector3);
		sharedMaterial.SetVector("_WaveScale4", vector2);
		Vector3 size = base.GetComponent<Renderer>().bounds.size;
		Vector3 s = new Vector3(size.x * vector2.x, size.z * vector2.y, 1f);
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(vector3.x, vector3.y, 0f), Quaternion.identity, s);
		sharedMaterial.SetMatrix("_WaveMatrix", matrix);
		s = new Vector3(size.x * vector2.z, size.z * vector2.w, 1f);
		matrix = Matrix4x4.TRS(new Vector3(vector3.z, vector3.w, 0f), Quaternion.identity, s);
		sharedMaterial.SetMatrix("_WaveMatrix2", matrix);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000053EC File Offset: 0x000035EC
	private void UpdateCameraModes(Camera src, Camera dest)
	{
		if (dest == null)
		{
			return;
		}
		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;
		if (src.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox skybox = src.GetComponent(typeof(Skybox)) as Skybox;
			Skybox skybox2 = dest.GetComponent(typeof(Skybox)) as Skybox;
			if (!skybox || !skybox.material)
			{
				skybox2.enabled = false;
			}
			else
			{
				skybox2.enabled = true;
				skybox2.material = skybox.material;
			}
		}
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
		dest.depthTextureMode = DepthTextureMode.None;
		dest.renderingPath = RenderingPath.Forward;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000054E8 File Offset: 0x000036E8
	private void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera)
	{
		Water3.WaterMode waterMode = this.GetWaterMode();
		reflectionCamera = null;
		if (waterMode >= Water3.WaterMode.FastAndNoRefraction)
		{
			if (!this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.m_TextureSize)
			{
				if (this.m_ReflectionTexture)
				{
					UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
				}
				this.m_ReflectionTexture = new RenderTexture(this.m_TextureSize, this.m_TextureSize, 16);
				this.m_ReflectionTexture.name = "__WaterReflection" + base.GetInstanceID();
				this.m_ReflectionTexture.isPowerOfTwo = true;
				this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
				this.m_OldReflectionTextureSize = this.m_TextureSize;
			}
			reflectionCamera = (this.m_ReflectionCameras[currentCamera] as Camera);
			if (!reflectionCamera)
			{
				GameObject gameObject = new GameObject(string.Concat(new object[]
				{
					"Water Refl Camera id",
					base.GetInstanceID(),
					" for ",
					currentCamera.GetInstanceID()
				}), new Type[]
				{
					typeof(Camera),
					typeof(Skybox)
				});
				reflectionCamera = gameObject.GetComponent<Camera>();
				reflectionCamera.enabled = false;
				reflectionCamera.transform.position = base.transform.position;
				reflectionCamera.transform.rotation = base.transform.rotation;
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				this.m_ReflectionCameras[currentCamera] = reflectionCamera;
			}
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00005670 File Offset: 0x00003870
	private Water3.WaterMode GetWaterMode()
	{
		if (this.m_HardwareWaterSupport < this.m_WaterMode)
		{
			return this.m_HardwareWaterSupport;
		}
		return this.m_WaterMode;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00005690 File Offset: 0x00003890
	private Water3.WaterMode FindHardwareWaterSupport()
	{
		if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.m_DepthTexturesSupported = false;
		}
		if (!SystemInfo.supportsRenderTextures || !base.GetComponent<Renderer>())
		{
			return Water3.WaterMode.Indie;
		}
		Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
		if (!sharedMaterial)
		{
			return Water3.WaterMode.Indie;
		}
		return Water3.WaterMode.Everything;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000056E8 File Offset: 0x000038E8
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

	// Token: 0x06000061 RID: 97 RVA: 0x00005714 File Offset: 0x00003914
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 v = pos + normal * this.m_ClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00005780 File Offset: 0x00003980
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(Water3.sgn(clipPlane.x), Water3.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00005830 File Offset: 0x00003A30
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

	// Token: 0x04000060 RID: 96
	public Water3.WaterMode m_WaterMode = Water3.WaterMode.Everything;

	// Token: 0x04000061 RID: 97
	public Water3Manager m_WaterManager;

	// Token: 0x04000062 RID: 98
	public bool m_DisablePixelLights = true;

	// Token: 0x04000063 RID: 99
	public int m_TextureSize = 256;

	// Token: 0x04000064 RID: 100
	public float m_ClipPlaneOffset = 0.07f;

	// Token: 0x04000065 RID: 101
	public LayerMask m_ReflectLayers = -1;

	// Token: 0x04000066 RID: 102
	private Hashtable m_ReflectionCameras = new Hashtable();

	// Token: 0x04000067 RID: 103
	private RenderTexture m_ReflectionTexture;

	// Token: 0x04000068 RID: 104
	private Water3.WaterMode m_HardwareWaterSupport = Water3.WaterMode.Everything;

	// Token: 0x04000069 RID: 105
	private int m_OldReflectionTextureSize;

	// Token: 0x0400006A RID: 106
	private static bool s_InsideWater;

	// Token: 0x0400006B RID: 107
	public bool realtime2DReflection = true;

	// Token: 0x0400006C RID: 108
	public bool autoEdgeBlend = true;

	// Token: 0x0400006D RID: 109
	public bool waterDisplacement = true;

	// Token: 0x0400006E RID: 110
	public bool refractionMask;

	// Token: 0x0400006F RID: 111
	public float m_Shininess = 100f;

	// Token: 0x04000070 RID: 112
	public float m_WaveScale = 0.04f;

	// Token: 0x04000071 RID: 113
	public Transform lightTransform;

	// Token: 0x04000072 RID: 114
	public Vector3 m_FoamWaveParams;

	// Token: 0x04000073 RID: 115
	public Vector2 m_WaveSpeedBumpA;

	// Token: 0x04000074 RID: 116
	public Vector2 m_WaveSpeedBumpB;

	// Token: 0x04000075 RID: 117
	public Vector3 m_DistortParams = new Vector3(0.18f, 0.8f, 2f);

	// Token: 0x04000076 RID: 118
	public float m_FadeExp;

	// Token: 0x04000077 RID: 119
	public float m_InvFade;

	// Token: 0x04000078 RID: 120
	public float m_InvFadeFoam;

	// Token: 0x04000079 RID: 121
	public float m_InvFadeDepthFade;

	// Token: 0x0400007A RID: 122
	public Vector2 m_ShoreTilingBumpA;

	// Token: 0x0400007B RID: 123
	public Vector2 m_ShoreTilingBumpB;

	// Token: 0x0400007C RID: 124
	public float m_UnderwaterCheckOffset = 0.001f;

	// Token: 0x0400007D RID: 125
	public Material m_Water3Material;

	// Token: 0x0400007E RID: 126
	private bool m_IsDirty;

	// Token: 0x0400007F RID: 127
	private bool m_DepthTexturesSupported = true;

	// Token: 0x02000015 RID: 21
	public enum WaterMode
	{
		// Token: 0x04000081 RID: 129
		Indie,
		// Token: 0x04000082 RID: 130
		FastAndNoRefraction,
		// Token: 0x04000083 RID: 131
		Optimized,
		// Token: 0x04000084 RID: 132
		Everything
	}
}
