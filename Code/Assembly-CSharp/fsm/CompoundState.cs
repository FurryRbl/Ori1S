using System;

namespace fsm
{
	// Token: 0x02000562 RID: 1378
	public class CompoundState : IState
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x0009CB24 File Offset: 0x0009AD24
		public CompoundState(params IState[] states)
		{
			this.m_states = states;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0009CBA8 File Offset: 0x0009ADA8
		public void OnEnter()
		{
			foreach (IState state in this.m_states)
			{
				state.OnEnter();
			}
			this.OnEnterEvent();
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0009CBE8 File Offset: 0x0009ADE8
		public void OnExit()
		{
			foreach (IState state in this.m_states)
			{
				state.OnExit();
			}
			this.OnExitEvent();
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0009CC28 File Offset: 0x0009AE28
		public void UpdateState()
		{
			foreach (IState state in this.m_states)
			{
				state.UpdateState();
			}
			this.OnUpdateEvent();
		}

		// Token: 0x04001E0B RID: 7691
		private readonly IState[] m_states;

		// Token: 0x04001E0C RID: 7692
		public Action OnUpdateEvent = delegate()
		{
		};

		// Token: 0x04001E0D RID: 7693
		public Action OnEnterEvent = delegate()
		{
		};

		// Token: 0x04001E0E RID: 7694
		public Action OnExitEvent = delegate()
		{
		};
	}
}
