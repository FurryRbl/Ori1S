using System;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</summary>
	// Token: 0x0200003F RID: 63
	[ConfigurationCollection(typeof(ConnectionStringSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionStringSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConnectionStringSettings" /> object with the specified name in the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConnectionStringSettings" /> object with the specified name; otherwise, null.</returns>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x170000B0 RID: 176
		public ConnectionStringSettings this[string Name]
		{
			get
			{
				foreach (object obj in this)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)obj;
					if (configurationElement is ConnectionStringSettings)
					{
						if (string.Compare(((ConnectionStringSettings)configurationElement).Name, Name, true, CultureInfo.InvariantCulture) == 0)
						{
							return configurationElement as ConnectionStringSettings;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the connection string at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConnectionStringSettings" /> object at the specified index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x170000B1 RID: 177
		public ConnectionStringSettings this[int index]
		{
			get
			{
				return (ConnectionStringSettings)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00007E10 File Offset: 0x00006010
		[MonoTODO]
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007E18 File Offset: 0x00006018
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionStringSettings();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007E20 File Offset: 0x00006020
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConnectionStringSettings)element).Name;
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConnectionStringSettings" /> object to the collection.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object to add to the collection.</param>
		// Token: 0x06000263 RID: 611 RVA: 0x00007E30 File Offset: 0x00006030
		public void Add(ConnectionStringSettings settings)
		{
			this.BaseAdd(settings);
		}

		/// <summary>Removes all the <see cref="T:System.Configuration.ConnectionStringSettings" /> objects from the collection.</summary>
		// Token: 0x06000264 RID: 612 RVA: 0x00007E3C File Offset: 0x0000603C
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns the collection index of the passed <see cref="T:System.Configuration.ConnectionStringSettings" /> object.</summary>
		/// <returns>The collection index of the specified <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object.</returns>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x06000265 RID: 613 RVA: 0x00007E44 File Offset: 0x00006044
		public int IndexOf(ConnectionStringSettings settings)
		{
			return base.BaseIndexOf(settings);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConnectionStringSettings" /> object from the collection.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x06000266 RID: 614 RVA: 0x00007E50 File Offset: 0x00006050
		public void Remove(ConnectionStringSettings settings)
		{
			base.BaseRemove(settings.Name);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConnectionStringSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x06000267 RID: 615 RVA: 0x00007E60 File Offset: 0x00006060
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConnectionStringSettings" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x06000268 RID: 616 RVA: 0x00007E6C File Offset: 0x0000606C
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007E78 File Offset: 0x00006078
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (!(element is ConnectionStringSettings))
			{
				base.BaseAdd(element);
			}
			if (this.IndexOf((ConnectionStringSettings)element) >= 0)
			{
				throw new ConfigurationException(string.Format("The element {0} already exist!", ((ConnectionStringSettings)element).Name));
			}
			this[index] = (ConnectionStringSettings)element;
		}
	}
}
