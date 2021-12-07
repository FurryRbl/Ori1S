using System;
using UnityEngine;

// Token: 0x020005FE RID: 1534
[Serializable]
public class MortarWormEnemyProjectileSpawnerTransform
{
	// Token: 0x06002676 RID: 9846 RVA: 0x000A8B30 File Offset: 0x000A6D30
	public Vector3 FindPosition(Vector3 direction)
	{
		float num = Mathf.Cos(0.3926991f);
		if (Vector3.Dot(direction, Vector3.up) > num)
		{
			return this.Up.position;
		}
		Vector3 vector = new Vector3(-1f, 1f);
		if (Vector3.Dot(direction, vector.normalized) > num)
		{
			return this.UpLeft.position;
		}
		Vector3 vector2 = new Vector3(1f, 1f);
		if (Vector3.Dot(direction, vector2.normalized) > num)
		{
			return this.UpRight.position;
		}
		if (Vector3.Dot(direction, Vector3.left) > 0f)
		{
			return this.Left.position;
		}
		return this.Right.position;
	}

	// Token: 0x0400210D RID: 8461
	public Transform Left;

	// Token: 0x0400210E RID: 8462
	public Transform UpLeft;

	// Token: 0x0400210F RID: 8463
	public Transform Up;

	// Token: 0x04002110 RID: 8464
	public Transform UpRight;

	// Token: 0x04002111 RID: 8465
	public Transform Right;
}
