using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023E RID: 574
	[Serializable]
	public class PerformanceCounterPermissionEntryCollection : CollectionBase
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0003497C File Offset: 0x00032B7C
		internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner)
		{
			this.owner = owner;
			System.Security.Permissions.ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length > 0)
			{
				foreach (System.Security.Permissions.ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					PerformanceCounterPermissionAccess permissionAccess = (PerformanceCounterPermissionAccess)resourcePermissionBaseEntry.PermissionAccess;
					string machineName = resourcePermissionBaseEntry.PermissionAccessPath[0];
					string categoryName = resourcePermissionBaseEntry.PermissionAccessPath[1];
					PerformanceCounterPermissionEntry value = new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName);
					base.InnerList.Add(value);
				}
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x000349F8 File Offset: 0x00032BF8
		internal PerformanceCounterPermissionEntryCollection(System.Security.Permissions.ResourcePermissionBaseEntry[] entries)
		{
			foreach (System.Security.Permissions.ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
			{
				base.List.Add(new PerformanceCounterPermissionEntry((PerformanceCounterPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0], resourcePermissionBaseEntry.PermissionAccessPath[1]));
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000493 RID: 1171
		public PerformanceCounterPermissionEntry this[int index]
		{
			get
			{
				return (PerformanceCounterPermissionEntry)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> to this collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to add. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013D5 RID: 5077 RVA: 0x00034A70 File Offset: 0x00032C70
		public int Add(PerformanceCounterPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013D6 RID: 5078 RVA: 0x00034A80 File Offset: 0x00032C80
		public void AddRange(PerformanceCounterPermissionEntry[] value)
		{
			foreach (PerformanceCounterPermissionEntry value2 in value)
			{
				base.List.Add(value2);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013D7 RID: 5079 RVA: 0x00034AB4 File Offset: 0x00032CB4
		public void AddRange(PerformanceCounterPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				PerformanceCounterPermissionEntry value2 = (PerformanceCounterPermissionEntry)obj;
				base.List.Add(value2);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</summary>
		/// <returns>true if the specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013D8 RID: 5080 RVA: 0x00034B24 File Offset: 0x00032D24
		public bool Contains(PerformanceCounterPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> that receives this collection's permission entries. </param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013D9 RID: 5081 RVA: 0x00034B34 File Offset: 0x00032D34
		public void CopyTo(PerformanceCounterPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		/// <param name="value">The permission entry for which to search. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013DA RID: 5082 RVA: 0x00034B44 File Offset: 0x00032D44
		public int IndexOf(PerformanceCounterPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry. </param>
		/// <param name="value">The permission entry to insert into this collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013DB RID: 5083 RVA: 0x00034B54 File Offset: 0x00032D54
		public void Insert(int index, PerformanceCounterPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x060013DC RID: 5084 RVA: 0x00034B64 File Offset: 0x00032D64
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x060013DD RID: 5085 RVA: 0x00034B74 File Offset: 0x00032D74
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />. </param>
		// Token: 0x060013DE RID: 5086 RVA: 0x00034B84 File Offset: 0x00032D84
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x060013DF RID: 5087 RVA: 0x00034B94 File Offset: 0x00032D94
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E0 RID: 5088 RVA: 0x00034BB0 File Offset: 0x00032DB0
		public void Remove(PerformanceCounterPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x040005B7 RID: 1463
		private PerformanceCounterPermission owner;
	}
}
