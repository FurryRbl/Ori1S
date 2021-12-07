using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002DA RID: 730
	[AttributeUsage(AttributeTargets.Parameter)]
	public class PathReferenceAttribute : Attribute
	{
		// Token: 0x060025FE RID: 9726 RVA: 0x000349B4 File Offset: 0x00032BB4
		public PathReferenceAttribute()
		{
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000349BC File Offset: 0x00032BBC
		public PathReferenceAttribute([PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000349CC File Offset: 0x00032BCC
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x000349D4 File Offset: 0x00032BD4
		[NotNull]
		public string BasePath { get; private set; }
	}
}
