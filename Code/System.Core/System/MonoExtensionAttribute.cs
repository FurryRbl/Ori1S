using System;

namespace System
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00005884 File Offset: 0x00003A84
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
