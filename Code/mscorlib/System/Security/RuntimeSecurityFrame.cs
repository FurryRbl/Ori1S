using System;
using System.Reflection;

namespace System.Security
{
	// Token: 0x02000546 RID: 1350
	internal class RuntimeSecurityFrame
	{
		// Token: 0x04001650 RID: 5712
		public AppDomain domain;

		// Token: 0x04001651 RID: 5713
		public MethodInfo method;

		// Token: 0x04001652 RID: 5714
		public RuntimeDeclSecurityEntry assert;

		// Token: 0x04001653 RID: 5715
		public RuntimeDeclSecurityEntry deny;

		// Token: 0x04001654 RID: 5716
		public RuntimeDeclSecurityEntry permitonly;
	}
}
