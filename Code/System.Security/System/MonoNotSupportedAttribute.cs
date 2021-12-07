using System;

namespace System
{
	// Token: 0x02000070 RID: 112
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
