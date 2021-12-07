using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Represents an attribute or a child of a configuration element. This class cannot be inherited.</summary>
	// Token: 0x02000030 RID: 48
	public sealed class ConfigurationProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		// Token: 0x060001DA RID: 474 RVA: 0x00006BAC File Offset: 0x00004DAC
		public ConfigurationProperty(string name, Type type) : this(name, type, ConfigurationProperty.NoDefaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		// Token: 0x060001DB RID: 475 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public ConfigurationProperty(string name, Type type, object default_value) : this(name, type, default_value, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values.</param>
		// Token: 0x060001DC RID: 476 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public ConfigurationProperty(string name, Type type, object default_value, ConfigurationPropertyOptions flags) : this(name, type, default_value, TypeDescriptor.GetConverter(type), new DefaultValidator(), flags, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values. </param>
		// Token: 0x060001DD RID: 477 RVA: 0x00006C1C File Offset: 0x00004E1C
		public ConfigurationProperty(string name, Type type, object default_value, TypeConverter converter, ConfigurationValidatorBase validation, ConfigurationPropertyOptions flags) : this(name, type, default_value, converter, validation, flags, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values. </param>
		/// <param name="description">The description of the configuration entity. </param>
		// Token: 0x060001DE RID: 478 RVA: 0x00006C3C File Offset: 0x00004E3C
		public ConfigurationProperty(string name, Type type, object default_value, TypeConverter converter, ConfigurationValidatorBase validation, ConfigurationPropertyOptions flags, string description)
		{
			this.name = name;
			this.converter = ((converter == null) ? TypeDescriptor.GetConverter(type) : converter);
			if (default_value != null)
			{
				if (default_value == ConfigurationProperty.NoDefaultValue)
				{
					TypeCode typeCode = Type.GetTypeCode(type);
					if (typeCode != TypeCode.Object)
					{
						if (typeCode != TypeCode.String)
						{
							default_value = Activator.CreateInstance(type);
						}
						else
						{
							default_value = string.Empty;
						}
					}
					else
					{
						default_value = null;
					}
				}
				else if (!type.IsAssignableFrom(default_value.GetType()))
				{
					if (!this.converter.CanConvertFrom(default_value.GetType()))
					{
						throw new ConfigurationErrorsException(string.Format("The default value for property '{0}' has a different type than the one of the property itself: expected {1} but was {2}", name, type, default_value.GetType()));
					}
					default_value = this.converter.ConvertFrom(default_value);
				}
			}
			this.default_value = default_value;
			this.flags = flags;
			this.type = type;
			this.validation = ((validation == null) ? new DefaultValidator() : validation);
			this.description = description;
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> used to convert this <see cref="T:System.Configuration.ConfigurationProperty" /> into an XML representation for writing to the configuration file.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> used to convert this <see cref="T:System.Configuration.ConfigurationProperty" /> into an XML representation for writing to the configuration file.</returns>
		/// <exception cref="T:System.Exception">This <see cref="T:System.Configuration.ConfigurationProperty" /> cannot be converted. </exception>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006D54 File Offset: 0x00004F54
		public TypeConverter Converter
		{
			get
			{
				return this.converter;
			}
		}

		/// <summary>Gets the default value for this <see cref="T:System.Configuration.ConfigurationProperty" /> property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to the type specified by the <see cref="P:System.Configuration.ConfigurationProperty.Type" /> property.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00006D5C File Offset: 0x00004F5C
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is the key for the containing <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationProperty" /> object is the key for the containing element; otherwise, false. The default is false.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00006D64 File Offset: 0x00004F64
		public bool IsKey
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsKey) != ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is required.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationProperty" /> is required; otherwise, false. The default is false.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006D74 File Offset: 0x00004F74
		public bool IsRequired
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsRequired) != ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets a value that indicates whether the property is the default collection of an element. </summary>
		/// <returns>true if the property is the default collection of an element; otherwise, false.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006D84 File Offset: 0x00004F84
		public bool IsDefaultCollection
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsDefaultCollection) != ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006D94 File Offset: 0x00004F94
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the description associated with the <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>A string value that describes the property.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006D9C File Offset: 0x00004F9C
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006DAC File Offset: 0x00004FAC
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006DB4 File Offset: 0x00004FB4
		internal object ConvertFromString(string value)
		{
			if (this.converter != null)
			{
				return this.converter.ConvertFromInvariantString(value);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006DD4 File Offset: 0x00004FD4
		internal string ConvertToString(object value)
		{
			if (this.converter != null)
			{
				return this.converter.ConvertToInvariantString(value);
			}
			throw new NotImplementedException();
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006DF4 File Offset: 0x00004FF4
		internal bool IsElement
		{
			get
			{
				return typeof(ConfigurationElement).IsAssignableFrom(this.type);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006E0C File Offset: 0x0000500C
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00006E14 File Offset: 0x00005014
		internal ConfigurationCollectionAttribute CollectionAttribute
		{
			get
			{
				return this.collectionAttribute;
			}
			set
			{
				this.collectionAttribute = value;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006E20 File Offset: 0x00005020
		internal void Validate(object value)
		{
			if (this.validation != null)
			{
				this.validation.Validate(value);
			}
		}

		// Token: 0x04000093 RID: 147
		internal static readonly object NoDefaultValue = new object();

		// Token: 0x04000094 RID: 148
		private string name;

		// Token: 0x04000095 RID: 149
		private Type type;

		// Token: 0x04000096 RID: 150
		private object default_value;

		// Token: 0x04000097 RID: 151
		private TypeConverter converter;

		// Token: 0x04000098 RID: 152
		private ConfigurationValidatorBase validation;

		// Token: 0x04000099 RID: 153
		private ConfigurationPropertyOptions flags;

		// Token: 0x0400009A RID: 154
		private string description;

		// Token: 0x0400009B RID: 155
		private ConfigurationCollectionAttribute collectionAttribute;
	}
}
