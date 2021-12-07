using System;

namespace fsm
{
	// Token: 0x02000074 RID: 116
	public class State : IState
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x000139E0 File Offset: 0x00011BE0
		public void UpdateState()
		{
			this.UpdateStateEvent();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000139ED File Offset: 0x00011BED
		public void OnEnter()
		{
			this.OnEnterEvent();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000139FA File Offset: 0x00011BFA
		public void OnExit()
		{
			this.OnExitEvent();
		}

		// Token: 0x040003E7 RID: 999
		public Action UpdateStateEvent = delegate()
		{
		};

		// Token: 0x040003E8 RID: 1000
		public Action OnEnterEvent = delegate()
		{
		};

		// Token: 0x040003E9 RID: 1001
		public Action OnExitEvent = delegate()
		{
		};
	}
}
