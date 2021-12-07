using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows control of code access permissions for event logging.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000226 RID: 550
	[Serializable]
	public sealed class EventLogPermission : System.Security.Permissions.ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x060012BC RID: 4796 RVA: 0x00032570 File Offset: 0x00030770
		public EventLogPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects. The <see cref="P:System.Diagnostics.EventLogPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x060012BD RID: 4797 RVA: 0x00032580 File Offset: 0x00030780
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x060012BE RID: 4798 RVA: 0x000325B8 File Offset: 0x000307B8
		public EventLogPermission(PermissionState state) : base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified access levels and the name of the computer to use.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. </param>
		// Token: 0x060012BF RID: 4799 RVA: 0x000325C8 File Offset: 0x000307C8
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.Add(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.EventLogPermissionEntryCollection" /> that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x000325F8 File Offset: 0x000307F8
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00032618 File Offset: 0x00030818
		private void SetUp()
		{
			base.TagNames = new string[]
			{
				"Machine"
			};
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003264C File Offset: 0x0003084C
		internal System.Security.Permissions.ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00032654 File Offset: 0x00030854
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003265C File Offset: 0x0003085C
		internal void Add(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.AddPermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003267C File Offset: 0x0003087C
		internal void Remove(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.RemovePermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04000560 RID: 1376
		private EventLogPermissionEntryCollection innerCollection;
	}
}
