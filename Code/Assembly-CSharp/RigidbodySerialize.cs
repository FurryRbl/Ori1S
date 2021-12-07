using System;
using UnityEngine;

// Token: 0x020006FC RID: 1788
public class RigidbodySerialize : SaveSerialize, IPooled, ISuspendable
{
	// Token: 0x06002A8F RID: 10895 RVA: 0x000B696B File Offset: 0x000B4B6B
	public void OnPoolSpawned()
	{
		this.m_angularVelocity = Vector3.zero;
		this.m_velocity = Vector3.zero;
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x000B6983 File Offset: 0x000B4B83
	public new void Awake()
	{
		SuspensionManager.Register(this);
		base.Awake();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		if (this.m_rigidbody == null)
		{
			UnityEngine.Object.DestroyObject(this);
		}
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x000B69B4 File Offset: 0x000B4BB4
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170006CB RID: 1739
	// (get) Token: 0x06002A93 RID: 10899 RVA: 0x000B69DB File Offset: 0x000B4BDB
	// (set) Token: 0x06002A92 RID: 10898 RVA: 0x000B69C2 File Offset: 0x000B4BC2
	public bool IsSuspended
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

	// Token: 0x06002A94 RID: 10900 RVA: 0x000B69E4 File Offset: 0x000B4BE4
	public override void Serialize(Archive ar)
	{
		if (this.SerializePosition)
		{
			base.transform.position = ar.Serialize(base.transform.position);
		}
		if (this.SerializeRotation)
		{
			base.transform.rotation = ar.Serialize(base.transform.rotation);
		}
		if (this.SeriailzeIsKinematic)
		{
			this.m_rigidbody.isKinematic = ar.Serialize(this.m_rigidbody.isKinematic);
		}
		if (!base.GetComponent<Rigidbody>().isKinematic)
		{
			if (this.SerializeVelocity)
			{
				this.m_rigidbody.velocity = ar.Serialize(this.m_rigidbody.velocity);
			}
			if (this.SerializeAngularVelocity)
			{
				this.m_rigidbody.angularVelocity = ar.Serialize(this.m_rigidbody.angularVelocity);
			}
		}
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x000B6AC4 File Offset: 0x000B4CC4
	public void Suspend()
	{
		if (!this.m_rigidbody.isKinematic)
		{
			this.m_velocity = this.m_rigidbody.velocity;
			this.m_angularVelocity = this.m_rigidbody.angularVelocity;
			this.m_rigidbody.velocity = Vector3.zero;
			this.m_rigidbody.angularVelocity = Vector3.zero;
			this.m_rigidbody.Sleep();
		}
		this.m_isSuspended = true;
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x000B6B38 File Offset: 0x000B4D38
	public void Resume()
	{
		if (!this.m_rigidbody.isKinematic)
		{
			this.m_rigidbody.velocity = this.m_velocity;
			this.m_rigidbody.angularVelocity = this.m_angularVelocity;
			this.m_rigidbody.WakeUp();
		}
		this.m_isSuspended = false;
	}

	// Token: 0x040025E4 RID: 9700
	private bool m_isSuspended;

	// Token: 0x040025E5 RID: 9701
	private Rigidbody m_rigidbody;

	// Token: 0x040025E6 RID: 9702
	private Vector3 m_angularVelocity;

	// Token: 0x040025E7 RID: 9703
	private Vector3 m_velocity;

	// Token: 0x040025E8 RID: 9704
	public bool SerializePosition = true;

	// Token: 0x040025E9 RID: 9705
	public bool SerializeRotation = true;

	// Token: 0x040025EA RID: 9706
	public bool SerializeVelocity = true;

	// Token: 0x040025EB RID: 9707
	public bool SerializeAngularVelocity = true;

	// Token: 0x040025EC RID: 9708
	public bool SeriailzeIsKinematic = true;
}
