using System;

namespace System
{
	// Token: 0x0200018B RID: 395
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x00051F5C File Offset: 0x0005015C
		public MonoLimitationAttribute(string comment) : base(comment)
		{
		}
	}
}
