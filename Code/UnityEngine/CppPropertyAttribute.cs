using System;

namespace UnityEngine
{
	// Token: 0x02000287 RID: 647
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal class CppPropertyAttribute : Attribute
	{
		// Token: 0x060025A3 RID: 9635 RVA: 0x000343E0 File Offset: 0x000325E0
		public CppPropertyAttribute(string getter, string setter)
		{
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000343E8 File Offset: 0x000325E8
		public CppPropertyAttribute(string getter)
		{
		}
	}
}
