using System;
using UnityEngine;

// Token: 0x020002DF RID: 735
[Category("General")]
public class EnableComponentAction : ActionMethod
{
	// Token: 0x06001670 RID: 5744 RVA: 0x00062B03 File Offset: 0x00060D03
	public override void Perform(IContext context)
	{
		this.Target.enabled = this.ShouldEnable;
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x00062B18 File Offset: 0x00060D18
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.Target.enabled = ar.Serialize(this.Target.enabled);
		}
		else
		{
			ar.Serialize(this.Target.enabled);
		}
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x00062B64 File Offset: 0x00060D64
	public override string GetNiceName()
	{
		if (this.Target == null)
		{
			return "Enable component action";
		}
		if (this.ShouldEnable)
		{
			return "Enable " + this.Target.GetType().Name + " component";
		}
		return "Disable " + this.Target.GetType().Name + " component";
	}

	// Token: 0x04001361 RID: 4961
	[NotNull]
	public MonoBehaviour Target;

	// Token: 0x04001362 RID: 4962
	public bool ShouldEnable = true;
}
