using System;
using Game;
using UnityEngine;

// Token: 0x020000BA RID: 186
[ExecuteInEditMode]
public class WaterZone : MonoBehaviour
{
	// Token: 0x060007D5 RID: 2005 RVA: 0x00021A33 File Offset: 0x0001FC33
	public void OnEnable()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Zones.WaterZones.Add(this);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00021A4C File Offset: 0x0001FC4C
	public static bool PositionInWater(Vector3 position)
	{
		int count = Zones.WaterZones.Count;
		for (int i = 0; i < count; i++)
		{
			WaterZone waterZone = Zones.WaterZones[i];
			if (waterZone.Bounds.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00021A96 File Offset: 0x0001FC96
	public void OnDisable()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Zones.WaterZones.Remove(this);
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00021AB0 File Offset: 0x0001FCB0
	public void FixedUpdate()
	{
		if (this.m_previousPosition != base.transform.position || this.m_previousScale != base.transform.lossyScale)
		{
			this.Bounds = new Rect(base.transform.position.x - base.transform.lossyScale.x * 0.5f, base.transform.position.y - base.transform.lossyScale.y * 0.5f, base.transform.lossyScale.x, base.transform.lossyScale.y);
			this.m_previousPosition = base.transform.position;
			this.m_previousScale = base.transform.lossyScale;
		}
	}

	// Token: 0x0400064D RID: 1613
	public int Damage = 20;

	// Token: 0x0400064E RID: 1614
	public Rect Bounds;

	// Token: 0x0400064F RID: 1615
	public bool HasTopSurface = true;

	// Token: 0x04000650 RID: 1616
	private Vector3 m_previousPosition;

	// Token: 0x04000651 RID: 1617
	private Vector3 m_previousScale;
}
