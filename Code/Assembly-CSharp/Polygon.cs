using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004ED RID: 1261
[ExecuteInEditMode]
[AddComponentMenu("Mesh/Polygon")]
public class Polygon : MonoBehaviour
{
	// Token: 0x14000038 RID: 56
	// (add) Token: 0x0600220D RID: 8717 RVA: 0x00094CB8 File Offset: 0x00092EB8
	// (remove) Token: 0x0600220E RID: 8718 RVA: 0x00094CD1 File Offset: 0x00092ED1
	public event Action OnPolygonUpdateComponents = delegate()
	{
	};

	// Token: 0x0600220F RID: 8719 RVA: 0x00094CEC File Offset: 0x00092EEC
	public void Awake()
	{
		if (!Application.isPlaying)
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			MeshCollider component2 = base.GetComponent<MeshCollider>();
			foreach (Polygon polygon in UnityEngine.Object.FindObjectsOfType(typeof(Polygon)))
			{
				MeshFilter component3 = polygon.GetComponent<MeshFilter>();
				MeshCollider component4 = polygon.GetComponent<MeshCollider>();
				if (polygon != this && component != null && component3 != null && component.sharedMesh == component3.sharedMesh)
				{
					component.sharedMesh = null;
					this.UpdateComponents();
				}
				if (polygon != this && component2 != null && component4 != null && component2.sharedMesh == component4.sharedMesh)
				{
					component2.sharedMesh = null;
					this.UpdateComponents();
				}
			}
		}
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x00094DEA File Offset: 0x00092FEA
	public void Start()
	{
		if (!Application.isPlaying)
		{
			this.UpdateComponents();
		}
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x00094DFC File Offset: 0x00092FFC
	public void UpdateComponents()
	{
		if (this.PolygonCollider.Enabled)
		{
			MeshCollider meshCollider = base.GetComponent<MeshCollider>();
			if (meshCollider == null)
			{
				meshCollider = base.gameObject.AddComponent<MeshCollider>();
			}
			if (meshCollider.sharedMesh == null || meshCollider.sharedMesh.name != "polygon")
			{
				meshCollider.sharedMesh = new Mesh();
				meshCollider.sharedMesh.name = "polygon";
			}
			this.GenerateMesh(this.PolygonCollider.GenerateFront, this.PolygonCollider.GenerateBack, this.PolygonCollider.GenerateSides, this.PolygonCollider.Extrude, this.PolygonCollider.Elevation, true, true, Vector2.one, Vector2.one, Vector2.one, this.ClosedShape, meshCollider.sharedMesh);
			meshCollider.enabled = !meshCollider.enabled;
			meshCollider.enabled = !meshCollider.enabled;
		}
		if (this.PolygonMesh.Enabled)
		{
			MeshFilter meshFilter = base.GetComponent<MeshFilter>();
			if (meshFilter == null)
			{
				meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (meshFilter.sharedMesh == null || meshFilter.sharedMesh.name != "polygon")
			{
				meshFilter.sharedMesh = new Mesh();
				meshFilter.sharedMesh.name = "polygon";
			}
			this.GenerateMesh(this.PolygonMesh.GenerateFront, this.PolygonMesh.GenerateBack, this.PolygonMesh.GenerateSides, this.PolygonMesh.Extrude, this.PolygonMesh.Elevation, true, true, this.FrontUVScale, this.BackUVScale, this.SideUVScale, this.ClosedShape, meshFilter.sharedMesh);
		}
		this.OnPolygonUpdateComponents();
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x00094FD4 File Offset: 0x000931D4
	private void GenerateMesh(bool front, bool back, bool sides, float extrude, float elevate, bool useNormals, bool useUVS, Vector2 frontUVScale, Vector2 backUVScale, Vector2 sideUVScale, bool closed, Mesh mesh)
	{
		mesh.Clear();
		if (this.Invert)
		{
			this.Points.Reverse();
		}
		if (this.Points.Count == 0)
		{
			return;
		}
		Vector3 b = Vector3.back * elevate;
		List<Vector3> list = new List<Vector3>();
		if (front)
		{
			foreach (Vector2 v in this.Points)
			{
				Vector3 a = v;
				list.Add(a + b);
			}
		}
		int count = list.Count;
		if (back)
		{
			foreach (Vector2 v2 in this.Points)
			{
				Vector3 a2 = v2;
				list.Add(a2 + Vector3.forward * extrude + b);
			}
		}
		int count2 = list.Count;
		if (sides)
		{
			foreach (Vector2 v3 in this.Points)
			{
				Vector3 a3 = v3;
				list.Add(a3 + b);
				list.Add(a3 + b);
			}
			foreach (Vector2 v4 in this.Points)
			{
				Vector3 a4 = v4;
				list.Add(a4 + Vector3.forward * extrude + b);
				list.Add(a4 + Vector3.forward * extrude + b);
			}
		}
		mesh.vertices = list.ToArray();
		if (useUVS)
		{
			List<Vector2> list2 = new List<Vector2>();
			float num = 0f;
			Vector2 b2 = this.Points[this.Points.Count - 1];
			foreach (Vector2 vector in this.Points)
			{
				num += (vector - b2).magnitude;
				b2 = vector;
			}
			if (front)
			{
				for (int i = 0; i < this.Points.Count; i++)
				{
					Vector2 vector2 = this.Points[i];
					list2.Add(new Vector2(vector2.x * frontUVScale.x, vector2.y * frontUVScale.y) + this.FrontUVOffset);
				}
			}
			if (back)
			{
				for (int j = 0; j < this.Points.Count; j++)
				{
					Vector2 vector3 = this.Points[j];
					list2.Add(new Vector2(vector3.x * backUVScale.x, vector3.y * backUVScale.y) + this.BackUVOffset);
				}
			}
			if (sides)
			{
				for (int k = 0; k < 2; k++)
				{
					Vector2 b3 = this.Points[0];
					float num2 = 0f;
					for (int l = 0; l < this.Points.Count; l++)
					{
						Vector2 vector4 = this.Points[l];
						num2 += (vector4 - b3).magnitude;
						list2.Add(new Vector2((float)k * extrude * sideUVScale.x, ((l != 0) ? num2 : num) * sideUVScale.y) + this.SideUVOffset);
						list2.Add(new Vector2((float)k * extrude * sideUVScale.x, num2 * sideUVScale.y) + this.SideUVOffset);
						b3 = vector4;
					}
				}
			}
			mesh.uv = list2.ToArray();
		}
		List<Vector2> list3 = new List<Vector2>();
		foreach (Vector2 v5 in this.Points)
		{
			Vector3 v6 = v5;
			list3.Add(v6);
		}
		List<int> list4 = new List<int>();
		bool flag;
		Triangulate.Process(ref list3, ref list4, out flag);
		if (!closed)
		{
			flag = false;
		}
		List<int> list5 = new List<int>();
		if (front)
		{
			foreach (int item in list4)
			{
				list5.Add(item);
			}
		}
		if (back)
		{
			for (int m = 0; m < list4.Count; m += 3)
			{
				list5.Add(list4[m + 2] + count);
				list5.Add(list4[m + 1] + count);
				list5.Add(list4[m] + count);
			}
		}
		if (sides)
		{
			int item2 = count2 + this.Points.Count * 0 + this.Points.Count * 2 - 1;
			int item3 = count2 + this.Points.Count * 2 + this.Points.Count * 2 - 1;
			for (int n = 0; n < this.Points.Count; n++)
			{
				int num3 = count2 + this.Points.Count * 0 + n * 2;
				int num4 = count2 + this.Points.Count * 2 + n * 2;
				if (closed || n != 0)
				{
					if (flag)
					{
						list5.Add(item2);
						list5.Add(num3);
						list5.Add(item3);
						list5.Add(num3);
						list5.Add(num4);
						list5.Add(item3);
					}
					else
					{
						list5.Add(item2);
						list5.Add(item3);
						list5.Add(num3);
						list5.Add(num3);
						list5.Add(item3);
						list5.Add(num4);
					}
				}
				item2 = num3 + 1;
				item3 = num4 + 1;
			}
		}
		mesh.triangles = list5.ToArray();
		if (useNormals)
		{
			List<Vector3> list6 = new List<Vector3>();
			if (front)
			{
				for (int num5 = 0; num5 < this.Points.Count; num5++)
				{
					list6.Add(Vector3.back);
				}
			}
			if (back)
			{
				for (int num6 = 0; num6 < this.Points.Count; num6++)
				{
					list6.Add(Vector3.forward);
				}
			}
			if (sides)
			{
				for (int num7 = 0; num7 < 2; num7++)
				{
					for (int num8 = 0; num8 < this.Points.Count; num8++)
					{
						Vector2 a5 = this.Points[(num8 + this.Points.Count - 1) % this.Points.Count];
						Vector2 vector5 = this.Points[num8];
						Vector2 b4 = this.Points[(num8 + 1) % this.Points.Count];
						Vector2 normalized = (a5 - vector5).normalized;
						Vector2 normalized2 = (vector5 - b4).normalized;
						Vector3 vector6 = new Vector3(normalized.y, -normalized.x, 0f);
						Vector2 vector7 = vector6.normalized;
						Vector3 vector8 = new Vector3(normalized2.y, -normalized2.x, 0f);
						Vector2 vector9 = vector8.normalized;
						Vector2 vector10 = (vector7 + vector9).normalized;
						if (flag)
						{
							vector10 *= -1f;
							vector7 *= -1f;
							vector9 *= -1f;
						}
						if (Vector2.Dot(vector7, vector9) > Mathf.Cos(0.017453292f * this.SmoothAngle))
						{
							list6.Add(vector10);
							list6.Add(vector10);
						}
						else
						{
							list6.Add(vector7);
							list6.Add(vector9);
						}
					}
				}
			}
		}
		if (this.Invert)
		{
			this.Points.Reverse();
		}
		Color[] array = new Color[mesh.vertexCount];
		for (int num9 = 0; num9 < array.Length; num9++)
		{
			array[num9] = Color.white;
		}
		mesh.colors = array;
		mesh.RecalculateBounds();
	}

	// Token: 0x04001C8D RID: 7309
	public static bool ModifyMode;

	// Token: 0x04001C8E RID: 7310
	public List<Vector2> Points = new List<Vector2>();

	// Token: 0x04001C8F RID: 7311
	public HashSet<int> Selected = new HashSet<int>();

	// Token: 0x04001C90 RID: 7312
	public float SmoothAngle = 35f;

	// Token: 0x04001C91 RID: 7313
	public Vector2 FrontUVScale = new Vector2(0.25f, 0.25f);

	// Token: 0x04001C92 RID: 7314
	public Vector2 FrontUVOffset = new Vector2(0f, 0f);

	// Token: 0x04001C93 RID: 7315
	public Vector2 BackUVScale = new Vector2(0.25f, 0.25f);

	// Token: 0x04001C94 RID: 7316
	public Vector2 BackUVOffset = new Vector2(0f, 0f);

	// Token: 0x04001C95 RID: 7317
	public Vector2 SideUVScale = new Vector2(0.25f, 0.25f);

	// Token: 0x04001C96 RID: 7318
	public Vector2 SideUVOffset = new Vector2(0f, 0f);

	// Token: 0x04001C97 RID: 7319
	public Polygon.ShapeData PolygonMesh = new Polygon.ShapeData(false);

	// Token: 0x04001C98 RID: 7320
	public Polygon.ShapeData PolygonCollider = new Polygon.ShapeData(true);

	// Token: 0x04001C99 RID: 7321
	public bool ClosedShape = true;

	// Token: 0x04001C9A RID: 7322
	public bool Invert;

	// Token: 0x04001C9B RID: 7323
	public int InsertBefore;

	// Token: 0x04001C9C RID: 7324
	public Mesh GeneratedRendererMesh;

	// Token: 0x04001C9D RID: 7325
	public Mesh GeneratedColliderMesh;

	// Token: 0x020004EE RID: 1262
	public enum State
	{
		// Token: 0x04001CA1 RID: 7329
		Normal,
		// Token: 0x04001CA2 RID: 7330
		Add,
		// Token: 0x04001CA3 RID: 7331
		Modify,
		// Token: 0x04001CA4 RID: 7332
		Dragging,
		// Token: 0x04001CA5 RID: 7333
		SelectionBox
	}

	// Token: 0x020004EF RID: 1263
	[Serializable]
	public class ShapeData
	{
		// Token: 0x06002214 RID: 8724 RVA: 0x00095940 File Offset: 0x00093B40
		public ShapeData(bool collider)
		{
			if (collider)
			{
				this.GenerateFront = false;
				this.GenerateBack = false;
				this.GenerateSides = true;
			}
			else
			{
				this.GenerateFront = true;
				this.GenerateBack = false;
				this.GenerateSides = false;
				this.Extrude = 0f;
				this.Elevation = 0f;
			}
		}

		// Token: 0x04001CA6 RID: 7334
		public bool GenerateFront = true;

		// Token: 0x04001CA7 RID: 7335
		public bool GenerateBack = true;

		// Token: 0x04001CA8 RID: 7336
		public bool GenerateSides = true;

		// Token: 0x04001CA9 RID: 7337
		public float Extrude = 1f;

		// Token: 0x04001CAA RID: 7338
		public float Elevation = 0.5f;

		// Token: 0x04001CAB RID: 7339
		public bool Enabled;
	}
}
