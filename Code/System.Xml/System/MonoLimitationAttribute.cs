using System;

namespace System
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00006A10 File Offset: 0x00004C10
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
