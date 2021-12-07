using System;

namespace System.Configuration
{
	/// <summary>Defines standard functionality for controls or libraries that store and retrieve application settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E5 RID: 485
	public interface IPersistComponentSettings
	{
		/// <summary>Gets or sets a value indicating whether the control should automatically persist its application settings properties.</summary>
		/// <returns>true if the control should automatically persist its state; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060010C9 RID: 4297
		// (set) Token: 0x060010CA RID: 4298
		bool SaveSettings { get; set; }

		/// <summary>Gets or sets the value of the application settings key for the current instance of the control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current instance of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060010CB RID: 4299
		// (set) Token: 0x060010CC RID: 4300
		string SettingsKey { get; set; }

		/// <summary>Reads the control's application settings into their corresponding properties and updates the control's state.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010CD RID: 4301
		void LoadComponentSettings();

		/// <summary>Resets the control's application settings properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010CE RID: 4302
		void ResetComponentSettings();

		/// <summary>Persists the control's application settings properties.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010CF RID: 4303
		void SaveComponentSettings();
	}
}
