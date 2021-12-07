using System;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event. </summary>
	/// <param name="sender">The source of the event, typically an application settings wrapper class derived from the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class.</param>
	/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> containing the data for the event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000509 RID: 1289
	// (Invoke) Token: 0x06002CD0 RID: 11472
	public delegate void SettingChangingEventHandler(object sender, SettingChangingEventArgs e);
}
