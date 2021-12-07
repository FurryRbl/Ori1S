using System;
using UnityEngine;

// Token: 0x02000986 RID: 2438
public class MoveRandomly : SaveSerialize, ISuspendable
{
	// Token: 0x06003558 RID: 13656 RVA: 0x000DFBCF File Offset: 0x000DDDCF
	public override void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x000DFBD7 File Offset: 0x000DDDD7
	public void Start()
	{
		this.m_startPosition = base.transform.position;
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x000DFBEA File Offset: 0x000DDDEA
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x000DFBF4 File Offset: 0x000DDDF4
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		base.transform.position = this.m_startPosition + new Vector3(FixedRandom.Range(-this.Delta, this.Delta, 0), 0f);
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x000DFC40 File Offset: 0x000DDE40
	public override void Serialize(Archive ar)
	{
		base.transform.position = ar.Serialize(base.transform.position);
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x000DFC69 File Offset: 0x000DDE69
	public void OnCollisionEnter(Collision collision)
	{
	}

	// Token: 0x17000860 RID: 2144
	// (get) Token: 0x0600355E RID: 13662 RVA: 0x000DFC6B File Offset: 0x000DDE6B
	// (set) Token: 0x0600355F RID: 13663 RVA: 0x000DFC73 File Offset: 0x000DDE73
	public bool IsSuspended { get; set; }

	// Token: 0x04002FF3 RID: 12275
	private Vector3 m_startPosition;

	// Token: 0x04002FF4 RID: 12276
	public float Delta;
}
