using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of settings property values that map <see cref="T:System.Configuration.SettingsProperty" /> objects to <see cref="T:System.Configuration.SettingsPropertyValue" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F7 RID: 503
	public class SettingsPropertyValueCollection : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</summary>
		// Token: 0x0600113F RID: 4415 RVA: 0x0002E208 File Offset: 0x0002C408
		public SettingsPropertyValueCollection()
		{
			this.items = new Hashtable();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsPropertyValue" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to add an item to the collection, but the collection was marked as read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001140 RID: 4416 RVA: 0x0002E21C File Offset: 0x0002C41C
		public void Add(SettingsPropertyValue property)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Add(property.Name, property);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0002E244 File Offset: 0x0002C444
		internal void Add(SettingsPropertyValueCollection vals)
		{
			foreach (object obj in vals)
			{
				SettingsPropertyValue property = (SettingsPropertyValue)obj;
				this.Add(property);
			}
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsPropertyValue" /> objects from the collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001142 RID: 4418 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Clear();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001143 RID: 4419 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		public object Clone()
		{
			return new SettingsPropertyValueCollection
			{
				items = (Hashtable)this.items.Clone()
			};
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection to an array.</summary>
		/// <param name="array">The array to copy the collection to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001144 RID: 4420 RVA: 0x0002E2FC File Offset: 0x0002C4FC
		public void CopyTo(Array array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001145 RID: 4421 RVA: 0x0002E310 File Offset: 0x0002C510
		public IEnumerator GetEnumerator()
		{
			return this.items.Values.GetEnumerator();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsPropertyValue" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to remove an item from the collection, but the collection was marked as read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001146 RID: 4422 RVA: 0x0002E324 File Offset: 0x0002C524
		public void Remove(string name)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Remove(name);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001147 RID: 4423 RVA: 0x0002E344 File Offset: 0x0002C544
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0002E350 File Offset: 0x0002C550
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection is synchronized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0002E360 File Offset: 0x0002C560
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an item from the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsPropertyValue" /> object with the specified <paramref name="name" />.</returns>
		/// <param name="name">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EA RID: 1002
		public SettingsPropertyValue this[string name]
		{
			get
			{
				return (SettingsPropertyValue)this.items[name];
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0002E37C File Offset: 0x0002C57C
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040004EA RID: 1258
		private Hashtable items;

		// Token: 0x040004EB RID: 1259
		private bool isReadOnly;
	}
}
