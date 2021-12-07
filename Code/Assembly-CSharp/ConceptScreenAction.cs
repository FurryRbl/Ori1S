using System;
using UnityEngine;

// Token: 0x020009E7 RID: 2535
public class ConceptScreenAction : ActionMethod
{
	// Token: 0x06003718 RID: 14104 RVA: 0x000E74F3 File Offset: 0x000E56F3
	public override void Perform(IContext context)
	{
		ConceptScreen.Instance.Activate(this.Texture);
	}

	// Token: 0x04003215 RID: 12821
	public Texture2D Texture;
}
