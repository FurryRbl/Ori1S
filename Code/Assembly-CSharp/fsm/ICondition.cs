using System;

namespace fsm
{
	// Token: 0x02000508 RID: 1288
	public interface ICondition
	{
		// Token: 0x0600228A RID: 8842
		bool Validate(IContext context);
	}
}
