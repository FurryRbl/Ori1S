using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.NameValueConfigurationElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200005A RID: 90
	[ConfigurationCollection(typeof(NameValueConfigurationElement), AddItemName = "add", RemoveItemName = "remove", ClearItemsName = "clear", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class NameValueConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.NameValueConfigurationCollection" />.</summary>
		/// <returns>A string array.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000930C File Offset: 0x0000750C
		public string[] AllKeys
		{
			get
			{
				return (string[])base.BaseGetAllKeys();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <returns>A <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> contained in the collection.</param>
		// Token: 0x170000E4 RID: 228
		public NameValueConfigurationElement this[string name]
		{
			get
			{
				return (NameValueConfigurationElement)base.BaseGet(name);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00009334 File Offset: 0x00007534
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NameValueConfigurationCollection.properties;
			}
		}

		/// <summary>Adds a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object to the collection.</summary>
		/// <param name="nameValue">A  <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x06000331 RID: 817 RVA: 0x0000933C File Offset: 0x0000753C
		public void Add(NameValueConfigurationElement nameValue)
		{
			base.BaseAdd(nameValue, false);
		}

		/// <summary>Clears the <see cref="T:System.Configuration.NameValueConfigurationCollection" />.</summary>
		// Token: 0x06000332 RID: 818 RVA: 0x00009348 File Offset: 0x00007548
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009350 File Offset: 0x00007550
		protected override ConfigurationElement CreateNewElement()
		{
			return new NameValueConfigurationElement(string.Empty, string.Empty);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009364 File Offset: 0x00007564
		protected override object GetElementKey(ConfigurationElement element)
		{
			NameValueConfigurationElement nameValueConfigurationElement = (NameValueConfigurationElement)element;
			return nameValueConfigurationElement.Name;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object from the collection based on the provided parameter.</summary>
		/// <param name="nameValue">A <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x06000335 RID: 821 RVA: 0x00009380 File Offset: 0x00007580
		public void Remove(NameValueConfigurationElement nameValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object from the collection based on the provided parameter.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x06000336 RID: 822 RVA: 0x00009388 File Offset: 0x00007588
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x040000FB RID: 251
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
