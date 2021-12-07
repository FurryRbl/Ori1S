using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an audit rule for a file or directory. This class cannot be inherited.</summary>
	// Token: 0x02000576 RID: 1398
	public sealed class FileSystemAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using a reference to a user account, a value that specifies the type of operation associated with the audit rule, and a value that specifies when to perform auditing. </summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule. </param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identity" /> parameter is not an <see cref="T:System.Security.Principal.IdentityReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="flags" /> parameter.-or-The <see cref="F:System.Security.AccessControl.AuditFlags.None" /> value was passed to the <paramref name="flags" /> parameter.</exception>
		// Token: 0x06003640 RID: 13888 RVA: 0x000B1EAC File Offset: 0x000B00AC
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags) : this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using a user account name, a value that specifies the type of operation associated with the audit rule, and a value that specifies when to perform auditing.</summary>
		/// <param name="identity">The name of a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule. </param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="flags" /> parameter.-or-The <see cref="F:System.Security.AccessControl.AuditFlags.None" /> value was passed to the <paramref name="flags" /> parameter.</exception>
		// Token: 0x06003641 RID: 13889 RVA: 0x000B1EBC File Offset: 0x000B00BC
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags) : this(new SecurityIdentifier(identity), fileSystemRights, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using the name of a reference to a user account, a value that specifies the type of operation associated with the audit rule, a value that determines how rights are inherited, a value that determines how rights are propagated, and a value that specifies when to perform auditing.  </summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how access masks are propagated to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how Access Control Entries (ACEs) are propagated to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identity" /> parameter is not an <see cref="T:System.Security.Principal.IdentityReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="flags" /> parameter.-or-The <see cref="F:System.Security.AccessControl.AuditFlags.None" /> value was passed to the <paramref name="flags" /> parameter.</exception>
		// Token: 0x06003642 RID: 13890 RVA: 0x000B1ECC File Offset: 0x000B00CC
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, 0, false, inheritanceFlags, propagationFlags, flags)
		{
			this.rights = fileSystemRights;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using the name of a user account, a value that specifies the type of operation associated with the audit rule, a value that determines how rights are inherited, a value that determines how rights are propagated, and a value that specifies when to perform auditing. </summary>
		/// <param name="identity">The name of a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how access masks are propagated to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how Access Control Entries (ACEs) are propagated to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x06003643 RID: 13891 RVA: 0x000B1EE4 File Offset: 0x000B00E4
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(new SecurityIdentifier(identity), fileSystemRights, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Gets the <see cref="T:System.Security.AccessControl.FileSystemRights" /> flags associated with the current <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.FileSystemRights" /> flags associated with the current <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object.</returns>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000B1EF8 File Offset: 0x000B00F8
		public FileSystemRights FileSystemRights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x04001700 RID: 5888
		private FileSystemRights rights;
	}
}
