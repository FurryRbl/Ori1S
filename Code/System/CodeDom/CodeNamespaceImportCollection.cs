using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects.</summary>
	// Token: 0x02000051 RID: 81
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespaceImportCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> class. </summary>
		// Token: 0x060002AF RID: 687 RVA: 0x0000C26C File Offset: 0x0000A46C
		public CodeNamespaceImportCollection()
		{
			this.data = new ArrayList();
			this.keys = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		int ICollection.Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		// Token: 0x060002B1 RID: 689 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  This property always returns false.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  This property always returns false.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x1700007B RID: 123
		object IList.this[int index]
		{
			get
			{
				return this.data[index];
			}
			set
			{
				this[index] = (CodeNamespaceImport)value;
			}
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060002B6 RID: 694 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		int IList.Add(object value)
		{
			this.Add((CodeNamespaceImport)value);
			return this.data.Count - 1;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the value is in the list; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		bool IList.Contains(object value)
		{
			return this.data.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />. </summary>
		/// <returns>The index of <paramref name="value" /> if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060002B8 RID: 696 RVA: 0x0000C30C File Offset: 0x0000A50C
		int IList.IndexOf(object value)
		{
			return this.data.IndexOf(value);
		}

		/// <summary>Inserts an item in the <see cref="T:System.Collections.IList" /> at the specified position. </summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000C31C File Offset: 0x0000A51C
		void IList.Insert(int index, object value)
		{
			this.data.Insert(index, value);
			CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)value;
			this.keys[codeNamespaceImport.Namespace] = codeNamespaceImport;
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060002BA RID: 698 RVA: 0x0000C350 File Offset: 0x0000A550
		void IList.Remove(object value)
		{
			string @namespace = ((CodeNamespaceImport)value).Namespace;
			this.data.Remove(value);
			foreach (object obj in this.data)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.Namespace == @namespace)
				{
					this.keys[@namespace] = codeNamespaceImport;
					return;
				}
			}
			this.keys.Remove(@namespace);
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060002BB RID: 699 RVA: 0x0000C400 File Offset: 0x0000A600
		void IList.RemoveAt(int index)
		{
			string @namespace = this[index].Namespace;
			this.data.RemoveAt(index);
			foreach (object obj in this.data)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.Namespace == @namespace)
				{
					this.keys[@namespace] = codeNamespaceImport;
					return;
				}
			}
			this.keys.Remove(@namespace);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  This property always returns null.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. This property always returns false. </returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.data.IsSynchronized;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060002BE RID: 702 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060002BF RID: 703 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		/// <summary>Gets the number of namespaces in the collection.</summary>
		/// <returns>The number of namespaces in the collection.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x1700007F RID: 127
		public CodeNamespaceImport this[int index]
		{
			get
			{
				return (CodeNamespaceImport)this.data[index];
			}
			set
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)this.data[index];
				this.keys.Remove(codeNamespaceImport.Namespace);
				this.data[index] = value;
				this.keys[value.Namespace] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to add to the collection. </param>
		// Token: 0x060002C3 RID: 707 RVA: 0x0000C55C File Offset: 0x0000A75C
		public void Add(CodeNamespaceImport value)
		{
			if (value == null)
			{
				throw new NullReferenceException();
			}
			if (!this.keys.ContainsKey(value.Namespace))
			{
				this.keys[value.Namespace] = value;
				this.data.Add(value);
			}
		}

		/// <summary>Adds a set of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects to the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeNamespaceImport" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060002C4 RID: 708 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public void AddRange(CodeNamespaceImport[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (CodeNamespaceImport value2 in value)
			{
				this.Add(value2);
			}
		}

		/// <summary>Clears the collection of members.</summary>
		// Token: 0x060002C5 RID: 709 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public void Clear()
		{
			this.data.Clear();
			this.keys.Clear();
		}

		/// <summary>Gets an enumerator that enumerates the collection members.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that indicates the collection members.</returns>
		// Token: 0x060002C6 RID: 710 RVA: 0x0000C604 File Offset: 0x0000A804
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x040000D8 RID: 216
		private Hashtable keys;

		// Token: 0x040000D9 RID: 217
		private ArrayList data;
	}
}
