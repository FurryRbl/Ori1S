using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000647 RID: 1607
public class ForceLightTorchStopChasingZone : MonoBehaviour
{
	// Token: 0x0600274E RID: 10062 RVA: 0x000AB37D File Offset: 0x000A957D
	public void Awake()
	{
		this.m_rect = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x000AB395 File Offset: 0x000A9595
	public void OnEnable()
	{
		ForceLightTorchStopChasingZone.All.Add(this);
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x000AB3A2 File Offset: 0x000A95A2
	public void OnDisable()
	{
		ForceLightTorchStopChasingZone.All.Remove(this);
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x000AB3B0 File Offset: 0x000A95B0
	public static bool IsInside(Vector3 position)
	{
		using (List<ForceLightTorchStopChasingZone>.Enumerator enumerator = ForceLightTorchStopChasingZone.All.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				ForceLightTorchStopChasingZone forceLightTorchStopChasingZone = enumerator.Current;
				return forceLightTorchStopChasingZone.m_rect.Contains(position);
			}
		}
		return false;
	}

	// Token: 0x040021E9 RID: 8681
	private Rect m_rect;

	// Token: 0x040021EA RID: 8682
	public static List<ForceLightTorchStopChasingZone> All = new List<ForceLightTorchStopChasingZone>();
}
