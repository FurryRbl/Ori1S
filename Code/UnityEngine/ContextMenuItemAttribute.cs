using System;

namespace UnityEngine
{
	// Token: 0x020002F5 RID: 757
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class ContextMenuItemAttribute : PropertyAttribute
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x00036400 File Offset: 0x00034600
		public ContextMenuItemAttribute(string name, string function)
		{
			this.name = name;
			this.function = function;
		}

		// Token: 0x04000BEC RID: 3052
		public readonly string name;

		// Token: 0x04000BED RID: 3053
		public readonly string function;
	}
}
