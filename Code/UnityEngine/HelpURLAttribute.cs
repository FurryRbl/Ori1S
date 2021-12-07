using System;

namespace UnityEngine
{
	// Token: 0x0200027D RID: 637
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HelpURLAttribute : Attribute
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x00033768 File Offset: 0x00031968
		public HelpURLAttribute(string url)
		{
			this.URL = url;
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x00033778 File Offset: 0x00031978
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x00033780 File Offset: 0x00031980
		public string URL { get; private set; }
	}
}
