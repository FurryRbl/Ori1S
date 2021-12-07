using System;
using UnityEngine;

// Token: 0x020001C7 RID: 455
[Serializable]
public class PathNode
{
	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x060010AB RID: 4267 RVA: 0x0004C4DE File Offset: 0x0004A6DE
	public bool TangentsAreLinked
	{
		get
		{
			return this.TangentIn == this.TangentOut * -1f;
		}
	}

	// Token: 0x04000E16 RID: 3606
	public Vector2 Position;

	// Token: 0x04000E17 RID: 3607
	public Vector2 TangentIn;

	// Token: 0x04000E18 RID: 3608
	public Vector2 TangentOut;
}
