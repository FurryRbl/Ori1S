using System;

namespace System
{
	// Token: 0x0200018C RID: 396
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x00051F68 File Offset: 0x00050168
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
