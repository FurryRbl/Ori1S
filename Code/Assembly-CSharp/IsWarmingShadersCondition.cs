using System;

// Token: 0x020002F3 RID: 755
public class IsWarmingShadersCondition : Condition
{
	// Token: 0x060016B7 RID: 5815 RVA: 0x00063520 File Offset: 0x00061720
	public override bool Validate(IContext context)
	{
		if (base.transform.root.name == "introLogos")
		{
			return UberShaderPrewarmer.IsComplete;
		}
		return UberShaderPrewarmer.IsComplete;
	}
}
