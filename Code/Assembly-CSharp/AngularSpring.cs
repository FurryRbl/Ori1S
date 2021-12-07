using System;
using UnityEngine;

// Token: 0x02000950 RID: 2384
public class AngularSpring : MonoBehaviour, ISuspendable
{
	// Token: 0x0600347A RID: 13434 RVA: 0x000DC5E8 File Offset: 0x000DA7E8
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_startRotation = this.m_transform.localEulerAngles.z;
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x000DC62C File Offset: 0x000DA82C
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x000DC634 File Offset: 0x000DA834
	public void FixedUpdate()
	{
		if (this.m_rigidbody.isKinematic)
		{
			return;
		}
		if (this.IsSuspended)
		{
			return;
		}
		float num = Mathf.DeltaAngle(this.m_transform.localEulerAngles.z, this.m_startRotation);
		this.m_rigidbody.angularVelocity += new Vector3(0f, 0f, this.Spring * num * Time.deltaTime * 0.0174f);
	}

	// Token: 0x17000841 RID: 2113
	// (get) Token: 0x0600347D RID: 13437 RVA: 0x000DC6B6 File Offset: 0x000DA8B6
	// (set) Token: 0x0600347E RID: 13438 RVA: 0x000DC6BE File Offset: 0x000DA8BE
	public bool IsSuspended { get; set; }

	// Token: 0x04002F58 RID: 12120
	private Transform m_transform;

	// Token: 0x04002F59 RID: 12121
	private Rigidbody m_rigidbody;

	// Token: 0x04002F5A RID: 12122
	private float m_startRotation;

	// Token: 0x04002F5B RID: 12123
	public float Spring;
}
