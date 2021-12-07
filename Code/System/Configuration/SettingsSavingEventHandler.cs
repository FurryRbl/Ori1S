using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event. </summary>
	/// <param name="sender">The source of the event, typically a data container or data-bound collection.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200050B RID: 1291
	// (Invoke) Token: 0x06002CD8 RID: 11480
	public delegate void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e);
}
