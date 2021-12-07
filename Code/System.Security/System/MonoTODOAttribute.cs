using System;

namespace System
{
	// Token: 0x0200006B RID: 107
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public MonoTODOAttribute()
		{
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x040001A1 RID: 417
		private string comment;
	}
}
