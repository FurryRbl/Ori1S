using System;
using UnityEngine;

// Token: 0x02000555 RID: 1365
public class MovingGroundHelper
{
	// Token: 0x060023A4 RID: 9124 RVA: 0x0009C1F1 File Offset: 0x0009A3F1
	public void SetGround(Transform ground)
	{
		if (ground != this.Ground)
		{
			this.Ground = ground;
		}
		this.UpdateGroundMatrix();
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x0009C214 File Offset: 0x0009A414
	public Vector2 CalculateDelta(Transform target)
	{
		if (this.Ground == null)
		{
			return Vector2.zero;
		}
		Vector3 position = target.position;
		Vector3 a = this.Ground.localToWorldMatrix.MultiplyPoint(this.GroundMatrix.MultiplyPoint(position));
		return a - position;
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x0009C26B File Offset: 0x0009A46B
	public void Update()
	{
		this.UpdateGroundMatrix();
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x0009C273 File Offset: 0x0009A473
	private void UpdateGroundMatrix()
	{
		if (this.Ground)
		{
			this.GroundMatrix = this.Ground.worldToLocalMatrix;
		}
	}

	// Token: 0x04001DE4 RID: 7652
	public Matrix4x4 GroundMatrix;

	// Token: 0x04001DE5 RID: 7653
	public Transform Ground;
}
