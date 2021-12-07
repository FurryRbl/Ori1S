using System;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	// Token: 0x02000068 RID: 104
	[ConfigurationCollection(typeof(ProviderSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProviderSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.ProviderSettings" /> object to the collection.</summary>
		/// <param name="provider">The <see cref="T:System.Configuration.ProviderSettings" /> object to add.</param>
		// Token: 0x0600038C RID: 908 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public void Add(ProviderSettings provider)
		{
			this.BaseAdd(provider);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x0600038D RID: 909 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009CE8 File Offset: 0x00007EE8
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSettings();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00009CF0 File Offset: 0x00007EF0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderSettings)element).Name;
		}

		/// <summary>Removes an element from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object to remove.</param>
		// Token: 0x06000390 RID: 912 RVA: 0x00009D00 File Offset: 0x00007F00
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		/// <summary>Gets or sets a value at the specified index in the <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ProviderSettings" />.</returns>
		/// <param name="index"></param>
		// Token: 0x17000109 RID: 265
		public ProviderSettings this[int n]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(n);
			}
			set
			{
				this.BaseAdd(n, value);
			}
		}

		/// <summary>Gets an item from the collection. </summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettings" /> object contained in the collection.</returns>
		/// <param name="key">A string reference to the <see cref="T:System.Configuration.ProviderSettings" /> object within the collection.</param>
		// Token: 0x1700010A RID: 266
		public ProviderSettings this[string key]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(key);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00009D38 File Offset: 0x00007F38
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettingsCollection.props;
			}
		}

		// Token: 0x0400011B RID: 283
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
