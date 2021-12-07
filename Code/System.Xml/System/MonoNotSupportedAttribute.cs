using System;

namespace System
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00006A1C File Offset: 0x00004C1C
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
