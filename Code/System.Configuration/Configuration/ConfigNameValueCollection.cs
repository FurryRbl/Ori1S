using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x0200001A RID: 26
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00002D38 File Offset: 0x00000F38
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002D40 File Offset: 0x00000F40
		public ConfigNameValueCollection(ConfigNameValueCollection col) : base(col.Count, col)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002D50 File Offset: 0x00000F50
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002D5C File Offset: 0x00000F5C
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002D64 File Offset: 0x00000F64
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000031 RID: 49
		private bool modified;
	}
}
