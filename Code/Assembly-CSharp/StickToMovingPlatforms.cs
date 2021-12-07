using System;
using Game;
using UnityEngine;

// Token: 0x020009A3 RID: 2467
public class StickToMovingPlatforms : MonoBehaviour
{
	// Token: 0x060035BC RID: 13756 RVA: 0x000E18E0 File Offset: 0x000DFAE0
	public void Awake()
	{
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060035BD RID: 13757 RVA: 0x000E18F8 File Offset: 0x000DFAF8
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060035BE RID: 13758 RVA: 0x000E1910 File Offset: 0x000DFB10
	public void OnRestoreCheckpoint()
	{
		this.m_ground = null;
	}

	// Token: 0x060035BF RID: 13759 RVA: 0x000E1919 File Offset: 0x000DFB19
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x060035C0 RID: 13760 RVA: 0x000E1922 File Offset: 0x000DFB22
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x060035C1 RID: 13761 RVA: 0x000E192C File Offset: 0x000DFB2C
	public void OnCollision(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			return;
		}
		if (collision.gameObject.GetComponent<PushPullBlock>())
		{
			return;
		}
		Vector3 a = Vector3.zero;
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			a += contactPoint.normal;
		}
		a.z = 0f;
		Transform transform = collision.transform;
		if (transform != this.m_ground)
		{
			this.m_ground = transform;
			this.UpdateGroundMatrix();
		}
		this.m_keepTracking = 3;
	}

	// Token: 0x060035C2 RID: 13762 RVA: 0x000E19DC File Offset: 0x000DFBDC
	public void UpdateGroundMatrix()
	{
		this.m_groundMatrix = this.m_ground.worldToLocalMatrix;
	}

	// Token: 0x060035C3 RID: 13763 RVA: 0x000E19F0 File Offset: 0x000DFBF0
	public void FixedUpdate()
	{
		if (this.m_ground)
		{
			Vector3 position = base.transform.position;
			Vector3 a = this.m_ground.localToWorldMatrix.MultiplyPoint(this.m_groundMatrix.MultiplyPoint(position));
			base.transform.position += a - position;
			this.UpdateGroundMatrix();
			this.m_keepTracking--;
			if (this.m_keepTracking == 0)
			{
				this.m_ground = null;
			}
		}
	}

	// Token: 0x04003056 RID: 12374
	private Transform m_ground;

	// Token: 0x04003057 RID: 12375
	private Matrix4x4 m_groundMatrix;

	// Token: 0x04003058 RID: 12376
	private int m_keepTracking;
}
