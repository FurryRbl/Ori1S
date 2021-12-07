using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	// Token: 0x0200006D RID: 109
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class PipeAccessRule : AccessRule
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x00019624 File Offset: 0x00017824
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRule(IdentityReference identity, PipeAccessRights rights, AccessControlType type) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001963C File Offset: 0x0001783C
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRule(string identity, PipeAccessRights rights, AccessControlType type) : this(null, rights, type)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00019654 File Offset: 0x00017854
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001965C File Offset: 0x0001785C
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRights PipeAccessRights { get; private set; }
	}
}
