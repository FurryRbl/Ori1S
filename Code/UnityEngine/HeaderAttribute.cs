using System;

namespace UnityEngine
{
	// Token: 0x020002F8 RID: 760
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x060026D5 RID: 9941 RVA: 0x0003644C File Offset: 0x0003464C
		public HeaderAttribute(string header)
		{
			this.header = header;
		}

		// Token: 0x04000BF0 RID: 3056
		public readonly string header;
	}
}
