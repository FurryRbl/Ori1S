using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002D2 RID: 722
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x06002496 RID: 9366 RVA: 0x00082834 File Offset: 0x00080A34
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00082844 File Offset: 0x00080A44
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00082854 File Offset: 0x00080A54
		public int GetToken(MethodInfo method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x0008285C File Offset: 0x00080A5C
		public int GetToken(MemberInfo member)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x0008286C File Offset: 0x00080A6C
		public int GetToken(SignatureHelper helper)
		{
			return this.m.AddRef(helper);
		}

		// Token: 0x04000DD0 RID: 3536
		private DynamicMethod m;
	}
}
