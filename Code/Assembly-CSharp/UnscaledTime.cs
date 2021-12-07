using System;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public static class UnscaledTime
{
	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00087AA8 File Offset: 0x00085CA8
	public static float deltaTime
	{
		get
		{
			return Time.deltaTime;
		}
	}

	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x06001ECD RID: 7885 RVA: 0x00087AAF File Offset: 0x00085CAF
	public static float fixedDeltaTime
	{
		get
		{
			return Time.fixedDeltaTime;
		}
	}
}
