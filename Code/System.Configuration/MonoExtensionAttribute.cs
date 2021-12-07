using System;

namespace System
{
	// Token: 0x02000072 RID: 114
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0000B740 File Offset: 0x00009940
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
