using System;

namespace System
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000589C File Offset: 0x00003A9C
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
