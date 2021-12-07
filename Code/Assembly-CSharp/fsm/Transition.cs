using System;

namespace fsm
{
	// Token: 0x0200055E RID: 1374
	public class Transition
	{
		// Token: 0x060023CF RID: 9167 RVA: 0x0009C9D7 File Offset: 0x0009ABD7
		public Transition(IState sourceState, IState targetState, ICondition condition, IAction action)
		{
			this.SourceState = sourceState;
			this.TargetState = targetState;
			this.Condition = condition;
			this.Action = action;
		}

		// Token: 0x04001DFE RID: 7678
		public IState SourceState;

		// Token: 0x04001DFF RID: 7679
		public IState TargetState;

		// Token: 0x04001E00 RID: 7680
		public ICondition Condition;

		// Token: 0x04001E01 RID: 7681
		public IAction Action;
	}
}
