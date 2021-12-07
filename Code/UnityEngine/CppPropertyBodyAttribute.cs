using System;

namespace UnityEngine
{
	// Token: 0x02000286 RID: 646
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal class CppPropertyBodyAttribute : Attribute
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x000343D0 File Offset: 0x000325D0
		public CppPropertyBodyAttribute(string getterBody, string setterBody)
		{
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000343D8 File Offset: 0x000325D8
		public CppPropertyBodyAttribute(string getterBody)
		{
		}
	}
}
