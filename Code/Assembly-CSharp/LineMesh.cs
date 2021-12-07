using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000982 RID: 2434
public class LineMesh : MonoBehaviour
{
	// Token: 0x1700085D RID: 2141
	// (get) Token: 0x06003548 RID: 13640 RVA: 0x000DF418 File Offset: 0x000DD618
	public Vector3 LastPosition
	{
		get
		{
			return this.Position[this.Position.Count - 1];
		}
	}

	// Token: 0x1700085E RID: 2142
	// (get) Token: 0x06003549 RID: 13641 RVA: 0x000DF432 File Offset: 0x000DD632
	public int Length
	{
		get
		{
			return this.Position.Count;
		}
	}

	// Token: 0x1700085F RID: 2143
	// (get) Token: 0x0600354A RID: 13642 RVA: 0x000DF440 File Offset: 0x000DD640
	public float WorldSpaceLength
	{
		get
		{
			float num = 0f;
			for (int i = 1; i < this.Position.Count; i++)
			{
				num += Vector3.Distance(this.Position[i - 1], this.Position[i]);
			}
			return num;
		}
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x000DF492 File Offset: 0x000DD692
	public void UpdateMesh()
	{
		this.m_mesh.Clear();
		this.UpdateVertices();
		this.UpdateTriangles();
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x000DF4AC File Offset: 0x000DD6AC
	public void Awake()
	{
		this.m_mesh = new Mesh();
		this.m_mesh.name = "lineMesh";
		this.UpdateVertices();
		this.UpdateTriangles();
		this.m_meshFilter = base.GetComponent<MeshFilter>();
		this.m_meshFilter.sharedMesh = this.m_mesh;
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x000DF500 File Offset: 0x000DD700
	private void UpdateTriangles()
	{
		int num = this.Position.Count * 6;
		if (this.Position.Count < 2)
		{
			this.m_mesh.triangles = new int[0];
			return;
		}
		if (this.m_triangles.Length != num)
		{
			this.m_triangles = new int[num];
		}
		int num2 = 0;
		for (int i = 0; i < 2 * this.Position.Count - 2; i += 2)
		{
			this.m_triangles[num2++] = i;
			this.m_triangles[num2++] = i + 1;
			this.m_triangles[num2++] = i + 3;
			this.m_triangles[num2++] = i;
			this.m_triangles[num2++] = i + 3;
			this.m_triangles[num2++] = i + 2;
		}
		this.m_mesh.triangles = this.m_triangles;
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x000DF5E4 File Offset: 0x000DD7E4
	private void UpdateVertices()
	{
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		int num = this.Position.Count * 2;
		if (this.Position.Count < 2)
		{
			num = 0;
		}
		if (this.m_vertices.Length != num)
		{
			this.m_vertices = new Vector3[num];
		}
		if (this.m_uvs.Length != num)
		{
			this.m_uvs = new Vector2[num];
		}
		if (this.m_normals.Length != num)
		{
			this.m_normals = new Vector3[num];
		}
		float num2 = 1f;
		if (this.UVMode == LineMesh.UVModeType.StretchEntireLine)
		{
			num2 = this.WorldSpaceLength;
		}
		Vector3 vector = (num != 0) ? this.Position[0] : Vector3.zero;
		Vector3 v = Vector3.zero;
		float num3 = 0f;
		for (int i = 0; i < num / 2; i++)
		{
			Vector3 vector2 = this.Position[i];
			Vector3 vector3;
			if (i == 0)
			{
				vector3 = Vector3.Cross((this.Position[1] - this.Position[0]).normalized, this.Normal) * this.Width * 0.5f;
			}
			else if (i == this.Position.Count - 1)
			{
				vector3 = Vector3.Cross((this.Position[i] - this.Position[i - 1]).normalized, this.Normal) * this.Width * 0.5f;
			}
			else
			{
				vector3 = Vector3.Cross(this.Position[i] - this.Position[i - 1], this.Normal).normalized * this.Width * 0.5f;
			}
			switch (this.UVMode)
			{
			case LineMesh.UVModeType.StretchPerPoint:
				num3 += 1f / (float)this.Position.Count;
				break;
			case LineMesh.UVModeType.StretchEntireLine:
				num3 += Vector3.Distance(vector2, vector) / num2;
				break;
			case LineMesh.UVModeType.StretchOverUnit:
				num3 += Vector3.Distance(vector2, vector);
				break;
			}
			if (i > 0)
			{
				Vector2 b = vector2;
				Vector2 v2 = vector3;
				Vector2 a = vector;
				Vector2 w = v;
				float num4 = Vector2Helper.Cross(a - b, w) / Vector2Helper.Cross(v2, w);
				if (num4 < 1f && num4 > 0f && this.PreventOverlap)
				{
					this.m_vertices[i * 2 + 1] = this.m_vertices[i * 2 - 1];
					this.m_uvs[i * 2 + 1] = this.m_uvs[i * 2 - 1];
				}
				else
				{
					this.m_vertices[i * 2 + 1] = worldToLocalMatrix.MultiplyPoint(vector2 + vector3);
					this.m_uvs[i * 2 + 1] = new Vector3(num3, 1f);
				}
				if (num4 > -1f && num4 < 0f && this.PreventOverlap)
				{
					this.m_vertices[i * 2] = this.m_vertices[i * 2 - 2];
					this.m_uvs[i * 2] = this.m_uvs[i * 2 - 2];
				}
				else
				{
					this.m_vertices[i * 2] = worldToLocalMatrix.MultiplyPoint(vector2 - vector3);
					this.m_uvs[i * 2] = new Vector2(num3, 0f);
				}
			}
			else
			{
				this.m_vertices[i * 2 + 1] = worldToLocalMatrix.MultiplyPoint(vector2 + vector3);
				this.m_vertices[i * 2] = worldToLocalMatrix.MultiplyPoint(vector2 - vector3);
				this.m_uvs[i * 2] = new Vector2(num3, 0f);
				this.m_uvs[i * 2 + 1] = new Vector3(num3, 1f);
			}
			vector = vector2;
			v = vector3;
		}
		this.m_mesh.vertices = this.m_vertices;
		this.m_mesh.uv = this.m_uvs;
	}

	// Token: 0x04002FE0 RID: 12256
	public bool PreventOverlap = true;

	// Token: 0x04002FE1 RID: 12257
	public float Width = 1f;

	// Token: 0x04002FE2 RID: 12258
	public List<Vector3> Position;

	// Token: 0x04002FE3 RID: 12259
	public Vector3 Normal = Vector3.back;

	// Token: 0x04002FE4 RID: 12260
	public LineMesh.UVModeType UVMode;

	// Token: 0x04002FE5 RID: 12261
	private Mesh m_mesh;

	// Token: 0x04002FE6 RID: 12262
	private MeshFilter m_meshFilter;

	// Token: 0x04002FE7 RID: 12263
	private Vector3[] m_vertices = new Vector3[0];

	// Token: 0x04002FE8 RID: 12264
	private Vector3[] m_normals = new Vector3[0];

	// Token: 0x04002FE9 RID: 12265
	private Vector2[] m_uvs = new Vector2[0];

	// Token: 0x04002FEA RID: 12266
	private int[] m_triangles = new int[0];

	// Token: 0x02000983 RID: 2435
	public enum UVModeType
	{
		// Token: 0x04002FEC RID: 12268
		StretchPerPoint,
		// Token: 0x04002FED RID: 12269
		StretchEntireLine,
		// Token: 0x04002FEE RID: 12270
		StretchOverUnit
	}
}
