using System;
using Game;

// Token: 0x020002B8 RID: 696
[Category("Ori")]
public class ActivateOriAction : ActionMethod
{
	// Token: 0x060015E6 RID: 5606 RVA: 0x00060F9F File Offset: 0x0005F19F
	public override void Perform(IContext context)
	{
		Characters.Ori.gameObject.SetActive(this.ShouldActivate);
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x00060FB6 File Offset: 0x0005F1B6
	public override string GetNiceName()
	{
		return (!this.ShouldActivate) ? "Deactivate Ori" : "Activate Ori";
	}

	// Token: 0x040012C8 RID: 4808
	public bool ShouldActivate = true;
}
