using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class StopRunningInDirectionZone : MonoBehaviour
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x0600018E RID: 398 RVA: 0x000073DE File Offset: 0x000055DE
	// (set) Token: 0x0600018F RID: 399 RVA: 0x000073E6 File Offset: 0x000055E6
	public Rect Bounds { get; set; }

	// Token: 0x06000190 RID: 400 RVA: 0x000073F0 File Offset: 0x000055F0
	public void Awake()
	{
		StopRunningInDirectionZone.All.Add(this);
		this.Bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000741E File Offset: 0x0000561E
	public void OnDestroy()
	{
		StopRunningInDirectionZone.All.Remove(this);
	}

	// Token: 0x04000139 RID: 313
	public static List<StopRunningInDirectionZone> All = new List<StopRunningInDirectionZone>();

	// Token: 0x0400013A RID: 314
	public bool StopLeft;
}
