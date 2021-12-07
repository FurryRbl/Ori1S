using System;

namespace fsm
{
	// Token: 0x02000563 RID: 1379
	public class AlwaysCondition : ICondition
	{
		// Token: 0x060023E4 RID: 9188 RVA: 0x0009CC73 File Offset: 0x0009AE73
		public bool Validate(IContext context)
		{
			return true;
		}
	}
}
