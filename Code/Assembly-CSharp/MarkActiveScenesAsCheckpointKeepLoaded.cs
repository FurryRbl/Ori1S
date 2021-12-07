using System;
using Core;

// Token: 0x020002FE RID: 766
public class MarkActiveScenesAsCheckpointKeepLoaded : ActionMethod
{
	// Token: 0x060016E1 RID: 5857 RVA: 0x00063ABA File Offset: 0x00061CBA
	public override void Perform(IContext context)
	{
		Scenes.Manager.MarkActiveScenesAsKeepLoaded();
	}
}
