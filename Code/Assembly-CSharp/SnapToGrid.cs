using System;
using UnityEngine;

// Token: 0x020009A0 RID: 2464
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{
	// Token: 0x060035B3 RID: 13747 RVA: 0x000E1660 File Offset: 0x000DF860
	private void Start()
	{
		if (Application.isPlaying)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x060035B4 RID: 13748 RVA: 0x000E1678 File Offset: 0x000DF878
	private void Update()
	{
		base.transform.localScale = SnapToGrid.Round(base.transform.localScale, SnapToGrid.Grid);
		base.transform.localPosition = SnapToGrid.Round(base.transform.localPosition, SnapToGrid.Grid);
	}

	// Token: 0x060035B5 RID: 13749 RVA: 0x000E16C8 File Offset: 0x000DF8C8
	private static Vector3 Round(Vector3 vector, Vector3 grid)
	{
		return new Vector3(SnapToGrid.Round(vector.x, grid.x), SnapToGrid.Round(vector.y, grid.y), SnapToGrid.Round(vector.z, grid.z));
	}

	// Token: 0x060035B6 RID: 13750 RVA: 0x000E1713 File Offset: 0x000DF913
	private static float Round(float v, float x)
	{
		if (x == 0f)
		{
			return v;
		}
		return Mathf.Round(v * x) / x;
	}

	// Token: 0x04003054 RID: 12372
	public static Vector3 Grid = Vector3.one;
}
