using System;

namespace System.Xml
{
	// Token: 0x020000DD RID: 221
	[AttributeUsage(AttributeTargets.All)]
	internal class MonoFIXAttribute : Attribute
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		public MonoFIXAttribute()
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002CD14 File Offset: 0x0002AF14
		public MonoFIXAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0002CD24 File Offset: 0x0002AF24
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000476 RID: 1142
		private string comment;
	}
}
