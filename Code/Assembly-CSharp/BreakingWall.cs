using System;
using UnityEngine;

// Token: 0x02000907 RID: 2311
[RequireComponent(typeof(HealthController))]
public class BreakingWall : MonoBehaviour, IDamageReciever
{
	// Token: 0x06003350 RID: 13136 RVA: 0x000D8363 File Offset: 0x000D6563
	private void Start()
	{
		this.m_healthController = base.GetComponent<HealthController>();
		this.m_healthController.OnHealthDepletedEvent += this.BreakWall;
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x000D8388 File Offset: 0x000D6588
	public void OnRecieveDamage(Damage damage)
	{
		this.m_healthController.Value -= damage.Amount;
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x000D83A2 File Offset: 0x000D65A2
	private void BreakWall()
	{
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x04002E4B RID: 11851
	private HealthController m_healthController;
}
