using System;
using UnityEngine;

// Token: 0x02000959 RID: 2393
[ExecuteInEditMode]
public class ClipPlane : MonoBehaviour
{
	// Token: 0x060034B0 RID: 13488 RVA: 0x000DD014 File Offset: 0x000DB214
	[ContextMenu("Crop to texture")]
	public void CropToTexture()
	{
		if (base.GetComponent<Renderer>() && base.GetComponent<Renderer>().sharedMaterial && base.GetComponent<Renderer>().sharedMaterial.mainTexture)
		{
			Rect occupiedRectNormalized = CropUtility.GetOccupiedRectNormalized((Texture2D)base.GetComponent<Renderer>().sharedMaterial.mainTexture);
			this.Left = occupiedRectNormalized.xMin;
			this.Right = 1f - occupiedRectNormalized.xMax;
			this.Bottom = occupiedRectNormalized.yMin;
			this.Top = 1f - occupiedRectNormalized.yMax;
			this.GenerateMesh();
		}
	}

	// Token: 0x060034B1 RID: 13489 RVA: 0x000DD0C1 File Offset: 0x000DB2C1
	public void OnDestroy()
	{
		this.FreeMesh();
	}

	// Token: 0x060034B2 RID: 13490 RVA: 0x000DD0C9 File Offset: 0x000DB2C9
	public void Awake()
	{
		this.GenerateMesh();
	}

	// Token: 0x060034B3 RID: 13491 RVA: 0x000DD0D1 File Offset: 0x000DB2D1
	public void FreeMesh()
	{
		if (this.m_mesh)
		{
			UnityEngine.Object.DestroyImmediate(this.m_mesh);
		}
	}

	// Token: 0x060034B4 RID: 13492 RVA: 0x000DD0F0 File Offset: 0x000DB2F0
	public void GenerateMesh()
	{
		this.FreeMesh();
		MeshFilter component = base.GetComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		mesh.name = "Plane (generated)";
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(this.Left - 0.5f, 0.5f - this.Top),
			new Vector3(0.5f - this.Right, 0.5f - this.Top),
			new Vector3(0.5f - this.Right, this.Bottom - 0.5f),
			new Vector3(this.Left - 0.5f, this.Bottom - 0.5f)
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(this.Left, 1f - this.Top),
			new Vector2(1f - this.Right, 1f - this.Top),
			new Vector2(1f - this.Right, this.Bottom),
			new Vector2(this.Left, this.Bottom)
		};
		Vector3[] normals = new Vector3[]
		{
			Vector3.back,
			Vector3.back,
			Vector3.back,
			Vector3.back
		};
		int[] triangles = new int[]
		{
			0,
			1,
			2,
			2,
			3,
			0
		};
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.normals = normals;
		mesh.triangles = triangles;
		component.mesh = mesh;
		this.m_mesh = mesh;
	}

	// Token: 0x04002F7F RID: 12159
	public float Left;

	// Token: 0x04002F80 RID: 12160
	public float Right;

	// Token: 0x04002F81 RID: 12161
	public float Top;

	// Token: 0x04002F82 RID: 12162
	public float Bottom;

	// Token: 0x04002F83 RID: 12163
	private Rect m_lastRect;

	// Token: 0x04002F84 RID: 12164
	private Mesh m_mesh;
}
