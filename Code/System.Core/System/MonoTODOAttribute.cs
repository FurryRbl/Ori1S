using System;

namespace System
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00005858 File Offset: 0x00003A58
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005860 File Offset: 0x00003A60
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005870 File Offset: 0x00003A70
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000031 RID: 49
		private string comment;
	}
}
