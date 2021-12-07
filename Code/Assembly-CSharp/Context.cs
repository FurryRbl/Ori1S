using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public static class Context
{
	// Token: 0x060015AE RID: 5550 RVA: 0x00060070 File Offset: 0x0005E270
	public static void SendContextToGameObject(GameObject target, IContext context)
	{
		foreach (IContextReciever contextReciever in target.FindComponents<IContextReciever>())
		{
			contextReciever.OnReceiveContext(context);
		}
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x000600A8 File Offset: 0x0005E2A8
	public static void SendContextToGameObjectAndChildren(GameObject target, IContext context)
	{
		foreach (IContextReciever contextReciever in target.FindComponentsInChildren<IContextReciever>())
		{
			contextReciever.OnReceiveContext(context);
		}
	}
}
