using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an access rule for a file or directory. This class cannot be inherited.</summary>
	// Token: 0x02000575 RID: 1397
	public sealed class FileSystemAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class using a reference to a user account, a value that specifies the type of operation associated with the access rule, and a value that specifies whether to allow or deny the operation. </summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the access rule. </param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identity" /> parameter is not an <see cref="T:System.Security.Principal.IdentityReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="type " />parameter.</exception>
		// Token: 0x0600363B RID: 13883 RVA: 0x000B1E54 File Offset: 0x000B0054
		public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, AccessControlType type) : this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class using the name of a user account, a value that specifies the type of operation associated with the access rule, and a value that describes whether to allow or deny the operation. </summary>
		/// <param name="identity">The name of a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the access rule. </param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="type " />parameter.</exception>
		// Token: 0x0600363C RID: 13884 RVA: 0x000B1E64 File Offset: 0x000B0064
		public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, AccessControlType type) : this(new SecurityIdentifier(identity), fileSystemRights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class using a reference to a user account, a value that specifies the type of operation associated with the access rule, a value that determines how rights are inherited, a value that determines how rights are propagated, and a value that specifies whether to allow or deny the operation.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how access masks are propagated to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how Access Control Entries (ACEs) are propagated to child objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identity" /> parameter is not an <see cref="T:System.Security.Principal.IdentityReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="type " />parameter.-or-An incorrect enumeration was passed to the <paramref name="inheritanceFlags " />parameter.-or-An incorrect enumeration was passed to the <paramref name="propagationFlags " />parameter.</exception>
		// Token: 0x0600363D RID: 13885 RVA: 0x000B1E78 File Offset: 0x000B0078
		public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, (int)fileSystemRights, false, inheritanceFlags, propagationFlags, type)
		{
			this.rights = fileSystemRights;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class using the name of a user account, a value that specifies the type of operation associated with the access rule, a value that determines how rights are inherited, a value that determines how rights are propagated, and a value that specifies whether to allow or deny the operation.</summary>
		/// <param name="identity">The name of a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how access masks are propagated to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how Access Control Entries (ACEs) are propagated to child objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="type " />parameter.-or-An incorrect enumeration was passed to the <paramref name="inheritanceFlags " />parameter.-or-An incorrect enumeration was passed to the <paramref name="propagationFlags " />parameter.</exception>
		// Token: 0x0600363E RID: 13886 RVA: 0x000B1E90 File Offset: 0x000B0090
		public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new SecurityIdentifier(identity), fileSystemRights, inheritanceFlags, propagationFlags, type)
		{
		}

		/// <summary>Gets the <see cref="T:System.Security.AccessControl.FileSystemRights" /> flags associated with the current <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.FileSystemRights" /> flags associated with the current <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object.</returns>
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000B1EA4 File Offset: 0x000B00A4
		public FileSystemRights FileSystemRights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x040016FF RID: 5887
		private FileSystemRights rights;
	}
}
