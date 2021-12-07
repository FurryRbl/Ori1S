using System;

namespace UnityEngine
{
	// Token: 0x020002FA RID: 762
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x060026D7 RID: 9943 RVA: 0x00036474 File Offset: 0x00034674
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00036484 File Offset: 0x00034684
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x04000BF3 RID: 3059
		public readonly int lines;
	}
}
