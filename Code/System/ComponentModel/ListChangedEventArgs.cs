using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event.</summary>
	// Token: 0x0200017D RID: 381
	public class ListChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the index of the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added, changed, or removed.</param>
		// Token: 0x06000D15 RID: 3349 RVA: 0x00020C74 File Offset: 0x0001EE74
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex) : this(listChangedType, newIndex, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, removed, or changed.</param>
		// Token: 0x06000D16 RID: 3350 RVA: 0x00020C80 File Offset: 0x0001EE80
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.changedType = listChangedType;
			this.propDesc = propDesc;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the old and new index of the item that was moved.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The new index of the item that was moved.</param>
		/// <param name="oldIndex">The old index of the item that was moved.</param>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00020C98 File Offset: 0x0001EE98
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.changedType = listChangedType;
			this.newIndex = newIndex;
			this.oldIndex = oldIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change, the index of the affected item, and a <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added or changed.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the item.</param>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc)
		{
			this.changedType = listChangedType;
			this.newIndex = newIndex;
			this.oldIndex = newIndex;
			this.propDesc = propDesc;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public ListChangedType ListChangedType
		{
			get
			{
				return this.changedType;
			}
		}

		/// <summary>Gets the old index of an item that has been moved.</summary>
		/// <returns>The old index of the moved item.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		public int OldIndex
		{
			get
			{
				return this.oldIndex;
			}
		}

		/// <summary>Gets the index of the item affected by the change.</summary>
		/// <returns>The index of the affected by the change.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00020CF8 File Offset: 0x0001EEF8
		public int NewIndex
		{
			get
			{
				return this.newIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, changed, or deleted.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected by the change.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00020D00 File Offset: 0x0001EF00
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propDesc;
			}
		}

		// Token: 0x04000390 RID: 912
		private ListChangedType changedType;

		// Token: 0x04000391 RID: 913
		private int oldIndex;

		// Token: 0x04000392 RID: 914
		private int newIndex;

		// Token: 0x04000393 RID: 915
		private PropertyDescriptor propDesc;
	}
}
