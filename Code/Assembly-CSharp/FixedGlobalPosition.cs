using System;
using UnityEngine;

// Token: 0x0200096A RID: 2410
public class FixedGlobalPosition : MonoBehaviour
{
	// Token: 0x060034EB RID: 13547 RVA: 0x000DE198 File Offset: 0x000DC398
	public void FixedUpdate()
	{
		base.transform.position = new Vector3(this.X, this.Y, this.Z);
	}

	// Token: 0x04002FA1 RID: 12193
	public float X;

	// Token: 0x04002FA2 RID: 12194
	public float Y;

	// Token: 0x04002FA3 RID: 12195
	public float Z;
}
