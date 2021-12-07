using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C0 RID: 1984
[ExecuteInEditMode]
public class SmoothLinesPlugin : MonoBehaviour
{
	// Token: 0x06002DB5 RID: 11701 RVA: 0x000C2FD6 File Offset: 0x000C11D6
	public void OnEnable()
	{
		this.CageStructureTool = base.GetComponent<CageStructureTool>();
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x000C2FE4 File Offset: 0x000C11E4
	public void SmoothLines()
	{
		this.CageStructureTool.FindAllLoops(this.Loops);
		this.Nodes.Clear();
		for (int i = 0; i < this.CageStructureTool.Vertices.Count; i++)
		{
			this.Nodes.Add(i, new SmoothLinesPlugin.NodeMetaData());
		}
		foreach (List<int> list in this.Loops)
		{
			if (list.Count >= 3)
			{
				for (int j = 0; j < list.Count; j++)
				{
					Vector3 position = this.CageStructureTool.VertexByIndex(list[(j + list.Count - 1) % list.Count]).Position;
					Vector3 position2 = this.CageStructureTool.VertexByIndex(list[j]).Position;
					Vector3 position3 = this.CageStructureTool.VertexByIndex(list[(j + list.Count + 1) % list.Count]).Position;
					SmoothLinesPlugin.NodeMetaData nodeMetaData = this.Nodes[list[j]];
					nodeMetaData.TangentIn = SmoothLinesPlugin.CalculateTangentIn(position2, position, position3, this.TangentMultiplier);
					nodeMetaData.TangentOut = SmoothLinesPlugin.CalculateTangentOut(position2, position, position3, this.TangentMultiplier);
				}
			}
		}
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x000C3164 File Offset: 0x000C1364
	public static Vector2 CalculateTangentOut(Vector3 position, Vector3 previousPosition, Vector3 nextPosition, float tangentMultiplier)
	{
		Vector3 normalized = (position - previousPosition + (nextPosition - position)).normalized;
		return normalized * (nextPosition - position).magnitude * tangentMultiplier;
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x000C31B0 File Offset: 0x000C13B0
	public static Vector2 CalculateTangentIn(Vector3 position, Vector3 previousPosition, Vector3 nextPosition, float tangentMultiplier)
	{
		Vector3 normalized = (position - previousPosition + (nextPosition - position)).normalized;
		return normalized * (previousPosition - position).magnitude * tangentMultiplier * -1f;
	}

	// Token: 0x04002914 RID: 10516
	public CageStructureTool CageStructureTool;

	// Token: 0x04002915 RID: 10517
	public List<List<int>> Loops = new List<List<int>>();

	// Token: 0x04002916 RID: 10518
	public Dictionary<int, SmoothLinesPlugin.NodeMetaData> Nodes = new Dictionary<int, SmoothLinesPlugin.NodeMetaData>();

	// Token: 0x04002917 RID: 10519
	public float TangentMultiplier = 0.3f;

	// Token: 0x020007C1 RID: 1985
	public class NodeMetaData
	{
		// Token: 0x04002918 RID: 10520
		public Vector3 TangentIn;

		// Token: 0x04002919 RID: 10521
		public Vector3 TangentOut;
	}
}
