using System;

namespace fsm
{
	// Token: 0x02000561 RID: 1377
	public class CallbackState : IState
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x0009CA38 File Offset: 0x0009AC38
		public CallbackState(IState state)
		{
			this.m_state = state;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x0009CABB File Offset: 0x0009ACBB
		public CallbackState AddUpdateStateAction(Action updateStateAction)
		{
			this.UpdateStateEvent = (Action)Delegate.Combine(this.UpdateStateEvent, updateStateAction);
			return this;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x0009CAD5 File Offset: 0x0009ACD5
		public void UpdateState()
		{
			this.m_state.UpdateState();
			this.UpdateStateEvent();
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0009CAED File Offset: 0x0009ACED
		public void OnEnter()
		{
			this.m_state.OnEnter();
			this.OnEnterEvent();
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x0009CB05 File Offset: 0x0009AD05
		public void OnExit()
		{
			this.m_state.OnExit();
			this.OnExitEvent();
		}

		// Token: 0x04001E04 RID: 7684
		private readonly IState m_state;

		// Token: 0x04001E05 RID: 7685
		public Action UpdateStateEvent = delegate()
		{
		};

		// Token: 0x04001E06 RID: 7686
		public Action OnEnterEvent = delegate()
		{
		};

		// Token: 0x04001E07 RID: 7687
		public Action OnExitEvent = delegate()
		{
		};
	}
}
