using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002DF RID: 735
	internal interface TokenGenerator
	{
		// Token: 0x0600257F RID: 9599
		int GetToken(string str);

		// Token: 0x06002580 RID: 9600
		int GetToken(MemberInfo member);

		// Token: 0x06002581 RID: 9601
		int GetToken(MethodInfo method, Type[] opt_param_types);

		// Token: 0x06002582 RID: 9602
		int GetToken(SignatureHelper helper);
	}
}
