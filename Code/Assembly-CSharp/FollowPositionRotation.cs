using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class FollowPositionRotation : MonoBehaviour, IPooled
{
	// Token: 0x0600041A RID: 1050 RVA: 0x00011055 File Offset: 0x0000F255
	public void OnPoolSpawned()
	{
		this.m_target = null;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00011060 File Offset: 0x0000F260
	public void SetTarget(Transform target)
	{
		this.m_target = target;
		if (this.FollowRotation)
		{
			this.m_localRotation = Quaternion.Inverse(this.m_target.rotation) * base.transform.rotation;
		}
		this.m_localPosition = this.m_target.InverseTransformPoint(base.transform.position);
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x000110C4 File Offset: 0x0000F2C4
	public void FixedUpdate()
	{
		if (base.transform != null && this.m_target != null)
		{
			if (this.FollowRotation)
			{
				base.transform.rotation = this.m_localRotation * this.m_target.rotation;
			}
			base.transform.position = this.m_target.TransformPoint(this.m_localPosition);
		}
		if (this.m_target == null || !this.m_target.gameObject.activeInHierarchy)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000370 RID: 880
	public bool FollowRotation = true;

	// Token: 0x04000371 RID: 881
	[PooledSafe]
	private Quaternion m_localRotation;

	// Token: 0x04000372 RID: 882
	[PooledSafe]
	private Vector3 m_localPosition;

	// Token: 0x04000373 RID: 883
	private Transform m_target;
}
