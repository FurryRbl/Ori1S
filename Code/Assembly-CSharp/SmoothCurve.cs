using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class SmoothCurve : MonoBehaviour
{
	// Token: 0x0600109B RID: 4251 RVA: 0x0004BFAE File Offset: 0x0004A1AE
	public void Start()
	{
		this.ApplyMesh();
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0004BFB8 File Offset: 0x0004A1B8
	public void AutoSmoothNodes()
	{
		for (int i = 0; i < this.Nodes.Count; i++)
		{
			this.AutoSmoothNode(i);
		}
		if (!this.ClosedShape && this.Nodes.Count > 0)
		{
			this.Nodes[0].TangentOut = Vector2.zero;
			this.Nodes[0].TangentIn = Vector2.zero;
			this.Nodes[this.Nodes.Count - 1].TangentIn = Vector2.zero;
			this.Nodes[this.Nodes.Count - 1].TangentOut = Vector2.zero;
		}
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0004C074 File Offset: 0x0004A274
	private void AutoSmoothNode(int nodeIndex)
	{
		SmoothCurve.PathNode node = this.Nodes[nodeIndex];
		SmoothCurve.PathNode nodeWrapped = this.GetNodeWrapped(nodeIndex - 1);
		SmoothCurve.PathNode nodeWrapped2 = this.GetNodeWrapped(nodeIndex + 1);
		this.AutoSmoothNode(node, nodeWrapped.Position, nodeWrapped2.Position);
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0004C0B8 File Offset: 0x0004A2B8
	public void AutoSmoothNode(SmoothCurve.PathNode node, Vector2 previousPosition, Vector2 nextPosition)
	{
		Vector3 a = (node.Position - previousPosition + (nextPosition - node.Position)).normalized;
		node.TangentOut = a * (nextPosition - node.Position).magnitude * this.TangentMultiplier;
		node.TangentIn = a * (previousPosition - node.Position).magnitude * this.TangentMultiplier * -1f;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0004C15C File Offset: 0x0004A35C
	public void ApplyMesh()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (component.sharedMesh == null)
		{
			component.sharedMesh = new Mesh();
			component.sharedMesh.name = "SmoothCurve";
		}
		Mesh sharedMesh = component.sharedMesh;
		this.GenerateMesh(sharedMesh);
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0004C1AC File Offset: 0x0004A3AC
	public void GenerateMesh(Mesh mesh)
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.Nodes.Count - 1; i++)
		{
			SmoothCurve.PathNode pathNode = this.Nodes[i];
			SmoothCurve.PathNode pathNode2 = this.Nodes[(i + 1) % this.Nodes.Count];
			for (int j = 0; j < 10; j++)
			{
				float r = (float)j / 10f;
				Vector3 a = pathNode.Position;
				Vector3 b = pathNode.Position + pathNode.TangentOut;
				Vector3 c = pathNode2.Position + pathNode2.TangentIn;
				Vector3 d = pathNode2.Position;
				list.Add(PathHelper.CalculateBeizer(a, b, c, d, r));
			}
		}
		List<Vector3> list2 = ReusableFunctions.GenerateVerticesFromPointList(list, 0.1f);
		mesh.vertices = list2.ToArray();
		mesh.triangles = ReusableFunctions.GenerateTriangleLineStrip(list.Count).ToArray();
		mesh.RecalculateBounds();
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0004C2C0 File Offset: 0x0004A4C0
	public SmoothCurve.PathNode GetNodeWrapped(int index)
	{
		int index2 = Mathf.RoundToInt(Mathf.Repeat((float)index, (float)this.Nodes.Count));
		return this.Nodes[index2];
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0004C2F2 File Offset: 0x0004A4F2
	public SmoothCurve.PathNode GetNode(int index)
	{
		return this.Nodes[index];
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0004C300 File Offset: 0x0004A500
	public int NodeCount
	{
		get
		{
			return this.Nodes.Count;
		}
	}

	// Token: 0x04000E10 RID: 3600
	public bool ClosedShape;

	// Token: 0x04000E11 RID: 3601
	public float TangentMultiplier = 0.3f;

	// Token: 0x04000E12 RID: 3602
	public List<SmoothCurve.PathNode> Nodes = new List<SmoothCurve.PathNode>();

	// Token: 0x020001C4 RID: 452
	[Serializable]
	public class PathNode
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0004C315 File Offset: 0x0004A515
		public bool TangentsAreLinked
		{
			get
			{
				return this.TangentIn == this.TangentOut * -1f;
			}
		}

		// Token: 0x04000E13 RID: 3603
		public Vector2 Position;

		// Token: 0x04000E14 RID: 3604
		public Vector2 TangentIn;

		// Token: 0x04000E15 RID: 3605
		public Vector2 TangentOut;
	}
}
