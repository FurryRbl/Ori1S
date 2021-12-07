using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving concrete wrapper classes to implement the application settings feature in Window Forms applications.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C7 RID: 455
	public abstract class ApplicationSettingsBase : SettingsBase, System.ComponentModel.INotifyPropertyChanged
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class to its default state.</summary>
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00029AB0 File Offset: 0x00027CB0
		protected ApplicationSettingsBase()
		{
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000FE7 RID: 4071 RVA: 0x00029ADC File Offset: 0x00027CDC
		protected ApplicationSettingsBase(System.ComponentModel.IComponent owner) : this(owner, string.Empty)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied settings key.</summary>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00029AEC File Offset: 0x00027CEC
		protected ApplicationSettingsBase(string settingsKey)
		{
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component and settings key.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000FE9 RID: 4073 RVA: 0x00029B20 File Offset: 0x00027D20
		protected ApplicationSettingsBase(System.ComponentModel.IComponent owner, string settingsKey)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.providerService = (ISettingsProviderService)owner.Site.GetService(typeof(ISettingsProviderService));
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Occurs after the value of an application settings property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000FEA RID: 4074 RVA: 0x00029B80 File Offset: 0x00027D80
		// (remove) Token: 0x06000FEB RID: 4075 RVA: 0x00029B9C File Offset: 0x00027D9C
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		/// <summary>Occurs before the value of an application settings property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000FEC RID: 4076 RVA: 0x00029BB8 File Offset: 0x00027DB8
		// (remove) Token: 0x06000FED RID: 4077 RVA: 0x00029BD4 File Offset: 0x00027DD4
		public event SettingChangingEventHandler SettingChanging;

		/// <summary>Occurs after the application settings are retrieved from storage.</summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000FEE RID: 4078 RVA: 0x00029BF0 File Offset: 0x00027DF0
		// (remove) Token: 0x06000FEF RID: 4079 RVA: 0x00029C0C File Offset: 0x00027E0C
		public event SettingsLoadedEventHandler SettingsLoaded;

		/// <summary>Occurs before values are saved to the data store.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000FF0 RID: 4080 RVA: 0x00029C28 File Offset: 0x00027E28
		// (remove) Token: 0x06000FF1 RID: 4081 RVA: 0x00029C44 File Offset: 0x00027E44
		public event SettingsSavingEventHandler SettingsSaving;

		/// <summary>Returns the value of the named settings property for the previous version of the same application.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the specified <see cref="T:System.Configuration.SettingsProperty" /> if found; otherwise, null.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the settings property whose value is to be returned.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">The property does not exist. The property count is zero or the property cannot be found in the data store.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00029C60 File Offset: 0x00027E60
		public object GetPreviousVersion(string propertyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the application settings property values from persistent storage.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00029C68 File Offset: 0x00027E68
		public void Reload()
		{
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
				if (applicationSettingsProvider != null)
				{
					applicationSettingsProvider.Reset(this.Context);
				}
			}
		}

		/// <summary>Restores the persisted application settings values to their corresponding default properties.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FF4 RID: 4084 RVA: 0x00029CEC File Offset: 0x00027EEC
		public void Reset()
		{
			this.Reload();
		}

		/// <summary>Stores the current values of the application settings properties.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00029CF4 File Offset: 0x00027EF4
		public override void Save()
		{
			this.Context.CurrentSettings = this;
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
			this.Context.CurrentSettings = null;
		}

		/// <summary>Updates application settings to reflect a more recent installation of the application.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x00029E0C File Offset: 0x0002800C
		public virtual void Upgrade()
		{
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.PropertyChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00029E10 File Offset: 0x00028010
		protected virtual void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00029E2C File Offset: 0x0002802C
		protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			if (this.SettingChanging != null)
			{
				this.SettingChanging(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x00029E48 File Offset: 0x00028048
		protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			if (this.SettingsLoaded != null)
			{
				this.SettingsLoaded(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FFA RID: 4090 RVA: 0x00029E64 File Offset: 0x00028064
		protected virtual void OnSettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.SettingsSaving != null)
			{
				this.SettingsSaving(sender, e);
			}
		}

		/// <summary>Gets the application settings context associated with the settings group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00029E80 File Offset: 0x00028080
		[System.ComponentModel.Browsable(false)]
		public override SettingsContext Context
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsContext result;
				try
				{
					if (this.context == null)
					{
						this.context = new SettingsContext();
						this.context["SettingsKey"] = string.Empty;
						Type type = base.GetType();
						this.context["GroupName"] = type.FullName;
						this.context["SettingsClassType"] = type;
					}
					result = this.context;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00029F38 File Offset: 0x00028138
		private void CacheValuesByProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection.Count > 0)
			{
				SettingsPropertyValueCollection vals = provider.GetPropertyValues(this.Context, settingsPropertyCollection);
				this.PropertyValues.Add(vals);
			}
			this.OnSettingsLoaded(this, new SettingsLoadedEventArgs(provider));
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00029FF0 File Offset: 0x000281F0
		private void InitializeSettings(SettingsPropertyCollection settings)
		{
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00029FF4 File Offset: 0x000281F4
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(propertyName);
			}
			if (this.propertyValues == null)
			{
				this.InitializeSettings(this.Properties);
			}
			if (this.PropertyValues[propertyName] == null)
			{
				this.CacheValuesByProvider(settingsProperty.Provider);
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		/// <summary>Gets or sets the value of the specified application settings property.</summary>
		/// <returns>If found, the value of the named settings property; otherwise, null.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current wrapper or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038B RID: 907
		[MonoTODO]
		public override object this[string propertyName]
		{
			get
			{
				if (base.IsSynchronized)
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
				SettingsProperty settingsProperty = this.Properties[propertyName];
				if (settingsProperty == null)
				{
					throw new SettingsPropertyNotFoundException(propertyName);
				}
				if (settingsProperty.IsReadOnly)
				{
					throw new SettingsPropertyIsReadOnlyException(propertyName);
				}
				if (value != null && !settingsProperty.PropertyType.IsAssignableFrom(value.GetType()))
				{
					throw new SettingsPropertyWrongTypeException(propertyName);
				}
				if (this.PropertyValues[propertyName] == null)
				{
					this.CacheValuesByProvider(settingsProperty.Provider);
				}
				SettingChangingEventArgs settingChangingEventArgs = new SettingChangingEventArgs(propertyName, base.GetType().FullName, this.settingsKey, value, false);
				this.OnSettingChanging(this, settingChangingEventArgs);
				if (!settingChangingEventArgs.Cancel)
				{
					this.PropertyValues[propertyName].PropertyValue = value;
					this.OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
				}
			}
		}

		/// <summary>Gets the collection of settings properties in the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects used in the current wrapper.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The associated settings provider could not be found or its instantiation failed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0002A188 File Offset: 0x00028388
		[System.ComponentModel.Browsable(false)]
		public override SettingsPropertyCollection Properties
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyCollection result;
				try
				{
					if (this.properties == null)
					{
						LocalFileSettingsProvider localFileSettingsProvider = null;
						this.properties = new SettingsPropertyCollection();
						foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
						{
							SettingAttribute[] array2 = (SettingAttribute[])propertyInfo.GetCustomAttributes(typeof(SettingAttribute), false);
							if (array2 != null && array2.Length != 0)
							{
								this.CreateSettingsProperty(propertyInfo, this.properties, ref localFileSettingsProvider);
							}
						}
					}
					result = this.properties;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002A260 File Offset: 0x00028460
		private void CreateSettingsProperty(PropertyInfo prop, SettingsPropertyCollection properties, ref LocalFileSettingsProvider local_provider)
		{
			SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
			SettingsProvider settingsProvider = null;
			object defaultValue = null;
			SettingsSerializeAs serializeAs = SettingsSerializeAs.String;
			bool flag = false;
			foreach (Attribute attribute in prop.GetCustomAttributes(false))
			{
				if (attribute is SettingsProviderAttribute)
				{
					Type type = Type.GetType(((SettingsProviderAttribute)attribute).ProviderTypeName);
					settingsProvider = (SettingsProvider)Activator.CreateInstance(type);
					settingsProvider.Initialize(null, null);
				}
				else if (attribute is DefaultSettingValueAttribute)
				{
					defaultValue = ((DefaultSettingValueAttribute)attribute).Value;
				}
				else if (attribute is SettingsSerializeAsAttribute)
				{
					serializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
					flag = true;
				}
				else if (attribute is ApplicationScopedSettingAttribute || attribute is UserScopedSettingAttribute)
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
				else
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
			}
			if (!flag)
			{
				System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(prop.PropertyType);
				if (converter != null && (!converter.CanConvertFrom(typeof(string)) || !converter.CanConvertTo(typeof(string))))
				{
					serializeAs = SettingsSerializeAs.Xml;
				}
			}
			SettingsProperty settingsProperty = new SettingsProperty(prop.Name, prop.PropertyType, settingsProvider, false, defaultValue, serializeAs, settingsAttributeDictionary, false, false);
			if (this.providerService != null)
			{
				settingsProperty.Provider = this.providerService.GetSettingsProvider(settingsProperty);
			}
			if (settingsProvider == null)
			{
				if (local_provider == null)
				{
					local_provider = new LocalFileSettingsProvider();
					local_provider.Initialize(null, null);
				}
				settingsProperty.Provider = local_provider;
				settingsProvider = local_provider;
			}
			if (settingsProvider != null)
			{
				SettingsProvider settingsProvider2 = this.Providers[settingsProvider.Name];
				if (settingsProvider2 != null)
				{
					settingsProperty.Provider = settingsProvider2;
				}
			}
			properties.Add(settingsProperty);
			if (settingsProperty.Provider != null && this.Providers[settingsProperty.Provider.Name] == null)
			{
				this.Providers.Add(settingsProperty.Provider);
			}
		}

		/// <summary>Gets a collection of property values.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> of property values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0002A474 File Offset: 0x00028674
		[System.ComponentModel.Browsable(false)]
		public override SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyValueCollection result;
				try
				{
					if (this.propertyValues == null)
					{
						this.propertyValues = new SettingsPropertyValueCollection();
					}
					result = this.propertyValues;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		/// <summary>Gets the collection of application settings providers used by the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing all the <see cref="T:System.Configuration.SettingsProvider" /> objects used by the settings properties of the current settings wrapper.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0002A4E8 File Offset: 0x000286E8
		[System.ComponentModel.Browsable(false)]
		public override SettingsProviderCollection Providers
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsProviderCollection result;
				try
				{
					if (this.providers == null)
					{
						this.providers = new SettingsProviderCollection();
					}
					result = this.providers;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		/// <summary>Gets or sets the settings key for the application settings group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0002A55C File Offset: 0x0002875C
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x0002A564 File Offset: 0x00028764
		[System.ComponentModel.Browsable(false)]
		public string SettingsKey
		{
			get
			{
				return this.settingsKey;
			}
			set
			{
				this.settingsKey = value;
			}
		}

		// Token: 0x0400046D RID: 1133
		private string settingsKey;

		// Token: 0x0400046E RID: 1134
		private SettingsContext context;

		// Token: 0x0400046F RID: 1135
		private SettingsPropertyCollection properties;

		// Token: 0x04000470 RID: 1136
		private ISettingsProviderService providerService;

		// Token: 0x04000471 RID: 1137
		private SettingsPropertyValueCollection propertyValues;

		// Token: 0x04000472 RID: 1138
		private SettingsProviderCollection providers;
	}
}
