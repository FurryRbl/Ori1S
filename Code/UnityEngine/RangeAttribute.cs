using System;

namespace UnityEngine
{
	// Token: 0x020002F9 RID: 761
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x0003645C File Offset: 0x0003465C
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000BF1 RID: 3057
		public readonly float min;

		// Token: 0x04000BF2 RID: 3058
		public readonly float max;
	}
}
