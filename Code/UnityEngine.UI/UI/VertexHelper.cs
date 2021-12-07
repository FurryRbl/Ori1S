using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x020000A0 RID: 160
	public class VertexHelper : IDisposable
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00018750 File Offset: 0x00016950
		public VertexHelper()
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000187B0 File Offset: 0x000169B0
		public VertexHelper(Mesh m)
		{
			this.m_Positions.AddRange(m.vertices);
			this.m_Colors.AddRange(m.colors32);
			this.m_Uv0S.AddRange(m.uv);
			this.m_Uv1S.AddRange(m.uv2);
			this.m_Normals.AddRange(m.normals);
			this.m_Tangents.AddRange(m.tangents);
			this.m_Indices.AddRange(m.GetIndices(0));
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000188C0 File Offset: 0x00016AC0
		public void Clear()
		{
			this.m_Positions.Clear();
			this.m_Colors.Clear();
			this.m_Uv0S.Clear();
			this.m_Uv1S.Clear();
			this.m_Normals.Clear();
			this.m_Tangents.Clear();
			this.m_Indices.Clear();
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001891C File Offset: 0x00016B1C
		public int currentVertCount
		{
			get
			{
				return this.m_Positions.Count;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001892C File Offset: 0x00016B2C
		public int currentIndexCount
		{
			get
			{
				return this.m_Indices.Count;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001893C File Offset: 0x00016B3C
		public void PopulateUIVertex(ref UIVertex vertex, int i)
		{
			vertex.position = this.m_Positions[i];
			vertex.color = this.m_Colors[i];
			vertex.uv0 = this.m_Uv0S[i];
			vertex.uv1 = this.m_Uv1S[i];
			vertex.normal = this.m_Normals[i];
			vertex.tangent = this.m_Tangents[i];
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000189B8 File Offset: 0x00016BB8
		public void SetUIVertex(UIVertex vertex, int i)
		{
			this.m_Positions[i] = vertex.position;
			this.m_Colors[i] = vertex.color;
			this.m_Uv0S[i] = vertex.uv0;
			this.m_Uv1S[i] = vertex.uv1;
			this.m_Normals[i] = vertex.normal;
			this.m_Tangents[i] = vertex.tangent;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018A38 File Offset: 0x00016C38
		public void FillMesh(Mesh mesh)
		{
			mesh.Clear();
			if (this.m_Positions.Count >= 65000)
			{
				throw new ArgumentException("Mesh can not have more than 65000 vertices");
			}
			mesh.SetVertices(this.m_Positions);
			mesh.SetColors(this.m_Colors);
			mesh.SetUVs(0, this.m_Uv0S);
			mesh.SetUVs(1, this.m_Uv1S);
			mesh.SetNormals(this.m_Normals);
			mesh.SetTangents(this.m_Tangents);
			mesh.SetTriangles(this.m_Indices, 0);
			mesh.RecalculateBounds();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public void Dispose()
		{
			ListPool<Vector3>.Release(this.m_Positions);
			ListPool<Color32>.Release(this.m_Colors);
			ListPool<Vector2>.Release(this.m_Uv0S);
			ListPool<Vector2>.Release(this.m_Uv1S);
			ListPool<Vector3>.Release(this.m_Normals);
			ListPool<Vector4>.Release(this.m_Tangents);
			ListPool<int>.Release(this.m_Indices);
			this.m_Positions = null;
			this.m_Colors = null;
			this.m_Uv0S = null;
			this.m_Uv1S = null;
			this.m_Normals = null;
			this.m_Tangents = null;
			this.m_Indices = null;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018B54 File Offset: 0x00016D54
		public void AddVert(Vector3 position, Color32 color, Vector2 uv0, Vector2 uv1, Vector3 normal, Vector4 tangent)
		{
			this.m_Positions.Add(position);
			this.m_Colors.Add(color);
			this.m_Uv0S.Add(uv0);
			this.m_Uv1S.Add(uv1);
			this.m_Normals.Add(normal);
			this.m_Tangents.Add(tangent);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018BAC File Offset: 0x00016DAC
		public void AddVert(Vector3 position, Color32 color, Vector2 uv0)
		{
			this.AddVert(position, color, uv0, Vector2.zero, VertexHelper.s_DefaultNormal, VertexHelper.s_DefaultTangent);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018BD4 File Offset: 0x00016DD4
		public void AddVert(UIVertex v)
		{
			this.AddVert(v.position, v.color, v.uv0, v.uv1, v.normal, v.tangent);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018C14 File Offset: 0x00016E14
		public void AddTriangle(int idx0, int idx1, int idx2)
		{
			this.m_Indices.Add(idx0);
			this.m_Indices.Add(idx1);
			this.m_Indices.Add(idx2);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018C48 File Offset: 0x00016E48
		public void AddUIVertexQuad(UIVertex[] verts)
		{
			int currentVertCount = this.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				this.AddVert(verts[i].position, verts[i].color, verts[i].uv0, verts[i].uv1, verts[i].normal, verts[i].tangent);
			}
			this.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			this.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018CD8 File Offset: 0x00016ED8
		public void AddUIVertexStream(List<UIVertex> verts, List<int> indices)
		{
			if (verts != null)
			{
				CanvasRenderer.AddUIVertexStream(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Normals, this.m_Tangents);
			}
			if (indices != null)
			{
				this.m_Indices.AddRange(indices);
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018D28 File Offset: 0x00016F28
		public void AddUIVertexTriangleStream(List<UIVertex> verts)
		{
			CanvasRenderer.SplitUIVertexStreams(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018D68 File Offset: 0x00016F68
		public void GetUIVertexStream(List<UIVertex> stream)
		{
			CanvasRenderer.CreateUIVertexStream(stream, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x040002AB RID: 683
		private List<Vector3> m_Positions = ListPool<Vector3>.Get();

		// Token: 0x040002AC RID: 684
		private List<Color32> m_Colors = ListPool<Color32>.Get();

		// Token: 0x040002AD RID: 685
		private List<Vector2> m_Uv0S = ListPool<Vector2>.Get();

		// Token: 0x040002AE RID: 686
		private List<Vector2> m_Uv1S = ListPool<Vector2>.Get();

		// Token: 0x040002AF RID: 687
		private List<Vector3> m_Normals = ListPool<Vector3>.Get();

		// Token: 0x040002B0 RID: 688
		private List<Vector4> m_Tangents = ListPool<Vector4>.Get();

		// Token: 0x040002B1 RID: 689
		private List<int> m_Indices = ListPool<int>.Get();

		// Token: 0x040002B2 RID: 690
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x040002B3 RID: 691
		private static readonly Vector3 s_DefaultNormal = Vector3.back;
	}
}
