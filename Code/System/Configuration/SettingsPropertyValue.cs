using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace System.Configuration
{
	/// <summary>Contains the value of a settings property that can be loaded and stored by an instance of <see cref="T:System.Configuration.SettingsBase" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F8 RID: 504
	public class SettingsPropertyValue
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValue" /> class, based on supplied parameters.</summary>
		/// <param name="property">Specifies a <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x0600114C RID: 4428 RVA: 0x0002E384 File Offset: 0x0002C584
		public SettingsPropertyValue(SettingsProperty property)
		{
			this.property = property;
			this.needPropertyValue = true;
		}

		/// <summary>Gets or sets whether the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has been deserialized. </summary>
		/// <returns>true if the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has been deserialized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0002E39C File Offset: 0x0002C59C
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
		public bool Deserialized
		{
			get
			{
				return this.deserialized;
			}
			set
			{
				this.deserialized = value;
			}
		}

		/// <summary>Gets or sets whether the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has changed. </summary>
		/// <returns>true if the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has changed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0002E3B8 File Offset: 0x0002C5B8
		public bool IsDirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		/// <summary>Gets the name of the property from the associated <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object that describes the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0002E3D4 File Offset: 0x0002C5D4
		public SettingsProperty Property
		{
			get
			{
				return this.property;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.SettingsProperty" /> object. When this value is set, the <see cref="P:System.Configuration.SettingsPropertyValue.IsDirty" /> property is set to true and <see cref="P:System.Configuration.SettingsPropertyValue.UsingDefaultValue" /> is set to false.When a value is first accessed from the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property, and if the value was initially stored into the <see cref="T:System.Configuration.SettingsPropertyValue" /> object as a serialized representation using the <see cref="P:System.Configuration.SettingsPropertyValue.SerializedValue" /> property, the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property will trigger deserialization of the underlying value.  As a side effect, the <see cref="P:System.Configuration.SettingsPropertyValue.Deserialized" /> property will be set to true.If this chain of events occurs in ASP.NET, and if an error occurs during the deserialization process, the error is logged using the health-monitoring feature of ASP.NET. By default, this means that deserialization errors will show up in the Application Event Log when running under ASP.NET. If this process occurs outside of ASP.NET, and if an error occurs during deserialization, the error is suppressed, and the remainder of the logic during deserialization occurs. If there is no serialized value to deserialize when the deserialization is attempted, then <see cref="T:System.Configuration.SettingsPropertyValue" /> object will instead attempt to return a default value if one was configured as defined on the associated <see cref="T:System.Configuration.SettingsProperty" /> instance. In this case, if the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property was set to either null, or to the string "[null]", then the <see cref="T:System.Configuration.SettingsPropertyValue" /> object will initialize the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property to either null for reference types, or to the default value for the associated value type.  On the other hand, if <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property holds a valid object reference or string value (other than "[null]"), then the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property is returned instead.If there is no serialized value to deserialize when the deserialization is attempted, and no default value was specified, then an empty string will be returned for string types. For all other types, a default instance will be returned by calling <see cref="M:System.Activator.CreateInstance(System.Type)" /> — for reference types this means an attempt will be made to create an object instance using the default constructor.  If this attempt fails, then null is returned.</returns>
		/// <exception cref="T:System.ArgumentException">While attempting to use the default value from the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property, an error occurred.  Either the attempt to convert <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property to a valid type failed, or the resulting value was not compatible with the type defined by <see cref="P:System.Configuration.SettingsProperty.PropertyType" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0002E3DC File Offset: 0x0002C5DC
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0002E478 File Offset: 0x0002C678
		public object PropertyValue
		{
			get
			{
				if (this.needPropertyValue)
				{
					this.propertyValue = this.GetDeserializedValue(this.serializedValue);
					if (this.propertyValue == null)
					{
						this.propertyValue = this.GetDeserializedDefaultValue();
						this.defaulted = true;
					}
					this.needPropertyValue = false;
				}
				if (this.propertyValue != null && !(this.propertyValue is string) && !(this.propertyValue is DateTime) && !this.property.PropertyType.IsPrimitive)
				{
					this.dirty = true;
				}
				return this.propertyValue;
			}
			set
			{
				this.propertyValue = value;
				this.dirty = true;
				this.needPropertyValue = false;
				this.needSerializedValue = true;
				this.defaulted = false;
			}
		}

		/// <summary>Gets or sets the serialized value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The serialized value of a <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The serialization options for the property indicated the use of a string type converter, but a type converter was not available.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0002E4A0 File Offset: 0x0002C6A0
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		public object SerializedValue
		{
			get
			{
				if (this.needSerializedValue)
				{
					this.needSerializedValue = false;
					switch (this.property.SerializeAs)
					{
					case SettingsSerializeAs.String:
						this.serializedValue = System.ComponentModel.TypeDescriptor.GetConverter(this.property.PropertyType).ConvertToInvariantString(this.propertyValue);
						break;
					case SettingsSerializeAs.Xml:
						if (this.propertyValue != null)
						{
							XmlSerializer xmlSerializer = new XmlSerializer(this.propertyValue.GetType());
							StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
							xmlSerializer.Serialize(stringWriter, this.propertyValue);
							this.serializedValue = stringWriter.ToString();
						}
						else
						{
							this.serializedValue = null;
						}
						break;
					case SettingsSerializeAs.Binary:
						if (this.propertyValue != null)
						{
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							MemoryStream memoryStream = new MemoryStream();
							binaryFormatter.Serialize(memoryStream, this.propertyValue);
							this.serializedValue = memoryStream.ToArray();
						}
						else
						{
							this.serializedValue = null;
						}
						break;
					default:
						this.serializedValue = null;
						break;
					}
				}
				return this.serializedValue;
			}
			set
			{
				this.serializedValue = value;
				this.needPropertyValue = true;
			}
		}

		/// <summary>Gets a Boolean value specifying whether the value of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object is the default value as defined by the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property value on the associated <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>true if the value of the <see cref="T:System.Configuration.SettingsProperty" /> object is the default value; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		public bool UsingDefaultValue
		{
			get
			{
				return this.defaulted;
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0002E5C8 File Offset: 0x0002C7C8
		internal object Reset()
		{
			this.propertyValue = this.GetDeserializedDefaultValue();
			this.dirty = true;
			this.defaulted = true;
			this.needPropertyValue = true;
			return this.propertyValue;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0002E5F4 File Offset: 0x0002C7F4
		private object GetDeserializedDefaultValue()
		{
			if (this.property.DefaultValue == null)
			{
				if (this.property.PropertyType.IsValueType)
				{
					return Activator.CreateInstance(this.property.PropertyType);
				}
				return null;
			}
			else if (this.property.DefaultValue is string && ((string)this.property.DefaultValue).Length == 0)
			{
				if (this.property.PropertyType != typeof(string))
				{
					return Activator.CreateInstance(this.property.PropertyType);
				}
				return string.Empty;
			}
			else
			{
				if (this.property.DefaultValue is string && ((string)this.property.DefaultValue).Length > 0)
				{
					return this.GetDeserializedValue(this.property.DefaultValue);
				}
				if (!this.property.PropertyType.IsAssignableFrom(this.property.DefaultValue.GetType()))
				{
					System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(this.property.PropertyType);
					return converter.ConvertFrom(null, CultureInfo.InvariantCulture, this.property.DefaultValue);
				}
				return this.property.DefaultValue;
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0002E734 File Offset: 0x0002C934
		private object GetDeserializedValue(object serializedValue)
		{
			if (serializedValue == null)
			{
				return null;
			}
			object result = null;
			try
			{
				switch (this.property.SerializeAs)
				{
				case SettingsSerializeAs.String:
					if (serializedValue is string)
					{
						result = System.ComponentModel.TypeDescriptor.GetConverter(this.property.PropertyType).ConvertFromInvariantString((string)serializedValue);
					}
					break;
				case SettingsSerializeAs.Xml:
				{
					XmlSerializer xmlSerializer = new XmlSerializer(this.property.PropertyType);
					StringReader reader = new StringReader((string)serializedValue);
					result = xmlSerializer.Deserialize(XmlReader.Create(reader));
					break;
				}
				case SettingsSerializeAs.Binary:
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream;
					if (serializedValue is string)
					{
						serializationStream = new MemoryStream(Convert.FromBase64String((string)serializedValue));
					}
					else
					{
						serializationStream = new MemoryStream((byte[])serializedValue);
					}
					result = binaryFormatter.Deserialize(serializationStream);
					break;
				}
				}
			}
			catch (Exception ex)
			{
				if (this.property.ThrowOnErrorDeserializing)
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x040004EC RID: 1260
		private readonly SettingsProperty property;

		// Token: 0x040004ED RID: 1261
		private object propertyValue;

		// Token: 0x040004EE RID: 1262
		private object serializedValue;

		// Token: 0x040004EF RID: 1263
		private bool needSerializedValue;

		// Token: 0x040004F0 RID: 1264
		private bool needPropertyValue;

		// Token: 0x040004F1 RID: 1265
		private bool dirty;

		// Token: 0x040004F2 RID: 1266
		private bool defaulted;

		// Token: 0x040004F3 RID: 1267
		private bool deserialized;
	}
}
