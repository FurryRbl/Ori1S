using System;
using UnityEngine;

// Token: 0x0200096E RID: 2414
public class FloatingRockController : MonoBehaviour
{
	// Token: 0x060034F4 RID: 13556 RVA: 0x000DE2C7 File Offset: 0x000DC4C7
	public void Awake()
	{
		this.m_startPosition = base.transform.position;
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x000DE2E8 File Offset: 0x000DC4E8
	public void OnEnable()
	{
		this.m_time = Mathf.PerlinNoise(base.transform.position.x, base.transform.position.y) * 500f;
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x000DE32C File Offset: 0x000DC52C
	public void FixedUpdate()
	{
		this.m_time += Time.deltaTime;
		Vector3 a = this.m_startPosition + new Vector3((Mathf.PerlinNoise(this.m_time, 0f) - 0.5f) * this.Radius * 2f, (Mathf.PerlinNoise(this.m_time, 10f) - 0.5f) * this.Radius * 2f);
		this.m_rigidbody.AddForce((a - base.transform.position).normalized * this.Acceleration, ForceMode.Acceleration);
		float num = Mathf.PerlinNoise(this.m_time, 50f) - 0.5f;
		this.m_rigidbody.AddTorque(0f, 0f, num * this.AngularAcceleration, ForceMode.Acceleration);
	}

	// Token: 0x04002FA9 RID: 12201
	public float Radius;

	// Token: 0x04002FAA RID: 12202
	private float m_time;

	// Token: 0x04002FAB RID: 12203
	private Vector3 m_startPosition;

	// Token: 0x04002FAC RID: 12204
	public float Acceleration;

	// Token: 0x04002FAD RID: 12205
	public float AngularAcceleration;

	// Token: 0x04002FAE RID: 12206
	private Rigidbody m_rigidbody;
}
