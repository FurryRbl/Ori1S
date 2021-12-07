using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x020001CD RID: 461
	internal class ConfigNameValueCollection : System.Collections.Specialized.NameValueCollection
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0002AB1C File Offset: 0x00028D1C
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002AB24 File Offset: 0x00028D24
		public ConfigNameValueCollection(ConfigNameValueCollection col) : base(col.Count, col)
		{
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002AB34 File Offset: 0x00028D34
		public ConfigNameValueCollection(IHashCodeProvider hashProvider, IComparer comparer) : base(hashProvider, comparer)
		{
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002AB40 File Offset: 0x00028D40
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0002AB4C File Offset: 0x00028D4C
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002AB54 File Offset: 0x00028D54
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x0400047D RID: 1149
		private bool modified;
	}
}
