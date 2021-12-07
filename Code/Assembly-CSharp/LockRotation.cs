using System;
using UnityEngine;

// Token: 0x02000484 RID: 1156
public class LockRotation : MonoBehaviour
{
	// Token: 0x06001F98 RID: 8088 RVA: 0x0008B030 File Offset: 0x00089230
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x0008B03E File Offset: 0x0008923E
	private void Start()
	{
		this.m_originalRotation = base.transform.eulerAngles;
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x0008B051 File Offset: 0x00089251
	private void LateUpdate()
	{
		this.m_transform.eulerAngles = this.m_originalRotation;
	}

	// Token: 0x04001B31 RID: 6961
	private Transform m_transform;

	// Token: 0x04001B32 RID: 6962
	private Vector3 m_originalRotation;
}
