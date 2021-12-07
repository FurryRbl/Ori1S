using System;

namespace fsm
{
	// Token: 0x02000079 RID: 121
	public interface IState
	{
		// Token: 0x0600052D RID: 1325
		void UpdateState();

		// Token: 0x0600052E RID: 1326
		void OnEnter();

		// Token: 0x0600052F RID: 1327
		void OnExit();
	}
}
