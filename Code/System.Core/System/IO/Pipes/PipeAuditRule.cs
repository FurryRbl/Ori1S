using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	// Token: 0x0200006E RID: 110
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class PipeAuditRule : AuditRule
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00019668 File Offset: 0x00017868
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAuditRule(IdentityReference identity, PipeAccessRights rights, AuditFlags flags) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00019680 File Offset: 0x00017880
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAuditRule(string identity, PipeAccessRights rights, AuditFlags flags) : this(null, rights, flags)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00019698 File Offset: 0x00017898
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x000196A0 File Offset: 0x000178A0
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRights PipeAccessRights { get; private set; }
	}
}
