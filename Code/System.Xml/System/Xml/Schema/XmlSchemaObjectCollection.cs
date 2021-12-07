using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>A collection of <see cref="T:System.Xml.Schema.XmlSchemaObject" />s.</summary>
	// Token: 0x0200022F RID: 559
	public class XmlSchemaObjectCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the XmlSchemaObjectCollection class.</summary>
		// Token: 0x06001621 RID: 5665 RVA: 0x00065F9C File Offset: 0x0006419C
		public XmlSchemaObjectCollection()
		{
		}

		/// <summary>Initializes a new instance of the XmlSchemaObjectCollection class that takes an <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <param name="parent">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x06001622 RID: 5666 RVA: 0x00065FA4 File Offset: 0x000641A4
		public XmlSchemaObjectCollection(XmlSchemaObject parent)
		{
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x170006C5 RID: 1733
		public virtual XmlSchemaObject this[int index]
		{
			get
			{
				return (XmlSchemaObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <returns>The index at which the item has been added.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter specified is not of type <see cref="T:System.Xml.Schema.XmlSchemaExternal" /> or its derived types <see cref="T:System.Xml.Schema.XmlSchemaImport" />, <see cref="T:System.Xml.Schema.XmlSchemaInclude" />, and <see cref="T:System.Xml.Schema.XmlSchemaRedefine" />.</exception>
		// Token: 0x06001625 RID: 5669 RVA: 0x00065FD0 File Offset: 0x000641D0
		public int Add(XmlSchemaObject item)
		{
			return base.List.Add(item);
		}

		/// <summary>Indicates if the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is in the XmlSchemaObjectCollection.</summary>
		/// <returns>true if the specified qualified name is in the collection; otherwise, returns false. If null is supplied, false is returned because there is no qualified name with a null name.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x06001626 RID: 5670 RVA: 0x00065FE0 File Offset: 0x000641E0
		public bool Contains(XmlSchemaObject item)
		{
			return base.List.Contains(item);
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchemaObject" />s from the collection into the given array, starting at the given index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the XmlSchemaObjectCollection. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the array at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multi-dimensional.- or - <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.- or - The number of elements in the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is greater than the available space from index to the end of the destination array. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> cannot be cast automatically to the type of the destination array. </exception>
		// Token: 0x06001627 RID: 5671 RVA: 0x00065FF0 File Offset: 0x000641F0
		public void CopyTo(XmlSchemaObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator for iterating through the XmlSchemaObjects contained in the XmlSchemaObjectCollection.</summary>
		/// <returns>The iterator returns <see cref="T:System.Xml.Schema.XmlSchemaObjectEnumerator" />.</returns>
		// Token: 0x06001628 RID: 5672 RVA: 0x00066000 File Offset: 0x00064200
		public new XmlSchemaObjectEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectEnumerator(base.List);
		}

		/// <summary>Gets the collection index corresponding to the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The index corresponding to the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> whose index you want to return. </param>
		// Token: 0x06001629 RID: 5673 RVA: 0x00066010 File Offset: 0x00064210
		public int IndexOf(XmlSchemaObject item)
		{
			return base.List.IndexOf(item);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <param name="index">The zero-based index at which an item should be inserted. </param>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		// Token: 0x0600162A RID: 5674 RVA: 0x00066020 File Offset: 0x00064220
		public void Insert(int index, XmlSchemaObject item)
		{
			base.List.Insert(index, item);
		}

		/// <summary>OnClear is invoked before the standard Clear behavior. For more information, see OnClear method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		// Token: 0x0600162B RID: 5675 RVA: 0x00066030 File Offset: 0x00064230
		protected override void OnClear()
		{
		}

		/// <summary>OnInsert is invoked before the standard Insert behavior. For more information, see OnInsert method <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x0600162C RID: 5676 RVA: 0x00066034 File Offset: 0x00064234
		protected override void OnInsert(int index, object item)
		{
		}

		/// <summary>OnRemove is invoked before the standard Remove behavior. For more information, see the OnRemove method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x0600162D RID: 5677 RVA: 0x00066038 File Offset: 0x00064238
		protected override void OnRemove(int index, object item)
		{
		}

		/// <summary>OnSet is invoked before the standard Set behavior. For more information, see the OnSet method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="oldValue">The old value. </param>
		/// <param name="newValue">The new value. </param>
		// Token: 0x0600162E RID: 5678 RVA: 0x0006603C File Offset: 0x0006423C
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Removes an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> from the XmlSchemaObjectCollection.</summary>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to remove. </param>
		// Token: 0x0600162F RID: 5679 RVA: 0x00066040 File Offset: 0x00064240
		public void Remove(XmlSchemaObject item)
		{
			base.List.Remove(item);
		}
	}
}
