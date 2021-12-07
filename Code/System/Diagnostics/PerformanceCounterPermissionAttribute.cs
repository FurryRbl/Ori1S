using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive performance counter permission checks. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200023C RID: 572
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x060013BF RID: 5055 RVA: 0x0003478C File Offset: 0x0003298C
		public PerformanceCounterPermissionAttribute(SecurityAction action) : base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the performance counter category.</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x000347C0 File Offset: 0x000329C0
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x000347C8 File Offset: 0x000329C8
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CategoryName");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the computer name for the performance counter.</summary>
		/// <returns>The server on which the category of the performance counter resides.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.MachineName" /> format is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000347E4 File Offset: 0x000329E4
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x000347EC File Offset: 0x000329EC
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				System.Security.Permissions.ResourcePermissionBase.ValidateMachineName(value);
				this.machineName = value;
			}
		}

		/// <summary>Gets or sets the access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x000347FC File Offset: 0x000329FC
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00034804 File Offset: 0x00032A04
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		/// <summary>Creates the permission based on the requested access levels that are set through the <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060013C6 RID: 5062 RVA: 0x00034810 File Offset: 0x00032A10
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.permissionAccess, this.machineName, this.categoryName);
		}

		// Token: 0x040005B3 RID: 1459
		private string categoryName;

		// Token: 0x040005B4 RID: 1460
		private string machineName;

		// Token: 0x040005B5 RID: 1461
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
