using System;

namespace System
{
	// Token: 0x02000075 RID: 117
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x0000B764 File Offset: 0x00009964
		public MonoNotSupportedAttribute(string comment) : base(comment)
		{
		}
	}
}
