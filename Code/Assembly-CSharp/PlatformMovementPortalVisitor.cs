using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class PlatformMovementPortalVisitor : MonoBehaviour, IPortalVisitor
{
	// Token: 0x06001CEE RID: 7406 RVA: 0x0007E8E1 File Offset: 0x0007CAE1
	public void Awake()
	{
		this.m_platformingMovement = base.GetComponent<PlatformMovement>();
		PortalVistor.All.Add(this);
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x0007E8FA File Offset: 0x0007CAFA
	public void OnDestroy()
	{
		PortalVistor.All.Remove(this);
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0007E91F File Offset: 0x0007CB1F
	// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x0007E907 File Offset: 0x0007CB07
	public Vector3 Position
	{
		get
		{
			return this.m_platformingMovement.Position;
		}
		set
		{
			this.m_platformingMovement.Position = value;
		}
	}

	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0007E93F File Offset: 0x0007CB3F
	// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x0007E92C File Offset: 0x0007CB2C
	public Vector3 Speed
	{
		get
		{
			return this.m_platformingMovement.WorldSpeed;
		}
		set
		{
			this.m_platformingMovement.WorldSpeed = value;
		}
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x0007E951 File Offset: 0x0007CB51
	public void OnGoThroughPortal()
	{
		this.OnGoThroughPortalAction();
		if (this.m_platformingMovement.LocalSpeedY < -this.MaxFallSpeed)
		{
			this.m_platformingMovement.LocalSpeedY = -this.MaxFallSpeed;
		}
		CameraFrustumOptimizer.ForceUpdate();
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x0007E98C File Offset: 0x0007CB8C
	public void OnPortalOverlapEnter()
	{
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0007E98E File Offset: 0x0007CB8E
	public void OnPortalOverlapExit()
	{
	}

	// Token: 0x04001921 RID: 6433
	private PlatformMovement m_platformingMovement;

	// Token: 0x04001922 RID: 6434
	public float MaxFallSpeed = 20f;

	// Token: 0x04001923 RID: 6435
	public Action OnGoThroughPortalAction = delegate()
	{
	};
}
