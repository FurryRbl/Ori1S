using System;
using UnityEngine;

// Token: 0x020005FD RID: 1533
public class MortarWormBlockedDetector : MonoBehaviour
{
	// Token: 0x0600266F RID: 9839 RVA: 0x000A8A9E File Offset: 0x000A6C9E
	public void OnValidate()
	{
		this.Mortar = base.transform.FindComponentUpwards<MortarWormEnemy>();
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x000A8AB1 File Offset: 0x000A6CB1
	public void Awake()
	{
		if (this.Mortar == null)
		{
			this.OnValidate();
		}
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x000A8ACA File Offset: 0x000A6CCA
	public void OnTriggerEnter(Collider collider)
	{
		this.OnTrigger(collider);
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x000A8AD3 File Offset: 0x000A6CD3
	public void OnTriggerStay(Collider collider)
	{
		this.OnTrigger(collider);
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x000A8ADC File Offset: 0x000A6CDC
	private void OnTrigger(Collider collider)
	{
		if (!collider.isTrigger && collider.GetComponent<Rigidbody>())
		{
			this.m_isBlocked = true;
		}
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x000A8B0B File Offset: 0x000A6D0B
	public void FixedUpdate()
	{
		this.Mortar.IsBlocked = this.m_isBlocked;
		this.m_isBlocked = false;
	}

	// Token: 0x0400210B RID: 8459
	public MortarWormEnemy Mortar;

	// Token: 0x0400210C RID: 8460
	private bool m_isBlocked;
}
