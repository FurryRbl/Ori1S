using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class CageStructureTool : MonoBehaviour, IStrippable
{
	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06000CF0 RID: 3312 RVA: 0x0003B515 File Offset: 0x00039715
	// (remove) Token: 0x06000CF1 RID: 3313 RVA: 0x0003B52E File Offset: 0x0003972E
	public event Action<CageStructureTool.Vertex> OnAddVertex = delegate(CageStructureTool.Vertex A_0)
	{
	};

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06000CF2 RID: 3314 RVA: 0x0003B547 File Offset: 0x00039747
	// (remove) Token: 0x06000CF3 RID: 3315 RVA: 0x0003B560 File Offset: 0x00039760
	public event Action<CageStructureTool.Vertex> OnRemoveVertex = delegate(CageStructureTool.Vertex A_0)
	{
	};

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06000CF4 RID: 3316 RVA: 0x0003B579 File Offset: 0x00039779
	// (remove) Token: 0x06000CF5 RID: 3317 RVA: 0x0003B592 File Offset: 0x00039792
	public event Action<CageStructureTool.Edge> OnAddEdge = delegate(CageStructureTool.Edge A_0)
	{
	};

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06000CF6 RID: 3318 RVA: 0x0003B5AB File Offset: 0x000397AB
	// (remove) Token: 0x06000CF7 RID: 3319 RVA: 0x0003B5C4 File Offset: 0x000397C4
	public event Action<CageStructureTool.Edge> OnRemoveEdge = delegate(CageStructureTool.Edge A_0)
	{
	};

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06000CF8 RID: 3320 RVA: 0x0003B5DD File Offset: 0x000397DD
	// (remove) Token: 0x06000CF9 RID: 3321 RVA: 0x0003B5F6 File Offset: 0x000397F6
	public event Action<CageStructureTool.Face> OnAddFace = delegate(CageStructureTool.Face A_0)
	{
	};

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06000CFA RID: 3322 RVA: 0x0003B60F File Offset: 0x0003980F
	// (remove) Token: 0x06000CFB RID: 3323 RVA: 0x0003B628 File Offset: 0x00039828
	public event Action<CageStructureTool.Face> OnRemoveFace = delegate(CageStructureTool.Face A_0)
	{
	};

	// Token: 0x06000CFC RID: 3324 RVA: 0x0003B644 File Offset: 0x00039844
	public CageStructureTool.Vertex VertexByIndex(int index)
	{
		if (index >= 0 && index < this.Vertices.Count)
		{
			return this.Vertices[index];
		}
		return null;
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0003B678 File Offset: 0x00039878
	public void RemoveVertex(CageStructureTool.Vertex vertex, bool sendModified = true)
	{
		this.MarkDirty();
		int num = this.FindVertexIndex(vertex);
		if (num == -1)
		{
			return;
		}
		this.DisconnectVertexFromEdges(num);
		for (int i = 0; i < this.Edges.Count; i++)
		{
			CageStructureTool.Edge edge = this.Edges[i];
			if (edge.VertexA == num || edge.VertexB == num)
			{
				this.RemoveEdgeAtIndex(i, false);
				i--;
			}
		}
		foreach (CageStructureTool.Edge edge2 in this.Edges)
		{
			if (edge2.VertexA > num)
			{
				edge2.VertexA--;
			}
			if (edge2.VertexB > num)
			{
				edge2.VertexB--;
			}
		}
		for (int j = 0; j < this.Faces.Count; j++)
		{
			CageStructureTool.Face face = this.Faces[j];
			for (int k = 0; k < face.Vertices.Count; k++)
			{
				if (face.Vertices[k] == num)
				{
					face.Vertices.RemoveAt(k);
					k--;
				}
				else if (face.Vertices[k] > num)
				{
					List<int> vertices;
					List<int> list = vertices = face.Vertices;
					int num2;
					int index = num2 = k;
					num2 = vertices[num2];
					list[index] = num2 - 1;
				}
			}
			if (face.Vertices.Count < 3)
			{
				this.Faces.RemoveAt(j);
				j--;
			}
		}
		this.Vertices.Remove(vertex);
		this.OnRemoveVertex(vertex);
		if (sendModified)
		{
			this.OnModified();
		}
		this.UpdateFaceTriangulation();
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0003B874 File Offset: 0x00039A74
	private void DisconnectVertexFromEdges(int vertexIndex)
	{
		this.MarkDirty();
		List<CageStructureTool.Edge> list = (from edge in this.Edges
		where edge.VertexA == vertexIndex || edge.VertexB == vertexIndex
		select edge).ToList<CageStructureTool.Edge>();
		if (list.Count == 2)
		{
			if (list[0].VertexA == vertexIndex && list[1].VertexA == vertexIndex)
			{
				this.AddEdge(list[1].VertexB, list[0].VertexB, true);
			}
			else if (list[0].VertexA == vertexIndex && list[1].VertexB == vertexIndex)
			{
				this.AddEdge(list[1].VertexA, list[0].VertexB, true);
			}
			else if (list[0].VertexB == vertexIndex && list[1].VertexA == vertexIndex)
			{
				this.AddEdge(list[1].VertexB, list[0].VertexA, true);
			}
			else if (list[0].VertexB == vertexIndex && list[1].VertexB == vertexIndex)
			{
				this.AddEdge(list[1].VertexA, list[0].VertexA, true);
			}
		}
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0003BA04 File Offset: 0x00039C04
	public CageStructureTool.Edge AddEdge(CageStructureTool.Vertex vertexA, CageStructureTool.Vertex vertexB, bool sendModified = true)
	{
		int vertexA2 = this.FindVertexIndex(vertexA);
		int vertexB2 = this.FindVertexIndex(vertexB);
		return this.AddEdge(vertexA2, vertexB2, sendModified);
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0003BA2C File Offset: 0x00039C2C
	public CageStructureTool.Edge AddEdge(int vertexA, int vertexB, bool sendModified = true)
	{
		this.MarkDirty();
		CageStructureTool.Edge edge = new CageStructureTool.Edge(vertexA, vertexB, this.VerticeUniqueID++);
		this.Edges.Add(edge);
		this.OnAddEdge(edge);
		if (sendModified)
		{
			this.OnModified();
		}
		return edge;
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0003BA84 File Offset: 0x00039C84
	public bool HasEdge(CageStructureTool.Vertex vertexA, CageStructureTool.Vertex vertexB)
	{
		int num = this.FindVertexIndex(vertexA);
		int num2 = this.FindVertexIndex(vertexB);
		foreach (CageStructureTool.Edge edge in this.Edges)
		{
			if ((edge.VertexA == num && edge.VertexB == num2) || (edge.VertexA == num2 && edge.VertexB == num))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0003BB24 File Offset: 0x00039D24
	public bool RemoveEdge(CageStructureTool.Vertex vertexA, CageStructureTool.Vertex vertexB, bool sendModified = true)
	{
		int vertexIndexA = this.FindVertexIndex(vertexA);
		int vertexIndexB = this.FindVertexIndex(vertexB);
		int num = this.Edges.FindIndex((CageStructureTool.Edge edge) => (edge.VertexA == vertexIndexA && edge.VertexB == vertexIndexB) || (edge.VertexA == vertexIndexB && edge.VertexB == vertexIndexA));
		if (num != -1)
		{
			this.RemoveEdgeAtIndex(num, sendModified);
		}
		return false;
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0003BB79 File Offset: 0x00039D79
	public void RemoveEdge(CageStructureTool.Edge edge, bool sendModified = true)
	{
		this.RemoveEdgeAtIndex(this.FindEdgeIndex(edge), sendModified);
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0003BB8C File Offset: 0x00039D8C
	public void RemoveEdgeAtIndex(int edgeIndex, bool sendModified = true)
	{
		CageStructureTool.Edge edge = this.Edges[edgeIndex];
		this.Edges.RemoveAt(edgeIndex);
		this.MarkDirty();
		for (int i = 0; i < this.Faces.Count; i++)
		{
			CageStructureTool.Face face = this.Faces[i];
			if (face.Vertices.Contains(edge.VertexA) && face.Vertices.Contains(edge.VertexB))
			{
				this.Faces.RemoveAt(i);
				i--;
			}
		}
		this.UpdateConnections();
		foreach (int index in this.Connections[edge.VertexA].Values)
		{
			this.AutoFaceForEdge(this.Edges[index]);
		}
		foreach (int index2 in this.Connections[edge.VertexB].Values)
		{
			this.AutoFaceForEdge(this.Edges[index2]);
		}
		this.OnRemoveEdge(edge);
		if (sendModified)
		{
			this.OnModified();
		}
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0003BD14 File Offset: 0x00039F14
	public int FindVertexIndex(CageStructureTool.Vertex vertex)
	{
		return this.Vertices.FindIndex((CageStructureTool.Vertex a) => a == vertex);
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0003BD48 File Offset: 0x00039F48
	public int FindEdgeIndex(CageStructureTool.Edge edge)
	{
		return this.Edges.FindIndex((CageStructureTool.Edge a) => a == edge);
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0003BD7C File Offset: 0x00039F7C
	public CageStructureTool.Vertex AddVertex(Vector3 position, bool sendModified = true)
	{
		this.MarkDirty();
		CageStructureTool.Vertex vertex = new CageStructureTool.Vertex(position, this.VerticeUniqueID++);
		this.Vertices.Add(vertex);
		this.OnAddVertex(vertex);
		if (sendModified)
		{
			this.OnModified();
		}
		return vertex;
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0003BDD4 File Offset: 0x00039FD4
	public void MergeVertex(CageStructureTool.Vertex vertex, CageStructureTool.Vertex selectedVertex)
	{
		this.MarkDirty();
		int num = this.FindVertexIndex(selectedVertex);
		int num2 = this.FindVertexIndex(vertex);
		foreach (CageStructureTool.Edge edge in this.Edges)
		{
			if (edge.VertexA == num)
			{
				edge.VertexA = num2;
			}
			if (edge.VertexB == num)
			{
				edge.VertexB = num2;
			}
		}
		foreach (CageStructureTool.Face face in this.Faces)
		{
			for (int i = 0; i < face.Vertices.Count; i++)
			{
				if (face.Vertices[i] == num)
				{
					face.Vertices[i] = num2;
				}
			}
		}
		this.RemoveVertex(selectedVertex, false);
		this.OnModified();
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0003BF00 File Offset: 0x0003A100
	public void AddFace(List<int> faces, bool updateTriangles = true, bool sendModified = true)
	{
		this.MarkDirty();
		CageStructureTool.Face face = new CageStructureTool.Face(faces, this.FaceUniqueID++);
		this.Faces.Add(face);
		this.OnAddFace(face);
		if (updateTriangles)
		{
			this.UpdateFaceTriangulation();
		}
		if (sendModified)
		{
			this.OnModified();
		}
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0003BF60 File Offset: 0x0003A160
	public void RemoveFace(CageStructureTool.Face face)
	{
		this.MarkDirty();
		this.Faces.Remove(face);
		this.OnRemoveFace(face);
		this.OnModified();
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0003BF8C File Offset: 0x0003A18C
	public void Register(ICageMetaData cageMetaData)
	{
		this.MetaData.Add(cageMetaData);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0003BF9A File Offset: 0x0003A19A
	public void Unregister(ICageMetaData cageMetaData)
	{
		this.MetaData.Remove(cageMetaData);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0003BFA9 File Offset: 0x0003A1A9
	public void UpdateFaceTriangulation()
	{
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0003BFAC File Offset: 0x0003A1AC
	public Rect[] FacesAsRectangles
	{
		get
		{
			if (this.m_faceOptimisation == null || this.m_faceOptimisation.Length == 0 || this.m_faceOptimisation.Length != this.Faces.Count)
			{
				this.m_faceOptimisation = new Rect[this.Faces.Count];
				for (int i = 0; i < this.Faces.Count; i++)
				{
					CageStructureTool.Face face = this.Faces[i];
					Bounds bounds = default(Bounds);
					for (int j = 0; j < face.Triangles.Count; j += 3)
					{
						Vector3 position = this.Vertices[face.Vertices[face.Triangles[j]]].Position;
						Vector3 position2 = this.Vertices[face.Vertices[face.Triangles[j + 1]]].Position;
						Vector3 position3 = this.Vertices[face.Vertices[face.Triangles[j + 2]]].Position;
						if (j == 0)
						{
							bounds = new Bounds(position, Vector3.zero);
						}
						else
						{
							bounds.Encapsulate(position);
						}
						bounds.Encapsulate(position2);
						bounds.Encapsulate(position3);
					}
					this.m_faceOptimisation[i] = new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);
				}
			}
			return this.m_faceOptimisation;
		}
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0003C15C File Offset: 0x0003A35C
	public CageStructureTool.Face FindFaceAtPositionFaster(Vector3 position)
	{
		Rect[] facesAsRectangles = this.FacesAsRectangles;
		for (int i = 0; i < facesAsRectangles.Length; i++)
		{
			if (facesAsRectangles[i].Contains(position))
			{
				CageStructureTool.Face face = this.Faces[i];
				for (int j = 0; j < face.Triangles.Count; j += 3)
				{
					Vector3 position2 = this.Vertices[face.Vertices[face.Triangles[j]]].Position;
					Vector3 position3 = this.Vertices[face.Vertices[face.Triangles[j + 1]]].Position;
					Vector3 position4 = this.Vertices[face.Vertices[face.Triangles[j + 2]]].Position;
					if (CageMath.Vector.PointInTriangle(position, position2, position3, position4))
					{
						return face;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0003C268 File Offset: 0x0003A468
	public CageStructureTool.Face FindFaceAtPosition(Vector3 position)
	{
		foreach (CageStructureTool.Face face in this.Faces)
		{
			for (int i = 0; i < face.Triangles.Count; i += 3)
			{
				Vector3 position2 = this.Vertices[face.Vertices[face.Triangles[i]]].Position;
				Vector3 position3 = this.Vertices[face.Vertices[face.Triangles[i + 1]]].Position;
				Vector3 position4 = this.Vertices[face.Vertices[face.Triangles[i + 2]]].Position;
				if (CageMath.Vector.PointInTriangle(position, position2, position3, position4))
				{
					return face;
				}
			}
		}
		return null;
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0003C388 File Offset: 0x0003A588
	public void UpdateConnections()
	{
		this.Connections = new Dictionary<int, Dictionary<int, int>>();
		for (int i = 0; i < this.Vertices.Count; i++)
		{
			this.Connections.Add(i, new Dictionary<int, int>());
		}
		for (int j = 0; j < this.Edges.Count; j++)
		{
			CageStructureTool.Edge edge = this.Edges[j];
			Dictionary<int, int> dictionary = this.Connections[edge.VertexA];
			Dictionary<int, int> dictionary2 = this.Connections[edge.VertexB];
			if (!dictionary.ContainsKey(edge.VertexB))
			{
				dictionary.Add(edge.VertexB, j);
			}
			if (!dictionary2.ContainsKey(edge.VertexA))
			{
				dictionary2.Add(edge.VertexA, j);
			}
		}
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0003C45C File Offset: 0x0003A65C
	public void FindLoop(int start, int goal)
	{
		List<List<int>> list = new List<List<int>>();
		foreach (CageStructureTool.Face face in this.Faces)
		{
			List<int> list2 = new List<int>();
			foreach (int item in face.Vertices)
			{
				list2.Add(item);
			}
			list2.Sort();
			list.Add(list2);
		}
		this.UpdateConnections();
		this.Connections[start].Remove(goal);
		this.Connections[goal].Remove(start);
		List<int> list3 = new List<int>();
		HashSet<int> hashSet = new HashSet<int>();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		List<int> list4 = new List<int>();
		list3.Add(start);
		while (list3.Count > 0)
		{
			int num = list3[0];
			list3.RemoveAt(0);
			foreach (int num2 in this.Connections[num].Keys)
			{
				if (!hashSet.Contains(num2))
				{
					if (num2 == goal)
					{
						int num3 = num2;
						dictionary[num2] = num;
						while (num3 != start)
						{
							list4.Add(num3);
							num3 = dictionary[num3];
						}
						list4.Add(start);
						List<int> list5 = list4.ToList<int>();
						list5.Sort();
						bool flag = false;
						foreach (List<int> list6 in list)
						{
							if (list5.Count == list6.Count)
							{
								bool flag2 = true;
								for (int i = 0; i < list6.Count; i++)
								{
									if (list6[i] != list5[i])
									{
										flag2 = false;
									}
								}
								if (flag2)
								{
									flag = true;
								}
							}
						}
						if (!flag)
						{
							this.AddFace(list4, true, true);
							this.UpdateFaceTriangulation();
							return;
						}
						list4.Clear();
					}
					else
					{
						dictionary[num2] = num;
						list3.Add(num2);
						hashSet.Add(num2);
					}
				}
			}
		}
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0003C74C File Offset: 0x0003A94C
	public List<CageStructureTool.Vertex> FindVerticesFromEdges(List<CageStructureTool.Edge> edges)
	{
		HashSet<CageStructureTool.Vertex> hashSet = new HashSet<CageStructureTool.Vertex>();
		foreach (CageStructureTool.Edge edge in edges)
		{
			hashSet.Add(this.VertexByIndex(edge.VertexA));
			hashSet.Add(this.VertexByIndex(edge.VertexB));
		}
		return hashSet.ToList<CageStructureTool.Vertex>();
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0003C7CC File Offset: 0x0003A9CC
	public List<CageStructureTool.Edge> FindEdgesFromVertices(List<CageStructureTool.Vertex> vertices)
	{
		HashSet<CageStructureTool.Edge> hashSet = new HashSet<CageStructureTool.Edge>();
		HashSet<CageStructureTool.Vertex> hashSet2 = new HashSet<CageStructureTool.Vertex>(vertices);
		foreach (CageStructureTool.Edge edge in this.Edges)
		{
			if (hashSet2.Contains(this.VertexByIndex(edge.VertexA)) && hashSet2.Contains(this.VertexByIndex(edge.VertexB)))
			{
				hashSet.Add(edge);
			}
		}
		return hashSet.ToList<CageStructureTool.Edge>();
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0003C868 File Offset: 0x0003AA68
	public CageStructureTool.Edge FindEdgeFromVertices(int vertexa, int vertexb)
	{
		foreach (CageStructureTool.Edge edge in this.Edges)
		{
			if (edge.VertexA == vertexa)
			{
				if (edge.VertexB == vertexb)
				{
					return edge;
				}
			}
			else if (edge.VertexA == vertexb && edge.VertexB == vertexa)
			{
				return edge;
			}
		}
		return null;
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0003C904 File Offset: 0x0003AB04
	public List<CageStructureTool.Edge> FindEdgesFromVertices(List<int> vertices)
	{
		HashSet<CageStructureTool.Edge> hashSet = new HashSet<CageStructureTool.Edge>();
		HashSet<int> hashSet2 = new HashSet<int>(vertices);
		foreach (CageStructureTool.Edge edge in this.Edges)
		{
			if (hashSet2.Contains(edge.VertexA) && hashSet2.Contains(edge.VertexB))
			{
				hashSet.Add(edge);
			}
		}
		return hashSet.ToList<CageStructureTool.Edge>();
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0003C994 File Offset: 0x0003AB94
	[ContextMenu("Generate mesh")]
	public void GenerateMesh()
	{
		Mesh mesh = new Mesh();
		mesh.name = "cageStructureTool";
		base.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
		CageStructureTool.GenerateMesh(mesh, this);
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0003C9CC File Offset: 0x0003ABCC
	public static void GenerateMesh(Mesh mesh, CageStructureTool cageStructureTool)
	{
		List<Vector3> list = new List<Vector3>();
		List<Color> list2 = new List<Color>();
		List<Vector3> list3 = new List<Vector3>();
		List<int> list4 = new List<int>();
		foreach (CageStructureTool.Face face in cageStructureTool.Faces)
		{
			List<Vector2> list5 = new List<Vector2>();
			foreach (int index in face.Vertices)
			{
				list5.Add(cageStructureTool.VertexByIndex(index).Position);
			}
			List<int> list6 = new List<int>();
			bool flag;
			Triangulate.Process(ref list5, ref list6, out flag);
			int count = list.Count;
			Color item = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			foreach (Vector2 v in list5)
			{
				list.Add(v);
				list2.Add(item);
				list3.Add(Vector3.back);
			}
			foreach (int num in list6)
			{
				list4.Add(count + num);
			}
		}
		mesh.vertices = list.ToArray();
		mesh.triangles = list4.ToArray();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0003CBC8 File Offset: 0x0003ADC8
	public void FindAllLoops(List<List<int>> loops)
	{
		loops.Clear();
		this.UpdateConnections();
		HashSet<int> hashSet = new HashSet<int>();
		for (int i = 0; i < this.Vertices.Count; i++)
		{
			if (!hashSet.Contains(i))
			{
				List<int> list = new List<int>();
				list.Add(i);
				loops.Add(list);
				int num = i;
				bool flag = true;
				while (flag)
				{
					flag = false;
					if (!hashSet.Contains(num))
					{
						hashSet.Add(num);
						if (this.Connections[num].Count != 2)
						{
							list.Clear();
							break;
						}
						foreach (int num2 in this.Connections[num].Keys)
						{
							if (!hashSet.Contains(num2))
							{
								list.Add(num2);
								num = num2;
								flag = true;
								break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
	private void AddFaceFromEdgePath(List<int> path)
	{
		if (this.HasDuplicateEdge(path))
		{
			return;
		}
		List<int> list = new List<int>();
		foreach (int item in path)
		{
			list.Add(item);
		}
		IOrderedEnumerable<int> temp = from f in list
		orderby f
		select f;
		List<CageStructureTool.Face> faces = this.Faces;
		if (!faces.Any((CageStructureTool.Face faceIter) => (from v in faceIter.Vertices
		orderby v
		select v).SequenceEqual(temp)))
		{
			this.AddFace(list, true, true);
		}
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0003CDAC File Offset: 0x0003AFAC
	private bool HasDuplicateEdge(List<int> path)
	{
		HashSet<CageStructureTool.Edge> hashSet = new HashSet<CageStructureTool.Edge>();
		for (int i = 0; i < path.Count; i++)
		{
			int vertexa = path[i];
			int vertexb = path[(i + 1) % path.Count];
			CageStructureTool.Edge item = this.FindEdgeFromVertices(vertexa, vertexb);
			if (hashSet.Contains(item))
			{
				return true;
			}
			hashSet.Add(item);
		}
		return false;
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0003CE14 File Offset: 0x0003B014
	private void AutoFaceForEdge(CageStructureTool.Edge e)
	{
		if (!this.Edges.Contains(e) || this.Edges.Count < 3 || this.Vertices.Count < 3)
		{
			return;
		}
		int count = this.Connections[e.VertexA].Count;
		int count2 = this.Connections[e.VertexB].Count;
		if (count <= 1 || count2 <= 1)
		{
			return;
		}
		if (e.VertexA == e.VertexB)
		{
			return;
		}
		CageStructureTool.Vertex vertexStart = this.VertexByIndex(e.VertexA);
		List<int> list = new List<int>();
		list.Add(e.VertexA);
		float num = 0f;
		if (this.TraceFaceInDir(vertexStart, e.VertexA, e, list, 1f, ref num) && num > 0f)
		{
			this.AddFaceFromEdgePath(list);
		}
		list.Clear();
		list.Add(e.VertexA);
		num = 0f;
		if (this.TraceFaceInDir(vertexStart, e.VertexA, e, list, -1f, ref num) && num < 0f)
		{
			this.AddFaceFromEdgePath(list);
		}
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x0003CF40 File Offset: 0x0003B140
	public void AutoFace(CageStructureTool.Edge e, bool wide)
	{
		List<CageStructureTool.Face> faces = this.Faces;
		int vertexA = e.VertexA;
		int vertexB = e.VertexB;
		foreach (CageStructureTool.Face face in faces.ToArray())
		{
			if (wide)
			{
				if (face.Vertices.Contains(vertexA) || face.Vertices.Contains(vertexB))
				{
					this.RemoveFace(face);
				}
			}
			else if (face.Vertices.Contains(vertexA) && face.Vertices.Contains(vertexB))
			{
				this.RemoveFace(face);
			}
		}
		this.UpdateConnections();
		if (wide)
		{
			int count = this.Connections[e.VertexA].Count;
			int count2 = this.Connections[e.VertexB].Count;
			if (count > 1)
			{
				List<int> list = this.Connections[e.VertexA].Values.ToList<int>();
				foreach (int index in list)
				{
					this.AutoFaceForEdge(this.Edges[index]);
				}
			}
			if (count2 > 1)
			{
				List<int> list2 = this.Connections[e.VertexB].Values.ToList<int>();
				foreach (int index2 in list2)
				{
					this.AutoFaceForEdge(this.Edges[index2]);
				}
			}
		}
		else
		{
			this.AutoFaceForEdge(e);
		}
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0003D124 File Offset: 0x0003B324
	private CageStructureTool.Vertex SplitVertexConnection(int baseVertex, int skipConnection, Vector2 dir, float traceDir)
	{
		Dictionary<int, int> dictionary = this.Connections[baseVertex];
		CageStructureTool.Vertex vertex = this.VertexByIndex(baseVertex);
		if (dictionary.Count == 3)
		{
			int num = -1;
			float num2 = 0f;
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				if (keyValuePair.Key != skipConnection)
				{
					CageStructureTool.Vertex vertex2 = this.VertexByIndex(keyValuePair.Key);
					Vector3 walkDir = vertex2.Position - vertex.Position;
					float walkAngle = CageStructureTool.GetWalkAngle(dir, walkDir);
					if (traceDir > 0f)
					{
						if (walkAngle >= num2 || num == -1)
						{
							num2 = walkAngle;
							num = keyValuePair.Key;
						}
					}
					else if (walkAngle <= num2 || num == -1)
					{
						num2 = walkAngle;
						num = keyValuePair.Key;
					}
				}
			}
			if (num != -1)
			{
				CageStructureTool.Vertex vertex3 = this.AddVertex(vertex.Position, false);
				int vertexB = this.FindVertexIndex(vertex3);
				this.AddEdge(num, vertexB, false);
				CageStructureTool.Vertex vertexA = this.VertexByIndex(num);
				this.RemoveEdge(vertexA, vertex, false);
				return vertex3;
			}
		}
		return null;
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0003D26C File Offset: 0x0003B46C
	public void SplitAtEdge(CageStructureTool.Edge e)
	{
		int vertexA = e.VertexA;
		int vertexB = e.VertexB;
		this.UpdateConnections();
		int count = this.Connections[vertexA].Count;
		int count2 = this.Connections[vertexB].Count;
		if (count != 3 || count2 != 3)
		{
			return;
		}
		Vector2 vector = (this.VertexByIndex(vertexA).Position - this.VertexByIndex(vertexB).Position).normalized;
		CageStructureTool.Vertex vertexA2 = this.SplitVertexConnection(vertexA, vertexB, vector, 1f);
		CageStructureTool.Vertex vertexB2 = this.SplitVertexConnection(vertexB, vertexA, vector, 1f);
		CageStructureTool.Edge edge = this.AddEdge(vertexA2, vertexB2, false);
		Vector3 a = Vector3.Cross(vector, Vector3.forward);
		this.VertexByIndex(edge.VertexA).Position += a * 0.05f + vector * 0.05f;
		this.VertexByIndex(edge.VertexB).Position += a * 0.05f - vector * 0.05f;
		this.UpdateConnections();
		this.AutoFaceForEdge(edge);
		this.AutoFaceForEdge(e);
		this.OnModified();
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0003D3D0 File Offset: 0x0003B5D0
	private bool TraceFaceInDir(CageStructureTool.Vertex vertexStart, int prevVertex, CageStructureTool.Edge edge, List<int> path, float traceDir, ref float totalAngle)
	{
		int num = edge.VertexA;
		int num2 = edge.VertexB;
		if (num2 == prevVertex)
		{
			int num3 = num;
			num = num2;
			num2 = num3;
		}
		CageStructureTool.Vertex vertex = this.VertexByIndex(num);
		CageStructureTool.Vertex vertex2 = this.VertexByIndex(num2);
		Vector3 position = vertex.Position;
		Vector3 position2 = vertex2.Position;
		Vector3 normalized = (position2 - position).normalized;
		if (vertex2 == vertexStart)
		{
			CageStructureTool.Vertex vertex3 = (this.VertexByIndex(path[0]) != vertexStart) ? this.VertexByIndex(path[0]) : this.VertexByIndex(path[1]);
			Vector3 walkDir = vertex3.Position - vertexStart.Position;
			float walkAngle = CageStructureTool.GetWalkAngle(normalized, walkDir);
			totalAngle += walkAngle;
			return true;
		}
		path.Add(num2);
		Dictionary<int, int> dictionary = this.Connections[num2];
		int num4 = this.FindEdgeIndex(edge);
		float num5 = 0f;
		CageStructureTool.Edge edge2 = null;
		foreach (KeyValuePair<int, int> keyValuePair in dictionary)
		{
			int value = keyValuePair.Value;
			if (value != num4)
			{
				int key = keyValuePair.Key;
				CageStructureTool.Vertex vertex4 = this.VertexByIndex(key);
				if (key != prevVertex && vertex4 != vertex2 && vertex4 != vertex)
				{
					Vector3 walkDir2 = vertex4.Position - vertex2.Position;
					float walkAngle2 = CageStructureTool.GetWalkAngle(normalized, walkDir2);
					if (traceDir > 0f)
					{
						if (walkAngle2 >= num5 || edge2 == null)
						{
							num5 = walkAngle2;
							edge2 = this.Edges[value];
						}
					}
					else if (walkAngle2 <= num5 || edge2 == null)
					{
						num5 = walkAngle2;
						edge2 = this.Edges[value];
					}
				}
			}
		}
		totalAngle += num5;
		return edge2 != null && this.TraceFaceInDir(vertexStart, num2, edge2, path, traceDir, ref totalAngle);
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
	private static float GetWalkAngle(Vector3 dir, Vector3 walkDir)
	{
		float num = Quaternion.FromToRotation(dir, walkDir).eulerAngles.z;
		if (num > 180f)
		{
			num -= 360f;
		}
		return num;
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0003D624 File Offset: 0x0003B824
	public void Clear(bool sendModified = true)
	{
		this.Vertices.Clear();
		this.Edges.Clear();
		this.Faces.Clear();
		this.VerticeUniqueID = 0;
		this.EdgeUniqueID = 0;
		this.FaceUniqueID = 0;
		if (sendModified)
		{
			this.OnModified();
		}
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0003D678 File Offset: 0x0003B878
	public void MarkDirty()
	{
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0003D67C File Offset: 0x0003B87C
	public bool DoStrip()
	{
		return (!base.transform.parent || !base.transform.parent.name.Contains("AreaMap")) && !(base.transform.name == "worldMapLogic") && !(base.transform.name == "lightTorchZone") && !(base.transform.name == "saveInTheDarkZone");
	}

	// Token: 0x04000ABA RID: 2746
	public int VerticeUniqueID;

	// Token: 0x04000ABB RID: 2747
	public int EdgeUniqueID;

	// Token: 0x04000ABC RID: 2748
	public int FaceUniqueID;

	// Token: 0x04000ABD RID: 2749
	public int EditingMode;

	// Token: 0x04000ABE RID: 2750
	public List<ICageMetaData> MetaData = new List<ICageMetaData>();

	// Token: 0x04000ABF RID: 2751
	public List<CageStructureTool.Vertex> Vertices = new List<CageStructureTool.Vertex>();

	// Token: 0x04000AC0 RID: 2752
	public List<CageStructureTool.Edge> Edges = new List<CageStructureTool.Edge>();

	// Token: 0x04000AC1 RID: 2753
	public List<CageStructureTool.Face> Faces = new List<CageStructureTool.Face>();

	// Token: 0x04000AC2 RID: 2754
	public Action OnModified = delegate()
	{
	};

	// Token: 0x04000AC3 RID: 2755
	public Action<CageStructureTool.Face, bool, bool> OnDisplayFace = delegate(CageStructureTool.Face A_0, bool A_1, bool A_2)
	{
	};

	// Token: 0x04000AC4 RID: 2756
	public Action<CageStructureTool.Edge, bool, bool> OnDisplayEdge = delegate(CageStructureTool.Edge A_0, bool A_1, bool A_2)
	{
	};

	// Token: 0x04000AC5 RID: 2757
	public Action<CageStructureTool.Vertex, bool, bool> OnDisplayVertex = delegate(CageStructureTool.Vertex A_0, bool A_1, bool A_2)
	{
	};

	// Token: 0x04000AC6 RID: 2758
	private Rect[] m_faceOptimisation;

	// Token: 0x04000AC7 RID: 2759
	public Dictionary<int, Dictionary<int, int>> Connections;

	// Token: 0x02000148 RID: 328
	[Serializable]
	public class Face
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x0003D728 File Offset: 0x0003B928
		public Face(List<int> vertices, int id)
		{
			this.ID = id;
			this.Vertices = vertices;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003D75F File Offset: 0x0003B95F
		public int GetVertexIndexAtWrappedIndex(int i)
		{
			return this.Vertices[i % this.Vertices.Count];
		}

		// Token: 0x04000AD9 RID: 2777
		public List<int> Triangles = new List<int>();

		// Token: 0x04000ADA RID: 2778
		public List<int> Vertices = new List<int>();

		// Token: 0x04000ADB RID: 2779
		public int ID;
	}

	// Token: 0x0200047C RID: 1148
	[Serializable]
	public class Vertex
	{
		// Token: 0x06001F6F RID: 8047 RVA: 0x0008A7FE File Offset: 0x000889FE
		public Vertex(Vector3 position, int id)
		{
			this.ID = id;
			this.Position = position;
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0008A833 File Offset: 0x00088A33
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x0008A83B File Offset: 0x00088A3B
		public Vector3 ScreenPosition { get; set; }

		// Token: 0x04001B1E RID: 6942
		public Vector3 Position;

		// Token: 0x04001B1F RID: 6943
		public Vector4 Metadata = new Vector4(0f, 0.5f, 0f, 0f);

		// Token: 0x04001B20 RID: 6944
		public int ID;
	}

	// Token: 0x0200047D RID: 1149
	[Serializable]
	public class Edge
	{
		// Token: 0x06001F72 RID: 8050 RVA: 0x0008A844 File Offset: 0x00088A44
		public Edge(int vertexA, int vertexB, int id)
		{
			this.ID = id;
			this.VertexA = vertexA;
			this.VertexB = vertexB;
		}

		// Token: 0x04001B22 RID: 6946
		public int VertexA;

		// Token: 0x04001B23 RID: 6947
		public int VertexB;

		// Token: 0x04001B24 RID: 6948
		public int ID;
	}
}
