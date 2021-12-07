using System;

namespace UnityEngine
{
	// Token: 0x020002F7 RID: 759
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x060026D3 RID: 9939 RVA: 0x00036428 File Offset: 0x00034628
		public SpaceAttribute()
		{
			this.height = 8f;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0003643C File Offset: 0x0003463C
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x04000BEF RID: 3055
		public readonly float height;
	}
}
