using System;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public class FixedRotation : MonoBehaviour, IPooled
{
	// Token: 0x06001F92 RID: 8082 RVA: 0x0008AF66 File Offset: 0x00089166
	public void OnPoolSpawned()
	{
		this.m_originalRotation = Quaternion.identity;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x0008AF73 File Offset: 0x00089173
	private void Start()
	{
		this.m_originalRotation = base.transform.rotation;
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x0008AF88 File Offset: 0x00089188
	private void FixedUpdate()
	{
		if (base.transform.rotation != this.m_originalRotation)
		{
			base.transform.rotation = this.m_originalRotation;
		}
	}

	// Token: 0x04001B30 RID: 6960
	private Quaternion m_originalRotation = Quaternion.identity;
}
