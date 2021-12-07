using System;
using System.Collections.Generic;

namespace fsm
{
	// Token: 0x02000546 RID: 1350
	public class TransitionManager
	{
		// Token: 0x06002355 RID: 9045 RVA: 0x0009A638 File Offset: 0x00098838
		public TransitionManager AddTransition(IState from, IState to, ICondition condition = null, IAction action = null)
		{
			List<Transition> list;
			if (!this.m_transitions.TryGetValue(from, out list))
			{
				list = new List<Transition>();
				this.m_transitions.Add(from, list);
			}
			list.Add(new Transition(from, to, condition, action));
			return this;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0009A67C File Offset: 0x0009887C
		public TransitionManager AddTransition(IState from, IState to, Func<bool> condition = null, Action action = null)
		{
			return this.AddTransition(from, to, new FuncCondition(condition), new FuncAction(action));
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0009A694 File Offset: 0x00098894
		public bool Process(StateMachine stateMachine)
		{
			List<Transition> conditionAndStatePairList;
			return this.m_transitions.TryGetValue(stateMachine.CurrentState, out conditionAndStatePairList) && this.ProcessTransitionList(stateMachine, conditionAndStatePairList);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0009A6CC File Offset: 0x000988CC
		public bool ProcessTransitionList(StateMachine stateMachine, List<Transition> conditionAndStatePairList)
		{
			for (int i = 0; i < conditionAndStatePairList.Count; i++)
			{
				Transition transition = conditionAndStatePairList[i];
				if (transition.Condition == null || transition.Condition.Validate(null))
				{
					if (transition.Action != null)
					{
						transition.Action.Perform(null);
					}
					stateMachine.ChangeState(transition.TargetState);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001DB0 RID: 7600
		private readonly Dictionary<IState, List<Transition>> m_transitions = new Dictionary<IState, List<Transition>>();
	}
}
