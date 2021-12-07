using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to instantiate a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ConfigurationPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Configuration.ConfigurationPropertyAttribute" /> class.</summary>
		/// <param name="name">Name of the <see cref="T:System.Configuration.ConfigurationProperty" /> object defined.</param>
		// Token: 0x060001EF RID: 495 RVA: 0x00006E3C File Offset: 0x0000503C
		public ConfigurationPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets a value indicating whether this is a key property for the decorated element property.</summary>
		/// <returns>true if the property is a key property for an element of the collection; otherwise, false. The default is false.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006E58 File Offset: 0x00005058
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00006E68 File Offset: 0x00005068
		public bool IsKey
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsKey) != ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsKey;
				}
				else
				{
					this.flags &= ~ConfigurationPropertyOptions.IsKey;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this is the default property collection for the decorated configuration property. </summary>
		/// <returns>true if the property represents the default collection of an element; otherwise, false. The default is false.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006EA0 File Offset: 0x000050A0
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00006EB0 File Offset: 0x000050B0
		public bool IsDefaultCollection
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsDefaultCollection) != ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsDefaultCollection;
				}
				else
				{
					this.flags &= ~ConfigurationPropertyOptions.IsDefaultCollection;
				}
			}
		}

		/// <summary>Gets or sets the default value for the decorated property.</summary>
		/// <returns>The object representing the default value of the decorated configuration-element property.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006EE8 File Offset: 0x000050E8
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006EF0 File Offset: 0x000050F0
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
			set
			{
				this.default_value = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> for the decorated configuration-element property.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values associated with the property.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006EFC File Offset: 0x000050FC
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00006F04 File Offset: 0x00005104
		public ConfigurationPropertyOptions Options
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		/// <summary>Gets the name of the decorated configuration-element property.</summary>
		/// <returns>The name of the decorated configuration-element property.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006F10 File Offset: 0x00005110
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets a value indicating whether the decorated element property is required.</summary>
		/// <returns>true if the property is required; otherwise, false. The default is false.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006F18 File Offset: 0x00005118
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00006F28 File Offset: 0x00005128
		public bool IsRequired
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsRequired) != ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsRequired;
				}
				else
				{
					this.flags &= ~ConfigurationPropertyOptions.IsRequired;
				}
			}
		}

		// Token: 0x0400009C RID: 156
		private string name;

		// Token: 0x0400009D RID: 157
		private object default_value = ConfigurationProperty.NoDefaultValue;

		// Token: 0x0400009E RID: 158
		private ConfigurationPropertyOptions flags;
	}
}
