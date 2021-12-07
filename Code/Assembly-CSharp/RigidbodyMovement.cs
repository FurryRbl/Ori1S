using System;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public class RigidbodyMovement : MonoBehaviour
{
	// Token: 0x0600219C RID: 8604 RVA: 0x00093494 File Offset: 0x00091694
	public void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x000934A2 File Offset: 0x000916A2
	public void ApplyForce(Vector3 force)
	{
		this.m_rigidbody.velocity += force * Time.deltaTime;
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x000934C5 File Offset: 0x000916C5
	public void ApplyImpulseForce(Vector3 force)
	{
		this.m_rigidbody.velocity += force;
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x000934E0 File Offset: 0x000916E0
	public void ApplySpringForce(float forcePerUnit, Vector3 position)
	{
		this.m_rigidbody.velocity += (position - this.m_rigidbody.position) * forcePerUnit * Time.deltaTime;
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x00093524 File Offset: 0x00091724
	public void MultiplySpeedOverTime(float multiplier)
	{
		this.m_rigidbody.velocity *= Mathf.Pow(multiplier, Time.deltaTime);
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x00093548 File Offset: 0x00091748
	public void ApplyDrag(float drag)
	{
		this.m_rigidbody.velocity -= this.m_rigidbody.velocity * drag * Time.deltaTime;
	}

	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x060021A2 RID: 8610 RVA: 0x00093586 File Offset: 0x00091786
	// (set) Token: 0x060021A3 RID: 8611 RVA: 0x00093593 File Offset: 0x00091793
	public Vector3 Velocity
	{
		get
		{
			return this.m_rigidbody.velocity;
		}
		set
		{
			this.m_rigidbody.velocity = value;
		}
	}

	// Token: 0x04001C47 RID: 7239
	private Rigidbody m_rigidbody;
}
