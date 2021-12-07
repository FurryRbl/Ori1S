using System;

namespace System.Configuration
{
	/// <summary>Used internally as the class that represents metadata about an individual configuration property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F4 RID: 500
	public class SettingsProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class, based on the supplied parameter.</summary>
		/// <param name="propertyToCopy">Specifies a copy of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06001123 RID: 4387 RVA: 0x0002E040 File Offset: 0x0002C240
		public SettingsProperty(SettingsProperty propertyToCopy) : this(propertyToCopy.Name, propertyToCopy.PropertyType, propertyToCopy.Provider, propertyToCopy.IsReadOnly, propertyToCopy.DefaultValue, propertyToCopy.SerializeAs, new SettingsAttributeDictionary(propertyToCopy.Attributes), propertyToCopy.ThrowOnErrorDeserializing, propertyToCopy.ThrowOnErrorSerializing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class. based on the supplied parameter.</summary>
		/// <param name="name">Specifies the name of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06001124 RID: 4388 RVA: 0x0002E090 File Offset: 0x0002C290
		public SettingsProperty(string name) : this(name, null, null, false, null, SettingsSerializeAs.String, new SettingsAttributeDictionary(), false, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class based on the supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="propertyType">The type of <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object to use for persistence.</param>
		/// <param name="isReadOnly">A <see cref="T:System.Boolean" /> value specifying whether the <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</param>
		/// <param name="defaultValue">The default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used to set the serialization scheme for storing application settings.</param>
		/// <param name="attributes">A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</param>
		/// <param name="throwOnErrorDeserializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</param>
		/// <param name="throwOnErrorSerializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully serialized.</param>
		// Token: 0x06001125 RID: 4389 RVA: 0x0002E0B0 File Offset: 0x0002C2B0
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this.name = name;
			this.propertyType = propertyType;
			this.provider = provider;
			this.isReadOnly = isReadOnly;
			this.defaultValue = defaultValue;
			this.serializeAs = serializeAs;
			this.attributes = attributes;
			this.throwOnErrorDeserializing = throwOnErrorDeserializing;
			this.throwOnErrorSerializing = throwOnErrorSerializing;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object containing the attributes of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0002E108 File Offset: 0x0002C308
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>An object containing the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0002E110 File Offset: 0x0002C310
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x0002E118 File Offset: 0x0002C318
		public virtual object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether a <see cref="T:System.Configuration.SettingsProperty" /> object is read-only. </summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingsProperty" /> is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0002E124 File Offset: 0x0002C324
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x0002E12C File Offset: 0x0002C32C
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0002E138 File Offset: 0x0002C338
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x0002E140 File Offset: 0x0002C340
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The type for the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0002E14C File Offset: 0x0002C34C
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x0002E154 File Offset: 0x0002C354
		public virtual Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
			set
			{
				this.propertyType = value;
			}
		}

		/// <summary>Gets or sets the provider for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProvider" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0002E160 File Offset: 0x0002C360
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x0002E168 File Offset: 0x0002C368
		public virtual SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Configuration.SettingsSerializeAs" /> object for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0002E174 File Offset: 0x0002C374
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x0002E17C File Offset: 0x0002C37C
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
			set
			{
				this.serializeAs = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully deserialized; otherwise, false.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0002E188 File Offset: 0x0002C388
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x0002E190 File Offset: 0x0002C390
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this.throwOnErrorDeserializing;
			}
			set
			{
				this.throwOnErrorDeserializing = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully serialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully serialized; otherwise, false.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0002E19C File Offset: 0x0002C39C
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this.throwOnErrorSerializing;
			}
			set
			{
				this.throwOnErrorSerializing = value;
			}
		}

		// Token: 0x040004E1 RID: 1249
		private string name;

		// Token: 0x040004E2 RID: 1250
		private Type propertyType;

		// Token: 0x040004E3 RID: 1251
		private SettingsProvider provider;

		// Token: 0x040004E4 RID: 1252
		private bool isReadOnly;

		// Token: 0x040004E5 RID: 1253
		private object defaultValue;

		// Token: 0x040004E6 RID: 1254
		private SettingsSerializeAs serializeAs;

		// Token: 0x040004E7 RID: 1255
		private SettingsAttributeDictionary attributes;

		// Token: 0x040004E8 RID: 1256
		private bool throwOnErrorDeserializing;

		// Token: 0x040004E9 RID: 1257
		private bool throwOnErrorSerializing;
	}
}
