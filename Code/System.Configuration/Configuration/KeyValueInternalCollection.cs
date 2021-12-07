using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000056 RID: 86
	internal class KeyValueInternalCollection : NameValueCollection
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00009010 File Offset: 0x00007210
		public void SetReadOnly()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000901C File Offset: 0x0000721C
		public override void Add(string name, string val)
		{
			this.Remove(name);
			base.Add(name, val);
		}
	}
}
