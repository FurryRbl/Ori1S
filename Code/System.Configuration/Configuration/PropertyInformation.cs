using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Contains meta-information on an individual property within the configuration. This type cannot be inherited.</summary>
	// Token: 0x0200005E RID: 94
	public sealed class PropertyInformation
	{
		// Token: 0x06000342 RID: 834 RVA: 0x000094DC File Offset: 0x000076DC
		internal PropertyInformation(ConfigurationElement owner, ConfigurationProperty property)
		{
			this.owner = owner;
			this.property = property;
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> object.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000094F4 File Offset: 0x000076F4
		public TypeConverter Converter
		{
			get
			{
				return this.property.Converter;
			}
		}

		/// <summary>Gets an object containing the default value related to a configuration attribute.</summary>
		/// <returns>An object containing the default value of the configuration attribute.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00009504 File Offset: 0x00007704
		public object DefaultValue
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		/// <summary>Gets the description of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The description of the configuration attribute.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00009514 File Offset: 0x00007714
		public string Description
		{
			get
			{
				return this.property.Description;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is a key.</summary>
		/// <returns>true if the configuration attribute is a key; otherwise, false.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009524 File Offset: 0x00007724
		public bool IsKey
		{
			get
			{
				return this.property.IsKey;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is locked.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object is locked; otherwise, false.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00009534 File Offset: 0x00007734
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000953C File Offset: 0x0000773C
		[MonoTODO]
		public bool IsLocked
		{
			get
			{
				return this.isLocked;
			}
			internal set
			{
				this.isLocked = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute has been modified.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object has been modified; otherwise, false.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00009548 File Offset: 0x00007748
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00009550 File Offset: 0x00007750
		public bool IsModified
		{
			get
			{
				return this.isModified;
			}
			internal set
			{
				this.isModified = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is required.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object is required; otherwise, false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000955C File Offset: 0x0000775C
		public bool IsRequired
		{
			get
			{
				return this.property.IsRequired;
			}
		}

		/// <summary>Gets the line number in the configuration file related to the configuration attribute.</summary>
		/// <returns>A line number of the configuration file.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000956C File Offset: 0x0000776C
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00009574 File Offset: 0x00007774
		[MonoTODO]
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			internal set
			{
				this.lineNumber = value;
			}
		}

		/// <summary>Gets the name of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009580 File Offset: 0x00007780
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		/// <summary>Gets the source file that corresponds to a configuration attribute.</summary>
		/// <returns>The source file of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00009590 File Offset: 0x00007790
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00009598 File Offset: 0x00007798
		[MonoTODO]
		public string Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000095A4 File Offset: 0x000077A4
		public Type Type
		{
			get
			{
				return this.property.Type;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000095B4 File Offset: 0x000077B4
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.property.Validator;
			}
		}

		/// <summary>Gets or sets an object containing the value related to a configuration attribute.</summary>
		/// <returns>An object containing the value for the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000095C4 File Offset: 0x000077C4
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00009648 File Offset: 0x00007848
		public object Value
		{
			get
			{
				if (this.origin == PropertyValueOrigin.Default)
				{
					if (!this.property.IsElement)
					{
						return this.DefaultValue;
					}
					ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(this.Type, true);
					configurationElement.InitFromProperty(this);
					if (this.owner != null && this.owner.IsReadOnly())
					{
						configurationElement.SetReadOnly();
					}
					this.val = configurationElement;
					this.origin = PropertyValueOrigin.Inherited;
				}
				return this.val;
			}
			set
			{
				this.val = value;
				this.isModified = true;
				this.origin = PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009660 File Offset: 0x00007860
		internal void Reset(PropertyInformation parentProperty)
		{
			if (parentProperty != null)
			{
				if (this.property.IsElement)
				{
					((ConfigurationElement)this.Value).Reset((ConfigurationElement)parentProperty.Value);
				}
				else
				{
					this.val = parentProperty.Value;
					this.origin = PropertyValueOrigin.Inherited;
				}
			}
			else
			{
				this.origin = PropertyValueOrigin.Default;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000096C4 File Offset: 0x000078C4
		internal bool IsElement
		{
			get
			{
				return this.property.IsElement;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyValueOrigin" /> object related to the configuration attribute. </summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyValueOrigin" /> object.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000096D4 File Offset: 0x000078D4
		public PropertyValueOrigin ValueOrigin
		{
			get
			{
				return this.origin;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000096DC File Offset: 0x000078DC
		internal string GetStringValue()
		{
			return this.property.ConvertToString(this.Value);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000096F0 File Offset: 0x000078F0
		internal void SetStringValue(string value)
		{
			this.val = this.property.ConvertFromString(value);
			if (!object.Equals(this.val, this.DefaultValue))
			{
				this.origin = PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00009724 File Offset: 0x00007924
		internal ConfigurationProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x04000100 RID: 256
		private bool isLocked;

		// Token: 0x04000101 RID: 257
		private bool isModified;

		// Token: 0x04000102 RID: 258
		private int lineNumber;

		// Token: 0x04000103 RID: 259
		private string source;

		// Token: 0x04000104 RID: 260
		private object val;

		// Token: 0x04000105 RID: 261
		private PropertyValueOrigin origin;

		// Token: 0x04000106 RID: 262
		private readonly ConfigurationElement owner;

		// Token: 0x04000107 RID: 263
		private readonly ConfigurationProperty property;
	}
}
