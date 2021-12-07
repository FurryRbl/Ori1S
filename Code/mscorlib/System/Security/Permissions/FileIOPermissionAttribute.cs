using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileIOPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005F9 RID: 1529
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06003A5A RID: 14938 RVA: 0x000C8BF8 File Offset: 0x000C6DF8
		public FileIOPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets full access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x000C8C04 File Offset: 0x000C6E04
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x000C8C10 File Offset: 0x000C6E10
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets append access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for append access.</returns>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000C8C30 File Offset: 0x000C6E30
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x000C8C38 File Offset: 0x000C6E38
		public string Append
		{
			get
			{
				return this.append;
			}
			set
			{
				this.append = value;
			}
		}

		/// <summary>Gets or sets the file or directory to which to grant path discovery.</summary>
		/// <returns>The absolute path of the file or directory.</returns>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000C8C44 File Offset: 0x000C6E44
		// (set) Token: 0x06003A60 RID: 14944 RVA: 0x000C8C4C File Offset: 0x000C6E4C
		public string PathDiscovery
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		/// <summary>Gets or sets read access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for read access.</returns>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000C8C58 File Offset: 0x000C6E58
		// (set) Token: 0x06003A62 RID: 14946 RVA: 0x000C8C60 File Offset: 0x000C6E60
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

		/// <summary>Gets or sets write access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for write access.</returns>
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000C8C6C File Offset: 0x000C6E6C
		// (set) Token: 0x06003A64 RID: 14948 RVA: 0x000C8C74 File Offset: 0x000C6E74
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

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000C8C80 File Offset: 0x000C6E80
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000C8C88 File Offset: 0x000C6E88
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.allFiles;
			}
			set
			{
				this.allFiles = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all local files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000C8C94 File Offset: 0x000C6E94
		// (set) Token: 0x06003A68 RID: 14952 RVA: 0x000C8C9C File Offset: 0x000C6E9C
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.allLocalFiles;
			}
			set
			{
				this.allLocalFiles = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be changed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be changed.</returns>
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000C8CA8 File Offset: 0x000C6EA8
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000C8CB0 File Offset: 0x000C6EB0
		public string ChangeAccessControl
		{
			get
			{
				return this.changeAccessControl;
			}
			set
			{
				this.changeAccessControl = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be viewed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be viewed.</returns>
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000C8CBC File Offset: 0x000C6EBC
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x000C8CC4 File Offset: 0x000C6EC4
		public string ViewAccessControl
		{
			get
			{
				return this.viewAccessControl;
			}
			set
			{
				this.viewAccessControl = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which file data can be viewed and modified.</summary>
		/// <returns>The absolute path of the file or directory in which file data can be viewed and modified.</returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called. The accessor is provided only for C# compiler compatibility.</exception>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000C8CD0 File Offset: 0x000C6ED0
		// (set) Token: 0x06003A6E RID: 14958 RVA: 0x000C8CD8 File Offset: 0x000C6ED8
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileIOPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The path information for a file or directory for which access is to be secured contains invalid characters or wildcard specifiers. </exception>
		// Token: 0x06003A6F RID: 14959 RVA: 0x000C8CF8 File Offset: 0x000C6EF8
		public override IPermission CreatePermission()
		{
			FileIOPermission fileIOPermission;
			if (base.Unrestricted)
			{
				fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			}
			else
			{
				fileIOPermission = new FileIOPermission(PermissionState.None);
				if (this.append != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Append, this.append);
				}
				if (this.path != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.PathDiscovery, this.path);
				}
				if (this.read != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Write, this.write);
				}
			}
			return fileIOPermission;
		}

		// Token: 0x04001952 RID: 6482
		private string append;

		// Token: 0x04001953 RID: 6483
		private string path;

		// Token: 0x04001954 RID: 6484
		private string read;

		// Token: 0x04001955 RID: 6485
		private string write;

		// Token: 0x04001956 RID: 6486
		private FileIOPermissionAccess allFiles;

		// Token: 0x04001957 RID: 6487
		private FileIOPermissionAccess allLocalFiles;

		// Token: 0x04001958 RID: 6488
		private string changeAccessControl;

		// Token: 0x04001959 RID: 6489
		private string viewAccessControl;
	}
}
