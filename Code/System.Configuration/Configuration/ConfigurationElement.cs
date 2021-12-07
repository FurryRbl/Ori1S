using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a configuration element within a configuration file.</summary>
	// Token: 0x02000021 RID: 33
	public abstract class ConfigurationElement
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003CE0 File Offset: 0x00001EE0
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00003CE8 File Offset: 0x00001EE8
		internal Configuration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				this._configuration = value;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003CF4 File Offset: 0x00001EF4
		internal virtual void InitFromProperty(PropertyInformation propertyInfo)
		{
			this.elementInfo = new ElementInformation(this, propertyInfo);
			this.Init();
		}

		/// <summary>Gets an <see cref="T:System.Configuration.ElementInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" /> object. </summary>
		/// <returns>An <see cref="T:System.Configuration.ElementInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003D0C File Offset: 0x00001F0C
		public ElementInformation ElementInformation
		{
			get
			{
				if (this.elementInfo == null)
				{
					this.elementInfo = new ElementInformation(this, null);
				}
				return this.elementInfo;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003D2C File Offset: 0x00001F2C
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003D34 File Offset: 0x00001F34
		internal string RawXml
		{
			get
			{
				return this.rawXml;
			}
			set
			{
				if (this.rawXml == null || value != null)
				{
					this.rawXml = value;
				}
			}
		}

		/// <summary>Sets the <see cref="T:System.Configuration.ConfigurationElement" /> object to its initial state.</summary>
		// Token: 0x06000121 RID: 289 RVA: 0x00003D50 File Offset: 0x00001F50
		protected internal virtual void Init()
		{
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationElementProperty" /> object that represents the <see cref="T:System.Configuration.ConfigurationElement" /> object itself.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementProperty" /> that represents the <see cref="T:System.Configuration.ConfigurationElement" /> itself.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003D54 File Offset: 0x00001F54
		protected internal virtual ConfigurationElementProperty ElementProperty
		{
			get
			{
				if (this.elementProperty == null)
				{
					this.elementProperty = new ConfigurationElementProperty(this.ElementInformation.Validator);
				}
				return this.elementProperty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> for the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current element is not associated with a context.</exception>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003D80 File Offset: 0x00001F80
		[MonoTODO]
		protected ContextInformation EvaluationContext
		{
			get
			{
				if (this.Configuration != null)
				{
					return this.Configuration.EvaluationContext;
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the collection of locked attributes.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public ConfigurationLockCollection LockAllAttributesExcept
		{
			get
			{
				if (this.lockAllAttributesExcept == null)
				{
					this.lockAllAttributesExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute | ConfigurationLockType.Exclude);
				}
				return this.lockAllAttributesExcept;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public ConfigurationLockCollection LockAllElementsExcept
		{
			get
			{
				if (this.lockAllElementsExcept == null)
				{
					this.lockAllElementsExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Element | ConfigurationLockType.Exclude);
				}
				return this.lockAllElementsExcept;
			}
		}

		/// <summary>Gets the collection of locked attributes </summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public ConfigurationLockCollection LockAttributes
		{
			get
			{
				if (this.lockAttributes == null)
				{
					this.lockAttributes = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute);
				}
				return this.lockAttributes;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003E08 File Offset: 0x00002008
		public ConfigurationLockCollection LockElements
		{
			get
			{
				if (this.lockElements == null)
				{
					this.lockElements = new ConfigurationLockCollection(this, ConfigurationLockType.Element);
				}
				return this.lockElements;
			}
		}

		/// <summary>Gets or sets a value indicating whether the element is locked.</summary>
		/// <returns>true if the element is locked; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element has already been locked at a higher configuration level.</exception>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003E28 File Offset: 0x00002028
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003E30 File Offset: 0x00002030
		public bool LockItem
		{
			get
			{
				return this.lockItem;
			}
			set
			{
				this.lockItem = value;
			}
		}

		/// <summary>Adds the invalid-property errors in this <see cref="T:System.Configuration.ConfigurationElement" /> object, and in all subelements, to the passed list.</summary>
		/// <param name="errorList">An object that implements the <see cref="T:System.Collections.IList" /> interface.</param>
		// Token: 0x0600012A RID: 298 RVA: 0x00003E3C File Offset: 0x0000203C
		[MonoTODO]
		protected virtual void ListErrors(IList list)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a property to the specified value.</summary>
		/// <param name="prop">The element property to set. </param>
		/// <param name="value">The value to assign to the property.</param>
		/// <param name="ignoreLocks">true if the locks on the property should be ignored; otherwise, false.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs if the element is read-only or <paramref name="ignoreLocks" /> is true but the locks cannot be ignored.</exception>
		// Token: 0x0600012B RID: 299 RVA: 0x00003E44 File Offset: 0x00002044
		[MonoTODO]
		protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
		{
			try
			{
				prop.Validate(value);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(string.Format("The value for the property '{0}' is not valid. The error is: {1}", prop.Name, ex.Message), ex);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003E9C File Offset: 0x0000209C
		internal ConfigurationPropertyCollection GetKeyProperties()
		{
			if (this.keyProps != null)
			{
				return this.keyProps;
			}
			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (configurationProperty.IsKey)
				{
					configurationPropertyCollection.Add(configurationProperty);
				}
			}
			return this.keyProps = configurationPropertyCollection;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003F3C File Offset: 0x0000213C
		internal ConfigurationElementCollection GetDefaultCollection()
		{
			if (this.defaultCollection != null)
			{
				return this.defaultCollection;
			}
			ConfigurationProperty configurationProperty = null;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty2 = (ConfigurationProperty)obj;
				if (configurationProperty2.IsDefaultCollection)
				{
					configurationProperty = configurationProperty2;
					break;
				}
			}
			if (configurationProperty != null)
			{
				this.defaultCollection = (this[configurationProperty] as ConfigurationElementCollection);
			}
			return this.defaultCollection;
		}

		/// <summary>Gets or sets a property or attribute of this configuration element.</summary>
		/// <returns>The specified property, attribute, or child element.</returns>
		/// <param name="prop">The property to access. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">
		///   <paramref name="prop" /> is null or does not exist within the element.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read only or locked.</exception>
		// Token: 0x17000053 RID: 83
		protected internal object this[ConfigurationProperty property]
		{
			get
			{
				return this[property.Name];
			}
			set
			{
				this[property.Name] = value;
			}
		}

		/// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
		/// <returns>The specified property, attribute, or child element</returns>
		/// <param name="propertyName">The name of the <see cref="T:System.Configuration.ConfigurationProperty" /> to access.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read-only or locked.</exception>
		// Token: 0x17000054 RID: 84
		protected internal object this[string property_name]
		{
			get
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[property_name];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + property_name + "' not found in configuration element");
				}
				return propertyInformation.Value;
			}
			set
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[property_name];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + property_name + "' not found in configuration element");
				}
				this.SetPropertyValue(propertyInformation.Property, value, false);
				propertyInformation.Value = value;
				this.modified = true;
			}
		}

		/// <summary>Gets the collection of properties.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000040A4 File Offset: 0x000022A4
		protected internal virtual ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.map == null)
				{
					this.map = ElementMap.GetMap(base.GetType());
				}
				return this.map.Properties;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.ConfigurationElement" /> instance to the specified object.</summary>
		/// <returns>true if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElement" /> instance; otherwise, false. The default is false. </returns>
		/// <param name="compareTo">The object to compare with.</param>
		// Token: 0x06000133 RID: 307 RVA: 0x000040D0 File Offset: 0x000022D0
		public override bool Equals(object compareTo)
		{
			ConfigurationElement configurationElement = compareTo as ConfigurationElement;
			if (configurationElement == null)
			{
				return false;
			}
			if (base.GetType() != configurationElement.GetType())
			{
				return false;
			}
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty property = (ConfigurationProperty)obj;
				if (!object.Equals(this[property], configurationElement[property]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</summary>
		/// <returns>A unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</returns>
		// Token: 0x06000134 RID: 308 RVA: 0x00004180 File Offset: 0x00002380
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty property = (ConfigurationProperty)obj;
				object obj2 = this[property];
				if (obj2 != null)
				{
					num += obj2.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000420C File Offset: 0x0000240C
		internal virtual bool HasValues()
		{
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004290 File Offset: 0x00002490
		internal virtual bool HasLocalModifications()
		{
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere && propertyInformation.IsModified)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element to read is locked.- or -An attribute of the current node is not recognized.- or -The lock status of the current node cannot be determined.  </exception>
		// Token: 0x06000137 RID: 311 RVA: 0x00004320 File Offset: 0x00002520
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			Hashtable hashtable = new Hashtable();
			reader.MoveToContent();
			while (reader.MoveToNextAttribute())
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[reader.LocalName];
				if (propertyInformation == null || (serializeCollectionKey && !propertyInformation.IsKey))
				{
					if (reader.LocalName == "lockAllAttributesExcept")
					{
						this.LockAllAttributesExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAllElementsExcept")
					{
						this.LockAllElementsExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAttributes")
					{
						this.LockAttributes.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockElements")
					{
						this.LockElements.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockItem")
					{
						this.LockItem = (reader.Value.ToLowerInvariant() == "true");
					}
					else if (!(reader.LocalName == "xmlns"))
					{
						if (!(this is ConfigurationSection) || !(reader.LocalName == "configSource"))
						{
							if (!this.OnDeserializeUnrecognizedAttribute(reader.LocalName, reader.Value))
							{
								throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.", reader);
							}
						}
					}
				}
				else
				{
					if (hashtable.ContainsKey(propertyInformation))
					{
						throw new ConfigurationErrorsException("The attribute '" + propertyInformation.Name + "' may only appear once in this element.", reader);
					}
					try
					{
						string value = reader.Value;
						this.ValidateValue(propertyInformation.Property, value);
						propertyInformation.SetStringValue(value);
					}
					catch (ConfigurationErrorsException)
					{
						throw;
					}
					catch (ConfigurationException)
					{
						throw;
					}
					catch (Exception ex)
					{
						string message = string.Format("The value for the property '{0}' is not valid. The error is: {1}", propertyInformation.Name, ex.Message);
						throw new ConfigurationErrorsException(message, reader);
					}
					hashtable[propertyInformation] = propertyInformation.Name;
				}
			}
			reader.MoveToElement();
			if (!reader.IsEmptyElement)
			{
				int depth = reader.Depth;
				reader.ReadStartElement();
				reader.MoveToContent();
				PropertyInformation propertyInformation2;
				for (;;)
				{
					if (reader.NodeType != XmlNodeType.Element)
					{
						reader.Skip();
					}
					else
					{
						propertyInformation2 = this.ElementInformation.Properties[reader.LocalName];
						if (propertyInformation2 == null || (serializeCollectionKey && !propertyInformation2.IsKey))
						{
							if (!this.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
							{
								if (propertyInformation2 != null)
								{
									break;
								}
								ConfigurationElementCollection configurationElementCollection = this.GetDefaultCollection();
								if (configurationElementCollection == null || !configurationElementCollection.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
								{
									break;
								}
							}
						}
						else
						{
							if (!propertyInformation2.IsElement)
							{
								goto Block_23;
							}
							if (hashtable.Contains(propertyInformation2))
							{
								goto Block_24;
							}
							ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation2.Value;
							configurationElement.DeserializeElement(reader, serializeCollectionKey);
							hashtable[propertyInformation2] = propertyInformation2.Name;
							if (depth == reader.Depth)
							{
								reader.Read();
							}
						}
					}
					if (depth >= reader.Depth)
					{
						goto IL_3A5;
					}
				}
				throw new ConfigurationErrorsException("Unrecognized element '" + reader.LocalName + "'.", reader);
				Block_23:
				throw new ConfigurationException("Property '" + propertyInformation2.Name + "' is not a ConfigurationElement.");
				Block_24:
				throw new ConfigurationErrorsException("The element <" + propertyInformation2.Name + "> may only appear once in this section.", reader);
			}
			reader.Skip();
			IL_3A5:
			this.modified = false;
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation3 = (PropertyInformation)obj;
				if (!string.IsNullOrEmpty(propertyInformation3.Name) && propertyInformation3.IsRequired && !hashtable.ContainsKey(propertyInformation3) && this.ElementInformation.Properties[propertyInformation3.Name] == null)
				{
					object obj2 = this.OnRequiredPropertyNotFound(propertyInformation3.Name);
					if (!object.Equals(obj2, propertyInformation3.DefaultValue))
					{
						propertyInformation3.Value = obj2;
						propertyInformation3.IsModified = false;
					}
				}
			}
			this.PostDeserialize();
		}

		/// <summary>Gets a value indicating whether an unknown attribute is encountered during deserialization.</summary>
		/// <returns>true when an unknown attribute is encountered while deserializing; otherwise, false.</returns>
		/// <param name="name">The name of the unrecognized attribute.</param>
		/// <param name="value">The value of the unrecognized attribute.</param>
		// Token: 0x06000138 RID: 312 RVA: 0x0000480C File Offset: 0x00002A0C
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether an unknown element is encountered during deserialization.</summary>
		/// <returns>true when an unknown element is encountered while deserializing; otherwise, false.</returns>
		/// <param name="elementName">The name of the unknown subelement.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> being used for deserialization.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element identified by <paramref name="elementName" /> is locked.- or -One or more of the element's attributes is locked.- or -<paramref name="elementName" /> is unrecognized, or the element has an unrecognized attribute.- or -The element has a Boolean attribute with an invalid value.- or -An attempt was made to deserialize a property more than once.- or -An attempt was made to deserialize a property that is not a valid member of the element.- or -The element cannot contain a CDATA or text element.</exception>
		// Token: 0x06000139 RID: 313 RVA: 0x00004810 File Offset: 0x00002A10
		protected virtual bool OnDeserializeUnrecognizedElement(string element, XmlReader reader)
		{
			return false;
		}

		/// <summary>Throws an exception when a required property is not found.</summary>
		/// <returns>None.</returns>
		/// <param name="name">The name of the required attribute that was not found.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">In all cases.</exception>
		// Token: 0x0600013A RID: 314 RVA: 0x00004814 File Offset: 0x00002A14
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new ConfigurationErrorsException("Required attribute '" + name + "' not found.");
		}

		/// <summary>Called before serialization.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that will be used to serialize the <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		// Token: 0x0600013B RID: 315 RVA: 0x0000482C File Offset: 0x00002A2C
		protected virtual void PreSerialize(XmlWriter writer)
		{
		}

		/// <summary>Called after deserialization.</summary>
		// Token: 0x0600013C RID: 316 RVA: 0x00004830 File Offset: 0x00002A30
		protected virtual void PostDeserialize()
		{
		}

		/// <summary>Used to initialize a default set of values for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		// Token: 0x0600013D RID: 317 RVA: 0x00004834 File Offset: 0x00002A34
		protected internal virtual void InitializeDefault()
		{
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded, when implemented in a derived class.</summary>
		/// <returns>true if the element has been modified; otherwise, false. </returns>
		// Token: 0x0600013E RID: 318 RVA: 0x00004838 File Offset: 0x00002A38
		protected internal virtual bool IsModified()
		{
			return this.modified;
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElement.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElement" /> object and all subelements.</summary>
		// Token: 0x0600013F RID: 319 RVA: 0x00004840 File Offset: 0x00002A40
		protected internal virtual void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, false.</returns>
		// Token: 0x06000140 RID: 320 RVA: 0x0000484C File Offset: 0x00002A4C
		public virtual bool IsReadOnly()
		{
			return this.readOnly;
		}

		/// <summary>Resets the internal state of the <see cref="T:System.Configuration.ConfigurationElement" /> object, including the locks and the properties collections.</summary>
		/// <param name="parentElement">The parent node of the configuration element.</param>
		// Token: 0x06000141 RID: 321 RVA: 0x00004854 File Offset: 0x00002A54
		protected internal virtual void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				this.ElementInformation.Reset(parentElement.ElementInformation);
			}
			else
			{
				this.InitializeDefault();
			}
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to false when implemented in a derived class.</summary>
		// Token: 0x06000142 RID: 322 RVA: 0x00004884 File Offset: 0x00002A84
		protected internal virtual void ResetModified()
		{
			this.modified = false;
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				propertyInformation.IsModified = false;
			}
		}

		/// <summary>Writes the contents of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <returns>true if any data was actually serialized; otherwise, false.</returns>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file. </param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current attribute is locked at a higher configuration level.</exception>
		// Token: 0x06000143 RID: 323 RVA: 0x00004900 File Offset: 0x00002B00
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			this.PreSerialize(writer);
			if (serializeCollectionKey)
			{
				ConfigurationPropertyCollection keyProperties = this.GetKeyProperties();
				foreach (object obj in keyProperties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					writer.WriteAttributeString(configurationProperty.Name, configurationProperty.ConvertToString(this[configurationProperty.Name]));
				}
				return keyProperties.Count > 0;
			}
			bool flag = false;
			foreach (object obj2 in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj2;
				if (!propertyInformation.IsElement && propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					if (!object.Equals(propertyInformation.Value, propertyInformation.DefaultValue))
					{
						writer.WriteAttributeString(propertyInformation.Name, propertyInformation.GetStringValue());
						flag = true;
					}
				}
			}
			foreach (object obj3 in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation2 = (PropertyInformation)obj3;
				if (propertyInformation2.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation2.Value;
					if (configurationElement != null)
					{
						flag = (configurationElement.SerializeToXmlElement(writer, propertyInformation2.Name) || flag);
					}
				}
			}
			return flag;
		}

		/// <summary>Writes the outer tags of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <returns>true if writing was successful; otherwise, false.</returns>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file. </param>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to be written. </param>
		/// <exception cref="T:System.Exception">The element has multiple child elements. </exception>
		// Token: 0x06000144 RID: 324 RVA: 0x00004AF8 File Offset: 0x00002CF8
		protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (!this.HasValues())
			{
				return false;
			}
			if (elementName != null && elementName != string.Empty)
			{
				writer.WriteStartElement(elementName);
			}
			bool result = this.SerializeElement(writer, false);
			if (elementName != null && elementName != string.Empty)
			{
				writer.WriteEndElement();
			}
			return result;
		}

		/// <summary>Modifies the <see cref="T:System.Configuration.ConfigurationElement" /> object to remove all values that should not be saved. </summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" />, or null if this is the top level.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> that determines which property values to include.</param>
		// Token: 0x06000145 RID: 325 RVA: 0x00004B58 File Offset: 0x00002D58
		protected internal virtual void Unmerge(ConfigurationElement source, ConfigurationElement parent, ConfigurationSaveMode updateMode)
		{
			if (parent != null && source.GetType() != parent.GetType())
			{
				throw new ConfigurationException("Can't unmerge two elements of different type");
			}
			foreach (object obj in source.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					PropertyInformation propertyInformation2 = this.ElementInformation.Properties[propertyInformation.Name];
					object value = propertyInformation.Value;
					if (parent == null || !parent.HasValue(propertyInformation.Name))
					{
						propertyInformation2.Value = value;
					}
					else if (value != null)
					{
						object obj2 = parent[propertyInformation.Name];
						if (propertyInformation.IsElement)
						{
							if (obj2 != null)
							{
								ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation2.Value;
								configurationElement.Unmerge((ConfigurationElement)value, (ConfigurationElement)obj2, updateMode);
							}
							else
							{
								propertyInformation2.Value = value;
							}
						}
						else if (!object.Equals(value, obj2) || updateMode == ConfigurationSaveMode.Full || (updateMode == ConfigurationSaveMode.Modified && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere))
						{
							propertyInformation2.Value = value;
						}
					}
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004CC4 File Offset: 0x00002EC4
		internal bool HasValue(string propName)
		{
			PropertyInformation propertyInformation = this.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin != PropertyValueOrigin.Default;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004CF8 File Offset: 0x00002EF8
		internal bool IsReadFromConfig(string propName)
		{
			PropertyInformation propertyInformation = this.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004D2C File Offset: 0x00002F2C
		private void ValidateValue(ConfigurationProperty p, string value)
		{
			ConfigurationValidatorBase validator;
			if (p == null || (validator = p.Validator) == null)
			{
				return;
			}
			if (!validator.CanValidate(p.Type))
			{
				throw new ConfigurationException(string.Format("Validator does not support type {0}", p.Type));
			}
			validator.Validate(p.ConvertFromString(value));
		}

		// Token: 0x04000056 RID: 86
		private string rawXml;

		// Token: 0x04000057 RID: 87
		private bool modified;

		// Token: 0x04000058 RID: 88
		private ElementMap map;

		// Token: 0x04000059 RID: 89
		private ConfigurationPropertyCollection keyProps;

		// Token: 0x0400005A RID: 90
		private ConfigurationElementCollection defaultCollection;

		// Token: 0x0400005B RID: 91
		private bool readOnly;

		// Token: 0x0400005C RID: 92
		private ElementInformation elementInfo;

		// Token: 0x0400005D RID: 93
		private ConfigurationElementProperty elementProperty;

		// Token: 0x0400005E RID: 94
		private Configuration _configuration;

		// Token: 0x0400005F RID: 95
		private ConfigurationLockCollection lockAllAttributesExcept;

		// Token: 0x04000060 RID: 96
		private ConfigurationLockCollection lockAllElementsExcept;

		// Token: 0x04000061 RID: 97
		private ConfigurationLockCollection lockAttributes;

		// Token: 0x04000062 RID: 98
		private ConfigurationLockCollection lockElements;

		// Token: 0x04000063 RID: 99
		private bool lockItem;
	}
}
