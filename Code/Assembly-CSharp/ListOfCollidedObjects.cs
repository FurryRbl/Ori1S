using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class ListOfCollidedObjects : MonoBehaviour
{
	// Token: 0x060018AA RID: 6314 RVA: 0x00069CDF File Offset: 0x00067EDF
	public void OnCollisionEnter(Collision collision)
	{
		if (base.enabled)
		{
			this.CollisionObjects.Add(collision.gameObject);
		}
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x00069CFD File Offset: 0x00067EFD
	public void OnCollisionExit(Collision collision)
	{
		if (base.enabled)
		{
			this.CollisionObjects.Remove(collision.gameObject);
		}
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x00069D1C File Offset: 0x00067F1C
	public void OnTriggerEnter(Collider other)
	{
		if (base.enabled)
		{
			this.TriggerObjects.Add(other.gameObject);
		}
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x00069D3A File Offset: 0x00067F3A
	private void OnTriggerExit(Collider other)
	{
		if (base.enabled)
		{
			this.TriggerObjects.RemoveUnordered(other.gameObject);
		}
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x00069D59 File Offset: 0x00067F59
	public void OnEnable()
	{
		this.TriggerObjects.Clear();
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x00069D68 File Offset: 0x00067F68
	public void FixedUpdate()
	{
		if (this.m_removeFunc == null)
		{
			this.m_removeFunc = ((GameObject a) => !a);
		}
		this.TriggerObjects.RemoveAll(this.m_removeFunc);
	}

	// Token: 0x04001537 RID: 5431
	public List<GameObject> TriggerObjects = new List<GameObject>();

	// Token: 0x04001538 RID: 5432
	public List<GameObject> CollisionObjects = new List<GameObject>();

	// Token: 0x04001539 RID: 5433
	private Predicate<GameObject> m_removeFunc;
}
