using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000987 RID: 2439
public class MovingStaticCollidersChecker : MonoBehaviour
{
	// Token: 0x06003561 RID: 13665 RVA: 0x000DFCBC File Offset: 0x000DDEBC
	public void FixedUpdate()
	{
		foreach (Collider collider in UnityEngine.Object.FindObjectsOfType(typeof(Collider)))
		{
			if (!this.m_reportedColliders.Contains(collider))
			{
				if (!collider.GetComponent<Rigidbody>())
				{
					Vector3 rhs;
					if (this.m_collidersPreviousPosition.TryGetValue(collider, out rhs))
					{
						if (collider.transform.position != rhs)
						{
							this.m_reportedColliders.Add(collider);
						}
					}
					else
					{
						this.m_collidersPreviousPosition.Add(collider, collider.transform.position);
					}
					Quaternion rhs2;
					if (this.m_collidersPreviousRotation.TryGetValue(collider, out rhs2))
					{
						if (collider.transform.rotation != rhs2)
						{
							this.m_reportedColliders.Add(collider);
						}
					}
					else
					{
						this.m_collidersPreviousRotation.Add(collider, collider.transform.rotation);
					}
					Vector3 rhs3;
					if (this.m_collidersPreviousScale.TryGetValue(collider, out rhs3))
					{
						if (collider.transform.localScale != rhs3)
						{
							this.m_reportedColliders.Add(collider);
						}
					}
					else
					{
						this.m_collidersPreviousScale.Add(collider, collider.transform.localScale);
					}
				}
			}
		}
	}

	// Token: 0x04002FF6 RID: 12278
	private Dictionary<Collider, Vector3> m_collidersPreviousPosition = new Dictionary<Collider, Vector3>();

	// Token: 0x04002FF7 RID: 12279
	private Dictionary<Collider, Quaternion> m_collidersPreviousRotation = new Dictionary<Collider, Quaternion>();

	// Token: 0x04002FF8 RID: 12280
	private Dictionary<Collider, Vector3> m_collidersPreviousScale = new Dictionary<Collider, Vector3>();

	// Token: 0x04002FF9 RID: 12281
	private HashSet<Collider> m_reportedColliders = new HashSet<Collider>();
}
