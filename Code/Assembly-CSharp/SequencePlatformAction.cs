using System;
using System.Collections.Generic;

// Token: 0x0200091C RID: 2332
public class SequencePlatformAction : ActionMethod
{
	// Token: 0x060033B9 RID: 13241 RVA: 0x000D9B48 File Offset: 0x000D7D48
	public override void Perform(IContext context)
	{
		this.m_found.Clear();
		SequencePlatformB sequencePlatformB = this.Sequence;
		if (!sequencePlatformB.Activated)
		{
			sequencePlatformB.Activated = true;
		}
		for (;;)
		{
			SequencePlatformB nextPlatform = sequencePlatformB.NextPlatform;
			if (!nextPlatform)
			{
				break;
			}
			if (this.m_found.Contains(nextPlatform))
			{
				break;
			}
			if (nextPlatform.Activated)
			{
				nextPlatform.Activated = false;
			}
			this.m_found.Add(nextPlatform);
			sequencePlatformB = nextPlatform;
		}
	}

	// Token: 0x04002EAA RID: 11946
	public SequencePlatformB Sequence;

	// Token: 0x04002EAB RID: 11947
	private readonly HashSet<SequencePlatformB> m_found = new HashSet<SequencePlatformB>();
}
