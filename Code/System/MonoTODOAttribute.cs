using System;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400001F RID: 31
		private string comment;
	}
}
