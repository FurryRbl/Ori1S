using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains meta-information about an individual element within the configuration. This class cannot be inherited.</summary>
	// Token: 0x02000045 RID: 69
	public sealed class ElementInformation
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00008178 File Offset: 0x00006378
		internal ElementInformation(ConfigurationElement owner, PropertyInformation propertyInfo)
		{
			this.propertyInfo = propertyInfo;
			this.owner = owner;
			this.properties = new PropertyInformationCollection();
			foreach (object obj in owner.Properties)
			{
				ConfigurationProperty property = (ConfigurationProperty)obj;
				this.properties.Add(new PropertyInformation(owner, property));
			}
		}

		/// <summary>Gets the errors for the associated element and subelements</summary>
		/// <returns>The collection containing the errors for the associated element and subelements</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00008214 File Offset: 0x00006414
		[MonoTODO]
		public ICollection Errors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is a <see cref="T:System.Configuration.ConfigurationElementCollection" /> collection.</summary>
		/// <returns>true if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is a <see cref="T:System.Configuration.ConfigurationElementCollection" /> collection; otherwise, false.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000821C File Offset: 0x0000641C
		public bool IsCollection
		{
			get
			{
				return this.owner is ConfigurationElementCollection;
			}
		}

		/// <summary>Gets a value that indicates whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object cannot be modified.</summary>
		/// <returns>true if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object cannot be modified; otherwise, false.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000822C File Offset: 0x0000642C
		public bool IsLocked
		{
			get
			{
				return this.propertyInfo != null && this.propertyInfo.IsLocked;
			}
		}

		/// <summary>Gets a value indicating whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is in the configuration file.</summary>
		/// <returns>true if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is in the configuration file; otherwise, false.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000824C File Offset: 0x0000644C
		[MonoTODO]
		public bool IsPresent
		{
			get
			{
				return this.propertyInfo != null;
			}
		}

		/// <summary>Gets the line number in the configuration file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is defined.</summary>
		/// <returns>The line number in the configuration file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is defined.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000825C File Offset: 0x0000645C
		public int LineNumber
		{
			get
			{
				return (this.propertyInfo == null) ? 0 : this.propertyInfo.LineNumber;
			}
		}

		/// <summary>Gets the source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</summary>
		/// <returns>The source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000827C File Offset: 0x0000647C
		public string Source
		{
			get
			{
				return (this.propertyInfo == null) ? null : this.propertyInfo.Source;
			}
		}

		/// <summary>Gets the type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000829C File Offset: 0x0000649C
		public Type Type
		{
			get
			{
				return (this.propertyInfo == null) ? this.owner.GetType() : this.propertyInfo.Type;
			}
		}

		/// <summary>Gets the object used to validate the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The object used to validate the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000082D0 File Offset: 0x000064D0
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return (this.propertyInfo == null) ? new DefaultValidator() : this.propertyInfo.Validator;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008300 File Offset: 0x00006500
		public PropertyInformationCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008308 File Offset: 0x00006508
		internal void Reset(ElementInformation parentInfo)
		{
			foreach (object obj in this.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				PropertyInformation parentProperty = parentInfo.Properties[propertyInformation.Name];
				propertyInformation.Reset(parentProperty);
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly PropertyInformation propertyInfo;

		// Token: 0x040000D2 RID: 210
		private readonly ConfigurationElement owner;

		// Token: 0x040000D3 RID: 211
		private readonly PropertyInformationCollection properties;
	}
}
