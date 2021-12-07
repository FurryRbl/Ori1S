using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000228 RID: 552
	[Serializable]
	public class EventLogPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.MachineName" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		// Token: 0x060012D5 RID: 4821 RVA: 0x0003287C File Offset: 0x00030A7C
		public EventLogPermissionEntry(EventLogPermissionAccess permissionAccess, string machineName)
		{
			System.Security.Permissions.ResourcePermissionBase.ValidateMachineName(machineName);
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
		}

		/// <summary>Gets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the computer on which to read or write events.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00032898 File Offset: 0x00030A98
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000328A0 File Offset: 0x00030AA0
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000328A8 File Offset: 0x00030AA8
		internal System.Security.Permissions.ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new System.Security.Permissions.ResourcePermissionBaseEntry((int)this.permissionAccess, new string[]
			{
				this.machineName
			});
		}

		// Token: 0x04000562 RID: 1378
		private EventLogPermissionAccess permissionAccess;

		// Token: 0x04000563 RID: 1379
		private string machineName;
	}
}
