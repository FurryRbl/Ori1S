using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200090A RID: 2314
public class LinearPath : MonoBehaviour
{
	// Token: 0x06003360 RID: 13152 RVA: 0x000D8AD5 File Offset: 0x000D6CD5
	public Vector3 LocalToWorld(Vector3 position)
	{
		return base.transform.TransformPoint(position);
	}

	// Token: 0x06003361 RID: 13153 RVA: 0x000D8AE4 File Offset: 0x000D6CE4
	public void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = this.PathColor;
		Vector3 from = base.transform.TransformPoint(this.Path[this.Path.Count - 1]);
		foreach (Vector3 position in this.Path)
		{
			Vector3 vector = base.transform.TransformPoint(position);
			Gizmos.DrawLine(from, vector);
			from = vector;
		}
		Gizmos.color = color;
	}

	// Token: 0x04002E6A RID: 11882
	public List<Vector3> Path = new List<Vector3>
	{
		Vector3.left,
		Vector3.left
	};

	// Token: 0x04002E6B RID: 11883
	public Color PathColor = Color.yellow;
}
