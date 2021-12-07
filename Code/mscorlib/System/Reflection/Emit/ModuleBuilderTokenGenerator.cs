using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002EC RID: 748
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x06002690 RID: 9872 RVA: 0x00088524 File Offset: 0x00086724
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00088534 File Offset: 0x00086734
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00088544 File Offset: 0x00086744
		public int GetToken(MemberInfo member)
		{
			return this.mb.GetToken(member);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00088554 File Offset: 0x00086754
		public int GetToken(MethodInfo method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00088564 File Offset: 0x00086764
		public int GetToken(SignatureHelper helper)
		{
			return this.mb.GetToken(helper);
		}

		// Token: 0x04000E77 RID: 3703
		private ModuleBuilder mb;
	}
}
