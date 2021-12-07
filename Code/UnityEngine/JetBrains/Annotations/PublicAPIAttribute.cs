using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002D7 RID: 727
	[MeansImplicitUse]
	public sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x060025F8 RID: 9720 RVA: 0x00034978 File Offset: 0x00032B78
		public PublicAPIAttribute()
		{
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00034980 File Offset: 0x00032B80
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00034990 File Offset: 0x00032B90
		// (set) Token: 0x060025FB RID: 9723 RVA: 0x00034998 File Offset: 0x00032B98
		[NotNull]
		public string Comment { get; private set; }
	}
}
