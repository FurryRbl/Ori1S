using System;

// Token: 0x0200087D RID: 2173
public class EnableMapAction : ActionMethod
{
	// Token: 0x0600310A RID: 12554 RVA: 0x000D0FFF File Offset: 0x000CF1FF
	public override void Perform(IContext context)
	{
	}

	// Token: 0x0600310B RID: 12555 RVA: 0x000D1001 File Offset: 0x000CF201
	public override string GetNiceName()
	{
		if (this.Enabled)
		{
			return "Enable Map";
		}
		return "Disable Map";
	}

	// Token: 0x04002C57 RID: 11351
	public bool Enabled = true;
}
