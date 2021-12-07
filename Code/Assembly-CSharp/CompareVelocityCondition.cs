using System;
using UnityEngine;

// Token: 0x020003BD RID: 957
public class CompareVelocityCondition : Condition
{
	// Token: 0x06001A96 RID: 6806 RVA: 0x00072973 File Offset: 0x00070B73
	public void Awake()
	{
		this.m_rigidBody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x00072984 File Offset: 0x00070B84
	public override bool Validate(IContext context)
	{
		return this.m_rigidBody.velocity.magnitude > this.Speed;
	}

	// Token: 0x04001711 RID: 5905
	public float Speed;

	// Token: 0x04001712 RID: 5906
	private Rigidbody m_rigidBody;
}
