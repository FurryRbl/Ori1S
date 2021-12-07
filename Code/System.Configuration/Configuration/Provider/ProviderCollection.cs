using System;
using System.Collections;

namespace System.Configuration.Provider
{
	/// <summary>Represents a collection of provider objects that inherit from <see cref="T:System.Configuration.Provider.ProviderBase" />.</summary>
	// Token: 0x02000013 RID: 19
	public class ProviderCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> class. </summary>
		// Token: 0x06000095 RID: 149 RVA: 0x000024C4 File Offset: 0x000006C4
		public ProviderCollection()
		{
			this.lookup = new Hashtable(10, StringComparer.InvariantCultureIgnoreCase);
			this.values = new ArrayList();
		}

		/// <summary>Copies the elements of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> to an array, starting at a particular array index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the array at which to start copying provider instances from the collection.</param>
		// Token: 0x06000096 RID: 150 RVA: 0x000024EC File Offset: 0x000006EC
		void ICollection.CopyTo(Array array, int index)
		{
			this.values.CopyTo(array, index);
		}

		/// <summary>Adds a provider to the collection.</summary>
		/// <param name="provider">The provider to be added.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is null.- or -The length of the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is less than 1.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000097 RID: 151 RVA: 0x000024FC File Offset: 0x000006FC
		public virtual void Add(ProviderBase provider)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (provider == null || provider.Name == null)
			{
				throw new ArgumentNullException();
			}
			int num = this.values.Add(provider);
			try
			{
				this.lookup.Add(provider.Name, num);
			}
			catch
			{
				this.values.RemoveAt(num);
				throw;
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is set to read-only.</exception>
		// Token: 0x06000098 RID: 152 RVA: 0x0000258C File Offset: 0x0000078C
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.values.Clear();
			this.lookup.Clear();
		}

		/// <summary>Copies the contents of the collection to the given array starting at the specified index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the collection item at which to start the copying process.</param>
		// Token: 0x06000099 RID: 153 RVA: 0x000025B8 File Offset: 0x000007B8
		public void CopyTo(ProviderBase[] array, int index)
		{
			this.values.CopyTo(array, index);
		}

		/// <summary>Returns an object that implements the <see cref="T:System.Collections.IEnumerator" /> interface to iterate through the collection.</summary>
		/// <returns>An object that implements <see cref="T:System.Collections.IEnumerator" /> to iterate through the collection.</returns>
		// Token: 0x0600009A RID: 154 RVA: 0x000025C8 File Offset: 0x000007C8
		public IEnumerator GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		/// <summary>Removes a provider from the collection.</summary>
		/// <param name="name">The name of the provider to be removed.</param>
		/// <exception cref="T:System.NotSupportedException">The collection has been set to read-only.</exception>
		// Token: 0x0600009B RID: 155 RVA: 0x000025D8 File Offset: 0x000007D8
		public void Remove(string name)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this.lookup[name];
			if (obj == null || !(obj is int))
			{
				throw new ArgumentException();
			}
			int num = (int)obj;
			if (num >= this.values.Count)
			{
				throw new ArgumentException();
			}
			this.values.RemoveAt(num);
			this.lookup.Remove(name);
			ArrayList arrayList = new ArrayList();
			foreach (object obj2 in this.lookup)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				if ((int)dictionaryEntry.Value > num)
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			foreach (object obj3 in arrayList)
			{
				string key = (string)obj3;
				this.lookup[key] = (int)this.lookup[key] - 1;
			}
		}

		/// <summary>Sets the collection to be read-only.</summary>
		// Token: 0x0600009C RID: 156 RVA: 0x00002760 File Offset: 0x00000960
		public void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Gets the number of providers in the collection.</summary>
		/// <returns>The number of providers in the collection.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000276C File Offset: 0x0000096C
		public int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000277C File Offset: 0x0000097C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the current object.</summary>
		/// <returns>The current object.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002780 File Offset: 0x00000980
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the provider with the specified name.</summary>
		/// <returns>The provider with the specified name.</returns>
		/// <param name="name">The key by which the provider is identified.</param>
		// Token: 0x17000026 RID: 38
		public ProviderBase this[string name]
		{
			get
			{
				object obj = this.lookup[name];
				if (obj == null)
				{
					return null;
				}
				return this.values[(int)obj] as ProviderBase;
			}
		}

		// Token: 0x04000023 RID: 35
		private Hashtable lookup;

		// Token: 0x04000024 RID: 36
		private bool readOnly;

		// Token: 0x04000025 RID: 37
		private ArrayList values;
	}
}
