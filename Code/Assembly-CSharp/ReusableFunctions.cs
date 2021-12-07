using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public static class ReusableFunctions
{
	// Token: 0x060010A7 RID: 4263 RVA: 0x0004C378 File Offset: 0x0004A578
	public static List<Vector3> GenerateVerticesFromPointList(List<Vector3> points, float thickness)
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < points.Count; i++)
		{
			Vector3 pointA = points[(i + points.Count - 1) % points.Count];
			Vector3 vector = points[i];
			Vector3 pointC = points[(i + 1) % points.Count];
			Vector3 a = ReusableFunctions.NormalFromThreePoints(pointA, vector, pointC);
			list.Add(vector + a * thickness * 0.5f);
			list.Add(vector - a * thickness * 0.5f);
		}
		return list;
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0004C41C File Offset: 0x0004A61C
	public static List<int> GenerateTriangleLineStrip(int pointCount)
	{
		int num = pointCount * 2;
		List<int> list = new List<int>();
		for (int i = 0; i < pointCount; i++)
		{
			list.Add(i * 2 % num);
			list.Add((i * 2 + 2) % num);
			list.Add((i * 2 + 1) % num);
			list.Add((i * 2 + 2) % num);
			list.Add((i * 2 + 3) % num);
			list.Add((i * 2 + 1) % num);
		}
		return list;
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0004C494 File Offset: 0x0004A694
	public static Vector3 NormalFromThreePoints(Vector3 pointA, Vector3 pointB, Vector3 pointC)
	{
		return (Vector3.Cross(Vector3.forward, pointB - pointA).normalized + (pointC - pointB).normalized).normalized;
	}
}
