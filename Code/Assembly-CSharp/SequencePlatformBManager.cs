using System;
using System.Collections.Generic;

// Token: 0x0200091E RID: 2334
public class SequencePlatformBManager : SaveSerialize
{
	// Token: 0x060033C8 RID: 13256 RVA: 0x000DA0E1 File Offset: 0x000D82E1
	public override void Serialize(Archive ar)
	{
	}

	// Token: 0x04002EBD RID: 11965
	public List<SequencePlatformB> PlatformSequence;

	// Token: 0x04002EBE RID: 11966
	public int CurrentPlatform;
}
