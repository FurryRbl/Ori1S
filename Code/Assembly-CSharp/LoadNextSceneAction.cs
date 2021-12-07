using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
[Category("System")]
public class LoadNextSceneAction : ActionMethod
{
	// Token: 0x060016C6 RID: 5830 RVA: 0x000637E1 File Offset: 0x000619E1
	public override void Perform(IContext context)
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
