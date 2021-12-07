using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the access rights to use when creating access and audit rules. </summary>
	// Token: 0x02000577 RID: 1399
	[Flags]
	public enum FileSystemRights
	{
		/// <summary>Specifies the right to read the contents of a directory.</summary>
		// Token: 0x04001702 RID: 5890
		ListDirectory = 1,
		/// <summary>Specifies the right to open and copy a file or folder.  This does not include the right to read file system attributes, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x04001703 RID: 5891
		ReadData = 1,
		/// <summary>Specifies the right to create a file.  </summary>
		// Token: 0x04001704 RID: 5892
		CreateFiles = 2,
		/// <summary>Specifies the right to open and write to a file or folder.  This does not include the right to open and write file system attributes, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x04001705 RID: 5893
		WriteData = 2,
		/// <summary>Specifies the right to append data to the end of a file.</summary>
		// Token: 0x04001706 RID: 5894
		AppendData = 4,
		/// <summary>Specifies the right to create a folder.  </summary>
		// Token: 0x04001707 RID: 5895
		CreateDirectories = 4,
		/// <summary>Specifies the right to open and copy extended file system attributes from a folder or file.  For example, this value specifies the right to view author and content information.  This does not include the right to read data, file system attributes, or access and audit rules.</summary>
		// Token: 0x04001708 RID: 5896
		ReadExtendedAttributes = 8,
		/// <summary>Specifies the right to open and write extended file system attributes to a folder or file.  This does not include the ability to write data, attributes, or access and audit rules.</summary>
		// Token: 0x04001709 RID: 5897
		WriteExtendedAttributes = 16,
		/// <summary>Specifies the right to run an application file.</summary>
		// Token: 0x0400170A RID: 5898
		ExecuteFile = 32,
		/// <summary>Specifies the right to list the contents of a folder and to run applications contained within that folder.</summary>
		// Token: 0x0400170B RID: 5899
		Traverse = 32,
		/// <summary>Specifies the right to delete a folder and any files contained within that folder.</summary>
		// Token: 0x0400170C RID: 5900
		DeleteSubdirectoriesAndFiles = 64,
		/// <summary>Specifies the right to open and copy file system attributes from a folder or file.  For example, this value specifies the right to view the file creation or modified date.  This does not include the right to read data, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x0400170D RID: 5901
		ReadAttributes = 128,
		/// <summary>Specifies the right to open and write file system attributes to a folder or file. This does not include the ability to write data, extended attributes, or access and audit rules.</summary>
		// Token: 0x0400170E RID: 5902
		WriteAttributes = 256,
		/// <summary>Specifies the right to create folders and files, and to add or remove data from files.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.WriteData" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.AppendData" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.WriteExtendedAttributes" /> right, and <see cref="F:System.Security.AccessControl.FileSystemRights.WriteAttributes" /> right. </summary>
		// Token: 0x0400170F RID: 5903
		Write = 278,
		/// <summary>Specifies the right to delete a folder or file. </summary>
		// Token: 0x04001710 RID: 5904
		Delete = 65536,
		/// <summary>Specifies the right to open and copy access and audit rules from a folder or file.  This does not include the right to read data, file system attributes, and extended file system attributes. </summary>
		// Token: 0x04001711 RID: 5905
		ReadPermissions = 131072,
		/// <summary>Specifies the right to open and copy folders or files as read-only.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.ReadData" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.ReadExtendedAttributes" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.ReadAttributes" /> right, and <see cref="F:System.Security.AccessControl.FileSystemRights.ReadPermissions" /> right.</summary>
		// Token: 0x04001712 RID: 5906
		Read = 131209,
		/// <summary>Specifies the right to open and copy folders or files as read-only, and to run application files.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.Read" /> right and the <see cref="F:System.Security.AccessControl.FileSystemRights.ExecuteFile" /> right.</summary>
		// Token: 0x04001713 RID: 5907
		ReadAndExecute = 131241,
		/// <summary>Specifies the right to read, write, list folder contents, delete folders and files, and run application files.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.ReadAndExecute" /> right, the <see cref="F:System.Security.AccessControl.FileSystemRights.Write" /> right, and the <see cref="F:System.Security.AccessControl.FileSystemRights.Delete" /> right.</summary>
		// Token: 0x04001714 RID: 5908
		Modify = 197055,
		/// <summary>Specifies the right to change the security and audit rules associated with a file or folder.</summary>
		// Token: 0x04001715 RID: 5909
		ChangePermissions = 262144,
		/// <summary>Specifies the right to change the owner of a folder or file.  Note that owners of a resource have full access to that resource.</summary>
		// Token: 0x04001716 RID: 5910
		TakeOwnership = 524288,
		/// <summary>Specifies whether the application can wait for a file handle to synchronize with the completion of an I/O operation.</summary>
		// Token: 0x04001717 RID: 5911
		Synchronize = 1048576,
		/// <summary>Specifies the right to exert full control over a folder or file, and to modify access control and audit rules.  This value represents the right to do anything with a file and is the combination of all rights in this enumeration.</summary>
		// Token: 0x04001718 RID: 5912
		FullControl = 2032127
	}
}
