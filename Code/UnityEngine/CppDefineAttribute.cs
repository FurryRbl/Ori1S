using System;

namespace UnityEngine
{
	// Token: 0x02000283 RID: 643
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	internal class CppDefineAttribute : Attribute
	{
		// Token: 0x0600259E RID: 9630 RVA: 0x000343B8 File Offset: 0x000325B8
		public CppDefineAttribute(string symbol, string value)
		{
		}
	}
}
