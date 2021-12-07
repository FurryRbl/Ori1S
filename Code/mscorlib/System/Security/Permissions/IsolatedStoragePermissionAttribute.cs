using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x02000605 RID: 1541
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStoragePermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000C994C File Offset: 0x000C7B4C
		protected IsolatedStoragePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the level of isolated storage that should be declared.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> values.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000C9958 File Offset: 0x000C7B58
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x000C9960 File Offset: 0x000C7B60
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.usage_allowed;
			}
			set
			{
				this.usage_allowed = value;
			}
		}

		/// <summary>Gets or sets the maximum user storage quota size.</summary>
		/// <returns>The maximum user storage quota size in bytes.</returns>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000C996C File Offset: 0x000C7B6C
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000C9974 File Offset: 0x000C7B74
		public long UserQuota
		{
			get
			{
				return this.user_quota;
			}
			set
			{
				this.user_quota = value;
			}
		}

		// Token: 0x04001990 RID: 6544
		private IsolatedStorageContainment usage_allowed;

		// Token: 0x04001991 RID: 6545
		private long user_quota;
	}
}
