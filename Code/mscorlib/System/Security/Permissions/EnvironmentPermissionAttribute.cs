using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.EnvironmentPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005F3 RID: 1523
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06003A1B RID: 14875 RVA: 0x000C7ACC File Offset: 0x000C5CCC
		public EnvironmentPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Sets full access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000C7AD8 File Offset: 0x000C5CD8
		// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000C7AE4 File Offset: 0x000C5CE4
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets read access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for read access.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000C7AF4 File Offset: 0x000C5CF4
		// (set) Token: 0x06003A1F RID: 14879 RVA: 0x000C7AFC File Offset: 0x000C5CFC
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for write access.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000C7B08 File Offset: 0x000C5D08
		// (set) Token: 0x06003A21 RID: 14881 RVA: 0x000C7B10 File Offset: 0x000C5D10
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.EnvironmentPermission" />.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.EnvironmentPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003A22 RID: 14882 RVA: 0x000C7B1C File Offset: 0x000C5D1C
		public override IPermission CreatePermission()
		{
			EnvironmentPermission environmentPermission;
			if (base.Unrestricted)
			{
				environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
			}
			else
			{
				environmentPermission = new EnvironmentPermission(PermissionState.None);
				if (this.read != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Write, this.write);
				}
			}
			return environmentPermission;
		}

		// Token: 0x04001936 RID: 6454
		private string read;

		// Token: 0x04001937 RID: 6455
		private string write;
	}
}
