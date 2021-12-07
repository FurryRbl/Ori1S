using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[ExecuteInEditMode]
public class Water3Manager : MonoBehaviour
{
	// Token: 0x06000069 RID: 105 RVA: 0x00005B08 File Offset: 0x00003D08
	public void SetDisplacementHeightMap(Texture2D map, int index)
	{
		if (!map)
		{
			return;
		}
		if (index == 0)
		{
			if (this.m_DisplacementHeightMap != map)
			{
				this.m_DisplacementHeightMap = map;
				this.FillWithGradiant(this.m_DisplacementHeightMap);
			}
		}
		else if (this.m_2ndDisplacementHeightMap != map)
		{
			this.m_2ndDisplacementHeightMap = map;
			this.FillWithGradiant(this.m_2ndDisplacementHeightMap);
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00005B74 File Offset: 0x00003D74
	public Texture2D GetDisplacementHeightMap(int index)
	{
		if (index == 0)
		{
			return this.m_DisplacementHeightMap;
		}
		return this.m_2ndDisplacementHeightMap;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00005B8C File Offset: 0x00003D8C
	public static Water3Manager Instance()
	{
		if (Water3Manager.s_Instance == null)
		{
			Water3Manager.s_Instance = (UnityEngine.Object.FindObjectOfType(typeof(Water3Manager)) as Water3Manager);
		}
		return Water3Manager.s_Instance;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00005BC8 File Offset: 0x00003DC8
	public void OnEnable()
	{
		this.m_Timer = 0f;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00005BD8 File Offset: 0x00003DD8
	public void Start()
	{
		this.SetDisplacementHeightMap((Texture2D)this.GetMaterialTexture("_DisplacementHeightMap"), 0);
		this.SetDisplacementHeightMap((Texture2D)this.GetMaterialTexture("_SecondDisplacementHeightMap"), 1);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00005C14 File Offset: 0x00003E14
	public void Update()
	{
		if (null == this.m_SharedWaterMaterial)
		{
			return;
		}
		if (Application.isPlaying)
		{
			this.m_Timer += Time.deltaTime;
		}
		this.m_SharedWaterMaterial.SetFloat("_NoiseTime", this.m_Timer);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00005C68 File Offset: 0x00003E68
	public Color GetMaterialColor(string name)
	{
		return this.m_SharedWaterMaterial.GetColor(name);
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00005C78 File Offset: 0x00003E78
	public void SetMaterialColor(string name, Color color)
	{
		this.m_SharedWaterMaterial.SetColor(name, color);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00005C88 File Offset: 0x00003E88
	public Vector4 GetMaterialVector(string name)
	{
		return this.m_SharedWaterMaterial.GetVector(name);
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00005C98 File Offset: 0x00003E98
	public void SetMaterialVector(string name, Vector4 vector)
	{
		this.m_SharedWaterMaterial.SetVector(name, vector);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00005CA8 File Offset: 0x00003EA8
	public Texture GetMaterialTexture(string theName)
	{
		return this.m_SharedWaterMaterial.GetTexture(theName);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00005CB8 File Offset: 0x00003EB8
	public void SetMaterialTexture(string theName, Texture parameter)
	{
		this.m_SharedWaterMaterial.SetTexture(theName, parameter);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00005CC8 File Offset: 0x00003EC8
	public float GetDisplaceMeshAmountAt(Vector3 pos, Transform t)
	{
		if (this.m_CpuDisplacementModel == Water3Manager.CpuDisplacementModel.None)
		{
			return 0f;
		}
		return this.DisplaceMeshAmountAt(pos, t);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00005CE4 File Offset: 0x00003EE4
	public void FillWithGradiant(Texture2D tex)
	{
		for (int i = 0; i < tex.width; i++)
		{
			for (int j = 0; j < tex.height; j++)
			{
				Color pixel = this.m_DisplacementHeightMap.GetPixel(i, j);
				Color pixel2 = this.m_DisplacementHeightMap.GetPixel(i - 1, j);
				Color pixel3 = this.m_DisplacementHeightMap.GetPixel(i, j - 1);
				this.m_DisplacementHeightMap.SetPixel(i, j, new Color((pixel.a - pixel2.a) * 0.5f + 0.5f, (pixel.a - pixel3.a) * 0.5f + 0.5f, pixel.a, pixel.a));
			}
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00005DA4 File Offset: 0x00003FA4
	public bool DisplaceMesh(Mesh m, Transform t)
	{
		if (this.m_CpuDisplacementModel == Water3Manager.CpuDisplacementModel.None)
		{
			return false;
		}
		if (null == this.m_DisplacementHeightMap || null == this.m_2ndDisplacementHeightMap)
		{
			return false;
		}
		float num = t.position.x / t.localScale.x;
		float num2 = t.position.z / t.localScale.z;
		Vector3[] vertices = m.vertices;
		Vector3[] normals = m.normals;
		float num3 = this.m_DisplacementTiling + Time.time * 0.1f * this.m_SmallWavesSpeed;
		float num4 = this.m_HeightDisplacement / t.localScale.y;
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vector;
			vector.x = (vertices[i].x + num) * this.m_DisplacementTiling + num3;
			vector.z = (vertices[i].z + num2) * this.m_DisplacementTiling + num3;
			Color pixelBilinear = this.m_DisplacementHeightMap.GetPixelBilinear(vector.x, vector.z);
			Color pixelBilinear2 = this.m_2ndDisplacementHeightMap.GetPixelBilinear(vector.z, vector.x);
			vertices[i][1] = ((pixelBilinear.a + pixelBilinear2.a) * 2f - 2f) * num4;
			normals[i][0] = ((pixelBilinear.r + pixelBilinear2.r) * 2f - 2f) * this.m_NormalsDisplacement;
			normals[i][2] = ((pixelBilinear.g + pixelBilinear2.g) * 2f - 2f) * this.m_NormalsDisplacement;
		}
		m.vertices = vertices;
		m.normals = normals;
		return true;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00005F8C File Offset: 0x0000418C
	private float DisplaceMeshAmountAt(Vector3 pos, Transform t)
	{
		float num = this.m_HeightDisplacement / t.localScale.y;
		float num2 = this.m_DisplacementTiling + Time.time * 0.1f * this.m_SmallWavesSpeed;
		Vector3 vector = pos;
		vector.x = vector.x * this.m_DisplacementTiling + num2;
		vector.z = vector.z * this.m_DisplacementTiling + num2;
		Color pixelBilinear = this.m_DisplacementHeightMap.GetPixelBilinear(vector.x, vector.z);
		Color pixelBilinear2 = this.m_2ndDisplacementHeightMap.GetPixelBilinear(vector.z, vector.x);
		return (pixelBilinear.a + pixelBilinear2.a * 2f - 1f) * num;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00006058 File Offset: 0x00004258
	private void OnApplicationQuit()
	{
		Water3Manager.s_Instance = null;
	}

	// Token: 0x04000086 RID: 134
	[HideInInspector]
	public Water3Manager.CpuDisplacementModel m_CpuDisplacementModel = Water3Manager.CpuDisplacementModel.NoiseBump;

	// Token: 0x04000087 RID: 135
	[HideInInspector]
	public float m_DisplacementTiling = 0.25f;

	// Token: 0x04000088 RID: 136
	[HideInInspector]
	public float m_NormalsDisplacement = 0.5f;

	// Token: 0x04000089 RID: 137
	[HideInInspector]
	public float m_HeightDisplacement = 3.5f;

	// Token: 0x0400008A RID: 138
	[HideInInspector]
	public float m_SmallWavesSpeed = 0.02f;

	// Token: 0x0400008B RID: 139
	[HideInInspector]
	private Texture2D m_DisplacementHeightMap;

	// Token: 0x0400008C RID: 140
	[HideInInspector]
	private Texture2D m_2ndDisplacementHeightMap;

	// Token: 0x0400008D RID: 141
	[HideInInspector]
	public Material m_SharedWaterMaterial;

	// Token: 0x0400008E RID: 142
	private static Water3Manager s_Instance;

	// Token: 0x0400008F RID: 143
	[HideInInspector]
	public float m_Timer;

	// Token: 0x02000018 RID: 24
	public enum CpuDisplacementModel
	{
		// Token: 0x04000091 RID: 145
		None,
		// Token: 0x04000092 RID: 146
		NoiseBump,
		// Token: 0x04000093 RID: 147
		FFT
	}
}
