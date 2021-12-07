using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EC RID: 492
	public class SettingChangingEventArgs : System.ComponentModel.CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingChangingEventArgs" /> class.</summary>
		/// <param name="settingName">A <see cref="T:System.String" /> containing the name of the application setting.</param>
		/// <param name="settingClass">A <see cref="T:System.String" /> containing a category description of the setting. Often this parameter is set to the application settings group name.</param>
		/// <param name="settingKey">A <see cref="T:System.String" /> containing the application settings key.</param>
		/// <param name="newValue">An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</param>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x060010E2 RID: 4322 RVA: 0x0002D724 File Offset: 0x0002B924
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel) : base(cancel)
		{
			this.settingName = settingName;
			this.settingClass = settingClass;
			this.settingKey = settingKey;
			this.newValue = newValue;
		}

		/// <summary>Gets the name of the application setting associated with the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application setting. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0002D74C File Offset: 0x0002B94C
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		/// <summary>Gets the application settings property category.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a category description of the setting. Typically, this parameter is set to the application settings group name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0002D754 File Offset: 0x0002B954
		public string SettingClass
		{
			get
			{
				return this.settingClass;
			}
		}

		/// <summary>Gets the application settings key associated with the property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the application settings key.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0002D75C File Offset: 0x0002B95C
		public string SettingKey
		{
			get
			{
				return this.settingKey;
			}
		}

		/// <summary>Gets the new value being assigned to the application settings property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0002D764 File Offset: 0x0002B964
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x040004D0 RID: 1232
		private string settingName;

		// Token: 0x040004D1 RID: 1233
		private string settingClass;

		// Token: 0x040004D2 RID: 1234
		private string settingKey;

		// Token: 0x040004D3 RID: 1235
		private object newValue;
	}
}
