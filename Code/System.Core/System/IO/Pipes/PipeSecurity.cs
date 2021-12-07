using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	// Token: 0x02000076 RID: 118
	[MonoNotSupported("ACL is not supported in Mono")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public class PipeSecurity : NativeObjectSecurity
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x000196AC File Offset: 0x000178AC
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeSecurity() : base(false, ResourceType.FileObject)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000196C0 File Offset: 0x000178C0
		public override Type AccessRightType
		{
			get
			{
				return typeof(PipeAccessRights);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000196CC File Offset: 0x000178CC
		public override Type AccessRuleType
		{
			get
			{
				return typeof(PipeAccessRule);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000196D8 File Offset: 0x000178D8
		public override Type AuditRuleType
		{
			get
			{
				return typeof(PipeAuditRule);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000196E4 File Offset: 0x000178E4
		[MonoNotSupported("ACL is not supported in Mono")]
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000196F0 File Offset: 0x000178F0
		[MonoNotSupported("ACL is not supported in Mono")]
		public void AddAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000196FC File Offset: 0x000178FC
		[MonoNotSupported("ACL is not supported in Mono")]
		public void AddAuditRule(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00019708 File Offset: 0x00017908
		[MonoNotSupported("ACL is not supported in Mono")]
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019714 File Offset: 0x00017914
		[MonoNotSupported("ACL is not supported in Mono")]
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		protected internal void Persist(SafeHandle handle)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00019720 File Offset: 0x00017920
		[MonoNotSupported("ACL is not supported in Mono")]
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		protected internal void Persist(string name)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001972C File Offset: 0x0001792C
		[MonoNotSupported("ACL is not supported in Mono")]
		public bool RemoveAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019738 File Offset: 0x00017938
		[MonoNotSupported("ACL is not supported in Mono")]
		public void RemoveAccessRuleSpecific(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019744 File Offset: 0x00017944
		[MonoNotSupported("ACL is not supported in Mono")]
		public bool RemoveAuditRule(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019750 File Offset: 0x00017950
		[MonoNotSupported("ACL is not supported in Mono")]
		public void RemoveAuditRuleAll(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001975C File Offset: 0x0001795C
		[MonoNotSupported("ACL is not supported in Mono")]
		public void RemoveAuditRuleSpecific(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019768 File Offset: 0x00017968
		[MonoNotSupported("ACL is not supported in Mono")]
		public void ResetAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019774 File Offset: 0x00017974
		[MonoNotSupported("ACL is not supported in Mono")]
		public void SetAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00019780 File Offset: 0x00017980
		[MonoNotSupported("ACL is not supported in Mono")]
		public void SetAuditRule(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}
	}
}
