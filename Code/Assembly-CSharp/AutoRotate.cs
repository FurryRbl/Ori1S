using System;
using UnityEngine;

// Token: 0x0200092C RID: 2348
public class AutoRotate : Suspendable, ISerializable, IDynamicGraphicHierarchy
{
	// Token: 0x060033F6 RID: 13302 RVA: 0x000DA728 File Offset: 0x000D8928
	private void Start()
	{
		this.m_startRotationZ = base.transform.eulerAngles.z;
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x000DA750 File Offset: 0x000D8950
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_currentTime += Time.deltaTime;
		base.transform.eulerAngles = new Vector3(0f, 0f, this.Speed * this.m_currentTime + this.m_startRotationZ);
	}

	// Token: 0x17000833 RID: 2099
	// (get) Token: 0x060033F8 RID: 13304 RVA: 0x000DA7A9 File Offset: 0x000D89A9
	// (set) Token: 0x060033F9 RID: 13305 RVA: 0x000DA7B1 File Offset: 0x000D89B1
	public override bool IsSuspended { get; set; }

	// Token: 0x060033FA RID: 13306 RVA: 0x000DA7BA File Offset: 0x000D89BA
	public void Serialize(Archive ar)
	{
		ar.Serialize(this.m_currentTime);
	}

	// Token: 0x04002EE5 RID: 12005
	public float Speed = 50f;

	// Token: 0x04002EE6 RID: 12006
	private float m_startRotationZ;

	// Token: 0x04002EE7 RID: 12007
	private float m_currentTime;
}
