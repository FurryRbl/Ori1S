using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
	// Token: 0x02000101 RID: 257
	[ComVisible(true)]
	public class DesignerVerbCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class.</summary>
		// Token: 0x06000A66 RID: 2662 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		public DesignerVerbCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class using the specified array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array that indicates the verbs to contain within the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000A67 RID: 2663 RVA: 0x0001D404 File Offset: 0x0001B604
		public DesignerVerbCollection(DesignerVerb[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at each valid index in the collection.</returns>
		/// <param name="index">The index at which to get or set the <see cref="T:System.ComponentModel.Design.DesignerVerb" />. </param>
		// Token: 0x17000265 RID: 613
		public DesignerVerb this[int index]
		{
			get
			{
				return (DesignerVerb)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to the collection.</summary>
		/// <returns>The index in the collection at which the verb was added.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add to the collection. </param>
		// Token: 0x06000A6A RID: 2666 RVA: 0x0001D43C File Offset: 0x0001B63C
		public int Add(DesignerVerb value)
		{
			return base.InnerList.Add(value);
		}

		/// <summary>Adds the specified set of designer verbs to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000A6B RID: 2667 RVA: 0x0001D44C File Offset: 0x0001B64C
		public void AddRange(DesignerVerb[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Adds the specified collection of designer verbs to the collection.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000A6C RID: 2668 RVA: 0x0001D45C File Offset: 0x0001B65C
		public void AddRange(DesignerVerbCollection value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> exists in the collection.</summary>
		/// <returns>true if the specified object exists in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to search for in the collection. </param>
		// Token: 0x06000A6D RID: 2669 RVA: 0x0001D46C File Offset: 0x0001B66C
		public bool Contains(DesignerVerb value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array beginning at the specified destination index.</summary>
		/// <param name="array">The array to copy collection members to. </param>
		/// <param name="index">The destination index to begin copying to. </param>
		// Token: 0x06000A6E RID: 2670 RVA: 0x0001D47C File Offset: 0x0001B67C
		public void CopyTo(DesignerVerb[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" />.</summary>
		/// <returns>The index of the specified object if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> whose index to get in the collection. </param>
		// Token: 0x06000A6F RID: 2671 RVA: 0x0001D48C File Offset: 0x0001B68C
		public int IndexOf(DesignerVerb value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <param name="index">The index in the collection at which to insert the verb. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to insert in the collection. </param>
		// Token: 0x06000A70 RID: 2672 RVA: 0x0001D49C File Offset: 0x0001B69C
		public void Insert(int index, DesignerVerb value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Raises the Clear event.</summary>
		// Token: 0x06000A71 RID: 2673 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		protected override void OnClear()
		{
		}

		/// <summary>Raises the Insert event.</summary>
		/// <param name="index">The index at which to insert an item. </param>
		/// <param name="value">The object to insert. </param>
		// Token: 0x06000A72 RID: 2674 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Raises the Remove event.</summary>
		/// <param name="index">The index at which to remove the item. </param>
		/// <param name="value">The object to remove. </param>
		// Token: 0x06000A73 RID: 2675 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Raises the Set event.</summary>
		/// <param name="index">The index at which to set the item. </param>
		/// <param name="oldValue">The old object. </param>
		/// <param name="newValue">The new object. </param>
		// Token: 0x06000A74 RID: 2676 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Raises the Validate event.</summary>
		/// <param name="value">The object to validate. </param>
		// Token: 0x06000A75 RID: 2677 RVA: 0x0001D4BC File Offset: 0x0001B6BC
		protected override void OnValidate(object value)
		{
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove from the collection. </param>
		// Token: 0x06000A76 RID: 2678 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		public void Remove(DesignerVerb value)
		{
			base.InnerList.Remove(value);
		}
	}
}
