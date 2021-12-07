using System;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class ForceRigidBodyWakeUp : MonoBehaviour
{
	// Token: 0x0600102D RID: 4141 RVA: 0x00049AD8 File Offset: 0x00047CD8
	private void Awake()
	{
		foreach (Rigidbody rigidbody in base.GetComponents<Rigidbody>())
		{
			rigidbody.WakeUp();
		}
	}
}
