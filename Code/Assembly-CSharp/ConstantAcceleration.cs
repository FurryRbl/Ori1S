using System;
using UnityEngine;

// Token: 0x0200095B RID: 2395
public class ConstantAcceleration : MonoBehaviour
{
	// Token: 0x060034BB RID: 13499 RVA: 0x000DD68D File Offset: 0x000DB88D
	public void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x000DD69B File Offset: 0x000DB89B
	public void FixedUpdate()
	{
		this.m_rigidbody.AddForceSafe(this.Acceleration, ForceMode.Acceleration);
	}

	// Token: 0x04002F89 RID: 12169
	public Vector3 Acceleration;

	// Token: 0x04002F8A RID: 12170
	private Rigidbody m_rigidbody;
}
