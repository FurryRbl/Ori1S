using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F3 RID: 499
	public class SettingsPropertyCollection : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</summary>
		// Token: 0x06001111 RID: 4369 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
		public SettingsPropertyCollection()
		{
			this.items = new Hashtable();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsProperty" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001112 RID: 4370 RVA: 0x0002DED8 File Offset: 0x0002C0D8
		public void Add(SettingsProperty property)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnAdd(property);
			this.items.Add(property.Name, property);
			this.OnAddComplete(property);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsProperty" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001113 RID: 4371 RVA: 0x0002DF18 File Offset: 0x0002C118
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnClear();
			this.items.Clear();
			this.OnClearComplete();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001114 RID: 4372 RVA: 0x0002DF50 File Offset: 0x0002C150
		public object Clone()
		{
			return new SettingsPropertyCollection
			{
				items = (Hashtable)this.items.Clone()
			};
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001115 RID: 4373 RVA: 0x0002DF7C File Offset: 0x0002C17C
		public void CopyTo(Array array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001116 RID: 4374 RVA: 0x0002DF90 File Offset: 0x0002C190
		public IEnumerator GetEnumerator()
		{
			return this.items.Values.GetEnumerator();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsProperty" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001117 RID: 4375 RVA: 0x0002DFA4 File Offset: 0x0002C1A4
		public void Remove(string name)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			SettingsProperty property = (SettingsProperty)this.items[name];
			this.OnRemove(property);
			this.items.Remove(name);
			this.OnRemoveComplete(property);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001118 RID: 4376 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		/// <summary>Performs additional, custom processing when adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06001119 RID: 4377 RVA: 0x0002DFFC File Offset: 0x0002C1FC
		protected virtual void OnAdd(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x0600111A RID: 4378 RVA: 0x0002E000 File Offset: 0x0002C200
		protected virtual void OnAddComplete(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing when clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x0600111B RID: 4379 RVA: 0x0002E004 File Offset: 0x0002C204
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional, custom processing after clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x0600111C RID: 4380 RVA: 0x0002E008 File Offset: 0x0002C208
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional, custom processing when removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x0600111D RID: 4381 RVA: 0x0002E00C File Offset: 0x0002C20C
		protected virtual void OnRemove(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x0600111E RID: 4382 RVA: 0x0002E010 File Offset: 0x0002C210
		protected virtual void OnRemoveComplete(SettingsProperty property)
		{
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0002E014 File Offset: 0x0002C214
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.SettingsPropertyCollection" /> is synchronized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0002E024 File Offset: 0x0002C224
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object with the specified <paramref name="name" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DD RID: 989
		public SettingsProperty this[string name]
		{
			get
			{
				return (SettingsProperty)this.items[name];
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0002E03C File Offset: 0x0002C23C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040004DF RID: 1247
		private Hashtable items;

		// Token: 0x040004E0 RID: 1248
		private bool isReadOnly;
	}
}
