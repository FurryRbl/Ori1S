using System;

namespace UnityEngine
{
	// Token: 0x02000274 RID: 628
	[AttributeUsage(AttributeTargets.Struct)]
	internal class IL2CPPStructAlignmentAttribute : Attribute
	{
		// Token: 0x06002554 RID: 9556 RVA: 0x00033478 File Offset: 0x00031678
		public IL2CPPStructAlignmentAttribute()
		{
			this.Align = 1;
		}

		// Token: 0x040009D2 RID: 2514
		public int Align;
	}
}
