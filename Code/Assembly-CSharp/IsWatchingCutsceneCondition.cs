using System;

// Token: 0x0200085E RID: 2142
public class IsWatchingCutsceneCondition : Condition
{
	// Token: 0x0600308F RID: 12431 RVA: 0x000CE95C File Offset: 0x000CCB5C
	public override bool Validate(IContext context)
	{
		return GameStateMachine.Instance.CurrentState == GameStateMachine.State.WatchCutscenes;
	}
}
