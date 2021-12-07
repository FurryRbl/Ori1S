using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for a <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023F RID: 575
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The <see cref="P:System.Diagnostics.PerformanceCounterPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The server on which the category of the performance counter resides. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null.-or-<paramref name="machineName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionAccess" /> is not a valid <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> value.-or-<paramref name="machineName" /> is not a valid computer name.</exception>
		// Token: 0x060013E1 RID: 5089 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public PerformanceCounterPermissionEntry(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if ((permissionAccess | PerformanceCounterPermissionAccess.Administer) != PerformanceCounterPermissionAccess.Administer)
			{
				throw new ArgumentException("permissionAccess");
			}
			System.Security.Permissions.ResourcePermissionBase.ValidateMachineName(machineName);
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
			this.categoryName = categoryName;
		}

		/// <summary>Gets the name of the performance counter category (performance object).</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00034C24 File Offset: 0x00032E24
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		/// <summary>Gets the name of the server on which the category of the performance counter resides.</summary>
		/// <returns>The name of the server on which the category resides.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00034C2C File Offset: 0x00032E2C
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access level of the entry.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00034C34 File Offset: 0x00032E34
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00034C3C File Offset: 0x00032E3C
		internal System.Security.Permissions.ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new System.Security.Permissions.ResourcePermissionBaseEntry((int)this.permissionAccess, new string[]
			{
				this.machineName,
				this.categoryName
			});
		}

		// Token: 0x040005B8 RID: 1464
		private const PerformanceCounterPermissionAccess All = PerformanceCounterPermissionAccess.Administer;

		// Token: 0x040005B9 RID: 1465
		private PerformanceCounterPermissionAccess permissionAccess;

		// Token: 0x040005BA RID: 1466
		private string machineName;

		// Token: 0x040005BB RID: 1467
		private string categoryName;
	}
}
