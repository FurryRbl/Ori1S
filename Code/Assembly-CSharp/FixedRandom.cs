using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class FixedRandom
{
	// Token: 0x060005BE RID: 1470 RVA: 0x00016CC0 File Offset: 0x00014EC0
	public static float ValueFromPosition(Vector3 position)
	{
		return FixedRandom.Values[FixedRandom.IndexFromPosition(position)];
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00016CD0 File Offset: 0x00014ED0
	public static int IndexFromPosition(Vector3 position)
	{
		int value = Mathf.FloorToInt(Mathf.PerlinNoise(position.x, position.y) * (float)(FixedRandom.Values.Length - 1));
		return Mathf.Clamp(value, 0, FixedRandom.Values.Length - 1);
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00016D13 File Offset: 0x00014F13
	public static int Range(int min, int max, int valuesIndex)
	{
		return (int)Mathf.Lerp((float)min, (float)max, FixedRandom.Values[valuesIndex]);
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00016D26 File Offset: 0x00014F26
	public static float Range(float min, float max, int valuesIndex)
	{
		return Mathf.Lerp(min, max, FixedRandom.Values[valuesIndex]);
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x00016D36 File Offset: 0x00014F36
	public static T GetRandomListItem<T>(List<T> list, int valuesIndex) where T : class
	{
		return list[FixedRandom.Range(0, list.Count, valuesIndex)];
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00016D4B File Offset: 0x00014F4B
	public static T GetRandomArrayItem<T>(T[] list, int valuesIndex) where T : class
	{
		return list[FixedRandom.Range(0, list.Length, valuesIndex)];
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00016D60 File Offset: 0x00014F60
	public static void UpdateValues()
	{
		UnityEngine.Random.seed = FixedRandom.FixedUpdateIndex;
		for (int i = 0; i < FixedRandom.Values.Length; i++)
		{
			FixedRandom.Values[i] = UnityEngine.Random.value;
		}
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00016D9B File Offset: 0x00014F9B
	public static void SetFixedUpdateIndex(int index)
	{
		FixedRandom.FixedUpdateIndex = index;
		FixedRandom.UpdateValues();
	}

	// Token: 0x04000475 RID: 1141
	public static float[] Values = new float[10];

	// Token: 0x04000476 RID: 1142
	public static int FixedUpdateIndex = -1;
}
