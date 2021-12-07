using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a configuration element containing a collection of child elements.</summary>
	// Token: 0x02000023 RID: 35
	[DebuggerDisplay("Count = {Count}")]
	public abstract class ConfigurationElementCollection : ConfigurationElement, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElementCollection" /> class. </summary>
		// Token: 0x0600014F RID: 335 RVA: 0x00004F48 File Offset: 0x00003148
		protected ConfigurationElementCollection()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.ConfigurationElementCollection" /> class.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> comparer to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x06000150 RID: 336 RVA: 0x00004F88 File Offset: 0x00003188
		protected ConfigurationElementCollection(IComparer comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>Copies the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to an array.</summary>
		/// <param name="arr">Array to which to copy this <see cref="T:System.Configuration.ConfigurationElementCollection" />.</param>
		/// <param name="index">Index location at which to begin copying.</param>
		// Token: 0x06000151 RID: 337 RVA: 0x00004FC4 File Offset: 0x000031C4
		void ICollection.CopyTo(Array arr, int index)
		{
			this.list.CopyTo(arr, index);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004FD4 File Offset: 0x000031D4
		internal override void InitFromProperty(PropertyInformation propertyInfo)
		{
			ConfigurationCollectionAttribute configurationCollectionAttribute = propertyInfo.Property.CollectionAttribute;
			if (configurationCollectionAttribute == null)
			{
				configurationCollectionAttribute = (Attribute.GetCustomAttribute(propertyInfo.Type, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
			}
			if (configurationCollectionAttribute != null)
			{
				this.addElementName = configurationCollectionAttribute.AddItemName;
				this.clearElementName = configurationCollectionAttribute.ClearItemsName;
				this.removeElementName = configurationCollectionAttribute.RemoveItemName;
			}
			base.InitFromProperty(propertyInfo);
		}

		/// <summary>Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005040 File Offset: 0x00003240
		public virtual ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005044 File Offset: 0x00003244
		private bool IsBasic
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.BasicMap || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005060 File Offset: 0x00003260
		private bool IsAlternate
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000507C File Offset: 0x0000327C
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.</summary>
		/// <returns>The name of the collection; otherwise, an empty string. The default is an empty string.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000508C File Offset: 0x0000328C
		protected virtual string ElementName
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the collection has been cleared.</summary>
		/// <returns>true if the collection has been cleared; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.</exception>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005094 File Offset: 0x00003294
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000509C File Offset: 0x0000329C
		public bool EmitClear
		{
			get
			{
				return this.emitClear;
			}
			set
			{
				this.emitClear = value;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.ConfigurationElementCollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000050A8 File Offset: 0x000032A8
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object used to synchronize access to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An object used to synchronize access to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000050AC File Offset: 0x000032AC
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.ConfigurationElement" /> to the <see cref="T:System.Configuration.ConfigurationElementCollection" /> will cause an exception to be thrown.</summary>
		/// <returns>true if an attempt to add a duplicate <see cref="T:System.Configuration.ConfigurationElement" /> to this <see cref="T:System.Configuration.ConfigurationElementCollection" /> will cause an exception to be thrown; otherwise, false. </returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000050B0 File Offset: 0x000032B0
		protected virtual bool ThrowOnDuplicate
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the add operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class. </summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000050D0 File Offset: 0x000032D0
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000050D8 File Offset: 0x000032D8
		protected internal string AddElementName
		{
			get
			{
				return this.addElementName;
			}
			set
			{
				this.addElementName = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the clear operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class. </summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000050E4 File Offset: 0x000032E4
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000050EC File Offset: 0x000032EC
		protected internal string ClearElementName
		{
			get
			{
				return this.clearElementName;
			}
			set
			{
				this.clearElementName = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the remove operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class. </summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000050F8 File Offset: 0x000032F8
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005100 File Offset: 0x00003300
		protected internal string RemoveElementName
		{
			get
			{
				return this.removeElementName;
			}
			set
			{
				this.removeElementName = value;
			}
		}

		/// <summary>Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add. </param>
		// Token: 0x06000163 RID: 355 RVA: 0x0000510C File Offset: 0x0000330C
		protected virtual void BaseAdd(ConfigurationElement element)
		{
			this.BaseAdd(element, this.ThrowOnDuplicate);
		}

		/// <summary>Adds a configuration element to the configuration element collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add. </param>
		/// <param name="throwIfExists">true to throw an exception if the <see cref="T:System.Configuration.ConfigurationElement" /> specified is already contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />; otherwise, false. </param>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Configuration.ConfigurationElement" /> to add already exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> and the <paramref name="throwIfExists" /> parameter is true. </exception>
		// Token: 0x06000164 RID: 356 RVA: 0x0000511C File Offset: 0x0000331C
		protected void BaseAdd(ConfigurationElement element, bool throwIfExists)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			if (this.IsAlternate)
			{
				this.list.Insert(this.inheritedLimitIndex, element);
				this.inheritedLimitIndex++;
			}
			else
			{
				int num = this.IndexOfKey(this.GetElementKey(element));
				if (num >= 0)
				{
					if (element.Equals(this.list[num]))
					{
						return;
					}
					if (throwIfExists)
					{
						throw new ConfigurationException("Duplicate element in collection");
					}
					this.list.RemoveAt(num);
				}
				this.list.Add(element);
			}
			this.modified = true;
		}

		/// <summary>Adds a configuration element to the configuration element collection.</summary>
		/// <param name="index">The index location at which to add the specified <see cref="T:System.Configuration.ConfigurationElement" />. </param>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add. </param>
		// Token: 0x06000165 RID: 357 RVA: 0x000051D0 File Offset: 0x000033D0
		protected virtual void BaseAdd(int index, ConfigurationElement element)
		{
			if (this.ThrowOnDuplicate && this.BaseIndexOf(element) != -1)
			{
				throw new ConfigurationException("Duplicate element in collection");
			}
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			if (this.IsAlternate && index > this.inheritedLimitIndex)
			{
				throw new ConfigurationErrorsException("Can't insert new elements below the inherited elements.");
			}
			if (!this.IsAlternate && index <= this.inheritedLimitIndex)
			{
				throw new ConfigurationErrorsException("Can't insert new elements above the inherited elements.");
			}
			this.list.Insert(index, element);
			this.modified = true;
		}

		/// <summary>Removes all configuration element objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.- or -A collection item has been locked in a higher-level configuration.</exception>
		// Token: 0x06000166 RID: 358 RVA: 0x00005270 File Offset: 0x00003470
		protected internal void BaseClear()
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			this.list.Clear();
			this.modified = true;
		}

		/// <summary>Gets the configuration element at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index.</returns>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationElement" /> to return. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="index" /> is less than 0.- or -There is no <see cref="T:System.Configuration.ConfigurationElement" /> at the specified <paramref name="index" />.</exception>
		// Token: 0x06000167 RID: 359 RVA: 0x000052A8 File Offset: 0x000034A8
		protected internal ConfigurationElement BaseGet(int index)
		{
			return (ConfigurationElement)this.list[index];
		}

		/// <summary>Returns the configuration element with the specified key.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key; otherwise, null.</returns>
		/// <param name="key">The key of the element to return. </param>
		// Token: 0x06000168 RID: 360 RVA: 0x000052BC File Offset: 0x000034BC
		protected internal ConfigurationElement BaseGet(object key)
		{
			int num = this.IndexOfKey(key);
			if (num != -1)
			{
				return (ConfigurationElement)this.list[num];
			}
			return null;
		}

		/// <summary>Returns an array of the keys for all of the configuration elements contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An array that contains the keys for all of the <see cref="T:System.Configuration.ConfigurationElement" /> objects contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		// Token: 0x06000169 RID: 361 RVA: 0x000052EC File Offset: 0x000034EC
		protected internal object[] BaseGetAllKeys()
		{
			object[] array = new object[this.list.Count];
			for (int i = 0; i < this.list.Count; i++)
			{
				array[i] = this.BaseGetKey(i);
			}
			return array;
		}

		/// <summary>Gets the key for the <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index location.</summary>
		/// <returns>The key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <param name="index">The index location for the <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="index" /> is less than 0.- or -There is no <see cref="T:System.Configuration.ConfigurationElement" /> at the specified <paramref name="index" />.</exception>
		// Token: 0x0600016A RID: 362 RVA: 0x00005334 File Offset: 0x00003534
		protected internal object BaseGetKey(int index)
		{
			if (index < 0 || index >= this.list.Count)
			{
				throw new ConfigurationErrorsException(string.Format("Index {0} is out of range", index));
			}
			return this.GetElementKey((ConfigurationElement)this.list[index]).ToString();
		}

		/// <summary>The index of the specified <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Configuration.ConfigurationElement" />; otherwise, -1.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> for the specified index location. </param>
		// Token: 0x0600016B RID: 363 RVA: 0x0000538C File Offset: 0x0000358C
		protected int BaseIndexOf(ConfigurationElement element)
		{
			return this.list.IndexOf(element);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000539C File Offset: 0x0000359C
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.CompareKeys(this.GetElementKey((ConfigurationElement)this.list[i]), key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key has been removed from the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key has been removed; otherwise, false. The default is false.</returns>
		/// <param name="key">The key of the element to check.</param>
		// Token: 0x0600016D RID: 365 RVA: 0x000053EC File Offset: 0x000035EC
		protected internal bool BaseIsRemoved(object key)
		{
			if (this.removed == null)
			{
				return false;
			}
			foreach (object obj in this.removed)
			{
				ConfigurationElement element = (ConfigurationElement)obj;
				if (this.CompareKeys(this.GetElementKey(element), key))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.ConfigurationElement" /> from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.ConfigurationElement" /> to remove. </param>
		/// <exception cref="T:System.Exception">No <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key exists in the collection, the element has already been removed, or the element cannot be removed because the value of its <see cref="P:System.Configuration.ConfigurationProperty.Type" /> is not <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" />. </exception>
		// Token: 0x0600016E RID: 366 RVA: 0x00005480 File Offset: 0x00003680
		protected internal void BaseRemove(object key)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			int num = this.IndexOfKey(key);
			if (num != -1)
			{
				this.BaseRemoveAt(num);
				this.modified = true;
			}
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index location.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationElement" /> to remove. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.- or -<paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Configuration.ConfigurationElement" /> objects in the collection.- or -The <see cref="T:System.Configuration.ConfigurationElement" /> object has already been removed.- or -The value of the <see cref="T:System.Configuration.ConfigurationElement" /> object has been locked at a higher level.- or -The <see cref="T:System.Configuration.ConfigurationElement" /> object was inherited.- or -The value of the <see cref="T:System.Configuration.ConfigurationElement" /> object's <see cref="P:System.Configuration.ConfigurationProperty.Type" /> is not <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" /> or <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMapAlternate" />.</exception>
		// Token: 0x0600016F RID: 367 RVA: 0x000054C0 File Offset: 0x000036C0
		protected internal void BaseRemoveAt(int index)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			ConfigurationElement configurationElement = (ConfigurationElement)this.list[index];
			if (!this.IsElementRemovable(configurationElement))
			{
				throw new ConfigurationErrorsException("Element can't be removed from element collection.");
			}
			if (this.inherited != null && this.inherited.Contains(configurationElement))
			{
				throw new ConfigurationErrorsException("Inherited items can't be removed.");
			}
			this.list.RemoveAt(index);
			this.modified = true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005548 File Offset: 0x00003748
		private bool CompareKeys(object key1, object key2)
		{
			if (this.comparer != null)
			{
				return this.comparer.Compare(key1, key2) == 0;
			}
			return object.Equals(key1, key2);
		}

		/// <summary>Copies the contents of the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to an array.</summary>
		/// <param name="array">Array to which to copy the contents of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</param>
		/// <param name="index">Index location at which to begin copying.</param>
		// Token: 0x06000171 RID: 369 RVA: 0x00005570 File Offset: 0x00003770
		public void CopyTo(ConfigurationElement[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		// Token: 0x06000172 RID: 370
		protected abstract ConfigurationElement CreateNewElement();

		/// <summary>Creates a new <see cref="T:System.Configuration.ConfigurationElement" /> when overridden in a derived class.</summary>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to create. </param>
		// Token: 0x06000173 RID: 371 RVA: 0x00005580 File Offset: 0x00003780
		protected virtual ConfigurationElement CreateNewElement(string elementName)
		{
			return this.CreateNewElement();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005588 File Offset: 0x00003788
		private ConfigurationElement CreateNewElementInternal(string elementName)
		{
			ConfigurationElement configurationElement;
			if (elementName == null)
			{
				configurationElement = this.CreateNewElement();
			}
			else
			{
				configurationElement = this.CreateNewElement(elementName);
			}
			configurationElement.Init();
			return configurationElement;
		}

		/// <summary>Compares the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to the specified object.</summary>
		/// <returns>true if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElementCollection" /> instance; otherwise, false. The default is false.</returns>
		/// <param name="compareTo">The object to compare. </param>
		// Token: 0x06000175 RID: 373 RVA: 0x000055B8 File Offset: 0x000037B8
		public override bool Equals(object compareTo)
		{
			ConfigurationElementCollection configurationElementCollection = compareTo as ConfigurationElementCollection;
			if (configurationElementCollection == null)
			{
				return false;
			}
			if (base.GetType() != configurationElementCollection.GetType())
			{
				return false;
			}
			if (this.Count != configurationElementCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (!this.BaseGet(i).Equals(configurationElementCollection.BaseGet(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <returns>An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for. </param>
		// Token: 0x06000176 RID: 374
		protected abstract object GetElementKey(ConfigurationElement element);

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.ConfigurationElementCollection" /> instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.ConfigurationElementCollection" /> current instance.</returns>
		// Token: 0x06000177 RID: 375 RVA: 0x0000562C File Offset: 0x0000382C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				num += this.BaseGet(i).GetHashCode();
			}
			return num;
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> which is used to iterate through the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> which is used to iterate through the <see cref="T:System.Configuration.ConfigurationElementCollection" /></returns>
		// Token: 0x06000178 RID: 376 RVA: 0x00005664 File Offset: 0x00003864
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>true if the element exists in the collection; otherwise, false. The default is false.</returns>
		/// <param name="elementName">The name of the element to verify. </param>
		// Token: 0x06000179 RID: 377 RVA: 0x00005674 File Offset: 0x00003874
		protected virtual bool IsElementName(string elementName)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> can be removed from the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Configuration.ConfigurationElement" /> can be removed from this <see cref="T:System.Configuration.ConfigurationElementCollection" />; otherwise, false. The default is true.</returns>
		/// <param name="element">The element to check.</param>
		// Token: 0x0600017A RID: 378 RVA: 0x00005678 File Offset: 0x00003878
		protected virtual bool IsElementRemovable(ConfigurationElement element)
		{
			return !this.IsReadOnly();
		}

		/// <summary>Indicates whether this <see cref="T:System.Configuration.ConfigurationElementCollection" /> has been modified since it was last saved or loaded when overridden in a derived class.</summary>
		/// <returns>true if any contained element has been modified; otherwise, false</returns>
		// Token: 0x0600017B RID: 379 RVA: 0x00005684 File Offset: 0x00003884
		protected internal override bool IsModified()
		{
			return this.modified;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only; otherwise, false.</returns>
		// Token: 0x0600017C RID: 380 RVA: 0x0000568C File Offset: 0x0000388C
		[MonoTODO]
		public override bool IsReadOnly()
		{
			return base.IsReadOnly();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005694 File Offset: 0x00003894
		internal override bool HasValues()
		{
			return this.list.Count > 0;
		}

		/// <summary>Resets the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to its unmodified state when overridden in a derived class.</summary>
		/// <param name="parentElement">The <see cref="T:System.Configuration.ConfigurationElement" /> representing the collection parent element, if any; otherwise, null. </param>
		// Token: 0x0600017E RID: 382 RVA: 0x000056A4 File Offset: 0x000038A4
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			bool isBasic = this.IsBasic;
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			for (int i = 0; i < configurationElementCollection.Count; i++)
			{
				ConfigurationElement parentElement2 = configurationElementCollection.BaseGet(i);
				ConfigurationElement configurationElement = this.CreateNewElementInternal(null);
				configurationElement.Reset(parentElement2);
				this.BaseAdd(configurationElement);
				if (isBasic)
				{
					if (this.inherited == null)
					{
						this.inherited = new ArrayList();
					}
					this.inherited.Add(configurationElement);
				}
			}
			if (this.IsAlternate)
			{
				this.inheritedLimitIndex = 0;
			}
			else
			{
				this.inheritedLimitIndex = this.Count - 1;
			}
			this.modified = false;
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElementCollection.IsModified" /> property to false when overridden in a derived class.</summary>
		// Token: 0x0600017F RID: 383 RVA: 0x00005750 File Offset: 0x00003950
		protected internal override void ResetModified()
		{
			this.modified = false;
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElementCollection.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object and for all sub-elements.</summary>
		// Token: 0x06000180 RID: 384 RVA: 0x0000575C File Offset: 0x0000395C
		[MonoTODO]
		protected internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		/// <summary>Writes the configuration data to an XML element in the configuration file when overridden in a derived class.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> was written to the configuration file successfully.</returns>
		/// <param name="writer">Output stream that writes XML to the configuration file. </param>
		/// <param name="serializeCollectionKey">true to serialize the collection key; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">One of the elements in the collection was added or replaced and starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x06000181 RID: 385 RVA: 0x00005764 File Offset: 0x00003964
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (serializeCollectionKey)
			{
				return base.SerializeElement(writer, serializeCollectionKey);
			}
			bool flag = false;
			if (this.IsBasic)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)this.list[i];
					if (this.ElementName != string.Empty)
					{
						flag = (configurationElement.SerializeToXmlElement(writer, this.ElementName) || flag);
					}
					else
					{
						flag = (configurationElement.SerializeElement(writer, false) || flag);
					}
				}
			}
			else
			{
				if (this.emitClear)
				{
					writer.WriteElementString(this.clearElementName, string.Empty);
					flag = true;
				}
				if (this.removed != null)
				{
					for (int j = 0; j < this.removed.Count; j++)
					{
						writer.WriteStartElement(this.removeElementName);
						((ConfigurationElement)this.removed[j]).SerializeElement(writer, true);
						writer.WriteEndElement();
					}
					flag = (flag || this.removed.Count > 0);
				}
				for (int k = 0; k < this.list.Count; k++)
				{
					ConfigurationElement configurationElement2 = (ConfigurationElement)this.list[k];
					configurationElement2.SerializeToXmlElement(writer, this.addElementName);
				}
				flag = (flag || this.list.Count > 0);
			}
			return flag;
		}

		/// <summary>Causes the configuration system to throw an exception.</summary>
		/// <returns>true if the unrecognized element was deserialized successfully; otherwise, false. The default is false.</returns>
		/// <param name="elementName">The name of the unrecognized element. </param>
		/// <param name="reader">An input stream that reads XML from the configuration file. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element specified in <paramref name="elementName" /> is the &lt;clear&gt; element.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementName" /> starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x06000182 RID: 386 RVA: 0x000058E8 File Offset: 0x00003AE8
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (this.IsBasic)
			{
				ConfigurationElement configurationElement = null;
				if (elementName == this.ElementName)
				{
					configurationElement = this.CreateNewElementInternal(null);
				}
				if (this.IsElementName(elementName))
				{
					configurationElement = this.CreateNewElementInternal(elementName);
				}
				if (configurationElement != null)
				{
					configurationElement.DeserializeElement(reader, false);
					this.BaseAdd(configurationElement);
					this.modified = false;
					return true;
				}
			}
			else if (elementName == this.clearElementName)
			{
				reader.MoveToContent();
				if (reader.MoveToNextAttribute())
				{
					throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.");
				}
				reader.MoveToElement();
				reader.Skip();
				this.BaseClear();
				this.emitClear = true;
				this.modified = false;
				return true;
			}
			else
			{
				if (elementName == this.removeElementName)
				{
					ConfigurationElement origElement = this.CreateNewElementInternal(null);
					ConfigurationElementCollection.ConfigurationRemoveElement configurationRemoveElement = new ConfigurationElementCollection.ConfigurationRemoveElement(origElement, this);
					configurationRemoveElement.DeserializeElement(reader, true);
					this.BaseRemove(configurationRemoveElement.KeyValue);
					this.modified = false;
					return true;
				}
				if (elementName == this.addElementName)
				{
					ConfigurationElement configurationElement2 = this.CreateNewElementInternal(null);
					configurationElement2.DeserializeElement(reader, false);
					this.BaseAdd(configurationElement2);
					this.modified = false;
					return true;
				}
			}
			return false;
		}

		/// <summary>Reverses the effect of merging configuration information from different levels of the configuration hierarchy </summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> object at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" /> object of the current element, or null if this is the top level.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> enumerated value that determines which property values to include.</param>
		// Token: 0x06000183 RID: 387 RVA: 0x00005A24 File Offset: 0x00003C24
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode updateMode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)sourceElement;
			ConfigurationElementCollection configurationElementCollection2 = (ConfigurationElementCollection)parentElement;
			for (int i = 0; i < configurationElementCollection.Count; i++)
			{
				ConfigurationElement configurationElement = configurationElementCollection.BaseGet(i);
				object elementKey = configurationElementCollection.GetElementKey(configurationElement);
				ConfigurationElement configurationElement2 = (configurationElementCollection2 == null) ? null : configurationElementCollection2.BaseGet(elementKey);
				if (configurationElement2 != null && updateMode != ConfigurationSaveMode.Full)
				{
					ConfigurationElement configurationElement3 = this.CreateNewElementInternal(null);
					configurationElement3.Unmerge(configurationElement, configurationElement2, ConfigurationSaveMode.Minimal);
					if (configurationElement3.HasValues())
					{
						this.BaseAdd(configurationElement3);
					}
				}
				else
				{
					ConfigurationElement configurationElement4 = this.CreateNewElementInternal(null);
					configurationElement4.Unmerge(configurationElement, null, ConfigurationSaveMode.Full);
					this.BaseAdd(configurationElement4);
				}
			}
			if (updateMode == ConfigurationSaveMode.Full)
			{
				this.EmitClear = true;
			}
			else if (configurationElementCollection2 != null)
			{
				for (int j = 0; j < configurationElementCollection2.Count; j++)
				{
					ConfigurationElement configurationElement5 = configurationElementCollection2.BaseGet(j);
					object elementKey2 = configurationElementCollection2.GetElementKey(configurationElement5);
					if (configurationElementCollection.IndexOfKey(elementKey2) == -1)
					{
						if (this.removed == null)
						{
							this.removed = new ArrayList();
						}
						this.removed.Add(configurationElement5);
					}
				}
			}
		}

		// Token: 0x04000067 RID: 103
		private ArrayList list = new ArrayList();

		// Token: 0x04000068 RID: 104
		private ArrayList removed;

		// Token: 0x04000069 RID: 105
		private ArrayList inherited;

		// Token: 0x0400006A RID: 106
		private bool emitClear;

		// Token: 0x0400006B RID: 107
		private bool modified;

		// Token: 0x0400006C RID: 108
		private IComparer comparer;

		// Token: 0x0400006D RID: 109
		private int inheritedLimitIndex;

		// Token: 0x0400006E RID: 110
		private string addElementName = "add";

		// Token: 0x0400006F RID: 111
		private string clearElementName = "clear";

		// Token: 0x04000070 RID: 112
		private string removeElementName = "remove";

		// Token: 0x02000024 RID: 36
		private sealed class ConfigurationRemoveElement : ConfigurationElement
		{
			// Token: 0x06000184 RID: 388 RVA: 0x00005B50 File Offset: 0x00003D50
			internal ConfigurationRemoveElement(ConfigurationElement origElement, ConfigurationElementCollection origCollection)
			{
				this._origElement = origElement;
				this._origCollection = origCollection;
				foreach (object obj in origElement.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (configurationProperty.IsKey)
					{
						this.properties.Add(configurationProperty);
					}
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000185 RID: 389 RVA: 0x00005BF0 File Offset: 0x00003DF0
			internal object KeyValue
			{
				get
				{
					foreach (object obj in this.Properties)
					{
						ConfigurationProperty property = (ConfigurationProperty)obj;
						this._origElement[property] = base[property];
					}
					return this._origCollection.GetElementKey(this._origElement);
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000186 RID: 390 RVA: 0x00005C7C File Offset: 0x00003E7C
			protected internal override ConfigurationPropertyCollection Properties
			{
				get
				{
					return this.properties;
				}
			}

			// Token: 0x04000071 RID: 113
			private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

			// Token: 0x04000072 RID: 114
			private readonly ConfigurationElement _origElement;

			// Token: 0x04000073 RID: 115
			private readonly ConfigurationElementCollection _origCollection;
		}
	}
}
