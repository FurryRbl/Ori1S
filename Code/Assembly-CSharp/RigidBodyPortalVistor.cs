using System;
using UnityEngine;

// Token: 0x020008D8 RID: 2264
public class RigidBodyPortalVistor : MonoBehaviour, IPortalVisitor
{
	// Token: 0x06003269 RID: 12905 RVA: 0x000D52B5 File Offset: 0x000D34B5
	public void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		PortalVistor.All.Add(this);
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x000D52CE File Offset: 0x000D34CE
	public void OnDestroy()
	{
		PortalVistor.All.Remove(this);
	}

	// Token: 0x17000805 RID: 2053
	// (get) Token: 0x0600326C RID: 12908 RVA: 0x000D52E9 File Offset: 0x000D34E9
	// (set) Token: 0x0600326B RID: 12907 RVA: 0x000D52DB File Offset: 0x000D34DB
	public Vector3 Position
	{
		get
		{
			return this.m_rigidbody.position;
		}
		set
		{
			this.m_rigidbody.position = value;
		}
	}

	// Token: 0x17000806 RID: 2054
	// (get) Token: 0x0600326D RID: 12909 RVA: 0x000D52F6 File Offset: 0x000D34F6
	// (set) Token: 0x0600326E RID: 12910 RVA: 0x000D5303 File Offset: 0x000D3503
	public Vector3 Speed
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

	// Token: 0x0600326F RID: 12911 RVA: 0x000D5311 File Offset: 0x000D3511
	public void OnGoThroughPortal()
	{
	}

	// Token: 0x06003270 RID: 12912 RVA: 0x000D5313 File Offset: 0x000D3513
	public void OnPortalOverlapEnter()
	{
	}

	// Token: 0x06003271 RID: 12913 RVA: 0x000D5315 File Offset: 0x000D3515
	public void OnPortalOverlapExit()
	{
	}

	// Token: 0x04002D5C RID: 11612
	private Rigidbody m_rigidbody;
}
