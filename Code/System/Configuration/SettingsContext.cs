using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Provides contextual information that the provider can use when persisting settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F1 RID: 497
	[Serializable]
	public class SettingsContext : Hashtable
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0002DE98 File Offset: 0x0002C098
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
		internal ApplicationSettingsBase CurrentSettings
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = value;
			}
		}

		// Token: 0x040004DD RID: 1245
		[NonSerialized]
		private ApplicationSettingsBase current;
	}
}
