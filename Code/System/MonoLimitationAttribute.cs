using System;

namespace System
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
