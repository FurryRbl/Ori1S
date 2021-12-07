using System;
using Core;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class UnlockFullGameLogic : MonoBehaviour
{
	// Token: 0x06000E49 RID: 3657 RVA: 0x00041FBC File Offset: 0x000401BC
	public void FixedUpdate()
	{
		if (Core.Input.Copy.OnPressed && !Core.Input.Copy.Used && this.action)
		{
			this.action.Perform(null);
		}
	}

	// Token: 0x04000B7B RID: 2939
	public ActionMethod action;
}
