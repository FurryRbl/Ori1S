using System;
using UnityEngine;

// Token: 0x0200090B RID: 2315
[Serializable]
internal class PathFollower
{
	// Token: 0x1700081B RID: 2075
	// (get) Token: 0x06003363 RID: 13155 RVA: 0x000D8B94 File Offset: 0x000D6D94
	public Vector3 WorldPosition
	{
		get
		{
			return this.m_position;
		}
	}

	// Token: 0x1700081C RID: 2076
	// (get) Token: 0x06003364 RID: 13156 RVA: 0x000D8B9C File Offset: 0x000D6D9C
	public float WorldAngle
	{
		get
		{
			return this.m_angle;
		}
	}

	// Token: 0x06003365 RID: 13157 RVA: 0x000D8BA4 File Offset: 0x000D6DA4
	public void UpdateFollower(float dt)
	{
		this.Distance += this.Speed * dt;
		float num = 0f;
		bool flag = false;
		do
		{
			if (flag && this.Speed > 0f)
			{
				this.Distance -= num;
			}
			Vector3 vector = this.Path.LocalToWorld(this.Path.Path[this.Node]);
			Vector3 vector2 = this.Path.LocalToWorld((this.Node + 1 >= this.Path.Path.Count) ? this.Path.Path[0] : this.Path.Path[this.Node + 1]);
			Vector3 vector3 = vector2 - vector;
			vector3.Normalize();
			this.m_angle = 57.29578f * Mathf.Atan2(vector3.y, vector3.x);
			num = Vector3.Distance(vector, vector2);
			if (flag && this.Speed < 0f)
			{
				this.Distance += num;
			}
			flag = false;
			this.m_position = Vector3.Lerp(vector, vector2, this.Distance / num);
			if (this.Distance > num)
			{
				flag = true;
				this.Node++;
				if (this.Node >= this.Path.Path.Count)
				{
					this.Node = 0;
				}
			}
			if (this.Distance < 0f)
			{
				flag = true;
				this.Node--;
				if (this.Node < 0)
				{
					this.Node = this.Path.Path.Count - 1;
				}
			}
		}
		while (flag);
	}

	// Token: 0x04002E6C RID: 11884
	public LinearPath Path;

	// Token: 0x04002E6D RID: 11885
	public int Node;

	// Token: 0x04002E6E RID: 11886
	public float Distance;

	// Token: 0x04002E6F RID: 11887
	public float Speed;

	// Token: 0x04002E70 RID: 11888
	private Vector3 m_position;

	// Token: 0x04002E71 RID: 11889
	private float m_angle;
}
