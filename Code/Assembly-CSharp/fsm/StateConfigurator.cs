using System;

namespace fsm
{
	// Token: 0x02000505 RID: 1285
	public class StateConfigurator
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x000973B5 File Offset: 0x000955B5
		public StateConfigurator(StateMachine stateMachine, IState state)
		{
			this.m_stateMachine = stateMachine;
			this.m_state = state;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000973CC File Offset: 0x000955CC
		public StateConfigurator AddTransition<T>(IState to, ICondition condition = null, IAction action = null) where T : ITrigger
		{
			this.m_stateMachine.FindTransitionManager(typeof(T)).AddTransition(this.m_state, to, condition, action);
			return this;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00097400 File Offset: 0x00095600
		public StateConfigurator AddTransition<T>(IState to, Func<bool> condition, Action action = null) where T : ITrigger
		{
			this.m_stateMachine.FindTransitionManager(typeof(T)).AddTransition(this.m_state, to, condition, action);
			return this;
		}

		// Token: 0x04001CED RID: 7405
		private readonly StateMachine m_stateMachine;

		// Token: 0x04001CEE RID: 7406
		private readonly IState m_state;
	}
}
