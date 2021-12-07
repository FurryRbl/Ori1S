using System;
using UnityEngine;

// Token: 0x02000892 RID: 2194
public class PositionOnWorldMap : MonoBehaviour
{
	// Token: 0x0600314A RID: 12618 RVA: 0x000D1DC9 File Offset: 0x000CFFC9
	private void FixedUpdate()
	{
		base.transform.position = WorldMapUI.Instance.WorldToUIPosition(this.Position);
	}

	// Token: 0x04002C9B RID: 11419
	public Vector3 Position;
}
