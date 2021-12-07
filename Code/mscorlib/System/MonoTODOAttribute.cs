using System;

namespace System
{
	// Token: 0x02000187 RID: 391
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06001454 RID: 5204 RVA: 0x00051F18 File Offset: 0x00050118
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00051F20 File Offset: 0x00050120
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00051F30 File Offset: 0x00050130
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x040007E8 RID: 2024
		private string comment;
	}
}
