using System;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x020002C1 RID: 705
	internal struct RefEmitPermissionSet
	{
		// Token: 0x0600237D RID: 9085 RVA: 0x0007E8F4 File Offset: 0x0007CAF4
		public RefEmitPermissionSet(SecurityAction action, string pset)
		{
			this.action = action;
			this.pset = pset;
		}

		// Token: 0x04000D69 RID: 3433
		public SecurityAction action;

		// Token: 0x04000D6A RID: 3434
		public string pset;
	}
}
