using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Configuration
{
	/// <summary>Represents a collection of configuration-element properties.</summary>
	// Token: 0x02000032 RID: 50
	public class ConfigurationPropertyCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> class. </summary>
		// Token: 0x060001FB RID: 507 RVA: 0x00006F60 File Offset: 0x00005160
		public ConfigurationPropertyCollection()
		{
			this.collection = new List<ConfigurationProperty>();
		}

		/// <summary>Copies this collection to an array.</summary>
		/// <param name="array">The array to which to copy.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		// Token: 0x060001FC RID: 508 RVA: 0x00006F74 File Offset: 0x00005174
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collection).CopyTo(array, index);
		}

		/// <summary>Gets the number of properties in the collection.</summary>
		/// <returns>The number of properties in the collection.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006F84 File Offset: 0x00005184
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationProperty" /> with the specified <paramref name="name" />.</returns>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to return. </param>
		// Token: 0x1700008F RID: 143
		public ConfigurationProperty this[string name]
		{
			get
			{
				foreach (ConfigurationProperty configurationProperty in this.collection)
				{
					if (configurationProperty.Name == name)
					{
						return configurationProperty;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007010 File Offset: 0x00005210
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007014 File Offset: 0x00005214
		public object SyncRoot
		{
			get
			{
				return this.collection;
			}
		}

		/// <summary>Adds a configuration property to the collection.</summary>
		/// <param name="property">The <see cref="T:System.Configuration.ConfigurationProperty" />  to add. </param>
		// Token: 0x06000201 RID: 513 RVA: 0x0000701C File Offset: 0x0000521C
		public void Add(ConfigurationProperty property)
		{
			this.collection.Add(property);
		}

		/// <summary>Specifies whether the configuration property is contained in this collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="name">An identifier for the <see cref="T:System.Configuration.ConfigurationProperty" /> to verify. </param>
		// Token: 0x06000202 RID: 514 RVA: 0x0000702C File Offset: 0x0000522C
		public bool Contains(string name)
		{
			ConfigurationProperty configurationProperty = this[name];
			return configurationProperty != null && this.collection.Contains(configurationProperty);
		}

		/// <summary>Copies this ConfigurationPropertyCollection to an array.</summary>
		/// <param name="array">Array to which to copy.</param>
		/// <param name="index">Index at which to begin copying.</param>
		// Token: 0x06000203 RID: 515 RVA: 0x00007058 File Offset: 0x00005258
		public void CopyTo(ConfigurationProperty[] array, int index)
		{
			this.collection.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection</returns>
		// Token: 0x06000204 RID: 516 RVA: 0x00007068 File Offset: 0x00005268
		public IEnumerator GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		/// <summary>Removes a configuration property from the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> was removed; otherwise, false.</returns>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to remove. </param>
		// Token: 0x06000205 RID: 517 RVA: 0x0000707C File Offset: 0x0000527C
		public bool Remove(string name)
		{
			return this.collection.Remove(this[name]);
		}

		/// <summary>Removes all configuration property objects from the collection.</summary>
		// Token: 0x06000206 RID: 518 RVA: 0x00007090 File Offset: 0x00005290
		public void Clear()
		{
			this.collection.Clear();
		}

		// Token: 0x0400009F RID: 159
		private List<ConfigurationProperty> collection;
	}
}
