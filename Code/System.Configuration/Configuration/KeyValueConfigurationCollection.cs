using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.KeyValueConfigurationElement" /> objects. </summary>
	// Token: 0x02000055 RID: 85
	[ConfigurationCollection(typeof(KeyValueConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class KeyValueConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="keyValue">A <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		// Token: 0x06000309 RID: 777 RVA: 0x00008F04 File Offset: 0x00007104
		public void Add(KeyValueConfigurationElement keyValue)
		{
			keyValue.Init();
			this.BaseAdd(keyValue);
		}

		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="key">A string specifying the key.</param>
		/// <param name="value">A string specifying the value.</param>
		// Token: 0x0600030A RID: 778 RVA: 0x00008F14 File Offset: 0x00007114
		public void Add(string key, string value)
		{
			this.Add(new KeyValueConfigurationElement(key, value));
		}

		/// <summary>Clears the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		// Token: 0x0600030B RID: 779 RVA: 0x00008F24 File Offset: 0x00007124
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object from the collection.</summary>
		/// <param name="key">A string specifying the <paramref name="key" />.</param>
		// Token: 0x0600030C RID: 780 RVA: 0x00008F2C File Offset: 0x0000712C
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		/// <returns>A string array.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00008F38 File Offset: 0x00007138
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[this.Count];
				int num = 0;
				foreach (object obj in this)
				{
					KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
					array[num++] = keyValueConfigurationElement.Key;
				}
				return array;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <returns>A configuration element, or null if the key does not exist in the collection.</returns>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> contained in the collection.</param>
		// Token: 0x170000D9 RID: 217
		public KeyValueConfigurationElement this[string key]
		{
			get
			{
				return (KeyValueConfigurationElement)base.BaseGet(key);
			}
		}

		/// <summary>When overridden in a derived class, the <see cref="M:System.Configuration.KeyValueConfigurationCollection.CreateNewElement" /> method creates a new <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>A new <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x0600030F RID: 783 RVA: 0x00008FCC File Offset: 0x000071CC
		protected override ConfigurationElement CreateNewElement()
		{
			return new KeyValueConfigurationElement();
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <returns>An object that acts as the key for the specified <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to which the key should be returned.</param>
		// Token: 0x06000310 RID: 784 RVA: 0x00008FD4 File Offset: 0x000071D4
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((KeyValueConfigurationElement)element).Key;
		}

		/// <summary>Gets a collection of configuration properties.</summary>
		/// <returns>A collection of configuration properties.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00008FE4 File Offset: 0x000071E4
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection();
				}
				return this.properties;
			}
		}

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection will cause an exception to be thrown.</summary>
		/// <returns>true if an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> will cause an exception to be thrown; otherwise, false. </returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009004 File Offset: 0x00007204
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040000EF RID: 239
		private ConfigurationPropertyCollection properties;
	}
}
