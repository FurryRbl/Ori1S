using System;

namespace UnityEngine
{
	// Token: 0x02000282 RID: 642
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	internal class CppIncludeAttribute : Attribute
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x000343B0 File Offset: 0x000325B0
		public CppIncludeAttribute(string header)
		{
		}
	}
}
