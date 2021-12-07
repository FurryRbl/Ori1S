using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides the base class used to support user property settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F0 RID: 496
	public abstract class SettingsBase
	{
		/// <summary>Initializes internal properties used by <see cref="T:System.Configuration.SettingsBase" /> object.</summary>
		/// <param name="context">The settings context related to the settings properties.</param>
		/// <param name="properties">The settings properties that will be accessible from the <see cref="T:System.Configuration.SettingsBase" /> instance.</param>
		/// <param name="providers">The initialized providers that should be used when loading and saving property values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010FF RID: 4351 RVA: 0x0002DA6C File Offset: 0x0002BC6C
		public void Initialize(SettingsContext context, SettingsPropertyCollection properties, SettingsProviderCollection providers)
		{
			this.context = context;
			this.properties = properties;
			this.providers = providers;
		}

		/// <summary>Stores the current values of the settings properties.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001100 RID: 4352 RVA: 0x0002DA84 File Offset: 0x0002BC84
		public virtual void Save()
		{
			if (this.sync)
			{
				lock (this)
				{
					this.SaveCore();
				}
			}
			else
			{
				this.SaveCore();
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
		private void SaveCore()
		{
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj2 in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (settingsPropertyValue.Property.Provider == settingsProvider)
					{
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				if (settingsPropertyValueCollection.Count > 0)
				{
					settingsProvider.SetPropertyValues(this.Context, settingsPropertyValueCollection);
				}
			}
		}

		/// <summary>Provides a <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized (thread safe).</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized.</returns>
		/// <param name="settingsBase">The class used to support user property settings.</param>
		// Token: 0x06001102 RID: 4354 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		public static SettingsBase Synchronized(SettingsBase settingsBase)
		{
			settingsBase.sync = true;
			return settingsBase;
		}

		/// <summary>Gets the associated settings context.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0002DBEC File Offset: 0x0002BDEC
		public virtual SettingsContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets a value indicating whether access to the object is synchronized (thread safe). </summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.SettingsBase" /> is synchronized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0002DBF4 File Offset: 0x0002BDF4
		[System.ComponentModel.Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.sync;
			}
		}

		/// <summary>Gets or sets the value of the specified settings property.</summary>
		/// <returns>If found, the value of the named settings property.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current object, or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x170003D5 RID: 981
		public virtual object this[string propertyName]
		{
			get
			{
				if (this.sync)
				{
					lock (this)
					{
						return this.GetPropertyValue(propertyName);
					}
				}
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				if (this.sync)
				{
					lock (this)
					{
						this.SetPropertyValue(propertyName, value);
					}
				}
				else
				{
					this.SetPropertyValue(propertyName, value);
				}
			}
		}

		/// <summary>Gets the collection of settings properties.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> collection containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		public virtual SettingsPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets a collection of settings property values.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects representing the actual data values for the properties managed by the <see cref="T:System.Configuration.SettingsBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0002DCC4 File Offset: 0x0002BEC4
		public virtual SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (this.sync)
				{
					lock (this)
					{
						return this.values;
					}
				}
				return this.values;
			}
		}

		/// <summary>Gets a collection of settings providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing <see cref="T:System.Configuration.SettingsProvider" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0002DD20 File Offset: 0x0002BF20
		public virtual SettingsProviderCollection Providers
		{
			get
			{
				return this.providers;
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0002DD28 File Offset: 0x0002BF28
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty;
			if (this.Properties == null || (settingsProperty = this.Properties[propertyName]) == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("The settings property '{0}' was not found", propertyName));
			}
			if (this.values[propertyName] == null)
			{
				foreach (object obj in settingsProperty.Provider.GetPropertyValues(this.Context, this.Properties))
				{
					SettingsPropertyValue property = (SettingsPropertyValue)obj;
					this.values.Add(property);
				}
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0002DE00 File Offset: 0x0002C000
		private void SetPropertyValue(string propertyName, object value)
		{
			SettingsProperty settingsProperty;
			if (this.Properties == null || (settingsProperty = this.Properties[propertyName]) == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("The settings property '{0}' was not found", propertyName));
			}
			if (settingsProperty.IsReadOnly)
			{
				throw new SettingsPropertyIsReadOnlyException(string.Format("The settings property '{0}' is read only", propertyName));
			}
			if (settingsProperty.PropertyType != value.GetType())
			{
				throw new SettingsPropertyWrongTypeException(string.Format("The value supplied is of a type incompatible with the settings property '{0}'", propertyName));
			}
			this.PropertyValues[propertyName].PropertyValue = value;
		}

		// Token: 0x040004D8 RID: 1240
		private bool sync;

		// Token: 0x040004D9 RID: 1241
		private SettingsContext context;

		// Token: 0x040004DA RID: 1242
		private SettingsPropertyCollection properties;

		// Token: 0x040004DB RID: 1243
		private SettingsProviderCollection providers;

		// Token: 0x040004DC RID: 1244
		private SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
	}
}
