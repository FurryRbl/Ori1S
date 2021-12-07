using System;

namespace UnityEngine
{
	// Token: 0x020002F6 RID: 758
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x060026D2 RID: 9938 RVA: 0x00036418 File Offset: 0x00034618
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x04000BEE RID: 3054
		public readonly string tooltip;
	}
}
