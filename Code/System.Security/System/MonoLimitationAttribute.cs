using System;

namespace System
{
	// Token: 0x0200006F RID: 111
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
