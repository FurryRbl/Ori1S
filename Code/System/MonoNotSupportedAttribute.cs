using System;

namespace System
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000216C File Offset: 0x0000036C
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
