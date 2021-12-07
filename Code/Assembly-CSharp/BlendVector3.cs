using System;
using UnityEngine;

// Token: 0x020003D9 RID: 985
public class BlendVector3 : Blend<Vector3>
{
	// Token: 0x06001B02 RID: 6914 RVA: 0x00073B18 File Offset: 0x00071D18
	public BlendVector3(Func<float, float> ease) : base(ease, new Func<Vector3, Vector3, float, Vector3>(Vector3.Lerp))
	{
	}
}
