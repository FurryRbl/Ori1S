using System;
using UnityEngine;

// Token: 0x02000740 RID: 1856
public class RigidbodySuspendable : Suspendable, IPooled
{
	// Token: 0x06002B94 RID: 11156 RVA: 0x000BB19C File Offset: 0x000B939C
	public void OnPoolSpawned()
	{
		this.m_angularVelocity = Vector3.zero;
		this.m_velocity = Vector3.zero;
		this.m_wasKinematic = false;
		this.m_isSuspended = false;
	}

	// Token: 0x06002B95 RID: 11157 RVA: 0x000BB1CD File Offset: 0x000B93CD
	public new void Awake()
	{
		base.Awake();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		if (this.m_rigidbody == null)
		{
			UnityEngine.Object.DestroyObject(this);
		}
	}

	// Token: 0x06002B96 RID: 11158 RVA: 0x000BB1F8 File Offset: 0x000B93F8
	public void Suspend()
	{
		this.m_wasKinematic = this.m_rigidbody.isKinematic;
		if (!this.m_wasKinematic)
		{
			this.m_velocity = this.m_rigidbody.velocity;
			this.m_angularVelocity = this.m_rigidbody.angularVelocity;
			this.m_rigidbody.isKinematic = true;
		}
		this.m_isSuspended = true;
	}

	// Token: 0x06002B97 RID: 11159 RVA: 0x000BB258 File Offset: 0x000B9458
	public void Resume()
	{
		if (!this.m_wasKinematic)
		{
			this.m_rigidbody.isKinematic = false;
			this.m_rigidbody.velocity = this.m_velocity;
			this.m_rigidbody.angularVelocity = this.m_angularVelocity;
		}
		this.m_isSuspended = false;
	}

	// Token: 0x170006F2 RID: 1778
	// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000BB2BE File Offset: 0x000B94BE
	// (set) Token: 0x06002B98 RID: 11160 RVA: 0x000BB2A5 File Offset: 0x000B94A5
	public override bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			if (value)
			{
				this.Suspend();
			}
			else
			{
				this.Resume();
			}
		}
	}

	// Token: 0x04002758 RID: 10072
	private Rigidbody m_rigidbody;

	// Token: 0x04002759 RID: 10073
	private Vector3 m_angularVelocity;

	// Token: 0x0400275A RID: 10074
	private Vector3 m_velocity;

	// Token: 0x0400275B RID: 10075
	private bool m_wasKinematic;

	// Token: 0x0400275C RID: 10076
	private bool m_isSuspended;
}
