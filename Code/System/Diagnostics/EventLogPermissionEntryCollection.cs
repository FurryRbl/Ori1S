using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000227 RID: 551
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x0003269C File Offset: 0x0003089C
		internal EventLogPermissionEntryCollection(EventLogPermission owner)
		{
			this.owner = owner;
			System.Security.Permissions.ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length > 0)
			{
				foreach (System.Security.Permissions.ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					EventLogPermissionAccess permissionAccess = (EventLogPermissionAccess)resourcePermissionBaseEntry.PermissionAccess;
					EventLogPermissionEntry value = new EventLogPermissionEntry(permissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0]);
					base.InnerList.Add(value);
				}
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000447 RID: 1095
		public EventLogPermissionEntry this[int index]
		{
			get
			{
				return (EventLogPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to this collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to add. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012C9 RID: 4809 RVA: 0x0003272C File Offset: 0x0003092C
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012CA RID: 4810 RVA: 0x0003273C File Offset: 0x0003093C
		public void AddRange(EventLogPermissionEntry[] value)
		{
			foreach (EventLogPermissionEntry value2 in value)
			{
				base.List.Add(value2);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.EventLogPermissionEntryCollection" /> that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012CB RID: 4811 RVA: 0x00032770 File Offset: 0x00030970
		public void AddRange(EventLogPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				EventLogPermissionEntry value2 = (EventLogPermissionEntry)obj;
				base.List.Add(value2);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012CC RID: 4812 RVA: 0x000327E0 File Offset: 0x000309E0
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that receives this collection's permission entries. </param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012CD RID: 4813 RVA: 0x000327F0 File Offset: 0x000309F0
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		/// <param name="value">The permission entry to search for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012CE RID: 4814 RVA: 0x00032800 File Offset: 0x00030A00
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry. </param>
		/// <param name="value">The permission entry to insert into this collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012CF RID: 4815 RVA: 0x00032810 File Offset: 0x00030A10
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x060012D0 RID: 4816 RVA: 0x00032820 File Offset: 0x00030A20
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x060012D1 RID: 4817 RVA: 0x00032830 File Offset: 0x00030A30
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />. </param>
		// Token: 0x060012D2 RID: 4818 RVA: 0x00032840 File Offset: 0x00030A40
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x060012D3 RID: 4819 RVA: 0x00032850 File Offset: 0x00030A50
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012D4 RID: 4820 RVA: 0x0003286C File Offset: 0x00030A6C
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x04000561 RID: 1377
		private EventLogPermission owner;
	}
}
