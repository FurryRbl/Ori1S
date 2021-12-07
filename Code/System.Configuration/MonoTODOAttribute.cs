using System;

namespace System
{
	// Token: 0x02000070 RID: 112
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0000B714 File Offset: 0x00009914
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000B71C File Offset: 0x0000991C
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000B72C File Offset: 0x0000992C
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400013E RID: 318
		private string comment;
	}
}
