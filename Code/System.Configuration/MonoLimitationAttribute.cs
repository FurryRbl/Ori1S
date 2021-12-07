using System;

namespace System
{
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000B758 File Offset: 0x00009958
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
