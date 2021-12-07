using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted use of the <see cref="N:System.Reflection" /> and <see cref="N:System.Reflection.Emit" /> namespaces.</summary>
	// Token: 0x02000616 RID: 1558
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ReflectionPermissionFlag
	{
		/// <summary>Enumeration of types and members is allowed. Invocation operations are allowed on visible types and members.</summary>
		// Token: 0x040019CB RID: 6603
		NoFlags = 0,
		/// <summary>This flag is obsolete. No flags are necessary to enumerate types and members and to examine their metadata. Use <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.NoFlags" /> instead.</summary>
		// Token: 0x040019CC RID: 6604
		[Obsolete("not used anymore")]
		TypeInformation = 1,
		/// <summary>Invocation operations on all members are allowed, regardless of grant set. If this flag is not set, invocation operations are allowed only on visible members.</summary>
		// Token: 0x040019CD RID: 6605
		MemberAccess = 2,
		/// <summary>Emitting debug symbols is allowed. Beginning with the .NET Framework version 2.0 Service Pack 1, this flag is no longer required to emit code.</summary>
		// Token: 0x040019CE RID: 6606
		ReflectionEmit = 4,
		/// <summary>TypeInformation, MemberAccess, and ReflectionEmit are set. <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.AllFlags" /> does not include <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.RestrictedMemberAccess" />.</summary>
		// Token: 0x040019CF RID: 6607
		AllFlags = 7,
		/// <summary>Restricted member access is provided for partially trusted code. Partially trusted code can access nonpublic types and members, but only if the grant set of the partially trusted code includes all permissions in the grant set of the assembly that contains the nonpublic types and members being accessed. This flag is new in the .NET Framework 2.0 SP1.</summary>
		// Token: 0x040019D0 RID: 6608
		[ComVisible(false)]
		RestrictedMemberAccess = 8
	}
}
