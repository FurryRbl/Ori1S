using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009A5 RID: 2469
public class Trail : MonoBehaviour, IPooled, ISuspendable
{
	// Token: 0x060035CB RID: 13771 RVA: 0x000E1BDC File Offset: 0x000DFDDC
	public void OnPoolSpawned()
	{
		this.m_time = 0f;
		this.Data.Clear();
	}

	// Token: 0x060035CC RID: 13772 RVA: 0x000E1BF4 File Offset: 0x000DFDF4
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
		this.m_lineMesh = base.GetComponent<LineMesh>();
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x000E1C14 File Offset: 0x000DFE14
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x17000868 RID: 2152
	// (get) Token: 0x060035CE RID: 13774 RVA: 0x000E1C1C File Offset: 0x000DFE1C
	public Vector3 TargetPosition
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x000E1C2C File Offset: 0x000DFE2C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.Data.Count == 0 || Vector3.Distance(this.LastPosition, this.TargetPosition) > this.MinVertexDistance)
		{
			this.LastPosition = this.TargetPosition;
			this.Data.Insert(0, new Trail.TrailPosition(this.LastPosition, this.m_time));
		}
		Vector3 vector = this.Data[0].Position;
		float startTime = this.Data[0].StartTime;
		float num = 0f;
		Vector3 vector2 = this.TargetPosition;
		float a = startTime;
		float a2 = num;
		this.m_lineMesh.Position.Clear();
		for (int i = 0; i < this.Data.Count; i++)
		{
			vector = this.Data[i].Position;
			if (i == 0)
			{
				vector = this.TargetPosition;
			}
			startTime = this.Data[i].StartTime;
			num += Vector3.Distance(vector, vector2);
			if (this.Data[i].StartTime + this.MaxTime < this.m_time)
			{
				float t = Mathf.InverseLerp(a, startTime, this.m_time - this.MaxTime);
				this.m_lineMesh.Position.Add(Vector3.Lerp(vector2, vector, t));
				this.Data.RemoveRange(i + 1, this.Data.Count - i - 1);
				break;
			}
			if (num > this.MaxDistance)
			{
				float t2 = Mathf.InverseLerp(a2, num, this.MaxDistance);
				this.m_lineMesh.Position.Add(Vector3.Lerp(vector2, vector, t2));
				this.Data.RemoveRange(i + 1, this.Data.Count - i - 1);
				break;
			}
			this.m_lineMesh.Position.Add(vector);
			vector2 = vector;
			a = startTime;
			a2 = num;
		}
		this.m_lineMesh.UpdateMesh();
		this.m_time += Time.deltaTime;
	}

	// Token: 0x17000869 RID: 2153
	// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000E1E5A File Offset: 0x000E005A
	// (set) Token: 0x060035D1 RID: 13777 RVA: 0x000E1E62 File Offset: 0x000E0062
	public bool IsSuspended { get; set; }

	// Token: 0x0400305D RID: 12381
	private LineMesh m_lineMesh;

	// Token: 0x0400305E RID: 12382
	private Transform m_transform;

	// Token: 0x0400305F RID: 12383
	public float MinVertexDistance;

	// Token: 0x04003060 RID: 12384
	public float MaxTime = 10000f;

	// Token: 0x04003061 RID: 12385
	public float MaxDistance = 2f;

	// Token: 0x04003062 RID: 12386
	private float m_time;

	// Token: 0x04003063 RID: 12387
	public List<Trail.TrailPosition> Data = new List<Trail.TrailPosition>();

	// Token: 0x04003064 RID: 12388
	public Vector3 LastPosition;

	// Token: 0x020009A6 RID: 2470
	[Serializable]
	public struct TrailPosition
	{
		// Token: 0x060035D2 RID: 13778 RVA: 0x000E1E6B File Offset: 0x000E006B
		public TrailPosition(Vector3 position, float startTime)
		{
			this.Position = position;
			this.StartTime = startTime;
		}

		// Token: 0x04003066 RID: 12390
		public Vector3 Position;

		// Token: 0x04003067 RID: 12391
		public float StartTime;
	}
}
