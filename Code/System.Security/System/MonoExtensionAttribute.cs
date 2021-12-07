using System;

namespace System
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
