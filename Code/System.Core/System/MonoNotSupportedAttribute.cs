using System;

namespace System
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000058A8 File Offset: 0x00003AA8
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
