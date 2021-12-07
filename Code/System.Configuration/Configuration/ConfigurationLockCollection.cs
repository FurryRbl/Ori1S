using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of locked configuration objects. This class cannot be inherited.</summary>
	// Token: 0x0200002C RID: 44
	public sealed class ConfigurationLockCollection : ICollection, IEnumerable
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000621C File Offset: 0x0000441C
		internal ConfigurationLockCollection(ConfigurationElement element, ConfigurationLockType lockType)
		{
			this.names = new ArrayList();
			this.element = element;
			this.lockType = lockType;
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060001B0 RID: 432 RVA: 0x00006240 File Offset: 0x00004440
		void ICollection.CopyTo(Array array, int index)
		{
			this.names.CopyTo(array, index);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006250 File Offset: 0x00004450
		private void CheckName(string name)
		{
			bool flag = (this.lockType & ConfigurationLockType.Attribute) == ConfigurationLockType.Attribute;
			if (this.valid_name_hash == null)
			{
				this.valid_name_hash = new Hashtable();
				foreach (object obj in this.element.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (flag != configurationProperty.IsElement)
					{
						this.valid_name_hash.Add(configurationProperty.Name, true);
					}
				}
				if (!flag)
				{
					ConfigurationElementCollection defaultCollection = this.element.GetDefaultCollection();
					this.valid_name_hash.Add(defaultCollection.AddElementName, true);
					this.valid_name_hash.Add(defaultCollection.ClearElementName, true);
					this.valid_name_hash.Add(defaultCollection.RemoveElementName, true);
				}
				string[] array = new string[this.valid_name_hash.Keys.Count];
				this.valid_name_hash.Keys.CopyTo(array, 0);
				this.valid_names = string.Join(",", array);
			}
			if (this.valid_name_hash[name] == null)
			{
				throw new ConfigurationErrorsException(string.Format("The {2} '{0}' is not valid in the locked list for this section.  The following {3} can be locked: '{1}'", new object[]
				{
					name,
					this.valid_names,
					(!flag) ? "element" : "attribute",
					(!flag) ? "elements" : "attributes"
				}));
			}
		}

		/// <summary>Locks a configuration object by adding it to the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		// Token: 0x060001B2 RID: 434 RVA: 0x00006404 File Offset: 0x00004604
		public void Add(string name)
		{
			this.CheckName(name);
			if (!this.names.Contains(name))
			{
				this.names.Add(name);
				this.is_modified = true;
			}
		}

		/// <summary>Clears all configuration objects from the collection.</summary>
		// Token: 0x060001B3 RID: 435 RVA: 0x00006440 File Offset: 0x00004640
		public void Clear()
		{
			this.names.Clear();
			this.is_modified = true;
		}

		/// <summary>Verifies whether a specific configuration object is locked.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> contains the specified configuration object; otherwise, false.</returns>
		/// <param name="name">The name of the configuration object to verify.</param>
		// Token: 0x060001B4 RID: 436 RVA: 0x00006454 File Offset: 0x00004654
		public bool Contains(string name)
		{
			return this.names.Contains(name);
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060001B5 RID: 437 RVA: 0x00006464 File Offset: 0x00004664
		public void CopyTo(string[] array, int index)
		{
			this.names.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x060001B6 RID: 438 RVA: 0x00006474 File Offset: 0x00004674
		public IEnumerator GetEnumerator()
		{
			return this.names.GetEnumerator();
		}

		/// <summary>Verifies whether a specific configuration object is read-only.</summary>
		/// <returns>true if the specified configuration object in the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is read-only; otherwise, false.</returns>
		/// <param name="name">The name of the configuration object to verify.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The specified configuration object is not in the collection.</exception>
		// Token: 0x060001B7 RID: 439 RVA: 0x00006484 File Offset: 0x00004684
		[MonoInternalNote("we can't possibly *always* return false here...")]
		public bool IsReadOnly(string name)
		{
			for (int i = 0; i < this.names.Count; i++)
			{
				if ((string)this.names[i] == name)
				{
					return false;
				}
			}
			throw new ConfigurationErrorsException(string.Format("The entry '{0}' is not in the collection.", name));
		}

		/// <summary>Removes a configuration object from the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		// Token: 0x060001B8 RID: 440 RVA: 0x000064DC File Offset: 0x000046DC
		public void Remove(string name)
		{
			this.names.Remove(name);
			this.is_modified = true;
		}

		/// <summary>Locks a set of configuration objects based on the supplied list.</summary>
		/// <param name="attributeList">A comma-delimited string.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when an item in the <paramref name="attributeList" /> parameter is not a valid lockable configuration attribute.</exception>
		// Token: 0x060001B9 RID: 441 RVA: 0x000064F4 File Offset: 0x000046F4
		public void SetFromList(string attributeList)
		{
			this.Clear();
			char[] separator = new char[]
			{
				','
			};
			string[] array = attributeList.Split(separator);
			foreach (string text in array)
			{
				this.Add(text.Trim());
			}
		}

		/// <summary>Gets a list of configuration objects contained in the collection.</summary>
		/// <returns>A comma-delimited string that lists the lock configuration objects in the collection.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006548 File Offset: 0x00004748
		public string AttributeList
		{
			get
			{
				string[] array = new string[this.names.Count];
				this.names.CopyTo(array, 0);
				return string.Join(",", array);
			}
		}

		/// <summary>Gets the number of locked configuration objects contained in the collection.</summary>
		/// <returns>The number of locked configuration objects contained in the collection.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006580 File Offset: 0x00004780
		public int Count
		{
			get
			{
				return this.names.Count;
			}
		}

		/// <summary>Gets a value specifying whether the collection of locked objects has parent elements.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection has parent elements; otherwise, false.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006590 File Offset: 0x00004790
		[MonoTODO]
		public bool HasParentElements
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value specifying whether the collection has been modified.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection has been modified; otherwise, false.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006594 File Offset: 0x00004794
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000659C File Offset: 0x0000479C
		[MonoTODO]
		public bool IsModified
		{
			get
			{
				return this.is_modified;
			}
			internal set
			{
				this.is_modified = value;
			}
		}

		/// <summary>Gets a value specifying whether the collection is synchronized.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is synchronized; otherwise, false.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000065A8 File Offset: 0x000047A8
		[MonoTODO]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000065AC File Offset: 0x000047AC
		[MonoTODO]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000088 RID: 136
		private ArrayList names;

		// Token: 0x04000089 RID: 137
		private ConfigurationElement element;

		// Token: 0x0400008A RID: 138
		private ConfigurationLockType lockType;

		// Token: 0x0400008B RID: 139
		private bool is_modified;

		// Token: 0x0400008C RID: 140
		private Hashtable valid_name_hash;

		// Token: 0x0400008D RID: 141
		private string valid_names;
	}
}
