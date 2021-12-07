using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000470 RID: 1136
public class SpiritLightDarknessZone : MonoBehaviour
{
	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0008A3B8 File Offset: 0x000885B8
	public Bounds WorldSpaceBoundingBox
	{
		get
		{
			Vector3 localScale = base.transform.localScale;
			localScale.z = 100f;
			return new Bounds(base.transform.position, localScale);
		}
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x0008A3F0 File Offset: 0x000885F0
	public static bool IsInsideDarknessZone(Vector3 worldPosition)
	{
		for (int i = 0; i < SpiritLightDarknessZone.All.Count; i++)
		{
			SpiritLightDarknessZone spiritLightDarknessZone = SpiritLightDarknessZone.All[i];
			if (spiritLightDarknessZone.WorldSpaceBoundingBox.Contains(worldPosition))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x0008A43B File Offset: 0x0008863B
	public void OnEnable()
	{
		SpiritLightDarknessZone.All.Add(this);
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x0008A448 File Offset: 0x00088648
	public void OnDisable()
	{
		SpiritLightDarknessZone.All.Remove(this);
	}

	// Token: 0x04001B04 RID: 6916
	public static List<SpiritLightDarknessZone> All = new List<SpiritLightDarknessZone>();
}
