using System;

namespace UnityEngine
{
	// Token: 0x020002FB RID: 763
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x00036494 File Offset: 0x00034694
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000364AC File Offset: 0x000346AC
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x04000BF4 RID: 3060
		public readonly int minLines;

		// Token: 0x04000BF5 RID: 3061
		public readonly int maxLines;
	}
}
