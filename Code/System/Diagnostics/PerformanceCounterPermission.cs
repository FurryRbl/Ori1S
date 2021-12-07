using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows control of code access permissions for <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023D RID: 573
	[Serializable]
	public sealed class PerformanceCounterPermission : System.Security.Permissions.ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class.</summary>
		// Token: 0x060013C7 RID: 5063 RVA: 0x0003483C File Offset: 0x00032A3C
		public PerformanceCounterPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects. The <see cref="P:System.Diagnostics.PerformanceCounterPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x060013C8 RID: 5064 RVA: 0x0003484C File Offset: 0x00032A4C
		public PerformanceCounterPermission(PerformanceCounterPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x060013C9 RID: 5065 RVA: 0x00034884 File Offset: 0x00032A84
		public PerformanceCounterPermission(PermissionState state) : base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified access levels, the name of the computer to use, and the category associated with the performance counter.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. </param>
		/// <param name="machineName">The server on which the performance counter and its associate category reside. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which the performance counter is associated. </param>
		// Token: 0x060013CA RID: 5066 RVA: 0x00034894 File Offset: 0x00032A94
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.Add(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x000348D0 File Offset: 0x00032AD0
		public PerformanceCounterPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000348F0 File Offset: 0x00032AF0
		private void SetUp()
		{
			base.TagNames = new string[]
			{
				"Machine",
				"Category"
			};
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003492C File Offset: 0x00032B2C
		internal System.Security.Permissions.ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00034934 File Offset: 0x00032B34
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003493C File Offset: 0x00032B3C
		internal void Add(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.AddPermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003495C File Offset: 0x00032B5C
		internal void Remove(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.RemovePermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x040005B6 RID: 1462
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
