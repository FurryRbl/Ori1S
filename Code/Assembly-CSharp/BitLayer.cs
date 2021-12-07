using System;
using UnityEngine;

// Token: 0x020006DD RID: 1757
public static class BitLayer
{
	// Token: 0x06002A0C RID: 10764 RVA: 0x000B4D35 File Offset: 0x000B2F35
	public static bool ContainsLayer(this LayerMask layerMask, int layer)
	{
		return (1 << layer & layerMask.value) != 0;
	}
}
