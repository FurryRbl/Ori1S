using System;

namespace System
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000069CC File Offset: 0x00004BCC
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000069D4 File Offset: 0x00004BD4
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000069E4 File Offset: 0x00004BE4
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x040000D7 RID: 215
		private string comment;
	}
}
