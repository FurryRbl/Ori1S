using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Provides persistence for application settings classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E7 RID: 487
	public class LocalFileSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LocalFileSettingsProvider" /> class.</summary>
		// Token: 0x060010D1 RID: 4305 RVA: 0x0002D52C File Offset: 0x0002B72C
		public LocalFileSettingsProvider()
		{
			this.impl = new CustomizableFileSettingsProvider();
		}

		/// <summary>Returns the value of the named settings property for the previous version of the same application. </summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValue" /> representing the application setting if found; otherwise, null.</returns>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> that describes where the application settings property is used.</param>
		/// <param name="property">The <see cref="T:System.Configuration.SettingsProperty" /> whose value is to be returned.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060010D2 RID: 4306 RVA: 0x0002D540 File Offset: 0x0002B740
		[MonoTODO]
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			return this.impl.GetPreviousVersion(context, property);
		}

		/// <summary>Returns the collection of setting property values for the specified application instance and settings property group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.</returns>
		/// <param name="context"></param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060010D3 RID: 4307 RVA: 0x0002D550 File Offset: 0x0002B750
		[MonoTODO]
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			return this.impl.GetPropertyValues(context, properties);
		}

		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="values"></param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010D4 RID: 4308 RVA: 0x0002D560 File Offset: 0x0002B760
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection values)
		{
			if (name == null)
			{
				name = "LocalFileSettingsProvider";
			}
			if (values != null)
			{
				this.impl.ApplicationName = values["applicationName"];
			}
			base.Initialize(name, values);
		}

		/// <summary>Resets all application settings properties associated with the specified application to their default values.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060010D5 RID: 4309 RVA: 0x0002D594 File Offset: 0x0002B794
		[MonoTODO]
		public void Reset(SettingsContext context)
		{
			this.impl.Reset(context);
		}

		/// <summary>Sets the values of the specified group of property settings.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="values">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.-or-There was a general failure saving the settings to the configuration file.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060010D6 RID: 4310 RVA: 0x0002D5A4 File Offset: 0x0002B7A4
		[MonoTODO]
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
		{
			this.impl.SetPropertyValues(context, values);
		}

		/// <summary>Attempts to migrate previous user-scoped settings from a previous version of the same application.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage. </param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.-or-The previous version of the configuration file could not be accessed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060010D7 RID: 4311 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		[MonoTODO]
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
			this.impl.Upgrade(context, properties);
		}

		/// <summary>Gets or sets the name of the currently running application.</summary>
		/// <returns>A string that contains the application's display name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x0002D5D4 File Offset: 0x0002B7D4
		public override string ApplicationName
		{
			get
			{
				return this.impl.ApplicationName;
			}
			set
			{
				this.impl.ApplicationName = value;
			}
		}

		// Token: 0x040004CF RID: 1231
		private CustomizableFileSettingsProvider impl;
	}
}
