using System;

namespace System
{
	// Token: 0x02000189 RID: 393
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06001458 RID: 5208 RVA: 0x00051F44 File Offset: 0x00050144
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
