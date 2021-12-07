using System;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	// Token: 0x020001F2 RID: 498
	public class SettingsLoadedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> class. </summary>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object from which settings are loaded.</param>
		// Token: 0x0600110F RID: 4367 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		public SettingsLoadedEventArgs(SettingsProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>Gets the settings provider used to store configuration settings.</summary>
		/// <returns>A settings provider.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0002DEBC File Offset: 0x0002C0BC
		public SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x040004DE RID: 1246
		private SettingsProvider provider;
	}
}
